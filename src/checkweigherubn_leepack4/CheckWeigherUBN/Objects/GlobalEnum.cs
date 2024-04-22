using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckWeigherUBN
{
  public enum eWeigerStatus
  {
    NG,
    OK,
    _1T,
    Over,
    CW_Disable,
    MAN,
  }

  public enum eShift
  {
    SHIFT_ALL = 0,
    SHIFT_1 = 1,
    SHIFT_2 = 2,
    SHIFT_3 = 3,
    
  }

  public enum eSKU
  {
    SKU_ALL = 0,
    SKU_1 = 1,
    SKU_2 = 2,
    SKU_3 = 3,
    SKU_4 = 4,
    SKU_5 = 5,
  }


  public enum eAction
  {
    PREVIEW,
    REPORT_EXCEL
  }

}
