using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;


namespace CheckWeigherUBN
{
  public class ConfigurationDB : DatabaseBaseObject
  {
    public enum eTABLE
    {
      Configuration,
      UserGroup,
      User,
      Communications,
      ProductManagement
    }


    /// <summary>
    /// value: "Configuration"
    /// </summary>
    private string Table_name = "Configuration";

    public ConfigurationDB()
    {
      _template_db_file_name = "Configuration";
      _IsNeedCreateNewFile = false;
      _template_path = "";
      _database_path = "";

    }

    private SQLiteDatabase GetSQLiteDatabase_Configuration()
    {
      SQLiteDatabase db = new SQLiteDatabase();
      string databaseName = "";
      FileInfo configurationFile = new FileInfo(String.Format("{0}\\{1}.s3db", Application.StartupPath, Table_name));
      //
      databaseName = configurationFile.FullName;
      //
      if (databaseName != "")
      {
        db = new SQLiteDatabase(databaseName);
      }
      else
      {
        db = null;
      }
      return db;
    }






    private string GetData(DataRow r, ConfigurationTypes.eConfiguration data)
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
    private ConfigurationTypes CreateObjectFromDataRow(DataRow r)
    {
      ConfigurationTypes dataRet = new ConfigurationTypes()
      {
        id = string_to_int(GetData(r, ConfigurationTypes.eConfiguration.id)),
        DatabasePath = (GetData(r, ConfigurationTypes.eConfiguration.DatabasePath)),
        TemplatePath = (GetData(r, ConfigurationTypes.eConfiguration.TemplatePath)),
        LastProductId = (GetData(r, ConfigurationTypes.eConfiguration.LastProductId)).Convert_to_Int(),
        MaxProductDisplay = (GetData(r, ConfigurationTypes.eConfiguration.MaxProductDisplay)).Convert_to_Int(),
        Permission1 = (GetData(r, ConfigurationTypes.eConfiguration.Permission1)).Convert_to_Int(),
        Permission2 = (GetData(r, ConfigurationTypes.eConfiguration.Permission2)).Convert_to_Int(),
        Permission3 = (GetData(r, ConfigurationTypes.eConfiguration.Permission3)).Convert_to_Int(),
        Permission4 = (GetData(r, ConfigurationTypes.eConfiguration.Permission4)).Convert_to_Int(),
        Permission5 = (GetData(r, ConfigurationTypes.eConfiguration.Permission5)).Convert_to_Int(),
        Permission6 = (GetData(r, ConfigurationTypes.eConfiguration.Permission6)).Convert_to_Int(),
        Permission7 = (GetData(r, ConfigurationTypes.eConfiguration.Permission7)).Convert_to_Int(),
        Permission8 = (GetData(r, ConfigurationTypes.eConfiguration.Permission8)).Convert_to_Int(),
        Permission9 = (GetData(r, ConfigurationTypes.eConfiguration.Permission9)).Convert_to_Int(),
        Permission10 = (GetData(r, ConfigurationTypes.eConfiguration.Permission10)).Convert_to_Int(),
        Permission11 = (GetData(r, ConfigurationTypes.eConfiguration.Permission11)).Convert_to_Int(),
        //
       // ProductCheckType = 0,// (GetData(r, ConfigurationTypes.eConfiguration.ProductCheckType)).Convert_to_Int(),
        //
        PC_Delay_Barcode = (GetData(r, ConfigurationTypes.eConfiguration.PC_Delay_Barcode)).Convert_to_Int(),
        PC_Reject_Time = (GetData(r, ConfigurationTypes.eConfiguration.PC_Reject_Time)).Convert_to_Int(),
        PC_Reject_Time_Box_Conti = (GetData(r, ConfigurationTypes.eConfiguration.PC_Reject_Time_Box_Conti)).Convert_to_Int(),
        PC_Delay_Reject = (GetData(r, ConfigurationTypes.eConfiguration.PC_Delay_Reject)).Convert_to_Int(),
        PC_Reject_Number_Box = (GetData(r, ConfigurationTypes.eConfiguration.PC_Reject_Number_Box)).Convert_to_Int(),
        //
        LineName = (GetData(r, ConfigurationTypes.eConfiguration.LineName)),
        ReportPath = (GetData(r, ConfigurationTypes.eConfiguration.ReportPath)),
      };
      return dataRet;
    }


    /// <summary>
    /// Load All Mail Address Support
    /// </summary>
    /// <returns></returns>
    public override object LoadAll()
    {
      List<ConfigurationTypes> list_data = new List<ConfigurationTypes>();
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
            ConfigurationTypes data = CreateObjectFromDataRow(r);
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
    /// Save Configuration to database
    /// </summary>
    /// <param name="dataSaved"></param>
    /// <returns></returns>
    public override object Save(object objectTobeSaved)
    {
      ConfigurationTypes dataSaved = (ConfigurationTypes)(objectTobeSaved);
      SQLiteDatabase db = GetSQLiteDatabase();
      //

      ConfigurationTypes dataReturn = dataSaved.Clone();
      bool IsOK = false;
      string id = "";
      //Finding correct id
      DataTable recipe;
      String query = "";
      try
      {
        Dictionary<String, String> data = dataSaved.BuildDictionaryWithValue();
        //
        query = String.Format("select * from {0} where id = '{1}'", Table_name, dataSaved.id);
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
            ConfigurationTypes oldData = CreateObjectFromDataRow(r);
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
      catch (Exception e)
      {
        dataReturn = null;
      }

      return dataReturn;
    }



    public virtual SQLiteDatabase GetSQLiteDatabase()
    {
      return GetSQLiteDatabase_Configuration();
    }


    /// <summary>
    /// Setup database when start-up
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="eTable"></param>
    /// <returns></returns>
    public virtual bool SetupDatabase(eTABLE eTable)
    {
      bool result = true;
      if (eTable == eTABLE.Configuration)
      {
        string tableName = eTable.ToString();
        //Check and Create Table 
        bool IsExists = base.CheckTableExists(_template_db_file_name, Table_name);
        if (IsExists == false)
        {
          /* Create Table if not exists */
          Dictionary<String, eSQLiteDatabaseDataType> DictionaryDB = ConfigurationTypes.GetDictionaryDB();
          result = CreateTableIfNotExists(tableName, DictionaryDB);
        }
        else
        {
          /* Use for Update --> Check Columns if Exists */
          Dictionary<String, eSQLiteDatabaseDataType> DictionaryDB = ConfigurationTypes.GetDictionaryDB();
          foreach (KeyValuePair<String, eSQLiteDatabaseDataType> pair in DictionaryDB)
          {
            result &= CreateColumnIfNotExists(tableName, pair.Key, pair.Value, null);
          }
        }
      }
      return result;
    }


  }
}
