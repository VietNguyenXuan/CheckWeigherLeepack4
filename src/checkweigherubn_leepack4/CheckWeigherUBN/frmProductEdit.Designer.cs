namespace CheckWeigherUBN
{
  partial class frmProductEdit
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtFGs = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.PM = new System.Windows.Forms.Label();
			this.txtTarget = new System.Windows.Forms.TextBox();
			this.txtPM = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtLowerLimit_1T = new System.Windows.Forms.TextBox();
			this.txtUpperLimit_1T = new System.Windows.Forms.TextBox();
			this.txtSKU = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.btLoadSKU = new System.Windows.Forms.Button();
			this.btExit = new System.Windows.Forms.Button();
			this.btSave = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtLowerLimit_2T = new System.Windows.Forms.TextBox();
			this.txtUpperLimit_2T = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 94);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(147, 33);
			this.label1.TabIndex = 0;
			this.label1.Text = "Description";
			// 
			// txtDescription
			// 
			this.txtDescription.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDescription.Location = new System.Drawing.Point(244, 88);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(643, 41);
			this.txtDescription.TabIndex = 1;
			this.txtDescription.Click += new System.EventHandler(this.txtDescription_Click);
			this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
			this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(15, 155);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 33);
			this.label2.TabIndex = 2;
			this.label2.Text = "FGs";
			// 
			// txtFGs
			// 
			this.txtFGs.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFGs.Location = new System.Drawing.Point(244, 152);
			this.txtFGs.Name = "txtFGs";
			this.txtFGs.Size = new System.Drawing.Size(143, 41);
			this.txtFGs.TabIndex = 3;
			this.txtFGs.Click += new System.EventHandler(this.txtFGs_Click);
			this.txtFGs.TextChanged += new System.EventHandler(this.txtBarcode_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(13, 219);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118, 33);
			this.label3.TabIndex = 4;
			this.label3.Text = "Target(g)";
			// 
			// PM
			// 
			this.PM.AutoSize = true;
			this.PM.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PM.Location = new System.Drawing.Point(13, 293);
			this.PM.Name = "PM";
			this.PM.Size = new System.Drawing.Size(90, 33);
			this.PM.TabIndex = 5;
			this.PM.Text = "PM(g)";
			// 
			// txtTarget
			// 
			this.txtTarget.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTarget.Location = new System.Drawing.Point(244, 219);
			this.txtTarget.Name = "txtTarget";
			this.txtTarget.Size = new System.Drawing.Size(143, 41);
			this.txtTarget.TabIndex = 6;
			this.txtTarget.Click += new System.EventHandler(this.txtTarget_Click);
			this.txtTarget.TextChanged += new System.EventHandler(this.txtTarget_TextChanged);
			// 
			// txtPM
			// 
			this.txtPM.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPM.Location = new System.Drawing.Point(244, 287);
			this.txtPM.Name = "txtPM";
			this.txtPM.Size = new System.Drawing.Size(143, 41);
			this.txtPM.TabIndex = 7;
			this.txtPM.Click += new System.EventHandler(this.txtDiff_Click);
			this.txtPM.TextChanged += new System.EventHandler(this.txtDiff_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(13, 360);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(230, 33);
			this.label5.TabIndex = 8;
			this.label5.Text = "Lower Limit 1T(g)";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(13, 429);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(228, 33);
			this.label6.TabIndex = 9;
			this.label6.Text = "Upper Limit 1T(g)";
			// 
			// txtLowerLimit_1T
			// 
			this.txtLowerLimit_1T.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLowerLimit_1T.Location = new System.Drawing.Point(244, 356);
			this.txtLowerLimit_1T.Name = "txtLowerLimit_1T";
			this.txtLowerLimit_1T.Size = new System.Drawing.Size(143, 41);
			this.txtLowerLimit_1T.TabIndex = 10;
			this.txtLowerLimit_1T.Click += new System.EventHandler(this.txtLowerLimit_Click);
			this.txtLowerLimit_1T.TextChanged += new System.EventHandler(this.txtLowerLimit_TextChanged);
			// 
			// txtUpperLimit_1T
			// 
			this.txtUpperLimit_1T.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUpperLimit_1T.Location = new System.Drawing.Point(244, 425);
			this.txtUpperLimit_1T.Name = "txtUpperLimit_1T";
			this.txtUpperLimit_1T.Size = new System.Drawing.Size(143, 41);
			this.txtUpperLimit_1T.TabIndex = 11;
			this.txtUpperLimit_1T.Click += new System.EventHandler(this.txtUpperLimit_Click);
			this.txtUpperLimit_1T.TextChanged += new System.EventHandler(this.txtUpperLimit_TextChanged);
			// 
			// txtSKU
			// 
			this.txtSKU.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSKU.Location = new System.Drawing.Point(244, 25);
			this.txtSKU.Name = "txtSKU";
			this.txtSKU.Size = new System.Drawing.Size(459, 41);
			this.txtSKU.TabIndex = 15;
			this.txtSKU.Click += new System.EventHandler(this.txtSKU_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(13, 31);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 33);
			this.label7.TabIndex = 14;
			this.label7.Text = "SKU";
			// 
			// btLoadSKU
			// 
			this.btLoadSKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btLoadSKU.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btLoadSKU.FlatAppearance.BorderSize = 5;
			this.btLoadSKU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btLoadSKU.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btLoadSKU.ForeColor = System.Drawing.Color.White;
			this.btLoadSKU.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btLoadSKU.Location = new System.Drawing.Point(723, 22);
			this.btLoadSKU.Name = "btLoadSKU";
			this.btLoadSKU.Size = new System.Drawing.Size(164, 50);
			this.btLoadSKU.TabIndex = 16;
			this.btLoadSKU.Text = "Load SKU";
			this.btLoadSKU.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btLoadSKU.UseVisualStyleBackColor = false;
			this.btLoadSKU.Click += new System.EventHandler(this.btLoadSKU_Click);
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
			this.btExit.Location = new System.Drawing.Point(723, 520);
			this.btExit.Name = "btExit";
			this.btExit.Size = new System.Drawing.Size(164, 59);
			this.btExit.TabIndex = 13;
			this.btExit.Text = "  Thoát";
			this.btExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btExit.UseVisualStyleBackColor = false;
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// btSave
			// 
			this.btSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btSave.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btSave.FlatAppearance.BorderSize = 5;
			this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSave.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btSave.ForeColor = System.Drawing.Color.White;
			this.btSave.Image = global::CheckWeigherUBN.Properties.Resources.Save_25px;
			this.btSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btSave.Location = new System.Drawing.Point(513, 520);
			this.btSave.Name = "btSave";
			this.btSave.Size = new System.Drawing.Size(173, 59);
			this.btSave.TabIndex = 12;
			this.btSave.Text = "    Lưu";
			this.btSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btSave.UseVisualStyleBackColor = false;
			this.btSave.Click += new System.EventHandler(this.btSave_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(510, 360);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(230, 33);
			this.label8.TabIndex = 17;
			this.label8.Text = "Lower Limit 2T(g)";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(510, 429);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(228, 33);
			this.label9.TabIndex = 18;
			this.label9.Text = "Upper Limit 2T(g)";
			// 
			// txtLowerLimit_2T
			// 
			this.txtLowerLimit_2T.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLowerLimit_2T.Location = new System.Drawing.Point(752, 356);
			this.txtLowerLimit_2T.Name = "txtLowerLimit_2T";
			this.txtLowerLimit_2T.Size = new System.Drawing.Size(133, 41);
			this.txtLowerLimit_2T.TabIndex = 19;
			this.txtLowerLimit_2T.Click += new System.EventHandler(this.txtLowerLimit_2T_Click);
			// 
			// txtUpperLimit_2T
			// 
			this.txtUpperLimit_2T.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUpperLimit_2T.Location = new System.Drawing.Point(752, 426);
			this.txtUpperLimit_2T.Name = "txtUpperLimit_2T";
			this.txtUpperLimit_2T.Size = new System.Drawing.Size(133, 41);
			this.txtUpperLimit_2T.TabIndex = 20;
			this.txtUpperLimit_2T.Click += new System.EventHandler(this.txtUpperLimit_2T_Click);
			// 
			// frmProductEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(903, 592);
			this.ControlBox = false;
			this.Controls.Add(this.txtUpperLimit_2T);
			this.Controls.Add(this.txtLowerLimit_2T);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.btLoadSKU);
			this.Controls.Add(this.txtSKU);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btExit);
			this.Controls.Add(this.btSave);
			this.Controls.Add(this.txtUpperLimit_1T);
			this.Controls.Add(this.txtLowerLimit_1T);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtPM);
			this.Controls.Add(this.txtTarget);
			this.Controls.Add(this.PM);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtFGs);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmProductEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Product Edit";
			this.Load += new System.EventHandler(this.frmProductEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtFGs;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label PM;
    private System.Windows.Forms.TextBox txtTarget;
    private System.Windows.Forms.TextBox txtPM;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtLowerLimit_1T;
    private System.Windows.Forms.TextBox txtUpperLimit_1T;
    private System.Windows.Forms.Button btExit;
    private System.Windows.Forms.Button btSave;
    private System.Windows.Forms.TextBox txtSKU;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btLoadSKU;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtLowerLimit_2T;
    private System.Windows.Forms.TextBox txtUpperLimit_2T;
  }
}