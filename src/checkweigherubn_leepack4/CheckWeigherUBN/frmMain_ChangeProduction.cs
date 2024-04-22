//#define ENABLE_DEMO_MODE
using CheckWeigherUBN.ASingleInstanceApp;
using CheckWeigherUBN.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
  public partial class frmMain : Form
  {
    private void OperationInformation1_OnSendChoseProduct(object sender)
    {
      Changover.frmGroupProduction frmGroupProduction = new Changover.frmGroupProduction(_configuration);
      frmGroupProduction.OnSendRequestChangeSku += FrmGroupProduction_OnSendRequestChangeSku;
      frmGroupProduction.ShowDialog();
    }

    private void FrmGroupProduction_OnSendRequestChangeSku(object sender, ProductManagementType product)
    {
      this.operationInformation1.SetNewProductionAndSensActiveChangeSku(product);
    }
  }
}
