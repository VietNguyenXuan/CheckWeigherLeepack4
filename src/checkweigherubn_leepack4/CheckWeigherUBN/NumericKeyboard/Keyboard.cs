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
  public partial class Keyboard : UserControl
  {

    public Keyboard()
    {
      InitializeComponent();
      this.Key0.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key1.SetKeyPad(DigitKeyPad.eKeyPad.KEY_1);
      this.Key2.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key3.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key4.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key5.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key6.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key7.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key8.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.Key9.SetKeyPad(DigitKeyPad.eKeyPad.KEY_0);
      this.KeyEnter.SetKeyPad(DigitKeyPad.eKeyPad.ENTER);
      this.KeyBackspace.SetKeyPad(DigitKeyPad.eKeyPad.BACKSPACE);
    }
  }
}
