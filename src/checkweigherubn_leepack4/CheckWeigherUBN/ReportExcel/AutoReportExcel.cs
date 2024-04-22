using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace CheckWeigherUBN
{
  
  public partial class frmMain
  {
    private Timer _reportExcel_timer = new Timer();

    private void SetupReportExcel()
    {
      _reportExcel_timer.Interval = 1000;//60000;
      _reportExcel_timer.Tick += _reportExcel_timer_Tick;
    }

    private void _reportExcel_timer_Tick(object sender, EventArgs e)
    {
      this._reportExcel_timer.Enabled = false;
      if (this.backgroundWorker2ReportExcel.IsBusy == false)
      {
        this.backgroundWorker2ReportExcel.RunWorkerAsync();
      }
      else
      {
        this._reportExcel_timer.Enabled = true;
      }
    }

    private List<string> FilterFGs(List<DataLogType> list_weigher_data)
    {

      List<string> list_FGs = new List<string>();
      string previous_FGs = "";
      for (int i = 0; i < list_weigher_data.Count; i++)
      {
        DataLogType weigher_data = list_weigher_data[i];
        if (weigher_data.FGs != previous_FGs)
        {
          list_FGs.Add(weigher_data.FGs);
          //
          previous_FGs = weigher_data.FGs;
        }/*if (weigher_data.FGs != FGs)*/
      }
      return list_FGs;
    }

    //private int ExportExcelWorkSheet_Data(ExcelWorksheet worksheet, List<DataLogType> list_weigher_data)
    //{
    //  int end_row = 1;
    //  if (worksheet != null)
    //  {
    //    /* Load all database from _fromDateTime to _endDateTime */
    //    //Start to expport excel 
    //    int row = 40;
    //    int column = 2;
    //    int id = 1;

    //    //Start to export weigher-data
    //    string FGs = "";
    //    string DateAsStr = "";
    //    string Desciption = "";
    //    int Shift = 0;
    //    double TargetWeight = 0;
    //    string from_time = "";
    //    string end_time = "";
    //    //
    //    for (int i = 0; i < list_weigher_data.Count; i++)
    //    {
    //      DataLogType dataLog = list_weigher_data[i];
    //      //
    //      try
    //      {
    //        Color backgroundColor = GetBackGroundColor(dataLog.Status);
    //        Color foreColor = GetForeColor(dataLog.Status);
    //        string statusAsStr = ConvertStatus(dataLog.Status);
    //        //
    //        DateTime dt = Convert.ToDateTime(dataLog.DateTime);

    //        SetCell_SolidBackground(worksheet, row, column++, id, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dt.ToShortDateString(), ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        DateTime time = dt;
    //        SetCell_SolidBackground(worksheet, row, column++, time, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, "Yujeng", ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.Description, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.FGs, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.ShiftId, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.Nozzle_Slot, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, statusAsStr, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        if (dataLog.RejectSW == (int)(eMode.CYLINDER_REJECT_DISABLE))
    //        {
    //          SetCell_SolidBackground(worksheet, row, column++, "DISABLE", ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        }
    //        else
    //        {
    //          SetCell_SolidBackground(worksheet, row, column++, "ENABLE", ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        }
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.Diff, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.Actual, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.GrossWeight, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.gPackageMaterial, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.Target, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        //
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.LowerLimit_2T, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.LowerLimit_1T, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.UpperLimit_2T, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        SetCell_SolidBackground(worksheet, row, column++, dataLog.UpperLimit_1T, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
    //        ////TargetWeight
    //        //SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
    //        //worksheet.Cells[row, column++].Formula = "=Fomulation!B2";

    //        ////Spec.
    //        ////SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
    //        ////worksheet.Cells[row, column++].Formula = "=Fomulation!B2";
    //        ////
    //        //SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
    //        //worksheet.Cells[row, column++].Formula = "=Fomulation!E4";
    //        ////
    //        //SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
    //        //worksheet.Cells[row, column++].Formula = "=Fomulation!E3";
    //        ////
    //        //SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
    //        //worksheet.Cells[row, column++].Formula = "=Fomulation!E2";
    //        //
    //        if (FGs == "")
    //        {
    //          FGs = dataLog.FGs;
    //        }
    //        if (Desciption == "")
    //        {
    //          Desciption = dataLog.Description;
    //        }
    //        if (DateAsStr == "")
    //        {
    //          DateAsStr = dt.ToShortDateString();
    //        }
    //        if (Shift == 0)
    //        {
    //          Shift = dataLog.ShiftId;
    //        }
    //        if (TargetWeight == 0)
    //        {
    //          TargetWeight = dataLog.Target;
    //        }

    //        if (from_time == "")
    //        {
    //          from_time = dt.ToString("HH:mm:ss");
    //        }

    //        end_time = dt.ToString("HH:mm:ss");
    //        //reset for next
    //        row++;
    //        column = 2;
    //        id++;
    //      }
    //      catch
    //      {
    //      }
    //    }/*for (int i = 0; i < list_alldata.Count; i++)*/
    //    //
    //    end_row = row - 1;
    //    //
    //    worksheet.Cells["C6"].Value = FGs;
    //    worksheet.Cells["C7"].Value = Desciption;
    //    worksheet.Cells["C8"].Value = TargetWeight;
    //    worksheet.Cells["C9"].Value = DateAsStr;
    //    worksheet.Cells["C10"].Value = Shift;
    //    worksheet.Cells["C12"].Value = from_time;
    //    worksheet.Cells["D12"].Value = end_time;
    //  }
    //  return end_row;
    //}


    //private void ExportExcelWorkSheet_Formula(ExcelWorksheet worksheet, int end_row)
    //{
    //  if (worksheet != null)
    //  {
    //    worksheet.Cells["B3"].Formula = String.Format("=AVERAGE('Line Charts'!M40:M{0})", end_row);
    //    worksheet.Cells["B4"].Formula = String.Format("=MIN('Line Charts'!M40:M{0})", end_row);
    //    worksheet.Cells["B5"].Formula = String.Format("=MAX('Line Charts'!M40:M{0})", end_row);
    //    worksheet.Cells["B6"].Formula = String.Format("=STDEV('Line Charts'!M40:M{0})", end_row);
    //  }
    //}

    //private bool ExportExcelDataLog(List<DataLogType> list_weigher_data, FileInfo newFile, FileInfo templateFile)
    //{
    //  bool ret = false;
    //  ExcelPackage package = new ExcelPackage(newFile, templateFile);
    //  ExcelWorksheet worksheet;

    

    //  bool IsExitLoop = false;
    //  int end_Row = 0;
    //  for (int i = 1; (i <= package.Workbook.Worksheets.Count) && (IsExitLoop == false); i++)
    //  {
    //    worksheet = package.Workbook.Worksheets[i];
    //    if (i == 1)
    //    {
    //      end_Row = ExportExcelWorkSheet_Data(worksheet, list_weigher_data);
    //    }
    //    else if (i == 2)
    //    {
    //      ExportExcelWorkSheet_Formula(worksheet, end_Row);
    //      IsExitLoop = true;
    //    }
    //  }
    


    //  package.Workbook.Properties.Title = String.Format("UBN CheckWeigher Report");
    //  package.Workbook.Properties.Author = "UBN CheckWeigher";
    //  package.Workbook.Properties.Comments = String.Format("UBN CheckWeigher Report");

    //  // set some extended property values
    //  package.Workbook.Properties.Company = "VuLe Technology";

    //  // set some custom property values
    //  package.Workbook.Properties.SetCustomPropertyValue("Checked by", "VuLe");
    //  package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "VuLe");
    //  // save our new workbook and we are done!
    
    //  package.Save();
    //  //
    //  ret = (bool)(true);
    //  return ret;
    //}


    private void backgroundWorker2ReportExcel_DoWork(object sender, DoWorkEventArgs e)
    {
      //eShift eCurShift = Shift.GetShiftFromClock();
      //DateTime datetime = Utils.GetDateTimeFromClockByShift();
      ////
      ////if ()
      //DataLogDB dataLogDB_sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
      //List<DataLogType> _list_weigher_data_from_database = dataLogDB_sql.LoadAllByDateShift(datetime, (int)(eCurShift));
      //try
      //{
      //  FileInfo templateFile;
      //  FileInfo newFile;

      //  //string template_file_name = "";
      //  templateFile = new FileInfo(String.Format("{0}\\ReportTemplate.xlsx", _configuration.TemplatePath));
      //  if (templateFile.Exists == true)
      //  {
      //    List<string> list_FGs = FilterFGs(_list_weigher_data_from_database);
      //  }
      //}
      //catch
      //{

      //}
    }

    private void backgroundWorker2ReportExcel_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {

    }

    private void backgroundWorker2ReportExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this._reportExcel_timer.Enabled = true;
    }
  }
}
