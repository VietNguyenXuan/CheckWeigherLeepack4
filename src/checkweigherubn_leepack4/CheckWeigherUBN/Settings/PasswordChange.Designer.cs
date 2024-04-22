namespace CheckWeigherUBN
{
  partial class PasswordChange
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblRole = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btChange = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::CheckWeigherUBN.Properties.Resources.user_25px;
			this.pictureBox1.Location = new System.Drawing.Point(3, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(27, 30);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblRole
			// 
			this.lblRole.AutoSize = true;
			this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRole.Location = new System.Drawing.Point(36, 12);
			this.lblRole.Name = "lblRole";
			this.lblRole.Size = new System.Drawing.Size(93, 33);
			this.lblRole.TabIndex = 1;
			this.lblRole.Text = "label1";
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.Location = new System.Drawing.Point(194, 9);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(173, 40);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtPassword.Click += new System.EventHandler(this.txtPassword_Click);
			// 
			// btChange
			// 
			this.btChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btChange.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btChange.FlatAppearance.BorderSize = 5;
			this.btChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btChange.ForeColor = System.Drawing.Color.White;
			this.btChange.Location = new System.Drawing.Point(392, 3);
			this.btChange.Name = "btChange";
			this.btChange.Size = new System.Drawing.Size(159, 51);
			this.btChange.TabIndex = 10;
			this.btChange.Text = "Change";
			this.btChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btChange.UseVisualStyleBackColor = false;
			this.btChange.Click += new System.EventHandler(this.btChange_Click);
			// 
			// PasswordChange
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.btChange);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.lblRole);
			this.Controls.Add(this.pictureBox1);
			this.Name = "PasswordChange";
			this.Size = new System.Drawing.Size(554, 57);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label lblRole;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button btChange;
  }
}
