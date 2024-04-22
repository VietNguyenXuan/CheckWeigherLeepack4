namespace CheckWeigherUBN
{
  partial class frmPassword
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPassword));
			this.btExit = new System.Windows.Forms.Button();
			this.passwordChangeManager = new CheckWeigherUBN.PasswordChange();
			this.passwordChangeME = new CheckWeigherUBN.PasswordChange();
			this.passwordChangeQuality = new CheckWeigherUBN.PasswordChange();
			this.passwordChangeOPshift3 = new CheckWeigherUBN.PasswordChange();
			this.passwordChangeOPshift2 = new CheckWeigherUBN.PasswordChange();
			this.passwordChangeOPshift1 = new CheckWeigherUBN.PasswordChange();
			this.SuspendLayout();
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
			this.btExit.Location = new System.Drawing.Point(213, 469);
			this.btExit.Name = "btExit";
			this.btExit.Size = new System.Drawing.Size(191, 60);
			this.btExit.TabIndex = 11;
			this.btExit.Text = "   Thoát";
			this.btExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btExit.UseVisualStyleBackColor = false;
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// passwordChangeManager
			// 
			this.passwordChangeManager.BackColor = System.Drawing.Color.White;
			this.passwordChangeManager.Location = new System.Drawing.Point(19, 359);
			this.passwordChangeManager.Name = "passwordChangeManager";
			this.passwordChangeManager.Size = new System.Drawing.Size(554, 57);
			this.passwordChangeManager.TabIndex = 5;
			// 
			// passwordChangeME
			// 
			this.passwordChangeME.BackColor = System.Drawing.Color.White;
			this.passwordChangeME.Location = new System.Drawing.Point(19, 290);
			this.passwordChangeME.Name = "passwordChangeME";
			this.passwordChangeME.Size = new System.Drawing.Size(554, 57);
			this.passwordChangeME.TabIndex = 4;
			// 
			// passwordChangeQuality
			// 
			this.passwordChangeQuality.BackColor = System.Drawing.Color.White;
			this.passwordChangeQuality.Location = new System.Drawing.Point(19, 221);
			this.passwordChangeQuality.Name = "passwordChangeQuality";
			this.passwordChangeQuality.Size = new System.Drawing.Size(554, 57);
			this.passwordChangeQuality.TabIndex = 3;
			// 
			// passwordChangeOPshift3
			// 
			this.passwordChangeOPshift3.BackColor = System.Drawing.Color.White;
			this.passwordChangeOPshift3.Location = new System.Drawing.Point(19, 152);
			this.passwordChangeOPshift3.Name = "passwordChangeOPshift3";
			this.passwordChangeOPshift3.Size = new System.Drawing.Size(554, 57);
			this.passwordChangeOPshift3.TabIndex = 2;
			// 
			// passwordChangeOPshift2
			// 
			this.passwordChangeOPshift2.BackColor = System.Drawing.Color.White;
			this.passwordChangeOPshift2.Location = new System.Drawing.Point(19, 81);
			this.passwordChangeOPshift2.Name = "passwordChangeOPshift2";
			this.passwordChangeOPshift2.Size = new System.Drawing.Size(554, 57);
			this.passwordChangeOPshift2.TabIndex = 1;
			// 
			// passwordChangeOPshift1
			// 
			this.passwordChangeOPshift1.BackColor = System.Drawing.Color.White;
			this.passwordChangeOPshift1.Location = new System.Drawing.Point(19, 12);
			this.passwordChangeOPshift1.Name = "passwordChangeOPshift1";
			this.passwordChangeOPshift1.Size = new System.Drawing.Size(554, 57);
			this.passwordChangeOPshift1.TabIndex = 0;
			// 
			// frmPassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(615, 553);
			this.ControlBox = false;
			this.Controls.Add(this.btExit);
			this.Controls.Add(this.passwordChangeManager);
			this.Controls.Add(this.passwordChangeME);
			this.Controls.Add(this.passwordChangeQuality);
			this.Controls.Add(this.passwordChangeOPshift3);
			this.Controls.Add(this.passwordChangeOPshift2);
			this.Controls.Add(this.passwordChangeOPshift1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmPassword";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Password";
			this.Load += new System.EventHandler(this.frmPassword_Load);
			this.ResumeLayout(false);

    }

    #endregion

    private PasswordChange passwordChangeOPshift1;
    private PasswordChange passwordChangeOPshift2;
    private PasswordChange passwordChangeOPshift3;
    private PasswordChange passwordChangeQuality;
    private PasswordChange passwordChangeME;
    private PasswordChange passwordChangeManager;
    private System.Windows.Forms.Button btExit;
  }
}