namespace CheckWeigherUBN
{
  partial class frmSettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btFolderBrowser = new System.Windows.Forms.Button();
			this.txtReportPath = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.txtDatabasePath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTemplatePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtPortNumber = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPLC_IPAddress = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cbBoxProducts = new System.Windows.Forms.ComboBox();
			this.label16 = new System.Windows.Forms.Label();
			this.txtMaxProductDisplay = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label15 = new System.Windows.Forms.Label();
			this.txtPC_Reject_Number_Box = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.txtPC_Delay_Reject = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtPC_Reject_Time_Box_Conti = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtPC_Reject_Time = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtPC_Delay_Barcode = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btPassword = new System.Windows.Forms.Button();
			this.btMatrixPermission = new System.Windows.Forms.Button();
			this.btExit = new System.Windows.Forms.Button();
			this.btSave = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btFolderBrowser);
			this.groupBox1.Controls.Add(this.txtReportPath);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.txtDatabasePath);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtTemplatePath);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1026, 207);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Template && Database";
			// 
			// btFolderBrowser
			// 
			this.btFolderBrowser.BackColor = System.Drawing.Color.Transparent;
			this.btFolderBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btFolderBrowser.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btFolderBrowser.ForeColor = System.Drawing.Color.White;
			this.btFolderBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btFolderBrowser.Image")));
			this.btFolderBrowser.Location = new System.Drawing.Point(937, 144);
			this.btFolderBrowser.Name = "btFolderBrowser";
			this.btFolderBrowser.Size = new System.Drawing.Size(69, 57);
			this.btFolderBrowser.TabIndex = 11;
			this.btFolderBrowser.UseVisualStyleBackColor = false;
			this.btFolderBrowser.Click += new System.EventHandler(this.btFolderBrowser_Click);
			// 
			// txtReportPath
			// 
			this.txtReportPath.Location = new System.Drawing.Point(184, 153);
			this.txtReportPath.Name = "txtReportPath";
			this.txtReportPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtReportPath.Size = new System.Drawing.Size(736, 41);
			this.txtReportPath.TabIndex = 5;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(21, 157);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(149, 33);
			this.label17.TabIndex = 4;
			this.label17.Text = "Report Path";
			// 
			// txtDatabasePath
			// 
			this.txtDatabasePath.Location = new System.Drawing.Point(184, 98);
			this.txtDatabasePath.Name = "txtDatabasePath";
			this.txtDatabasePath.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtDatabasePath.Size = new System.Drawing.Size(736, 41);
			this.txtDatabasePath.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 102);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118, 33);
			this.label2.TabIndex = 2;
			this.label2.Text = "Database";
			// 
			// txtTemplatePath
			// 
			this.txtTemplatePath.Location = new System.Drawing.Point(184, 41);
			this.txtTemplatePath.Name = "txtTemplatePath";
			this.txtTemplatePath.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtTemplatePath.Size = new System.Drawing.Size(736, 41);
			this.txtTemplatePath.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121, 33);
			this.label1.TabIndex = 0;
			this.label1.Text = "Template";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtPortNumber);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.txtPLC_IPAddress);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(12, 225);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(1026, 151);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Communication";
			// 
			// txtPortNumber
			// 
			this.txtPortNumber.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPortNumber.Location = new System.Drawing.Point(231, 98);
			this.txtPortNumber.Name = "txtPortNumber";
			this.txtPortNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPortNumber.Size = new System.Drawing.Size(229, 41);
			this.txtPortNumber.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 103);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(155, 33);
			this.label3.TabIndex = 2;
			this.label3.Text = "PortNumber";
			// 
			// txtPLC_IPAddress
			// 
			this.txtPLC_IPAddress.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPLC_IPAddress.Location = new System.Drawing.Point(231, 41);
			this.txtPLC_IPAddress.Name = "txtPLC_IPAddress";
			this.txtPLC_IPAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPLC_IPAddress.Size = new System.Drawing.Size(229, 41);
			this.txtPLC_IPAddress.TabIndex = 1;
			this.txtPLC_IPAddress.Click += new System.EventHandler(this.txtPLC_IPAddress_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(20, 45);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(196, 33);
			this.label4.TabIndex = 0;
			this.label4.Text = "PLC IP Address";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cbBoxProducts);
			this.groupBox3.Controls.Add(this.groupBox4);
			this.groupBox3.Controls.Add(this.label16);
			this.groupBox3.Controls.Add(this.txtMaxProductDisplay);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(13, 399);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(1025, 105);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Hiển thị";
			// 
			// cbBoxProducts
			// 
			this.cbBoxProducts.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbBoxProducts.FormattingEnabled = true;
			this.cbBoxProducts.Location = new System.Drawing.Point(179, 86);
			this.cbBoxProducts.Name = "cbBoxProducts";
			this.cbBoxProducts.Size = new System.Drawing.Size(166, 29);
			this.cbBoxProducts.TabIndex = 6;
			this.cbBoxProducts.Visible = false;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(16, 91);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(94, 17);
			this.label16.TabIndex = 5;
			this.label16.Text = "Loại sản phẩm";
			this.label16.Visible = false;
			// 
			// txtMaxProductDisplay
			// 
			this.txtMaxProductDisplay.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMaxProductDisplay.Location = new System.Drawing.Point(259, 41);
			this.txtMaxProductDisplay.Name = "txtMaxProductDisplay";
			this.txtMaxProductDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtMaxProductDisplay.Size = new System.Drawing.Size(166, 41);
			this.txtMaxProductDisplay.TabIndex = 4;
			this.txtMaxProductDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(20, 45);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(229, 33);
			this.label6.TabIndex = 0;
			this.label6.Text = "Số sản phẩm tối đa";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.txtPC_Reject_Number_Box);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.txtPC_Delay_Reject);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.txtPC_Reject_Time_Box_Conti);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.txtPC_Reject_Time);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.txtPC_Delay_Barcode);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox4.Location = new System.Drawing.Point(478, 51);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(541, 31);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Thông số cài đặt";
			this.groupBox4.Visible = false;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(344, 185);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(55, 19);
			this.label15.TabIndex = 17;
			this.label15.Text = "(1 -- 5)";
			// 
			// txtPC_Reject_Number_Box
			// 
			this.txtPC_Reject_Number_Box.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPC_Reject_Number_Box.Location = new System.Drawing.Point(235, 175);
			this.txtPC_Reject_Number_Box.Name = "txtPC_Reject_Number_Box";
			this.txtPC_Reject_Number_Box.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPC_Reject_Number_Box.Size = new System.Drawing.Size(101, 29);
			this.txtPC_Reject_Number_Box.TabIndex = 16;
			this.txtPC_Reject_Number_Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtPC_Reject_Number_Box.Click += new System.EventHandler(this.txtPC_Reject_Number_Box_Click);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(20, 185);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(207, 19);
			this.label14.TabIndex = 15;
			this.label14.Text = "Số thùng nằm giữa khoảng reject";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(344, 147);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(111, 19);
			this.label13.TabIndex = 14;
			this.label13.Text = "(1 -- 50) x 10ms";
			// 
			// txtPC_Delay_Reject
			// 
			this.txtPC_Delay_Reject.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPC_Delay_Reject.Location = new System.Drawing.Point(235, 137);
			this.txtPC_Delay_Reject.Name = "txtPC_Delay_Reject";
			this.txtPC_Delay_Reject.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPC_Delay_Reject.Size = new System.Drawing.Size(101, 29);
			this.txtPC_Delay_Reject.TabIndex = 13;
			this.txtPC_Delay_Reject.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtPC_Delay_Reject.Click += new System.EventHandler(this.txtPC_Delay_Reject_Click);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(20, 147);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(124, 19);
			this.label12.TabIndex = 12;
			this.label12.Text = "Delay trigger reject";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(344, 108);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(119, 19);
			this.label11.TabIndex = 11;
			this.label11.Text = "(1 -- 500) x 10ms";
			// 
			// txtPC_Reject_Time_Box_Conti
			// 
			this.txtPC_Reject_Time_Box_Conti.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPC_Reject_Time_Box_Conti.Location = new System.Drawing.Point(235, 98);
			this.txtPC_Reject_Time_Box_Conti.Name = "txtPC_Reject_Time_Box_Conti";
			this.txtPC_Reject_Time_Box_Conti.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPC_Reject_Time_Box_Conti.Size = new System.Drawing.Size(101, 29);
			this.txtPC_Reject_Time_Box_Conti.TabIndex = 10;
			this.txtPC_Reject_Time_Box_Conti.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtPC_Reject_Time_Box_Conti.Click += new System.EventHandler(this.txtPC_Reject_Time_Box_Conti_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(20, 108);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(182, 19);
			this.label10.TabIndex = 9;
			this.label10.Text = "Thời gian thùng tiếp theo vào";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(344, 71);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(111, 19);
			this.label9.TabIndex = 8;
			this.label9.Text = "(1 -- 50) x 10ms";
			// 
			// txtPC_Reject_Time
			// 
			this.txtPC_Reject_Time.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPC_Reject_Time.Location = new System.Drawing.Point(235, 60);
			this.txtPC_Reject_Time.Name = "txtPC_Reject_Time";
			this.txtPC_Reject_Time.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPC_Reject_Time.Size = new System.Drawing.Size(101, 29);
			this.txtPC_Reject_Time.TabIndex = 7;
			this.txtPC_Reject_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtPC_Reject_Time.Click += new System.EventHandler(this.txtPC_Reject_Time_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(20, 71);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(81, 19);
			this.label8.TabIndex = 6;
			this.label8.Text = "Time Reject";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(344, 33);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111, 19);
			this.label7.TabIndex = 5;
			this.label7.Text = "(1 -- 50) x 10ms";
			// 
			// txtPC_Delay_Barcode
			// 
			this.txtPC_Delay_Barcode.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPC_Delay_Barcode.Location = new System.Drawing.Point(235, 25);
			this.txtPC_Delay_Barcode.Name = "txtPC_Delay_Barcode";
			this.txtPC_Delay_Barcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPC_Delay_Barcode.Size = new System.Drawing.Size(101, 29);
			this.txtPC_Delay_Barcode.TabIndex = 4;
			this.txtPC_Delay_Barcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtPC_Delay_Barcode.Click += new System.EventHandler(this.txtPC_Delay_Barcode_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(20, 33);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(146, 19);
			this.label5.TabIndex = 0;
			this.label5.Text = "Delay Trigger Barcode";
			// 
			// btPassword
			// 
			this.btPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btPassword.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btPassword.FlatAppearance.BorderSize = 5;
			this.btPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btPassword.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btPassword.ForeColor = System.Drawing.Color.White;
			this.btPassword.Image = global::CheckWeigherUBN.Properties.Resources.key_25px;
			this.btPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btPassword.Location = new System.Drawing.Point(308, 544);
			this.btPassword.Name = "btPassword";
			this.btPassword.Size = new System.Drawing.Size(234, 56);
			this.btPassword.TabIndex = 14;
			this.btPassword.Text = "     Password";
			this.btPassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btPassword.UseVisualStyleBackColor = false;
			this.btPassword.Click += new System.EventHandler(this.btPassword_Click);
			// 
			// btMatrixPermission
			// 
			this.btMatrixPermission.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btMatrixPermission.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btMatrixPermission.FlatAppearance.BorderSize = 5;
			this.btMatrixPermission.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btMatrixPermission.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btMatrixPermission.ForeColor = System.Drawing.Color.White;
			this.btMatrixPermission.Image = global::CheckWeigherUBN.Properties.Resources.soccer_yellow_card_25px;
			this.btMatrixPermission.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btMatrixPermission.Location = new System.Drawing.Point(13, 544);
			this.btMatrixPermission.Name = "btMatrixPermission";
			this.btMatrixPermission.Size = new System.Drawing.Size(288, 56);
			this.btMatrixPermission.TabIndex = 12;
			this.btMatrixPermission.Text = "Ma trận phân quyền";
			this.btMatrixPermission.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btMatrixPermission.UseVisualStyleBackColor = false;
			this.btMatrixPermission.Click += new System.EventHandler(this.btMatrixPermission_Click);
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
			this.btExit.Location = new System.Drawing.Point(797, 543);
			this.btExit.Name = "btExit";
			this.btExit.Size = new System.Drawing.Size(240, 56);
			this.btExit.TabIndex = 10;
			this.btExit.Text = "       Thoát";
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
			this.btSave.Location = new System.Drawing.Point(549, 544);
			this.btSave.Name = "btSave";
			this.btSave.Size = new System.Drawing.Size(240, 56);
			this.btSave.TabIndex = 9;
			this.btSave.Text = "   Lưu && Thoát";
			this.btSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btSave.UseVisualStyleBackColor = false;
			this.btSave.Click += new System.EventHandler(this.btSave_Click);
			// 
			// frmSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1056, 617);
			this.ControlBox = false;
			this.Controls.Add(this.btPassword);
			this.Controls.Add(this.btMatrixPermission);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btSave);
			this.Controls.Add(this.btExit);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.frmSettings_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtDatabasePath;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTemplatePath;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtPLC_IPAddress;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btSave;
    private System.Windows.Forms.Button btExit;
    private System.Windows.Forms.TextBox txtPortNumber;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.TextBox txtMaxProductDisplay;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button btMatrixPermission;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.TextBox txtPC_Delay_Barcode;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TextBox txtPC_Reject_Number_Box;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox txtPC_Delay_Reject;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtPC_Reject_Time_Box_Conti;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtPC_Reject_Time;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ComboBox cbBoxProducts;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Button btPassword;
    private System.Windows.Forms.TextBox txtReportPath;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Button btFolderBrowser;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
  }
}