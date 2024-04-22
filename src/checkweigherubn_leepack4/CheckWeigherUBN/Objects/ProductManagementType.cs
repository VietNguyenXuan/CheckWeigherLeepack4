using System;
using System.Collections.Generic;


namespace CheckWeigherUBN
{
  public class ProductManagementType : BaseObject, ICloneable
  {
    public enum eProductManagement
    {
      id = 0,
      SKU,
      Description,
      Barcode,
      Target,
      /// <summary>
      /// LowerLimit cho  1T
      /// </summary>
      LowerLimit_1T,
      /// <summary>
      /// Upper limit cho 1T
      /// </summary>
      UpperLimit_1T,

      Min1pcs,
      Max1pcs,
      Diff,
      RowId,
      ProductCheckType,
      /// <summary>
      /// LowerLimit cho  2T
      /// </summary>
      LowerLimit_2T,
      /// <summary>
      /// UpperLimit cho  2T
      /// </summary>
      UpperLimit_2T,
      /// <summary>
      /// FG code
      /// </summary>
      FGs,

      /// <summary>
      /// Trong lượng bao bì
      /// </summary>
      gPackageMaterial,      
    }


    public static Dictionary<String, eSQLiteDatabaseDataType> GetDictionaryDB()
    {
      Dictionary<String, eSQLiteDatabaseDataType> dictionaryDB = new Dictionary<String, eSQLiteDatabaseDataType>();
      dictionaryDB.Add(ProductManagementType.eProductManagement.id.ToString(), eSQLiteDatabaseDataType.INTEGER_PRIMARY_KEY_AUTOINCREMENT);
      dictionaryDB.Add(ProductManagementType.eProductManagement.SKU.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(ProductManagementType.eProductManagement.Description.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(ProductManagementType.eProductManagement.Barcode.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(ProductManagementType.eProductManagement.Target.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.LowerLimit_1T.ToString(), eSQLiteDatabaseDataType.INTEGER);      
      dictionaryDB.Add(ProductManagementType.eProductManagement.UpperLimit_1T.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.Min1pcs.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.Max1pcs.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.Diff.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.RowId.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.ProductCheckType.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.LowerLimit_2T.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.UpperLimit_2T.ToString(), eSQLiteDatabaseDataType.INTEGER);
      dictionaryDB.Add(ProductManagementType.eProductManagement.FGs.ToString(), eSQLiteDatabaseDataType.TEXT);
      dictionaryDB.Add(ProductManagementType.eProductManagement.gPackageMaterial.ToString(), eSQLiteDatabaseDataType.INTEGER);
      return dictionaryDB;
    }

    public override Dictionary<String, String> BuildDictionaryWithValue()
    {
      Dictionary = new Dictionary<String, String>();
      //
      Dictionary.Add(ProductManagementType.eProductManagement.SKU.ToString(), SKU.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.Description.ToString(), Description.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.Barcode.ToString(), Barcode.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.Target.ToString(), Target.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.LowerLimit_1T.ToString(), LowerLimit_1T.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.UpperLimit_1T.ToString(), UpperLimit_1T.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.Min1pcs.ToString(), Min1pcs.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.Max1pcs.ToString(), Max1pcs.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.Diff.ToString(), Diff.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.RowId.ToString(), RowId.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.ProductCheckType.ToString(), ProductCheckType.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.LowerLimit_2T.ToString(), LowerLimit_2T.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.UpperLimit_2T.ToString(), UpperLimit_2T.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.FGs.ToString(), FGs.ToString());
      Dictionary.Add(ProductManagementType.eProductManagement.gPackageMaterial.ToString(), gPackageMaterial.ToString());

      return Dictionary;
    }


    /// <summary>
    /// 
    /// </summary>
    public int id = 0;
    public string SKU = "";
    /// <summary>
    /// Description
    /// </summary>
    public string Description = "";
    public string Barcode = "";
    public double Target = 0;
    
    public int Min1pcs = 0;
    public int Max1pcs = 0;
    public int Diff = 0;
    public int RowId = 0;
    public int ProductCheckType = 0;

    /// <summary>
    /// LowerLimit cho 1T
    /// </summary>
    public double LowerLimit_1T = 0;
    /// <summary>
    /// UpperLimit cho 1T
    /// </summary>
    public double UpperLimit_1T = 0;
    //
    /// <summary>
    /// LowerLimit cho 1T
    /// </summary>
    public double LowerLimit_2T = 0;
    /// <summary>
    /// UpperLimit cho 1T
    /// </summary>
    public double UpperLimit_2T = 0;

    public string FGs = "";

    /// <summary>
    /// 
    /// </summary>
    public double gPackageMaterial = 0;
    /// <summary>
    /// 
    /// </summary>
    public ProductManagementType()
    {
    }

    //public ProductManagementType(string sKU, string description, string barcode, string target, string lowerLimit_1T, string upperLimit_1T, string min1pcs, string max1pcs, string diff, string lowerLimit_2T, string upperLimit_2T)
    //{
    //  this.SKU = sKU;
    //  this.Description = description;
    //  this.Barcode = barcode;
    //  this.Target = target.Convert_to_Int();
    //  this.LowerLimit = lowerLimit_1T.Convert_to_Int();
    //  this.UpperLimit = upperLimit_1T.Convert_to_Int();
    //  this.Min1pcs = min1pcs.Convert_to_Int();
    //  this.Max1pcs = max1pcs.Convert_to_Int();
    //  this.Diff = diff.Convert_to_Int();
    //  this.LowerLimit_2T = lowerLimit_1T.Convert_to_Int();
    //  this.UpperLimit_2T = upperLimit_2T.Convert_to_Int();

    //  //User input
    //  this.RowId = 0;
    //  this.ProductCheckType = 0;
    //}


    object ICloneable.Clone()
    {
      return this.Clone();
    }

    // <summary>
    /// Copy to instance
    /// </summary>
    /// <returns></returns>
    public ProductManagementType Clone()
    {
      ProductManagementType dataRet = new ProductManagementType()
      {
        SKU = SKU,
        Description = Description,
        Barcode = Barcode,
        Target = Target,
        LowerLimit_1T = LowerLimit_1T,
        UpperLimit_1T = UpperLimit_1T,
        Min1pcs = Min1pcs,
        Max1pcs = Max1pcs,
        Diff = Diff,
        RowId = RowId,
        ProductCheckType = ProductCheckType,
        LowerLimit_2T = LowerLimit_2T,
        UpperLimit_2T = UpperLimit_2T,
        FGs = FGs,
        gPackageMaterial = gPackageMaterial,
      };
      return dataRet;
    }
    /// <summary>
    /// Check data if kDifferent
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public bool checkDifferent(ProductManagementType dst)
    {
      bool ret = false;
      ret |= (SKU != dst.SKU);
      ret |= (Description != dst.Description);
      ret |= (Barcode != dst.Barcode);
      ret |= (Target != dst.Target);
      ret |= (LowerLimit_1T != dst.LowerLimit_1T);
      ret |= (UpperLimit_1T != dst.UpperLimit_1T);
      ret |= (Min1pcs != dst.Min1pcs);
      ret |= (Max1pcs != dst.Max1pcs);
      ret |= (Diff != dst.Diff);
      ret |= (RowId != dst.RowId);
      ret |= (ProductCheckType != dst.ProductCheckType);
      ret |= (LowerLimit_2T != dst.LowerLimit_2T);
      ret |= (UpperLimit_2T != dst.UpperLimit_2T);
      ret |= (FGs != dst.FGs);
      ret |= (gPackageMaterial != dst.gPackageMaterial);
      return ret;
    }

  }
}
