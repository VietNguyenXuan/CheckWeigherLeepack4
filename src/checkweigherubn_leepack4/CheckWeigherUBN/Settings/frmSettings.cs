using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace CheckWeigherUBN
{
  public partial class frmSettings : Form
  {
    public delegate void RequestSendUpdateParameter(NumericKeyboard.frmKeyBoard.eFromParent eFromParent, int value);
    public event RequestSendUpdateParameter OnRequestSendUpdateParameter;
    //
    private ConfigurationTypes _configuration = new ConfigurationTypes();
    private bool IsEnableUpdateData = true;
    //
    private string _saveText = "";
    private bool IsKeepText = false;
    private NumericKeyboard.frmKeyBoard.eFromParent _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.PRODUCTION_EDIT_TARGET;
    private int startLocation_X = 0;
    private int startLocation_Y = 0;
    //
    public frmSettings(ConfigurationTypes configuration)
    {
      InitializeComponent();
      _configuration = configuration;
      this.ControlBox = false;
    }


    private void btExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btSave_Click(object sender, EventArgs e)
    {
      bool IsNeedSave = false;
      if (this.txtTemplatePath.Text == "")
      {
        MessageBox.Show("Vui lòng nhập Template Path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtDatabasePath.Text == "")
      {
        MessageBox.Show("Vui lòng nhập Database Path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPLC_IPAddress.Text == "")
      {
        MessageBox.Show("Vui lòng nhập PLC IP Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPortNumber.Text == "")
      {
        MessageBox.Show("Vui lòng nhập PortNumber", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtMaxProductDisplay.Text == "")
      {
        MessageBox.Show("Vui lòng nhập MaxProductDisplay", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPC_Delay_Barcode.Text == "")
      {
        MessageBox.Show("Vui lòng nhập 'Delay Trigger Barcode'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPC_Reject_Time.Text == "")
      {
        MessageBox.Show("Vui lòng nhập 'Time Reject'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPC_Reject_Time_Box_Conti.Text == "")
      {
        MessageBox.Show("Vui lòng nhập 'Thời gian thùng tiếp theo vào'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPC_Delay_Reject.Text == "")
      {
        MessageBox.Show("Vui lòng nhập 'Delay trigger reject'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else if (this.txtPC_Reject_Number_Box.Text == "")
      {
        MessageBox.Show("Vui lòng nhập 'Số thùng nằm giữa khoảng reject'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      //this.txtPC_Delay_Barcode.Text = _configuration.PC_Delay_Barcode.ToString();
      //this.txtPC_Reject_Time.Text = _configuration.PC_Reject_Time.ToString();
      //this.txtPC_Reject_Time_Box_Conti.Text = _configuration.PC_Reject_Time_Box_Conti.ToString();
      //this.txtPC_Delay_Reject.Text = _configuration.PC_Delay_Reject.ToString();
      //this.txtPC_Reject_Number_Box.Text = _configuration.PC_Reject_Number_Box.ToString();
      else
      {
        IsNeedSave = (this.txtTemplatePath.Text != _configuration.TemplatePath);
        IsNeedSave |= (this.txtDatabasePath.Text != _configuration.DatabasePath);
        IsNeedSave |= (this.txtPLC_IPAddress.Text != _configuration.Communication.PLC_IpAddress);
        IsNeedSave |= (this.txtPortNumber.Text != _configuration.Communication.PortNumber.ToString());
        IsNeedSave |= (this.txtMaxProductDisplay.Text != _configuration.MaxProductDisplay.ToString());
        //
        IsNeedSave |= (this.txtPC_Delay_Barcode.Text != _configuration.PC_Delay_Barcode.ToString());
        IsNeedSave |= (this.txtPC_Reject_Time.Text != _configuration.PC_Reject_Time.ToString());
        IsNeedSave |= (this.txtPC_Reject_Time_Box_Conti.Text != _configuration.PC_Reject_Time_Box_Conti.ToString());
        IsNeedSave |= (this.txtPC_Delay_Reject.Text != _configuration.PC_Delay_Reject.ToString());
        IsNeedSave |= (this.txtPC_Reject_Number_Box.Text != _configuration.PC_Reject_Number_Box.ToString());

        //
        if ((this.txtPC_Delay_Barcode.Text != _configuration.PC_Delay_Barcode.ToString()) || (this.txtPC_Delay_Barcode.BackColor == Color.Yellow))
        {
          if (OnRequestSendUpdateParameter != null)
          {
            OnRequestSendUpdateParameter(NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Barcode, this.txtPC_Delay_Barcode.Text.Convert_to_Int());
          }
          _configuration.PC_Delay_Barcode = this.txtPC_Delay_Barcode.Text.Convert_to_Int();
          IsNeedSave |= true;
        }
        //
        if ((this.txtPC_Reject_Time.Text != _configuration.PC_Reject_Time.ToString())|| (this.txtPC_Reject_Time.BackColor == Color.Yellow))
        {
          if (OnRequestSendUpdateParameter != null)
          {
            OnRequestSendUpdateParameter(NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time, this.txtPC_Reject_Time.Text.Convert_to_Int());
          }
          _configuration.PC_Reject_Time = this.txtPC_Reject_Time.Text.Convert_to_Int();
          IsNeedSave |= true;
        }
        //
        if ((this.txtPC_Reject_Time_Box_Conti.Text != _configuration.PC_Reject_Time_Box_Conti.ToString())|| (this.txtPC_Reject_Time_Box_Conti.BackColor == Color.Yellow))
        {
          if (OnRequestSendUpdateParameter != null)
          {
            OnRequestSendUpdateParameter(NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time_Box_Conti, this.txtPC_Reject_Time_Box_Conti.Text.Convert_to_Int());
          }
          _configuration.PC_Reject_Time_Box_Conti = this.txtPC_Reject_Time_Box_Conti.Text.Convert_to_Int();
          IsNeedSave |= true;
        }
        //
        if ((this.txtPC_Delay_Reject.Text != _configuration.PC_Delay_Reject.ToString())|| (this.txtPC_Delay_Reject.BackColor == Color.Yellow))
        {
          if (OnRequestSendUpdateParameter != null)
          {
            OnRequestSendUpdateParameter(NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Reject, this.txtPC_Delay_Reject.Text.Convert_to_Int());
          }
          _configuration.PC_Delay_Reject = this.txtPC_Delay_Reject.Text.Convert_to_Int();
          IsNeedSave |= true;
        }
        //
        if ((this.txtPC_Reject_Number_Box.Text != _configuration.PC_Reject_Number_Box.ToString())|| (this.txtPC_Reject_Number_Box.BackColor == Color.Yellow))
        {
          if (OnRequestSendUpdateParameter != null)
          {
            OnRequestSendUpdateParameter(NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Number_Box, this.txtPC_Reject_Number_Box.Text.Convert_to_Int());
          }
          _configuration.PC_Reject_Number_Box = this.txtPC_Reject_Number_Box.Text.Convert_to_Int();
          IsNeedSave |= true;
        }

        //
        IsNeedSave |= (_configuration.ReportPath != txtReportPath.Text);
        //---------------------
        if (IsNeedSave == true)
        {
          _configuration.TemplatePath = this.txtTemplatePath.Text;
          _configuration.DatabasePath = this.txtDatabasePath.Text;
          _configuration.MaxProductDisplay = this.txtMaxProductDisplay.Text.Convert_to_Int();
          _configuration.ReportPath = txtReportPath.Text;
          //save 
          ConfigurationDB sqlConfigurationDB = new ConfigurationDB();
          sqlConfigurationDB.Save(_configuration);

          //
          CommunicationsDB sqlCommunicationsDB = new CommunicationsDB();
          _configuration.Communication.PLC_IpAddress = this.txtPLC_IPAddress.Text;
          _configuration.Communication.PortNumber = Utils.string_to_int(this.txtPortNumber.Text);
          sqlCommunicationsDB.Save(_configuration.Communication);
        }
        //
        this.Close();
      }
    }

    private void AddProductCheckTypeToCombobox()
    {
      this.cbBoxProducts.Items.Clear();
      this.cbBoxProducts.Items.Add("THÙNG");
      this.cbBoxProducts.Items.Add("CHAI");
    }

    private void frmSettings_Load(object sender, EventArgs e)
    {
      IsEnableUpdateData = false;
      //
      this.txtTemplatePath.Text = _configuration.TemplatePath;
      this.txtDatabasePath.Text = _configuration.DatabasePath;
      this.txtPLC_IPAddress.Text = _configuration.Communication.PLC_IpAddress;
      this.txtPortNumber.Text = _configuration.Communication.PortNumber.ToString();
      this.txtMaxProductDisplay.Text = _configuration.MaxProductDisplay.ToString();
      this.txtReportPath.Text = _configuration.ReportPath.ToString();
      //---------------------------------------------
      AddProductCheckTypeToCombobox();
      //if (_configuration.ProductCheckType == (int)(eProductCheck.BOX))
      //{
      //  this.cbBoxProducts.Text = "THÙNG";
      //}
      //else if (_configuration.ProductCheckType == (int)(eProductCheck.BOTTLE))
      //{
      //  this.cbBoxProducts.Text = "CHAI";
      //}
      //---------------------------------------------
      this.txtPC_Delay_Barcode.Text = _configuration.PC_Delay_Barcode.ToString();
      this.txtPC_Reject_Time.Text = _configuration.PC_Reject_Time.ToString();
      this.txtPC_Reject_Time_Box_Conti.Text = _configuration.PC_Reject_Time_Box_Conti.ToString();
      this.txtPC_Delay_Reject.Text = _configuration.PC_Delay_Reject.ToString();
      this.txtPC_Reject_Number_Box.Text = _configuration.PC_Reject_Number_Box.ToString();
      //
      //
      startLocation_X = this.Location.X + this.groupBox4.Location.X;
      startLocation_Y = this.Location.Y + this.groupBox4.Location.Y;
      //
      IsEnableUpdateData = true;
    }

    private void txtPLC_IPAddress_Click(object sender, EventArgs e)
    {
      //Process process = Process.Start(new ProcessStartInfo(
      //      ((Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\osk.exe"))));
    }

    private void btMatrixPermission_Click(object sender, EventArgs e)
    {
      frmMatrixPermission frmMatrixPermission = new frmMatrixPermission(_configuration);
      frmMatrixPermission.ShowDialog();
    }

    

    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      if (IsEnableUpdateData == true)
      {
        if (this.txtPC_Delay_Barcode.Text != machineData.PC_Delay_Barcode.value.Convert_to_Int().ToString())
        {
          this.txtPC_Delay_Barcode.BackColor = Color.Yellow;
        }
        //
        if (this.txtPC_Reject_Time.Text != machineData.PC_Reject_Time_54.value.Convert_to_Int().ToString())
        {
          this.txtPC_Reject_Time.BackColor = Color.Yellow;
        }
        //
        if (this.txtPC_Reject_Time_Box_Conti.Text != machineData.PC_Reject_Time_Box_Conti_57.value.Convert_to_Int().ToString())
        {
          this.txtPC_Reject_Time_Box_Conti.BackColor = Color.Yellow;
        }
        //
        if (this.txtPC_Delay_Reject.Text != machineData.PC_Delay_Reject_58.value.Convert_to_Int().ToString())
        {
          this.txtPC_Delay_Reject.BackColor = Color.Yellow;
        }
        //
        if (this.txtPC_Reject_Number_Box.Text != machineData.PC_Reject_Number_Box_59.value.Convert_to_Int().ToString())
        {
          this.txtPC_Reject_Number_Box.BackColor = Color.Yellow;
        }
      }
      //
      
    }

    private void DisplayKeyBoard(int locationX, int locationY, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      IsKeepText = false;
      if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Barcode)
      {
        _saveText = this.txtPC_Delay_Barcode.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time)
      {
        _saveText = this.txtPC_Reject_Time.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time_Box_Conti)
      {
        _saveText = this.txtPC_Reject_Time_Box_Conti.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Reject)
      {
        _saveText = this.txtPC_Delay_Reject.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Number_Box)
      {
        _saveText = this.txtPC_Reject_Number_Box.Text;
      }
      IsEnableUpdateData = false;
      //
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
        if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Barcode)
        {
          txtPC_Delay_Barcode.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time)
        {
          this.txtPC_Reject_Time.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time_Box_Conti)
        {
          this.txtPC_Reject_Time_Box_Conti.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Reject)
        {
          this.txtPC_Delay_Reject.Text = _saveText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Number_Box)
        {
          this.txtPC_Reject_Number_Box.Text = _saveText;
        }
      }
      //
      IsEnableUpdateData = true;
    }


    private void FrmKeyBoard_OnSendKeyPad(object sender, NumericKeyboard.DigitKeyPad.eKeyPad keyPad, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      string currentText = "";
      if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Barcode)
      {
        currentText = txtPC_Delay_Barcode.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time)
      {
        currentText = this.txtPC_Reject_Time.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time_Box_Conti)
      {
        currentText = this.txtPC_Reject_Time_Box_Conti.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Reject)
      {
        currentText = this.txtPC_Delay_Reject.Text;
      }
      else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Number_Box)
      {
        currentText = this.txtPC_Reject_Number_Box.Text;
      }
      //

      if (currentText == "0")
      {
        currentText = "";
      }
      //
      try
      {
        if (((int)(keyPad) >= 0) && ((int)(keyPad) <= 9))
        {
          string tmp = String.Format("{0}{1}", currentText, GetKeyChar(keyPad));
          int tmpAsInt = tmp.Convert_to_Int();
          if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Barcode)
          {
            currentText += SetColorWithMaxMin(this.txtPC_Delay_Barcode,  GetKeyChar(keyPad), tmpAsInt, 1, 50);
          }
          else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time)
          {
            currentText += SetColorWithMaxMin(this.txtPC_Reject_Time, GetKeyChar(keyPad), tmpAsInt, 1, 50);
          }
          else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time_Box_Conti)
          {
            currentText += SetColorWithMaxMin(this.txtPC_Reject_Time_Box_Conti,  GetKeyChar(keyPad), tmpAsInt, 1, 500);
          }
          else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Reject)
          {
            currentText += SetColorWithMaxMin(this.txtPC_Delay_Reject, GetKeyChar(keyPad), tmpAsInt, 1, 50);
          }
          else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Number_Box)
          {
            currentText += SetColorWithMaxMin(this.txtPC_Reject_Number_Box, GetKeyChar(keyPad), tmpAsInt, 1, 5);
          }

          //
         
        }
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.BACKSPACE)
        {
          if (currentText.Length > 0)
          {
            string currentValue = currentText.Remove(currentText.Length - 1); ;
            currentText = currentValue;
          }
        }
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.ENTER)
        {
          if (currentText == "") currentText = "1";
          IsKeepText = true;
        }
        //-------------------------------------------------------------------
        if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Barcode)
        {
          txtPC_Delay_Barcode.Text = currentText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time)
        {
          this.txtPC_Reject_Time.Text = currentText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time_Box_Conti)
        {
          this.txtPC_Reject_Time_Box_Conti.Text = currentText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Reject)
        {
          this.txtPC_Delay_Reject.Text = currentText;
        }
        else if (_eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Number_Box)
        {
          this.txtPC_Reject_Number_Box.Text = currentText;
        }

      }
      catch
      {
      }
    }

    private string SetColorWithMaxMin(TextBox textBox, string currentValue, int tmpAsInt, int min, int max)
    {
      string currentText = "";
      //Check with max-min
      if ((tmpAsInt >= min) && (tmpAsInt <= max))
      {
        currentText = currentValue;
        textBox.BackColor = Color.White;
      }
      else
      {
        //textBox.BackColor = Color.Red;
      }
      return currentText;
    }
    private string GetKeyChar(NumericKeyboard.DigitKeyPad.eKeyPad keyPad)
    {
      string keyReturn = "";
      if (((int)(keyPad) >= 0) && ((int)(keyPad) <= 9))
      {
        keyReturn = String.Format("{0}",(int)(keyPad));
      }
      return keyReturn;
    }

    private void txtPC_Delay_Barcode_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Barcode;
      DisplayKeyBoard(startLocation_X + this.txtPC_Delay_Barcode.Location.X + 70, startLocation_Y + this.txtPC_Delay_Barcode.Location.Y, _eFromParent);
    }

    private void txtPC_Reject_Time_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time;
      DisplayKeyBoard(startLocation_X + this.txtPC_Reject_Time.Location.X + 70, startLocation_Y + this.txtPC_Reject_Time.Location.Y, _eFromParent);
    }

    private void txtPC_Reject_Time_Box_Conti_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time_Box_Conti;
      DisplayKeyBoard(startLocation_X + this.txtPC_Reject_Time_Box_Conti.Location.X + 70, startLocation_Y + this.txtPC_Reject_Time.Location.Y, _eFromParent);
    }

    private void txtPC_Delay_Reject_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Reject;
      DisplayKeyBoard(startLocation_X + this.txtPC_Delay_Reject.Location.X + 70, startLocation_Y + this.txtPC_Reject_Time.Location.Y, _eFromParent);
    }

    private void txtPC_Reject_Number_Box_Click(object sender, EventArgs e)
    {
      _eFromParent = NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Number_Box;
      DisplayKeyBoard(startLocation_X + this.txtPC_Reject_Number_Box.Location.X + 70, startLocation_Y + this.txtPC_Reject_Time.Location.Y, _eFromParent);
    }

    private void btPassword_Click(object sender, EventArgs e)
    {
      frmPassword frmPassword = new frmPassword(_configuration);
      frmPassword.ShowDialog();
    }

    private void btFolderBrowser_Click(object sender, EventArgs e)
    {
      DialogResult result =  folderBrowserDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        this.txtReportPath.Text = folderBrowserDialog1.SelectedPath;
        this.txtReportPath.ForeColor = Color.Red;
      }
    }
  }
}
