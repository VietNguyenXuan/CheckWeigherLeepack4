using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
  public partial class SwitchManManual : UserControl
  {
    public delegate void SendModeChangeTo(object sender, eDeviceType eDeviceType, eMode eSwitchModeRequest);
    public event SendModeChangeTo OnSendModeChangeTo;

    private bool IsEnableUpdateAutoManual = true;
    //private 
    private eMode _eSwitchMode = eMode.AUTO;
    private eDeviceType _eDeviceType = eDeviceType.SWITCH_BUZZER;
    private ConfigurationTypes _configuration;
    public SwitchManManual()
    {
      InitializeComponent();
    }

    public void SetTitle(string title)
    {
      this.lblTitle.Text = title;
    }

    public void SetDevice(eDeviceType eDeviceType)
    {
      _eDeviceType = eDeviceType;
      if (_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_AUTO_ASSIGN_CO)
      {
        _eSwitchMode = eMode.AUTO_ASSIGN_CO_DISABLE;
        this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_DISABLE;
      }
      //if (_eSwitchMode == eMode.AUTO_ASSIGN_CO_DISABLE)
    }
    public void SetConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
    }

    /// <summary>
    /// Receive status from PLC
    /// </summary>
    /// <param name="rawdata"></param>
    /// <param name="machineData"></param>
    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      int machineStatus = machineData.PLC_ControlStatus.value.Convert_to_Int();
      if (IsEnableUpdateAutoManual == true)
      {
        if (_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_REJECT)
        {
          bool IsPLC_Reject_SW_ON = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Reject_SW_ON);//
          if (IsPLC_Reject_SW_ON == true)
          {
            _eSwitchMode = eMode.REJECT_ENABLE;
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_ENABLE;
          }
          else
          {
            _eSwitchMode = eMode.REJECT_DISABLE;
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_DISABLE;
          }
        }
        else if (_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_WEIGHER)
        {
          bool IsPLC_Weigher_Disable = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Weigher_Disable);
          if (IsPLC_Weigher_Disable == false)
          {
            _eSwitchMode = eMode.WEIGHER_DISABLE;
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_DISABLE;
          }
          else
          {
            _eSwitchMode = eMode.WEIGHER_ENABLE;
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_ENABLE;
          }
        }
        else if (_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_BARCODE)
        {
          bool IsPLC_Barcode_Disable = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Barcode_Disable);
          if (IsPLC_Barcode_Disable == true)
          {
            _eSwitchMode = eMode.BARCODE_DISABLE;
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_DISABLE;
          }
          else
          {
            _eSwitchMode = eMode.BARCODE_ENABLE;
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_ENABLE;
          }
        }
        else if (_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_AUTO_ASSIGN_CO)
        {
          bool Is_PLC_SwAutoManChangeoverByALC_Request = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_SwAutoManChangeoverByALC_Request);
          if (Is_PLC_SwAutoManChangeoverByALC_Request == false)
          {
            _eSwitchMode = eMode.AUTO_ASSIGN_CO_DISABLE;
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_DISABLE;
          }
          else
          {
            _eSwitchMode = eMode.AUTO_ASSIGN_CO_ENABLE;
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_ENABLE;
          }
        }

        else if (_eDeviceType == eDeviceType.SWITCH_AUTO_MAN)
        {
          bool IsManMode = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Man_mode);
          if (IsManMode.ConvertToSystemMode() == eMode.AUTO)
          {
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Button_MAN;
            _eSwitchMode = eMode.MANUAL;
          }
          else
          {
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Button_AUTO;
            _eSwitchMode = eMode.AUTO;
          }
        }
        else if (_eDeviceType == eDeviceType.SWITCH_BUZZER)
        {
          bool statusBuzzer = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Buzzer_Off);
          if (statusBuzzer == true)
          {
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_OFF;
            _eSwitchMode = eMode.BUZZER_OFF;
          }
          else
          {
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_ON;
            _eSwitchMode = eMode.BUZZER_ON;
          }
        }
        //_eDeviceType
        else if (_eDeviceType == eDeviceType.BUTTON_CYLINDER_REJECT)
        {
          bool statusBuzzer = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Reject_SW_ON);
          if (statusBuzzer == true)
          {
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_ON;
            _eSwitchMode = eMode.BUTTON_CYLINDER_REJECT_ON;
          }
          else
          {
            this.btSwitchOnOff.Image = global::CheckWeigherUBN.Properties.Resources.Switch_OFF;
            _eSwitchMode = eMode.BUTTON_CYLINDER_REJECT_OFF;
          }
        }
      }
    }

    private void btSwitchOnOff_Click(object sender, EventArgs e)
    {
      IsEnableUpdateAutoManual = false;
      if (OnSendModeChangeTo != null)
      {
        if ((_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_REJECT)&& (CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_Disable_Cylinder)))
        {
          if (_eSwitchMode == eMode.REJECT_ENABLE)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.REJECT_DISABLE);
          }
          else if (_eSwitchMode == eMode.REJECT_DISABLE)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.REJECT_ENABLE);
          }
        }
        else if ((_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_WEIGHER) && (CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_Disable_checkweigher)))
        {
          if (_eSwitchMode == eMode.WEIGHER_ENABLE)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.WEIGHER_DISABLE);
          }
          else if (_eSwitchMode == eMode.WEIGHER_DISABLE)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.WEIGHER_ENABLE);
          }
        }
        else if ((_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_BARCODE) && (CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_Disable_Buzzer)))
        {
          if (_eSwitchMode == eMode.BARCODE_ENABLE)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.BARCODE_DISABLE);
          }
          else if (_eSwitchMode == eMode.BARCODE_DISABLE)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.BARCODE_ENABLE);
          }
        }
        //SWITCH_ENABLE_DISABLE_AUTO_ASSIGN_CO
        else if ((_eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_AUTO_ASSIGN_CO) && (CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_Disable_Buzzer)))
        {
          if (_eSwitchMode == eMode.AUTO_ASSIGN_CO_ENABLE)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.AUTO_ASSIGN_CO_DISABLE);
          }
          else if (_eSwitchMode == eMode.AUTO_ASSIGN_CO_DISABLE)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.AUTO_ASSIGN_CO_ENABLE);
          }
        }
        else if ((_eDeviceType == eDeviceType.SWITCH_AUTO_MAN) && (CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_CHuyen_che_do_man_auto_va_chay_che_do_auto)))
        {
          if (_eSwitchMode == eMode.MANUAL)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.AUTO);
          }
          else if (_eSwitchMode == eMode.AUTO)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.MANUAL);
          }
        }
        else if( (_eDeviceType == eDeviceType.SWITCH_BUZZER) && (CheckPemission(ePemission.MAIN_Tat_Buzzer)))
        {
					if (_eSwitchMode == eMode.BUZZER_OFF)
					{
						OnSendModeChangeTo(this, _eDeviceType, eMode.BUZZER_OFF);
					}
					else if (_eSwitchMode == eMode.BUZZER_ON)
					{
						OnSendModeChangeTo(this, _eDeviceType, eMode.BUZZER_ON);
					}
					//if (_eSwitchMode == eMode.BUZZER_ON)
					//{
					//  OnSendModeChangeTo(this, _eDeviceType, eMode.BUZZER_OFF);
					//}
					//else if (_eSwitchMode == eMode.BUZZER_OFF)
					//{
					//  OnSendModeChangeTo(this, _eDeviceType, eMode.BUZZER_ON);
					//}
				}
        
        else if (_eDeviceType == eDeviceType.BUTTON_CYLINDER_REJECT)
        {
          if (_eSwitchMode == eMode.BUTTON_CYLINDER_REJECT_ON)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.BUTTON_CYLINDER_REJECT_OFF);
          }
          else if (_eSwitchMode == eMode.BUTTON_CYLINDER_REJECT_OFF)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.BUTTON_CYLINDER_REJECT_ON);
          }
        }

        //
        timer_delay.Enabled = true;
      }
    }
    

    private void timer_delay_Tick(object sender, EventArgs e)
    {
      this.timer_delay.Enabled = false;
      IsEnableUpdateAutoManual = true;
    }

    private bool CheckPemission(ePemission ePemission)
    {
      bool ret = Utils.CheckPemission(_configuration, ePemission);
      if (ret == false)
      {
        ShowMessageLogin();
      }
      return ret;
    }
    private void ShowMessageLogin()
    {
      MessageBox.Show("Bạn không được phép truy cập trang này", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }
}
