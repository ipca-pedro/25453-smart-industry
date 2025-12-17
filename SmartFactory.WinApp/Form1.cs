using System;
using System.Windows.Forms;
using SmartFactory.WinApp.ServiceRef;

namespace SmartFactory.WinApp
{
    public partial class Form1 : Form
    {
        private MachineServiceClient cliente = new MachineServiceClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void CarregarDados()
        {
            try
            {
                MachineRule[] regras = cliente.GetAllRules();
                dataGridView1.DataSource = regras;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao ligar ao servidor: " + ex.Message);
            }
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            try
            {
                MachineRule novaRegra = new MachineRule();
                // CORREÇÃO: Alinhado com a coluna rule_name do SQL
                novaRegra.RuleName = txtDescricao.Text;
                // CORREÇÃO: Alinhado com a coluna threshold_value do SQL
                novaRegra.ThresholdValue = double.Parse(txtLimite.Text);

                // Campos adicionais presentes na tabela machine_rules
                novaRegra.TargetSensorId = txtPolo.Text; // Usando txtPolo para o sensor como exemplo
                novaRegra.IsActive = true;

                string resposta = cliente.CreateNewRule(novaRegra);

                MessageBox.Show(resposta);
                CarregarDados();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar: Verifique os dados.\n" + ex.Message);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Selecione uma regra ou escreva o ID.");
                    return;
                }

                int idParaAtualizar = int.Parse(txtId.Text);
                double novoLimite = double.Parse(txtLimite.Text);
                string novoNome = txtDescricao.Text;

                // CORREÇÃO: Passando os parâmetros com os nomes lógicos corretos
                string resposta = cliente.UpdateMachineRule(idParaAtualizar, novoLimite, novoNome);

                MessageBox.Show(resposta);
                CarregarDados();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar: " + ex.Message);
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text)) return;

                int idParaApagar = int.Parse(txtId.Text);
                DialogResult confirmacao = MessageBox.Show("Apagar regra " + idParaApagar + "?", "Confirmar", MessageBoxButtons.YesNo);

                if (confirmacao == DialogResult.Yes)
                {
                    string resposta = cliente.DeleteMachineRule(idParaApagar);
                    MessageBox.Show(resposta);
                    CarregarDados();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dataGridView1.Rows[e.RowIndex];

                // CORREÇÃO: Os nomes das colunas na Grelha devem bater com a Model
                txtId.Text = linha.Cells["Id"].Value.ToString();
                txtDescricao.Text = linha.Cells["RuleName"].Value.ToString();
                txtLimite.Text = linha.Cells["ThresholdValue"].Value.ToString();
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtDescricao.Clear();
            txtLimite.Clear();
            txtPolo.Clear();
        }
    }
}