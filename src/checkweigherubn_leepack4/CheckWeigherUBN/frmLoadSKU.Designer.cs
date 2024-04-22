namespace CheckWeigherUBN
{
  partial class frmLoadSKU
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadSKU));
			this.cbSKU = new System.Windows.Forms.ComboBox();
			this.btExit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cbSKU
			// 
			this.cbSKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbSKU.FormattingEnabled = true;
			this.cbSKU.Location = new System.Drawing.Point(12, 16);
			this.cbSKU.Name = "cbSKU";
			this.cbSKU.Size = new System.Drawing.Size(412, 41);
			this.cbSKU.TabIndex = 8;
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btExit.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btExit.FlatAppearance.BorderSize = 5;
			this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btExit.ForeColor = System.Drawing.Color.White;
			this.btExit.Image = global::CheckWeigherUBN.Properties.Resources.icons8_Shutdown_30px;
			this.btExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btExit.Location = new System.Drawing.Point(453, 10);
			this.btExit.Name = "btExit";
			this.btExit.Size = new System.Drawing.Size(138, 52);
			this.btExit.TabIndex = 13;
			this.btExit.Text = "  OK";
			this.btExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btExit.UseVisualStyleBackColor = false;
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// frmLoadSKU
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(601, 70);
			this.ControlBox = false;
			this.Controls.Add(this.btExit);
			this.Controls.Add(this.cbSKU);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmLoadSKU";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Load SKU";
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cbSKU;
    private System.Windows.Forms.Button btExit;
  }
}