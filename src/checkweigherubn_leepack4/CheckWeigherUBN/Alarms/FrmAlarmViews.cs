using CheckWeigherUBN.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN.Alarms
{
  public partial class FrmAlarmViews : Form
  {
    private ConfigurationTypes _configuration;
    public FrmAlarmViews(ConfigurationTypes configuration)
    {
      InitializeComponent();
      _configuration = configuration;
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btExit1_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btLoad_Click(object sender, EventArgs e)
    {
      try
      {
        DateTime dt = Convert.ToDateTime(dateTimePicker3.Text);
        eShift shift = (eShift)(cbShift.SelectedIndex);
        this.errorLog1.DisplayAlarmToListView(dt, shift, _configuration.TemplatePath, _configuration.DatabasePath);
        
      }
      catch
      {
      }

    }

    private string _alarm_output_file_name = "";
		private List<AlarmType> _list_alarms = new List<AlarmType>();
    private bool _is_alarm_export_ok = false;

		private void btReport_Click(object sender, EventArgs e)
		{
      
      //string _output_file_name = "Test";
			//load data to review
			try
			{
				DateTime dt = Convert.ToDateTime(dateTimePicker3.Text);
				eShift shift = (eShift)(cbShift.SelectedIndex);
				_list_alarms =  this.errorLog1.DisplayAlarmToListView(dt, shift, _configuration.TemplatePath, _configuration.DatabasePath);
				//
        if (_list_alarms.Count > 0)
        {
          string file_name = $"AlarmReports__{dt.Year}_{dt.Month}_{dt.Day}__{shift}";
					saveFileDialog1.FileName = String.Format("{0}", file_name);
          //-------------------------------------------------------------------------------------
          saveFileDialog1.ShowDialog();
          //--------------------------------------
				
        }
        else
        {

        }
			}
			catch
			{
			}



			
		}

		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
      this._alarm_output_file_name = this.saveFileDialog1.FileName;
			this._is_alarm_export_ok = false;
			//-----------------
			ReportExcel reportExcel = new ReportExcel(_configuration, _alarm_output_file_name, this._list_alarms);
			object ret = reportExcel.Execute();
			if (ret != null)
			{
				if (ret is bool)
				{
					//_IsExportExcel_OK = (bool)(ret);
					// MessageBox.Show("Data export OK");

					_is_alarm_export_ok = (bool)(ret);

					this.timer_delay.Enabled = true;
				}
			}
		}

		private void timer_delay_Tick(object sender, EventArgs e)
		{
			this.timer_delay.Enabled = false;
      if (_is_alarm_export_ok == true)
      {
        //MessageBox.Show("Data export OK");

        FrmConfirmation frmConfirmation = new FrmConfirmation("Data export OK", FrmConfirmation.eConfirmationType.ExportDataAlarms);
        frmConfirmation.ShowDialog();
      }
      else
      {
				//MessageBox.Show("Data export FAIL");
				FrmConfirmation frmConfirmation = new FrmConfirmation("Data export FAIL", FrmConfirmation.eConfirmationType.ExportDataAlarms);
				frmConfirmation.ShowDialog();
			}
		}

		private void FrmAlarmViews_Load(object sender, EventArgs e)
		{
			try
			{
				DateTime dt = Convert.ToDateTime(dateTimePicker3.Text);
				eShift shift = (eShift)(cbShift.SelectedIndex);
				this.errorLog1.DisplayAlarmToListView(dt, shift, _configuration.TemplatePath, _configuration.DatabasePath);

			}
			catch
			{
			}
		}
	}
}
