
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace CheckWeigherUBN
{
  public partial class ReportExcel : ExcelHandleBaseClass
  {
    
    /// <summary>
    /// Không bao gồm .xlsx
    /// </summary>
    private string _out_file_path = "";

    private DateTime _fromDateTime;
    private DateTime _endDateTime;
    //
    List<DataLogType> _list_weigher_data_all = new List<DataLogType>();
    private List<AlarmType> _list_alarms = new List<AlarmType>();

		public enum eReportType
    {
      WeigherDatalog,
      Alarms
    }

    private eReportType _eReportType = eReportType.WeigherDatalog;

		public ReportExcel(ConfigurationTypes configuration, string output_file, List<DataLogType> list_data) :
      base(configuration)
    {
      _configuration = configuration;
      _out_file_path = output_file;
      //
      _list_weigher_data_all = list_data;
      this._eReportType = eReportType.WeigherDatalog;

		}

		


	 public ReportExcel(ConfigurationTypes configuration, string output_file, List<AlarmType> alarms) :
			base(configuration)
		{
			_configuration = configuration;
			_out_file_path = output_file;
      //
      _list_alarms = alarms;
			this._eReportType = eReportType.Alarms;
		}
		/// <summary>
		/// Export excel for formula
		/// </summary>
		/// <param name="worksheet"></param>
		/// <param name="end_row"></param>
		private void ExportExcelWorkSheet_Formula(ExcelWorksheet worksheet, int end_row)
    {
      if (worksheet != null)
      {
        worksheet.Cells["B3"].Formula = String.Format("=AVERAGE('Line Charts'!M40:M{0})", end_row);
        worksheet.Cells["B4"].Formula = String.Format("=MIN('Line Charts'!M40:M{0})", end_row);
        worksheet.Cells["B5"].Formula = String.Format("=MAX('Line Charts'!M40:M{0})", end_row);
        worksheet.Cells["B6"].Formula = String.Format("=STDEV('Line Charts'!M40:M{0})", end_row);
      }
    }

    /// <summary>
    /// Get Background color
    /// </summary>
    /// <param name="Status"></param>
    /// <returns></returns>
    private Color GetBackGroundColor(int Status)
    {
      Color color = Color.White;

      if (Status == (int)(eWeigerStatus.NG))
      {
        color = Color.Red;
      }
      else if (Status == (int)(eWeigerStatus.OK))
      {
        color = Color.White;
      }
      else if (Status == (int)(eWeigerStatus._1T))
      {
        color = Color.Yellow;
      }
      else if (Status == (int)(eWeigerStatus.Over))
			{
				color = Color.Orange;
      }
			else if ((Status == (int)(eWeigerStatus.CW_Disable)) || (Status == (int)(eWeigerStatus.MAN)))
      {
				color = Color.Red;
			}
				return color;
    }

    /// <summary>
    /// Get Fore color
    /// </summary>
    /// <param name="Status"></param>
    /// <returns></returns>
    private Color GetForeColor(int Status)
    {
      Color color = Color.White;

      if (Status == (int)(eWeigerStatus.NG))
      {
        color = Color.White;
      }
      else if (Status == (int)(eWeigerStatus.OK))
      {
        color = Color.Black;
      }
      else if (Status == (int)(eWeigerStatus._1T))
      {
        color = Color.Black;
      }
      else if (Status == (int)(eWeigerStatus.Over))
      {
        color = Color.Black;
      }
			else if ((Status == (int)(eWeigerStatus.CW_Disable) ) || (Status == (int)(eWeigerStatus.MAN)))
			{
				color = Color.White;
			}
			return color;
    }


    /// <summary>
    /// Get Fore color
    /// </summary>
    /// <param name="Status"></param>
    /// <returns></returns>
    private string ConvertStatus(int Status)
    {
      string ret = "";

      if (Status == (int)(eWeigerStatus.NG))
      {
        ret = "NG";
      }
      else if (Status == (int)(eWeigerStatus.OK))
      {
        ret = "OK";
      }
      else if (Status == (int)(eWeigerStatus._1T))
      {
        ret = "1T";
      }
      else if (Status == (int)(eWeigerStatus.Over))
      {
        ret = "Over";
      }
			else if (Status == (int)(eWeigerStatus.CW_Disable) )
			{
				ret = "CW Disable";
			}
			else if (Status == (int)(eWeigerStatus.MAN))
			{
				ret = "MAN";
			}
			return ret;
    }


    private int ExportExcelWorkSheet_Data(ExcelWorksheet worksheet, List<DataLogType> list_weigher_data)
    {
      int end_row = 1;
      if (worksheet != null)
      {
        /* Load all database from _fromDateTime to _endDateTime */
        //Start to expport excel 
        int row = 40;
        int column = 2;
        int id = 1;
       
        //Start to export weigher-data
        string FGs = "";
        string DateAsStr = "";
        string Desciption = "";
        int Shift = 0;
        double TargetWeight = 0;
        string from_time = "";
        string end_time = "";
        //
        for (int i = 0; i < list_weigher_data.Count; i++)
        {
          DataLogType dataLog = list_weigher_data[i];
          //
          try
          {
            Color backgroundColor = GetBackGroundColor(dataLog.Status);
            Color foreColor = GetForeColor(dataLog.Status);
            string statusAsStr = ConvertStatus(dataLog.Status);
            //
            DateTime dt = Convert.ToDateTime(dataLog.DateTime);

            SetCell_SolidBackground(worksheet, row, column++, id, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dt.ToShortDateString(), ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            DateTime time = dt;
            SetCell_SolidBackground(worksheet, row, column++, time, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, _configuration.LineName, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.Description, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.FGs, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.ShiftId, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            //SetCell_SolidBackground(worksheet, row, column++, dataLog.Nozzle_Slot, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, statusAsStr, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            if (dataLog.RejectSW == (int)(eMode.CYLINDER_REJECT_DISABLE))
            {
              SetCell_SolidBackground(worksheet, row, column++, "DISABLE", ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            }
            else
            {
              SetCell_SolidBackground(worksheet, row, column++, "ENABLE", ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            }
            SetCell_SolidBackground(worksheet, row, column++, dataLog.Diff, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.Actual, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.GrossWeight, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.gPackageMaterial, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.Target, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            //
            SetCell_SolidBackground(worksheet, row, column++, dataLog.LowerLimit_2T, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.LowerLimit_1T, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.UpperLimit_2T, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            SetCell_SolidBackground(worksheet, row, column++, dataLog.UpperLimit_1T, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
            ////TargetWeight
            //SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
            //worksheet.Cells[row, column++].Formula = "=Fomulation!B2";

            ////Spec.
            ////SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
            ////worksheet.Cells[row, column++].Formula = "=Fomulation!B2";
            ////
            //SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
            //worksheet.Cells[row, column++].Formula = "=Fomulation!E4";
            ////
            //SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
            //worksheet.Cells[row, column++].Formula = "=Fomulation!E3";
            ////
            //SetCell_SolidBackground(worksheet, row, column, "", ExcelHorizontalAlignment.Center);
            //worksheet.Cells[row, column++].Formula = "=Fomulation!E2";
            //
            if (FGs == "")
            {
              FGs = dataLog.FGs;
            }
            if (Desciption == "")
            {
              Desciption = dataLog.Description;
            }
            if (DateAsStr == "")
            {
              DateAsStr = dt.ToShortDateString();
            }
            if (Shift == 0)
            {
              Shift = dataLog.ShiftId;
            }
            if (TargetWeight == 0)
            {
              TargetWeight = dataLog.Target;
            }

            if (from_time == "")
            {
              from_time = dt.ToString("HH:mm:ss");
            }

            end_time = dt.ToString("HH:mm:ss");
            //reset for next
            row++;
            column = 2;
            id++;
          }
          catch
          {
          }
        }/*for (int i = 0; i < list_alldata.Count; i++)*/
        //
        end_row = row - 1;
        //
        worksheet.Cells["C6"].Value = FGs;
        worksheet.Cells["C7"].Value = Desciption;
        worksheet.Cells["C8"].Value = TargetWeight;
        worksheet.Cells["C9"].Value = DateAsStr;
        worksheet.Cells["C10"].Value = Shift;
        worksheet.Cells["C12"].Value = from_time;
        worksheet.Cells["D12"].Value = end_time;
      }
      return end_row;
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

    private bool ExportExcelDataLog(List<DataLogType> list_weigher_data, FileInfo newFile, FileInfo templateFile)
    {
      bool ret = false;
      ExcelPackage package = new ExcelPackage(newFile, templateFile);
      ExcelWorksheet worksheet;

      this.ReportProgress(0, string.Format("Please wait, creating new file from template..."));

      bool IsExitLoop = false;
      int end_Row = 0;
      for (int i = 1; (i <= package.Workbook.Worksheets.Count) && (IsExitLoop == false); i++)
      {
        worksheet = package.Workbook.Worksheets[i];
        if (i == 1)
        {
          end_Row = ExportExcelWorkSheet_Data(worksheet, list_weigher_data);
        }
        else if (i == 2)
        {
          ExportExcelWorkSheet_Formula(worksheet, end_Row);
          IsExitLoop = true;
        }
      }
      this.ReportProgress(100, String.Format("Please wait, generating report excel file for Changeover done..."));


      package.Workbook.Properties.Title = String.Format("UBN CheckWeigher Report");
      package.Workbook.Properties.Author = "UBN CheckWeigher";
      package.Workbook.Properties.Comments = String.Format("UBN CheckWeigher Report");

      // set some extended property values
      package.Workbook.Properties.Company = "VuLe Technology";

      // set some custom property values
      package.Workbook.Properties.SetCustomPropertyValue("Checked by", "VuLe");
      package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "VuLe");
      // save our new workbook and we are done!
      this.ReportProgress(100, String.Format("Generating report excel file done..."));
      package.Save();
      //
      ret = (bool)(true);
      return ret;
    }

    private object StartToExportExcel()
    {
			object ret = null;
      if (this._eReportType == eReportType.WeigherDatalog)
      {
        ret = StartToExportExcel_WeigherDataLog();
      }
      else
      {
				ret = StartToExportExcel_Alarms();
			}
      return ret;
    }


		private object StartToExportExcel_WeigherDataLog()
    {
      object ret = null;
      try
      {
        FileInfo templateFile;
        FileInfo newFile;

        

        //string template_file_name = "";
        templateFile = new FileInfo(String.Format("{0}\\ReportTemplate.xlsx", _configuration.TemplatePath));

        if (templateFile.Exists == true)
        {
          List<string> list_FGs = FilterFGs(_list_weigher_data_all);


          if (list_FGs.Count > 0)
          {
            for (int FGs_idx = 0; FGs_idx < list_FGs.Count; FGs_idx++)
            {
              string FGs = list_FGs[FGs_idx];

              newFile = new FileInfo(String.Format("{0}_{1}.xlsx", _out_file_path, FGs));
              // If any file exists in this directory, then delete it
              if (newFile.Exists)
              {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(String.Format("{0}_{1}.xlsx", _out_file_path, FGs));
              }

              List<DataLogType> list_weigher_data = _list_weigher_data_all.FindAll(x => x.FGs == FGs);
              ret = ExportExcelDataLog(list_weigher_data, newFile, templateFile);

             
            }
          }/*if (list_FGs.Count > 0)*/
          else
          {
            newFile = new FileInfo(String.Format("{0}.xlsx", _out_file_path));
            // If any file exists in this directory, then delete it
            if (newFile.Exists)
            {
              newFile.Delete(); // ensures we create a new workbook
              newFile = new FileInfo(String.Format("{0}.xlsx", _out_file_path));
            }
            ret = ExportExcelDataLog(_list_weigher_data_all, newFile, templateFile);
          }

        }
        else
        {
          ret = (bool)(false);
        }
      }
      catch (Exception e)
      {
        string mess = e.ToString();
        ret = (bool)(false);
        this.ReportProgress(0, String.Format("{0}", "Xuất báo cáo lỗi..."));
      }
      return ret;
    }

    public override object Execute()
    {
      object ret = null;
      ret = StartToExportExcel();
      return ret;
    }
  }
}
