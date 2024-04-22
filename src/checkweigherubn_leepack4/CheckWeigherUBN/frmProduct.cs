using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckWeigherUBN.Dialogs;
using GlacialComponents.Controls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace CheckWeigherUBN
{
  public partial class frmProduct : Form
  {
    public delegate void UpdateProductDone(object sender, ProductManagementType product);
    public event UpdateProductDone OnUpdateProductDone;

    public delegate void SendImportDataFromExcelDone(object sender);
    public event SendImportDataFromExcelDone OnSendImportDataFromExcelDone;
    //
    private const int COL_ID = 0;
    private const int COL_SKU = COL_ID + 1;
    private const int COL_DESCRIPTION = COL_SKU + 1;
    private const int COL_FGs = COL_DESCRIPTION + 1;
    private const int COL_TARGET = COL_FGs + 1;
    private const int COL_MIN_1T = COL_TARGET + 1;
    private const int COL_MAX_1T = COL_MIN_1T + 1;
    private const int COL_MIN_2T = COL_MAX_1T + 1;
    private const int COL_MAX_2T = COL_MIN_2T + 1;
    private const int COL_PM = COL_MAX_2T + 1;
    //
    private ConfigurationTypes _configuration = null;
    //
    private bool _IsWaitingDeactiveControl = false;
    private bool bIsAddFinish = false;
    private GLItem selectedItem = null;

    private bool _isImportExcelDone = false;
    public frmProduct(ConfigurationTypes configuration)
    {
      InitializeComponent();
      //
      _configuration = configuration;
      //
      this.btEdit.Enabled = false;
      //
      this.glacialList1.ItemChangedEvent += new GlacialComponents.Controls.ChangedEventHandler(this.glacialList1_ItemChangedEvent);
    }

    private string old_Description = "";
    private string old_Barcode = "";
    private string old_Target = "";
    private string old_Diff = "";
    private string old_Min_1T = "";
    private string old_Max_1T = "";
    private string old_Min_2T = "";
    private string old_Max_2T = "";
    private string old_PM = "";
    //
    private void glacialList1_ItemChangedEvent(object source, ChangedEventArgs e)
    {
      if (e.ChangedType == ChangedTypes.ActiveEmbeddedControlChanged)
      {
        _IsWaitingDeactiveControl = true;
        //
        if (selectedItem != null)
        {
          old_Description = selectedItem.SubItems[COL_DESCRIPTION].Text;
          old_Barcode = selectedItem.SubItems[COL_FGs].Text;
          old_Target = selectedItem.SubItems[COL_TARGET].Text;
          old_Min_1T = selectedItem.SubItems[COL_MIN_1T].Text;
          old_Max_1T = selectedItem.SubItems[COL_MAX_1T].Text;
          old_Min_2T = selectedItem.SubItems[COL_MIN_2T].Text;
          old_Max_2T = selectedItem.SubItems[COL_MAX_2T].Text;
          old_PM = selectedItem.SubItems[COL_PM].Text;
        }
        //
        Utils utils = new Utils();
        utils.StartKeyboardOSK();
      }
      else if ((e.ChangedType == ChangedTypes.DeactiveEmbeddedControlChanged) && (_IsWaitingDeactiveControl == true))
      {
        if ((selectedItem != null) && (e.Item != null))
        {
          if (selectedItem.Text == e.Item.Text)
          {
            string new_Description = e.Item.SubItems[COL_DESCRIPTION].Text;
            string new_Barcode = e.Item.SubItems[COL_FGs].Text;
            string new_Target = e.Item.SubItems[COL_TARGET].Text;
            string new_Min_1T = e.Item.SubItems[COL_MIN_1T].Text;
            string new_Max_1T = e.Item.SubItems[COL_MAX_1T].Text;
            string new_Min_2T = e.Item.SubItems[COL_MIN_2T].Text;
            string new_Max_2T = e.Item.SubItems[COL_MAX_2T].Text;
            string new_PM = e.Item.SubItems[COL_PM].Text;
            //
            if (new_Description != old_Description)
            {
              selectedItem.SubItems[COL_DESCRIPTION].ForeColor = Color.Red;
            }
            if (new_Barcode != old_Barcode)
            {
              selectedItem.SubItems[COL_FGs].ForeColor = Color.Red;
            }
            if (new_Target != old_Target)
            {
              selectedItem.SubItems[COL_TARGET].ForeColor = Color.Red;
            }
            
            if (new_Min_1T != old_Min_1T)
            {
              selectedItem.SubItems[COL_MIN_1T].ForeColor = Color.Red;
            }
            if (new_Max_1T != old_Max_1T)
            {
              selectedItem.SubItems[COL_MAX_1T].ForeColor = Color.Red;
            }
            if (new_Min_2T != old_Min_2T)
            {
              selectedItem.SubItems[COL_MIN_2T].ForeColor = Color.Red;
            }
            if (new_Max_2T != old_Max_2T)
            {
              selectedItem.SubItems[COL_MAX_2T].ForeColor = Color.Red;
            }
            if (new_PM != old_PM)
            {
              selectedItem.SubItems[COL_PM].ForeColor = Color.Red;
            }
          }
        }
        _IsWaitingDeactiveControl = false;
      }
      else
      {
        if (e.Item != null)
        {
          if ((this.glacialList1.Items.Count > 0))
          {
            if (bIsAddFinish == true)
            {
              if (selectedItem != e.Item)
              {
                selectedItem = e.Item;
                //
                this.btEdit.Enabled = true;
              }
            }
            else
            {
              selectedItem = null;
            }
          }/*if ((this.glacialList1.Items.Count > 0))*/
        }
      }
    }

    private void btAddFirstTime_Click(object sender, EventArgs e)
    {
      //ProductManagementType product1 = new ProductManagementType("Sunlight Floorcare", "", "1000", "15", "985", "1015");
      //ProductManagementType product2 = new ProductManagementType("Sunlight Floorcare", "", "3800", "57", "3743", "3857");
      //ProductManagementType product3 = new ProductManagementType("Sunlight Dishwash", "", "1500", "22", "1477", "1522");
      //ProductManagementType product4 = new ProductManagementType("Sunlight Dishwash", "", "3800", "57", "3743", "3857");
      //ProductManagementType product5 = new ProductManagementType("OMO", "", "2400", "36", "2364", "2436");
      //ProductManagementType product6 = new ProductManagementType("OMO", "", "2700", "40", "2659", "2740");
      //ProductManagementType product7 = new ProductManagementType("OMO", "", "3800", "57", "3743", "3857");
      //ProductManagementType product8 = new ProductManagementType("OMO", "", "4200", "63", "4137", "4263");
      //ProductManagementType product9 = new ProductManagementType("Comfort ", "", "1800", "27", "1773", "1827");
      //ProductManagementType product10 = new ProductManagementType("Comfort ", "", "3800", "57", "3743", "3857");
      ////
      //ProductManagementDB sqlProductManagementDB = new ProductManagementDB();
      //object ret_product1 = sqlProductManagementDB.Save(product1);
      //object ret_product2 = sqlProductManagementDB.Save(product2);
      //object ret_product3 = sqlProductManagementDB.Save(product3);
      //object ret_product4 = sqlProductManagementDB.Save(product4);
      //object ret_product5 = sqlProductManagementDB.Save(product5);
      //object ret_product6 = sqlProductManagementDB.Save(product6);
      //object ret_product7 = sqlProductManagementDB.Save(product7);
      //object ret_product8 = sqlProductManagementDB.Save(product8);
      //object ret_product9 = sqlProductManagementDB.Save(product9);
      //object ret_product10 = sqlProductManagementDB.Save(product10);

      /****** Load ProductManagement ********/
      ReloadAllProducts();
    }

    /// <summary>
    /// reload all product from database
    /// </summary>
    private void ReloadAllProducts()
    {
      ProductManagementDB sql = new ProductManagementDB();
      object objRet = sql.LoadAll();
      if (objRet is List<ProductManagementType>)
      {
        List<ProductManagementType> list_ProductManagement = (List<ProductManagementType>)(objRet);
        if (list_ProductManagement.Count > 0)
        {
          _configuration.list_ProductManagement = list_ProductManagement;
        }
      }
    }

    private void frmProduct_Load(object sender, EventArgs e)
    {
      DisplayProductManagementToListview();
    }
		private void DisplayProductManagementToListview()
    {
			this.glacialList1.Items.Clear();

      bIsAddFinish = false;
      for (int i = 0; i < _configuration.list_ProductManagement.Count; i++)
      {
        ProductManagementType product = _configuration.list_ProductManagement[i];
        GLItem item = new GLItem();
        item.SubItems[COL_ID].Text = product.id.ToString();
        item.SubItems[COL_SKU].Text = product.SKU;
        item.SubItems[COL_DESCRIPTION].Text = product.Description;
        item.SubItems[COL_FGs].Text = product.FGs;
        item.SubItems[COL_TARGET].Text = product.Target.ToString();
        item.SubItems[COL_MIN_1T].Text = product.LowerLimit_1T.ToString();
        item.SubItems[COL_MAX_1T].Text = product.UpperLimit_1T.ToString();
        item.SubItems[COL_MIN_2T].Text = product.LowerLimit_2T.ToString();
        item.SubItems[COL_MAX_2T].Text = product.UpperLimit_2T.ToString();
        item.SubItems[COL_PM].Text = product.gPackageMaterial.ToString();
        //
        this.glacialList1.Items.Add(item);
      }/*for (int i = 0; i < _configuration.list_ProductManagement.Count; i++)*/
      bIsAddFinish = true;
      this.glacialList1.Refresh();
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      Utils utils = new Utils();
      utils.CloseKeyboardOSK();
      //
      if (_isImportExcelDone == true)
      {
        if (OnSendImportDataFromExcelDone != null)
        {
          OnSendImportDataFromExcelDone(this);
        }
      }
      this.Close();
    }

    private void btSave_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < _configuration.list_ProductManagement.Count; i++)
      {
        ProductManagementType product = _configuration.list_ProductManagement[i];
        GLItem item = new GLItem();
        bool IsNeedSave = item.SubItems[COL_DESCRIPTION].ForeColor == Color.Red;
        IsNeedSave |= item.SubItems[COL_FGs].ForeColor == Color.Red;
        //
        this.glacialList1.Items.Add(item);
      }

      if (_isImportExcelDone == true)
      {
        if (OnSendImportDataFromExcelDone != null)
        {
          OnSendImportDataFromExcelDone(this);
        }
      }

      this.Close();
    }

    private void btEdit_Click(object sender, EventArgs e)
    {
      if (CheckPemission(ePemission.PRODUCT_Cai_dat_du_lieu_can_chinh_sua_thong_tin_da_load))
      {
        if (selectedItem != null)
        {
          /* find correct Product */
          List<ProductManagementType> list_ProductManagement = _configuration.list_ProductManagement.FindAll(x => x.id.ToString() == selectedItem.SubItems[COL_ID].Text);
          if (list_ProductManagement.Count > 0)
          {
            ProductManagementType productManagement = list_ProductManagement[0];
            frmProductEdit frmProduct = new frmProductEdit(_configuration, productManagement);
            frmProduct.OnUpdateProductDone += FrmProduct_OnUpdateProductDone;
            frmProduct.FormClosed += FrmProduct_FormClosed;
            frmProduct.ShowDialog();
            frmProduct.BringToFront();
          }
        }
      }
    }

    private void btAddNew_Click(object sender, EventArgs e)
    {
      frmProductEdit frmProduct = new frmProductEdit(_configuration, null);
      frmProduct.OnUpdateProductDone += FrmProduct_OnUpdateProductDone;
      frmProduct.FormClosed += FrmProduct_FormClosed;
      frmProduct.ShowDialog();
      frmProduct.BringToFront();
    }

    //private void GetProduct
    private void FrmProduct_OnUpdateProductDone(object sender, ProductManagementType product, frmProductEdit.eProductEditType eProductEdit)
    {
      if (product != null)
      {
        ProductManagementDB sqlProductManagementDB = new ProductManagementDB();
        object ret = sqlProductManagementDB.Save(product);
        
        //
        ReloadAllProducts();
        //
        DisplayProductManagementToListview();
        //Send request update to PLC
        if (OnUpdateProductDone != null)
        {
          if (ret != null)
          {
            if (ret is ProductManagementType)
            {
              ProductManagementType productRet = (ProductManagementType)(ret);
              OnUpdateProductDone(this, productRet);
            }
          }

          
        }
      }
      
    }

    private void FrmProduct_FormClosed(object sender, FormClosedEventArgs e)
    {
      Utils utils = new Utils();
      utils.CloseKeyboardOSK();
    }

    private void btImportFromExcel_Click(object sender, EventArgs e)
    {
      if (CheckPemission(ePemission.PRODUCT_Cai_dat_du_lieu_can_import_database_tu_excel))
      {
        _isImportExcelDone = false;
        //
        frmImportProductByExcel frmImportProductByExcel = new frmImportProductByExcel(_configuration);
        frmImportProductByExcel.OnSendImportDataFromExcelDone += FrmImportProductByExcel_OnSendImportDataFromExcelDone;
        frmImportProductByExcel.FormClosed += FrmImportProductByExcel_FormClosed;        
        frmImportProductByExcel.ShowDialog();
        frmImportProductByExcel.BringToFront();
      }
    }

    private void FrmImportProductByExcel_OnSendImportDataFromExcelDone(object sender)
    {
      _isImportExcelDone = true;
    }

    private void FrmImportProductByExcel_FormClosed(object sender, FormClosedEventArgs e)
    {
      DisplayProductManagementToListview();
    }
    private bool CheckPemission(ePemission ePemission)
    {
      bool ret = Utils.CheckPemission(_configuration, ePemission);
      if (ret == false)
      {
        ShowMessageLogin();
      }
      return ret;
    }
    private void ShowMessageLogin()
    {
      //MessageBox.Show("Bạn không được phép truy cập trang này", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

      FrmWarning frmWarning = new FrmWarning("Bạn không được phép truy cập trang này !");

		}

  }
}
