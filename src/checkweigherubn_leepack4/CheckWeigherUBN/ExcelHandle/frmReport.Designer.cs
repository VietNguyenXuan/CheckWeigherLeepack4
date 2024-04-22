namespace CheckWeigherUBN
{
  partial class frmReport
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
      GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
      GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lblStatus = new System.Windows.Forms.Label();
      this.lblPercent = new System.Windows.Forms.Label();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.btExit = new System.Windows.Forms.Button();
      this.btExport = new System.Windows.Forms.Button();
      this.btPreview = new System.Windows.Forms.Button();
      this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.glacialList1 = new GlacialComponents.Controls.GlacialList();
      this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.glacialList1);
      this.splitContainer1.Size = new System.Drawing.Size(691, 397);
      this.splitContainer1.SplitterDistance = 131;
      this.splitContainer1.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lblStatus);
      this.groupBox1.Controls.Add(this.lblPercent);
      this.groupBox1.Controls.Add(this.progressBar1);
      this.groupBox1.Controls.Add(this.btExit);
      this.groupBox1.Controls.Add(this.btExport);
      this.groupBox1.Controls.Add(this.btPreview);
      this.groupBox1.Controls.Add(this.dateTimePicker2);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.dateTimePicker1);
      this.groupBox1.Controls.Add(this.pictureBox1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(691, 131);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      // 
      // lblStatus
      // 
      this.lblStatus.AutoSize = true;
      this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStatus.ForeColor = System.Drawing.Color.Black;
      this.lblStatus.Location = new System.Drawing.Point(123, 110);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(0, 16);
      this.lblStatus.TabIndex = 10;
      // 
      // lblPercent
      // 
      this.lblPercent.AutoSize = true;
      this.lblPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPercent.Location = new System.Drawing.Point(485, 90);
      this.lblPercent.Name = "lblPercent";
      this.lblPercent.Size = new System.Drawing.Size(27, 16);
      this.lblPercent.TabIndex = 9;
      this.lblPercent.Text = "0%";
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(125, 84);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(354, 22);
      this.progressBar1.TabIndex = 8;
      // 
      // btExit
      // 
      this.btExit.Location = new System.Drawing.Point(535, 21);
      this.btExit.Name = "btExit";
      this.btExit.Size = new System.Drawing.Size(65, 53);
      this.btExit.TabIndex = 7;
      this.btExit.Text = "Exit";
      this.btExit.UseVisualStyleBackColor = true;
      this.btExit.Click += new System.EventHandler(this.btExit_Click);
      // 
      // btExport
      // 
      this.btExport.Location = new System.Drawing.Point(464, 21);
      this.btExport.Name = "btExport";
      this.btExport.Size = new System.Drawing.Size(65, 53);
      this.btExport.TabIndex = 6;
      this.btExport.Text = "Export";
      this.btExport.UseVisualStyleBackColor = true;
      this.btExport.Click += new System.EventHandler(this.btExport_Click);
      // 
      // btPreview
      // 
      this.btPreview.Location = new System.Drawing.Point(393, 21);
      this.btPreview.Name = "btPreview";
      this.btPreview.Size = new System.Drawing.Size(65, 53);
      this.btPreview.TabIndex = 5;
      this.btPreview.Text = "Preview";
      this.btPreview.UseVisualStyleBackColor = true;
      this.btPreview.Click += new System.EventHandler(this.btPreview_Click);
      // 
      // dateTimePicker2
      // 
      this.dateTimePicker2.Location = new System.Drawing.Point(182, 54);
      this.dateTimePicker2.Name = "dateTimePicker2";
      this.dateTimePicker2.Size = new System.Drawing.Size(180, 20);
      this.dateTimePicker2.TabIndex = 4;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(123, 60);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(20, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "To";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(123, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(30, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "From";
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.Location = new System.Drawing.Point(182, 23);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(180, 20);
      this.dateTimePicker1.TabIndex = 1;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::CheckWeigherUBN.Properties.Resources.excel_64x64;
      this.pictureBox1.Location = new System.Drawing.Point(25, 39);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(66, 67);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // glacialList1
      // 
      this.glacialList1.ActivatedEmbeddedData = null;
      this.glacialList1.AllowColumnResize = true;
      this.glacialList1.AllowMultiselect = false;
      this.glacialList1.AlternateBackground = System.Drawing.Color.DarkGreen;
      this.glacialList1.AlternatingColors = false;
      this.glacialList1.AutoHeight = true;
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
      glColumn2.Width = 100;
      glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn3.CheckBoxes = false;
      glColumn3.CheckBoxesReadOnly = false;
      glColumn3.DisplayProgressBar = false;
      glColumn3.EnableContextMenu = true;
      glColumn3.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn3.ImageIndex = -1;
      glColumn3.Name = "Barcode";
      glColumn3.NumericSort = false;
      glColumn3.Text = "Barcode";
      glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn3.Width = 100;
      glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn4.CheckBoxes = false;
      glColumn4.CheckBoxesReadOnly = false;
      glColumn4.DisplayProgressBar = false;
      glColumn4.EnableContextMenu = true;
      glColumn4.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn4.ImageIndex = -1;
      glColumn4.Name = "Description";
      glColumn4.NumericSort = false;
      glColumn4.Text = "Description";
      glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn4.Width = 120;
      glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn5.CheckBoxes = false;
      glColumn5.CheckBoxesReadOnly = false;
      glColumn5.DisplayProgressBar = false;
      glColumn5.EnableContextMenu = true;
      glColumn5.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn5.ImageIndex = -1;
      glColumn5.Name = "Target";
      glColumn5.NumericSort = false;
      glColumn5.Text = "Target(g)";
      glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn5.Width = 60;
      glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn6.CheckBoxes = false;
      glColumn6.CheckBoxesReadOnly = false;
      glColumn6.DisplayProgressBar = false;
      glColumn6.EnableContextMenu = true;
      glColumn6.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn6.ImageIndex = -1;
      glColumn6.Name = "Actual";
      glColumn6.NumericSort = false;
      glColumn6.Text = "Actual(g)";
      glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn6.Width = 60;
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
      glColumn7.Width = 60;
      glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn8.CheckBoxes = false;
      glColumn8.CheckBoxesReadOnly = false;
      glColumn8.DisplayProgressBar = false;
      glColumn8.EnableContextMenu = true;
      glColumn8.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn8.ImageIndex = -1;
      glColumn8.Name = "Status";
      glColumn8.NumericSort = false;
      glColumn8.Text = "Status";
      glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn8.Width = 60;
      glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn9.CheckBoxes = false;
      glColumn9.CheckBoxesReadOnly = false;
      glColumn9.DisplayProgressBar = false;
      glColumn9.EnableContextMenu = true;
      glColumn9.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
      glColumn9.ImageIndex = -1;
      glColumn9.Name = "RejectSW";
      glColumn9.NumericSort = false;
      glColumn9.Text = "RejectSW";
      glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      glColumn9.Width = 70;
      this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7,
            glColumn8,
            glColumn9});
      this.glacialList1.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
      this.glacialList1.Dock = System.Windows.Forms.DockStyle.Fill;
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
      this.glacialList1.ItemHeight = 17;
      this.glacialList1.ItemWordWrap = false;
      this.glacialList1.Location = new System.Drawing.Point(0, 0);
      this.glacialList1.Name = "glacialList1";
      this.glacialList1.Selectable = true;
      this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
      this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
      this.glacialList1.ShowBorder = true;
      this.glacialList1.ShowFocusRect = false;
      this.glacialList1.Size = new System.Drawing.Size(691, 262);
      this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
      this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
      this.glacialList1.TabIndex = 3;
      this.glacialList1.Text = "glacialList1";
      // 
      // backgroundWorker1
      // 
      this.backgroundWorker1.WorkerReportsProgress = true;
      this.backgroundWorker1.WorkerSupportsCancellation = true;
      this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
      this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
      this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.Filter = "Excel files|*.xlsx";
      this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
      // 
      // timer1
      // 
      this.timer1.Interval = 500;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // frmReport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(691, 397);
      this.ControlBox = false;
      this.Controls.Add(this.splitContainer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "frmReport";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Report excel";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button btExit;
    private System.Windows.Forms.Button btExport;
    private System.Windows.Forms.Button btPreview;
    private System.Windows.Forms.DateTimePicker dateTimePicker2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private System.Windows.Forms.Label lblPercent;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Label lblStatus;
    private GlacialComponents.Controls.GlacialList glacialList1;
  }
}