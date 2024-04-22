using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;


namespace CheckWeigherUBN
{
  
  public class SQLiteDatabase
    {
        String dbConnection;

        /// <summary>
        ///     Default Constructor for SQLiteDatabase Class.
        /// </summary>
        public SQLiteDatabase()
        {
            //dbConnection = "Data Source=database\\recipes.s3db";
            //dbConnection = "Data Source=database\\Line1.s3db";
            //dbConnection = "Data Source=database\\LineCounter.s3db";
        }

        /// <summary>
        ///     Single Param Constructor for specifying the DB file.
        /// </summary>
        /// <param name="inputFile">The File containing the DB</param>
        public SQLiteDatabase(String inputFile)
        {
            dbConnection = String.Format("Data Source={0}", inputFile);
        }

        /// <summary>
        ///     Single Param Constructor for specifying advanced connection options.
        /// </summary>
        /// <param name="connectionOpts">A dictionary containing all desired options and their values</param>
        public SQLiteDatabase(Dictionary<String, String> connectionOpts)
        {
            String str = "";
            foreach (KeyValuePair<String, String> row in connectionOpts)
            {
                str += String.Format("{0}={1}; ", row.Key, row.Value);
            }
            str = str.Trim().Substring(0, str.Length - 1);
            dbConnection = str;
        }

        /// <summary>
        ///     Allows the programmer to run a query against the Database.
        /// </summary>
        /// <param name="sql">The SQL to run</param>
        /// <returns>A DataTable containing the result set.</returns>
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                cnn.Open();
                SQLiteCommand mycommand = new SQLiteCommand(cnn);
                mycommand.CommandText = sql;
                SQLiteDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }
        
        /// <summary>
        /// {
        /// 
        /// }
        ///     Allows the programmer to interact with the database for purposes other than a query.
        /// </summary>
        /// <param name="sql">The SQL to be run.</param>
        /// <returns>An Integer containing the number of rows updated.</returns>
        public int ExecuteNonQuery(string sql)
        {
            SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            int rowsUpdated = mycommand.ExecuteNonQuery();
            cnn.Close();
            return rowsUpdated;
        }

        /// <summary>
        ///     Allows the programmer to retrieve single items from the DB.
        /// </summary>
        /// <param name="sql">The query to run.</param>
        /// <returns>A string.</returns>
        public string ExecuteScalar(string sql)
        {
            SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            object value = mycommand.ExecuteScalar();
            cnn.Close();
            if (value != null)
            {
                return value.ToString();
            }
            return "";
        }
      /// <summary>
        ///     Allows the programmer to easily update rows in the DB.
        /// </summary>
        /// <param name="tableName">The table to update.</param>
        /// <param name="data">A dictionary containing Column names and their new values.</param>
        /// <param name="where">The where clause for the update statement.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool CheckTableExists(String tableName)
        {
          Boolean returnCode = true;
          DataTable tables;
          try
          {
            tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
            bool IsFound = false;
            for (int i = 0; i < tables.Rows.Count && (IsFound == false); i++)
            {
              DataRow table = tables.Rows[i];
              IsFound = (table["NAME"].ToString() == tableName);
            }
            returnCode = IsFound;
          }
          catch
          {
            returnCode = false;
          }
          return returnCode;
        }

    /// <summary>
    /// Create new table if not Exists
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool CreateTableIfNotExists(String tableName, Dictionary<String, eSQLiteDatabaseDataType> data)
    {
      String vals = "";
      Boolean returnCode = true;
      if (data.Count >= 1)
      {
        foreach (KeyValuePair<String, eSQLiteDatabaseDataType> val in data)
        {
          string value_str = (getType(val.Value)).ToString();
          vals += String.Format(" {0} {1},", val.Key.ToString(), value_str.ToString());
        }
        vals = vals.Substring(0, vals.Length - 1);
      }
      try
      {
        string query = String.Format("CREATE TABLE IF NOT EXISTS {0} ({1});", tableName, vals);
        this.ExecuteNonQuery(query);
      }
      catch
      {
        returnCode = false;
      }
      return returnCode;
    }


    private string getType(eSQLiteDatabaseDataType SQLiteType)
    {
      string SQLiteTypeAsStr = SQLiteType.ToString();
      if (SQLiteType == eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY_AUTOINCREMENT)
      {
        SQLiteTypeAsStr = "INTEGER PRIMARY KEY AUTOINCREMENT";
      }
      else if (SQLiteType == eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY)
      {
        SQLiteTypeAsStr = "INTEGER PRIMARY KEY";
      }
      return SQLiteTypeAsStr;
    }

    /// <summary>
    /// Check if column is exists or not
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="Column"></param>
    /// <returns></returns>
    public bool CheckColumnExists(String tableName, string Column)
    {
      Boolean returnCode = true;
      try
      {
        string query = String.Format("select * from {0}", tableName);
        DataTable recipe = this.GetDataTable(query);
        returnCode = recipe.Columns.Contains(Column);
      }
      catch
      {
        returnCode = false;
      }
      return returnCode;
    }

    public bool CreateColumnIfNotExists(string tableName, string Column, eSQLiteDatabaseDataType SQLiteType, object value)
    {
      bool IsColumnExists = CheckColumnExists(tableName, Column);
      if (IsColumnExists == true)
      {
        /* do nothing */
      }
      else
      {
        String vals = "";
        IsColumnExists = true;
        try
        {
          string SQLiteTypeAsStr = (getType(SQLiteType)).ToString();
          string query = String.Format("ALTER TABLE {0} ADD COLUMN {1} {2};", tableName, Column, SQLiteTypeAsStr);
          this.ExecuteNonQuery(query);
          //
          if (value != null)
          {
            string value_as_string = (string)(value.ToString());
            query = String.Format("Update {0} set {1} = {2};", tableName, Column, value_as_string);
            try
            {
              this.ExecuteNonQuery(query);
            }
            catch
            {
            }
          }
        }
        catch
        {
          IsColumnExists = false;
        }
        //

      }
      return IsColumnExists;
    }

        /// <summary>
        ///     Allows the programmer to easily update rows in the DB.
        /// </summary>
        /// <param name="tableName">The table to update.</param>
        /// <param name="data">A dictionary containing Column names and their new values.</param>
        /// <param name="where">The where clause for the update statement.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Update(String tableName, Dictionary<String, String> data, String where)
        {
            String vals = "";
            Boolean returnCode = true;
            if (data.Count >= 1)
            {
                foreach (KeyValuePair<String, String> val in data)
                {
                  string value_str = val.Value.ToString();
                  if (value_str.Contains("\"") == true)
                  {
                    value_str = Regex.Replace(value_str, "\"", "\'");
                    vals += String.Format(" {0} = \"{1}\",", val.Key.ToString(), value_str.ToString());
                    //vals += String.Format(" {0} = \"{1}\",", val.Key.ToString(), val.Value.ToString());
                  }
                  else
                  {
                    vals += String.Format(" {0} = \"{1}\",", val.Key.ToString(), val.Value.ToString());
                  }
                }
                vals = vals.Substring(0, vals.Length - 1);
            }
            try
            {
              string query = String.Format("update {0} set {1} where {2};", tableName, vals, where);
              this.ExecuteNonQuery(query);
            }
            catch (Exception fail)
            {
              System.Console.WriteLine(fail.Message);
              returnCode = false;
            }
      return returnCode;
        }

        /// <summary>
        ///     Allows the programmer to easily delete rows from the DB.
        /// </summary>
        /// <param name="tableName">The table from which to delete.</param>
        /// <param name="where">The where clause for the delete.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Delete(String tableName, String where)
        {
          Boolean returnCode = true;
          try
          {
              this.ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
          }
          catch (Exception fail)
          {
            System.Console.WriteLine(fail.Message);
            returnCode = false;
          }
          return returnCode;
        }

        /// <summary>
        ///     Allows the programmer to easily insert into the DB
        /// </summary>
        /// <param name="tableName">The table into which we insert the data.</param>
        /// <param name="data">A dictionary containing the column names and data for the insert.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Insert(String tableName, Dictionary<String, String> data)
        {
            String columns = "";
            String values = "";
            Boolean returnCode = true;
            foreach (KeyValuePair<String, String> val in data)
            {
                columns += String.Format(" {0},", val.Key.ToString());
                //if (val.Value.Contains("'"))
                //{
                //  values += String.Format(" \"{0}\",", val.Value);
                //}
                //else
                //{
                //  values += String.Format(" '{0}',", val.Value);
                //}
                string value_str = val.Value.ToString();
                if (value_str.Contains("\""))
                {
                  value_str = Regex.Replace(value_str, "\"", "\'");
                  values += String.Format(" {1}{0}{2},", value_str, "\"", "\"");
                }
                else
                {
                  values += String.Format(" {1}{0}{2},", val.Value, "\"", "\"");
                }
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            try
            {
              string query = String.Format("insert into {0}({1}) values({2});", tableName, columns, values);
              this.ExecuteNonQuery(query);
            }
            catch (Exception fail)
            {
              System.Console.WriteLine(fail.Message);
              returnCode = false;
            }
            return returnCode;
        }
        /// <summary>
        ///     Allows the programmer to easily delete all data from the DB.
        /// </summary>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearDB()
        {
            DataTable tables;
            try
            {
                tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
                foreach (DataRow table in tables.Rows)
                {
                    this.ClearTable(table["NAME"].ToString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Allows the user to easily clear all data from a specific table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearTable(String table)
        {
            try
            {

                this.ExecuteNonQuery(String.Format("delete from {0};", table));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
