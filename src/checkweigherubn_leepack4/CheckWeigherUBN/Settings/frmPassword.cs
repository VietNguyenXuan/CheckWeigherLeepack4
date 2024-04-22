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
  public partial class frmPassword : Form
  {
    private ConfigurationTypes _configuration = null;
    public frmPassword(ConfigurationTypes configuration)
    {
      InitializeComponent();
      //
      _configuration = configuration;
     
    }

    private void frmPassword_Load(object sender, EventArgs e)
    {
      for (int i = 0; i < _configuration.list_User.Count; i++)
      {
        UserType user = _configuration.list_User[i];
        if (user.RoleId == (int)(UserType.eRole.OP_shift_1))
        {
          this.passwordChangeOPshift1.SetConfiguration(_configuration);
          this.passwordChangeOPshift1.SetRole(UserType.eRole.OP_shift_1);
          this.passwordChangeOPshift1.SetPassword(user.Password, this.Location.X + this.passwordChangeOPshift1.Location.X, this.Location.Y + this.passwordChangeOPshift1.Location.Y);
        }
        else if (user.RoleId == (int)(UserType.eRole.OP_shift_2))
        {
          this.passwordChangeOPshift2.SetConfiguration(_configuration);
          this.passwordChangeOPshift2.SetRole(UserType.eRole.OP_shift_2);
          this.passwordChangeOPshift2.SetPassword(user.Password, this.Location.X + this.passwordChangeOPshift2.Location.X, this.Location.Y + this.passwordChangeOPshift2.Location.Y);
        }
        else if (user.RoleId == (int)(UserType.eRole.OP_shift_3))
        {
          this.passwordChangeOPshift3.SetConfiguration(_configuration);
          this.passwordChangeOPshift3.SetRole(UserType.eRole.OP_shift_3);
          this.passwordChangeOPshift3.SetPassword(user.Password, this.Location.X + this.passwordChangeOPshift3.Location.X, this.Location.Y + this.passwordChangeOPshift3.Location.Y);
        }
        else if (user.RoleId == (int)(UserType.eRole.Quality))
        {
          this.passwordChangeQuality.SetConfiguration(_configuration);
          this.passwordChangeQuality.SetRole(UserType.eRole.Quality);
          this.passwordChangeQuality.SetPassword(user.Password, this.Location.X + this.passwordChangeQuality.Location.X, this.Location.Y + this.passwordChangeQuality.Location.Y);
        }
        else if (user.RoleId == (int)(UserType.eRole.M_E))
        {
          this.passwordChangeME.SetConfiguration(_configuration);
          this.passwordChangeME.SetRole(UserType.eRole.M_E);
          this.passwordChangeME.SetPassword(user.Password, this.Location.X + this.passwordChangeME.Location.X, this.Location.Y + this.passwordChangeME.Location.Y);
        }
        else if (user.RoleId == (int)(UserType.eRole.Manager))
        {
          this.passwordChangeManager.SetConfiguration(_configuration);
          this.passwordChangeManager.SetRole(UserType.eRole.Manager);
          this.passwordChangeManager.SetPassword(user.Password, this.Location.X + this.passwordChangeManager.Location.X, this.Location.Y + this.passwordChangeManager.Location.Y);
        }
      }
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
