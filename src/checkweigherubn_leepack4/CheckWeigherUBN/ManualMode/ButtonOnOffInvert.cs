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
  public partial class ButtonOnOffInvert : UserControl
  {
    private bool IsEnableUpdateAutoManual = true;
    //
    public delegate void SendModeChangeTo(object sender, eDeviceType eDeviceType, eMode eSwitchModeRequest);
    public event SendModeChangeTo OnSendModeChangeTo;


    private eDeviceType _eDeviceType = eDeviceType.UNKNOW;
    private eMode _eButtonCurrentMode = eMode.AUTO;
    private ConfigurationTypes _configuration;
    public ButtonOnOffInvert()
    {
      InitializeComponent();
    }

    public void SetTitle(string title)
    {
      this.lblTitle.Text = title;
    }

		public void SetImg(string nameImg)
		{
			this.btStartStop.Image = Image.FromFile($"{Application.StartupPath}\\Image\\{nameImg}"); 
		}


		public void SetDevice(eDeviceType eDevice)
    {
      _eDeviceType = eDevice;
    }

    public void SetConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
    }


    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      if (IsEnableUpdateAutoManual == true)
      {
        int machineStatus = machineData.PLC_ControlStatus.value.Convert_to_Int();
        if (_eDeviceType == eDeviceType.BUTTON_CYLINDER_REJECT)
        {
          bool status = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Reject_SW_ON);
          if (status == true)
          {
						//this.btStartStop.Image = global::CheckWeigherUBN.Properties.Resources.STOP_Button_1_OFF;
						this.btStartStop.Image = Image.FromFile($"{Application.StartupPath}\\Image\\PUSH_OFF.png");
						_eButtonCurrentMode = eMode.BUTTON_CYLINDER_REJECT_ON;
          }
          else
          {
            //this.btStartStop.Image = global::CheckWeigherUBN.Properties.Resources.START_Button_1_ON;
						this.btStartStop.Image = Image.FromFile($"{Application.StartupPath}\\Image\\PUSH_ON.png");
						_eButtonCurrentMode = eMode.BUTTON_CYLINDER_REJECT_OFF;
          }
        }
        else
        {
          bool IsReject_Test = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Reject_Enable_Disable);
          if (IsReject_Test == true)
          {
            _eButtonCurrentMode = eMode.BUTTON_CYLINDER_REJECT_ON;
            this.btStartStop.Image = global::CheckWeigherUBN.Properties.Resources.STOP_Button_1_OFF;
          }
          else
          {
            _eButtonCurrentMode = eMode.BUTTON_CYLINDER_REJECT_OFF;
            this.btStartStop.Image = global::CheckWeigherUBN.Properties.Resources.START_Button_1_ON;
          }
        }
      }
    }




    private void btStartStop_Click(object sender, EventArgs e)
    {
      IsEnableUpdateAutoManual = false;
      if (_eDeviceType == eDeviceType.BUTTON_CYLINDER_REJECT)
      {
        if (_eButtonCurrentMode == eMode.BUTTON_CYLINDER_REJECT_ON)
        {
          if (OnSendModeChangeTo != null)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.BUTTON_CYLINDER_REJECT_OFF);
          }
        }
        else if (_eButtonCurrentMode == eMode.BUTTON_CYLINDER_REJECT_OFF)
        {
          if (OnSendModeChangeTo != null)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.BUTTON_CYLINDER_REJECT_ON);
          }
        }
      }
      else if ((_eDeviceType == eDeviceType.BUTTON_CYLINDER_REJECT) && (CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_Disable_Cylinder)))
      {
        if (_eButtonCurrentMode == eMode.BUTTON_CYLINDER_REJECT_ON)
        {
          if (OnSendModeChangeTo != null)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.BUTTON_CYLINDER_REJECT_OFF);
          }
        }
        else if (_eButtonCurrentMode == eMode.BUTTON_CYLINDER_REJECT_OFF)
        {
          if (OnSendModeChangeTo != null)
          {
            OnSendModeChangeTo(this, _eDeviceType, eMode.BUTTON_CYLINDER_REJECT_ON);
          }
        }
      }
      else
      {
      }
      this.timer_delay.Enabled = true;
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
