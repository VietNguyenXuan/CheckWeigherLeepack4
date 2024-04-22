using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
  public partial class PasswordChange : UserControl
  {
    private UserType.eRole _eRole = UserType.eRole.Admin;
    private ConfigurationTypes _configuration = null;
    private int startLocation_X = 0;
    private int startLocation_Y = 0;
    //
    private string _saveText = "";
    private bool IsKeepText = false;
    public PasswordChange()
    {
      InitializeComponent();
    }

    public void SetConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
    }

    public void SetRole(UserType.eRole eRole)
    {
      _eRole = eRole;
      if (UserType.eRole.OP_shift_1 == _eRole)
      {
        this.lblRole.Text = "OP shift 1";
      }
      else if (UserType.eRole.OP_shift_2 == _eRole)
      {
        this.lblRole.Text = "OP shift 2";
      }
      else if (UserType.eRole.OP_shift_3 == _eRole)
      {
        this.lblRole.Text = "OP shift 3";
      }
      else if (UserType.eRole.Quality == _eRole)
      {
        this.lblRole.Text = "Quality";
      }
      else if (UserType.eRole.M_E == _eRole)
      {
        this.lblRole.Text = "M&&E";
      }
      else if (UserType.eRole.Manager == _eRole)
      {
        this.lblRole.Text = "Manager";
      }

      //OP_shift_2 = 2,
      //OP_shift_3,
      //Quality,
      //M_E,
      //Manager,
      //Admin,
    }
    public void SetPassword(string password, int locationX, int locationY)
    {
      this.txtPassword.Text = password;
      startLocation_X = locationX;
      startLocation_Y = locationY;
    }

    private void txtPassword_Click(object sender, EventArgs e)
    {
      DisplayKeyBoard(startLocation_X + 200, startLocation_Y, NumericKeyboard.frmKeyBoard.eFromParent.PASSWORD_CHANGE);
    }

    private string currentText = "";

    private void FrmKeyBoard_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (IsKeepText == true)
      {
        /* do nothing */
      }
      else
      {
        txtPassword.Text = _saveText;
        txtPassword.BackColor = Color.White;
      }
    }
    private void FrmKeyBoard_OnSendKeyPad(object sender, NumericKeyboard.DigitKeyPad.eKeyPad keyPad, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      if (((int)(keyPad) >= 0) && ((int)(keyPad) <= 9))
      {
        currentText += String.Format("{0}", (int)(keyPad));
      }
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
      txtPassword.Text = currentText;
     
       txtPassword.BackColor = Color.Yellow;
    }

    private void DisplayKeyBoard(int locationX, int locationY, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      IsKeepText = false;
      _saveText = this.txtPassword.Text;

      //
      currentText = "";
      NumericKeyboard.frmKeyBoard frmKeyBoard = new NumericKeyboard.frmKeyBoard(locationX, locationY, eFromParent);
      frmKeyBoard.OnSendKeyPad += FrmKeyBoard_OnSendKeyPad;
      frmKeyBoard.FormClosed += FrmKeyBoard_FormClosed;
      frmKeyBoard.ShowDialog();
    }

    private void btChange_Click(object sender, EventArgs e)
    {
      UserDB sqlUserDB = new UserDB();
      bool IsExitLoop = false;
      for (int i = 0; (i < _configuration.list_User.Count) && (IsExitLoop == false); i++)
      {
        UserType user = _configuration.list_User[i];
        if (user.RoleId == (int)(_eRole))
        {
          IsExitLoop = true;
          //
          if (user.Password != txtPassword.Text)
          {
            user.Password = txtPassword.Text;
            user.Save();
          }
          txtPassword.BackColor = Color.White;
          //sqlUserDB.Save(user);
        }/*if (user.RoleId == (int)(_eRole))*/
      }/*for (int i = 0; (i < _configuration.list_User.Count) && (IsExitLoop == false); i++)*/
    }
  }
}
