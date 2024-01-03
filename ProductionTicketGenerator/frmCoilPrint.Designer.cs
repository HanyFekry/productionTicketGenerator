
namespace ProductionTicketGenerator
{
    partial class frmCoilPrint
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.gvWos);
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.gvTickets);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cmbPrinter);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbOPF);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.dtpProduction);
            this.groupBox1.Location = new System.Drawing.Point(27, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(860, 471);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coils";
            // 
            // gvWos
            // 
            this.gvWos.AllowUserToAddRows = false;
            this.gvWos.AllowUserToDeleteRows = false;
            this.gvWos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvWos.Location = new System.Drawing.Point(45, 347);
            this.gvWos.Name = "gvWos";
            this.gvWos.Size = new System.Drawing.Size(489, 101);
            this.gvWos.TabIndex = 102;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.Green;
            this.btnView.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location = new System.Drawing.Point(356, 61);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(90, 32);
            this.btnView.TabIndex = 101;
            this.btnView.Text = "عرض";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // gvTickets
            // 
            this.gvTickets.AllowUserToAddRows = false;
            this.gvTickets.AllowUserToDeleteRows = false;
            this.gvTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTickets.Location = new System.Drawing.Point(45, 99);
            this.gvTickets.Name = "gvTickets";
            this.gvTickets.Size = new System.Drawing.Size(771, 248);
            this.gvTickets.TabIndex = 100;
            this.gvTickets.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvTickets_RowHeaderMouseClick);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Maroon;
            this.label16.Location = new System.Drawing.Point(764, 367);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 17);
            this.label16.TabIndex = 99;
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
            this.cmbPrinter.Location = new System.Drawing.Point(565, 365);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(194, 21);
            this.cmbPrinter.TabIndex = 98;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.icons8_print_40;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrint.Location = new System.Drawing.Point(611, 400);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(115, 48);
            this.btnPrint.TabIndex = 97;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(258, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 96;
            this.label7.Text = "أمر التصنيع";
            // 
            // cmbOPF
            // 
            this.cmbOPF.FormattingEnabled = true;
            this.cmbOPF.Location = new System.Drawing.Point(92, 31);
            this.cmbOPF.Name = "cmbOPF";
            this.cmbOPF.Size = new System.Drawing.Size(165, 21);
            this.cmbOPF.TabIndex = 95;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Maroon;
            this.label14.Location = new System.Drawing.Point(628, 34);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 18);
            this.label14.TabIndex = 94;
            this.label14.Text = "تاريخ الإنتاج";
            // 
            // dtpProduction
            // 
            this.dtpProduction.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProduction.Location = new System.Drawing.Point(452, 33);
            this.dtpProduction.Name = "dtpProduction";
            this.dtpProduction.Size = new System.Drawing.Size(165, 20);
            this.dtpProduction.TabIndex = 93;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.exit_icon2;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Location = new System.Drawing.Point(419, 499);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(115, 50);
            this.btnDelete.TabIndex = 87;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmCoilPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProductionTicketGenerator.Properties.Resources.body_bg;
            this.ClientSize = new System.Drawing.Size(909, 561);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCoilPrint";
            this.Text = "طباعة لفات";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.btnDelete_Click);
            this.Load += new System.EventHandler(this.frmCoilPrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTickets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gvWos;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.DataGridView gvTickets;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbOPF;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpProduction;
        private System.Windows.Forms.Button btnDelete;
    }
}