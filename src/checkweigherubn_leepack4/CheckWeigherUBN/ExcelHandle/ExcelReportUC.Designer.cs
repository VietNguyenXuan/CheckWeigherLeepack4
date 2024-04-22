namespace CheckWeigherUBN.ExcelHandle
{
  partial class ExcelReportUC
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.components = new System.ComponentModel.Container();
			GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn10 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn11 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn12 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn13 = new GlacialComponents.Controls.GLColumn();
			this.label3 = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.timer_delay = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.btExit = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btReportByDateShiftSKU = new System.Windows.Forms.Button();
			this.btReportByDateTime = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btExit1 = new System.Windows.Forms.Button();
			this.excelReportByDateAndTime1 = new CheckWeigherUBN.ExcelHandle.ExcelReportByDateAndTime();
			this.excelReportByDateShiftSKU1 = new CheckWeigherUBN.ExcelHandle.ExcelReportByDateShiftSKU();
			this.glacialList1 = new GlacialComponents.Controls.GlacialList();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.pic_animation = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic_animation)).BeginInit();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label3.Location = new System.Drawing.Point(3, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(231, 29);
			this.label3.TabIndex = 23;
			this.label3.Text = "Export to Excel file";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.Transparent;
			this.lblStatus.Location = new System.Drawing.Point(164, 4);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(16, 15);
			this.lblStatus.TabIndex = 20;
			this.lblStatus.Text = "...";
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// timer_delay
			// 
			this.timer_delay.Interval = 150;
			this.timer_delay.Tick += new System.EventHandler(this.timer_delay_Tick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.ForestGreen;
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.btExit);
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(948, 55);
			this.panel1.TabIndex = 31;
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btExit.Dock = System.Windows.Forms.DockStyle.Right;
			this.btExit.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btExit.FlatAppearance.BorderSize = 5;
			this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btExit.ForeColor = System.Drawing.Color.Red;
			this.btExit.Image = global::CheckWeigherUBN.Properties.Resources.delete_24x24;
			this.btExit.Location = new System.Drawing.Point(819, 0);
			this.btExit.Name = "btExit";
			this.btExit.Size = new System.Drawing.Size(129, 55);
			this.btExit.TabIndex = 17;
			this.btExit.UseVisualStyleBackColor = false;
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btReportByDateShiftSKU);
			this.panel2.Controls.Add(this.btReportByDateTime);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 55);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(948, 48);
			this.panel2.TabIndex = 32;
			// 
			// btReportByDateShiftSKU
			// 
			this.btReportByDateShiftSKU.Dock = System.Windows.Forms.DockStyle.Left;
			this.btReportByDateShiftSKU.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
			this.btReportByDateShiftSKU.FlatAppearance.BorderSize = 4;
			this.btReportByDateShiftSKU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btReportByDateShiftSKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btReportByDateShiftSKU.Image = global::CheckWeigherUBN.Properties.Resources.excel_24x24;
			this.btReportByDateShiftSKU.Location = new System.Drawing.Point(477, 0);
			this.btReportByDateShiftSKU.Name = "btReportByDateShiftSKU";
			this.btReportByDateShiftSKU.Size = new System.Drawing.Size(465, 48);
			this.btReportByDateShiftSKU.TabIndex = 3;
			this.btReportByDateShiftSKU.Text = "  Report theo Ngày-Ca-SKU";
			this.btReportByDateShiftSKU.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btReportByDateShiftSKU.UseVisualStyleBackColor = true;
			this.btReportByDateShiftSKU.Click += new System.EventHandler(this.btReportByDateShiftSKU_Click);
			// 
			// btReportByDateTime
			// 
			this.btReportByDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btReportByDateTime.Dock = System.Windows.Forms.DockStyle.Left;
			this.btReportByDateTime.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btReportByDateTime.FlatAppearance.BorderSize = 4;
			this.btReportByDateTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btReportByDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btReportByDateTime.ForeColor = System.Drawing.Color.White;
			this.btReportByDateTime.Image = global::CheckWeigherUBN.Properties.Resources.excel_24x24;
			this.btReportByDateTime.Location = new System.Drawing.Point(0, 0);
			this.btReportByDateTime.Name = "btReportByDateTime";
			this.btReportByDateTime.Size = new System.Drawing.Size(477, 48);
			this.btReportByDateTime.TabIndex = 2;
			this.btReportByDateTime.Text = "     Report theo Ngày-Giờ";
			this.btReportByDateTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btReportByDateTime.UseVisualStyleBackColor = false;
			this.btReportByDateTime.Click += new System.EventHandler(this.btReportByDateTime_Click);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.btExit1);
			this.panel3.Controls.Add(this.excelReportByDateAndTime1);
			this.panel3.Controls.Add(this.excelReportByDateShiftSKU1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 103);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(948, 139);
			this.panel3.TabIndex = 33;
			// 
			// btExit1
			// 
			this.btExit1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btExit1.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btExit1.FlatAppearance.BorderSize = 4;
			this.btExit1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btExit1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btExit1.ForeColor = System.Drawing.Color.White;
			this.btExit1.Image = global::CheckWeigherUBN.Properties.Resources.Shutdown_32px;
			this.btExit1.Location = new System.Drawing.Point(788, 45);
			this.btExit1.Name = "btExit1";
			this.btExit1.Size = new System.Drawing.Size(144, 88);
			this.btExit1.TabIndex = 5;
			this.btExit1.Text = "Thoát";
			this.btExit1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btExit1.UseVisualStyleBackColor = false;
			this.btExit1.Click += new System.EventHandler(this.btExit1_Click);
			// 
			// excelReportByDateAndTime1
			// 
			this.excelReportByDateAndTime1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.excelReportByDateAndTime1.Location = new System.Drawing.Point(0, 0);
			this.excelReportByDateAndTime1.Name = "excelReportByDateAndTime1";
			this.excelReportByDateAndTime1.Size = new System.Drawing.Size(948, 139);
			this.excelReportByDateAndTime1.TabIndex = 0;
			// 
			// excelReportByDateShiftSKU1
			// 
			this.excelReportByDateShiftSKU1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.excelReportByDateShiftSKU1.Location = new System.Drawing.Point(0, 0);
			this.excelReportByDateShiftSKU1.Name = "excelReportByDateShiftSKU1";
			this.excelReportByDateShiftSKU1.Size = new System.Drawing.Size(948, 139);
			this.excelReportByDateShiftSKU1.TabIndex = 1;
			// 
			// glacialList1
			// 
			this.glacialList1.ActivatedEmbeddedData = null;
			this.glacialList1.AllowColumnResize = true;
			this.glacialList1.AllowMultiselect = false;
			this.glacialList1.AlternateBackground = System.Drawing.Color.DarkGreen;
			this.glacialList1.AlternatingColors = false;
			this.glacialList1.AutoHeight = false;
			this.glacialList1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.glacialList1.BackgroundStretchToFit = true;
			glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn1.CheckBoxes = false;
			glColumn1.CheckBoxesReadOnly = false;
			glColumn1.DisplayProgressBar = false;
			glColumn1.EnableContextMenu = true;
			glColumn1.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn1.ImageIndex = -1;
			glColumn1.Name = "ID";
			glColumn1.NumericSort = false;
			glColumn1.Text = "ID";
			glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn1.Width = 50;
			glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn2.CheckBoxes = false;
			glColumn2.CheckBoxesReadOnly = false;
			glColumn2.DisplayProgressBar = false;
			glColumn2.EnableContextMenu = true;
			glColumn2.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn2.ImageIndex = -1;
			glColumn2.Name = "DateTime";
			glColumn2.NumericSort = false;
			glColumn2.Text = "Date Time";
			glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn2.Width = 140;
			glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn3.CheckBoxes = false;
			glColumn3.CheckBoxesReadOnly = false;
			glColumn3.DisplayProgressBar = false;
			glColumn3.EnableContextMenu = true;
			glColumn3.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn3.ImageIndex = -1;
			glColumn3.Name = "Nozzle";
			glColumn3.NumericSort = false;
			glColumn3.Text = "Nozzle";
			glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn3.Width = 50;
			glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn4.CheckBoxes = false;
			glColumn4.CheckBoxesReadOnly = false;
			glColumn4.DisplayProgressBar = false;
			glColumn4.EnableContextMenu = true;
			glColumn4.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn4.ImageIndex = -1;
			glColumn4.Name = "Target";
			glColumn4.NumericSort = false;
			glColumn4.Text = "Target(g)";
			glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn4.Width = 70;
			glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn5.CheckBoxes = false;
			glColumn5.CheckBoxesReadOnly = false;
			glColumn5.DisplayProgressBar = false;
			glColumn5.EnableContextMenu = true;
			glColumn5.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn5.ImageIndex = -1;
			glColumn5.Name = "Net";
			glColumn5.NumericSort = false;
			glColumn5.Text = "Net(g)";
			glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn5.Width = 70;
			glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn6.CheckBoxes = false;
			glColumn6.CheckBoxesReadOnly = false;
			glColumn6.DisplayProgressBar = false;
			glColumn6.EnableContextMenu = true;
			glColumn6.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn6.ImageIndex = -1;
			glColumn6.Name = "Gross";
			glColumn6.NumericSort = false;
			glColumn6.Text = "Gross (g)";
			glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn6.Width = 70;
			glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn7.CheckBoxes = false;
			glColumn7.CheckBoxesReadOnly = false;
			glColumn7.DisplayProgressBar = false;
			glColumn7.EnableContextMenu = true;
			glColumn7.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn7.ImageIndex = -1;
			glColumn7.Name = "Diff";
			glColumn7.NumericSort = false;
			glColumn7.Text = "Diff(g)";
			glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn7.Width = 70;
			glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn8.CheckBoxes = false;
			glColumn8.CheckBoxesReadOnly = false;
			glColumn8.DisplayProgressBar = false;
			glColumn8.EnableContextMenu = true;
			glColumn8.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn8.ImageIndex = -1;
			glColumn8.Name = "Min1T";
			glColumn8.NumericSort = false;
			glColumn8.Text = "Min 1T(g)";
			glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn8.Width = 70;
			glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn9.CheckBoxes = false;
			glColumn9.CheckBoxesReadOnly = false;
			glColumn9.DisplayProgressBar = false;
			glColumn9.EnableContextMenu = true;
			glColumn9.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn9.ImageIndex = -1;
			glColumn9.Name = "Max1T";
			glColumn9.NumericSort = false;
			glColumn9.Text = "Max 1T(g)";
			glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn9.Width = 70;
			glColumn10.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn10.CheckBoxes = false;
			glColumn10.CheckBoxesReadOnly = false;
			glColumn10.DisplayProgressBar = false;
			glColumn10.EnableContextMenu = true;
			glColumn10.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn10.ImageIndex = -1;
			glColumn10.Name = "Min2T";
			glColumn10.NumericSort = false;
			glColumn10.Text = "Min2T(g)";
			glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn10.Width = 70;
			glColumn11.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn11.CheckBoxes = false;
			glColumn11.CheckBoxesReadOnly = false;
			glColumn11.DisplayProgressBar = false;
			glColumn11.EnableContextMenu = true;
			glColumn11.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn11.ImageIndex = -1;
			glColumn11.Name = "Max2T";
			glColumn11.NumericSort = false;
			glColumn11.Text = "Max2T(g)";
			glColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn11.Width = 70;
			glColumn12.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn12.CheckBoxes = false;
			glColumn12.CheckBoxesReadOnly = false;
			glColumn12.DisplayProgressBar = false;
			glColumn12.EnableContextMenu = true;
			glColumn12.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn12.ImageIndex = -1;
			glColumn12.Name = "Status";
			glColumn12.NumericSort = false;
			glColumn12.Text = "Status";
			glColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn12.Width = 70;
			glColumn13.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn13.CheckBoxes = false;
			glColumn13.CheckBoxesReadOnly = false;
			glColumn13.DisplayProgressBar = false;
			glColumn13.EnableContextMenu = true;
			glColumn13.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn13.ImageIndex = -1;
			glColumn13.Name = "RejectSW";
			glColumn13.NumericSort = false;
			glColumn13.Text = "Reject SW";
			glColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn13.Width = 80;
			this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7,
            glColumn8,
            glColumn9,
            glColumn10,
            glColumn11,
            glColumn12,
            glColumn13});
			this.glacialList1.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
			this.glacialList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glacialList1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.glacialList1.FullRowSelect = true;
			this.glacialList1.GridColor = System.Drawing.Color.LightGray;
			this.glacialList1.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
			this.glacialList1.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
			this.glacialList1.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
			this.glacialList1.HeaderHeight = 22;
			this.glacialList1.HeaderVisible = true;
			this.glacialList1.HeaderWordWrap = false;
			this.glacialList1.HighLight_SelectedSubItem = false;
			this.glacialList1.HighLightSelectedSubItemColor = System.Drawing.Color.Red;
			this.glacialList1.HotColumnTracking = false;
			this.glacialList1.HotItemTracking = false;
			this.glacialList1.HotTrackingColor = System.Drawing.Color.LightGray;
			this.glacialList1.HoverEvents = false;
			this.glacialList1.HoverTime = 1;
			this.glacialList1.ImageList = null;
			this.glacialList1.ItemHeight = 25;
			this.glacialList1.ItemWordWrap = false;
			this.glacialList1.Location = new System.Drawing.Point(0, 242);
			this.glacialList1.Name = "glacialList1";
			this.glacialList1.Selectable = true;
			this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
			this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
			this.glacialList1.ShowBorder = true;
			this.glacialList1.ShowFocusRect = false;
			this.glacialList1.Size = new System.Drawing.Size(948, 353);
			this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
			this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
			this.glacialList1.TabIndex = 34;
			this.glacialList1.Text = "glacialList1";
			// 
			// timer1
			// 
			this.timer1.Interval = 150;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// pic_animation
			// 
			this.pic_animation.BackColor = System.Drawing.Color.White;
			this.pic_animation.ErrorImage = null;
			this.pic_animation.Image = global::CheckWeigherUBN.Properties.Resources.Animation;
			this.pic_animation.Location = new System.Drawing.Point(398, 294);
			this.pic_animation.Name = "pic_animation";
			this.pic_animation.Size = new System.Drawing.Size(123, 114);
			this.pic_animation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pic_animation.TabIndex = 39;
			this.pic_animation.TabStop = false;
			// 
			// ExcelReportUC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pic_animation);
			this.Controls.Add(this.glacialList1);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "ExcelReportUC";
			this.Size = new System.Drawing.Size(948, 595);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pic_animation)).EndInit();
			this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btExit;
    private System.Windows.Forms.Label lblStatus;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.Windows.Forms.Timer timer_delay;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private ExcelReportByDateAndTime excelReportByDateAndTime1;
    private ExcelReportByDateShiftSKU excelReportByDateShiftSKU1;
    private GlacialComponents.Controls.GlacialList glacialList1;
    private System.Windows.Forms.Button btReportByDateShiftSKU;
    private System.Windows.Forms.Button btReportByDateTime;
    private System.Windows.Forms.PictureBox pic_animation;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button btExit1;
  }
}
