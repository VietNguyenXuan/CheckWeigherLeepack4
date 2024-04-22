namespace CheckWeigherUBN
{
  partial class ConvoyerDisplay
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.label1 = new System.Windows.Forms.Label();
      this.convoyerSpeed3 = new CheckWeigherUBN.ConvoyerSpeed();
      this.convoyerSpeed2 = new CheckWeigherUBN.ConvoyerSpeed();
      this.convoyerSpeed1 = new CheckWeigherUBN.ConvoyerSpeed();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.label1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.convoyerSpeed3);
      this.splitContainer1.Panel2.Controls.Add(this.convoyerSpeed2);
      this.splitContainer1.Panel2.Controls.Add(this.convoyerSpeed1);
      this.splitContainer1.Size = new System.Drawing.Size(605, 271);
      this.splitContainer1.SplitterDistance = 25;
      this.splitContainer1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.SystemColors.Highlight;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(605, 25);
      this.label1.TabIndex = 1;
      this.label1.Text = "Thông tin băng tải";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // convoyerSpeed3
      // 
      this.convoyerSpeed3.BackColor = System.Drawing.Color.White;
      this.convoyerSpeed3.Location = new System.Drawing.Point(420, 2);
      this.convoyerSpeed3.Name = "convoyerSpeed3";
      this.convoyerSpeed3.Size = new System.Drawing.Size(182, 238);
      this.convoyerSpeed3.TabIndex = 5;
      // 
      // convoyerSpeed2
      // 
      this.convoyerSpeed2.BackColor = System.Drawing.Color.White;
      this.convoyerSpeed2.Location = new System.Drawing.Point(213, 2);
      this.convoyerSpeed2.Name = "convoyerSpeed2";
      this.convoyerSpeed2.Size = new System.Drawing.Size(182, 238);
      this.convoyerSpeed2.TabIndex = 4;
      // 
      // convoyerSpeed1
      // 
      this.convoyerSpeed1.BackColor = System.Drawing.Color.White;
      this.convoyerSpeed1.Location = new System.Drawing.Point(6, 2);
      this.convoyerSpeed1.Name = "convoyerSpeed1";
      this.convoyerSpeed1.Size = new System.Drawing.Size(182, 238);
      this.convoyerSpeed1.TabIndex = 3;
      // 
      // ConvoyerDisplay
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.splitContainer1);
      this.Name = "ConvoyerDisplay";
      this.Size = new System.Drawing.Size(605, 271);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private ConvoyerSpeed convoyerSpeed3;
    private ConvoyerSpeed convoyerSpeed2;
    private ConvoyerSpeed convoyerSpeed1;
    private System.Windows.Forms.Label label1;



  }
}
