namespace CheckWeigherUBN
{
  partial class frmLogin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
			this.label5 = new System.Windows.Forms.Label();
			this.cbUser = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btOK = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(17, 66);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(71, 32);
			this.label5.TabIndex = 7;
			this.label5.Text = "User";
			// 
			// cbUser
			// 
			this.cbUser.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbUser.FormattingEnabled = true;
			this.cbUser.Location = new System.Drawing.Point(153, 65);
			this.cbUser.Name = "cbUser";
			this.cbUser.Size = new System.Drawing.Size(243, 40);
			this.cbUser.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(17, 132);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 32);
			this.label1.TabIndex = 11;
			this.label1.Text = "Password";
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.Location = new System.Drawing.Point(153, 127);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(244, 41);
			this.txtPassword.TabIndex = 12;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtPassword.Click += new System.EventHandler(this.txtPassword_Click);
			// 
			// btOK
			// 
			this.btOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btOK.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btOK.FlatAppearance.BorderSize = 5;
			this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btOK.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btOK.ForeColor = System.Drawing.Color.White;
			this.btOK.Location = new System.Drawing.Point(235, 199);
			this.btOK.Name = "btOK";
			this.btOK.Size = new System.Drawing.Size(162, 52);
			this.btOK.TabIndex = 13;
			this.btOK.Text = "OK";
			this.btOK.UseVisualStyleBackColor = false;
			this.btOK.Click += new System.EventHandler(this.btOK_Click);
			// 
			// btCancel
			// 
			this.btCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btCancel.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btCancel.FlatAppearance.BorderSize = 5;
			this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btCancel.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btCancel.ForeColor = System.Drawing.Color.White;
			this.btCancel.Location = new System.Drawing.Point(26, 199);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(162, 52);
			this.btCancel.TabIndex = 14;
			this.btCancel.Text = "Cancel";
			this.btCancel.UseVisualStyleBackColor = false;
			this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(133, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(159, 32);
			this.label2.TabIndex = 15;
			this.label2.Text = "Đăng nhập:";
			// 
			// frmLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Teal;
			this.ClientSize = new System.Drawing.Size(420, 263);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.btOK);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbUser);
			this.Controls.Add(this.label5);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmLogin_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cbUser;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button btOK;
    private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.Label label2;
	}
}