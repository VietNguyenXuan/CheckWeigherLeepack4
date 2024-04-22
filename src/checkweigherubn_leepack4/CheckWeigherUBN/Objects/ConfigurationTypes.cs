using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CheckWeigherUBN
{
  public class ConfigurationTypes : BaseObject, ICloneable
  {
    public enum eConfiguration
    {
      id,
      DatabasePath,
      TemplatePath,
      LastProductId,
      MaxProductDisplay,
      Permission1,
      Permission2,
      Permission3,
      Permission4,
      Permission5,
      Permission6,
      Permission7,
      Permission8,
      Permission9,
      Permission10,
      Permission11,
      //
      ProductCheckType,
      //
      PC_Delay_Barcode,
      PC_Reject_Time,
      PC_Reject_Time_Box_Conti,
      PC_Delay_Reject,
      PC_Reject_Number_Box,
      //
      LineName,
      ReportPath,
    }
    
    /// <summary>
    /// id: 
    /// </summary>
    public int id = 0;
    /// <summary>
    /// DatabasePath: 
    /// </summary>
    public string DatabasePath = "";
    /// <summary>
    /// TemplatePath: 
    /// </summary>
    public string TemplatePath = "";

    public int LastProductId = 0;

    public int MaxProductDisplay = 1000;

    public int Permission1 = 0;
    public int Permission2 = 0;
    public int Permission3 = 0;
    public int Permission4 = 0;
    public int Permission5 = 0;
    public int Permission6 = 0;
    public int Permission7 = 0;
    public int Permission8 = 0;
    public int Permission9 = 0;
    public int Permission10 = 0;
    public int Permission11 = 0;

    //loại sản phẩm để check: thùng hoặc chai
    //public int ProductCheckType = 0;
    //
    public int PC_Delay_Barcode = 0;
    public int PC_Reject_Time = 0;
    public int PC_Reject_Time_Box_Conti = 0;
    public int PC_Delay_Reject = 0;
    public int PC_Reject_Number_Box = 0;

    /// <summary>
    /// LineName
    /// </summary>
    public string LineName = "";

    /// <summary>
    /// Report Path
    /// </summary>
    public string ReportPath = "";

    /// <summary>
    /// //
    /// </summary>
    public List<UserGroupType> list_UserGroup = new List<UserGroupType>();
    public List<UserType> list_User = new List<UserType>();
    public CommunicationsType Communication = null;
    public List<ProductManagementType> list_ProductManagement = new List<ProductManagementType>();
    //
    public UserType currentUserLogin = null;

    public List<DataLogType> list_DataLogInShift = new List<DataLogType>();
   
    //
    public static Dictionary<String, eSQLiteDatabaseDataType> GetDictionaryDB()
    {
      Dictionary<String, eSQLiteDatabaseDataType> dictionaryDB = new Dictionary<String, eSQLiteDatabaseDataType>();
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.id.ToString(), eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY_AUTOINCREMENT);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.DatabasePath.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.TemplatePath.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.LastProductId.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.MaxProductDisplay.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission1.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission2.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission3.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission4.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission5.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission6.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission7.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission8.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission9.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission10.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.Permission11.ToString(), eSQLiteDatabaseDataType.INTEGER);
      //
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.ProductCheckType.ToString(), eSQLiteDatabaseDataType.INTEGER);
      
      //
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.PC_Delay_Barcode.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.PC_Reject_Time.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.PC_Reject_Time_Box_Conti.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.PC_Delay_Reject.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.PC_Reject_Number_Box.ToString(), eSQLiteDatabaseDataType.INTEGER);

      //
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.LineName.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(ConfigurationTypes.eConfiguration.ReportPath.ToString(), eSQLiteDatabaseDataType.TEXT);
      //
      return dictionaryDB;
    }

    public override Dictionary<String, String> BuildDictionaryWithValue()
    {
      Dictionary = new Dictionary<String, String>();
      //
      Dictionary.Add(ConfigurationTypes.eConfiguration.DatabasePath.ToString(), DatabasePath.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.TemplatePath.ToString(), TemplatePath.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.LastProductId.ToString(), LastProductId.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.MaxProductDisplay.ToString(), MaxProductDisplay.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission1.ToString(), Permission1.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission2.ToString(), Permission2.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission3.ToString(), Permission3.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission4.ToString(), Permission4.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission5.ToString(), Permission5.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission6.ToString(), Permission6.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission7.ToString(), Permission7.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission8.ToString(), Permission8.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission9.ToString(), Permission9.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission10.ToString(), Permission10.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.Permission11.ToString(), Permission11.ToString());
      //
      Dictionary.Add(ConfigurationTypes.eConfiguration.ProductCheckType.ToString(), "0");
      //
      Dictionary.Add(ConfigurationTypes.eConfiguration.PC_Delay_Barcode.ToString(), PC_Delay_Barcode.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.PC_Reject_Time.ToString(), PC_Reject_Time.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.PC_Reject_Time_Box_Conti.ToString(), PC_Reject_Time_Box_Conti.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.PC_Delay_Reject.ToString(), PC_Delay_Reject.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.PC_Reject_Number_Box.ToString(), PC_Reject_Number_Box.ToString());
      //
      Dictionary.Add(ConfigurationTypes.eConfiguration.LineName.ToString(), LineName.ToString());
      Dictionary.Add(ConfigurationTypes.eConfiguration.ReportPath.ToString(), ReportPath.ToString());
      return Dictionary;
    }


    /// <summary>
    /// Constructor
    /// </summary>
    public ConfigurationTypes()
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
    public ConfigurationTypes Clone()
    {
      ConfigurationTypes dataRet = new ConfigurationTypes()
      {
        DatabasePath = DatabasePath,
        TemplatePath = TemplatePath,
        LastProductId = LastProductId,
        MaxProductDisplay = MaxProductDisplay,
        Permission1 = Permission1,
        Permission2 = Permission2,
        Permission3 = Permission3,
        Permission4 = Permission4,
        Permission5 = Permission5,
        Permission6 = Permission6,
        Permission7 = Permission7,
        Permission8 = Permission8,
        Permission9 = Permission9,
        Permission10 = Permission10,
        Permission11 = Permission11,
        //
       
        //
        PC_Delay_Barcode = PC_Delay_Barcode,
        PC_Reject_Time = PC_Reject_Time,
        PC_Reject_Time_Box_Conti = PC_Reject_Time,
        PC_Delay_Reject = PC_Delay_Reject,
        PC_Reject_Number_Box = PC_Reject_Number_Box,
        //
        LineName = LineName,
        ReportPath = ReportPath,
      };
      return dataRet;
    }
    /// <summary>
    /// Check data if kDifferent
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public bool checkDifferent(ConfigurationTypes dst)
    {
      bool ret = false;
      ret |= (DatabasePath != dst.DatabasePath);
      ret |= (TemplatePath != dst.TemplatePath);
      ret |= (LastProductId != dst.LastProductId);
      ret |= (MaxProductDisplay != dst.MaxProductDisplay);
      ret |= (Permission1 != dst.Permission1);
      ret |= (Permission2 != dst.Permission2);
      ret |= (Permission3 != dst.Permission3);
      ret |= (Permission4 != dst.Permission4);
      ret |= (Permission5 != dst.Permission5);
      ret |= (Permission6 != dst.Permission6);
      ret |= (Permission7 != dst.Permission7);
      ret |= (Permission8 != dst.Permission8);
      ret |= (Permission9 != dst.Permission9);
      ret |= (Permission10 != dst.Permission10);
      ret |= (Permission11 != dst.Permission11);
      //
    
      //
      ret |= (PC_Delay_Barcode != dst.PC_Delay_Barcode);
      ret |= (PC_Reject_Time != dst.PC_Reject_Time);
      ret |= (PC_Reject_Time_Box_Conti != dst.PC_Reject_Time_Box_Conti);
      ret |= (PC_Delay_Reject != dst.PC_Delay_Reject);
      ret |= (PC_Reject_Number_Box != dst.PC_Reject_Number_Box);
      //
      ret |= (LineName != dst.LineName);
      ret |= (ReportPath != dst.ReportPath);
      //
      return ret;
    }
  }


}
