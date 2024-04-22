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
  public class UserGroupDB : ConfigurationDB
  {

    /// <summary>
    /// Material_Waste
    /// </summary>
    private const string Table_name = "UserGroup";

    public UserGroupDB()
      : base()
    {
      _template_db_file_name = "Configuration";
    }



    public override bool SetupDatabase(eTABLE eTable)
    {
      bool result = true;
      if (eTable == eTABLE.UserGroup)
      {
        //Check and Create Table MailAddessSupport
        string tableName = eTable.ToString();
        bool IsExists = base.CheckTableExists(_template_db_file_name, tableName);
        if (IsExists == false)
        {
          /* Create Table if not exists */
          Dictionary<String, eSQLiteDatabaseDataType> DictionaryDB = UserGroupType.GetDictionaryDB();
          result = CreateTableIfNotExists(tableName, DictionaryDB);
        }
        else
        {
          /* Use for Update --> Check Columns if Exists */
          Dictionary<String, eSQLiteDatabaseDataType> DictionaryDB = UserGroupType.GetDictionaryDB();
          foreach (KeyValuePair<String, eSQLiteDatabaseDataType> pair in DictionaryDB)
          {
            result &= CreateColumnIfNotExists(tableName, pair.Key, pair.Value);
          }
        }
      }
      return result;
    }


    private string GetData(DataRow r, UserGroupType.eUserGroup data)
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
    private UserGroupType CreateObjectFromDataRow(DataRow r)
    {
      UserGroupType dataRet = new UserGroupType()
      {
        id = string_to_int(GetData(r, UserGroupType.eUserGroup.id)),
        RoleId = (string_to_int(GetData(r, UserGroupType.eUserGroup.RoleId))),
        roleEnable = string_to_int(GetData(r, UserGroupType.eUserGroup.roleEnable)),
        Password = (GetData(r, UserGroupType.eUserGroup.Password)),
      };
      return dataRet;
    }

    /// <summary>
    /// Load All Mail Address Support
    /// </summary>
    /// <returns></returns>
    public override object LoadAll()
    {
      List<UserGroupType> list_data = new List<UserGroupType>();
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
            UserGroupType data = CreateObjectFromDataRow(r);
            if ((data.RoleId == (int)eUserGroupRole.Admin) || (data.RoleId == (int)eUserGroupRole.OP))
            {
              //Add to list 
              list_data.Add(data);
            }
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
      UserGroupType dataSaved = (UserGroupType)(objectTobeSaved);
      SQLiteDatabase db = GetSQLiteDatabase();
      UserGroupType dataReturn = dataSaved.Clone();
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
            UserGroupType oldData = CreateObjectFromDataRow(r);
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

  }
}
