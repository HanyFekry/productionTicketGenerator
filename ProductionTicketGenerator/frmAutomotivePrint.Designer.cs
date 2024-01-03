
namespace ProductionTicketGenerator
{
    partial class frmAutomotivePrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSalesOrder = new System.Windows.Forms.TextBox();
            this.gvWos = new System.Windows.Forms.DataGridView();
            this.btnView = new System.Windows.Forms.Button();
            this.gvTickets = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbOPF = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpProduction = new System.Windows.Forms.DateTimePicker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gvPalletDetailsAuto = new System.Windows.Forms.DataGridView();
            this.btnViewAuto = new System.Windows.Forms.Button();
            this.gvTicketsAuto = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPrinterAuto = new System.Windows.Forms.ComboBox();
            this.btnPrintAuto = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbOPFAuto = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpProductionAuto = new System.Windows.Forms.DateTimePicker();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTickets)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPalletDetailsAuto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTicketsAuto)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(28, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(859, 470);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.body_bg;
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtSalesOrder);
            this.tabPage1.Controls.Add(this.gvWos);
            this.tabPage1.Controls.Add(this.btnView);
            this.tabPage1.Controls.Add(this.gvTickets);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.cmbPrinter);
            this.tabPage1.Controls.Add(this.btnPrint);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.cmbOPF);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.dtpProduction);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(851, 444);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Leoni";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(18, 46);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 25);
            this.btnSave.TabIndex = 94;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(235, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 93;
            this.label4.Text = "أمر البيع";
            // 
            // txtSalesOrder
            // 
            this.txtSalesOrder.Location = new System.Drawing.Point(92, 47);
            this.txtSalesOrder.Name = "txtSalesOrder";
            this.txtSalesOrder.Size = new System.Drawing.Size(127, 20);
            this.txtSalesOrder.TabIndex = 87;
            // 
            // gvWos
            // 
            this.gvWos.AllowUserToAddRows = false;
            this.gvWos.AllowUserToDeleteRows = false;
            this.gvWos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvWos.Location = new System.Drawing.Point(45, 333);
            this.gvWos.Name = "gvWos";
            this.gvWos.Size = new System.Drawing.Size(452, 101);
            this.gvWos.TabIndex = 92;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.Green;
            this.btnView.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location = new System.Drawing.Point(356, 47);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(90, 32);
            this.btnView.TabIndex = 91;
            this.btnView.Text = "عرض";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // gvTickets
            // 
            this.gvTickets.AllowUserToAddRows = false;
            this.gvTickets.AllowUserToDeleteRows = false;
            this.gvTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTickets.Location = new System.Drawing.Point(45, 85);
            this.gvTickets.Name = "gvTickets";
            this.gvTickets.Size = new System.Drawing.Size(771, 248);
            this.gvTickets.TabIndex = 90;
            this.gvTickets.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvTickets_RowHeaderMouseClick);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Maroon;
            this.label16.Location = new System.Drawing.Point(764, 353);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 17);
            this.label16.TabIndex = 89;
            this.label16.Text = "الطابعة";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbPrinter.Location = new System.Drawing.Point(565, 351);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(194, 21);
            this.cmbPrinter.TabIndex = 88;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.icons8_print_40;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrint.Location = new System.Drawing.Point(611, 386);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(115, 48);
            this.btnPrint.TabIndex = 87;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(235, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 84;
            this.label7.Text = "أمر التصنيع";
            // 
            // cmbOPF
            // 
            this.cmbOPF.FormattingEnabled = true;
            this.cmbOPF.Location = new System.Drawing.Point(92, 17);
            this.cmbOPF.Name = "cmbOPF";
            this.cmbOPF.Size = new System.Drawing.Size(127, 21);
            this.cmbOPF.TabIndex = 83;
            this.cmbOPF.SelectedIndexChanged += new System.EventHandler(this.cmbOPF_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Maroon;
            this.label14.Location = new System.Drawing.Point(628, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 18);
            this.label14.TabIndex = 82;
            this.label14.Text = "تاريخ الإنتاج";
            // 
            // dtpProduction
            // 
            this.dtpProduction.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProduction.Location = new System.Drawing.Point(452, 19);
            this.dtpProduction.Name = "dtpProduction";
            this.dtpProduction.Size = new System.Drawing.Size(165, 20);
            this.dtpProduction.TabIndex = 81;
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.body_bg;
            this.tabPage2.Controls.Add(this.gvPalletDetailsAuto);
            this.tabPage2.Controls.Add(this.btnViewAuto);
            this.tabPage2.Controls.Add(this.gvTicketsAuto);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cmbPrinterAuto);
            this.tabPage2.Controls.Add(this.btnPrintAuto);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cmbOPFAuto);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.dtpProductionAuto);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(851, 444);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "General Automotive";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gvPalletDetailsAuto
            // 
            this.gvPalletDetailsAuto.AllowUserToAddRows = false;
            this.gvPalletDetailsAuto.AllowUserToDeleteRows = false;
            this.gvPalletDetailsAuto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPalletDetailsAuto.Location = new System.Drawing.Point(43, 330);
            this.gvPalletDetailsAuto.Name = "gvPalletDetailsAuto";
            this.gvPalletDetailsAuto.Size = new System.Drawing.Size(452, 101);
            this.gvPalletDetailsAuto.TabIndex = 102;
            // 
            // btnViewAuto
            // 
            this.btnViewAuto.BackColor = System.Drawing.Color.Green;
            this.btnViewAuto.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnViewAuto.ForeColor = System.Drawing.Color.White;
            this.btnViewAuto.Location = new System.Drawing.Point(354, 44);
            this.btnViewAuto.Name = "btnViewAuto";
            this.btnViewAuto.Size = new System.Drawing.Size(90, 32);
            this.btnViewAuto.TabIndex = 101;
            this.btnViewAuto.Text = "عرض";
            this.btnViewAuto.UseVisualStyleBackColor = false;
            this.btnViewAuto.Click += new System.EventHandler(this.btnViewAuto_Click);
            // 
            // gvTicketsAuto
            // 
            this.gvTicketsAuto.AllowUserToAddRows = false;
            this.gvTicketsAuto.AllowUserToDeleteRows = false;
            this.gvTicketsAuto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTicketsAuto.Location = new System.Drawing.Point(43, 82);
            this.gvTicketsAuto.Name = "gvTicketsAuto";
            this.gvTicketsAuto.Size = new System.Drawing.Size(759, 248);
            this.gvTicketsAuto.TabIndex = 100;
            this.gvTicketsAuto.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvTicketsAuto_RowHeaderMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SlateBlue;
            this.label1.Location = new System.Drawing.Point(762, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 99;
            this.label1.Text = "الطابعة";
            // 
            // cmbPrinterAuto
            // 
            this.cmbPrinterAuto.FormattingEnabled = true;
            this.cmbPrinterAuto.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbPrinterAuto.Location = new System.Drawing.Point(563, 348);
            this.cmbPrinterAuto.Name = "cmbPrinterAuto";
            this.cmbPrinterAuto.Size = new System.Drawing.Size(194, 21);
            this.cmbPrinterAuto.TabIndex = 98;
            // 
            // btnPrintAuto
            // 
            this.btnPrintAuto.BackColor = System.Drawing.Color.White;
            this.btnPrintAuto.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.icons8_print_40;
            this.btnPrintAuto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrintAuto.Location = new System.Drawing.Point(609, 383);
            this.btnPrintAuto.Name = "btnPrintAuto";
            this.btnPrintAuto.Size = new System.Drawing.Size(115, 48);
            this.btnPrintAuto.TabIndex = 97;
            this.btnPrintAuto.UseVisualStyleBackColor = false;
            this.btnPrintAuto.Click += new System.EventHandler(this.btnPrintAuto_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.SlateBlue;
            this.label2.Location = new System.Drawing.Point(256, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 96;
            this.label2.Text = "أمر التصنيع";
            // 
            // cmbOPFAuto
            // 
            this.cmbOPFAuto.FormattingEnabled = true;
            this.cmbOPFAuto.Location = new System.Drawing.Point(90, 14);
            this.cmbOPFAuto.Name = "cmbOPFAuto";
            this.cmbOPFAuto.Size = new System.Drawing.Size(165, 21);
            this.cmbOPFAuto.TabIndex = 95;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.SlateBlue;
            this.label3.Location = new System.Drawing.Point(626, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 18);
            this.label3.TabIndex = 94;
            this.label3.Text = "تاريخ الإنتاج";
            // 
            // dtpProductionAuto
            // 
            this.dtpProductionAuto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProductionAuto.Location = new System.Drawing.Point(450, 16);
            this.dtpProductionAuto.Name = "dtpProductionAuto";
            this.dtpProductionAuto.Size = new System.Drawing.Size(165, 20);
            this.dtpProductionAuto.TabIndex = 93;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.exit_icon2;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Location = new System.Drawing.Point(415, 499);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(115, 50);
            this.btnDelete.TabIndex = 86;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmAutomotivePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.body_bg;
            this.ClientSize = new System.Drawing.Size(909, 561);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmAutomotivePrint";
            this.Text = "طباعة";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAutomotivePrint_FormClosed);
            this.Load += new System.EventHandler(this.frmAutomotivePrint_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTickets)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPalletDetailsAuto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTicketsAuto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gvTickets;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbOPF;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpProduction;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.DataGridView gvWos;
        private System.Windows.Forms.DataGridView gvPalletDetailsAuto;
        private System.Windows.Forms.Button btnViewAuto;
        private System.Windows.Forms.DataGridView gvTicketsAuto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPrinterAuto;
        private System.Windows.Forms.Button btnPrintAuto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbOPFAuto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpProductionAuto;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSalesOrder;
    }
}

