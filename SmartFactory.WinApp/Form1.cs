using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using SmartFactory.Models;
using SmartFactory.WinApp.ServiceRef;

namespace SmartFactory.WinApp
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _restClient = new HttpClient();
        // AJUSTA O PORTO PARA O DA TUA API
        private const string API_URL = "http://localhost:50442/api/sensors";

        public Form1()
        {
            InitializeComponent();
            SetupDashboard();
        }

        private void SetupDashboard()
        {
            // Inicia o Timer de 5 segundos para a monitorização REST
            monitoringTimer.Interval = 5000;
            monitoringTimer.Start();
            txtConsole.AppendText($"[{DateTime.Now:HH:mm:ss}] Sistema Iniciado. Monitorização REST ativa.\r\n");
        }

        // --- CAMADA DE LEITURA (REST API) ---
        private async void monitoringTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var response = await _restClient.GetStringAsync(API_URL);
                var sensors = JsonConvert.DeserializeObject<List<SensorData>>(response);

                dgvSensors.DataSource = sensors;

                // Estilização Industrial
                foreach (DataGridViewRow row in dgvSensors.Rows)
                {
                    double valor = Convert.ToDouble(row.Cells["Valor"].Value);
                    // Se a temperatura/valor for crítico (>50), destaca a linha
                    if (valor > 50)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 210, 210); // Vermelho suave
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                lblLastUpdate.Text = "Última Leitura REST: " + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                txtConsole.AppendText($"[ERRO REST] {ex.Message}\r\n");
            }
        }

        // --- CAMADA DE ATUAÇÃO (SOAP WCF) ---
        private void btnApplyIntervention_Click(object sender, EventArgs e)
        {
            if (dgvSensors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma máquina na tabela para intervir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dados da Máquina Selecionada
            string machineName = dgvSensors.SelectedRows[0].Cells["SensorId"].Value.ToString();
            double newThreshold = (double)numPerformance.Value;

            // Numa App Real, buscaríamos o ID da regra associada à máquina. 
            // Para teste, assumimos ID 1 ou um ID fixo da tua BD.
            int ruleId = 1;

            var confirm = MessageBox.Show($"Confirmar ajuste de performance para {machineName}?", "Confirmação SOAP", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var soapClient = new MachineServiceClient())
                    {
                        // INVOCAÇÃO SOAP TRANSACIONAL (Update + Log na BD)
                        bool success = soapClient.SetMachinePerformance(ruleId, newThreshold, machineName);

                        if (success)
                        {
                            txtConsole.AppendText($"[{DateTime.Now:HH:mm:ss}] SOAP SUCCESS: {machineName} ajustada para {newThreshold}%\r\n");
                            MessageBox.Show("Intervenção registada e executada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtConsole.AppendText($"[{DateTime.Now:HH:mm:ss}] SOAP ERROR: Falha na transação na BD.\r\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro de comunicação SOAP: " + ex.Message);
                }
            }
        }

        private void dgvSensors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSensors.SelectedRows.Count > 0)
            {
                string id = dgvSensors.SelectedRows[0].Cells["SensorId"].Value.ToString();
                lblSelectedInfo.Text = $"MÁQUINA SELECIONADA: {id}";
                btnApplyIntervention.Enabled = true;
            }
        }
    }
}