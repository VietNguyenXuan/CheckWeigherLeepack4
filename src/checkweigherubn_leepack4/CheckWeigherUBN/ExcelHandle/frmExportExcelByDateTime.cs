using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN.ExcelHandle
{
  public partial class frmExportExcelByDateTime : Form
  {
    private ConfigurationTypes _configuration = new ConfigurationTypes();
    public frmExportExcelByDateTime()
    {
      InitializeComponent();
      //
      this.excelReportUC1.OnBtExitClicked += ExcelReportUC1_OnBtExitClicked;
    }

    private void ExcelReportUC1_OnBtExitClicked(object sender)
    {
      this.Close();
    }


    public void UpdateConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
      this.excelReportUC1.UpdateConfiguration(configuration);
    }


    
  }
}
