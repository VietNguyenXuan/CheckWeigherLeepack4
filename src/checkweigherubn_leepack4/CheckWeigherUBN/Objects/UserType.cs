using System;
using System.Collections.Generic;


namespace CheckWeigherUBN
{

  public class UserType : BaseObject, ICloneable
  {
    public enum eRole
    {
      OP_shift_1 = 1,
      OP_shift_2 = 2,
      OP_shift_3,
      Quality,
      M_E,
      Manager,
      Admin,
    }

    public enum eUser
    {
      id = 0,
      UserName,
      RoleId,
      Password,
      Spare01,
    }


    public static Dictionary<String, eSQLiteDatabaseDataType> GetDictionaryDB()
    {
      Dictionary<String, eSQLiteDatabaseDataType> dictionaryDB = new Dictionary<String, eSQLiteDatabaseDataType>();
      dictionaryDB.Add(UserType.eUser.id.ToString(), eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY_AUTOINCREMENT);
      dictionaryDB.Add(UserType.eUser.UserName.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(UserType.eUser.RoleId.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(UserType.eUser.Password.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(UserType.eUser.Spare01.ToString(), eSQLiteDatabaseDataType.TEXT);
      return dictionaryDB;
    }

    public override Dictionary<String, String> BuildDictionaryWithValue()
    {
      Dictionary = new Dictionary<String, String>();
      //
      Dictionary.Add(UserType.eUser.UserName.ToString(), UserName.ToString());
      Dictionary.Add(UserType.eUser.RoleId.ToString(), RoleId.ToString());
      Dictionary.Add(UserType.eUser.Password.ToString(), Password.ToString());
      Dictionary.Add(UserType.eUser.Spare01.ToString(), Spare01.ToString());
      return Dictionary;
    }


    /// <summary>
    /// 
    /// </summary>
    public int id = 0;
    /// <summary>
    /// Username
    /// </summary>
    public string UserName = "";
    public int RoleId = 0;

    /// <summary>
    /// Password when login
    /// </summary>
    public string Password = "";

    public string Spare01 = "";

    public UserType()
    {
    }

    public void Save()
    {
      UserDB sqlUserDB = new UserDB();
      sqlUserDB.Save(this);
    }
    //
    object ICloneable.Clone()
    {
      return this.Clone();
    }

    // <summary>
    /// Copy to instance
    /// </summary>
    /// <returns></returns>
    public UserType Clone()
    {
      UserType dataRet = new UserType()
      {
        RoleId = RoleId,
        UserName = UserName,
        Password = Password,
        Spare01 = Spare01,
      };
      return dataRet;
    }
    /// <summary>
    /// Check data if kDifferent
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public bool checkDifferent(UserType dst)
    {
      bool ret = false;
      ret |= (UserName != dst.UserName);
      ret |= (RoleId != dst.RoleId);
      ret |= (Password != dst.Password);
      ret |= (Spare01 != dst.Spare01);
      return ret;
    }

  }
}
