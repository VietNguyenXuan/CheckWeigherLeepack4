#define FORCE_MANUAL_MODE
#define CHECK_WEIGHER_YUJENG_BOTTLE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
  public partial class frmManual : Form
  {
    public delegate void SendBufferDataChanged(object sender, int buffer_id, int value);
    public event SendBufferDataChanged OnSendBufferDataChanged;
    //
    public delegate void SendRequestDemoMode(object sender);
    public event SendRequestDemoMode OnSendRequestDemoMode;
    //
    public delegate void SendRequestStartStopPB(object sender, eDeviceType eConveyor, eConveyorStatus conveyorStatusRequest);
    public event SendRequestStartStopPB OnSendRequestStartStopPB;

    //
    public delegate void SendSpeedValueSetup(object sender, eDeviceType eConveyor, int value);
    public event SendSpeedValueSetup OnSendSpeedValueSetup;
    //
    public delegate void SendModeChangeTo(object sender, eDeviceType eDeviceType, eMode eSwitchmode);
    public event SendModeChangeTo OnSendModeChangeTo;



    //
    private ConfigurationTypes _configuration = null;
    private CheckWeigherUBN.ManualMode.DisplayPLCMemory displayPLCMemory = null;
    private bool _is_1_huong_load_done = false;
    private bool _is_2_huong_load_done = false;

    private Timer _delay_update_direction = new Timer();
    private bool _is_enable_update_status = true;

    private bool _is_privious_direction = false;
    public frmManual(ConfigurationTypes configuration)
    {
      InitializeComponent();

      _delay_update_direction.Interval = 250;
      _delay_update_direction.Tick += _delay_update_direction_Tick;

      this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
      this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);

      _configuration = configuration;
      this.startStopButton_BT_TACH_CHAI.SetConveyor(eDeviceType.BT_TACH_CHAI);
      this.startStopButton_BT_CAN.SetConveyor(eDeviceType.BT_CAN);
      this.startStopButton_BT_REJECT.SetConveyor(eDeviceType.BT_REJECT);
      this.startStopConveyorALL.SetConveyor(eDeviceType.BT_ALL);
      //
      this.switchManManual1.SetTitle("PLC Auto Mode");
      this.switchManManual1.SetDevice(eDeviceType.SWITCH_AUTO_MAN);
      this.switchManManual1.SetConfiguration(_configuration);
      //
      this.switchBuzzerTest.SetTitle("Buzzer");
      this.switchBuzzerTest.SetDevice(eDeviceType.SWITCH_BUZZER);
      this.switchBuzzerTest.SetConfiguration(_configuration);
      //
#if CHECK_WEIGHER_YUJENG_BOTTLE
      this.switchEnableDisableAutoAssignChangeover.SetTitle("Reject");
      this.switchEnableDisableAutoAssignChangeover.SetDevice(eDeviceType.SWITCH_ENABLE_DISABLE_AUTO_ASSIGN_CO);
      this.switchEnableDisableAutoAssignChangeover.SetConfiguration(_configuration);
#else
      this.switchEnableDisableAutoAssignChangeover.SetTitle("Barcode");
      this.switchEnableDisableAutoAssignChangeover.SetDevice(eDeviceType.SWITCH_ENABLE_DISABLE_BARCODE);
      this.switchEnableDisableAutoAssignChangeover.SetConfiguration(_configuration);
#endif
      //
      this.switchEnableDisableWeigher.SetTitle("Weigher");
      this.switchEnableDisableWeigher.SetDevice(eDeviceType.SWITCH_ENABLE_DISABLE_WEIGHER);
      this.switchEnableDisableWeigher.SetConfiguration(_configuration);
      //
      this.switchEnableDisableReject.SetTitle("Reject");
      this.switchEnableDisableReject.SetDevice(eDeviceType.SWITCH_ENABLE_DISABLE_REJECT);
      this.switchEnableDisableReject.SetConfiguration(_configuration);
      //
      this.switchCyclinderOnOff.SetTitle("Cylinder reject");
      this.switchCyclinderOnOff.SetDevice(eDeviceType.BUTTON_CYLINDER_REJECT);
      this.switchCyclinderOnOff.SetConfiguration(_configuration);
      //
      this.buttonCyclinderOnOff.SetTitle("Cylinder reject");
      this.buttonCyclinderOnOff.SetImg("PUSH_ON.png");
      this.buttonCyclinderOnOff.SetDevice(eDeviceType.BUTTON_CYLINDER_REJECT);
      this.buttonCyclinderOnOff.SetConfiguration(_configuration);





      //
      this.startStopButton_BT_TACH_CHAI.OnSendSpeedValueSetup += StartStopButton_OnSendSpeedValueSetup;
      this.startStopButton_BT_CAN.OnSendSpeedValueSetup += StartStopButton_OnSendSpeedValueSetup;
      this.startStopButton_BT_REJECT.OnSendSpeedValueSetup += StartStopButton_OnSendSpeedValueSetup;
      this.startStopConveyorALL.OnSendSpeedValueSetup += StartStopButton_OnSendSpeedValueSetup;
      //
      this.startStopButton_BT_TACH_CHAI.OnSendRequestStartStopPB += StartStopButton_OnSendRequestStartStopPB;
      this.startStopButton_BT_CAN.OnSendRequestStartStopPB += StartStopButton_OnSendRequestStartStopPB;
      this.startStopButton_BT_REJECT.OnSendRequestStartStopPB += StartStopButton_OnSendRequestStartStopPB;
      this.startStopConveyorALL.OnSendRequestStartStopPB += StartStopButton_OnSendRequestStartStopPB;

      //
      this.switchManManual1.OnSendModeChangeTo += Switch_OnSendModeChangeTo;
      this.switchBuzzerTest.OnSendModeChangeTo += Switch_OnSendModeChangeTo;
      this.switchEnableDisableAutoAssignChangeover.OnSendModeChangeTo += Switch_OnSendModeChangeTo;
      this.switchEnableDisableWeigher.OnSendModeChangeTo += Switch_OnSendModeChangeTo;
      this.switchEnableDisableReject.OnSendModeChangeTo += Switch_OnSendModeChangeTo;
      //
      this.switchCyclinderOnOff.OnSendModeChangeTo += Switch_OnSendModeChangeTo;
      //
      this.buttonCyclinderOnOff.OnSendModeChangeTo += Switch_OnSendModeChangeTo;
      //
      if (_configuration.currentUserLogin != null)
      {
        if (_configuration.currentUserLogin.RoleId == (int)(UserType.eRole.Admin))
        {
          this.btDemoMode.Visible = true;
          this.btViewPLCMemory.Visible = true;
        }
      }
      //
      this.bufferSettings1.SetId(1);
      this.bufferSettings2.SetId(2);
      this.bufferSettings3.SetId(3);
      this.bufferSettings4.SetId(4);
      this.bufferSettings5.SetId(5);
      this.bufferSettings6.SetId(6);
      this.bufferSettings7.SetId(7);
      this.bufferSettings8.SetId(8);
      //
      this.bufferSettings1.OnSendBufferDataChanged += BufferSettings_OnSendBufferDataChanged;
      this.bufferSettings2.OnSendBufferDataChanged += BufferSettings_OnSendBufferDataChanged;
      this.bufferSettings3.OnSendBufferDataChanged += BufferSettings_OnSendBufferDataChanged;
      this.bufferSettings4.OnSendBufferDataChanged += BufferSettings_OnSendBufferDataChanged;
      this.bufferSettings5.OnSendBufferDataChanged += BufferSettings_OnSendBufferDataChanged;
      this.bufferSettings6.OnSendBufferDataChanged += BufferSettings_OnSendBufferDataChanged;
      this.bufferSettings7.OnSendBufferDataChanged += BufferSettings_OnSendBufferDataChanged;
      this.bufferSettings8.OnSendBufferDataChanged += BufferSettings_OnSendBufferDataChanged;
    }

    private void _delay_update_direction_Tick(object sender, EventArgs e)
    {
      this._delay_update_direction.Enabled = false;
      _is_enable_update_status = true;
    }

    private void BufferSettings_OnSendBufferDataChanged(object sender, int buffer_id, int value)
    {
      if (OnSendBufferDataChanged != null)
      {
        OnSendBufferDataChanged(this, buffer_id, value);
      }
    }

    private void StartStopButton_OnSendRequestStartStopPB(object sender, eDeviceType eConveyor, eConveyorStatus conveyorStatusRequest)
    {
      if (OnSendRequestStartStopPB != null)
      {
        OnSendRequestStartStopPB(sender, eConveyor, conveyorStatusRequest);
      }
    }

    private void Switch_OnSendModeChangeTo(object sender, eDeviceType eDeviceType, eMode eSwitchRequest)
    {
      if (OnSendModeChangeTo != null)
      {
        OnSendModeChangeTo(sender, eDeviceType, eSwitchRequest);
      }
    }

    private void StartStopButton_OnSendSpeedValueSetup(object sender, eDeviceType eConveyor, int value)
    {
      if (OnSendSpeedValueSetup != null)
      {
        OnSendSpeedValueSetup(sender, eConveyor, value);
      }
    }

    

    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      this.startStopButton_BT_TACH_CHAI.UpdateData(rawdata, machineData);
      this.startStopButton_BT_CAN.UpdateData(rawdata, machineData);
      this.startStopButton_BT_REJECT.UpdateData(rawdata, machineData);
      this.startStopConveyorALL.UpdateData(rawdata, machineData);
      this.switchManManual1.UpdateData(rawdata, machineData);
      this.switchBuzzerTest.UpdateData(rawdata, machineData);
      this.switchEnableDisableAutoAssignChangeover.UpdateData(rawdata, machineData);
      this.switchEnableDisableWeigher.UpdateData(rawdata, machineData);

      //--------------------------------------
      this.switchEnableDisableReject.UpdateData(rawdata, machineData);
      this.switchCyclinderOnOff.UpdateData(rawdata, machineData);
      this.buttonCyclinderOnOff.UpdateData(rawdata, machineData);
      //

      //
      int machineStatus = machineData.PLC_ControlStatus.value.Convert_to_Int();
      bool IsManMode = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Man_mode);
      eMode eSystemMode = IsManMode.ConvertToSystemMode();
#if FORCE_MANUAL_MODE
      this.startStopButton_BT_TACH_CHAI.Enabled = true;
      this.startStopButton_BT_CAN.Enabled = true;
      this.startStopButton_BT_REJECT.Enabled = true;
      this.startStopConveyorALL.Enabled = true;
#else
      if (eSystemMode == eMode.MANUAL)
      {
        this.startStopButton_BT_TACH_CHAI.Enabled = true;
        this.startStopButton_BT_CAN.Enabled = true;
        this.startStopButton_BT_REJECT.Enabled = true;
        this.startStopConveyorALL.Enabled = false;
      }
      else
      {
        this.startStopButton_BT_TACH_CHAI.Enabled = false;
        this.startStopButton_BT_CAN.Enabled = false;
        this.startStopButton_BT_REJECT.Enabled = false;
        this.startStopConveyorALL.Enabled = true;
      }
#endif
      this.bufferSettings1.UpdateData(rawdata, machineData);
      this.bufferSettings2.UpdateData(rawdata, machineData);
      this.bufferSettings3.UpdateData(rawdata, machineData);
      this.bufferSettings4.UpdateData(rawdata, machineData);
      this.bufferSettings5.UpdateData(rawdata, machineData);
      this.bufferSettings6.UpdateData(rawdata, machineData);
      this.bufferSettings7.UpdateData(rawdata, machineData);
      this.bufferSettings8.UpdateData(rawdata, machineData);
      //------

      if (_is_enable_update_status == true)
      {
        bool Is_2_huong = machineStatus.ToBoolean((int)ePLC_ControlStatus.PC_Select_Reject_Type);
        if (_is_privious_direction != Is_2_huong)
        {
          radioButton1.Checked = (Is_2_huong == false);
          radioButton2.Checked = (Is_2_huong == true);

          _is_privious_direction = Is_2_huong;
        }
      }








      //
      if (displayPLCMemory != null)
      {
        displayPLCMemory.UpdateData(rawdata, machineData);
      }
    }


    private void btExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void frmManual_FormClosed(object sender, FormClosedEventArgs e)
    {
      Utils utils = new Utils();
      utils.CloseKeyboardOSK();
    }

    private void frmManual_Move(object sender, EventArgs e)
    {
      UpdateLocation();
    }

    private void UpdateLocation()
    {
      //
      this.startStopButton_BT_TACH_CHAI.UpdateLocation(this.Location.X + startStopButton_BT_TACH_CHAI.Location.X, this.Location.Y + startStopButton_BT_TACH_CHAI.Location.Y);
      this.startStopButton_BT_CAN.UpdateLocation(this.Location.X + startStopButton_BT_CAN.Location.X, this.Location.Y + startStopButton_BT_CAN.Location.Y);
      this.startStopButton_BT_REJECT.UpdateLocation(this.Location.X + startStopButton_BT_REJECT.Location.X, this.Location.Y + startStopButton_BT_REJECT.Location.Y);
      this.startStopConveyorALL.UpdateLocation(this.Location.X + startStopConveyorALL.Location.X, this.Location.Y + startStopConveyorALL.Location.Y);
      //
      //this.bufferSettings1.UpdateLocation(this.Location.X + bufferSettings1.Location.X,  bufferSettings1.Location.Y);
      //this.bufferSettings2.UpdateLocation(this.Location.X + bufferSettings2.Location.X, this.panel1.Location.Y + bufferSettings2.Location.Y);
      //this.bufferSettings3.UpdateLocation(this.Location.X + bufferSettings3.Location.X, this.panel1.Location.Y + bufferSettings3.Location.Y);
      //this.bufferSettings4.UpdateLocation(this.Location.X + bufferSettings4.Location.X, this.panel1.Location.Y + bufferSettings4.Location.Y);
      //this.bufferSettings5.UpdateLocation(this.Location.X + bufferSettings5.Location.X, this.panel1.Location.Y + bufferSettings5.Location.Y);
      //this.bufferSettings6.UpdateLocation(this.Location.X + bufferSettings6.Location.X, this.panel1.Location.Y + bufferSettings6.Location.Y);

    }

    private void frmManual_Load(object sender, EventArgs e)
    {
      UpdateLocation();
      this.btViewPLCMemory.Visible = false;
			this.btDemoMode.Visible = false;
		}

    private void btDemoMode_Click(object sender, EventArgs e)
    {
      if (OnSendRequestDemoMode != null)
      {
        OnSendRequestDemoMode(this);
      }
    }

    private void switchEnableDisableAutoAssignChangeover_Load(object sender, EventArgs e)
    {

    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      
      if (_is_1_huong_load_done == true)
      {
        if (radioButton1.Checked == true)
        {
          _is_enable_update_status = false;
          this._delay_update_direction.Enabled = true;
          //
          if (OnSendModeChangeTo != null)//RADIO_CHON_HUONG_REJECT
          {
            OnSendModeChangeTo(this, eDeviceType.RADIO_CHON_HUONG_REJECT, eMode.RADIO_CHON_HUONG_REJECT_1_HUONG);
          }
        }
        
      }
      else
      {
        _is_1_huong_load_done = true;
       
      }
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      //if (_is_2_huong_load_done == true)
      {
        if (radioButton2.Checked == true)
        {
          _is_enable_update_status = false;
          this._delay_update_direction.Enabled = true;
          if (OnSendModeChangeTo != null)//RADIO_CHON_HUONG_REJECT
          {
            OnSendModeChangeTo(this, eDeviceType.RADIO_CHON_HUONG_REJECT, eMode.RADIO_CHON_HUONG_REJECT_2_HUONG);
          }
        }
      }
      //else
      //{
      //  _is_2_huong_load_done = true;
      //}
    }

    //private void ActtiveCheckedChanged()
    //{
    //  if (radioButton1.Checked == true)
    //  {
    //    if(OnSendModeChangeTo != null)//RADIO_CHON_HUONG_REJECT
    //    {
    //      OnSendModeChangeTo(this, eDeviceType.RADIO_CHON_HUONG_REJECT, eMode.RADIO_CHON_HUONG_REJECT_1_HUONG );
    //    }
    //  }
    //  else
    //  {
    //    if (OnSendModeChangeTo != null)//RADIO_CHON_HUONG_REJECT
    //    {
    //      OnSendModeChangeTo(this, eDeviceType.RADIO_CHON_HUONG_REJECT, eMode.RADIO_CHON_HUONG_REJECT_2_HUONG);
    //    }
    //  }
    //}

    private void btViewPLCMemory_Click(object sender, EventArgs e)
    {
#if usePlcServer
      if (displayPLCMemory == null)
      {
        displayPLCMemory = new ManualMode.DisplayPLCMemory();
        displayPLCMemory.Show();
        displayPLCMemory.BringToFront();
      }
      else
      {
        displayPLCMemory.BringToFront();
      }
#endif
      
    }
  }
}
