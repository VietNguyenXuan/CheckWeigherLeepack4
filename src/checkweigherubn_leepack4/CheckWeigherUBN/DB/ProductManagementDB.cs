using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
  public class ProductManagementDB : ConfigurationDB
  {

    /// <summary>
    /// Material_Waste
    /// </summary>
    private const string Table_name = "ProductManagement";

    public ProductManagementDB()
      : base()
    {
      _template_db_file_name = "Configuration";
    }



    public override bool SetupDatabase(eTABLE eTable)
    {
      bool result = true;
      if (eTable == eTABLE.ProductManagement)
      {
        //Check and Create Table MailAddessSupport
        string tableName = eTable.ToString();
        bool IsExists = base.CheckTableExists(_template_db_file_name, tableName);
        if (IsExists == false)
        {
          /* Create Table if not exists */
          Dictionary<String, eSQLiteDatabaseDataType> DictionaryDB = ProductManagementType.GetDictionaryDB();
          result = CreateTableIfNotExists(tableName, DictionaryDB);
        }
        else
        {
          /* Use for Update --> Check Columns if Exists */
          Dictionary<String, eSQLiteDatabaseDataType> DictionaryDB = ProductManagementType.GetDictionaryDB();
          foreach (KeyValuePair<String, eSQLiteDatabaseDataType> pair in DictionaryDB)
          {
            result &= CreateColumnIfNotExists(tableName, pair.Key, pair.Value);
          }
        }
      }
      return result;
    }


    private string GetData(DataRow r, ProductManagementType.eProductManagement data)
    {
      string ret = "";
      try
      {
        ret = r[data.ToString()].ToString();
      }
      catch
      {
      }
      return ret;
    }


    /// <summary>
    /// Create data from data_row
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    private ProductManagementType CreateObjectFromDataRow(DataRow r)
    {
      ProductManagementType dataRet = new ProductManagementType()
      {
        id = string_to_int(GetData(r, ProductManagementType.eProductManagement.id)),
        SKU = GetData(r, ProductManagementType.eProductManagement.SKU),
        Description = GetData(r, ProductManagementType.eProductManagement.Description),
        Barcode = GetData(r, ProductManagementType.eProductManagement.Barcode).RemoveSpace(),
        Target = GetData(r, ProductManagementType.eProductManagement.Target).Convert_to_Int(),
        Diff = GetData(r, ProductManagementType.eProductManagement.Diff).Convert_to_Int(),
        LowerLimit_1T = GetData(r, ProductManagementType.eProductManagement.LowerLimit_1T).Convert_to_Double(),
        UpperLimit_1T = GetData(r, ProductManagementType.eProductManagement.UpperLimit_1T).Convert_to_Double(),
        Min1pcs = GetData(r, ProductManagementType.eProductManagement.Min1pcs).Convert_to_Int(),
        Max1pcs = GetData(r, ProductManagementType.eProductManagement.Max1pcs).Convert_to_Int(),
        RowId = GetData(r, ProductManagementType.eProductManagement.RowId).Convert_to_Int(),
        ProductCheckType = GetData(r, ProductManagementType.eProductManagement.ProductCheckType).Convert_to_Int(),
        LowerLimit_2T= GetData(r, ProductManagementType.eProductManagement.LowerLimit_2T).Convert_to_Double(),
        UpperLimit_2T = GetData(r, ProductManagementType.eProductManagement.UpperLimit_2T).Convert_to_Double(),
        FGs = GetData(r, ProductManagementType.eProductManagement.FGs),
        gPackageMaterial = GetData(r, ProductManagementType.eProductManagement.gPackageMaterial).Convert_to_Double(),
      };
      dataRet.LowerLimit_1T = (double)((int)(dataRet.LowerLimit_1T));
      dataRet.UpperLimit_1T = (double)((int)(dataRet.UpperLimit_1T));
      dataRet.LowerLimit_2T = (double)((int)(dataRet.LowerLimit_2T));
      dataRet.UpperLimit_2T = (double)((int)(dataRet.UpperLimit_2T));


      return dataRet;
    }

    /// <summary>
    /// Load All Mail Address Support
    /// </summary>
    /// <returns></returns>
    public override object LoadAll()
    {
      List<ProductManagementType> list_data = new List<ProductManagementType>();
      try
      {
        DataTable recipe;
        String query = "";
        query = String.Format("select * from {0}", Table_name);
        SQLiteDatabase db = GetSQLiteDatabase();
        if (db != null)
        {
          recipe = db.GetDataTable(query);
          foreach (DataRow r in recipe.Rows)
          {
            ProductManagementType data = CreateObjectFromDataRow(r);
            list_data.Add(data);
          }
        }
      }
      catch
      {
      }
      return list_data;
    }

    /// <summary>
    /// Save ProductManagement to database
    /// </summary>
    /// <param name="dataSaved"></param>
    /// <returns></returns>
    public override object Save(object objectTobeSaved)
    {
      ProductManagementType dataSaved = (ProductManagementType)(objectTobeSaved);
      SQLiteDatabase db = GetSQLiteDatabase();
      ProductManagementType dataReturn = dataSaved.Clone();
      bool IsOK = false;
      string id = "";
      //Finding correct id
      DataTable recipe;
      String query = "";
      try
      {
        Dictionary<String, String> data = dataSaved.BuildDictionaryWithValue();
        //
        query = String.Format("select * from {0} where (id = '{1}' OR RowId = '{2}' AND ProductCheckType = '{3}')", Table_name, dataSaved.id, dataSaved.RowId, dataSaved.ProductCheckType);
        recipe = db.GetDataTable(query);
        bool IsHaveData = (recipe.Rows.Count > 0);

        /* Create new */
        if (IsHaveData == false)
        {
          IsOK = db.Insert(Table_name, data);
          if (IsOK == true)
          {
            query = String.Format("select * from {0} ORDER by id DESC limit 1", Table_name);
            recipe = db.GetDataTable(query);
            foreach (DataRow r in recipe.Rows)
            {
              dataReturn = CreateObjectFromDataRow(r);
            }
          }/*if (IsOK == true)*/
        }
        else
        {
          /* Update */
          foreach (DataRow r in recipe.Rows)
          {
            ProductManagementType oldData = CreateObjectFromDataRow(r);
            bool IsEnableSave = oldData.checkDifferent(dataSaved);
            if (IsEnableSave == true)
            {
              id = r["id"].ToString();
              string where = String.Format("id = '{0}'", id);
              IsOK = db.Update(Table_name, data, where);
              //
              if (IsOK == true)
              {
                dataReturn.id = string_to_int(id);
              }
              else
              {
                dataReturn = null;
              }
            }
            else
            {
              IsOK = true;
              dataReturn = oldData;
            }

          }/*foreach (DataRow r in recipe.Rows)*/
        }
      }
      catch
      {
        dataReturn = null;
      }

      return dataReturn;
    }
    /// <summary>
    /// Delete from database
    /// </summary>
    /// <param name="dataDeleted"></param>
    /// <returns></returns>
    public bool Delete(ProductManagementType dataDeleted)
    {
      SQLiteDatabase db = GetSQLiteDatabase();
      bool result = false;
      if (db != null)
      {
        string table_name = Table_name;
        //
        string where = String.Format("id = '{0}'", dataDeleted.id);
        result = db.Delete(table_name, where);
      }/*if (db != null)*/
      return result;
    }
  }
}
