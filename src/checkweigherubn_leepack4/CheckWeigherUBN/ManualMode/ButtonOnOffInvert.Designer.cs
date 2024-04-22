namespace CheckWeigherUBN
{
  partial class ButtonOnOffInvert
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
			this.lblTitle = new System.Windows.Forms.Label();
			this.btStartStop = new System.Windows.Forms.Button();
			this.timer_START_Button = new System.Windows.Forms.Timer(this.components);
			this.timer_STOP_Button = new System.Windows.Forms.Timer(this.components);
			this.timer_delay = new System.Windows.Forms.Timer(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTitle.BackColor = System.Drawing.Color.Teal;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(3, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(131, 61);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Control Mode";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btStartStop
			// 
			this.btStartStop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btStartStop.Image = global::CheckWeigherUBN.Properties.Resources.START_Button_1_ON;
			this.btStartStop.Location = new System.Drawing.Point(0, 61);
			this.btStartStop.Margin = new System.Windows.Forms.Padding(0);
			this.btStartStop.Name = "btStartStop";
			this.btStartStop.Size = new System.Drawing.Size(137, 208);
			this.btStartStop.TabIndex = 0;
			this.btStartStop.UseVisualStyleBackColor = true;
			this.btStartStop.Click += new System.EventHandler(this.btStartStop_Click);
			// 
			// timer_START_Button
			// 
			this.timer_START_Button.Interval = 500;
			// 
			// timer_STOP_Button
			// 
			this.timer_STOP_Button.Interval = 500;
			// 
			// timer_delay
			// 
			this.timer_delay.Interval = 500;
			this.timer_delay.Tick += new System.EventHandler(this.timer_delay_Tick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.btStartStop, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.67658F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.32342F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(137, 269);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// ButtonOnOffInvert
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ButtonOnOffInvert";
			this.Size = new System.Drawing.Size(137, 269);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Timer timer_START_Button;
    private System.Windows.Forms.Timer timer_STOP_Button;
    private System.Windows.Forms.Button btStartStop;
    private System.Windows.Forms.Timer timer_delay;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}
