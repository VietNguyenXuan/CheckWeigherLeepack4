using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
  public partial class frmImportProductByExcel : Form
  {
    public delegate void SendImportDataFromExcelDone(object sender);
    public event SendImportDataFromExcelDone OnSendImportDataFromExcelDone;

    private string _file_path = "";
    private ConfigurationTypes _configuration;
    public frmImportProductByExcel(ConfigurationTypes configuration)
    {
      InitializeComponent();
      _configuration = configuration;
    }

    private void labelProgress_Click(object sender, EventArgs e)
    {

    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      //btExit.Enabled = false;
      ExcelProductImport excelProductImport = new ExcelProductImport(_configuration, _file_path);
      try
      {
        object ret = (excelProductImport.Execute());
        if (ret != null)
        {
          List<ProductManagementType> list_return_Product = (List<ProductManagementType>)(ret);
        }
      }
      catch
      {
      }
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {

    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (OnSendImportDataFromExcelDone != null)
      {
        OnSendImportDataFromExcelDone(this);
      }
      this.Close();

    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      openFileDialog1.ShowDialog();
    }

    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {
      _file_path = openFileDialog1.FileName;
      txtFilePath.Text = openFileDialog1.FileName;
      //
      if (backgroundWorker1.IsBusy == false)
      {
        backgroundWorker1.RunWorkerAsync();
      }
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      if (backgroundWorker1.IsBusy == false)
      {
        this.Close();
      }
    }
  }
}
