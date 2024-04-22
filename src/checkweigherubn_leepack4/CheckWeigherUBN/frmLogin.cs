using CheckWeigherUBN.Dialogs;
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
  public partial class frmLogin : Form
  {
    public delegate void SendLoginOK(object sender);
    public event SendLoginOK OnSendLoginOK;
    private string _saveText = "";
    private bool IsKeepText = false;
    private ConfigurationTypes _configuration = new ConfigurationTypes();

    public frmLogin(ConfigurationTypes configuration)
    {
      InitializeComponent();
      _configuration = configuration;
    }

    private void frmLogin_Load(object sender, EventArgs e)
    {
      this.cbUser.Items.Clear();
      for (int i = 0; i < _configuration.list_User.Count; i++)
      {
        if ("vuletech123" == _configuration.list_User[i].UserName)
        {
        }
        else
        {
          this.cbUser.Items.Add(_configuration.list_User[i].UserName);
        }
      }
      //

    }

    private void btCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btOK_Click(object sender, EventArgs e)
    {
      //Check password
      bool IsExitLoop = false;
      bool IsClose = false;
      for (int i = 0; (i < _configuration.list_User.Count) && (IsExitLoop == false); i++)
      {
        UserType user = _configuration.list_User[i];
        //
        if (user.UserName == this.cbUser.Text)
        {
          IsExitLoop = true;
          if (user.Password == this.txtPassword.Text)
          {
            _configuration.currentUserLogin = user;
            IsClose = true;
            if (OnSendLoginOK != null)
            {
              OnSendLoginOK(this);
            }
          }
          else
          {
            //MessageBox.Show("Password không đúng. Vui lòng nhập lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

						FrmWarning frmWarning = new FrmWarning("Password không đúng. Vui lòng nhập lại !");
						frmWarning.ShowDialog();
					}
        }/*if (user.UserName == this.cbUser.Text)*/
      }/*for (int i = 0; (i < _configuration.list_User.Count) && (IsExitLoop == false); i++)*/
      //
      if (IsClose == true)
      {
        this.Close();
      }
    }

    private void DisplayNumericKeyboard(int locationX, int locationY)
    {
      //save Text before change
      IsKeepText = false;
      _saveText = this.txtPassword.Text;
      //
      NumericKeyboard.frmKeyBoard frmKeyBoard = new NumericKeyboard.frmKeyBoard(locationX, locationY, NumericKeyboard.frmKeyBoard.eFromParent.LOGIN);
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
        this.txtPassword.Text = _saveText;
      }
    }

    private void FrmKeyBoard_OnSendKeyPad(object sender, NumericKeyboard.DigitKeyPad.eKeyPad keyPad, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      string currentText = this.txtPassword.Text;

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
        this.txtPassword.Text = currentText;
      }
      catch
      {
      }
      //

    }

    private void txtPassword_Click(object sender, EventArgs e)
    {
      DisplayNumericKeyboard(this.Location.X + this.txtPassword.Location.X, this.Location.Y + this.txtPassword.Location.Y + 60);
    }
  }
}
