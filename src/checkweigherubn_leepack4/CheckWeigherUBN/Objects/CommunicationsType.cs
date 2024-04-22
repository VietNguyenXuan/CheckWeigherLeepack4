using System;
using System.Collections.Generic;


namespace CheckWeigherUBN
{

  public class CommunicationsType : BaseObject, ICloneable
  {
    public enum eCommunications
    {
      id,
      PLC_IpAddress,
      PortNumber,
    }


    public static Dictionary<String, eSQLiteDatabaseDataType> GetDictionaryDB()
    {
      Dictionary<String, eSQLiteDatabaseDataType> dictionaryDB = new Dictionary<String, eSQLiteDatabaseDataType>();
      dictionaryDB.Add(CommunicationsType.eCommunications.id.ToString(), eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY_AUTOINCREMENT);
      dictionaryDB.Add(CommunicationsType.eCommunications.PLC_IpAddress.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(CommunicationsType.eCommunications.PortNumber.ToString(), eSQLiteDatabaseDataType.INTEGER);
      return dictionaryDB;
    }

    public override Dictionary<String, String> BuildDictionaryWithValue()
    {
      Dictionary = new Dictionary<String, String>();
      //
      Dictionary.Add(CommunicationsType.eCommunications.PLC_IpAddress.ToString(), PLC_IpAddress.ToString());
      Dictionary.Add(CommunicationsType.eCommunications.PortNumber.ToString(), PortNumber.ToString());
      return Dictionary;
    }


    /// <summary>
    /// 
    /// </summary>
    public int id = 0;
    public string PLC_IpAddress = "";
    public int PortNumber = 2000;


    public CommunicationsType()
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
    public CommunicationsType Clone()
    {
      CommunicationsType dataRet = new CommunicationsType()
      {
        PLC_IpAddress = PLC_IpAddress,
        PortNumber = PortNumber,
      };
      return dataRet;
    }
    /// <summary>
    /// Check data if kDifferent
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public bool checkDifferent(CommunicationsType dst)
    {
      bool ret = false;
      ret |= (PLC_IpAddress != dst.PLC_IpAddress);
      ret |= (PortNumber != dst.PortNumber);
      return ret;
    }

  }
}
