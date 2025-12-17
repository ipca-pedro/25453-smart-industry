using System;
using System.Windows.Forms;
using SmartFactory.WinApp.ServiceRef;

namespace SmartFactory.WinApp
{
    public partial class Form1 : Form
    {
        // Cliente SOAP instanciado à moda antiga (Legacy Style)
        private MachineServiceClient cliente = new MachineServiceClient();

        public Form1()
        {
            InitializeComponent();
        }

        // Evento que corre quando a App abre
        private void Form1_Load(object sender, EventArgs e)
        {
            // Opcional: Carregar a lista logo ao iniciar
            CarregarDados();
        }

        // --- BOTÃO LISTAR (READ) ---
        private void btnListar_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void CarregarDados()
        {
            try
            {
                // Chama o serviço SOAP e mete os dados na grelha automaticamente
                MachineRule[] regras = cliente.GetAllRules();
                dataGridView1.DataSource = regras;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao ligar ao servidor: " + ex.Message);
            }
        }

        // --- BOTÃO CRIAR (CREATE) ---
        private void btnCriar_Click(object sender, EventArgs e)
        {
            try
            {
                // Criar o objeto para enviar
                MachineRule novaRegra = new MachineRule();
                novaRegra.Descricao = txtDescricao.Text;
                // Parse simples (Cuidado: num projeto real validar se é número)
                novaRegra.LimiteAtivacao = double.Parse(txtLimite.Text);
                novaRegra.Polo = txtPolo.Text;

                // Enviar para o servidor
                string resposta = cliente.CreateNewRule(novaRegra);

                MessageBox.Show(resposta);
                CarregarDados(); // Atualiza a lista
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar: Verifique se os números estão corretos.\n" + ex.Message);
            }
        }

        // --- BOTÃO ATUALIZAR (UPDATE) ---
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Selecione uma regra ou escreva o ID para atualizar.");
                    return;
                }

                int idParaAtualizar = int.Parse(txtId.Text);
                double novoLimite = double.Parse(txtLimite.Text);
                string novaDescricao = txtDescricao.Text;

                // Chama o novo método que criámos no serviço
                string resposta = cliente.UpdateMachineRule(idParaAtualizar, novoLimite, novaDescricao);

                MessageBox.Show(resposta);
                CarregarDados();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar: " + ex.Message);
            }
        }

        // --- BOTÃO APAGAR (DELETE) ---
        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Escreva o ID da regra que quer apagar.");
                    return;
                }

                int idParaApagar = int.Parse(txtId.Text);

                // Confirmação de segurança
                DialogResult confirmacao = MessageBox.Show(
                    "Tem a certeza que quer apagar a regra " + idParaApagar + "?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

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

        // --- EXTRAS: Preencher caixas ao clicar na grelha ---
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar erro se clicar no cabeçalho
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dataGridView1.Rows[e.RowIndex];

                // Preencher as caixas com os dados da linha selecionada
                txtId.Text = linha.Cells["RuleId"].Value.ToString();
                txtDescricao.Text = linha.Cells["Descricao"].Value.ToString();
                txtLimite.Text = linha.Cells["LimiteAtivacao"].Value.ToString();
                txtPolo.Text = linha.Cells["Polo"].Value.ToString();
            }
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtDescricao.Text = "";
            txtLimite.Text = "";
            txtPolo.Text = "";
        }
    }
}