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

  public partial class frmLoadSKU : Form
  {
    public delegate void SendChooseSKU(object sender, string SKU);
    public event SendChooseSKU OnSendChooseSKU;

    private ConfigurationTypes _configuration = null;

    private bool IsAddItem_SKU_OK = false;
    public frmLoadSKU(ConfigurationTypes configuration)
    {
      InitializeComponent();
      _configuration = configuration;
      AddProductItem();
    }

    public void AddProductItem()
    {
      IsAddItem_SKU_OK = false;
      //
      this.cbSKU.Items.Clear();
      for (int i = 0; i < _configuration.list_ProductManagement.Count; i++)
      {
        ProductManagementType product = _configuration.list_ProductManagement[i];
        //
        if (this.cbSKU.Items.Contains(product.SKU) == false)
        {
          this.cbSKU.Items.Add(product.SKU);
        }
      }

      IsAddItem_SKU_OK = true;
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      if (this.cbSKU.Text != "")
      {
        if (OnSendChooseSKU != null)
        {
          OnSendChooseSKU(this, this.cbSKU.Text);
        }
      }
      this.Close();
    }
  }
}
