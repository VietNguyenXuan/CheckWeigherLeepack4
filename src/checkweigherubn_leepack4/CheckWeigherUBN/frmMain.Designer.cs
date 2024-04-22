namespace CheckWeigherUBN
{
    partial class frmMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.menuSettings = new System.Windows.Forms.ToolStripButton();
			this.menuProduct = new System.Windows.Forms.ToolStripButton();
			this.menuManual = new System.Windows.Forms.ToolStripButton();
			this.menuExportExcel = new System.Windows.Forms.ToolStripButton();
			this.menuLogin = new System.Windows.Forms.ToolStripButton();
			this.menuShutdown = new System.Windows.Forms.ToolStripButton();
			this.menuClockDisplay = new System.Windows.Forms.ToolStripLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.weigherDisplay1 = new CheckWeigherUBN.WeigherDisplay();
			this.operationInformation1 = new CheckWeigherUBN.OperationInformation();
			this.ConvoyerDisplay1 = new CheckWeigherUBN.ConvoyerDisplay();
			this.errorConnection1 = new CheckWeigherUBN.ErrorConnection();
			this.weigherProcessLogging1 = new CheckWeigherUBN.WeigherProcessLogging();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.btClickToViewAlarm = new System.Windows.Forms.Button();
			this.errorLog1 = new CheckWeigherUBN.ErrorLog();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.timer_clock = new System.Windows.Forms.Timer(this.components);
			this.timer1_demo = new System.Windows.Forms.Timer(this.components);
			this.timer_delay_shutdown = new System.Windows.Forms.Timer(this.components);
			this.timer_check_export_excel = new System.Windows.Forms.Timer(this.components);
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.backgroundWorker2ReportExcel = new System.ComponentModel.BackgroundWorker();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings,
            this.menuProduct,
            this.menuManual,
            this.menuExportExcel,
            this.menuLogin,
            this.menuShutdown,
            this.menuClockDisplay});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1364, 58);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// menuSettings
			// 
			this.menuSettings.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuSettings.Image = global::CheckWeigherUBN.Properties.Resources.service_manager24x24;
			this.menuSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuSettings.Name = "menuSettings";
			this.menuSettings.Size = new System.Drawing.Size(127, 55);
			this.menuSettings.Text = "   Settings   ";
			this.menuSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
			// 
			// menuProduct
			// 
			this.menuProduct.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuProduct.Image = global::CheckWeigherUBN.Properties.Resources.product_163_24x24;
			this.menuProduct.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuProduct.Name = "menuProduct";
			this.menuProduct.Size = new System.Drawing.Size(122, 55);
			this.menuProduct.Text = "  Product   ";
			this.menuProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.menuProduct.Click += new System.EventHandler(this.menuProduct_Click);
			// 
			// menuManual
			// 
			this.menuManual.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuManual.Image = global::CheckWeigherUBN.Properties.Resources.setting24x24;
			this.menuManual.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuManual.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuManual.Name = "menuManual";
			this.menuManual.Size = new System.Drawing.Size(119, 55);
			this.menuManual.Text = "  Manual   ";
			this.menuManual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.menuManual.Click += new System.EventHandler(this.menuManual_Click);
			// 
			// menuExportExcel
			// 
			this.menuExportExcel.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuExportExcel.Image = global::CheckWeigherUBN.Properties.Resources.application_vnd_ms_excel;
			this.menuExportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuExportExcel.Name = "menuExportExcel";
			this.menuExportExcel.Size = new System.Drawing.Size(112, 55);
			this.menuExportExcel.Text = "  Report   ";
			this.menuExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.menuExportExcel.Click += new System.EventHandler(this.menuExportExcel_Click);
			// 
			// menuLogin
			// 
			this.menuLogin.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuLogin.Image = global::CheckWeigherUBN.Properties.Resources.gdm_login_photo24x24;
			this.menuLogin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuLogin.Name = "menuLogin";
			this.menuLogin.Size = new System.Drawing.Size(156, 55);
			this.menuLogin.Text = "   Đăng nhập   ";
			this.menuLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.menuLogin.Click += new System.EventHandler(this.menuLogin_Click);
			// 
			// menuShutdown
			// 
			this.menuShutdown.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuShutdown.Image = global::CheckWeigherUBN.Properties.Resources.delete_24x24;
			this.menuShutdown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuShutdown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuShutdown.Name = "menuShutdown";
			this.menuShutdown.Size = new System.Drawing.Size(72, 55);
			this.menuShutdown.Text = "Thoát";
			this.menuShutdown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.menuShutdown.Click += new System.EventHandler(this.menuShutdown_Click);
			// 
			// menuClockDisplay
			// 
			this.menuClockDisplay.AutoSize = false;
			this.menuClockDisplay.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuClockDisplay.Margin = new System.Windows.Forms.Padding(360, 1, 0, 2);
			this.menuClockDisplay.Name = "menuClockDisplay";
			this.menuClockDisplay.RightToLeftAutoMirrorImage = true;
			this.menuClockDisplay.Size = new System.Drawing.Size(273, 43);
			this.menuClockDisplay.Text = "DDMMdd HH:MM:ss";
			this.menuClockDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 58);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
			this.splitContainer1.Size = new System.Drawing.Size(1364, 708);
			this.splitContainer1.SplitterDistance = 615;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.errorConnection1);
			this.splitContainer2.Panel2.Controls.Add(this.weigherProcessLogging1);
			this.splitContainer2.Size = new System.Drawing.Size(1364, 615);
			this.splitContainer2.SplitterDistance = 397;
			this.splitContainer2.TabIndex = 0;
			// 
			// splitContainer3
			// 
			this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.weigherDisplay1);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.operationInformation1);
			this.splitContainer3.Panel2.Controls.Add(this.ConvoyerDisplay1);
			this.splitContainer3.Size = new System.Drawing.Size(1364, 397);
			this.splitContainer3.SplitterDistance = 445;
			this.splitContainer3.TabIndex = 0;
			// 
			// weigherDisplay1
			// 
			this.weigherDisplay1.BackColor = System.Drawing.Color.White;
			this.weigherDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.weigherDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.weigherDisplay1.Location = new System.Drawing.Point(0, 0);
			this.weigherDisplay1.Name = "weigherDisplay1";
			this.weigherDisplay1.Size = new System.Drawing.Size(441, 393);
			this.weigherDisplay1.TabIndex = 0;
			// 
			// operationInformation1
			// 
			this.operationInformation1.BackColor = System.Drawing.Color.White;
			this.operationInformation1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.operationInformation1.Location = new System.Drawing.Point(0, 0);
			this.operationInformation1.Name = "operationInformation1";
			this.operationInformation1.Size = new System.Drawing.Size(911, 393);
			this.operationInformation1.TabIndex = 1;
			// 
			// ConvoyerDisplay1
			// 
			this.ConvoyerDisplay1.BackColor = System.Drawing.Color.White;
			this.ConvoyerDisplay1.Location = new System.Drawing.Point(0, 161);
			this.ConvoyerDisplay1.Name = "ConvoyerDisplay1";
			this.ConvoyerDisplay1.Size = new System.Drawing.Size(605, 94);
			this.ConvoyerDisplay1.TabIndex = 0;
			// 
			// errorConnection1
			// 
			this.errorConnection1.BackColor = System.Drawing.Color.White;
			this.errorConnection1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.errorConnection1.Location = new System.Drawing.Point(413, 12);
			this.errorConnection1.Name = "errorConnection1";
			this.errorConnection1.Size = new System.Drawing.Size(527, 199);
			this.errorConnection1.TabIndex = 1;
			// 
			// weigherProcessLogging1
			// 
			this.weigherProcessLogging1.BackColor = System.Drawing.Color.White;
			this.weigherProcessLogging1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.weigherProcessLogging1.Location = new System.Drawing.Point(0, 0);
			this.weigherProcessLogging1.Name = "weigherProcessLogging1";
			this.weigherProcessLogging1.Size = new System.Drawing.Size(1360, 210);
			this.weigherProcessLogging1.TabIndex = 0;
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.btClickToViewAlarm);
			this.splitContainer4.Panel1.Controls.Add(this.errorLog1);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.pictureBox1);
			this.splitContainer4.Size = new System.Drawing.Size(1364, 89);
			this.splitContainer4.SplitterDistance = 1119;
			this.splitContainer4.TabIndex = 0;
			// 
			// btClickToViewAlarm
			// 
			this.btClickToViewAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btClickToViewAlarm.Dock = System.Windows.Forms.DockStyle.Right;
			this.btClickToViewAlarm.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btClickToViewAlarm.FlatAppearance.BorderSize = 8;
			this.btClickToViewAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btClickToViewAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btClickToViewAlarm.ForeColor = System.Drawing.Color.White;
			this.btClickToViewAlarm.Location = new System.Drawing.Point(926, 0);
			this.btClickToViewAlarm.Name = "btClickToViewAlarm";
			this.btClickToViewAlarm.Size = new System.Drawing.Size(193, 89);
			this.btClickToViewAlarm.TabIndex = 91;
			this.btClickToViewAlarm.Text = "VIEW ALARMS";
			this.btClickToViewAlarm.UseVisualStyleBackColor = false;
			this.btClickToViewAlarm.Click += new System.EventHandler(this.btClickToViewAlarm_Click);
			// 
			// errorLog1
			// 
			this.errorLog1.BackColor = System.Drawing.Color.White;
			this.errorLog1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.errorLog1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorLog1.Location = new System.Drawing.Point(0, 0);
			this.errorLog1.Name = "errorLog1";
			this.errorLog1.Size = new System.Drawing.Size(1119, 89);
			this.errorLog1.TabIndex = 90;
			this.errorLog1.Load += new System.EventHandler(this.errorLog1_Load);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Image = global::CheckWeigherUBN.Properties.Resources.Logo_15_01_2022;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(241, 89);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// timer_clock
			// 
			this.timer_clock.Enabled = true;
			this.timer_clock.Interval = 800;
			this.timer_clock.Tick += new System.EventHandler(this.timer_clock_Tick);
			// 
			// timer1_demo
			// 
			this.timer1_demo.Interval = 1000;
			this.timer1_demo.Tick += new System.EventHandler(this.timer1_demo_Tick);
			// 
			// timer_delay_shutdown
			// 
			this.timer_delay_shutdown.Interval = 3000;
			this.timer_delay_shutdown.Tick += new System.EventHandler(this.timer_delay_shutdown_Tick);
			// 
			// timer_check_export_excel
			// 
			this.timer_check_export_excel.Interval = 60000;
			this.timer_check_export_excel.Tick += new System.EventHandler(this.timer_check_export_excel_Tick);
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// backgroundWorker2ReportExcel
			// 
			this.backgroundWorker2ReportExcel.WorkerReportsProgress = true;
			this.backgroundWorker2ReportExcel.WorkerSupportsCancellation = true;
			this.backgroundWorker2ReportExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2ReportExcel_DoWork);
			this.backgroundWorker2ReportExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2ReportExcel_ProgressChanged);
			this.backgroundWorker2ReportExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2ReportExcel_RunWorkerCompleted);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1364, 766);
			this.ControlBox = false;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton menuShutdown;
        private System.Windows.Forms.ToolStripButton menuSettings;
        private System.Windows.Forms.ToolStripButton menuExportExcel;
        private System.Windows.Forms.ToolStripButton menuLogin;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private WeigherProcessLogging weigherProcessLogging1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private WeigherDisplay weigherDisplay1;
        private ConvoyerDisplay ConvoyerDisplay1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripButton menuProduct;
        private OperationInformation operationInformation1;
        private System.Windows.Forms.ToolStripButton menuManual;
    private ErrorConnection errorConnection1;
    private System.Windows.Forms.ToolStripLabel menuClockDisplay;
    private System.Windows.Forms.Timer timer_clock;
    private System.Windows.Forms.Timer timer1_demo;
    private System.Windows.Forms.Timer timer_delay_shutdown;
    private System.Windows.Forms.Timer timer_check_export_excel;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.ComponentModel.BackgroundWorker backgroundWorker2ReportExcel;
    private ErrorLog errorLog1;
		private System.Windows.Forms.Button btClickToViewAlarm;
	}
}

