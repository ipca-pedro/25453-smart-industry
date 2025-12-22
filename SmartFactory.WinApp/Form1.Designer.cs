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
            this.btnApplyPerformance = new System.Windows.Forms.Button();
            this.numPerformance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSelectedMachine = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSensors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPerformance)).BeginInit();
            this.SuspendLayout();

            // dgvSensors
            this.dgvSensors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSensors.Location = new System.Drawing.Point(12, 40);
            this.dgvSensors.MultiSelect = false;
            this.dgvSensors.Name = "dgvSensors";
            this.dgvSensors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSensors.Size = new System.Drawing.Size(540, 250);
            this.dgvSensors.SelectionChanged += new System.EventHandler(this.dgvSensors_SelectionChanged);

            // monitoringTimer
            this.monitoringTimer.Interval = 5000; // 5 segundos
            this.monitoringTimer.Tick += new System.EventHandler(this.monitoringTimer_Tick);

            // btnApplyPerformance
            this.btnApplyPerformance.Location = new System.Drawing.Point(570, 120);
            this.btnApplyPerformance.Name = "btnApplyPerformance";
            this.btnApplyPerformance.Size = new System.Drawing.Size(150, 30);
            this.btnApplyPerformance.Text = "Aplicar Intervenção";
            this.btnApplyPerformance.Click += new System.EventHandler(this.btnApplyPerformance_Click);

            // numPerformance
            this.numPerformance.Location = new System.Drawing.Point(570, 90);
            this.numPerformance.Name = "numPerformance";
            this.numPerformance.Size = new System.Drawing.Size(150, 20);
            this.numPerformance.Value = new decimal(new int[] { 100, 0, 0, 0 });

            // label1 (Título Performance)
            this.label1.Location = new System.Drawing.Point(570, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.Text = "Nova Performance (%):";

            // lblSelectedMachine
            this.lblSelectedMachine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelectedMachine.Location = new System.Drawing.Point(570, 40);
            this.lblSelectedMachine.Name = "lblSelectedMachine";
            this.lblSelectedMachine.Size = new System.Drawing.Size(200, 23);
            this.lblSelectedMachine.Text = "Máquina: Nenhuma";

            // txtConsole
            this.txtConsole.Location = new System.Drawing.Point(12, 310);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.Size = new System.Drawing.Size(708, 100);

            // lblLastUpdate
            this.lblLastUpdate.Location = new System.Drawing.Point(12, 15);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(300, 23);
            this.lblLastUpdate.Text = "A aguardar atualização...";

            // Form1
            this.ClientSize = new System.Drawing.Size(734, 431);
            this.Controls.Add(this.lblLastUpdate);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.lblSelectedMachine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numPerformance);
            this.Controls.Add(this.btnApplyPerformance);
            this.Controls.Add(this.dgvSensors);
            this.Name = "Form1";
            this.Text = "SmartFactory - Gestão Industrial Híbrida (REST/SOAP)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSensors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPerformance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvSensors;
        private System.Windows.Forms.Timer monitoringTimer;
        private System.Windows.Forms.Button btnApplyPerformance;
        private System.Windows.Forms.NumericUpDown numPerformance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSelectedMachine;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Label lblLastUpdate;
    }
}