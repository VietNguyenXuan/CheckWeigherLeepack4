namespace CheckWeigherUBN
{
  partial class SwitchManManual
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btSwitchOnOff = new System.Windows.Forms.Button();
			this.timer_START_Button = new System.Windows.Forms.Timer(this.components);
			this.timer_STOP_Button = new System.Windows.Forms.Timer(this.components);
			this.timer_delay = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.splitContainer2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(143, 269);
			this.panel1.TabIndex = 5;
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
			this.splitContainer2.Panel1.Controls.Add(this.lblTitle);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.btSwitchOnOff);
			this.splitContainer2.Size = new System.Drawing.Size(139, 265);
			this.splitContainer2.SplitterDistance = 61;
			this.splitContainer2.TabIndex = 0;
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.Teal;
			this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(0, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(135, 57);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Control Mode";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btSwitchOnOff
			// 
			this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_DISABLE;
			this.btSwitchOnOff.Location = new System.Drawing.Point(-2, -5);
			this.btSwitchOnOff.Name = "btSwitchOnOff";
			this.btSwitchOnOff.Size = new System.Drawing.Size(141, 205);
			this.btSwitchOnOff.TabIndex = 0;
			this.btSwitchOnOff.UseVisualStyleBackColor = true;
			this.btSwitchOnOff.Click += new System.EventHandler(this.btSwitchOnOff_Click);
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
			this.timer_delay.Interval = 250;
			this.timer_delay.Tick += new System.EventHandler(this.timer_delay_Tick);
			// 
			// SwitchManManual
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.panel1);
			this.Name = "SwitchManManual";
			this.Size = new System.Drawing.Size(143, 269);
			this.panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Timer timer_START_Button;
    private System.Windows.Forms.Timer timer_STOP_Button;
    private System.Windows.Forms.Button btSwitchOnOff;
    private System.Windows.Forms.Timer timer_delay;
  }
}
