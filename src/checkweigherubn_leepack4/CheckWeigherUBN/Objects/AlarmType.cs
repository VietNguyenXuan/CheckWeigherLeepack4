using System;
using System.Collections.Generic;


namespace CheckWeigherUBN
{

  public class AlarmType : BaseObject, ICloneable
  {
    public enum eAlarm
    {
      id,
      DateTime,
      ShiftId,
      AlarmCode,
      Description
    }


    public static Dictionary<String, eSQLiteDatabaseDataType> GetDictionaryDB()
    {
      Dictionary<String, eSQLiteDatabaseDataType> dictionaryDB = new Dictionary<String, eSQLiteDatabaseDataType>();
      dictionaryDB.Add(AlarmType.eAlarm.id.ToString(), eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY_AUTOINCREMENT);
      dictionaryDB.Add(AlarmType.eAlarm.DateTime.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(AlarmType.eAlarm.ShiftId.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(AlarmType.eAlarm.AlarmCode.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(AlarmType.eAlarm.Description.ToString(), eSQLiteDatabaseDataType.TEXT);
      //
      return dictionaryDB;
    }



    public override Dictionary<String, String> BuildDictionaryWithValue()
    {
      Dictionary = new Dictionary<String, String>();
      //
      Dictionary.Add(AlarmType.eAlarm.DateTime.ToString(), DateTime.ToString());
      Dictionary.Add(AlarmType.eAlarm.ShiftId.ToString(), ShiftId.ToString());
      Dictionary.Add(AlarmType.eAlarm.AlarmCode.ToString(), AlarmCode.ToString());
      Dictionary.Add(AlarmType.eAlarm.Description.ToString(), Description.ToString());
      return Dictionary;
    }


    public int id = 0;
    public string DateTime = "";
    public int ShiftId = 0;
    public int AlarmCode = 0;
    public string Description = "";

    private bool isErrorAlarm = false;

    public void SetErrorAlarm(bool value)
    {
      isErrorAlarm = value;
    }
    public bool GetErrorAlarm()
    {
      return isErrorAlarm;
    }
    //
    public AlarmType()
    {
    }

    object ICloneable.Clone()
    {
      return this.Clone();
    }

    // <summary>
    /// Copy to instance
    /// </summary>
    /// <returns></returns>
    public AlarmType Clone()
    {
      AlarmType dataRet = new AlarmType()
      {
        DateTime = DateTime,
        AlarmCode = AlarmCode,
        ShiftId = ShiftId,
        Description = Description,
      };
      return dataRet;
    }
    /// <summary>
    /// Check data if kDifferent
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public bool checkDifferent(AlarmType dst)
    {
      bool ret = false;
      ret |= (AlarmCode != dst.AlarmCode);
      ret |= (Description != dst.Description);
      ret |= (ShiftId != dst.ShiftId);
      //ret |= (DateTime != dst.DateTime);
      return ret;
    }


  }
}
