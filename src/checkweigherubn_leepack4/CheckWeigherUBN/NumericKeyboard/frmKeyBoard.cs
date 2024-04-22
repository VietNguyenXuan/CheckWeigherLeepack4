using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN.NumericKeyboard
{
  public partial class frmKeyBoard : Form
  {
    
    public delegate void SendKeyPad(object sender, DigitKeyPad.eKeyPad keyPad, eFromParent eFromParent);
    public event SendKeyPad OnSendKeyPad;

    private eFromParent _eFromParent;

    public frmKeyBoard(int location_X, int location_Y, eFromParent eFromParent)
    {
      InitializeComponent();
      //
      _eFromParent = eFromParent;
      //
      this.Key0.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key1.SetKeyPad(DigitKeyPad.eKeyPad.KEY_1);
      this.Key2.SetKeyPad(DigitKeyPad.eKeyPad.KEY_2);
      this.Key3.SetKeyPad(DigitKeyPad.eKeyPad.KEY_3);
      this.Key4.SetKeyPad(DigitKeyPad.eKeyPad.KEY_4);
      this.Key5.SetKeyPad(DigitKeyPad.eKeyPad.KEY_5);
      this.Key6.SetKeyPad(DigitKeyPad.eKeyPad.KEY_6);
      this.Key7.SetKeyPad(DigitKeyPad.eKeyPad.KEY_7);
      this.Key8.SetKeyPad(DigitKeyPad.eKeyPad.KEY_8);
      this.Key9.SetKeyPad(DigitKeyPad.eKeyPad.KEY_9);
      this.KeyEnter.SetKeyPad(DigitKeyPad.eKeyPad.ENTER);
      this.KeyBackspace.SetKeyPad(DigitKeyPad.eKeyPad.BACKSPACE);
      //
      this.Key0.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key1.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key2.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key3.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key4.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key5.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key6.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key7.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key8.OnSendKeyPad += Key_OnSendKeyPad;
      this.Key9.OnSendKeyPad += Key_OnSendKeyPad;
      this.KeyEnter.OnSendKeyPad += Key_OnSendKeyPad;
      this.KeyBackspace.OnSendKeyPad += Key_OnSendKeyPad;
      //
      this.Location = new Point(location_X, location_Y);
    }

    private void Key_OnSendKeyPad(object sender, DigitKeyPad.eKeyPad keyPad)
    {
      if (OnSendKeyPad != null)
      {
        OnSendKeyPad(this, keyPad, _eFromParent);
      }
      //
      if (keyPad == DigitKeyPad.eKeyPad.ENTER)
      {
        this.Close();
      }
    }

    public enum eFromParent
    {
      PRODUCTION_EDIT_TARGET,
      PRODUCTION_EDIT_PM,
      PRODUCTION_EDIT_LOWER_LIMIT_1T,
      PRODUCTION_EDIT_UPPER_LIMIT_1T,
      PRODUCTION_EDIT_LOWER_LIMIT_2T,
      PRODUCTION_EDIT_UPPER_LIMIT_2T,
      PRODUCTION_EDIT_SKU,
      PRODUCTION_EDIT_FGs,
      PRODUCTION_EDIT_2T_LOW_LIMIT,
      PRODUCTION_EDIT_2T_HIGHT_LIMIT,
      //
      MANUAL,
      LOGIN,
      //
      SETTINGS_PC_Delay_Barcode,
      SETTINGS_PC_Reject_Time,
      SETTINGS_PC_Reject_Time_Box_Conti,
      SETTINGS_PC_Delay_Reject,
      SETTINGS_PC_Reject_Number_Box,
      //
      PASSWORD_CHANGE
    }
  }
}
