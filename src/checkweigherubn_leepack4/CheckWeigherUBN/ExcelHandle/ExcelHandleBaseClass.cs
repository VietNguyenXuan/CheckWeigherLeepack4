#define USING_PM_EWO_NEW_TEMPLATE
#define ENABLE_MERGE_LASS_EVENT
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
  public class ExcelHandleBaseClass
  {
    public delegate void SendMessage(int percent, string status);
    public event SendMessage OnSendMessage;


    protected ConfigurationTypes _configuration;

    //
    protected FileInfo _templateFile = null;


    public string excelbarcodePassword = "";//"Password@12345";




    public ExcelHandleBaseClass()
    {
    }
    public ExcelHandleBaseClass(ConfigurationTypes configuration)
    {
      _configuration = configuration;
    }


    /// <summary>
    /// Get Package from excel file
    /// </summary>
    /// <param name="excelFile"></param>
    /// <returns></returns>
    public ExcelPackage GetExcelPackage(FileInfo excelFile, string password)
    {
      ExcelPackage package = new ExcelPackage(excelFile);
      if (password != "")
      {
        try
        {
          package = new ExcelPackage(excelFile, excelbarcodePassword);
        }
        catch
        {
          package = new ExcelPackage(excelFile);
        }
      }/*if (IsNeedPassword == true)*/
      else
      {
        package = new ExcelPackage(excelFile);
      }
      return package;
    }

    /// <summary>
    /// Set password for sheet
    /// </summary>
    /// <param name="worksheet"></param>
    public void SetPassword(ExcelWorksheet worksheet, string password)
    {
      if (password != "")
      {
        worksheet.Protection.SetPassword(password);
        //
        worksheet.Protection.IsProtected = true;
        worksheet.Protection.AllowEditObject = false;
        worksheet.Protection.AllowSelectUnlockedCells = false;
        worksheet.Protection.AllowSelectLockedCells = false;
      }
    }


    public FileInfo CreateNewFile(string output_file)
    {
      FileInfo newFile;
      newFile = new FileInfo(String.Format("{0}", output_file));
      // If any file exists in this directory, then delete it
      if (newFile.Exists)
      {
        try
        {
          /* Make sure user not open file */
          newFile.Delete(); // ensures we create a new workbook
          newFile = new FileInfo(String.Format("{0}", output_file));
        }
        catch
        {
          newFile = new FileInfo(String.Format("{0}_{1}", output_file, DateTime.Now.ToString("HH:mm:ss:ffff")));
        }
      }
      return newFile;
    }



    public virtual Color getBackgroundColor(int idx)
    {
      Color color = Color.FromArgb(219, 229, 241);
      if (idx % 2 == 0)
      {
        color = Color.FromArgb(184, 204, 228);
      }
      return color;
    }


    public virtual Color getForeColor(int idx)
    {
      Color color = Color.Black;
      if (idx % 2 == 0)
      {
        color = Color.Black;
      }
      return color;
    }


    public void SetCell_SolidBackground(ExcelWorksheet worksheet, int row, int column, object value, OfficeOpenXml.Style.ExcelHorizontalAlignment Alignment, Color background_color)
    {
      try
      {
        var cell = worksheet.Cells[row, column];
        var fill = cell.Style.Fill;
        fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        fill.BackgroundColor.SetColor(background_color);
        cell.Value = value;
        cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);
        cell.Style.HorizontalAlignment = Alignment;
        cell.Style.Font.Color.SetColor(Color.White);
        //cell.AutoFitColumns();
      }
      catch
      {
      }
    }

    public void SetCell_SolidBackground(ExcelWorksheet worksheet, int row, int column, object value, OfficeOpenXml.Style.ExcelHorizontalAlignment Alignment)
    {
      try
      {
        var cell = worksheet.Cells[row, column];
        var fill = cell.Style.Fill;
        //
        Color defaultBackgroundColor = getBackgroundColor(row);
        Color defaultForeColor = getForeColor(row);
        //
        fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        fill.BackgroundColor.SetColor(defaultBackgroundColor);
        //
        cell.Value = value;
        cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);
        cell.Style.HorizontalAlignment = Alignment;
        cell.Style.Font.Color.SetColor(defaultForeColor);
        //cell.AutoFitColumns();
      }
      catch
      {
      }
    }

    public void SetCell_SolidBackground(ExcelWorksheet worksheet, int row, int column, object value, OfficeOpenXml.Style.ExcelHorizontalAlignment Alignment, Color background_color, Color ForeColor)
    {
      try
      {
        var cell = worksheet.Cells[row, column];
        var fill = cell.Style.Fill;
        fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        fill.BackgroundColor.SetColor(background_color);
        cell.Value = value;
        cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);
        cell.Style.HorizontalAlignment = Alignment;
        cell.Style.Font.Color.SetColor(ForeColor);
        //cell.AutoFitColumns();
      }
      catch
      {
      }
    }
    /// <summary>
    /// Copy from source file to tmp
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dest"></param>
    /// <returns></returns>
    public FileInfo CopyFile(string src, string dest)
    {
      FileInfo fileInfoDest = null;
      try
      {
        //src.CopyTo(dest, true);
        File.Copy(src, dest, true);
        //
        fileInfoDest = new FileInfo(dest);
      }
      catch
      {
      }
      return fileInfoDest;
    }



    public string GetCell_Value(ExcelWorksheet worksheet, string cell)
    {
      string ret = "";
      try
      {
        if (worksheet.Cells[cell].Value != null)
        {
          try
          {
            ret = worksheet.Cells[cell].Value.ToString().Trim();
          }
          catch
          {
            ret = "";
          }
        }
        else
        {
          ret = "";
        }
      }
      catch
      {
        ret = "";
      }
      return ret;
    }

    public string GetCell_Value(ExcelWorksheet worksheet, int row, int column)
    {
      string ret = "";
      try
      {
        if ((column > 0) && (row > 0))
        {
          if (worksheet.Cells[row, column].Value != null)
          {
            try
            {
              ret = worksheet.Cells[row, column].Value.ToString().Trim();
            }
            catch
            {
              ret = "";
            }
          }
          else
          {
            ret = "";
          }
        }
      }
      catch
      {
        ret = "";
      }
      return ret;
    }

    public void ReportProgress(int percent, string status)
    {
      if (OnSendMessage != null)
      {
        OnSendMessage(percent, status);
      }
    }



    public string Getcell(string col, int row)
    {
      return (String.Format("{0}{1}", col, row));
    }


    public void SetCellWithBorder(ExcelWorksheet worksheet, int row, int column, object value, OfficeOpenXml.Style.ExcelHorizontalAlignment Alignment)
    {
      try
      {
        worksheet.Cells[row, column].Value = value;
        worksheet.Cells[row, column].Style.HorizontalAlignment = Alignment;
        worksheet.Cells[row, column].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);
      }
      catch
      {
      }
      //worksheet.Cells[row, column].AutoFitColumns(20, 40);
    }

    public void SetCellWithFomula(ExcelWorksheet worksheet, string cell, string Formula, OfficeOpenXml.Style.ExcelHorizontalAlignment Alignment)
    {
      try
      {
        worksheet.Cells[cell].Formula = Formula;
        worksheet.Cells[cell].Style.HorizontalAlignment = Alignment;
        worksheet.Cells[cell].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);
      }
      catch
      {
      }
      //worksheet.Cells[row, column].AutoFitColumns(20, 40);
    }


    /// <summary>
    /// Loading barcode --> import from excel file
    /// </summary>
    /// <returns></returns>
    public virtual object Execute()
    {
      object ret = null;
      return ret;
    }


    public virtual object Execute(FileInfo templateFile, ExcelWorksheet worksheet, int startRow)
    {
      object ret = null;
      return ret;
    }


  }



}
