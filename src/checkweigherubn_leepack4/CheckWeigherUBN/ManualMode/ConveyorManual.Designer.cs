namespace CheckWeigherUBN
{
  partial class ConveyorManual
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.btSTART = new System.Windows.Forms.Button();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.lblStatus = new System.Windows.Forms.Label();
			this.conveyorSpeed = new CircularProgressBar();
			this.lblTitle = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btSetValue = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lblSpeedInput = new System.Windows.Forms.TextBox();
			this.timer_START_Button = new System.Windows.Forms.Timer(this.components);
			this.timer_STOP_Button = new System.Windows.Forms.Timer(this.components);
			this.timer_delay = new System.Windows.Forms.Timer(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.67658F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.22305F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.72862F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(289, 269);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.63668F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.36332F));
			this.tableLayoutPanel3.Controls.Add(this.btSTART, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 61);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(289, 162);
			this.tableLayoutPanel3.TabIndex = 6;
			// 
			// btSTART
			// 
			this.btSTART.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btSTART.Image = global::CheckWeigherUBN.Properties.Resources.START_Button_1_OFF;
			this.btSTART.Location = new System.Drawing.Point(0, 0);
			this.btSTART.Margin = new System.Windows.Forms.Padding(0);
			this.btSTART.Name = "btSTART";
			this.btSTART.Size = new System.Drawing.Size(129, 162);
			this.btSTART.TabIndex = 76;
			this.btSTART.UseVisualStyleBackColor = true;
			this.btSTART.Click += new System.EventHandler(this.btSTART_Click);
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Controls.Add(this.lblStatus, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.conveyorSpeed, 0, 0);
			this.tableLayoutPanel4.Location = new System.Drawing.Point(129, 0);
			this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.76471F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.23529F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(160, 162);
			this.tableLayoutPanel4.TabIndex = 77;
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatus.BackColor = System.Drawing.Color.Red;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.White;
			this.lblStatus.Location = new System.Drawing.Point(0, 132);
			this.lblStatus.Margin = new System.Windows.Forms.Padding(0);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(160, 30);
			this.lblStatus.TabIndex = 5;
			this.lblStatus.Text = "STOP";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// conveyorSpeed
			// 
			this.conveyorSpeed.BackColor = System.Drawing.Color.White;
			this.conveyorSpeed.CircularColor = System.Drawing.Color.Blue;
			this.conveyorSpeed.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.conveyorSpeed.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.conveyorSpeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.conveyorSpeed.Location = new System.Drawing.Point(20, 8);
			this.conveyorSpeed.Margin = new System.Windows.Forms.Padding(20, 8, 3, 3);
			this.conveyorSpeed.Maximum = ((long)(100));
			this.conveyorSpeed.MinimumSize = new System.Drawing.Size(100, 100);
			this.conveyorSpeed.Name = "conveyorSpeed";
			this.conveyorSpeed.ProgressColor1 = System.Drawing.Color.Gold;
			this.conveyorSpeed.ProgressColor2 = System.Drawing.Color.White;
			this.conveyorSpeed.ProgressShape = CircularProgressBar._ProgressShape.Flat;
			this.conveyorSpeed.Size = new System.Drawing.Size(118, 118);
			this.conveyorSpeed.TabIndex = 75;
			this.conveyorSpeed.Text = "circularProgressBar1";
			this.conveyorSpeed.Value = ((long)(50));
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTitle.BackColor = System.Drawing.Color.Teal;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(0, 0);
			this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(289, 61);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Tách chai";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
			this.tableLayoutPanel2.Controls.Add(this.btSetValue, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblSpeedInput, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 226);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(283, 40);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// btSetValue
			// 
			this.btSetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btSetValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btSetValue.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btSetValue.FlatAppearance.BorderSize = 4;
			this.btSetValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSetValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btSetValue.ForeColor = System.Drawing.Color.White;
			this.btSetValue.Location = new System.Drawing.Point(200, 0);
			this.btSetValue.Margin = new System.Windows.Forms.Padding(0);
			this.btSetValue.Name = "btSetValue";
			this.btSetValue.Size = new System.Drawing.Size(83, 40);
			this.btSetValue.TabIndex = 4;
			this.btSetValue.Text = "SET";
			this.btSetValue.UseVisualStyleBackColor = false;
			this.btSetValue.Click += new System.EventHandler(this.btSetValue_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 40);
			this.label1.TabIndex = 3;
			this.label1.Text = "Tốc độ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSpeedInput
			// 
			this.lblSpeedInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSpeedInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSpeedInput.Location = new System.Drawing.Point(105, 3);
			this.lblSpeedInput.Name = "lblSpeedInput";
			this.lblSpeedInput.Size = new System.Drawing.Size(92, 35);
			this.lblSpeedInput.TabIndex = 2;
			this.lblSpeedInput.Click += new System.EventHandler(this.lblSpeedInput_Click);
			this.lblSpeedInput.TextChanged += new System.EventHandler(this.lblSpeedInput_TextChanged);
			// 
			// timer_START_Button
			// 
			this.timer_START_Button.Interval = 500;
			// 
			// timer_STOP_Button
			// 
			this.timer_STOP_Button.Interval = 500;
			this.timer_STOP_Button.Tick += new System.EventHandler(this.timer_STOP_Button_Tick);
			// 
			// timer_delay
			// 
			this.timer_delay.Tick += new System.EventHandler(this.timer_delay_Tick);
			// 
			// ConveyorManual
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ConveyorManual";
			this.Size = new System.Drawing.Size(289, 269);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Timer timer_START_Button;
    private System.Windows.Forms.Timer timer_STOP_Button;
    private System.Windows.Forms.TextBox lblSpeedInput;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btSetValue;
    private System.Windows.Forms.Timer timer_delay;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Button btSTART;
    private CircularProgressBar conveyorSpeed;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
	}
}
