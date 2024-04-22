using System;
using System.Collections.Generic;


namespace CheckWeigherUBN
{

  public class DataLogType : BaseObject, ICloneable
  {
    public enum eDataLog
    {
      id,
      CurrentID,
      DateTime,
      ProductId,
      Barcode,
      Description,
      Target,
      Actual,
      Diff,
      LowerLimit_1T,
      UpperLimit_1T,
      LowerLimit_2T,
      UpperLimit_2T,
      Status,
      RejectSW,
      Nozzle_Slot,
      FGs,
      ShiftId,
      /// <summary>
      /// Bao bì
      /// </summary>
      gPackageMaterial,

      /// <summary>
      /// Trọng lượng tính luôn bao bì
      /// </summary>
      GrossWeight,
    }


    public static Dictionary<String, eSQLiteDatabaseDataType> GetDictionaryDB()
    {
      Dictionary<String, eSQLiteDatabaseDataType> dictionaryDB = new Dictionary<String, eSQLiteDatabaseDataType>();
      dictionaryDB.Add(DataLogType.eDataLog.id.ToString(), eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY_AUTOINCREMENT);
      dictionaryDB.Add(DataLogType.eDataLog.CurrentID.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(DataLogType.eDataLog.DateTime.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(DataLogType.eDataLog.ProductId.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(DataLogType.eDataLog.Barcode.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(DataLogType.eDataLog.Description.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(DataLogType.eDataLog.Target.ToString(), eSQLiteDatabaseDataType.FLOAT);
      dictionaryDB.Add(DataLogType.eDataLog.Actual.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(DataLogType.eDataLog.Diff.ToString(), eSQLiteDatabaseDataType.FLOAT);
      dictionaryDB.Add(DataLogType.eDataLog.LowerLimit_1T.ToString(), eSQLiteDatabaseDataType.FLOAT);
      dictionaryDB.Add(DataLogType.eDataLog.UpperLimit_1T.ToString(), eSQLiteDatabaseDataType.FLOAT);
      dictionaryDB.Add(DataLogType.eDataLog.LowerLimit_2T.ToString(), eSQLiteDatabaseDataType.FLOAT);
      dictionaryDB.Add(DataLogType.eDataLog.UpperLimit_2T.ToString(), eSQLiteDatabaseDataType.FLOAT);
      dictionaryDB.Add(DataLogType.eDataLog.Status.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(DataLogType.eDataLog.RejectSW.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(DataLogType.eDataLog.Nozzle_Slot.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(DataLogType.eDataLog.FGs.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(DataLogType.eDataLog.ShiftId.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(DataLogType.eDataLog.gPackageMaterial.ToString(), eSQLiteDatabaseDataType.FLOAT);
      dictionaryDB.Add(DataLogType.eDataLog.GrossWeight.ToString(), eSQLiteDatabaseDataType.FLOAT);
      //
      return dictionaryDB;
    }



    public override Dictionary<String, String> BuildDictionaryWithValue()
    {
      Dictionary = new Dictionary<String, String>();
      //
      Dictionary.Add(DataLogType.eDataLog.CurrentID.ToString(), CurrentID.ToString());
      Dictionary.Add(DataLogType.eDataLog.DateTime.ToString(), DateTime.ToString());
      Dictionary.Add(DataLogType.eDataLog.ProductId.ToString(), ProductId.ToString());
      Dictionary.Add(DataLogType.eDataLog.Barcode.ToString(), Barcode.ToString());
      Dictionary.Add(DataLogType.eDataLog.Description.ToString(), Description.ToString());
      Dictionary.Add(DataLogType.eDataLog.Target.ToString(), Target.ToString());
      Dictionary.Add(DataLogType.eDataLog.Actual.ToString(), Actual.ToString());
      Dictionary.Add(DataLogType.eDataLog.Diff.ToString(), Diff.ToString());
      Dictionary.Add(DataLogType.eDataLog.LowerLimit_1T.ToString(), LowerLimit_1T.ToString());
      Dictionary.Add(DataLogType.eDataLog.UpperLimit_1T.ToString(), UpperLimit_1T.ToString());
      Dictionary.Add(DataLogType.eDataLog.LowerLimit_2T.ToString(), LowerLimit_2T.ToString());
      Dictionary.Add(DataLogType.eDataLog.UpperLimit_2T.ToString(), UpperLimit_2T.ToString());
      Dictionary.Add(DataLogType.eDataLog.Status.ToString(), Status.ToString());
      Dictionary.Add(DataLogType.eDataLog.RejectSW.ToString(), RejectSW.ToString());
      Dictionary.Add(DataLogType.eDataLog.Nozzle_Slot.ToString(), Nozzle_Slot.ToString());
      Dictionary.Add(DataLogType.eDataLog.FGs.ToString(), FGs.ToString());
      Dictionary.Add(DataLogType.eDataLog.ShiftId.ToString(), ShiftId.ToString());
      Dictionary.Add(DataLogType.eDataLog.gPackageMaterial.ToString(), gPackageMaterial.ToString());
      Dictionary.Add(DataLogType.eDataLog.GrossWeight.ToString(), GrossWeight.ToString());
      return Dictionary;
    }


    public int id = 0;
    public int CurrentID = 0;
    public int ProductId = 0;
    public string DateTime = "";
    public string Barcode = "";
    public string Description = "";

    /// <summary>
    /// Target Weight
    /// </summary>
    public double Target = 0;

    /// <summary>
    /// Net Weight: đã trừ bao trì
    /// </summary>
    public int Actual =0;
    public double Diff = 0;
    //
    public double LowerLimit_1T = 0;
    public double UpperLimit_1T = 0;
    //
    public double LowerLimit_2T = 0;
    public double UpperLimit_2T = 0;
    //
    public int Status = 0;
    public int RejectSW = 0;
    public int Nozzle_Slot = 0;
    public string FGs = "";
    public int ShiftId = 0;

    /// <summary>
    /// 
    /// </summary>
    public double gPackageMaterial = 0;
    /// <summary>
    /// Gross Weight: bao gồm bao bì
    /// </summary>
    public double GrossWeight = 0;


    //
    //
    public DataLogType()
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
    public DataLogType Clone()
    {
      DataLogType dataRet = new DataLogType()
      {
        CurrentID = CurrentID,
        ProductId = ProductId,
        DateTime = DateTime,
        Barcode = Barcode,
        Description = Description,
        Target = Target,
        Actual = Actual,
        Diff = Diff,
        LowerLimit_1T = LowerLimit_1T,
        UpperLimit_1T = UpperLimit_1T,
        LowerLimit_2T = LowerLimit_2T,
        UpperLimit_2T = UpperLimit_2T,
        Status = Status,
        RejectSW = RejectSW,
        Nozzle_Slot = Nozzle_Slot,
        FGs = FGs,
        ShiftId = ShiftId,
        gPackageMaterial= gPackageMaterial,
        GrossWeight = GrossWeight,
      };
      return dataRet;
    }
    /// <summary>
    /// Check data if kDifferent
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public bool checkDifferent(DataLogType dst)
    {
      bool ret = false;
      ret |= (CurrentID != dst.CurrentID);
      ret |= (ProductId != dst.ProductId);
      //ret |= (DateTime != dst.DateTime);
      ret |= (Barcode != dst.Barcode);
      ret |= (Description != dst.Description);
      ret |= (Target != dst.Target);
      ret |= (Actual != dst.Actual);
      ret |= (Diff != dst.Diff);
      ret |= (Status != dst.Status);
      ret |= (RejectSW != dst.RejectSW);
      ret |= (Nozzle_Slot != dst.Nozzle_Slot);
      ret |= (LowerLimit_1T != dst.LowerLimit_1T);
      ret |= (UpperLimit_1T != dst.UpperLimit_1T);
      ret |= (LowerLimit_2T != dst.LowerLimit_2T);
      ret |= (UpperLimit_2T != dst.UpperLimit_2T);
      ret |= (FGs != dst.FGs);
      ret |= (ShiftId != dst.ShiftId);
      ret |= (gPackageMaterial != dst.gPackageMaterial);//GrossWeight
      ret |= (GrossWeight != dst.GrossWeight);
      return ret;
    }


  }
}
