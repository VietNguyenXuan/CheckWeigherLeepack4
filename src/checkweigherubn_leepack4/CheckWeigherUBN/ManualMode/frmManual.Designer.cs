namespace CheckWeigherUBN
{
  partial class frmManual
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManual));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.switchEnableDisableAutoAssignChangeover = new CheckWeigherUBN.SwitchManManual();
			this.btViewPLCMemory = new System.Windows.Forms.Button();
			this.btDemoMode = new System.Windows.Forms.Button();
			this.btExit = new System.Windows.Forms.Button();
			this.switchManManual1 = new CheckWeigherUBN.SwitchManManual();
			this.switchCyclinderOnOff = new CheckWeigherUBN.SwitchManManual();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.startStopButton_BT_TACH_CHAI = new CheckWeigherUBN.ConveyorManual();
			this.startStopButton_BT_CAN = new CheckWeigherUBN.ConveyorManual();
			this.startStopButton_BT_REJECT = new CheckWeigherUBN.ConveyorManual();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.switchEnableDisableReject = new CheckWeigherUBN.SwitchManManual();
			this.switchBuzzerTest = new CheckWeigherUBN.SwitchManManual();
			this.buttonCyclinderOnOff = new CheckWeigherUBN.ButtonOnOffInvert();
			this.switchEnableDisableWeigher = new CheckWeigherUBN.SwitchManManual();
			this.startStopConveyorALL = new CheckWeigherUBN.ConveyorManual();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bufferSettings1 = new CheckWeigherUBN.ManualMode.BufferSettings();
			this.bufferSettings2 = new CheckWeigherUBN.ManualMode.BufferSettings();
			this.bufferSettings8 = new CheckWeigherUBN.ManualMode.BufferSettings();
			this.bufferSettings5 = new CheckWeigherUBN.ManualMode.BufferSettings();
			this.bufferSettings3 = new CheckWeigherUBN.ManualMode.BufferSettings();
			this.bufferSettings4 = new CheckWeigherUBN.ManualMode.BufferSettings();
			this.bufferSettings6 = new CheckWeigherUBN.ManualMode.BufferSettings();
			this.bufferSettings7 = new CheckWeigherUBN.ManualMode.BufferSettings();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.switchEnableDisableAutoAssignChangeover);
			this.splitContainer1.Panel1.Controls.Add(this.btViewPLCMemory);
			this.splitContainer1.Panel1.Controls.Add(this.btDemoMode);
			this.splitContainer1.Panel1.Controls.Add(this.btExit);
			this.splitContainer1.Panel1.Controls.Add(this.switchManManual1);
			this.splitContainer1.Panel1.Controls.Add(this.switchCyclinderOnOff);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.splitContainer1.Size = new System.Drawing.Size(1350, 729);
			this.splitContainer1.SplitterDistance = 57;
			this.splitContainer1.TabIndex = 0;
			// 
			// switchEnableDisableAutoAssignChangeover
			// 
			this.switchEnableDisableAutoAssignChangeover.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.switchEnableDisableAutoAssignChangeover.BackColor = System.Drawing.Color.White;
			this.switchEnableDisableAutoAssignChangeover.Location = new System.Drawing.Point(413, -113);
			this.switchEnableDisableAutoAssignChangeover.Name = "switchEnableDisableAutoAssignChangeover";
			this.switchEnableDisableAutoAssignChangeover.Size = new System.Drawing.Size(78, 163);
			this.switchEnableDisableAutoAssignChangeover.TabIndex = 14;
			this.switchEnableDisableAutoAssignChangeover.Visible = false;
			// 
			// btViewPLCMemory
			// 
			this.btViewPLCMemory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btViewPLCMemory.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btViewPLCMemory.FlatAppearance.BorderSize = 5;
			this.btViewPLCMemory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btViewPLCMemory.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btViewPLCMemory.ForeColor = System.Drawing.Color.White;
			this.btViewPLCMemory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btViewPLCMemory.Location = new System.Drawing.Point(665, 1);
			this.btViewPLCMemory.Name = "btViewPLCMemory";
			this.btViewPLCMemory.Size = new System.Drawing.Size(252, 50);
			this.btViewPLCMemory.TabIndex = 13;
			this.btViewPLCMemory.Text = "Xem vùng nhớ plc";
			this.btViewPLCMemory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btViewPLCMemory.UseVisualStyleBackColor = false;
			this.btViewPLCMemory.Visible = false;
			this.btViewPLCMemory.Click += new System.EventHandler(this.btViewPLCMemory_Click);
			// 
			// btDemoMode
			// 
			this.btDemoMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btDemoMode.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btDemoMode.FlatAppearance.BorderSize = 5;
			this.btDemoMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btDemoMode.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btDemoMode.ForeColor = System.Drawing.Color.White;
			this.btDemoMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btDemoMode.Location = new System.Drawing.Point(934, 1);
			this.btDemoMode.Name = "btDemoMode";
			this.btDemoMode.Size = new System.Drawing.Size(194, 50);
			this.btDemoMode.TabIndex = 12;
			this.btDemoMode.Text = "   Demo";
			this.btDemoMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btDemoMode.UseVisualStyleBackColor = false;
			this.btDemoMode.Visible = false;
			this.btDemoMode.Click += new System.EventHandler(this.btDemoMode_Click);
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btExit.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btExit.FlatAppearance.BorderSize = 5;
			this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btExit.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btExit.ForeColor = System.Drawing.Color.White;
			this.btExit.Image = global::CheckWeigherUBN.Properties.Resources.icons8_Shutdown_30px;
			this.btExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btExit.Location = new System.Drawing.Point(1142, 1);
			this.btExit.Name = "btExit";
			this.btExit.Size = new System.Drawing.Size(194, 50);
			this.btExit.TabIndex = 11;
			this.btExit.Text = "   Thoát";
			this.btExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btExit.UseVisualStyleBackColor = false;
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// switchManManual1
			// 
			this.switchManManual1.BackColor = System.Drawing.Color.White;
			this.switchManManual1.Enabled = false;
			this.switchManManual1.Location = new System.Drawing.Point(10, -2);
			this.switchManManual1.Name = "switchManManual1";
			this.switchManManual1.Size = new System.Drawing.Size(106, 44);
			this.switchManManual1.TabIndex = 4;
			this.switchManManual1.Visible = false;
			// 
			// switchCyclinderOnOff
			// 
			this.switchCyclinderOnOff.BackColor = System.Drawing.Color.White;
			this.switchCyclinderOnOff.Location = new System.Drawing.Point(144, -2);
			this.switchCyclinderOnOff.Name = "switchCyclinderOnOff";
			this.switchCyclinderOnOff.Size = new System.Drawing.Size(115, 44);
			this.switchCyclinderOnOff.TabIndex = 13;
			this.switchCyclinderOnOff.Visible = false;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(884, 668);
			this.tableLayoutPanel2.TabIndex = 19;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel4.ColumnCount = 3;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel4.Controls.Add(this.startStopButton_BT_TACH_CHAI, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.startStopButton_BT_CAN, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.startStopButton_BT_REJECT, 2, 0);
			this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 283);
			this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(884, 283);
			this.tableLayoutPanel4.TabIndex = 1;
			// 
			// startStopButton_BT_TACH_CHAI
			// 
			this.startStopButton_BT_TACH_CHAI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.startStopButton_BT_TACH_CHAI.BackColor = System.Drawing.Color.White;
			this.startStopButton_BT_TACH_CHAI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.startStopButton_BT_TACH_CHAI.Location = new System.Drawing.Point(3, 3);
			this.startStopButton_BT_TACH_CHAI.Name = "startStopButton_BT_TACH_CHAI";
			this.startStopButton_BT_TACH_CHAI.Size = new System.Drawing.Size(288, 277);
			this.startStopButton_BT_TACH_CHAI.TabIndex = 0;
			// 
			// startStopButton_BT_CAN
			// 
			this.startStopButton_BT_CAN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.startStopButton_BT_CAN.BackColor = System.Drawing.Color.White;
			this.startStopButton_BT_CAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.startStopButton_BT_CAN.Location = new System.Drawing.Point(297, 3);
			this.startStopButton_BT_CAN.Name = "startStopButton_BT_CAN";
			this.startStopButton_BT_CAN.Size = new System.Drawing.Size(288, 277);
			this.startStopButton_BT_CAN.TabIndex = 1;
			// 
			// startStopButton_BT_REJECT
			// 
			this.startStopButton_BT_REJECT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.startStopButton_BT_REJECT.BackColor = System.Drawing.Color.White;
			this.startStopButton_BT_REJECT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.startStopButton_BT_REJECT.Location = new System.Drawing.Point(591, 3);
			this.startStopButton_BT_REJECT.Name = "startStopButton_BT_REJECT";
			this.startStopButton_BT_REJECT.Size = new System.Drawing.Size(290, 277);
			this.startStopButton_BT_REJECT.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(0, 566);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(884, 102);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Chọn kiểu reject";
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(282, 45);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(137, 37);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "2 hướng";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(26, 45);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(137, 37);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "1 hướng";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.ColumnCount = 5;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.97039F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.7426F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.40091F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.28702F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4852F));
			this.tableLayoutPanel3.Controls.Add(this.switchEnableDisableReject, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.switchBuzzerTest, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.buttonCyclinderOnOff, 3, 0);
			this.tableLayoutPanel3.Controls.Add(this.switchEnableDisableWeigher, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.startStopConveyorALL, 4, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(884, 283);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// switchEnableDisableReject
			// 
			this.switchEnableDisableReject.BackColor = System.Drawing.Color.White;
			this.switchEnableDisableReject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.switchEnableDisableReject.Location = new System.Drawing.Point(153, 3);
			this.switchEnableDisableReject.Name = "switchEnableDisableReject";
			this.switchEnableDisableReject.Size = new System.Drawing.Size(142, 277);
			this.switchEnableDisableReject.TabIndex = 18;
			// 
			// switchBuzzerTest
			// 
			this.switchBuzzerTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.switchBuzzerTest.BackColor = System.Drawing.Color.White;
			this.switchBuzzerTest.Location = new System.Drawing.Point(3, 3);
			this.switchBuzzerTest.Name = "switchBuzzerTest";
			this.switchBuzzerTest.Size = new System.Drawing.Size(144, 277);
			this.switchBuzzerTest.TabIndex = 7;
			// 
			// buttonCyclinderOnOff
			// 
			this.buttonCyclinderOnOff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCyclinderOnOff.BackColor = System.Drawing.Color.White;
			this.buttonCyclinderOnOff.Location = new System.Drawing.Point(446, 3);
			this.buttonCyclinderOnOff.Name = "buttonCyclinderOnOff";
			this.buttonCyclinderOnOff.Size = new System.Drawing.Size(138, 277);
			this.buttonCyclinderOnOff.TabIndex = 17;
			// 
			// switchEnableDisableWeigher
			// 
			this.switchEnableDisableWeigher.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.switchEnableDisableWeigher.BackColor = System.Drawing.Color.White;
			this.switchEnableDisableWeigher.Location = new System.Drawing.Point(301, 3);
			this.switchEnableDisableWeigher.Name = "switchEnableDisableWeigher";
			this.switchEnableDisableWeigher.Size = new System.Drawing.Size(139, 277);
			this.switchEnableDisableWeigher.TabIndex = 9;
			// 
			// startStopConveyorALL
			// 
			this.startStopConveyorALL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.startStopConveyorALL.BackColor = System.Drawing.Color.White;
			this.startStopConveyorALL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.startStopConveyorALL.Location = new System.Drawing.Point(590, 3);
			this.startStopConveyorALL.Name = "startStopConveyorALL";
			this.startStopConveyorALL.Size = new System.Drawing.Size(291, 277);
			this.startStopConveyorALL.TabIndex = 3;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.bufferSettings1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.bufferSettings2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.bufferSettings8, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.bufferSettings5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.bufferSettings3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.bufferSettings4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.bufferSettings6, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.bufferSettings7, 0, 6);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(900, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(436, 665);
			this.tableLayoutPanel1.TabIndex = 18;
			// 
			// bufferSettings1
			// 
			this.bufferSettings1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bufferSettings1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.bufferSettings1.Location = new System.Drawing.Point(0, 0);
			this.bufferSettings1.Margin = new System.Windows.Forms.Padding(0);
			this.bufferSettings1.Name = "bufferSettings1";
			this.bufferSettings1.Size = new System.Drawing.Size(436, 83);
			this.bufferSettings1.TabIndex = 0;
			this.bufferSettings1.Title = "Data";
			// 
			// bufferSettings2
			// 
			this.bufferSettings2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bufferSettings2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.bufferSettings2.Location = new System.Drawing.Point(0, 83);
			this.bufferSettings2.Margin = new System.Windows.Forms.Padding(0);
			this.bufferSettings2.Name = "bufferSettings2";
			this.bufferSettings2.Size = new System.Drawing.Size(436, 83);
			this.bufferSettings2.TabIndex = 1;
			this.bufferSettings2.Title = "Data";
			// 
			// bufferSettings8
			// 
			this.bufferSettings8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bufferSettings8.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.bufferSettings8.Location = new System.Drawing.Point(0, 581);
			this.bufferSettings8.Margin = new System.Windows.Forms.Padding(0);
			this.bufferSettings8.Name = "bufferSettings8";
			this.bufferSettings8.Size = new System.Drawing.Size(436, 84);
			this.bufferSettings8.TabIndex = 15;
			this.bufferSettings8.Title = "Data";
			// 
			// bufferSettings5
			// 
			this.bufferSettings5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bufferSettings5.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.bufferSettings5.Location = new System.Drawing.Point(0, 332);
			this.bufferSettings5.Margin = new System.Windows.Forms.Padding(0);
			this.bufferSettings5.Name = "bufferSettings5";
			this.bufferSettings5.Size = new System.Drawing.Size(436, 83);
			this.bufferSettings5.TabIndex = 4;
			this.bufferSettings5.Title = "Data";
			// 
			// bufferSettings3
			// 
			this.bufferSettings3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bufferSettings3.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.bufferSettings3.Location = new System.Drawing.Point(0, 166);
			this.bufferSettings3.Margin = new System.Windows.Forms.Padding(0);
			this.bufferSettings3.Name = "bufferSettings3";
			this.bufferSettings3.Size = new System.Drawing.Size(436, 83);
			this.bufferSettings3.TabIndex = 2;
			this.bufferSettings3.Title = "Data";
			// 
			// bufferSettings4
			// 
			this.bufferSettings4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bufferSettings4.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.bufferSettings4.Location = new System.Drawing.Point(0, 249);
			this.bufferSettings4.Margin = new System.Windows.Forms.Padding(0);
			this.bufferSettings4.Name = "bufferSettings4";
			this.bufferSettings4.Size = new System.Drawing.Size(436, 83);
			this.bufferSettings4.TabIndex = 3;
			this.bufferSettings4.Title = "Data";
			// 
			// bufferSettings6
			// 
			this.bufferSettings6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bufferSettings6.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.bufferSettings6.Location = new System.Drawing.Point(0, 415);
			this.bufferSettings6.Margin = new System.Windows.Forms.Padding(0);
			this.bufferSettings6.Name = "bufferSettings6";
			this.bufferSettings6.Size = new System.Drawing.Size(436, 83);
			this.bufferSettings6.TabIndex = 5;
			this.bufferSettings6.Title = "Data";
			// 
			// bufferSettings7
			// 
			this.bufferSettings7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bufferSettings7.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.bufferSettings7.Location = new System.Drawing.Point(0, 498);
			this.bufferSettings7.Margin = new System.Windows.Forms.Padding(0);
			this.bufferSettings7.Name = "bufferSettings7";
			this.bufferSettings7.Size = new System.Drawing.Size(436, 83);
			this.bufferSettings7.TabIndex = 14;
			this.bufferSettings7.Title = "Data";
			// 
			// frmManual
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1350, 729);
			this.ControlBox = false;
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmManual";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Manual";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManual_FormClosed);
			this.Load += new System.EventHandler(this.frmManual_Load);
			this.Move += new System.EventHandler(this.frmManual_Move);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private ConveyorManual startStopButton_BT_REJECT;
    private ConveyorManual startStopButton_BT_CAN;
    private ConveyorManual startStopButton_BT_TACH_CHAI;
    private System.Windows.Forms.Button btExit;
    private ConveyorManual startStopConveyorALL;
    private SwitchManManual switchManManual1;
    private SwitchManManual switchBuzzerTest;
    private SwitchManManual switchEnableDisableWeigher;
    private System.Windows.Forms.Button btDemoMode;
    private ManualMode.BufferSettings bufferSettings3;
    private ManualMode.BufferSettings bufferSettings2;
    private ManualMode.BufferSettings bufferSettings1;
    private ManualMode.BufferSettings bufferSettings6;
    private ManualMode.BufferSettings bufferSettings5;
    private ManualMode.BufferSettings bufferSettings4;
    private SwitchManManual switchCyclinderOnOff;
    private ManualMode.BufferSettings bufferSettings8;
    private ManualMode.BufferSettings bufferSettings7;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.Button btViewPLCMemory;
    private ButtonOnOffInvert buttonCyclinderOnOff;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private SwitchManManual switchEnableDisableAutoAssignChangeover;
		private SwitchManManual switchEnableDisableReject;
	}
}