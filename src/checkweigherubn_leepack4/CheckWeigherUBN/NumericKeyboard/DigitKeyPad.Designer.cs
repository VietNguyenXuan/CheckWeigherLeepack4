namespace CheckWeigherUBN.NumericKeyboard
{
  partial class DigitKeyPad
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DigitKeyPad));
			this.lblKeyPad = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// lblKeyPad
			// 
			this.lblKeyPad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.lblKeyPad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblKeyPad.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblKeyPad.ForeColor = System.Drawing.Color.Black;
			this.lblKeyPad.Location = new System.Drawing.Point(22, 12);
			this.lblKeyPad.Name = "lblKeyPad";
			this.lblKeyPad.Size = new System.Drawing.Size(75, 48);
			this.lblKeyPad.TabIndex = 88;
			this.lblKeyPad.Text = "1";
			this.lblKeyPad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblKeyPad.Click += new System.EventHandler(this.lblKeyPad_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(119, 72);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 87;
			this.pictureBox1.TabStop = false;
			// 
			// DigitKeyPad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.lblKeyPad);
			this.Controls.Add(this.pictureBox1);
			this.Name = "DigitKeyPad";
			this.Size = new System.Drawing.Size(119, 72);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblKeyPad;
    private System.Windows.Forms.PictureBox pictureBox1;
  }
}
