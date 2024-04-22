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
  public partial class ExcelReportByDateAndTime : UserControl
  {
    public delegate void SendRequest(object sender, DateTime dateTimeWithTimeFrom, DateTime dateTimeWithTimeTo, eAction eAction);
    public event SendRequest OnSendRequest;
    //
    

    public ExcelReportByDateAndTime()
    {
      InitializeComponent();
      //
      this.Load += ExcelReportByDateAndTime_Load;
    }

    private void ExcelReportByDateAndTime_Load(object sender, EventArgs e)
    {
      ActiveDateAndTimeStartup();
    }

    private void btExportExcelByDateAndTime_Click(object sender, EventArgs e)
    {
      ActiveRequestReport(eAction.REPORT_EXCEL);
    }

    private void btPreview_Click(object sender, EventArgs e)
    {
      ActiveRequestReport(eAction.PREVIEW);
    }

    private void ActiveRequestReport(eAction action)
    {
      string time_from = String.Format("{0}", this.timePickerFrom.Value.ToString("HH:mm:ss"));
      string time_to = String.Format("{0}", this.timePickerTo.Value.ToString("HH:mm:ss"));
      //
      string dt_from = String.Format("{0} {1}", this.dateTimePicker3.Value.Date.ToString("yyyy/MM/dd"), time_from);
      string dt_to = String.Format("{0} {1}", this.dateTimePicker3.Value.Date.ToString("yyyy/MM/dd"), time_to);
      //
      DateTime dateTimeWithTimeFrom = Convert.ToDateTime(dt_from);
      DateTime dateTimeWithTimeTo = Convert.ToDateTime(dt_to);

      //
      if (dateTimeWithTimeTo < dateTimeWithTimeFrom)
      {
				//dateTimeWithTimeTo = dateTimeWithTimeTo.AddDays(+1);
				MessageBox.Show("Vui lòng chọn ngày bắt đầu nhỏ hơn ngày kết thúc !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      else if (dateTimeWithTimeTo == dateTimeWithTimeFrom)
      {
				dateTimeWithTimeTo = dateTimeWithTimeTo.AddHours(+1);
			}  
      //
      if (OnSendRequest != null)
      {
        OnSendRequest(this, dateTimeWithTimeFrom, dateTimeWithTimeTo, action);
      }
    }

   
    private void ActiveDateAndTimeStartup()
    {
      eShift currentShift = Shift.GetShiftFromClock();
      //
      if (currentShift == eShift.SHIFT_1)
      {
        string datetime_from = this.dateTimePicker3.Value.Date.ToString("yyyy/MM/dd 06:00:00");
        timePickerFrom.Value = Convert.ToDateTime(datetime_from);
        string datetime_to = this.dateTimePicker3.Value.Date.ToString("yyyy/MM/dd 14:00:00");
        timePickerTo.Value = Convert.ToDateTime(datetime_to);
        //
        //comboBox_Shift.SelectedIndex = 0;
        //comboBox_SKU.SelectedIndex = 0;
      }
      else if (currentShift == eShift.SHIFT_2)
      {
        string datetime_from = this.dateTimePicker3.Value.Date.ToString("yyyy/MM/dd 14:00:00");
        timePickerFrom.Value = Convert.ToDateTime(datetime_from);
        string datetime_to = this.dateTimePicker3.Value.Date.ToString("yyyy/MM/dd 22:00:00");
        timePickerTo.Value = Convert.ToDateTime(datetime_to);
        //
        //comboBox_Shift.SelectedIndex = 1;
        //comboBox_SKU.SelectedIndex = 0;
      }
      else if (currentShift == eShift.SHIFT_3)
      {
        string datetime_from = this.dateTimePicker3.Value.Date.ToString("yyyy/MM/dd 22:00:00");
        timePickerFrom.Value = Convert.ToDateTime(datetime_from);
        string datetime_to = this.dateTimePicker3.Value.Date.AddDays(+1).ToString("yyyy/MM/dd 06:00:00");
        timePickerTo.Value = Convert.ToDateTime(datetime_to);
        //
        //comboBox_Shift.SelectedIndex = 2;
        //comboBox_SKU.SelectedIndex = 0;
      }
    }

  }
}
