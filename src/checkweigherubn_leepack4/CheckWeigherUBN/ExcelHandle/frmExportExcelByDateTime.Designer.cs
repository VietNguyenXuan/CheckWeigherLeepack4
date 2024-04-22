namespace CheckWeigherUBN.ExcelHandle
{
  partial class frmExportExcelByDateTime
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportExcelByDateTime));
      this.excelReportUC1 = new CheckWeigherUBN.ExcelHandle.ExcelReportUC();
      this.SuspendLayout();
      // 
      // excelReportUC1
      // 
      this.excelReportUC1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.excelReportUC1.Location = new System.Drawing.Point(0, 0);
      this.excelReportUC1.Name = "excelReportUC1";
      this.excelReportUC1.Size = new System.Drawing.Size(943, 634);
      this.excelReportUC1.TabIndex = 0;
      // 
      // frmExportExcelByDateTime
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(943, 634);
      this.Controls.Add(this.excelReportUC1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmExportExcelByDateTime";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "ExportExcel";
      this.ResumeLayout(false);

    }

    #endregion

    private ExcelReportUC excelReportUC1;
  }
}