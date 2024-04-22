using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckWeigherUBN
{
  public class Shift
  {
    /// <summary> 
    /// value: "06:00:00"
    /// </summary>
    public const string Shift1_Start = "06:00:00";
    /// <summary> 
    /// value: "14:00:00"
    /// </summary>
    public const string Shift2_Start = "14:00:00";
    /// <summary> 
    /// value: "22:00:00"
    /// </summary>
    public const string Shift3_Start = "22:00:00";

    /// <summary> 
    /// value: "23:59:59"
    /// </summary>
    public const string Shift3_Midnight = "23:59:59";



    public static eShift GetShiftFromDateTime(DateTime datetime)
    {
      eShift shift = eShift.SHIFT_1;
      string cur_hour = (datetime.ToString("HH:mm:00"));

      if ((String.Compare(cur_hour, Shift1_Start) >= 0) && (String.Compare(cur_hour, Shift2_Start) < 0))
      {
        shift = eShift.SHIFT_1;
      }
      else if ((String.Compare(cur_hour, Shift2_Start) >= 0) && (String.Compare(cur_hour, Shift3_Start) < 0))
      {
        shift = eShift.SHIFT_2;
      }
      else
      {
        shift = eShift.SHIFT_3;
      }
      return shift;
    }

    public static DateTime GetShiftStartFromClock()
    {
      DateTime currentDateTime = DateTime.Now;
      eShift eShift = GetShiftFromDateTime(currentDateTime);
      if (eShift == eShift.SHIFT_3)
      {
        string time = currentDateTime.ToString("HH:mm:ss");
        if ((String.Compare(time, Shift3_Start) >= 0) &&
          (String.Compare(time, Shift3_Midnight) <= 0))
        {
          /* do nothing */
        }
        else
        {
          currentDateTime = DateTime.Now.AddDays(-1);
        }
      }
      else
      {
        /* do nothing */
      }
      currentDateTime = GetShiftStartFromDateTime(currentDateTime);
      return currentDateTime;
    }

    public static DateTime GetShiftEndFromClock()
    {
      DateTime DateTimeRet = DateTime.Now;
      eShift eShift = GetShiftFromDateTime(DateTime.Now);
      DateTime DateTimeShiftStart = GetShiftStartFromClock();
      if ((eShift == eShift.SHIFT_1) || (eShift == eShift.SHIFT_2))
      {
        DateTimeRet = DateTimeShiftStart.AddHours(+8);
      }
      else if (eShift == eShift.SHIFT_3)
      {
        string time = DateTimeRet.ToString("HH:mm:ss");
        string dtEndAsString = "";
        if ((String.Compare(time, Shift3_Start) >= 0) &&
          (String.Compare(time, Shift3_Midnight) <= 0))
        {
          dtEndAsString = String.Format("{0} {1}", DateTimeShiftStart.AddDays(+1).ToString("yyyy/MM/dd"), Shift1_Start);
        }
        else
        {
          dtEndAsString = String.Format("{0} {1}", DateTimeRet.ToString("yyyy/MM/dd"), Shift1_Start);
        }
        if (dtEndAsString != "")
        {
          try
          {
            DateTimeRet = Convert.ToDateTime(dtEndAsString);
          }
          catch
          {

          }
        }
      }
      return DateTimeRet;
    }
    public static DateTime GetShiftStartFromDateTime(DateTime datetime)
    {
      DateTime DateTimeRet = datetime;
      eShift eShift = GetShiftFromDateTime(datetime);
      string dtAsString = "";
      try
      {
        if (eShift == eShift.SHIFT_1)
        {
          dtAsString = String.Format("{0} {1}", datetime.ToString("yyyy/MM/dd"), Shift1_Start);
        }
        else if (eShift == eShift.SHIFT_2)
        {
          dtAsString = String.Format("{0} {1}", datetime.ToString("yyyy/MM/dd"), Shift2_Start);
        }
        else if (eShift == eShift.SHIFT_3)
        {
          dtAsString = String.Format("{0} {1}", datetime.ToString("yyyy/MM/dd"), Shift3_Start);
        }
        DateTimeRet = Convert.ToDateTime(dtAsString);
      }
      catch
      {
      }
      return DateTimeRet;
    }

    public static eShift GetShiftFromClock()
    {
      return GetShiftFromDateTime(DateTime.Now);
    }

    public Shift()
    {
    }
  }
}
