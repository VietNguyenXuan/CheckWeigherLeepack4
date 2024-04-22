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
  public class UserDB : ConfigurationDB
  {

    /// <summary>
    /// Material_Waste
    /// </summary>
    private const string Table_name = "User";

    public UserDB()
      : base()
    {
      _template_db_file_name = "Configuration";
    }



    public override bool SetupDatabase(eTABLE eTable)
    {
      bool result = true;
      if (eTable == eTABLE.User)
      {
        //Check and Create Table MailAddessSupport
        string tableName = eTable.ToString();
        bool IsExists = base.CheckTableExists(_template_db_file_name, tableName);
        if (IsExists == false)
        {
          /* Create Table if not exists */
          Dictionary<String, eSQLiteDatabaseDataType> DictionaryDB = UserType.GetDictionaryDB();
          result = CreateTableIfNotExists(tableName, DictionaryDB);
        }
        else
        {
          /* Use for Update --> Check Columns if Exists */
          Dictionary<String, eSQLiteDatabaseDataType> DictionaryDB = UserType.GetDictionaryDB();
          foreach (KeyValuePair<String, eSQLiteDatabaseDataType> pair in DictionaryDB)
          {
            result &= CreateColumnIfNotExists(tableName, pair.Key, pair.Value);
          }
        }
      }
      return result;
    }


    private string GetData(DataRow r, UserType.eUser data)
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
    private UserType CreateObjectFromDataRow(DataRow r)
    {
      UserType dataRet = new UserType()
      {
        id = string_to_int(GetData(r, UserType.eUser.id)),
        RoleId = (string_to_int(GetData(r, UserType.eUser.RoleId))),
        UserName = (GetData(r, UserType.eUser.UserName)),
        Password = (GetData(r, UserType.eUser.Password)),
      };
      return dataRet;
    }

    /// <summary>
    /// Load All Mail Address Support
    /// </summary>
    /// <returns></returns>
    public override object LoadAll()
    {
      List<UserType> list_data = new List<UserType>();
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
            UserType data = CreateObjectFromDataRow(r);
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
    /// Save User to database
    /// </summary>
    /// <param name="dataSaved"></param>
    /// <returns></returns>
    public override object Save(object objectTobeSaved)
    {
      UserType dataSaved = (UserType)(objectTobeSaved);
      SQLiteDatabase db = GetSQLiteDatabase();
      UserType dataReturn = dataSaved.Clone();
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
            UserType oldData = CreateObjectFromDataRow(r);
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
    public bool Delete(UserType dataDeleted)
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
