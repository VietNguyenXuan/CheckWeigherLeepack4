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
  public partial class CheckboxPermission : UserControl
  {
    
    public CheckboxPermission()
    {
      InitializeComponent();
    }
    public void SetPermission(int Permission)
    {
      this.checkBoxWithBorder0.SetValue(Permission.ToBoolean(0));
      this.checkBoxWithBorder1.SetValue(Permission.ToBoolean(1));
      this.checkBoxWithBorder2.SetValue(Permission.ToBoolean(2));
      this.checkBoxWithBorder3.SetValue(Permission.ToBoolean(3));
      this.checkBoxWithBorder4.SetValue(Permission.ToBoolean(4));
      this.checkBoxWithBorder5.SetValue(Permission.ToBoolean(5));
      this.checkBoxWithBorder6.SetValue(Permission.ToBoolean(6));
      this.checkBoxWithBorder7.SetValue(Permission.ToBoolean(7));
      this.checkBoxWithBorder8.SetValue(Permission.ToBoolean(8));
      this.checkBoxWithBorder9.SetValue(Permission.ToBoolean(9));
      this.checkBoxWithBorder10.SetValue(Permission.ToBoolean(10));
    }
    public int GetPermission()
    {
      int tmp = 0;
      if (this.checkBoxWithBorder0.GetValue() == true)
      {
        tmp |= (1 << 0);
      }
      else
      {
        tmp &= ~(1 << 0);
      }
      //--------------------------
      if (this.checkBoxWithBorder1.GetValue() == true)
      {
        tmp |= (1 << 1);
      }
      else
      {
        tmp &= ~(1 << 1);
      }
      //--------------------------
      if (this.checkBoxWithBorder2.GetValue() == true)
      {
        tmp |= (1 << 2);
      }
      else
      {
        tmp &= ~(1 << 2);
      }
      //--------------------------
      if (this.checkBoxWithBorder3.GetValue() == true)
      {
        tmp |= (1 << 3);
      }
      else
      {
        tmp &= ~(1 << 3);
      }
      //--------------------------
      if (this.checkBoxWithBorder4.GetValue() == true)
      {
        tmp |= (1 << 4);
      }
      else
      {
        tmp &= ~(1 << 4);
      }
      //--------------------------
      if (this.checkBoxWithBorder5.GetValue() == true)
      {
        tmp |= (1 << 5);
      }
      else
      {
        tmp &= ~(1 << 5);
      }
      //--------------------------
      if (this.checkBoxWithBorder6.GetValue() == true)
      {
        tmp |= (1 << 6);
      }
      else
      {
        tmp &= ~(1 << 6);
      }
      //--------------------------
      if (this.checkBoxWithBorder7.GetValue() == true)
      {
        tmp |= (1 << 7);
      }
      else
      {
        tmp &= ~(1 << 7);
      }
      //--------------------------
      if (this.checkBoxWithBorder8.GetValue() == true)
      {
        tmp |= (1 << 8);
      }
      else
      {
        tmp &= ~(1 << 8);
      }
      //--------------------------
      if (this.checkBoxWithBorder9.GetValue() == true)
      {
        tmp |= (1 << 9);
      }
      else
      {
        tmp &= ~(1 << 9);
      }
      //--------------------------
      if (this.checkBoxWithBorder10.GetValue() == true)
      {
        tmp |= (1 << 10);
      }
      else
      {
        tmp &= ~(1 << 10);
      }
      return tmp;
    }
  }
}
