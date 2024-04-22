using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GlacialComponents.Controls;
namespace CheckWeigherUBN.ExcelHandle
{
  public partial class ExcelReportUC : UserControl
  {
    public delegate void btExitClicked(object sender);
    public event btExitClicked OnBtExitClicked;


    private ConfigurationTypes _configuration = new ConfigurationTypes();

    private eRequest _eRequest = ExcelReportUC.eRequest.NO_THING;
    private DateTime _dateTimeWithTimeFrom;
    private DateTime _dateTimeWithTimeTo;
    private eShift _eShift = eShift.SHIFT_1;

    private string _folder_Path = "";
    private string _output_file_name = "";
    private bool _IsExportExcel_OK = false;


    private List<DataLogType> _list_weigher_data_from_database = new List<DataLogType>();
    //
    private const int COL_ID = 0;
    private const int COL_DATETIME = COL_ID + 1;
    private const int COL_NOZZLE = COL_DATETIME + 1;
    private const int COL_TARGET = COL_NOZZLE + 1;
    private const int COL_ACTUAL = COL_TARGET + 1;
    private const int COL_GROSS = COL_ACTUAL + 1;
    private const int COL_DIFF = COL_GROSS + 1;
    private const int COL_MIN_1T = COL_DIFF + 1;
    private const int COL_MAX_1T = COL_MIN_1T + 1;
    private const int COL_MIN_2T = COL_MAX_1T + 1;
    private const int COL_MAX_2T = COL_MIN_2T + 1;
    private const int COL_STATUS = COL_MAX_2T + 1;
    private const int COL_REJECTED_SW = COL_STATUS + 1;

    public ExcelReportUC()
    {
      InitializeComponent();
      //
      this.excelReportByDateAndTime1.OnSendRequest += ExcelReportByDateAndTime1_OnSendRequest;
      this.excelReportByDateShiftSKU1.OnSendRequestReport += ExcelReportByDateShiftSKU1_OnSendRequestReport;

			//this.glacialList1.
			//item.SubItems[COL_NOZZLE].Text = product.Nozzle_Slot.ToString();
			this.glacialList1.Columns[COL_NOZZLE].Width = 0;

		}

		private void ExcelReportByDateShiftSKU1_OnSendRequestReport(object sender, DateTime dateTime, eShift eShift, eAction eAction)
    {
      this._dateTimeWithTimeFrom = dateTime;
      this._eShift = eShift;
      if (eAction == eAction.PREVIEW)
      {
        this._eRequest = eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_PREVIEW;
      }
      else if (eAction == eAction.REPORT_EXCEL)
      {
        this._eRequest = eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL;
      }
      //
      if (backgroundWorker1.IsBusy == false)
      {
        this.backgroundWorker1.RunWorkerAsync();
      }
    }

    private void ExcelReportByDateAndTime1_OnSendRequest(object sender, DateTime dateTimeWithTimeFrom, DateTime dateTimeWithTimeTo, eAction eAction)
    {
      this._dateTimeWithTimeFrom = dateTimeWithTimeFrom;
      this._dateTimeWithTimeTo = dateTimeWithTimeTo;
      if (eAction == eAction.PREVIEW)
      {
        this._eRequest = eRequest.LOAD_DATA_BY_DATE_TIME_TO_PREVIEW;
      }
      else
      {
        this._eRequest = eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL;
      }
      //
      if (backgroundWorker1.IsBusy == false)
      {
        this.backgroundWorker1.RunWorkerAsync();
      }
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      if (OnBtExitClicked != null)
      {
        OnBtExitClicked(this);
      }
    }

    

    public void UpdateConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
      this.pic_animation.Visible = false;
    }

    



    private void btReportByDateTime_Click(object sender, EventArgs e)
    {
      this.excelReportByDateAndTime1.BringToFront();
			this.btExit1.BringToFront();
      this.btReportByDateShiftSKU.BackColor = Color.White;
			this.btReportByDateShiftSKU.FlatAppearance.BorderColor = Color.Gainsboro;
			this.btReportByDateShiftSKU.ForeColor = Color.Black;

			this.btReportByDateTime.BackColor = Color.FromArgb(0, 71, 160);
			this.btReportByDateTime.FlatAppearance.BorderColor = Color.DodgerBlue;
			this.btReportByDateTime.ForeColor = Color.White;
		}

    private void btReportByDateShiftSKU_Click(object sender, EventArgs e)
    {
      this.excelReportByDateShiftSKU1.BringToFront();
      this.btExit1.BringToFront();
			this.btReportByDateShiftSKU.BackColor = Color.FromArgb(0, 71, 160);
			this.btReportByDateShiftSKU.FlatAppearance.BorderColor = Color.DodgerBlue;
			this.btReportByDateShiftSKU.ForeColor = Color.White;

			this.btReportByDateTime.BackColor = Color.White;
			this.btReportByDateTime.FlatAppearance.BorderColor = Color.Gainsboro;
			this.btReportByDateTime.ForeColor = Color.Black;
		}

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      if ((_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_PREVIEW) || (_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL))
      {
        DataLogDB dataLogDB_sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
				_list_weigher_data_from_database.Clear();
				_list_weigher_data_from_database = dataLogDB_sql.LoadAllByDateFromDateTo(_dateTimeWithTimeFrom, _dateTimeWithTimeTo);

      }
      else if ((this._eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_PREVIEW) || (this._eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL))
      {
        DataLogDB dataLogDB_sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
        if (_eShift == eShift.SHIFT_ALL)
        {
          _list_weigher_data_from_database.Clear();

					_list_weigher_data_from_database.AddRange(dataLogDB_sql.LoadAllByDateShift(_dateTimeWithTimeFrom, (int)(eShift.SHIFT_1)));
          _list_weigher_data_from_database.AddRange(dataLogDB_sql.LoadAllByDateShift(_dateTimeWithTimeFrom, (int)(eShift.SHIFT_2)));
          _list_weigher_data_from_database.AddRange(dataLogDB_sql.LoadAllByDateShift(_dateTimeWithTimeFrom, (int)(eShift.SHIFT_3)));
        }
        else
        {
					_list_weigher_data_from_database.Clear();

					_list_weigher_data_from_database = dataLogDB_sql.LoadAllByDateShift(_dateTimeWithTimeFrom, (int)_eShift);
        }/*if (_eShift == eShift.SHIFT_ALL)*/
      }
      else if (_eRequest == eRequest.START_TO_EXPORT_EXCEL)
      {
        ReportExcel reportExcel = new ReportExcel(_configuration, _output_file_name, _list_weigher_data_from_database);
        object ret = reportExcel.Execute();
        if (ret != null)
        {
          if (ret is bool)
          {
            _IsExportExcel_OK = (bool)(ret);
          }
        }
      }
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {

    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      
      this.timer_delay.Enabled = true;
    }

    private enum eRequest
    {
      LOAD_DATA_BY_DATE_TIME_TO_PREVIEW,
      LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL,
      //
      LOAD_DATA_BY_DATE_SHIFT_TO_PREVIEW,
      LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL,
      //
      START_TO_EXPORT_EXCEL,
      NO_THING,
    }

    private void timer_delay_Tick(object sender, EventArgs e)
    {
      this.timer_delay.Enabled = false;

      if ((_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL) ||
          (_eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL) ||
          (_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_PREVIEW) ||
          (_eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_PREVIEW)
          )
      {
        this.glacialList1.Items.Clear();
        //
        foreach (DataLogType product in _list_weigher_data_from_database)
        {
          GLItem item = new GLItem();
          //---- put data to listview
          item.SubItems[COL_ID].Text = product.CurrentID.ToString();
          item.SubItems[COL_DATETIME].Text = product.DateTime;
          //item.SubItems[COL_NOZZLE].Text = product.Nozzle_Slot.ToString();
          item.SubItems[COL_TARGET].Text = product.Target.ToString();
          item.SubItems[COL_ACTUAL].Text = product.Actual.ToString();
          item.SubItems[COL_GROSS].Text = product.GrossWeight.ToString();
          item.SubItems[COL_DIFF].Text = product.Diff.ToString();
          item.SubItems[COL_MIN_1T].Text = product.LowerLimit_1T.ToString();
          item.SubItems[COL_MAX_1T].Text = product.UpperLimit_1T.ToString();
          item.SubItems[COL_MIN_2T].Text = product.LowerLimit_2T.ToString();
          item.SubItems[COL_MAX_2T].Text = product.UpperLimit_2T.ToString();
          //----------
          if (product.Status == (int)(eWeigerStatus.NG))
          {
            item.SubItems[COL_STATUS].Text = eWeigerStatus.NG.ToString();
            item.BackColor = Color.Red;
            item.ForeColor = Color.White;
          }
          else if (product.Status == (int)(eWeigerStatus.OK))
          {
            item.SubItems[COL_STATUS].Text = eWeigerStatus.OK.ToString();
          }
          else if (product.Status == (int)(eWeigerStatus._1T))
          {
            item.SubItems[COL_STATUS].Text = "1T";
            item.BackColor = Color.Yellow;
            item.ForeColor = Color.Black;
          }
          else if (product.Status == (int)(eWeigerStatus.Over))
          {
            item.SubItems[COL_STATUS].Text = "Over";
            item.BackColor = Color.Orange;
            item.ForeColor = Color.Black;
          }
          else if (product.Status == (int)(eWeigerStatus.CW_Disable))
					{
						item.SubItems[COL_STATUS].Text = "CW Disable";
						item.BackColor = Color.Red;
						item.ForeColor = Color.White;
					}
					else if (product.Status == (int)(eWeigerStatus.MAN))
					{
						item.SubItems[COL_STATUS].Text = "MAN";
						item.BackColor = Color.Red;
						item.ForeColor = Color.White;
					}
					else
          {
          }
          //-----------------------------------------------------------------
          if (product.RejectSW == (int)(eMode.CYLINDER_REJECT_ENABLE))
          {
            item.SubItems[COL_REJECTED_SW].Text = "ENABLE";
          }
          else
          {
            item.SubItems[COL_REJECTED_SW].Text = "DISABLE";
          }
          //
          this.glacialList1.Items.Insert(0, item);
        }
        //_list_weigher_data_from_database
        this.glacialList1.Refresh();
        //
        if ((_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL) || (_eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL))
        {
          DialogResult result = this.folderBrowserDialog1.ShowDialog();
          if (result == DialogResult.OK)
          {
            this._folder_Path = this.folderBrowserDialog1.SelectedPath;
            this.timer1.Enabled = true;
          }
        }/*if ((_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL) || (_eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL))*/
      }/* if ((_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL) || 
          (_eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL) ||
          (_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_PREVIEW) ||
          (_eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_PREVIEW)
          )*/
      else if (_eRequest == eRequest.START_TO_EXPORT_EXCEL)
      {
        if (_IsExportExcel_OK == true)
        {
          MessageBox.Show("Xuất báo cáo thành công.", "Xuất báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          MessageBox.Show("Xuất báo cáo lỗi.", "Xuất báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      this.pic_animation.Visible = false;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if ((_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL) || (_eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL))
      {
        string file_name = "";
        if (_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL)
        {
          file_name = String.Format("Report_{0}_from_{1}_to_{2}", _dateTimeWithTimeFrom.ToString("yyyyMMdd"), _dateTimeWithTimeFrom.ToString("HH_mm_ss"), _dateTimeWithTimeTo.ToString("HH_mm_ss"));
        }
        else
        {
          file_name = String.Format("Report_{0}_Shift{1}", _dateTimeWithTimeFrom.ToString("yyyyMMdd"), (int)(_eShift));
        }
        //
        if (_folder_Path != "")
        {
          _output_file_name = String.Format("{0}\\{1}", _folder_Path, file_name);
          _eRequest = eRequest.START_TO_EXPORT_EXCEL;
          //
          if (this.backgroundWorker1 != null)
          {
            this.backgroundWorker1.RunWorkerAsync();
          }
        }
      }/*if ((_eRequest == eRequest.LOAD_DATA_BY_DATE_TIME_TO_EXPORT_EXCEL) || (_eRequest == eRequest.LOAD_DATA_BY_DATE_SHIFT_TO_EXPORT_EXCEL))*/
    }

    private void btExit1_Click(object sender, EventArgs e)
    {
      if (OnBtExitClicked != null)
      {
        OnBtExitClicked(this);
      }
    }
  }
}
