/*==============================================================================
**                              CheckWeigherUBN
**                            Copyright 2016
**------------------------------------------------------------------------------
** Supported PLCs      : Mitsubishi
** Supported Compilers : Compiler independent (GxWorks3, Visual Studio 2017)
**------------------------------------------------------------------------------
** File name         : ErrorLog.cs
**
** Module name       : CheckWeigherUBN
**
**
** Summary: 
**
**= History ====================================================================
** 01.00 15/03/2019 dungvt
** - Creation
===============================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GlacialComponents.Controls;
using OfficeOpenXml;

namespace CheckWeigherUBN
{
  public partial class ErrorLog : UserControl
  {
    public delegate void RequestAlarmReset(object sender);
    public event RequestAlarmReset OnRequestAlarmReset;
    //

    private const int COL_ID = 0;
    private const int COL_ERROR_CODE = COL_ID + 1;
    private const int COL_ERROR_DESCRIPTION = COL_ERROR_CODE + 1;
    private const int COL_DATE_TIME = COL_ERROR_DESCRIPTION + 1;

    //
    private ConfigurationTypes _configuration = null;
    private int _error_code_previous = -1;
    private int _warning_code_previous = -1;

    private Timer _delay_timer = new Timer();
    public ErrorLog()
    {
      InitializeComponent();
      //
      this._delay_timer.Interval = 1000;
      this._delay_timer.Tick += _delay_timer_Tick;
    }

    private void _delay_timer_Tick(object sender, EventArgs e)
    {
      this._delay_timer.Enabled = false;
      DateTime dt = Utils.GetDateTimeFromClockByShift();
      eShift shift = Shift.GetShiftFromClock();
     // DisplayAlarmToListView(dt, shift, _configuration.TemplatePath, _configuration.DatabasePath);
    }

    /// <summary>
    /// Update configuration from main
    /// </summary>
    /// <param name="configuration"></param>
    public void UpdateConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
      //load from database
      this._delay_timer.Enabled = true;
    }
    private void AddWarningLogToListView(bool currentWarning, bool previousWarning, eWarningType eWarningCode)
    {
      if (currentWarning != previousWarning)
      {
        string warning_code = ((int)(eWarningCode) + 1).ToString().PadLeft(3, '0');
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
          description = "Warning 03: Tạm dừng do máy phía sau";
        }
        else if (eWarningCode == eWarningType.Warning_LD_Behind)
        {
          description = "Warning 04: Tạm dừng do máy phía trước";
        }
        else if (eWarningCode == eWarningType.Warning_31)
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



				//
				if (description != "")
        {
          bool IsFound = false;
          for (int i = 0; (i < this.glacialList1.Items.Count) && (IsFound == false); i++)
          {
            IsFound = (this.glacialList1.Items[i].SubItems[COL_ERROR_CODE].Text == warning_code);
          }

          if (IsFound == false)
          {            
            /* Check if we already added */
            GLItem item = new GLItem();
            item.SubItems[COL_ID].Text = (this.glacialList1.Items.Count + 1).ToString();
            item.SubItems[COL_ERROR_CODE].Text = String.Format("{0}", warning_code);
            item.SubItems[COL_ERROR_DESCRIPTION].Text = description;
            item.SubItems[COL_DATE_TIME].Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            this.glacialList1.Items.Insert(0, item);
          }
        }
      }/*if (currentError != previousError)*/
    }

    private AlarmType SaveErrorLogToDatabase(bool currentError, bool previousError, eErrorType eErrorCode)
    {
      AlarmType alarm = null;
			if (currentError != previousError)
      {
				string error_code = ((int)(eErrorCode) + 1).ToString().PadLeft(3, '0');
				string description = Utils.GetErrorDescription(eErrorCode);
				//save to database
				alarm = SaveAlarm((int)(eErrorCode), description);
			}
      return alarm;
		}

		private void AddErrorLogToListView(bool currentError, bool previousError, eErrorType eErrorCode)
    {
      if (currentError != previousError)
      {
        string error_code = ((int)(eErrorCode) + 1).ToString().PadLeft(3, '0');
        string description = Utils.GetErrorDescription(eErrorCode);
        //if (eErrorCode == eErrorType.Error_Barcode_Comm)
        //{
        //  description = "Lỗi Truyền thông PLC với Barcode";
        //}
        //else if (eErrorCode == eErrorType.Error_IND570_Comm)
        //{
        //  description = "Lỗi Truyền thông PLC cân IND";
        //}
        //else if (eErrorCode == eErrorType.Error_Btai_Vao)
        //{
        //  description = "Lỗi băng tải vào cân";
        //}
        //else if (eErrorCode == eErrorType.Error_Btai_Can)
        //{
        //  description = "Lỗi băng tải cân";
        //}
        //else if (eErrorCode == eErrorType.Error_Btai_Ra)
        //{
        //  description = "Lỗi băng tải loại chai/thùng";
        //}
        //else if (eErrorCode == eErrorType.Error_Emergency_Stop)
        //{
        //  description = "Lỗi Nút nhấn khẩn";
        //}
        //else if (eErrorCode == eErrorType.Error_Overweight)
        //{
        //  description = "Lỗi quá trọng lượng";
        //}
        //else if (eErrorCode == eErrorType.Error_Reject_Ket)
        //{
        //  description = "Lỗi kẹt thùng/chai ở vị trí loại";
        //}
        //else if (eErrorCode == eErrorType.Error_Btai_Ra_Overload)
        //{
        //  description = "Lỗi đầy túi trên băng tải loại túi";
        //}
        //else if (eErrorCode == eErrorType.Error_Weigher_Door_open)
        //{
        //  description = "Cửa vị trí băng tải cân mở";
        //}
        //else if (eErrorCode == eErrorType.Error_Reject_Door_open)
        //{
        //  description = "Cửa vị trí lấy túi NG mở";
        //}
        //else if (eErrorCode == eErrorType.Error_sensor_weigh_IN)
        //{
        //  description = "Lỗi cảm biến vào băng tải cân";
        //}
        //else if (eErrorCode == eErrorType.Error_sensor_weigh_OUT)
        //{
        //  description = "Lỗi cảm biến ra băng tải cân";
        //}
        //else if (eErrorCode == eErrorType.Error_IND570_Alarm)
        //{
        //  description = "Lỗi đầu cân IND570";
        //}
        //else if (eErrorCode == eErrorType.Error_IND570_Over_Cap)
        //{
        //  description = "Lỗi quá trọng lượng cân cho phép";
        //}
        //else if (eErrorCode == eErrorType.Error_Barcode_Fail)
        //{
        //  description = "Lỗi barcode IFM";
        //}
        //else if (eErrorCode == eErrorType.Error_Dynamic_OFF)
        //{
        //   description = "Lỗi mất tín hiệu cân động running";
        //}
        //else if (eErrorCode == eErrorType.Error_PS_Air_Fail)
        //{
        //  description = "Lỗi mất khí nén";
        //}
        //Error_Barcode_Fail,
        //Error_BOX_NG,
        //
        if (description != "")
        {
          bool IsFound = false;
          for (int i = 0; (i < this.glacialList1.Items.Count) && (IsFound == false); i++)
          {
            IsFound = (this.glacialList1.Items[i].SubItems[COL_ERROR_CODE].Text == error_code);
          }

          if (IsFound == false)
          {
            // bool IsAlreadyAdded = this.glacialList1.Items.
            /* Check if we already added */
            GLItem item = new GLItem();
            item.SubItems[COL_ID].Text = (this.glacialList1.Items.Count + 1).ToString();
            item.SubItems[COL_ERROR_CODE].Text = String.Format("{0}", error_code);
            item.SubItems[COL_ERROR_DESCRIPTION].Text = description;
            item.SubItems[COL_DATE_TIME].Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            this.glacialList1.Items.Insert(0, item);
            //save to database
            SaveAlarm((int)(eErrorCode), description);

          }
        }
      }/*if (currentError != previousError)*/
		}
		private AlarmType CreateAlarm(int alarm_code, string description)
		{
			//AlarmDB alarmDBsql = new AlarmDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
			eShift eCurShift = Shift.GetShiftFromClock();
			DateTime dt_now = Utils.GetDateTimeFromClockByShift();
			AlarmType alarm = new AlarmType
			{
				AlarmCode = alarm_code,
				Description = description,
				DateTime = dt_now.ToString("yyyy/MM/dd HH:mm:ss"),
				ShiftId = (int)(eCurShift)
			};
			return alarm;

		}

		private AlarmType SaveAlarm(int error_code, string description)
    {

      AlarmDB alarmDBsql = new AlarmDB(_configuration.TemplatePath, _configuration.DatabasePath, false);

      AlarmType alarm = CreateAlarm(error_code, description);
      alarmDBsql.Save(alarm);
      return alarm;

		}

    private List<AlarmType> list_errors = new List<AlarmType>();
		private List<AlarmType> list_warnings = new List<AlarmType>();

		private void UpdateDataErrorCodeAndWarningCode_old(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
			int error_code = machineData.PLC_Error_code.value.Convert_to_Int();
			int warning_code = machineData.PLC_WarningCode.value.Convert_to_Int();


      bool is_need_refreshh_list_view = (error_code != _error_code_previous) || (_warning_code_previous != warning_code);
			//
			if ((error_code == 0) && (warning_code == 0))
      {
				list_errors.Clear();
        list_warnings.Clear();
        //--------------------------------------------------
				if (this.glacialList1.Items.Count > 0)
        {
          this.glacialList1.Items.Clear();
          //save to _error_code_previous
          _error_code_previous = error_code;
          _warning_code_previous = warning_code;
          //
          this.glacialList1.Refresh();
        }
      }
      else
      {
      
        //error
        if (error_code > 0)
        {
					//có error mới
					//list_errors.Clear();

					//list_errors.Clear();
					//for (int i = 0; i < (int)eErrorType.End; i++)
     //     {
     //       bool current_error = error_code.ToBoolean(i);
        
					//	if (current_error == true)
					//	{
					//		eErrorType error_type = (eErrorType)(i);
					//		string description = Utils.GetErrorDescription(error_type);

					//		AlarmType error_alarm = CreateAlarm(i, description);
					//		if (error_alarm != null)
					//		{
					//			list_errors.Add(error_alarm);
					//		}
					//	}
					//}









					if (_error_code_previous != error_code)
          {
            //------------- duyệt qua từng bit error -------------
            for (int i = 0; i < (int)eErrorType.End; i++)
            {
              bool current_error = error_code.ToBoolean(i);
              bool previous_error = _error_code_previous.ToBoolean(i);
              //
              eErrorType error_type = (eErrorType)(i);

              //có bit error mới
              if (current_error != previous_error)
              {
								if ((previous_error == false) && (current_error == true))
                {
									if ((list_errors.Exists(x => x.AlarmCode == (int)(error_type))) == true)
									{
										//đã add trước đó --> do nothing
									}
									else
									{
										//chưa có --> thêm mới
										AlarmType error_saved = SaveErrorLogToDatabase(current_error, previous_error, error_type);
										if (error_saved != null)
										{
											list_errors.Add(error_saved);
										}
									}
								}
                //Remove nếu hết lỗi
                else if ((previous_error == true) && (current_error == false)  )
                {
                  int idx = -1;
                  for(int k =0;  k < list_errors.Count && (idx == -1);k++)
                  {
										if ((list_errors.Exists(x => x.AlarmCode == (int)(error_type))) == true)
                    {
                      idx = k;
                    }
									}
                  //remove
                  if (idx >= 0)
                  {
										list_errors.RemoveAt(idx);
									}
								}/* else if ((previous_error == true) && (current_error == false)  )*/
							}
            }/* for (int i = 0; i < (int)eErrorType.End; i++)*/


            _error_code_previous = error_code;

					}
          else
          {
            //do nothing
          }
				}
        else
        {
					list_errors.Clear();
				}

        //--------------warning
        if (warning_code > 0)
        {
					//list_warnings.Clear();

					if (_warning_code_previous != warning_code)
          {
						for (int i = 0; i < (int)eWarningType.End; i++)
            {
							bool current_warning = warning_code.ToBoolean(i);
							bool previous_warning = _warning_code_previous.ToBoolean(i);
							eWarningType warning_type = (eWarningType)(i);
							if ((previous_warning == false) && (current_warning == true))
							{
								if ((list_warnings.Exists(x => x.AlarmCode == (int)(warning_type))) == true)
								{
									//đã add trước đó --> do nothing
								}
								else
								{
									//chưa có --> thêm mới
									string description = Utils.GetWarningDescription(warning_type);
									AlarmType warning_alarm = CreateAlarm(i, description);
                  if (warning_alarm != null)
                  {
                    list_warnings.Add(warning_alarm);
                  }
                }
							}
							//Remove nếu hết lỗi
							else if ((previous_warning == true) && (current_warning == false))
							{
								int idx = -1;
								for (int k = 0; k < list_warnings.Count && (idx == -1); k++)
								{
									if ((list_warnings.Exists(x => x.AlarmCode == (int)(warning_type))) == true)
									{
										idx = k;
									}
								}
								//remove
								if (idx >= 0)
								{
									list_warnings.RemoveAt(idx);
								}
							}/* else if ((previous_error == true) && (current_error == false)  )*/





						}
						_warning_code_previous = warning_code;
						//     for (int i = 0; i < (int)eWarningType.End; i++)
						//     {
						//       bool current_warning = warning_code.ToBoolean(i);
						//       bool previous_warning = _warning_code_previous.ToBoolean(i);
						////
						//eWarningType warning_type = (eWarningType)(i);
						//if (current_warning != previous_warning)
						//{								
						//         if (current_warning == true)
						//         {
						//           string description = Utils.GetWarningDescription(warning_type);

						//           AlarmType warning_alarm = CreateAlarm(i, description);
						//           if (warning_alarm != null)
						//           {
						//             list_warnings.Add(warning_alarm);
						//           }
						//         }
						//	//if ((list_warnings.Exists(x => x.AlarmCode == (int)(warning_type))) == true)
						//	//{
						//	//	//do nothing
						//	//         //  if (current_warning == false) &&
						//	//}
						//	//else
						//	// {
						//	//  string description = Utils.GetWarningDescription(warning_type);

						//	//  AlarmType warning_alarm = CreateAlarm(i, description);
						//	//  if (warning_alarm != null)
						//	//  {
						//	//	  list_warnings.Add(warning_alarm);
						//	//  }
						//	// }
						//}
						//     }


					}
        }
        else
        {
          list_warnings.Clear();
        }
        //------------ display to list-view
        //delete old
        List<int> error_id_need_delete = new List<int>();
				List<int> warning_id_need_delete = new List<int>();

        if (is_need_refreshh_list_view)
        {
					this.glacialList1.Items.Clear();
					List<AlarmType> all_error_warnings = new List<AlarmType>();
					all_error_warnings.AddRange(list_errors);
					all_error_warnings.AddRange(list_warnings);


					for (int i = 0; i < all_error_warnings.Count; i++)
					{
						AlarmType alarm = all_error_warnings[i];
						GLItem item = new GLItem();
						item.SubItems[COL_ID].Text = (this.glacialList1.Items.Count + 1).ToString();
						item.SubItems[COL_ERROR_CODE].Text = String.Format("{0}", alarm.AlarmCode);
						item.SubItems[COL_ERROR_DESCRIPTION].Text = alarm.Description;
						item.SubItems[COL_DATE_TIME].Text = alarm.DateTime;
						this.glacialList1.Items.Insert(0, item);

					}
					////	if (is_need_refreshh_list_view)
					{
						this.glacialList1.Refresh();

					}
				}

        


			}
		}


    private void UpdateDataErrorCodeAndWarningCode(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      int error_code = machineData.PLC_Error_code.value.Convert_to_Int();
      int warning_code = machineData.PLC_WarningCode.value.Convert_to_Int();
      bool is_need_refreshh_list_view = (error_code != _error_code_previous) || (_warning_code_previous != warning_code);
      //backup listview
      List < AlarmType > alarms_backups = new List<AlarmType>();
      for (int i = 0; i < this.glacialList1.Items.Count; i++)
      {
        GLItem item = this.glacialList1.Items[i];
        AlarmType alarm = new AlarmType()
        {
          AlarmCode = item.SubItems[COL_ERROR_CODE].Text.Convert_to_Int(),
          DateTime = item.SubItems[COL_DATE_TIME].Text
        };
        alarms_backups.Add(alarm);
      }
      //-----------------------

      if (is_need_refreshh_list_view == true)
      {
        this.glacialList1.Items.Clear();

        List<AlarmType> all_alarms_warnings = new List<AlarmType>();

        for (int i = 0; i < (int)eErrorType.End; i++)
        {
          bool current_error = error_code.ToBoolean(i);
          if (current_error == true)
          {
            eErrorType error_type = (eErrorType)(i);
            string description = Utils.GetErrorDescription(error_type);
            AlarmType error_alarm = CreateAlarm(i, description);
            error_alarm.SetErrorAlarm(true);
            all_alarms_warnings.Add(error_alarm);
          }
        }
        for (int i = 0; i < (int)eWarningType.End; i++)
        {
          bool current_warning = warning_code.ToBoolean(i);

          if (current_warning == true)
          {
            eWarningType warning_type = (eWarningType)(i);
            string description = Utils.GetWarningDescription(warning_type);
            AlarmType warning_alarm = CreateAlarm(i, description);
            warning_alarm.SetErrorAlarm(false);
            all_alarms_warnings.Add(warning_alarm);
          }
        }
        //Add to listview
        for (int i = 0; i < all_alarms_warnings.Count; i++)
        {
          AlarmType alarm = all_alarms_warnings[i];
          //Search from backup to get datetime
          AlarmType alarm_from_backup = alarms_backups.Find(x => x.AlarmCode == alarm.AlarmCode);
          if (alarm_from_backup != null) // --> có alarm trước đó
          {
            alarm.DateTime = alarm_from_backup.DateTime;
          }
          else
          {
            //save database
            if (alarm.GetErrorAlarm() == true)
            {
              AlarmDB alarmDBsql = new AlarmDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
              alarmDBsql.Save(alarm);
            }
          }

          GLItem item = new GLItem();
          item.SubItems[COL_ID].Text = (this.glacialList1.Items.Count + 1).ToString();
          item.SubItems[COL_ERROR_CODE].Text = String.Format("{0}", alarm.AlarmCode);
          item.SubItems[COL_ERROR_DESCRIPTION].Text = alarm.Description;
          item.SubItems[COL_DATE_TIME].Text = alarm.DateTime;
          this.glacialList1.Items.Insert(0, item);

        }



        this.glacialList1.Refresh();
      }


      //save 
      if (error_code != _error_code_previous)
      {
        _error_code_previous = error_code;
      }
      if (warning_code != _warning_code_previous)
      {
        _warning_code_previous = warning_code;
      }
    }



    public List<AlarmType> DisplayAlarmToListView(DateTime dt, eShift shift, string templatePath, string databasePath)
    {
      //DateTime dt = Convert.ToDateTime(dateTimePicker3.Text);
      //eShift shift = (eShift)(cbShift.SelectedIndex);
      //
      AlarmDB sql = new AlarmDB(templatePath, databasePath, false);
      List<AlarmType> alarms = new List<AlarmType>();
      if (shift == eShift.SHIFT_ALL)
      {
        alarms = sql.LoadAllByDateTime(dt);
      }
      else
      {
        alarms = sql.LoadAllByDateShift(dt, (int)(shift));
      }


      this.glacialList1.Items.Clear();
      foreach (AlarmType alarm in alarms)
      {
        GLItem item = new GLItem();

        item.SubItems[COL_ID].Text = alarm.id.ToString();
        item.SubItems[COL_ERROR_CODE].Text = alarm.AlarmCode.ToString();
        item.SubItems[COL_ERROR_DESCRIPTION].Text = alarm.Description;
        item.SubItems[COL_DATE_TIME].Text = alarm.DateTime;
        //
        this.glacialList1.Items.Insert(0, item);
      }
      this.glacialList1.Refresh();
      return alarms;

		}

    private void UpdateDataWarningCode(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      int warning_code = machineData.PLC_WarningCode.value.Convert_to_Int();
      //
      if (warning_code == 0)  /* No error --> clear request */
      {
        if (this.glacialList1.Items.Count > 0)
        {
          this.glacialList1.Items.Clear();
          //save to _warning_code_previous
          _warning_code_previous = warning_code;
          this.glacialList1.Refresh();
        }
      }
      else
      {
        if (_warning_code_previous != warning_code)
        {
          for (int i = 0; i < (int)eWarningType.End; i++)
          {
            bool current_warning = warning_code.ToBoolean(i);
            bool previous_warning = _warning_code_previous.ToBoolean(i);
            //
            AddWarningLogToListView(current_warning, previous_warning, (eWarningType)(i));
          }
          //
          this.glacialList1.Refresh();
          //
          //save to _error_code_previous
          _warning_code_previous = warning_code;
        }

      }
    }


    //private void Add
    /// <param name="machineData"></param>
    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      UpdateDataErrorCodeAndWarningCode(rawdata, machineData);
      //UpdateDataWarningCode(rawdata, machineData);
    }

    //private void btAlarmReset_Click(object sender, EventArgs e)
    //{
    //  if (OnRequestAlarmReset != null)
    //  {
    //    OnRequestAlarmReset(this);
    //  }
    //}

    private void btAlarmReset_1_Click(object sender, EventArgs e)
    {
        if (OnRequestAlarmReset != null)
        {
            OnRequestAlarmReset(this);
        }
    }
  }
}
