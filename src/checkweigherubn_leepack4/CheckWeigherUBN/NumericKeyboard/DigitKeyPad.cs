using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN.NumericKeyboard
{
  public partial class DigitKeyPad : UserControl
  {
    public delegate void SendKeyPad(object sender, eKeyPad keyPad);
    public event SendKeyPad OnSendKeyPad;
    //
    public enum eKeyPad
    {
      KEY_0,
      KEY_1,
      KEY_2,
      KEY_3,
      KEY_4,
      KEY_5,
      KEY_6,
      KEY_7,
      KEY_8,
      KEY_9,
      ENTER,
      BACKSPACE
    }
    //this.lblKeyPad.Image = global::CheckWeigherUBN.Properties.Resources.enter_key_25px;
    //Resources.backspace_25px
    private eKeyPad _eKeyPad = eKeyPad.KEY_0;

    
    public DigitKeyPad()
    {
      InitializeComponent();
    }
    public void SetKeyPad(eKeyPad eKeyPad)
    {
      _eKeyPad = eKeyPad;
      if (_eKeyPad == eKeyPad.KEY_0)
      {
        this.lblKeyPad.Text = "0";
      }
      else if (_eKeyPad == eKeyPad.KEY_1)
      {
        this.lblKeyPad.Text = "1";
      }
      else if (_eKeyPad == eKeyPad.KEY_2)
      {
        this.lblKeyPad.Text = "2";
      }
      else if (_eKeyPad == eKeyPad.KEY_3)
      {
        this.lblKeyPad.Text = "3";
      }
      else if (_eKeyPad == eKeyPad.KEY_4)
      {
        this.lblKeyPad.Text = "4";
      }
      else if (_eKeyPad == eKeyPad.KEY_5)
      {
        this.lblKeyPad.Text = "5";
      }
      else if (_eKeyPad == eKeyPad.KEY_6)
      {
        this.lblKeyPad.Text = "6";
      }
      else if (_eKeyPad == eKeyPad.KEY_7)
      {
        this.lblKeyPad.Text = "7";
      }
      else if (_eKeyPad == eKeyPad.KEY_8)
      {
        this.lblKeyPad.Text = "8";
      }
      else if (_eKeyPad == eKeyPad.KEY_9)
      {
        this.lblKeyPad.Text = "9";
      }
      else if (_eKeyPad == eKeyPad.ENTER)
      {
        this.lblKeyPad.Text = "";
        this.lblKeyPad.Image = global::CheckWeigherUBN.Properties.Resources.enter_key_25px;
      }
      else if (_eKeyPad == eKeyPad.BACKSPACE)
      {
        this.lblKeyPad.Text = "";
        this.lblKeyPad.Image = global::CheckWeigherUBN.Properties.Resources.backspace_25px;
      }

    }

    private void lblKeyPad_Click(object sender, EventArgs e)
    {
      if (OnSendKeyPad != null)
      {
        OnSendKeyPad(this, _eKeyPad);
      }
    }
  }
}
