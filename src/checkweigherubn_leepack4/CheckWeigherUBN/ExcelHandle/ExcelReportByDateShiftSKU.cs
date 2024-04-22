using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN.ExcelHandle
{
  public partial class ExcelReportByDateShiftSKU : UserControl
  {
    public delegate void SendRequestReport(object sender, DateTime dateTime, eShift eShift, eAction eAction);
    public event SendRequestReport OnSendRequestReport;


    public ExcelReportByDateShiftSKU()
    {
      InitializeComponent();
      this.Load += ExcelReportByDateShiftSKU_Load;
    }

    private void ExcelReportByDateShiftSKU_Load(object sender, EventArgs e)
    {
      ActiveDateAndTimeStartup();
    }


    private void ActiveRequestReport(eAction action)
    {
      try
      {
        string dt_from = String.Format("{0}", this.dateTimePicker4.Value.Date.ToString("yyyy/MM/dd"));
        eShift eShift = eShift.SHIFT_1;
        //
        DateTime dateTime = Convert.ToDateTime(dt_from);
        if (comboBox_Shift.SelectedIndex == (int)(eShift.SHIFT_1) - 1)
        {
          eShift = eShift.SHIFT_1;
        }
        else if (comboBox_Shift.SelectedIndex == (int)(eShift.SHIFT_2) - 1)
        {
          eShift = eShift.SHIFT_2;
        }
        else if (comboBox_Shift.SelectedIndex == (int)(eShift.SHIFT_3) - 1)
        {
          eShift = eShift.SHIFT_3;
        }
        else
        {
          eShift = eShift.SHIFT_ALL;
        }
        if (OnSendRequestReport != null)
        {
          OnSendRequestReport(this, dateTime, eShift, action);
        }
      }
      catch
      {
      }
    }


    private void ActiveDateAndTimeStartup()
    {
      eShift currentShift = Shift.GetShiftFromClock();
      comboBox_Shift.SelectedIndex = (int)(currentShift) - 1;
    }

    private void btPreview_Click(object sender, EventArgs e)
    {
      ActiveRequestReport(eAction.PREVIEW);
    }

    private void btExport_Click(object sender, EventArgs e)
    {
      ActiveRequestReport(eAction.REPORT_EXCEL);
    }
  }
}
