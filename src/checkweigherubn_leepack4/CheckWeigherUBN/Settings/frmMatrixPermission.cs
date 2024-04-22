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
  public partial class frmMatrixPermission : Form
  {
    private ConfigurationTypes _configuration;
    public frmMatrixPermission(ConfigurationTypes configuration)
    {
      InitializeComponent();
      _configuration = configuration;
    }

   

    private void btExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btSave_Click(object sender, EventArgs e)
    {
      int Permission1 = this.checkboxPermission1.GetPermission();
      int Permission2 = this.checkboxPermission2.GetPermission();
      int Permission3 = this.checkboxPermission3.GetPermission();
      int Permission4 = this.checkboxPermission4.GetPermission();
      int Permission5 = this.checkboxPermission5.GetPermission();
      int Permission6 = this.checkboxPermission6.GetPermission();
      int Permission7 = this.checkboxPermission7.GetPermission();
      //

      bool IsEnableSave = false;
      if (Permission1 != _configuration.Permission1)
      {
        _configuration.Permission1 = Permission1;
        IsEnableSave = true;
      }
      //
      if (Permission2 != _configuration.Permission2)
      {
        _configuration.Permission2 = Permission2;
        IsEnableSave = true;
      }
      //
      if (Permission3 != _configuration.Permission3)
      {
        _configuration.Permission3 = Permission3;
        IsEnableSave = true;
      }
      //
      if (Permission4 != _configuration.Permission4)
      {
        _configuration.Permission4 = Permission4;
        IsEnableSave = true;
      }
      //
      if (Permission5 != _configuration.Permission5)
      {
        _configuration.Permission5 = Permission5;
        IsEnableSave = true;
      }
      //
      if (Permission6 != _configuration.Permission6)
      {
        _configuration.Permission6 = Permission6;
        IsEnableSave = true;
      }
      //
      if (Permission7 != _configuration.Permission7)
      {
        _configuration.Permission7 = Permission7;
        IsEnableSave = true;
      }

      //
      if (IsEnableSave == true)
      {
        ConfigurationDB sqlConfigurationDB = new ConfigurationDB();
        sqlConfigurationDB.Save(_configuration);
      }
      //
      this.Close();
    }

    private void frmMatrixPermission_Load(object sender, EventArgs e)
    {
      this.checkboxPermission1.SetPermission(_configuration.Permission1);
      this.checkboxPermission2.SetPermission(_configuration.Permission2);
      this.checkboxPermission3.SetPermission(_configuration.Permission3);
      this.checkboxPermission4.SetPermission(_configuration.Permission4);
      this.checkboxPermission5.SetPermission(_configuration.Permission5);
      this.checkboxPermission6.SetPermission(_configuration.Permission6);
      this.checkboxPermission7.SetPermission(_configuration.Permission7);
    }

		private void label4_Click(object sender, EventArgs e)
		{

		}
	}
}
