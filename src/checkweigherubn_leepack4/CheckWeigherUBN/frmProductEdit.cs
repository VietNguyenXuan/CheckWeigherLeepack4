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
  public partial class frmProductEdit : Form
  {
    public delegate void UpdateProductDone(object sender, ProductManagementType product, eProductEditType eProductEdit);
    public event UpdateProductDone OnUpdateProductDone;
    //
    private ProductManagementType currentProduct = null;
    private ConfigurationTypes _configuration = null;

    private bool IsSetupDone = false;
    private string _saveText = "";
    private bool IsKeepText = false;
    private NumericKeyboard.frmKeyBoard.eFromParent _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_TARGET;


    public frmProductEdit(ConfigurationTypes configuration, ProductManagementType product)
    {
      InitializeComponent();
      //
      currentProduct = product;
      _configuration = configuration;
    }

    public enum eProductEditType
    {
      ADD_NEW,
      EDIT,
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void frmProductEdit_Load(object sender, EventArgs e)
    {
      IsSetupDone = false;
      if (currentProduct != null)
      {
        this.txtSKU.Text = currentProduct.SKU;
        this.txtDescription.Text = currentProduct.Description;
        this.txtFGs.Text = currentProduct.FGs;
        this.txtTarget.Text = currentProduct.Target.ToString();
        this.txtPM.Text = currentProduct.gPackageMaterial.ToString();
        this.txtLowerLimit_1T.Text = currentProduct.LowerLimit_1T.ToString();
        this.txtUpperLimit_1T.Text = currentProduct.UpperLimit_1T.ToString();
        this.txtLowerLimit_2T.Text = currentProduct.LowerLimit_2T.ToString();
        this.txtUpperLimit_2T.Text = currentProduct.UpperLimit_2T.ToString();
      }
      IsSetupDone = true;
    }

    
    private void btSave_Click(object sender, EventArgs e)
    {
      //check if valid data
      if (this.txtSKU.Text == "")
      {
        MessageBox.Show("Vui lòng nhập vào ô 'SKU'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtDescription.Text == "")
      {
        MessageBox.Show("Vui lòng nhập vào ô 'Description'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtFGs.Text == "")
      {
        MessageBox.Show("Vui lòng nhập vào ô 'FGs'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtTarget.Text == "")
      {
        MessageBox.Show("Vui lòng nhập vào ô 'Target'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPM.Text == "")
      {
        MessageBox.Show("Vui lòng nhập vào ô 'PM'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtLowerLimit_1T.Text == "")
      {
        MessageBox.Show("Vui lòng nhập vào ô 'LowerLimit'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtUpperLimit_1T.Text == "")
      {
        MessageBox.Show("Vui lòng nhập vào ô 'UpperLimit'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtTarget.Text.IsValidNumber() == false)
      {
        MessageBox.Show("Giá trị nhập vào ô 'Target' không đúng. Vui lòng kiểm tra lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPM.Text.IsValidNumber() == false)
      {
        MessageBox.Show("Giá trị nhập vào ô 'PM' không đúng. Vui lòng kiểm tra lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtLowerLimit_1T.Text.IsValidNumber() == false)
      {
        MessageBox.Show("Giá trị nhập vào ô 'LowerLimit' không đúng. Vui lòng kiểm tra lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtUpperLimit_1T.Text.IsValidNumber() == false)
      {
        MessageBox.Show("Giá trị nhập vào ô 'UpperLimit' không đúng. Vui lòng kiểm tra lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else
      {
        if (currentProduct != null)
        {
          ProductManagementType product = new ProductManagementType();
          product.id = currentProduct.id;
          product.SKU = currentProduct.SKU;
          product.Description = this.txtDescription.Text;
          product.FGs = this.txtFGs.Text;
          product.Target = this.txtTarget.Text.Convert_to_Double();
          product.gPackageMaterial = this.txtPM.Text.Convert_to_Double();
          product.LowerLimit_1T = this.txtLowerLimit_1T.Text.Convert_to_Double();
          product.UpperLimit_1T = this.txtUpperLimit_1T.Text.Convert_to_Double();
          product.LowerLimit_2T = this.txtLowerLimit_2T.Text.Convert_to_Double();
          product.UpperLimit_2T = this.txtUpperLimit_2T.Text.Convert_to_Double();

          product.RowId = _configuration.list_ProductManagement.Find(x => (x.id == currentProduct.id)).RowId;
          product.ProductCheckType = _configuration.list_ProductManagement.Find(x => (x.id == currentProduct.id)).ProductCheckType;


          if (OnUpdateProductDone != null)
          {
            OnUpdateProductDone(this, product, eProductEditType.EDIT);
          }
        }
        else
        {
          //Add new
          ProductManagementType product = new ProductManagementType();
          product.SKU = this.txtSKU.Text;
          product.Description = this.txtDescription.Text;
          product.FGs = this.txtFGs.Text;
          product.Target = this.txtTarget.Text.Convert_to_Int();
          product.gPackageMaterial = this.txtPM.Text.Convert_to_Int();
          product.LowerLimit_1T = this.txtLowerLimit_1T.Text.Convert_to_Int();
          product.UpperLimit_1T = this.txtUpperLimit_1T.Text.Convert_to_Int();
          product.LowerLimit_2T = this.txtLowerLimit_2T.Text.Convert_to_Int();
          product.UpperLimit_2T = this.txtUpperLimit_2T.Text.Convert_to_Int();
          //
          if (OnUpdateProductDone != null)
          {
            OnUpdateProductDone(this, product, eProductEditType.ADD_NEW);
          }
        }
        
        this.Close();
      }
    }

    private void txtDescription_TextChanged(object sender, EventArgs e)
    {
      if (IsSetupDone == true)
      {
        this.txtDescription.ForeColor = Color.Red;
      }
    }

    private void txtBarcode_TextChanged(object sender, EventArgs e)
    {
      if (IsSetupDone == true)
      {
        this.txtFGs.ForeColor = Color.Red;
      }
    }

    private void txtTarget_TextChanged(object sender, EventArgs e)
    {
      if (IsSetupDone == true)
      {
        this.txtTarget.ForeColor = Color.Red;
      }
    }

    private void txtDiff_TextChanged(object sender, EventArgs e)
    {
      if (IsSetupDone == true)
      {
        this.txtPM.ForeColor = Color.Red;
      }
    }

    private void txtLowerLimit_TextChanged(object sender, EventArgs e)
    {
      if (IsSetupDone == true)
      {
        this.txtLowerLimit_1T.ForeColor = Color.Red;
      }
    }

    private void txtUpperLimit_TextChanged(object sender, EventArgs e)
    {
      if (IsSetupDone == true)
      {
        this.txtUpperLimit_1T.ForeColor = Color.Red;
      }
    }

    private void StartKeyboardOSK(int locationX, int locationY)
    {
      //Utils utils = new Utils();
      //utils.StartKeyboardOSK();

    }


    private void txtDescription_Click(object sender, EventArgs e)
    {
      // StartKeyboardOSK(this.txtDescription.Location.X, this.txtDescription.Location.Y);
     
    }

    private void txtFGs_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_FGs;
      DisplayKeyBoard(this.Location.X + this.txtTarget.Location.X + 70, this.Location.Y + this.txtTarget.Location.Y, _eFromParent);
    }

    private void txtLowerLimit_2T_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_LOW_LIMIT;
      DisplayKeyBoard(this.Location.X + this.txtTarget.Location.X + 70, this.Location.Y + this.txtTarget.Location.Y, _eFromParent);
    }

    private void txtUpperLimit_2T_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_HIGHT_LIMIT;
      DisplayKeyBoard(this.Location.X + this.txtTarget.Location.X + 70, this.Location.Y + this.txtTarget.Location.Y, _eFromParent);
    }


    private void DisplayKeyBoard(int locationX, int locationY, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      IsKeepText = false;
      if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_LOW_LIMIT)
      {
        _saveText = this.txtLowerLimit_2T.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_HIGHT_LIMIT)
      {
        _saveText = this.txtUpperLimit_2T.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_FGs)
      {
        _saveText = this.txtFGs.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_TARGET)
      {
        _saveText = this.txtTarget.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_PM)
      {
        _saveText = this.txtPM.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_1T)
      {
        _saveText = this.txtLowerLimit_1T.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_1T)
      {
        _saveText = this.txtUpperLimit_1T.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_2T)
      {
        _saveText = this.txtLowerLimit_2T.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_2T)
      {
        _saveText = this.txtUpperLimit_2T.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_SKU)
      {
        _saveText = this.txtSKU.Text;
      }
      //
      NumericKeyboard.frmKeyBoard frmKeyBoard = new NumericKeyboard.frmKeyBoard(locationX, locationY, _eFromParent);
      frmKeyBoard.OnSendKeyPad += FrmKeyBoard_OnSendKeyPad;
      frmKeyBoard.FormClosed += FrmKeyBoard_FormClosed;
      frmKeyBoard.ShowDialog();
    }

    private void FrmKeyBoard_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (IsKeepText == true)
      {
        /* do nothing */
      }
      else
      {
        if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_LOW_LIMIT)
        {
          txtLowerLimit_2T.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_HIGHT_LIMIT)
        {
          txtUpperLimit_2T.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_FGs)
        {
          txtFGs.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_TARGET)
        {
          txtTarget.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_PM)
        {
          this.txtPM.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_1T)
        {
          txtLowerLimit_1T.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_1T)
        {
          txtUpperLimit_1T.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_2T)
        {
          txtLowerLimit_2T.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_2T)
        {
          txtUpperLimit_2T.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_SKU)
        {
          txtSKU.Text = _saveText;
        }
      }
    }

    private void FrmKeyBoard_OnSendKeyPad(object sender, NumericKeyboard.DigitKeyPad.eKeyPad keyPad, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      string currentText = "";
      if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_LOW_LIMIT)
      {
        currentText = txtLowerLimit_2T.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_HIGHT_LIMIT)
      {
        currentText = txtUpperLimit_2T.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_FGs)
      {
        currentText = txtFGs.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_TARGET)
      {
        currentText = txtTarget.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_PM)
      {
        currentText = txtPM.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_1T)
      {
        currentText = txtLowerLimit_1T.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_1T)
      {
        currentText = txtUpperLimit_1T.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_2T)
      {
        currentText = txtLowerLimit_2T.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_2T)
      {
        currentText = txtUpperLimit_2T.Text;
      }
      else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_SKU)
      {
        currentText = txtSKU.Text;
      }
      //
      try
      {
        if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_0) currentText += "0";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_1) currentText += "1";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_2) currentText += "2";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_3) currentText += "3";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_4) currentText += "4";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_5) currentText += "5";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_6) currentText += "6";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_7) currentText += "7";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_8) currentText += "8";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_9) currentText += "9";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.BACKSPACE)
        {
          if (currentText.Length > 0)
          {
            currentText = currentText.Remove(currentText.Length - 1);
          }
        }
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.ENTER)
        {
          IsKeepText = true;
        }
        //
        if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_LOW_LIMIT)
        {
          this.txtLowerLimit_2T.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_2T_HIGHT_LIMIT)
        {
          this.txtUpperLimit_2T.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_FGs)
        {
          this.txtFGs.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_TARGET)
        {
          this.txtTarget.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_PM)
        {
          this.txtPM.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_1T)
        {
          txtLowerLimit_1T.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_1T)
        {
          txtUpperLimit_1T.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_2T)
        {
          txtLowerLimit_2T.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_2T)
        {
          txtUpperLimit_2T.Text = currentText;
        }
        else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_SKU)
        {
          txtSKU.Text = currentText;
        }

      }
      catch
      {
      }
      //
    }


    private void txtTarget_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_TARGET;
      DisplayKeyBoard(this.Location.X + this.txtTarget.Location.X + 70, this.Location.Y + this.txtTarget.Location.Y + 70,  _eFromParent);
    }

    private void txtDiff_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_PM;
      DisplayKeyBoard(this.Location.X + this.txtPM.Location.X + 160, this.Location.Y + this.txtPM.Location.Y, _eFromParent);
    }

    private void txtLowerLimit_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_LOWER_LIMIT_1T;
      DisplayKeyBoard(this.Location.X + this.txtLowerLimit_1T.Location.X + 160, this.Location.Y + this.txtLowerLimit_1T.Location.Y - 20, _eFromParent);
    }

    private void txtUpperLimit_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_UPPER_LIMIT_1T;
      DisplayKeyBoard(this.Location.X + this.txtUpperLimit_1T.Location.X + 160, this.Location.Y + this.txtUpperLimit_1T.Location.Y - 50 , _eFromParent);
    }

    private void txtSKU_Click(object sender, EventArgs e)
    {
      //_eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_SKU;
      //DisplayKeyBoard(this.Location.X + this.txtSKU.Location.X + 70, this.Location.Y + this.txtSKU.Location.Y, _eFromParent);
    }

    private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
    {
      //if (e.KeyChar ==)
    }

    

    private void btLoadSKU_Click(object sender, EventArgs e)
    {
      frmLoadSKU frmLoadSKU = new frmLoadSKU(_configuration);
      frmLoadSKU.OnSendChooseSKU += FrmLoadSKU_OnSendChooseSKU;
      frmLoadSKU.ShowDialog();
      frmLoadSKU.BringToFront();
    }

    private void FrmLoadSKU_OnSendChooseSKU(object sender, string SKU)
    {
      this.txtSKU.Text = SKU;
    }

   
  }
}
