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
namespace CheckWeigherUBN
{
	public partial class ReportExcel : ExcelHandleBaseClass
	{
		private object StartToExportExcel_Alarms()
		{
			object ret = null;
			try
			{
				FileInfo templateFile;
				FileInfo newFile;



				//string template_file_name = "";
				templateFile = new FileInfo(String.Format("{0}\\AlarmReportTemplate.xlsx", _configuration.TemplatePath));

				if (templateFile.Exists == true)
				{



					newFile = new FileInfo(String.Format("{0}", _out_file_path)); //xoas duoi
					// If any file exists in this directory, then delete it
					if (newFile.Exists)
					{
						newFile.Delete(); // ensures we create a new workbook
						newFile = new FileInfo(String.Format("{0}", _out_file_path)); //xoas duoi
					}
			//		bool ret = false;
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
							//	end_Row = ExportExcelWorkSheet_Data(worksheet, list_weigher_data);
							int row = 9;
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
							for (int idx= 0; idx < _list_alarms.Count; idx++)
							{
								AlarmType dataLog = _list_alarms[idx];
								try
								{
									int index = 0;
									string dateTime = "";
									int error_code = 0;
									string error_description = "";

									Color backgroundColor = Color.White;
									Color foreColor = Color.Black;
									//
									index = (idx + 1);
									dateTime = dataLog.DateTime;
									error_code = dataLog.AlarmCode;
									error_description = dataLog.Description;
									//
									SetCell_SolidBackground(worksheet, row, column++, index, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
									SetCell_SolidBackground(worksheet, row, column++, dateTime, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
									SetCell_SolidBackground(worksheet, row, column++, error_code, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);
									SetCell_SolidBackground(worksheet, row, column++, error_description, ExcelHorizontalAlignment.Center, backgroundColor, foreColor);

									//
									column = 2;
									row++;
									if(idx == 13)
									{

										int mm = 0;
									}
								}
								catch
								{
								}
							}/*for (int i = 0; i < list_alldata.Count; i++)*/
						
							IsExitLoop = true;
						}
					}
















					this.ReportProgress(100, String.Format("Please wait, generating report excel file for  done..."));














					package.Workbook.Properties.Title = String.Format("UBN CheckWeigher Report");
					package.Workbook.Properties.Author = "I-Soft";
					package.Workbook.Properties.Comments = String.Format("UBN CheckWeigher Alarm Report");

					// set some extended property values
					package.Workbook.Properties.Company = "I-Soft Technology";

					// set some custom property values
					package.Workbook.Properties.SetCustomPropertyValue("Checked by", "I-Soft");
					package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "I-Soft");
					// save our new workbook and we are done!
					this.ReportProgress(100, String.Format("Generating report excel file done..."));
					package.Save();
					//
					ret = (bool)(true);

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
	}
}
