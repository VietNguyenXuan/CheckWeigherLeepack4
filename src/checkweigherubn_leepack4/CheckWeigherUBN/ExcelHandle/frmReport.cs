using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GlacialComponents.Controls;
namespace CheckWeigherUBN
{
  public partial class frmReport : Form
  {
    private string filePath = "";
    private DateTime fromDateTime = DateTime.Now;
    private DateTime toDateTime = DateTime.Now;
    private ConfigurationTypes _configuration;

    private const int COL_ID = 0;
    private const int COL_DATETIME = COL_ID + 1;
    private const int COL_BARCODE = COL_DATETIME + 1;
    private const int COL_DESCRIPTION = COL_BARCODE + 1;
    private const int COL_TARGET = COL_DESCRIPTION + 1;
    private const int COL_ACTUAL = COL_TARGET + 1;
    private const int COL_DIFF = COL_ACTUAL + 1;
    private const int COL_STATUS = COL_DIFF + 1;
    private const int COL_REJECTED_SW = COL_STATUS + 1;
    //
    private bool _IsWaitingDeactiveControl = false;
    private bool bIsAddFinish = false;
    private GLItem selectedItem = null;

    //
    private eLoadFile _eLoadFile = eLoadFile.TO_EXPORT_EXCEL_FILE;
    private enum eLoadFile
    {
      TO_PREVIEW,
      TO_EXPORT_EXCEL_FILE,
    }
    public frmReport(ConfigurationTypes configuration)
    {
      InitializeComponent();
      _configuration = configuration;
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btExport_Click(object sender, EventArgs e)
    {
      _eLoadFile = eLoadFile.TO_EXPORT_EXCEL_FILE;
      //
      DateTime fromDateTimeTmp = this.dateTimePicker1.Value;
      DateTime toDateTimeTmp = this.dateTimePicker2.Value;
      //
      fromDateTime = new DateTime(fromDateTimeTmp.Year, fromDateTimeTmp.Month, fromDateTimeTmp.Day);
      toDateTime = new DateTime(toDateTimeTmp.Year, toDateTimeTmp.Month, toDateTimeTmp.Day);
      //
      if (this.backgroundWorker1.IsBusy == false)
      {
        string fromDateTimeTmpAsStr = fromDateTimeTmp.ToString("yyyyMMdd");
        string toDateTimeTmpAsStr = toDateTime.ToString("yyyyMMdd");
        if (fromDateTimeTmpAsStr == toDateTimeTmpAsStr)
        {
          saveFileDialog1.FileName = String.Format("Checkweigher_Mespack_Line_{0}.xlsx", fromDateTimeTmp.ToString("yyyyMMdd"));
        }
        else
        {
          saveFileDialog1.FileName = String.Format("Checkweigher_Mespack_Line_{0}_{1}.xlsx", fromDateTimeTmp.ToString("yyyyMMdd"), toDateTimeTmp.ToString("yyyyMMdd"));
        }

        this.saveFileDialog1.ShowDialog();
      }
    }

    private void ReportExcel_OnSendMessage(int percent, string status)
    {
      this.backgroundWorker1.ReportProgress(percent, status);
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      if (_eLoadFile == eLoadFile.TO_EXPORT_EXCEL_FILE)
      {
        //ReportExcel reportExcel = new ReportExcel(_configuration, filePath, fromDateTime, toDateTime);
        //reportExcel.OnSendMessage += ReportExcel_OnSendMessage;
        //reportExcel.Execute();
      }
      else
      {
        //LoadDataToPreview();
      }
    }

    private void LoadDataToPreview()
    {
      try
      {
        /* Load all database from _fromDateTime to _endDateTime */
        DataLogDB sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, true);
        List<DataLogType> list_alldata = new List<DataLogType>();
        //
        DateTime startDateTime = fromDateTime;
        while (startDateTime <= toDateTime)
        {
          List<DataLogType> list_data = sql.LoadAllByDateTime(startDateTime);
          list_alldata.AddRange(list_data);
          //
          startDateTime = startDateTime.AddDays(+1);
        }
        //
        if (list_alldata.Count > 0)
        {
          this.glacialList1.Items.Clear();

          bIsAddFinish = false;

          for (int i = 0; i < list_alldata.Count; i++)
          {
            DataLogType data = list_alldata[i];
            GLItem item = new GLItem();
            item.SubItems[COL_ID].Text = data.id.ToString();
            item.SubItems[COL_DATETIME].Text = data.DateTime;
            item.SubItems[COL_BARCODE].Text = data.Barcode;
            item.SubItems[COL_DESCRIPTION].Text = data.Description.ToString();
            item.SubItems[COL_TARGET].Text = data.Target.ToString();
            item.SubItems[COL_ACTUAL].Text = data.Actual.ToString();
            item.SubItems[COL_DIFF].Text = data.Diff.ToString();
            if (data.Status == 0)
            {
              item.SubItems[COL_STATUS].Text = "NG";
              item.BackColor = Color.Yellow;
            }
            else
            {
              item.SubItems[COL_STATUS].Text = "OK";
            }
            //
            if (data.RejectSW == (int)(eMode.CYLINDER_REJECT_ENABLE))
            {
              item.SubItems[COL_REJECTED_SW].Text = "ENABLE";
            }
            else
            {
              item.SubItems[COL_REJECTED_SW].Text = "DISABLE";
            }

           

            //         private const int COL_ID = 0;
            //private const int COL_DATETIME = COL_ID + 1;
            //private const int COL_BARCODE = COL_DATETIME + 1;
            //private const int COL_DESCRIPTION = COL_BARCODE + 1;
            //private const int COL_TARGET = COL_DESCRIPTION + 1;
            //private const int COL_ACTUAL = COL_TARGET + 1;
            //private const int COL_DIFF = COL_ACTUAL + 1;
            //private const int COL_STATUS = COL_DIFF + 1;


            //
            this.glacialList1.Items.Add(item);
          }/*for (int i = 0; i < _configuration.list_ProductManagement.Count; i++)*/
          bIsAddFinish = true;
          this.glacialList1.Refresh();
        }
      }
      catch
      {
      }
    }


    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (e.UserState is String)
      {
        int percent = e.ProgressPercentage;
        this.lblStatus.Text = String.Format("{0}.. {1}%", (String)e.UserState, percent);
        //
        this.progressBar1.Value = percent;
        this.lblPercent.Text = String.Format("{0}%", percent);
      }
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.btExit.Enabled = true;
    }

    private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
    {
      filePath = saveFileDialog1.FileName;
      this.btExit.Enabled = false;
      timer1.Enabled = true;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      timer1.Enabled = false;
      if (this.backgroundWorker1.IsBusy == false)
      {
        this.backgroundWorker1.RunWorkerAsync();
      }
    }

    private void btPreview_Click(object sender, EventArgs e)
    {
      _eLoadFile = eLoadFile.TO_PREVIEW;
      LoadDataToPreview();
      //if (this.backgroundWorker1.IsBusy == false)
      //{
      //  this.backgroundWorker1.RunWorkerAsync();
      //}
    }
  }
}
