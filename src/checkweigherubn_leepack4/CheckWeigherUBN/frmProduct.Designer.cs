namespace CheckWeigherUBN
{
  partial class frmProduct
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
			GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn10 = new GlacialComponents.Controls.GLColumn();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProduct));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btImportFromExcel = new System.Windows.Forms.Button();
			this.btAddNew = new System.Windows.Forms.Button();
			this.btEdit = new System.Windows.Forms.Button();
			this.btAddFirstTime = new System.Windows.Forms.Button();
			this.btExit = new System.Windows.Forms.Button();
			this.btSave = new System.Windows.Forms.Button();
			this.glacialList1 = new GlacialComponents.Controls.GlacialList();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
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
			this.splitContainer1.Panel1.Controls.Add(this.btImportFromExcel);
			this.splitContainer1.Panel1.Controls.Add(this.btAddNew);
			this.splitContainer1.Panel1.Controls.Add(this.btEdit);
			this.splitContainer1.Panel1.Controls.Add(this.btAddFirstTime);
			this.splitContainer1.Panel1.Controls.Add(this.btExit);
			this.splitContainer1.Panel1.Controls.Add(this.btSave);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.glacialList1);
			this.splitContainer1.Size = new System.Drawing.Size(1280, 621);
			this.splitContainer1.SplitterDistance = 71;
			this.splitContainer1.TabIndex = 0;
			// 
			// btImportFromExcel
			// 
			this.btImportFromExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btImportFromExcel.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btImportFromExcel.FlatAppearance.BorderSize = 5;
			this.btImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btImportFromExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btImportFromExcel.ForeColor = System.Drawing.Color.White;
			this.btImportFromExcel.Image = global::CheckWeigherUBN.Properties.Resources.excel_24x24;
			this.btImportFromExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btImportFromExcel.Location = new System.Drawing.Point(529, 6);
			this.btImportFromExcel.Name = "btImportFromExcel";
			this.btImportFromExcel.Size = new System.Drawing.Size(223, 55);
			this.btImportFromExcel.TabIndex = 16;
			this.btImportFromExcel.Text = " Import từ excel";
			this.btImportFromExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btImportFromExcel.UseVisualStyleBackColor = false;
			this.btImportFromExcel.Click += new System.EventHandler(this.btImportFromExcel_Click);
			// 
			// btAddNew
			// 
			this.btAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btAddNew.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btAddNew.ForeColor = System.Drawing.Color.White;
			this.btAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btAddNew.Location = new System.Drawing.Point(99, 6);
			this.btAddNew.Name = "btAddNew";
			this.btAddNew.Size = new System.Drawing.Size(69, 46);
			this.btAddNew.TabIndex = 15;
			this.btAddNew.Text = "Thêm mới";
			this.btAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btAddNew.UseVisualStyleBackColor = false;
			this.btAddNew.Visible = false;
			this.btAddNew.Click += new System.EventHandler(this.btAddNew_Click);
			// 
			// btEdit
			// 
			this.btEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btEdit.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btEdit.FlatAppearance.BorderSize = 5;
			this.btEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btEdit.ForeColor = System.Drawing.Color.White;
			this.btEdit.Image = global::CheckWeigherUBN.Properties.Resources.edit_property_25px;
			this.btEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btEdit.Location = new System.Drawing.Point(756, 6);
			this.btEdit.Name = "btEdit";
			this.btEdit.Size = new System.Drawing.Size(187, 55);
			this.btEdit.TabIndex = 14;
			this.btEdit.Text = "   Chỉnh sửa";
			this.btEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btEdit.UseVisualStyleBackColor = false;
			this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
			// 
			// btAddFirstTime
			// 
			this.btAddFirstTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btAddFirstTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btAddFirstTime.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btAddFirstTime.ForeColor = System.Drawing.Color.White;
			this.btAddFirstTime.Image = global::CheckWeigherUBN.Properties.Resources.Save_25px;
			this.btAddFirstTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btAddFirstTime.Location = new System.Drawing.Point(3, 6);
			this.btAddFirstTime.Name = "btAddFirstTime";
			this.btAddFirstTime.Size = new System.Drawing.Size(90, 46);
			this.btAddFirstTime.TabIndex = 13;
			this.btAddFirstTime.Text = "Create first time";
			this.btAddFirstTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btAddFirstTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btAddFirstTime.UseVisualStyleBackColor = false;
			this.btAddFirstTime.Visible = false;
			this.btAddFirstTime.Click += new System.EventHandler(this.btAddFirstTime_Click);
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btExit.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btExit.FlatAppearance.BorderSize = 5;
			this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btExit.ForeColor = System.Drawing.Color.White;
			this.btExit.Image = global::CheckWeigherUBN.Properties.Resources.icons8_Shutdown_30px;
			this.btExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btExit.Location = new System.Drawing.Point(1102, 6);
			this.btExit.Name = "btExit";
			this.btExit.Size = new System.Drawing.Size(169, 55);
			this.btExit.TabIndex = 12;
			this.btExit.Text = "     Thoát";
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
			this.btSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btSave.ForeColor = System.Drawing.Color.White;
			this.btSave.Image = global::CheckWeigherUBN.Properties.Resources.Save_25px;
			this.btSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btSave.Location = new System.Drawing.Point(948, 6);
			this.btSave.Name = "btSave";
			this.btSave.Size = new System.Drawing.Size(149, 55);
			this.btSave.TabIndex = 11;
			this.btSave.Text = "    Lưu";
			this.btSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btSave.UseVisualStyleBackColor = false;
			this.btSave.Click += new System.EventHandler(this.btSave_Click);
			// 
			// glacialList1
			// 
			this.glacialList1.ActivatedEmbeddedData = null;
			this.glacialList1.AllowColumnResize = false;
			this.glacialList1.AllowMultiselect = false;
			this.glacialList1.AlternateBackground = System.Drawing.Color.DarkGreen;
			this.glacialList1.AlternatingColors = false;
			this.glacialList1.AutoHeight = false;
			this.glacialList1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.glacialList1.BackgroundStretchToFit = true;
			glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn1.CheckBoxes = false;
			glColumn1.CheckBoxesReadOnly = false;
			glColumn1.DisplayProgressBar = false;
			glColumn1.EnableContextMenu = true;
			glColumn1.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn1.ImageIndex = -1;
			glColumn1.Name = "ID";
			glColumn1.NumericSort = false;
			glColumn1.Text = "ID";
			glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn1.Width = 50;
			glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn2.CheckBoxes = false;
			glColumn2.CheckBoxesReadOnly = false;
			glColumn2.DisplayProgressBar = false;
			glColumn2.EnableContextMenu = true;
			glColumn2.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn2.ImageIndex = -1;
			glColumn2.Name = "SKU";
			glColumn2.NumericSort = false;
			glColumn2.Text = "SKU";
			glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn2.Width = 130;
			glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn3.CheckBoxes = false;
			glColumn3.CheckBoxesReadOnly = false;
			glColumn3.DisplayProgressBar = false;
			glColumn3.EnableContextMenu = true;
			glColumn3.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn3.ImageIndex = -1;
			glColumn3.Name = "Description";
			glColumn3.NumericSort = false;
			glColumn3.Text = "Description";
			glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn3.Width = 430;
			glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn4.CheckBoxes = false;
			glColumn4.CheckBoxesReadOnly = false;
			glColumn4.DisplayProgressBar = false;
			glColumn4.EnableContextMenu = true;
			glColumn4.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn4.ImageIndex = -1;
			glColumn4.Name = "SKUx";
			glColumn4.NumericSort = false;
			glColumn4.Text = "FGs";
			glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn4.Width = 100;
			glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn5.CheckBoxes = false;
			glColumn5.CheckBoxesReadOnly = false;
			glColumn5.DisplayProgressBar = false;
			glColumn5.EnableContextMenu = true;
			glColumn5.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn5.ImageIndex = -1;
			glColumn5.Name = "Target";
			glColumn5.NumericSort = false;
			glColumn5.Text = "Target(g)";
			glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn5.Width = 100;
			glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn6.CheckBoxes = false;
			glColumn6.CheckBoxesReadOnly = false;
			glColumn6.DisplayProgressBar = false;
			glColumn6.EnableContextMenu = true;
			glColumn6.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn6.ImageIndex = -1;
			glColumn6.Name = "Min_1T";
			glColumn6.NumericSort = false;
			glColumn6.Text = "Min 1T (g)";
			glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn6.Width = 100;
			glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn7.CheckBoxes = false;
			glColumn7.CheckBoxesReadOnly = false;
			glColumn7.DisplayProgressBar = false;
			glColumn7.EnableContextMenu = true;
			glColumn7.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn7.ImageIndex = -1;
			glColumn7.Name = "Max_1T";
			glColumn7.NumericSort = false;
			glColumn7.Text = "Max 1T(g)";
			glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn7.Width = 100;
			glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn8.CheckBoxes = false;
			glColumn8.CheckBoxesReadOnly = false;
			glColumn8.DisplayProgressBar = false;
			glColumn8.EnableContextMenu = true;
			glColumn8.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn8.ImageIndex = -1;
			glColumn8.Name = "Min_2T";
			glColumn8.NumericSort = false;
			glColumn8.Text = "Min 2T(g)";
			glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn8.Width = 90;
			glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn9.CheckBoxes = false;
			glColumn9.CheckBoxesReadOnly = false;
			glColumn9.DisplayProgressBar = false;
			glColumn9.EnableContextMenu = true;
			glColumn9.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn9.ImageIndex = -1;
			glColumn9.Name = "Max_2T";
			glColumn9.NumericSort = false;
			glColumn9.Text = "Max 2T(g)";
			glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn9.Width = 90;
			glColumn10.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn10.CheckBoxes = false;
			glColumn10.CheckBoxesReadOnly = false;
			glColumn10.DisplayProgressBar = false;
			glColumn10.EnableContextMenu = true;
			glColumn10.GLActivatedEmbeddePreviousTypes = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn10.ImageIndex = -1;
			glColumn10.Name = "PM";
			glColumn10.NumericSort = false;
			glColumn10.Text = "PM(g)";
			glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn10.Width = 100;
			this.glacialList1.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7,
            glColumn8,
            glColumn9,
            glColumn10});
			this.glacialList1.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
			this.glacialList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glacialList1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.glacialList1.FullRowSelect = true;
			this.glacialList1.GridColor = System.Drawing.Color.LightGray;
			this.glacialList1.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
			this.glacialList1.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
			this.glacialList1.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
			this.glacialList1.HeaderHeight = 22;
			this.glacialList1.HeaderVisible = true;
			this.glacialList1.HeaderWordWrap = false;
			this.glacialList1.HighLight_SelectedSubItem = false;
			this.glacialList1.HighLightSelectedSubItemColor = System.Drawing.Color.Red;
			this.glacialList1.HotColumnTracking = false;
			this.glacialList1.HotItemTracking = false;
			this.glacialList1.HotTrackingColor = System.Drawing.Color.LightGray;
			this.glacialList1.HoverEvents = false;
			this.glacialList1.HoverTime = 1;
			this.glacialList1.ImageList = null;
			this.glacialList1.ItemHeight = 30;
			this.glacialList1.ItemWordWrap = false;
			this.glacialList1.Location = new System.Drawing.Point(0, 0);
			this.glacialList1.Name = "glacialList1";
			this.glacialList1.Selectable = true;
			this.glacialList1.SelectedTextColor = System.Drawing.Color.White;
			this.glacialList1.SelectionColor = System.Drawing.Color.DarkBlue;
			this.glacialList1.ShowBorder = true;
			this.glacialList1.ShowFocusRect = false;
			this.glacialList1.Size = new System.Drawing.Size(1280, 546);
			this.glacialList1.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
			this.glacialList1.SuperFlatHeaderColor = System.Drawing.Color.White;
			this.glacialList1.TabIndex = 0;
			this.glacialList1.Text = "glacialList1";
			// 
			// frmProduct
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1280, 621);
			this.ControlBox = false;
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmProduct";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Product Management";
			this.Load += new System.EventHandler(this.frmProduct_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Button btExit;
    private System.Windows.Forms.Button btSave;
    private GlacialComponents.Controls.GlacialList glacialList1;
    private System.Windows.Forms.Button btAddFirstTime;
    private System.Windows.Forms.Button btEdit;
    private System.Windows.Forms.Button btAddNew;
    private System.Windows.Forms.Button btImportFromExcel;
  }
}