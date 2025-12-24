namespace SmartFactory.WinApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvSensors = new System.Windows.Forms.DataGridView();
            this.monitoringTimer = new System.Windows.Forms.Timer(this.components);
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.groupIntervention = new System.Windows.Forms.GroupBox();
            this.lblSelectedInfo = new System.Windows.Forms.Label();
            this.numPerformance = new System.Windows.Forms.NumericUpDown();
            this.lblPerf = new System.Windows.Forms.Label();
            this.btnApplyIntervention = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.lblConsole = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSensors)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.groupIntervention.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPerformance)).BeginInit();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Size = new System.Drawing.Size(850, 60);

            // lblTitle
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Text = "SmartFactory Management Cockpit";

            // dgvSensors (Dashboard)
            this.dgvSensors.AllowUserToAddRows = false;
            this.dgvSensors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSensors.BackgroundColor = System.Drawing.Color.White;
            this.dgvSensors.ColumnHeadersHeight = 30;
            this.dgvSensors.Location = new System.Drawing.Point(12, 100);
            this.dgvSensors.Name = "dgvSensors";
            this.dgvSensors.ReadOnly = true;
            this.dgvSensors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSensors.Size = new System.Drawing.Size(550, 280);
            this.dgvSensors.SelectionChanged += new System.EventHandler(this.dgvSensors_SelectionChanged);

            // groupIntervention (Cockpit SOAP)
            this.groupIntervention.Controls.Add(this.btnApplyIntervention);
            this.groupIntervention.Controls.Add(this.lblPerf);
            this.groupIntervention.Controls.Add(this.numPerformance);
            this.groupIntervention.Controls.Add(this.lblSelectedInfo);
            this.groupIntervention.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupIntervention.Location = new System.Drawing.Point(580, 100);
            this.groupIntervention.Size = new System.Drawing.Size(250, 280);
            this.groupIntervention.Text = "CONTROLO SOAP (WCF)";

            this.lblSelectedInfo.Location = new System.Drawing.Point(15, 40);
            this.lblSelectedInfo.Size = new System.Drawing.Size(220, 40);
            this.lblSelectedInfo.Text = "Máquina: Nenhuma";

            this.lblPerf.Location = new System.Drawing.Point(15, 100);
            this.lblPerf.Text = "Ajustar Performance (%):";

            this.numPerformance.Location = new System.Drawing.Point(15, 125);
            this.numPerformance.Size = new System.Drawing.Size(100, 25);
            this.numPerformance.Value = 100;

            this.btnApplyIntervention.BackColor = System.Drawing.Color.DarkOrange;
            this.btnApplyIntervention.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyIntervention.Location = new System.Drawing.Point(15, 170);
            this.btnApplyIntervention.Size = new System.Drawing.Size(220, 45);
            this.btnApplyIntervention.Text = "APLICAR COMANDO";
            this.btnApplyIntervention.Click += new System.EventHandler(this.btnApplyIntervention_Click);

            // Console de Auditoria
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.ForeColor = System.Drawing.Color.Lime;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtConsole.Location = new System.Drawing.Point(12, 410);
            this.txtConsole.Multiline = true;
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(820, 120);

            // lblLastUpdate
            this.lblLastUpdate.Location = new System.Drawing.Point(12, 75);
            this.lblLastUpdate.Text = "A aguardar REST...";

            // Form1
            this.ClientSize = new System.Drawing.Size(850, 550);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.groupIntervention);
            this.Controls.Add(this.dgvSensors);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblLastUpdate);
            this.Name = "Form1";
            this.Text = "SmartFactory v2.0 - Cockpit de Gestão";
        }
    }
}