using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckWeigherUBN
{
  public enum ePemission
  {
    SETTING_Cai_dat_thong_so_truyen_thong,
    PRODUCT_Cai_dat_du_lieu_can_chinh_sua_thong_tin_da_load,
    PRODUCT_Cai_dat_du_lieu_can_import_database_tu_excel,
    MANUAL_CAI_DAT_MANUAL_Disable_Cylinder,
    MANUAL_CAI_DAT_MANUAL_Disable_Buzzer,
    MANUAL_CAI_DAT_MANUAL_Disable_checkweigher,
    MANUAL_CAI_DAT_MANUAL_CHuyen_che_do_man_auto_va_chay_che_do_auto,
    REPORT_xem_va_xuat_report,
    MAIN_Load_va_xac_nhan_chuyen_doi_san_pham,
    MAIN_Reset_counter,
    MAIN_Tat_Buzzer,
  }

  public enum eMode
  {
    BARCODE_ENABLE,
    BARCODE_DISABLE,
    WEIGHER_ENABLE,
    WEIGHER_DISABLE,
    BUZZER_ON,
    BUZZER_OFF,
    CYLINDER_REJECT_ENABLE,
    CYLINDER_REJECT_DISABLE,
    AUTO_ASSIGN_CO_ENABLE,
    AUTO_ASSIGN_CO_DISABLE,
    //
    REJECT_ENABLE,
    REJECT_DISABLE,
    //
    AUTO,
    MANUAL,
    //
    BUTTON_CYLINDER_REJECT_ON,
    BUTTON_CYLINDER_REJECT_OFF,
    //
    RADIO_CHON_HUONG_REJECT_1_HUONG,
    RADIO_CHON_HUONG_REJECT_2_HUONG,
    //
    UNKNOW,
  }

  public enum eProductCheck
  {
    BOX,
    BOTTLE,
    ITEM,
  }

  public enum eDeviceType
  {
    BT_TACH_CHAI,
    BT_CAN,
    BT_REJECT,
    BT_ALL,
    //
    SWITCH_BUZZER,
    SWITCH_AUTO_MAN,
    SWITCH_ENABLE_DISABLE_BARCODE,
    SWITCH_ENABLE_DISABLE_AUTO_ASSIGN_CO,
    SWITCH_ENABLE_DISABLE_WEIGHER,
    SWITCH_ENABLE_DISABLE_REJECT,
    //
    BUTTON_CYLINDER_REJECT,
    //
    RADIO_CHON_HUONG_REJECT,

    UNKNOW,
  }

  public enum eConveyorStatus
  {
    RUN,
    STOP
  }

  public enum eBuzzerStatus
  {
    ON,
    OFF
  }


  static class Conversion
  {
    /// <summary>
    ///  Convert Boolean to Conveyor status
    /// </summary>
    /// <param name="IsRun"></param>
    /// <returns></returns>
    public static eConveyorStatus ConvertConveyorStatus(this bool IsRun)
    {
      eConveyorStatus status = eConveyorStatus.RUN;
      if (IsRun == true)
      {
        status = eConveyorStatus.RUN;
      }
      else
      {
        status = eConveyorStatus.STOP;
      }
      return status;
    }
    /// <summary>
    /// Convert Boolean to Barcode status
    /// </summary>
    /// <param name="IsBarcodeDisable"></param>
    /// <returns></returns>
    public static eMode ConvertToBarcodeMode(this bool IsBarcodeDisable)
    {
      eMode mode = eMode.BARCODE_ENABLE;
      if (IsBarcodeDisable == true)
      {
        mode = eMode.BARCODE_DISABLE;
      }
      else
      {
        mode = eMode.BARCODE_ENABLE;
      }
      return mode;
    }

    /// <summary>
    /// Convert Boolean to AutoAssignChangeover status
    /// </summary>
    /// <param name="IsBarcodeDisable"></param>
    /// <returns></returns>
    public static eMode ConvertToAutoAssignChangeoverMode(this bool IsAssignChangeover)
    {
      eMode mode = eMode.AUTO_ASSIGN_CO_DISABLE;
      if (IsAssignChangeover == true)
      {
        mode = eMode.AUTO_ASSIGN_CO_ENABLE;
      }
      else
      {
        mode = eMode.AUTO_ASSIGN_CO_DISABLE;
      }
      return mode;
    }

    //CYLINDER_REJECT_ON
    /// <summary>
    /// Convert Boolean to Weigher status
    /// </summary>
    /// <param name="IsBuzzerOff"></param>
    /// <returns></returns>
    public static eMode ConvertToCylinderRejectMode(this bool IsCylinderRejectMode)
    {
      eMode mode = eMode.CYLINDER_REJECT_ENABLE;
      if (IsCylinderRejectMode == true)
      {
        mode = eMode.CYLINDER_REJECT_ENABLE;
      }
      else
      {
        mode = eMode.CYLINDER_REJECT_DISABLE;
      }
      return mode;
    }

    /// <summary>
    /// Convert Boolean to Weigher status
    /// </summary>
    /// <param name="IsBuzzerOff"></param>
    /// <returns></returns>
    public static eMode ConvertToWeigherMode(this bool IsWeigherOff)
    {
      eMode mode = eMode.WEIGHER_ENABLE;
      if (IsWeigherOff == true)
      {
        mode = eMode.WEIGHER_DISABLE;
      }
      else
      {
        mode = eMode.WEIGHER_ENABLE;
      }
      return mode;
    }

    /// <summary>
    /// Convert Boolean to Buzzer status
    /// </summary>
    /// <param name="IsBuzzerOff"></param>
    /// <returns></returns>
    public static eMode ConvertToBuzzerMode(this bool IsBuzzerOff)
    {
      eMode mode = eMode.BUZZER_ON;
      if (IsBuzzerOff == true)
      {
        mode = eMode.BUZZER_OFF;
      }
      else
      {
        mode = eMode.BUZZER_ON;
      }
      return mode;
    }

    /// <summary>
    /// Convert Boolean to System status
    /// </summary>
    /// <param name="IsAutoMode"></param>
    /// <returns></returns>
    public static eMode ConvertToSystemMode(this bool IsAutoMode)
    {
      eMode mode = eMode.AUTO;
      if (IsAutoMode == false)
      {
        mode = eMode.MANUAL;
      }
      else
      {
        mode = eMode.AUTO;
      }
      return mode;
    }

    /// <summary>
    /// Check if string is valid number
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsValidNumber(this string str)
    {
      bool ret = false;
      try
      {
        double ret_tst = double.Parse(str);
        ret = true;
      }
      catch
      {
      }
      return ret;
    }

    /// <summary>
    /// Convert string to double
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double Convert_to_Double(this string str)
    {
      double ret = 0;
      if (str != "")
      {
        try
        {
          ret = double.Parse(str);
        }
        catch
        {
        }
      }
      return ret;
    }

    /// <summary>
    /// Convert string to int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int Convert_to_Int(this string str)
    {
      double ret = Convert_to_Double(str);
      return (int)(ret);
    }

    /// <summary>
    /// Convert object to string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Convert_to_String(this object value)
    {
      string ret = "0";
      if (value is string)
      {
        ret = (string)(value);
      }
      else if (value is int)
      {
        ret = ((int)(value)).ToString();
      }
      else if (value is double)
      {
        ret = ((double)(value)).ToString();
      }
      return ret;
    }

    /// <summary>
    /// Convert object to string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int Convert_to_Int(this object value)
    {
      int ret = 0;
      if ((value is int))
      {
        ret = (int)(value);
      }
      else if (value is double)
      {
        ret = (int)((double)(value));
      }
      return ret;
    }

    public static bool ToBoolean(this int value, int startIndex)
    {
      //byte[] bytes = BitConverter.GetBytes(value);
      //bool bit_value = BitConverter.ToBoolean(bytes, startIndex);
      int tmp = ((1 << startIndex));
      bool bit_value = (value & tmp) == tmp;
      return bit_value;
    }

    public static string RemoveSpace(this string value)
    {
      string ret = "";
      for (int i = 0; i < value.Length; i++)
      {
        if (value[i] == ' ')
        {
          /* do nothing */
        }
        else
        {
          ret += String.Format("{0}", value[i]);
        }
      }
      return ret;
    }

    
  }

  public enum SHIFT
  {
    UNKNOWN_0 = 0,
    SHIFT_1 = 1,
    SHIFT_2 = 2,
    SHIFT_3 = 3,
    SHIFT_ALL = 4,
  }


  public class Utils
  {
    /// <summary> 
    /// value: "06:00"
    /// </summary>
    public static string Shift1_Start = "06:00:00";
    /// <summary> 
    /// value: "14:00"
    /// </summary>
    public static string Shift2_Start = "14:00:00";
    /// <summary> 
    /// value: "22:00:00"
    /// </summary>
    public static string Shift3_Start = "22:00:00";

    /// <summary> 
    /// value: "23:59:59"
    /// </summary>
    public static string Shift3_End = "23:59:59";


    public static SHIFT Get_SHIFT_FromDateTime(DateTime datetime)
    {
      SHIFT shift = SHIFT.UNKNOWN_0;
      string cur_hour = (datetime.ToString("HH:mm:00"));

      if ((String.Compare(cur_hour, Shift1_Start) >= 0) && (String.Compare(cur_hour, Shift2_Start) < 0))
      {
        shift = SHIFT.SHIFT_1;
      }
      else if ((String.Compare(cur_hour, Shift2_Start) >= 0) && (String.Compare(cur_hour, Shift3_Start) < 0))
      {
        shift = SHIFT.SHIFT_2;
      }
      else
      {
        shift = SHIFT.SHIFT_3;
      }
      return shift;
    }

    public static SHIFT Get_SHIFT_FromClock()
    {
      return Get_SHIFT_FromDateTime(DateTime.Now);
    }

    private static string GetTimeFromClock()
    {
      return (DateTime.Now.ToString("HH:mm:ss"));
    }


    public static string GetDateFromClockByShift()
    {
      SHIFT shift = Get_SHIFT_FromClock();
      string date = "";
      if (shift == SHIFT.SHIFT_3)
      {
        string time = GetTimeFromClock();
        if ((String.Compare(time, Shift3_Start) >= 0) &&
          (String.Compare(time, Shift3_End) <= 0))
        {
          date = String.Format("{0}", DateTime.Now.ToString("yyyy/MM/dd"));
        }
        else
        {
          date = String.Format("{0}", DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"));
        }
      }
      else if (shift == SHIFT.SHIFT_2)
      {
        date = String.Format("{0}", DateTime.Now.ToString("yyyy/MM/dd"));
      }
      else if (shift == SHIFT.SHIFT_1)
      {
        date = String.Format("{0}", DateTime.Now.ToString("yyyy/MM/dd"));
      }
      else
      {
        date = DateTime.Now.ToString("yyyy/MM/dd");
      }
      return date;
    }


    public static DateTime GetDateTimeFromClockByShift()
    {
      SHIFT shift = Get_SHIFT_FromClock();
      DateTime datetime = DateTime.Now;
      if (shift == SHIFT.SHIFT_3)
      {
        string time = GetTimeFromClock();
        if ((String.Compare(time, Shift3_Start) >= 0) &&
          (String.Compare(time, Shift3_End) <= 0))
        {
          datetime = DateTime.Now;
        }
        else
        {
          datetime = DateTime.Now.AddDays(-1);
        }
      }
      else
      {
        datetime = DateTime.Now;
      }
      return datetime;
    }


    public void StartKeyboardOSK()
    {
      try
      {
        /*
        System.Diagnostics.Process[] list_process = System.Diagnostics.Process.GetProcessesByName("osk");
        if (list_process.Length == 0)
        {
          System.Diagnostics.Process process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(
             ((Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\osk.exe"))));
        }*/
      }
      catch
      {
      }
    }

    public void CloseKeyboardOSK()
    {
      try
      {
        /*
        System.Diagnostics.Process[] list_process = System.Diagnostics.Process.GetProcessesByName("osk");
        for (int i = 0; i < list_process.Length; i++)
        {
          list_process[i].Kill();
        }
        */
      }
      catch
      {
      }
    }

    public static double string_to_double(string str)
    {
      double ret = 0;
      if (str != "")
      {
        try
        {
          ret = double.Parse(str);
        }
        catch
        {
        }
      }
      return ret;
    }


    public static int string_to_int(string str)
    {
      double ret = string_to_double(str);
      return (int)(ret);
    }


    public static string GetValueAsString(object value)
    {
      string value_ret = "0";
      if (value is string)
      {
        value_ret = (string)(value);
      }
      return value_ret;
    }

    public static int GetValueAsInt(object value)
    {
      int value_ret = 0;
      if (value is int)
      {
        value_ret = (int)(value);
      }
      return value_ret;
    }


    public static bool CheckPemission(ConfigurationTypes configuration, ePemission ePemission)
    {
      bool ret = false;
      if (configuration.currentUserLogin != null)
      {
        if (configuration.currentUserLogin.RoleId == (int)(UserType.eRole.OP_shift_1))
        {
          ret = configuration.Permission1.ToBoolean((int)(ePemission));
        }
        else if (configuration.currentUserLogin.RoleId == (int)(UserType.eRole.OP_shift_2))
        {
          ret = configuration.Permission2.ToBoolean((int)(ePemission));
        }
        else if (configuration.currentUserLogin.RoleId == (int)(UserType.eRole.OP_shift_3))
        {
          ret = configuration.Permission3.ToBoolean((int)(ePemission));
        }
        else if (configuration.currentUserLogin.RoleId == (int)(UserType.eRole.Quality))
        {
          ret = configuration.Permission4.ToBoolean((int)(ePemission));
        }
        else if (configuration.currentUserLogin.RoleId == (int)(UserType.eRole.M_E))
        {
          ret = configuration.Permission5.ToBoolean((int)(ePemission));
        }
        else if (configuration.currentUserLogin.RoleId == (int)(UserType.eRole.Manager))
        {
          ret = configuration.Permission6.ToBoolean((int)(ePemission));
        }
        else if (configuration.currentUserLogin.RoleId == (int)(UserType.eRole.Admin))
        {
          ret = configuration.Permission7.ToBoolean((int)(ePemission));
        }
      }
      return ret;
    }

    public static string GetWarningDescription(eWarningType eWarningCode)
    {
			string description = "";
			if (eWarningCode == eWarningType.Warning_Box_Fail)
			{
				description = "Warning 00: Cảnh báo lỗi túi";
			}
			else if (eWarningCode == eWarningType.Warining_Save_Power_Mode) //M1033
			{
				description = "Warning 01: Trạng thái chờ"; // Warning 01: Cảnh báo Không có túi ở băng tải đầu vào
			}
			else if (eWarningCode == eWarningType.Warning_DYN_Fail)
			{
				description = "Warning 02: Cân động bị lỗi";
			}
			else if (eWarningCode == eWarningType.Warning_LD_front)
			{
				description = "Warning 03: Tạm dừng do máy phía trước";
			}
			else if (eWarningCode == eWarningType.Warning_LD_Behind)
			{
				description = "Warning 04: Tạm dừng do máy phía sau";
			}
			else if (eWarningCode == eWarningType.Warning_05)
			{
				description = "Warning 05: Cảnh báo sensor đầu vào băng tải infeed bị che hoặc hư";
			}
			else if (eWarningCode == eWarningType.Warning_06)
			{
				description = "Warning 06: ";
			}
			else if (eWarningCode == eWarningType.Warning_07)
			{
				description = "Warning 07: ";
			}
			else if (eWarningCode == eWarningType.Warning_08)
			{
				description = "Warning 08: ";
			}
			else if (eWarningCode == eWarningType.Warning_09)
			{
				description = "Warning 09: ";
			}
			else if (eWarningCode == eWarningType.Warning_10)
			{
				description = "Warning 10: ";
			}
			else if (eWarningCode == eWarningType.Warning_11)
			{
				description = "Warning 11: ";
			}
			else if (eWarningCode == eWarningType.Warning_12)
			{
				description = "Warning 12: ";
			}
			else if (eWarningCode == eWarningType.Warning_13)
			{
				description = "Warning 13: ";
			}
			else if (eWarningCode == eWarningType.Warning_14)
			{
				description = "Warning 14: ";
			}
			else if (eWarningCode == eWarningType.Warning_15)
			{
				description = "Warning 15: ";
			}
			else if (eWarningCode == eWarningType.Warning_16)
			{
				description = "Warning 16: ";
			}
			else if (eWarningCode == eWarningType.Warning_17)
			{
				description = "Warning 17: ";
			}
			else if (eWarningCode == eWarningType.Warning_18)
			{
				description = "Warning 18: ";
			}
			else if (eWarningCode == eWarningType.Warning_19)
			{
				description = "Warning 19: ";
			}
			else if (eWarningCode == eWarningType.Warning_20)
			{
				description = "Warning 20: ";
			}
			else if (eWarningCode == eWarningType.Warning_21)
			{
				description = "Warning 21: ";
			}
			else if (eWarningCode == eWarningType.Warning_22)
			{
				description = "Warning 22: ";
			}
			else if (eWarningCode == eWarningType.Warning_23)
			{
				description = "Warning 23: ";
			}
			else if (eWarningCode == eWarningType.Warning_24)
			{
				description = "Warning 24: ";
			}
			else if (eWarningCode == eWarningType.Warning_25)
			{
				description = "Warning 25: ";
			}
			else if (eWarningCode == eWarningType.Warning_26)
			{
				description = "Warning 26: ";
			}
			else if (eWarningCode == eWarningType.Warning_27)
			{
				description = "Warning 27: ";
			}
			else if (eWarningCode == eWarningType.Warning_28)
			{
				description = "Warning 28: ";
			}
			else if (eWarningCode == eWarningType.Warning_29)
			{
				description = "Warning 29: ";
			}
			else if (eWarningCode == eWarningType.Warning_30)
			{
				description = "Warning 30: ";
			}
			else if (eWarningCode == eWarningType.Warning_31)
			{
				description = "Warning 31: ";
			}
			return description;
		}


		public static string GetErrorDescription(eErrorType eErrorCode)
    {
      string description = "";
      if (eErrorCode == eErrorType.Error_Barcode_Comm)
      {
        description = "Error 0: Lỗi Truyền thông PLC với Barcode";
      }
      else if (eErrorCode == eErrorType.Error_IND570_Comm)
      {
        description = "Error 1: Lỗi Truyền thông PLC cân IND";
      }
      else if (eErrorCode == eErrorType.Error_Btai_Vao)
      {
        description = "Error 2: Lỗi băng tải vào cân";
      }
      else if (eErrorCode == eErrorType.Error_Btai_Can)
      {
        description = "Error 3: Lỗi băng tải cân";
      }
      else if (eErrorCode == eErrorType.Error_Btai_Ra)
      {
        description = "Error 4: Lỗi băng tải loại túi";
      }
      else if (eErrorCode == eErrorType.Error_Emergency_Stop)
      {
        description = "Error 5: Lỗi Nút nhấn khẩn";
      }
      else if (eErrorCode == eErrorType.Error_Sensor_Reject_Front)
      {
        description = "Error 6: Lỗi sensor báo reject phía trước hoặc đầy túi khay phía trước";
      }
      else if (eErrorCode == eErrorType.Error_Sensor_Reject_Behind)
      {
        description = "Error 7: Lỗi sensor báo reject phía sau hoặc đầy túi khay phía sau";
      }
      else if (eErrorCode == eErrorType.Error_Btai_Ra_Overload)
      {
        description = "Error 8: Lỗi đầy túi trên băng tải đầu ra";
      }
      else if (eErrorCode == eErrorType.Error_Door_open_Coy_CW)
      {
        description = "Error 9: Cửa vị trí băng tải cân mở";
      }
      else if (eErrorCode == eErrorType.Error_Door_open_Front)
      {
        description = "Error 10: Cửa vị trí lấy túi lỗi phía trước mở";
      }
      else if (eErrorCode == eErrorType.Error_sensor_weigh_IN)
      {
        description = "Error 11: Lỗi sensor vào cân";
      }
      else if (eErrorCode == eErrorType.Error_sensor_weigh_OUT)
      {
        description = "Error 12: Lỗi cảm biến ra cân";
      }
      else if (eErrorCode == eErrorType.Error_IND570_Alarm)
      {
        description = "Error 13: Lỗi đầu cân IND570";
      }
      else if (eErrorCode == eErrorType.Error_IND570_Over_Cap)
      {
        description = "Error 14: Lỗi quá trọng lượng cân cho phép";
      }
      else if (eErrorCode == eErrorType.Error_Barcode_Fail)
      {
        description = "Error 15: ";
      }
      else if (eErrorCode == eErrorType.Error_Dynamic_OFF)
      {
        description = "Error 16: Lỗi mất tín hiệu cân động";
      }
      else if (eErrorCode == eErrorType.Error_PS_Air_Fail)
      {
        description = "Error 17: Lỗi mất khí nén";
      }

			///Viet dev add
			else if (eErrorCode == eErrorType.Error_Door_open_Behind)
			{
				description = "Error 18: Cửa vị trí lấy túi lỗi phía sau mở";
			}
			else if (eErrorCode == eErrorType.Error_sensor_CYL_Front)
			{
				description = "Error 19: Lỗi sensor hành trình xylanh phía trước hoặc hư xylanh/selenoid";
			}
			else if (eErrorCode == eErrorType.Error_sensor_CYL_Behind)
			{
				description = "Error 20: Lỗi sensor hành trình xylanh phía sau hoặc hư xylanh/selenoid";
			}
			else if (eErrorCode == eErrorType.Error_sensor_PowerSave)
			{
				description = "Error 21: Lỗi sensor xác định túi vào băng tải tách túi";
			}
			else if (eErrorCode == eErrorType.Error_CYL_reject)
			{
				description = "Error 22: Lỗi xylanh loại bỏ túi";
			}
			else if (eErrorCode == eErrorType.Error_23)
			{
				description = "Error 23: Lỗi loại túi không đi qua sensor hộc phía trước";
			}
			else if (eErrorCode == eErrorType.Error_24)
			{
				description = "Error 24: Lỗi loại túi không đi qua sensor hộc phía sau ";
			}
			else if (eErrorCode == eErrorType.Error_25)
			{
				description = "Error 25: ";
			}
			else if (eErrorCode == eErrorType.Error_26)
			{
				description = "Error 26: ";
			}
			else if (eErrorCode == eErrorType.Error_27)
			{
				description = "Error 27: ";
			}
			else if (eErrorCode == eErrorType.Error_28)
			{
				description = "Error 28: ";
			}
			else if (eErrorCode == eErrorType.Error_29)
			{
				description = "Error 29:";
			}
			else if (eErrorCode == eErrorType.Error_30)
			{
				description = "Error 30: ";
			}
			else if (eErrorCode == eErrorType.Error_31)
			{
				description = "Error 31: ";
			}

			return description;
    }

  }
}
