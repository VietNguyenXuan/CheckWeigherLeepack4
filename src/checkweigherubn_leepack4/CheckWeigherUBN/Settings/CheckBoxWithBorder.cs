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
  public partial class CheckBoxWithBorder : UserControl
  {
    public CheckBoxWithBorder()
    {
      InitializeComponent();
    }

    public void SetValue(bool value)
    {
      this.checkBox1.Checked = value;
    }

    public bool GetValue()
    {
      return this.checkBox1.Checked;
    }
  }
}
