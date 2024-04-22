using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
namespace CheckWeigherUBN
{
  public class ExcelProductImport: ExcelHandleBaseClass
  {
    private string _file_path = "";

    public ExcelProductImport(ConfigurationTypes configuration, string file_path): base(configuration)
    {
      _configuration = configuration;
     _file_path = file_path;
    }


    private double GetDataAsDouble(string input_value)
    {
      double value = input_value.Convert_to_Double();//(int)((input_value.Convert_to_Double() * 10) / 10); //(int)(double((input_value.Convert_to_Double() * 10) / 10));
      return value;
    }
    //
    private object ReadProductFromExcelFor_Bottle()
    {
      object ret = null;
      List<ProductManagementType> list_Barcode = new List<ProductManagementType>();
      Utils utils = new Utils();
      FileInfo existingFile = new FileInfo(String.Format("{0}", _file_path));
      if (existingFile.Exists == true)
      {
        try
        {
          ExcelPackage package = GetExcelPackage(existingFile, "");
          ExcelWorkbook workBook = package.Workbook;
          if (workBook != null)
          {
            if (workBook.Worksheets.Count > 0)
            {
              /* Check if barcode network*/
              bool IsExit = false;
              for (int i = 1; (i <= workBook.Worksheets.Count) && (IsExit == false); i++)
              {
                try
                {
                  ExcelWorksheet currentWorksheet = workBook.Worksheets[i];
                  //string G31 = GetCell_Value(currentWorksheet, "G31").Trim();
                  //if (G31 == "Case weight (trọng lượng chai theo giới hạn T)")
                  if (currentWorksheet.Name == "Master_Data")
                  {
                    IsExit = true;
                    int start_row = 4;

                    int COL_FGs = 1;
                    int COL_SKU = 2;
                    int COL_Description = 3;

                    int COL_LowerLimit_1T = 14;
                    int COL_Target = 15;
                    int COL_UpperLimit_1T = 16;

                    int COL_LowerLimit_2T = 17;
                    int COL_UpperLimit_2T = 19;

                    int COL_gPackageMaterial = 6; //Viet dev

                

                    //Find start-row
                    bool is_exit_start_row = false;
                    for (int row = 0; (row <= currentWorksheet.Dimension.End.Row) && (is_exit_start_row == false); row++)
                    {
                      string SKU = GetCell_Value(currentWorksheet, row, COL_SKU);
                      if (SKU.Trim().ToLower() == "sku")
                      {
                        is_exit_start_row = true;
                        start_row = row + 1;
                      }
                    }
                      //
                    int nCountToExit = 0;
                    List<ProductManagementType> list_ProductManagement = new List<ProductManagementType>();
                    //
                    for (int row = start_row; (row <= currentWorksheet.Dimension.End.Row) && (nCountToExit < 100); row++)
                    {
                      string SKU = GetCell_Value(currentWorksheet, row, COL_SKU);
                      if (SKU != "")
                      {
                        string Description = GetCell_Value(currentWorksheet, row, COL_Description);
                        string Barcode =  "";
                        string FGs = GetCell_Value(currentWorksheet, row, COL_FGs);
                        string LowerLimit_1T = GetCell_Value(currentWorksheet, row, COL_LowerLimit_1T);
                        string Target = GetCell_Value(currentWorksheet, row, COL_Target);
                        string UpperLimit_1T = GetCell_Value(currentWorksheet, row, COL_UpperLimit_1T);
                        string Min1pcs = "0";
                        string Max1pcs = "0";
                        string Diff = "0";
                        string LowerLimit_2T = GetCell_Value(currentWorksheet, row, COL_LowerLimit_2T);
                        string UpperLimit_2T = GetCell_Value(currentWorksheet, row, COL_UpperLimit_2T);
                        string gPackageMaterial = GetCell_Value(currentWorksheet, row, COL_gPackageMaterial);
                        //
                        int factor = 1;

												ProductManagementType product = new ProductManagementType()
                        {
                          SKU = SKU,
                          Description = Description,
                          Barcode = Barcode,
													LowerLimit_1T = ((int)(Math.Round(GetDataAsDouble(LowerLimit_1T)))),//(int)(double((LowerLimit_1T.Convert_to_Double() * 10)/10)),
													Target = (int)Math.Round(GetDataAsDouble(Target)),//(int)(Target.Convert_to_Double() * factor),
                          UpperLimit_1T = (int)Math.Round(GetDataAsDouble(UpperLimit_1T)),//(int)(UpperLimit_1T.Convert_to_Double() * factor),
                          Min1pcs = 0,//GetDataAsInt(Target),// (int)(Min1pcs.Convert_to_Double() * factor),
                          Max1pcs = 0,//GetDataAsInt(Target),//(int)(Max1pcs.Convert_to_Double() * factor),
                          Diff = 0,// GetDataAsInt(Target),//(int)(Diff.Convert_to_Double() * factor),
                          RowId = row,
                          ProductCheckType = (int)(eProductCheck.BOTTLE),
                          FGs = FGs,
                          LowerLimit_2T = (int)Math.Round(GetDataAsDouble(LowerLimit_2T)),//(int)(LowerLimit_2T.Convert_to_Double() * factor),
                          UpperLimit_2T =(int) Math.Round(GetDataAsDouble(UpperLimit_2T)),//(int)(UpperLimit_2T.Convert_to_Double() * factor),
                          gPackageMaterial = (int)Math.Round(GetDataAsDouble(gPackageMaterial))
                        };
                        list_ProductManagement.Add(product);
                        //
                        if (SKU == "")
                        {
                          nCountToExit++;
                        }
                      }
                    }
                    if (list_ProductManagement.Count > 0)
                    {
                      //delte all RowId = 0;
                      ProductManagementDB sqlProductManagementDB = new ProductManagementDB();
                      _configuration.list_ProductManagement = _configuration.list_ProductManagement.FindAll(x => x.ProductCheckType == (int)(eProductCheck.BOTTLE));
                      //
                      //1. Search from excel file 
                      for (int idx = 0; idx < list_ProductManagement.Count; idx++)
                      {
                        ProductManagementType product = list_ProductManagement[idx];
                        ProductManagementType productFromDatabase = _configuration.list_ProductManagement.FindLast(x => (x.RowId == product.RowId));
                        if (productFromDatabase != null)
                        {
                          /* found from database --> update */
                          product.id = productFromDatabase.id;
                          object productRet = sqlProductManagementDB.Save(product);
                        }
                        else
                        {
                          /* not found from database */
                          object productRet = sqlProductManagementDB.Save(product);
                        }
                      }/*for (int idx = 0; idx < list_ProductManagement.Count; idx++)*/

                      //2. Search from database
                      for (int idx = 0; idx < _configuration.list_ProductManagement.Count; idx++)
                      {
                        ProductManagementType productFromDatabase = _configuration.list_ProductManagement[idx];
                        ProductManagementType productFromExcel = list_ProductManagement.FindLast(x => (x.RowId == productFromDatabase.RowId));
                        if (productFromExcel != null)
                        {
                          /* found --> do nothing  */
                        }
                        else
                        {
                          /* not found from excel --> delete */
                          sqlProductManagementDB.Delete(productFromDatabase);
                        }
                      }

                      //3. Reload all from database
                      object objRet = sqlProductManagementDB.LoadAll();
                      if (objRet is List<ProductManagementType>)
                      {
                        List<ProductManagementType> list_ProductManagementAll = (List<ProductManagementType>)(objRet);
                        if (list_ProductManagementAll.Count > 0)
                        {
                          _configuration.list_ProductManagement = list_ProductManagementAll;
                        }
                      }
                    }

                  }
                }
                catch (Exception ex)
                {
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
      return ret;
    }

    private object ReadProductFromExcelFor_Bottle_Old()
    {
      object ret = null;
      List<ProductManagementType> list_Barcode = new List<ProductManagementType>();
      Utils utils = new Utils();
      FileInfo existingFile = new FileInfo(String.Format("{0}", _file_path));
      if (existingFile.Exists == true)
      {
        try
        {
          ExcelPackage package = GetExcelPackage(existingFile, "");
          ExcelWorkbook workBook = package.Workbook;
          if (workBook != null)
          {
            if (workBook.Worksheets.Count > 0)
            {
              /* Check if barcode network*/
              bool IsExit = false;
              for (int i = 1; (i <= workBook.Worksheets.Count) && (IsExit == false); i++)
              {
                try
                {
                  ExcelWorksheet currentWorksheet = workBook.Worksheets[i];
                  //string G31 = GetCell_Value(currentWorksheet, "G31").Trim();
                  //if (G31 == "Case weight (trọng lượng chai theo giới hạn T)")
                  if (i == 2)
                  {
                    IsExit = true;
                    int start_row = 7; //int start_row = 7;

                    int COL_FGs = 5;
                    int COL_SKU = 3;
                    int COL_Description = 4;

                    int COL_LowerLimit_1T = 7;
                    int COL_Target = 8;
                    int COL_UpperLimit_1T = 9;

                    int COL_LowerLimit_2T = 11;
                    int COL_UpperLimit_2T = 13;

                    //int COL_gPackageMaterial = 17;
                    int COL_gPackageMaterial = 17; //Viet dev

                    //          int COL_FGs = 1;
                    //int COL_SKU = 2;
                    //          int COL_Description = 3;

                    //          int COL_LowerLimit_1T = 14;
                    //          int COL_Target = 15;
                    //          int COL_UpperLimit_1T = 16;

                    //          int COL_LowerLimit_2T = 17;
                    //          int COL_UpperLimit_2T = 19;

                    //          //int COL_gPackageMaterial = 17;
                    //int COL_gPackageMaterial = 6; //Viet dev


                    //Find start-row
                    bool is_exit_start_row = false;
                    for (int row = 0; (row <= currentWorksheet.Dimension.End.Row) && (is_exit_start_row == false); row++)
                    {
                      string SKU = GetCell_Value(currentWorksheet, row, COL_SKU);
                      if (SKU.Trim().ToLower() == "sku")
                      {
                        is_exit_start_row = true;
                        start_row = row + 1;
                      }
                    }
                    //
                    int nCountToExit = 0;
                    List<ProductManagementType> list_ProductManagement = new List<ProductManagementType>();
                    //
                    for (int row = start_row; (row <= currentWorksheet.Dimension.End.Row) && (nCountToExit < 100); row++)
                    {
                      string SKU = GetCell_Value(currentWorksheet, row, COL_SKU);
                      if (SKU != "")
                      {
                        string Description = GetCell_Value(currentWorksheet, row, COL_Description);
                        string Barcode = "";
                        string FGs = GetCell_Value(currentWorksheet, row, COL_FGs);
                        string LowerLimit_1T = GetCell_Value(currentWorksheet, row, COL_LowerLimit_1T);
                        string Target = GetCell_Value(currentWorksheet, row, COL_Target);
                        string UpperLimit_1T = GetCell_Value(currentWorksheet, row, COL_UpperLimit_1T);
                        string Min1pcs = "0";
                        string Max1pcs = "0";
                        string Diff = "0";
                        string LowerLimit_2T = GetCell_Value(currentWorksheet, row, COL_LowerLimit_2T);
                        string UpperLimit_2T = GetCell_Value(currentWorksheet, row, COL_UpperLimit_2T);
                        string gPackageMaterial = GetCell_Value(currentWorksheet, row, COL_gPackageMaterial);
                        //
                        int factor = 1;

                        ProductManagementType product = new ProductManagementType()
                        {
                          SKU = SKU,
                          Description = Description,
                          Barcode = Barcode,
                          LowerLimit_1T = ((int)(Math.Round(GetDataAsDouble(LowerLimit_1T)))),//(int)(double((LowerLimit_1T.Convert_to_Double() * 10)/10)),
                          Target = (int)Math.Round(GetDataAsDouble(Target)),//(int)(Target.Convert_to_Double() * factor),
                          UpperLimit_1T = (int)Math.Round(GetDataAsDouble(UpperLimit_1T)),//(int)(UpperLimit_1T.Convert_to_Double() * factor),
                          Min1pcs = 0,//GetDataAsInt(Target),// (int)(Min1pcs.Convert_to_Double() * factor),
                          Max1pcs = 0,//GetDataAsInt(Target),//(int)(Max1pcs.Convert_to_Double() * factor),
                          Diff = 0,// GetDataAsInt(Target),//(int)(Diff.Convert_to_Double() * factor),
                          RowId = row,
                          ProductCheckType = (int)(eProductCheck.BOTTLE),
                          FGs = FGs,
                          LowerLimit_2T = (int)Math.Round(GetDataAsDouble(LowerLimit_2T)),//(int)(LowerLimit_2T.Convert_to_Double() * factor),
                          UpperLimit_2T = (int)Math.Round(GetDataAsDouble(UpperLimit_2T)),//(int)(UpperLimit_2T.Convert_to_Double() * factor),
                          gPackageMaterial = (int)Math.Round(GetDataAsDouble(gPackageMaterial))
                        };
                        list_ProductManagement.Add(product);
                        //
                        if (SKU == "")
                        {
                          nCountToExit++;
                        }
                      }
                    }
                    if (list_ProductManagement.Count > 0)
                    {
                      //delte all RowId = 0;
                      ProductManagementDB sqlProductManagementDB = new ProductManagementDB();
                      _configuration.list_ProductManagement = _configuration.list_ProductManagement.FindAll(x => x.ProductCheckType == (int)(eProductCheck.BOTTLE));
                      //
                      //1. Search from excel file 
                      for (int idx = 0; idx < list_ProductManagement.Count; idx++)
                      {
                        ProductManagementType product = list_ProductManagement[idx];
                        ProductManagementType productFromDatabase = _configuration.list_ProductManagement.FindLast(x => (x.RowId == product.RowId));
                        if (productFromDatabase != null)
                        {
                          /* found from database --> update */
                          product.id = productFromDatabase.id;
                          object productRet = sqlProductManagementDB.Save(product);
                        }
                        else
                        {
                          /* not found from database */
                          object productRet = sqlProductManagementDB.Save(product);
                        }
                      }/*for (int idx = 0; idx < list_ProductManagement.Count; idx++)*/

                      //2. Search from database
                      for (int idx = 0; idx < _configuration.list_ProductManagement.Count; idx++)
                      {
                        ProductManagementType productFromDatabase = _configuration.list_ProductManagement[idx];
                        ProductManagementType productFromExcel = list_ProductManagement.FindLast(x => (x.RowId == productFromDatabase.RowId));
                        if (productFromExcel != null)
                        {
                          /* found --> do nothing  */
                        }
                        else
                        {
                          /* not found from excel --> delete */
                          sqlProductManagementDB.Delete(productFromDatabase);
                        }
                      }

                      //3. Reload all from database
                      object objRet = sqlProductManagementDB.LoadAll();
                      if (objRet is List<ProductManagementType>)
                      {
                        List<ProductManagementType> list_ProductManagementAll = (List<ProductManagementType>)(objRet);
                        if (list_ProductManagementAll.Count > 0)
                        {
                          _configuration.list_ProductManagement = list_ProductManagementAll;
                        }
                      }
                    }

                  }
                }
                catch (Exception ex)
                {
                }
              }
            }
          }
        }
        catch
        {
        }
      }
      return ret;
    }


    private object ReadProductFromExcelFor_Box()
    {
      object ret = null;
      List<ProductManagementType> list_Barcode = new List<ProductManagementType>();
      Utils utils = new Utils();
      FileInfo existingFile = new FileInfo(String.Format("{0}", _file_path));
      if (existingFile.Exists == true)
      {
        try
        {
          ExcelPackage package = GetExcelPackage(existingFile, "");
          ExcelWorkbook workBook = package.Workbook;
          if (workBook != null)
          {
            if (workBook.Worksheets.Count > 0)
            {
              /* Check if barcode network*/
              bool IsExit = false;
              for (int i = 1; (i <= workBook.Worksheets.Count) && (IsExit == false); i++)
              {
                try
                {
                  ExcelWorksheet currentWorksheet = workBook.Worksheets[i];
                  string A29 = GetCell_Value(currentWorksheet, "A29");
                  if (A29 == "Messpack")
                  {
                    IsExit = true;
                    int start_row = 29;
                    int COL_SKU = 3;
                    int COL_Description = 4;
                    int COL_Barcode = 10;
                    int COL_LowerLimit = 11;
                    int COL_Target = 12;
                    int COL_UpperLimit = 13;
                    int COL_Min1pcs = 15;
                    int COL_Max1pcs = 16;
                    int COL_Diff = 14;

                    int nCountToExit = 0;
                    List<ProductManagementType> list_ProductManagement = new List<ProductManagementType>();
                    //
                    for (int row = start_row; (row <= currentWorksheet.Dimension.End.Row) && (nCountToExit < 100); row++)
                    {
                      string SKU = GetCell_Value(currentWorksheet, row, COL_SKU);
                      if (SKU != "")
                      {
                        string Description = GetCell_Value(currentWorksheet, row, COL_Description);
                        string Barcode = GetCell_Value(currentWorksheet, row, COL_Barcode);
                        string LowerLimit = GetCell_Value(currentWorksheet, row, COL_LowerLimit);
                        string Target = GetCell_Value(currentWorksheet, row, COL_Target);
                        string UpperLimit = GetCell_Value(currentWorksheet, row, COL_UpperLimit);
                        string Min1pcs = GetCell_Value(currentWorksheet, row, COL_Min1pcs);
                        string Max1pcs = GetCell_Value(currentWorksheet, row, COL_Max1pcs);
                        string Diff = GetCell_Value(currentWorksheet, row, COL_Diff);
                        //
                        ProductManagementType product = new ProductManagementType()
                        {
                          SKU = SKU,
                          Description = Description,
                          Barcode = Barcode,
                          LowerLimit_1T = (int)(LowerLimit.Convert_to_Double() * 1000),
                          Target = (int)(Target.Convert_to_Double() * 1000),
                          UpperLimit_1T = (int)(UpperLimit.Convert_to_Double() * 1000),
                          Min1pcs = (int)(Min1pcs.Convert_to_Double() * 1000),
                          Max1pcs = (int)(Max1pcs.Convert_to_Double() * 1000),
                          Diff = (int)(Diff.Convert_to_Double() * 1000),
                          RowId = row,
                          ProductCheckType = 0
                        };
                        list_ProductManagement.Add(product);
                        //
                        if (SKU == "")
                        {
                          nCountToExit++;
                        }
                      }
                    }
                    if (list_ProductManagement.Count > 0)
                    {
                      ProductManagementDB sqlProductManagementDB = new ProductManagementDB();
                      _configuration.list_ProductManagement = _configuration.list_ProductManagement.FindAll(x => x.ProductCheckType == (int)(eProductCheck.BOX));

                      //1. Search from excel file 
                      for (int idx = 0; idx < list_ProductManagement.Count; idx++)
                      {
                        ProductManagementType product = list_ProductManagement[idx];
                        ProductManagementType productFromDatabase = _configuration.list_ProductManagement.FindLast(x => (x.RowId == product.RowId));
                        if (productFromDatabase != null)
                        {
                          /* found from database */
                          product.id = productFromDatabase.id;
                          object productRet = sqlProductManagementDB.Save(product);
                        }
                        else
                        {
                          /* not found from database */
                          object productRet = sqlProductManagementDB.Save(product);
                        }
                      }/*for (int idx = 0; idx < list_ProductManagement.Count; idx++)*/

                      //2. Search from database
                      for (int idx = 0; idx < _configuration.list_ProductManagement.Count; idx++)
                      {
                        ProductManagementType productFromDatabase = _configuration.list_ProductManagement[idx];
                        ProductManagementType productFromExcel = list_ProductManagement.FindLast(x => (x.RowId == productFromDatabase.RowId));
                        if (productFromExcel != null)
                        {
                          /* found --> do nothing  */
                        }
                        else
                        {
                          /* not found from excel --> delete */
                          sqlProductManagementDB.Delete(productFromDatabase);
                        }
                      }

                      //3. Reload all from database
                      object objRet = sqlProductManagementDB.LoadAll();
                      if (objRet is List<ProductManagementType>)
                      {
                        List<ProductManagementType> list_ProductManagementAll = (List<ProductManagementType>)(objRet);
                        if (list_ProductManagementAll.Count > 0)
                        {
                          _configuration.list_ProductManagement = list_ProductManagement;
                        }
                      }



                      //int mm = 0;
                      ////delte all RowId = 0;
                      //ProductManagementDB sqlProductManagementDB = new ProductManagementDB();
                      //for (int idx = 0; idx < _configuration.list_ProductManagement.Count; idx++)
                      //{
                      //  ProductManagementType product = _configuration.list_ProductManagement[idx];
                      //  if (product.RowId == 0)
                      //  {
                      //    sqlProductManagementDB.Delete(product);
                      //  }
                      //}
                      ////
                      //_configuration.list_ProductManagement.Clear();
                      ////
                      //for (int idx = 0; idx < list_ProductManagement.Count; idx++)
                      //{
                      //  ProductManagementType product = list_ProductManagement[idx];
                      //  object productRet = sqlProductManagementDB.Save(list_ProductManagement[idx]);
                      //  if (productRet != null)
                      //  {
                      //    if (productRet is ProductManagementType)
                      //    {
                      //      product = (ProductManagementType)(productRet);
                      //      _configuration.list_ProductManagement.Add(product);
                      //    }
                      //  }
                      //}
                      ////
                      //ret = _configuration.list_ProductManagement;
                    }
                    
                  }
                }
                catch
                {
                }
              }
            }
          }
        }
        catch
        {
        }
      }
      return ret;
    }

    public override object Execute()
    {
      object ret = null;
      //if (_configuration.ProductCheckType == (int)(eProductCheck.BOX))
      //{
      //  ret = ReadProductFromExcelFor_Box();
      //}
      //else if (_configuration.ProductCheckType == (int)(eProductCheck.BOTTLE))
      //{
      //  ret = ReadProductFromExcelFor_Bottle();
      //}
      ReadProductFromExcelFor_Bottle();
      return ret;
    }

  }
  
}
