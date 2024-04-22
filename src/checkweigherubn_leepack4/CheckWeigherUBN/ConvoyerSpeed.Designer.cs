namespace CheckWeigherUBN
{
  partial class ConvoyerSpeed
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvoyerSpeed));
      this.grouper5 = new CodeVendor.Controls.Grouper();
      this.pictureBox11 = new System.Windows.Forms.PictureBox();
      this.label27 = new System.Windows.Forms.Label();
      this.circularProgressBar1 = new CircularProgressBar();
      this.grouper5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
      this.SuspendLayout();
      // 
      // grouper5
      // 
      this.grouper5.BackgroundColor = System.Drawing.Color.White;
      this.grouper5.BackgroundGradientColor = System.Drawing.Color.White;
      this.grouper5.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
      this.grouper5.BorderColor = System.Drawing.Color.Black;
      this.grouper5.BorderThickness = 1F;
      this.grouper5.Controls.Add(this.circularProgressBar1);
      this.grouper5.Controls.Add(this.pictureBox11);
      this.grouper5.Controls.Add(this.label27);
      this.grouper5.CustomGroupBoxColor = System.Drawing.Color.White;
      this.grouper5.GroupImage = null;
      this.grouper5.GroupTitle = "";
      this.grouper5.Location = new System.Drawing.Point(3, 3);
      this.grouper5.Name = "grouper5";
      this.grouper5.Padding = new System.Windows.Forms.Padding(20);
      this.grouper5.PaintGroupBox = false;
      this.grouper5.RoundCorners = 10;
      this.grouper5.ShadowColor = System.Drawing.SystemColors.Desktop;
      this.grouper5.ShadowControl = true;
      this.grouper5.ShadowThickness = 3;
      this.grouper5.Size = new System.Drawing.Size(163, 209);
      this.grouper5.TabIndex = 74;
      // 
      // pictureBox11
      // 
      this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
      this.pictureBox11.Location = new System.Drawing.Point(10, 31);
      this.pictureBox11.Name = "pictureBox11";
      this.pictureBox11.Size = new System.Drawing.Size(110, 4);
      this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox11.TabIndex = 1;
      this.pictureBox11.TabStop = false;
      // 
      // label27
      // 
      this.label27.AutoSize = true;
      this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label27.Location = new System.Drawing.Point(5, 11);
      this.label27.Name = "label27";
      this.label27.Size = new System.Drawing.Size(76, 20);
      this.label27.TabIndex = 2;
      this.label27.Text = "Băng tải";
      // 
      // circularProgressBar1
      // 
      this.circularProgressBar1.BackColor = System.Drawing.Color.White;
      this.circularProgressBar1.CircularColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
      this.circularProgressBar1.Font = new System.Drawing.Font("Segoe UI", 15F);
      this.circularProgressBar1.Location = new System.Drawing.Point(10, 41);
      this.circularProgressBar1.Maximum = ((long)(100));
      this.circularProgressBar1.MinimumSize = new System.Drawing.Size(100, 100);
      this.circularProgressBar1.Name = "circularProgressBar1";
      this.circularProgressBar1.ProgressColor1 = System.Drawing.Color.Blue;
      this.circularProgressBar1.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.circularProgressBar1.ProgressShape = CircularProgressBar._ProgressShape.Round;
      this.circularProgressBar1.Size = new System.Drawing.Size(145, 145);
      this.circularProgressBar1.TabIndex = 74;
      this.circularProgressBar1.Text = "circularProgressBar1";
      this.circularProgressBar1.Value = ((long)(50));
      // 
      // ConvoyerSpeed
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.grouper5);
      this.Name = "ConvoyerSpeed";
      this.Size = new System.Drawing.Size(180, 228);
      this.grouper5.ResumeLayout(false);
      this.grouper5.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private CodeVendor.Controls.Grouper grouper5;
    private CircularProgressBar circularProgressBar1;
    private System.Windows.Forms.PictureBox pictureBox11;
    private System.Windows.Forms.Label label27;


  }
}
