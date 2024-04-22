using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
  public class DataLogDB : DatabaseBaseObject
  {
    /// <summary>
    /// value: "datalog"
    /// </summary>
    private string Table_name = "datalog";

    public DataLogDB(string template_path, string database_path, bool IsNeedCreateNewFile) : base(template_path, database_path, IsNeedCreateNewFile)
    {
      _template_db_file_name = "datalog";
      _IsNeedCreateNewFile = true;
      //
      _template_path = template_path;
      _database_path = database_path;

    }

    private string GetData(DataRow r, DataLogType.eDataLog data)
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
    private DataLogType CreateObjectFromDataRow(DataRow r)
    {
      DataLogType dataRet = new DataLogType()
      {
        id = string_to_int(GetData(r, DataLogType.eDataLog.id)),
        CurrentID = (GetData(r, DataLogType.eDataLog.CurrentID)).Convert_to_Int(),
        DateTime = (GetData(r, DataLogType.eDataLog.DateTime)),
        Nozzle_Slot = (GetData(r, DataLogType.eDataLog.Nozzle_Slot)).Convert_to_Int(),
        ProductId = (GetData(r, DataLogType.eDataLog.ProductId)).Convert_to_Int(),
        Barcode = (GetData(r, DataLogType.eDataLog.Barcode)),
        Description = (GetData(r, DataLogType.eDataLog.Description)),
        Target = (GetData(r, DataLogType.eDataLog.Target)).Convert_to_Double(),
        Actual = (GetData(r, DataLogType.eDataLog.Actual)).Convert_to_Int(),
        Diff = (GetData(r, DataLogType.eDataLog.Diff)).Convert_to_Double(),
        LowerLimit_1T = (GetData(r, DataLogType.eDataLog.LowerLimit_1T)).Convert_to_Double(),
        UpperLimit_1T = (GetData(r, DataLogType.eDataLog.UpperLimit_1T)).Convert_to_Double(),
        LowerLimit_2T = (GetData(r, DataLogType.eDataLog.LowerLimit_2T)).Convert_to_Double(),
        UpperLimit_2T = (GetData(r, DataLogType.eDataLog.UpperLimit_2T)).Convert_to_Double(),
        Status = (GetData(r, DataLogType.eDataLog.Status)).Convert_to_Int(),
        RejectSW = (GetData(r, DataLogType.eDataLog.RejectSW)).Convert_to_Int(),
        FGs = (GetData(r, DataLogType.eDataLog.FGs)),
        ShiftId = (GetData(r, DataLogType.eDataLog.ShiftId)).Convert_to_Int(),
        gPackageMaterial = (GetData(r, DataLogType.eDataLog.gPackageMaterial)).Convert_to_Double(),
        GrossWeight = (GetData(r, DataLogType.eDataLog.GrossWeight)).Convert_to_Double(),
      };
      dataRet.LowerLimit_1T = (double)((int)(dataRet.LowerLimit_1T));
      dataRet.UpperLimit_1T = (double)((int)(dataRet.UpperLimit_1T));
      dataRet.LowerLimit_2T = (double)((int)(dataRet.LowerLimit_2T));
      dataRet.UpperLimit_2T = (double)((int)(dataRet.UpperLimit_2T));
      return dataRet;
    }

    /// <summary>
    /// Save to database
    /// </summary>
    /// <param name="dataSaved"></param>
    /// <returns></returns>
    public override object Save(object dataSave)
    {
      bool IsOK = false;
      DataLogType dataReturn = null;
      if (dataSave is DataLogType)
      {
        DataLogType dataSaved = (DataLogType)(dataSave);

        dataReturn = dataSaved.Clone();
        string query = "";
        DataTable recipe;
        try
        {
          DateTime datetime_from_data_log = Convert.ToDateTime(dataSaved.DateTime);
          DateTime datetime_now = DateTime.Now;
          if ((datetime_from_data_log > datetime_now.AddDays(1)) || (datetime_from_data_log < datetime_from_start_software))
          {
            /* do nothing */
          }
          else
          {
            string date = String.Format("{0}", datetime_from_data_log.ToString("yyyyMMdd"));
            string db_file_name = String.Format("{0}_{1}", date, _template_db_file_name);

            SQLiteDatabase db = GetSQLiteDatabase(db_file_name, eDataBaseProcess.WRITE);


            Dictionary<String, String> data = dataSaved.BuildDictionaryWithValue();
            //
            query = String.Format("select * from {0} where {1} = '{2}'", Table_name, DataLogType.eDataLog.id.ToString(), dataSaved.id);
            recipe = db.GetDataTable(query);
            bool IsHaveData = (recipe.Rows.Count > 0);

            /* Create new */
            if (IsHaveData == false)
            {
              IsOK = db.Insert(Table_name, data);
              //
              if (IsOK == true)
              {
                query = String.Format("select * from {0} ORDER by id DESC limit 1", Table_name);
                recipe = db.GetDataTable(query);
                foreach (DataRow r in recipe.Rows)
                {
                  dataReturn = CreateObjectFromDataRow(r);
                }
              }/*if (IsOK == true)*/
              else
              {
                dataReturn = null;
              }
            }
            else
            {
              /* Update */
              foreach (DataRow r in recipe.Rows)
              {
                DataLogType oldData = CreateObjectFromDataRow(r);
                bool IsEnableSave = oldData.checkDifferent(dataSaved);
                if (IsEnableSave == true)
                {
                  string id = r["id"].ToString();
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
                  /* Nothing to update */
                  IsOK = true;
                  dataReturn = oldData;
                }

              }/*foreach (DataRow r in recipe.Rows)*/
            }
          }
        }
        catch
        {
        }
      }
      return dataReturn;
    }


    //
    /// <summary>
    /// Load All Mail Address Support
    /// </summary>
    /// <returns></returns>
    public List<DataLogType> LoadAllByDateShift(DateTime datetime, int shift)
    {
      List<DataLogType> list_data = new List<DataLogType>();
      try
      {
        DateTime datetime_from_data_log = datetime;
        DateTime datetime_now = DateTime.Now;
        if ((datetime_from_data_log > datetime_now.AddDays(1)) || (datetime_from_data_log < datetime_from_start_software))
        {
          /* do nothing */
        }
        else
        {
          DataTable recipe;
          String query = "";
          string datetime_as_str = datetime.ToString("yyyyMMdd");

          query = String.Format("select * from {0} where ShiftId = {1}", Table_name, shift);
          string file_name = String.Format("{0}_{1}", datetime_as_str, _template_db_file_name);
          SQLiteDatabase db = GetSQLiteDatabase(file_name, eDataBaseProcess.READ);
          if (db != null)
          {
            recipe = db.GetDataTable(query);
            foreach (DataRow r in recipe.Rows)
            {
              DataLogType data = CreateObjectFromDataRow(r);
              list_data.Add(data);
            }
          }
        }
      }
      catch (Exception fail)
      {
        String error = "The following error has occurred:";
        error += fail.Message.ToString();
      }
      return list_data;
    }
    /// <summary>
    /// Load database from dateTimeFrom --> dateTimeTo
    /// </summary>
    /// <param name="LineId"></param>
    /// <param name="dateTimeFrom"></param>
    /// <param name="dateTimeTo"></param>
    /// <returns></returns>
    public List<DataLogType> LoadAllByDateFromDateTo_old(DateTime dateTimeFrom, DateTime dateTimeTo)
    {
      List<DataLogType> list_data = new List<DataLogType>();
      try
      {
        DateTime datetime_from_data_log = dateTimeFrom;
        DateTime datetime_now = DateTime.Now;
        if ((datetime_from_data_log > datetime_now.AddDays(1)) || (datetime_from_data_log < datetime_from_start_software))
        {
          /* do nothing */
        }
        else
        {
          DataTable recipe;
          String query = "";
          string datetime_as_str = dateTimeFrom.ToString("yyyyMMdd");
          //
          string dt_start = dateTimeFrom.ToString("yyyy/MM/dd HH:mm:ss");
          string dt_end = dateTimeTo.ToString("yyyy/MM/dd HH:mm:ss");
          string condition_1 = String.Format("('{0}' <= {1}) AND ({1} <'{2}') ", dt_start, DataLogType.eDataLog.DateTime.ToString(), dt_end);
          //
          query = String.Format("select * from {0} where ({1})", Table_name, condition_1);

          //
          query = String.Format("select * from {0}", Table_name);
          string file_name = String.Format("{0}_{1}", datetime_as_str, _template_db_file_name);
          SQLiteDatabase db = GetSQLiteDatabase(file_name, eDataBaseProcess.READ);
          if (db != null)
          {
            recipe = db.GetDataTable(query);
            foreach (DataRow r in recipe.Rows)
            {
              DataLogType data = CreateObjectFromDataRow(r);
              list_data.Add(data);
            }
          }
        }
      }
      catch (Exception fail)
      {
        String error = "The following error has occurred:";
        error += fail.Message.ToString();
      }
      return list_data;
    }

		public List<DataLogType> LoadAllByDateFromDateTo(DateTime dateTimeFrom, DateTime dateTimeTo)
		{
			List<DataLogType> list_data = new List<DataLogType>();
			try
			{
				DateTime datetime_from_data_log = dateTimeFrom;
				DateTime datetime_now = DateTime.Now;
				if ((datetime_from_data_log > datetime_now.AddDays(1)) || (datetime_from_data_log < datetime_from_start_software))
				{
					/* do nothing */
				}
				else
				{
					DataTable recipe;
					String query = "";
					string datetime_as_str = dateTimeFrom.ToString("yyyyMMdd");
					//
					string dt_start = dateTimeFrom.ToString("yyyy/MM/dd HH:mm:ss");
					string dt_end = dateTimeTo.ToString("yyyy/MM/dd HH:mm:ss");
					string condition_1 = String.Format("('{0}' <= {1}) AND ({1} <'{2}') ", dt_start, DataLogType.eDataLog.DateTime.ToString(), dt_end);
					//
					query = String.Format("select * from {0} where ({1})", Table_name, condition_1);

					//
					//query = String.Format("select * from {0}", Table_name);
					string file_name = String.Format("{0}_{1}", datetime_as_str, _template_db_file_name);
					SQLiteDatabase db = GetSQLiteDatabase(file_name, eDataBaseProcess.READ);
					if (db != null)
					{
						recipe = db.GetDataTable(query);
						foreach (DataRow r in recipe.Rows)
						{
							DataLogType data = CreateObjectFromDataRow(r);
							list_data.Add(data);
						}
					}
				}
			}
			catch (Exception fail)
			{
				String error = "The following error has occurred:";
				error += fail.Message.ToString();
			}
			return list_data;
		}


		/// <summary>
		/// Load All Mail Address Support
		/// </summary>
		/// <returns></returns>
		public List<DataLogType> LoadAllByDateTime(DateTime datetime)
    {
      List<DataLogType> list_data = new List<DataLogType>();
      try
      {
        DateTime datetime_from_data_log = datetime;
        DateTime datetime_now = DateTime.Now;
        if ((datetime_from_data_log > datetime_now.AddDays(1)) || (datetime_from_data_log < datetime_from_start_software))
        {
          /* do nothing */
        }
        else
        {
          DataTable recipe;
          String query = "";
          string datetime_as_str = datetime.ToString("yyyyMMdd");

          query = String.Format("select * from {0}", Table_name);
          string file_name = String.Format("{0}_{1}", datetime_as_str, _template_db_file_name);
          SQLiteDatabase db = GetSQLiteDatabase(file_name, eDataBaseProcess.READ);
          if (db != null)
          {
            recipe = db.GetDataTable(query);
            foreach (DataRow r in recipe.Rows)
            {
              DataLogType data = CreateObjectFromDataRow(r);
              list_data.Add(data);
            }
          }
        }
      }
      catch (Exception fail)
      {
        String error = "The following error has occurred:";
        error += fail.Message.ToString();
      }
      return list_data;
    }
  }
}
