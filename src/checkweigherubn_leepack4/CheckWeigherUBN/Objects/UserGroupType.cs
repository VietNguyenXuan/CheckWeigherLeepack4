using System;
using System.Collections.Generic;


namespace CheckWeigherUBN
{
  /// <summary>
  /// Chức vụ
  /// </summary>
  public enum eUserGroupRole
  {
    /// <summary>
    /// Operator: nhân viên vận hành máy
    /// </summary>
    OP = 1,
    /// <summary>
    /// Administrator
    /// </summary>
    Admin = 2,
  }

  public enum eRolePermit
  {
    /// <summary>
    /// Assign loss bằng tay
    /// </summary>
    EnableAssignLossByManual = 0x01,
    /// <summary>
    /// khong scan barcode khi chuyen doi, nhap tay thi truong ca xac nhan
    /// </summary>
    //EnableScanBarocdeByManualWhenChangeover = 0x02,
  }






  public class UserGroupType : BaseObject, ICloneable
  {
    public enum eUserGroup
    {
      id,
      RoleId,
      roleEnable,
      Password,
      Spare01,
    }


    public static Dictionary<String, eSQLiteDatabaseDataType> GetDictionaryDB()
    {
      Dictionary<String, eSQLiteDatabaseDataType> dictionaryDB = new Dictionary<String, eSQLiteDatabaseDataType>();
      dictionaryDB.Add(UserGroupType.eUserGroup.id.ToString(), eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY_AUTOINCREMENT);
      dictionaryDB.Add(UserGroupType.eUserGroup.RoleId.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(UserGroupType.eUserGroup.roleEnable.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(UserGroupType.eUserGroup.Password.ToString(), eSQLiteDatabaseDataType.TEXT);
      return dictionaryDB;
    }

    public override Dictionary<String, String> BuildDictionaryWithValue()
    {
      Dictionary = new Dictionary<String, String>();
      //
      Dictionary.Add(UserGroupType.eUserGroup.RoleId.ToString(), RoleId.ToString());
      Dictionary.Add(UserGroupType.eUserGroup.roleEnable.ToString(), roleEnable.ToString());
      Dictionary.Add(UserGroupType.eUserGroup.Password.ToString(), Password.ToString());
      return Dictionary;
    }



    public int id = 0;
    public int RoleId = (int)(eUserGroupRole.OP);
    /// <summary>
    /// 
    /// </summary>
    public int roleEnable = 0;

    /// <summary>
    /// Password when login
    /// </summary>
    public string Password = "";

    //
    /// <summary>
    /// Assign loss bằng tay
    /// </summary>
    public bool IsEnableAssignLossByManual = false;
    public bool IsDataChanged = false;

    public UserGroupType()
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
    public UserGroupType Clone()
    {
      UserGroupType dataRet = new UserGroupType()
      {
        RoleId = RoleId,
        roleEnable = roleEnable,
        Password = Password,
      };
      return dataRet;
    }
    /// <summary>
    /// Check data if kDifferent
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public bool checkDifferent(UserGroupType dst)
    {
      bool ret = false;
      ret |= (RoleId != dst.RoleId);
      ret |= (roleEnable != dst.roleEnable);
      ret |= (Password != dst.Password);
      return ret;
    }




    public void AssignRolePermit()
    {
      this.IsEnableAssignLossByManual = ((roleEnable & (int)eRolePermit.EnableAssignLossByManual) == (int)eRolePermit.EnableAssignLossByManual);
    }

    public void ApplyAllRole()
    {
      roleEnable = 0;
      roleEnable |= (int)eRolePermit.EnableAssignLossByManual;
      //
      AssignRolePermit();
    }


    public int ConvertRolePermit()
    {
      roleEnable = 0;
      if (this.IsEnableAssignLossByManual == true) roleEnable |= (int)eRolePermit.EnableAssignLossByManual;

      return roleEnable;
    }


  }
}
