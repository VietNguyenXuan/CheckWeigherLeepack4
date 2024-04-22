using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
  public abstract class DatabaseBaseObject
  {
    public enum eDataBaseProcess
    {
      READ,
      WRITE
    }

    /// <summary>
    /// _template_db_file_name: ex: Configuration
    /// </summary>
    public string _template_db_file_name = "";

    public string _template_path = "";
    public string _database_path = "";

    /// <summary>
    /// _start_datetime_db: DateTime where software created: 2017/06/09
    /// </summary>
    private const string _start_datetime_db = "2019/03/01";

    public DateTime datetime_from_start_software = Convert.ToDateTime(_start_datetime_db);
    public bool _IsNeedCreateNewFile = true;


    public DatabaseBaseObject()
    {
      
    }
    
    public DatabaseBaseObject(string templatePath, string databasePath, bool IsNeedCreateNewFile)
    {
      _template_path = templatePath;
      _database_path = databasePath;
      _IsNeedCreateNewFile = IsNeedCreateNewFile;
    }
    

    /// <summary>
    /// Check datetime to get database
    /// </summary>
    /// <param name="datetime"></param>
    /// <returns></returns>
    public bool CheckDateTimeValid(DateTime datetime)
    {
      bool ret = false;
      DateTime datetime_from_data_log = datetime;
      DateTime datetime_now = DateTime.Now;
      if ((datetime_from_data_log > datetime_now.AddDays(1)) || (datetime_from_data_log < datetime_from_start_software))
      {
        /* do nothing */
      }
      else
      {
        ret = true;
      }
      return ret;
    }

    /// <summary>
    /// Check table name is exists from database file 
    /// </summary>
    public bool CheckTableExists(string db_file_name, string table_name)
    {
      bool IsTableExists = false;
      SQLiteDatabase db = GetSQLiteDatabase(db_file_name, eDataBaseProcess.READ);
      if (db != null)
      {
        IsTableExists = db.CheckTableExists(table_name);
      }
      return (IsTableExists);
    }

    /// <summary>
    /// Create Table if not exists
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool CreateTableIfNotExists(String tableName, Dictionary<String, eSQLiteDatabaseDataType> data)
    {
      bool IsTableExists = false;
      string db_file_name = _template_db_file_name;
      SQLiteDatabase db = GetSQLiteDatabase(db_file_name, eDataBaseProcess.READ);
      if (db != null)
      {
        IsTableExists = db.CreateTableIfNotExists(tableName, data);
      }
      return (IsTableExists);
    }
    /// <summary>
    /// Create Colimn If not exist
    /// </summary>
    /// <param name="table_name"></param>
    /// <param name="column_name"></param>
    /// <param name="SQLiteType"></param>
    /// <returns></returns>
    public bool CreateColumnIfNotExists(string table_name, string column_name, eSQLiteDatabaseDataType SQLiteType)
    {
      bool IsColumnExists = false;
      string db_file_name = _template_db_file_name;
      SQLiteDatabase db = GetSQLiteDatabase(db_file_name, eDataBaseProcess.READ);
      if (db != null)
      {
        object value = null;
        IsColumnExists = db.CreateColumnIfNotExists(table_name, column_name, SQLiteType, value);
      }
      return (IsColumnExists);
    }

    /// <summary>
    /// Create Colimn If not exist
    /// </summary>
    /// <param name="table_name"></param>
    /// <param name="column_name"></param>
    /// <param name="SQLiteType"></param>
    /// <returns></returns>
    public bool CreateColumnIfNotExists(string table_name, string column_name, eSQLiteDatabaseDataType SQLiteType, object value)
    {
      bool IsColumnExists = false;
      string db_file_name = _template_db_file_name;
      SQLiteDatabase db = GetSQLiteDatabase(db_file_name, eDataBaseProcess.READ);
      if (db != null)
      {
        IsColumnExists = db.CreateColumnIfNotExists(table_name, column_name, SQLiteType, value);
      }
      return (IsColumnExists);
    }




    /// <summary>
    /// Check table name is exists from database file 
    /// </summary>
    public bool CheckColumnExists(string db_file_name, string table_name, string column_name)
    {
      bool IsColumnExists = false;
      SQLiteDatabase db = GetSQLiteDatabase(db_file_name, eDataBaseProcess.READ);
      if (db != null)
      {
        IsColumnExists = db.CheckColumnExists(table_name, column_name);
      }
      return (IsColumnExists);
    }

    public virtual bool SetupDatabase()
    {
      bool result = true;
      return result;
    }

    public virtual object LoadAll()
    {
      return null;
    }

    public virtual object Save(object objectTobeSaved)
    {
      return objectTobeSaved;
    }
    /// <summary>
    /// Get & Create SQLiteDatabase 
    /// </summary>
    public virtual SQLiteDatabase GetSQLiteDatabase(string new_db_file_name, eDataBaseProcess eDataBaseProcess)
    {
      SQLiteDatabase db = new SQLiteDatabase();
      string databaseName = "";
      FileInfo templateFile = new FileInfo(String.Format("{0}\\{1}.s3db", _template_path, _template_db_file_name));
      FileInfo newFile = new FileInfo(String.Format("{0}\\{1}.s3db", _database_path, new_db_file_name));
      //
      
      bool IsNeedCreateNewFile = false;
      if (_template_path == "")
      {
        templateFile = new FileInfo(String.Format("{0}\\{1}.s3db", Application.StartupPath, _template_db_file_name));//Application.StartupPath
      }
      if (_database_path == "")
      {
        newFile = new FileInfo(String.Format("{0}\\{1}.s3db", Application.StartupPath, new_db_file_name));
      }
      
      if (newFile.Exists == true)
      {
        databaseName = newFile.FullName;
      }
      else
      {
        IsNeedCreateNewFile = (_IsNeedCreateNewFile == true) && (eDataBaseProcess == eDataBaseProcess.WRITE);
        if (IsNeedCreateNewFile == true)
        {
          try
          {
            if (templateFile.Exists == true)
            {
              /* Create folder if not exists */
              bool ret = FileUtils.CreateNewFolderContainsFileIfnotExists(newFile.FullName);
              //
              if (ret == true)
              {
                File.Copy(templateFile.FullName, newFile.FullName);
                databaseName = newFile.FullName;
              }
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex);
            databaseName = "";
          }
        }/*if (IsNeedCreateNewFile == true)*/
      }
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
    #region Util functions
    /// <summary>
    /// Get value from data row as string
    /// </summary>
    /// <param name="r"></param>
    /// <param name="Key"></param>
    /// <returns></returns>
    public string get_string(DataRow r, string Key)
    {
      string ret = "";
      try
      {
        ret = r[Key].ToString();
      }
      catch
      {
      }
      return ret;
    }
    /// <summary>
    /// Get value from data row as Int
    /// </summary>
    /// <param name="r"></param>
    /// <param name="Key"></param>
    /// <returns></returns>
    public int get_int(DataRow r, string Key)
    {
      int ret = (-1); //Invalid
      try
      {
        ret = string_to_int(r[Key].ToString());
      }
      catch
      {
      }
      return ret;
    }
    /// <summary>
    ///string_to_int: convert string to integer
    ///return (-1) in case of error 
    /// </summary>
    public int string_to_int(string str_value)
    {
      int value = 0;
      if (str_value != "")
      {
        try
        {
          value = (int)(double.Parse(str_value));
        }
        catch
        {
          value = (-1);
        }
      }
      return value;
    }

    /// <summary>
    ///: convert string to double
    ///return (-1) in case of error 
    /// </summary>
    public double string_to_double(string str_value)
    {
      double value = 0;
      if (str_value != "")
      {
        try
        {
          value = double.Parse(str_value);
        }
        catch
        {
          value = (0);
        }
      }
      return value;
    }
    /// <summary>
    ///string_to_bool: convert string to boolean
    /// </summary>
    public bool string_to_bool(string str)
    {
      bool ret = false;
      ret = (str.ToLower() == "true") || (str.ToLower() == "1");
      return ret;
    }
    /// <summary>
    ///bool_to_int: convert boolean to Int
    /// </summary>
    public int bool_to_int(bool boolValue)
    {
      int ret = 0;
      if (boolValue == true)
      {
        ret = 1;
      }
      else
      {
        ret = 0;
      }
      return ret;
    }

    /// <summary>
    ///int_to_bool: convert int to bool
    /// </summary>
    public bool int_to_bool(int value)
    {
      bool ret = (value > 0);
      return ret;
    }

    #endregion
  }
}
