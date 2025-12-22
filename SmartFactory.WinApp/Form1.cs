using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using SmartFactory.Models; // Classes SensorData e MachineRule
using SmartFactory.WinApp.ServiceRef; // O nome da tua Service Reference

namespace SmartFactory.WinApp
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _restClient = new HttpClient();
        private const string API_URL = "http://localhost:XXXX/api/sensors"; // Ajusta o Porto aqui!

        public Form1()
        {
            InitializeComponent();
            monitoringTimer.Start(); // Inicia a monitorização automática
        }

        // --- MONITORIZAÇÃO (REST) ---
        private async void monitoringTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Busca dados JSON da API REST (Projeto 1)
                var response = await _restClient.GetStringAsync(API_URL);
                var sensors = JsonConvert.DeserializeObject<List<SensorData>>(response);

                dgvSensors.DataSource = sensors;

                // Alerta Visual: Se a temperatura estiver alta, pinta de vermelho
                foreach (DataGridViewRow row in dgvSensors.Rows)
                {
                    if (row.Cells["Valor"].Value != null && Convert.ToDouble(row.Cells["Valor"].Value) > 50)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
                lblLastUpdate.Text = "Última atualização: " + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                txtConsole.AppendText($"[REST ERROR] {ex.Message}\r\n");
            }
        }

        // --- INTERVENÇÃO MANUAL (SOAP) ---
        private void btnApplyPerformance_Click(object sender, EventArgs e)
        {
            if (dgvSensors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma máquina na tabela primeiro.");
                return;
            }

            // Extrai dados da linha selecionada
            string machineName = dgvSensors.SelectedRows[0].Cells["SensorId"].Value.ToString();
            double newPerf = (double)numPerformance.Value;

            // Nota: Numa App real, o ruleId viria da BD. Aqui usamos 1 para teste.
            int ruleId = 1;

            try
            {
                using (var soapClient = new MachineServiceClient())
                {
                    // Invoca o serviço SOAP que executa o SQL Hardcoded + Log
                    bool success = soapClient.SetMachinePerformance(ruleId, newPerf, machineName);

                    if (success)
                    {
                        txtConsole.AppendText($"[{DateTime.Now:HH:mm}] SOAP: Performance de {machineName} alterada para {newPerf}%\r\n");
                        MessageBox.Show($"Intervenção registada com sucesso para {machineName}!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao processar transação na base de dados.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão SOAP: " + ex.Message);
            }
        }

        private void dgvSensors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSensors.SelectedRows.Count > 0)
            {
                lblSelectedMachine.Text = "Máquina: " + dgvSensors.SelectedRows[0].Cells["SensorId"].Value.ToString();
            }
        }
    }
}