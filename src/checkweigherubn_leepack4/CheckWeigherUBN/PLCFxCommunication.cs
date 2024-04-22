using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TcpComm;
namespace CheckWeigherUBN
{
  public class PLCFxCommunication
  {
    #region Events
    public delegate void ReadDataDone(object sender, PLCFx5U_RawData rawdata, PLC_MachineData machineData);
    public event ReadDataDone OnReadDataDone;

    public delegate void NotifyCommunicationStatus(object sender, STATUS status);
    public event NotifyCommunicationStatus OnNotifyCommunicationStatus;
    #endregion

    private System.Windows.Forms.Timer timer_communication_with_plc;

    private eDATA_GROUP_HMI_Data _maxGroupEND = eDATA_GROUP_HMI_Data.DATA_GROUP_END;
    private ushort PortNumber = 2000;
    private string PLC_IPAddress = "";

    private bool _IsUsingTimerSend = false;

    private int timer_to_check_timeout = 0;
    //
    private TcpComm.TcpCommUC tcpCommUC1 = new TcpComm.TcpCommUC();
    //
    private eDATA_GROUP_HMI_Data _current_group_DWORD = eDATA_GROUP_HMI_Data.DATA_GROUP_0;


    RequestRegister _write_register = new RequestRegister();

    List<RequestRegister> list_UserRequestCmd = new List<RequestRegister>();
    private const int MAX_WRITE_REGISTER = 10;

    private eProtocol _eProtocol = eProtocol.Mitsubishi_Ethernet_SNTP;
    //
    /// <summary>
    /// Địa chỉ Word bắt đầu
    /// </summary>
    private const int START_DEVICE_TO_READ = 1000; //D1000
    /// <summary>
    /// Số lượng Word
    /// </summary>
    private const int MAX_LENGTH = 50 * (int)(eDATA_GROUP_HMI_Data.DATA_GROUP_END); //100

    private PLCFx5U_RawData PLC_Rawdata = new PLCFx5U_RawData(START_DEVICE_TO_READ, MAX_LENGTH);
    private PLC_MachineData PLC_Machinedata = new PLC_MachineData(START_DEVICE_TO_READ);

    /// <summary>
    /// Số word cho phép read từ PLC
    /// </summary>
    private const int ETHERNET_SNMP_MAX_WORD = 50;
    //
    private System.Windows.Forms.Timer timer_sendFx5U = new System.Windows.Forms.Timer();

    private const int INVALID_DATA = (-1);


    public struct RequestRegister
    {
      public int id;
      public eREQUEST_MODE _registerWriteCmd;
      public int _lineId;
      public int _machineIdTobeRequested;
      public int _dataToWrite;
      public int[] _blockDataToWrite;
    }

    public enum eDataWrite
    {
      SINGLE,
      BLOCK,
    }
    public enum eProtocol
    {
      Mitsubishi_Serial = 1,
      Siemens_TCPIP = 2,
      Mitsubishi_Ethernet_SNTP = 3,
      Kepware_OPC_Server = 4,
      MC_Protocol = 5
    }


    public void StartCommunication()
    {
#if usePlcServer
      tcpCommUC1.Init("113.161.93.162", 100, ETHERNET_PROTOCOL.SLMP_BINARY_CODES, MASTER_SLAVE.MASTER);
#else
      tcpCommUC1.Init(PLC_IPAddress, PortNumber, ETHERNET_PROTOCOL.SLMP_BINARY_CODES, MASTER_SLAVE.MASTER);
#endif
    }

    //public void ReadHoldingRegister()
    //{
    //  tcpCommUC1.ReadHoldingRegister();
    //}


    public PLCFxCommunication(string plcIpAddress, ushort portNumber)
    {
      PLC_IPAddress = plcIpAddress;
      PortNumber = portNumber;
      //
      timer_communication_with_plc = new System.Windows.Forms.Timer();
      this.timer_communication_with_plc.Interval = 500;
      this.timer_communication_with_plc.Tick += new System.EventHandler(this.timer_communication_with_plc_Tick);
      //
      tcpCommUC1.SetIndex(1);
      tcpCommUC1.OnNotifyStatus += new TcpComm.TcpCommUC.NotifyStatus(tcpCommUC1_OnNotifyStatus);
      tcpCommUC1.OnReadDeviceData += new TcpCommUC.ReadDeviceData(tcpCommUC1_OnReadDeviceData);
      //
      timer_sendFx5U.Tick += Timer_sendFx5U_Tick;
      //timer_sendFx5U.Interval = 25;
      timer_sendFx5U.Interval = 15;
      //We create 100 WriteRegister register = new WriteRegister();
      for (int i = 0; i < MAX_WRITE_REGISTER; i++)
      {
        RequestRegister register = new RequestRegister();
        register.id = i;
        register._registerWriteCmd = eREQUEST_MODE.NONE; //Init
        register._dataToWrite = 0;
        //register._machineIdTobeRequested = 0;
        //register._lineId = 0;
        
        //
        list_UserRequestCmd.Add(register);
      }
    }


    public void Retry()
    {
      //
      ResetCurrentRequestRegister();
      //
      _current_group_DWORD = eDATA_GROUP_HMI_Data.DATA_GROUP_0;
      _current_group_DWORD = SendDataWithFx5uTcpComm(_current_group_DWORD);

      if (timer_communication_with_plc.Enabled == false)
      {
        timer_communication_with_plc.Enabled = true;
      }
    }

    

    public bool GetConnectStatus()
    {
      bool ret = tcpCommUC1.GetConnectStatus();
      return ret;
    }


    public void StopCommunication()
    {
      if (tcpCommUC1 != null)
      {
        tcpCommUC1.DeInit();
      }
    }


    private void timer_communication_with_plc_Tick(object sender, EventArgs e)
    {
      /* Check if we don't have data */
      timer_communication_with_plc.Enabled = false;
      if (timer_to_check_timeout++ >= 20)
      {
        timer_to_check_timeout = 0;

        /* plc disconnect */
        if (OnNotifyCommunicationStatus != null)
        {
          OnNotifyCommunicationStatus(this, STATUS.TIME_OUT);
          //
          ResetCurrentRequestRegister();
          //
          _current_group_DWORD = eDATA_GROUP_HMI_Data.DATA_GROUP_0;
          _current_group_DWORD = SendDataWithFx5uTcpComm(_current_group_DWORD);
        }
        //
      }
      else if (timer_to_check_timeout++ >= 10)
      {
        _current_group_DWORD = eDATA_GROUP_HMI_Data.DATA_GROUP_0;
        _current_group_DWORD = SendDataWithFx5uTcpComm(_current_group_DWORD);
      }
      timer_communication_with_plc.Enabled = true;
    }

    private void tcpCommUC1_OnNotifyStatus(object ent, TcpComm.STATUS status)
    {
      if (status == TcpComm.STATUS.INIT_OK)
      {
        if (OnNotifyCommunicationStatus != null)
        {
          OnNotifyCommunicationStatus(this, STATUS.INIT_OK);
        }

        timer_to_check_timeout = 0; //INIT_OK

        _current_group_DWORD = PLCFxCommunication.eDATA_GROUP_HMI_Data.DATA_GROUP_0;
        //
        if (timer_communication_with_plc.Enabled == false) timer_communication_with_plc.Enabled = true;
        //
      }
      else if (status == TcpComm.STATUS.INIT_FAILED)
      {
        if (OnNotifyCommunicationStatus != null)
        {
          OnNotifyCommunicationStatus(this, STATUS.INIT_FAILED);
        }
      }
      else if (status == STATUS.TRY_AGAIN)
      {
        timer_to_check_timeout = 0; //INIT_OK

        _current_group_DWORD = PLCFxCommunication.eDATA_GROUP_HMI_Data.DATA_GROUP_0;
        if (tcpCommUC1 != null)
        {
          SendDataWithFx5uTcpComm(_current_group_DWORD); //startup
        }
        //
        if (timer_communication_with_plc.Enabled == false) timer_communication_with_plc.Enabled = true;
      }
      else if (status == TcpComm.STATUS.CLOSE_DONE)
      {
        if (OnNotifyCommunicationStatus != null)
        {
          OnNotifyCommunicationStatus(this, STATUS.CLOSE_DONE);
        }
      }
      else if (status == STATUS.READ_DATA_OK)
      {
        timer_to_check_timeout = 0;
      }
      else if (status == STATUS.WRITE_DATA_OK)
      {
        //System.Threading.Thread.Sleep(50);
        //
        timer_to_check_timeout = 0;

        _current_group_DWORD = PLCFxCommunication.eDATA_GROUP_HMI_Data.DATA_GROUP_0;
        SendDataWithFx5uTcpComm(_current_group_DWORD);
        //
        if (OnNotifyCommunicationStatus != null)
        {
          OnNotifyCommunicationStatus(this, STATUS.WRITE_DATA_OK);
        }
      }
      else if (status == TcpComm.STATUS.WRONG_FORMAT_DEVICE_OR_DEVICE_NOT_SUPPORT)
      {

      }
    }




    private void tcpCommUC1_OnReadDeviceData(object ent, int index, List<TcpComm.FX_DATA> list_data, bool IsCorrectChecksum)
    {
      timer_to_check_timeout = 0;
      if (_current_group_DWORD != _maxGroupEND)
      {
        this.ProcessReceiveData(list_data);
        //
        SendDataWithFx5uTcpComm(_current_group_DWORD);
      }
      else if (_current_group_DWORD == _maxGroupEND)
      {
        _current_group_DWORD = PLCFxCommunication.eDATA_GROUP_HMI_Data.DATA_GROUP_0;

        this.ProcessReceiveData(list_data);
        //
        if (OnReadDataDone != null)
        {
          OnReadDataDone(this, PLC_Rawdata, PLC_Machinedata);
        }
        //Countinue
        SendDataWithFx5uTcpComm(_current_group_DWORD);
      }
      //
      if (OnNotifyCommunicationStatus != null)
      {
        OnNotifyCommunicationStatus(this, STATUS.READ_DATA_OK);
      }
    }



    public enum eREQUEST_MODE
    {
      SETUP_RTC_PREPARE,
      SETUP_RTC_ENABLE,
      //
      RESETUP_COVOYER_1_SPEED,
      RESETUP_COVOYER_2_SPEED,
      RESETUP_COVOYER_3_SPEED,
      RESETUP_COVOYER_Auto_Speed,
      //
      WRITE_PC_START_PB,
      WRITE_PC_STOP_PB,
      WRITE_PC_ALARM_RESET_PB,
      WRITE_PC_Man_mode,
      WRITE_PC_BT_TACH_CHAI_START_PB,
      WRITE_PC_BT_TACH_CHAI_STOP_PB,
      WRITE_PC_BT_CAN_START_PB,
      WRITE_PC_BT_CAN_STOP_PB,
      WRITE_PC_BT_REJECT_START_PB,
      WRITE_PC_BT_REJECT_STOP_PB,
      WRITE_PC_Reject_Cyl_ON, //nut nhan test cycline ON
      WRITE_PC_Reject_Cyl_OFF,//nut nhan test cycline OFF
      WRITE_PC_Weigher_Disable,
      WRITE_PC_Barcode_NG,
      WRITE_PC_Buzzer_OFF,
      WRITE_PC_Reject_Test,
      WRITE_PC_Shutdown_OK,
      WRITE_PC_Counter_Reset_PB, //PLC must be clear this bit after using
      WRITE_PC_Barcode_Disable,
      WRITE_PC_Reject_Disable,
      WRITE_PC_Reject_Enable,
      /// <summary>
      /// ///////////////////////////////////////////
      /// </summary>
      WRITE_PC_Buzzer_ON,
      WRITE_PC_Auto_mode,
      WRITE_PC_Weigher_Enable,
      WRITE_PC_Barcode_Enable,
      WRITE_PC_AutoAssignCO_Enable,
      WRITE_PC_AutoAssignCO_Disable,
      //
      WRITE_PC_Product_Type,
      WRITE_PC_Target_value,
      WRITE_PC_Sai_so,
      WRITE_PC_Gioi_han_duoi_Min_1T,
      WRITE_PC_Gioi_han_tren_Max_1T,
      //
      WRITE_PC_Gioi_han_duoi_Min_2T,
      WRITE_PC_Gioi_han_duoi_Max_2T,
      //
      WRITE_PC_BarcodeW,
      //
      WRITE_PC_Delay_Barcode,
      WRITE_PC_Reject_Time,
      WRITE_PC_Reject_Time_Box_Conti,
      WRITE_PC_Delay_Reject,
      WRITE_PC_Reject_Number_Box,


      WRITE_RESET_PC_CONTROL,
      //
      WRITE_PC_Nozzle_Reset_PB, //PLC must be clear this bit after using

      //
      RESETUP_BUFFER_DATA_1,
      RESETUP_BUFFER_DATA_2,
      RESETUP_BUFFER_DATA_3,
      RESETUP_BUFFER_DATA_4,
      RESETUP_BUFFER_DATA_5,
      RESETUP_BUFFER_DATA_6,
      RESETUP_BUFFER_DATA_7,
      RESETUP_BUFFER_DATA_8,
      //
      WRITE_PC_VALUE_BAO_BI,

      //
      WRITE_PC_OpenDoor_ON_PB,
      WRITE_PC_OpenDoor_OFF_PB,
      //

      WRITE_PC_Reject_Time_54,
      WRITE_PC_Reject_Time_Box_Conti_57,
      WRITE_PC_Delay_Reject_58,
      WRITE_PC_Reject_Number_Box_59,
      WRITE_PC_Front_Machine_Run_Time_388,
      WRITE_PC_Front_Machine_Stop_Time_389,
      WRITE_PC_Behind_Machine_Run_Time_390,
      WRITE_PC_Behind_Machine_Stop_Time_391,

      
      //
      WRITE_PC_REJECT_1_HUONG,
      WRITE_PC_REJECT_2_HUONG,




        //
      NONE
    }

    private enum WRITE_COMMAND
    {
      RESET_COUNTER = 19,
      RESET_BY_SHIFT_CHANGED = 20,
      SETUP_RTC = 21,
      START_MACHINE = 22,
      STOP_MACHINE = 23,
      ENZIM_START = 24,
      ENZIM_STOP = 25,
      NONE
    }

   
    public enum eDATA_GROUP_HMI_Data
    {
      DATA_GROUP_0 = 0, //50W
      DATA_GROUP_1 = 1,//100W
      DATA_GROUP_2,//150
      DATA_GROUP_3,//200
      DATA_GROUP_4,//250
      DATA_GROUP_5,//300
      DATA_GROUP_6,//350
      DATA_GROUP_7,//400
      DATA_GROUP_8,//450

      DATA_GROUP_END,

    }

    /// <summary>
    /// Reset curretn request
    /// </summary>
    public void ResetCurrentRequestRegister()
    {
      bool IsExitLoop = false;
      for (int i = 0; (i < list_UserRequestCmd.Count) && (IsExitLoop == false); i++)
      {
        if (list_UserRequestCmd[i].id == _write_register.id)
        {
          IsExitLoop = true;
          //
          _write_register._registerWriteCmd = eREQUEST_MODE.NONE;
          _write_register._lineId = 0;
          _write_register._machineIdTobeRequested = 0;
          _write_register._dataToWrite = 0;

          //
          list_UserRequestCmd[i] = _write_register;
        }/*if (list_UserRequestCmd[i].id == _write_register.id)*/
      }/*for (int i = 0; (i < list_UserRequestCmd.Count) && (IsExitLoop == false); i++)*/
    }

    /// <summary>
    /// Add new request
    /// </summary>
    /// <param name="WriteCmd"></param>
    public void RegisterUserRequest(eREQUEST_MODE WriteCmd)
    {
      RegisterUserRequest(WriteCmd, INVALID_DATA);
    }
    /// <summary>
    /// Add new request
    /// </summary>
    /// <param name="WriteCmd"></param>
    public void RegisterUserRequest(eREQUEST_MODE WriteCmd, int dataToWrite)
    {
      bool IsExitLoop = false;
      for (int i = 0; (i < list_UserRequestCmd.Count) && (IsExitLoop == false); i++)
      {
        if (list_UserRequestCmd[i]._registerWriteCmd == eREQUEST_MODE.NONE)
        {
          IsExitLoop = true;
          //
          RequestRegister register = new RequestRegister();
          register.id = list_UserRequestCmd[i].id;
          register._registerWriteCmd = WriteCmd;
          register._machineIdTobeRequested = 0;
          register._dataToWrite = dataToWrite;
          //
          list_UserRequestCmd[i] = register;
        }
      }
    }


    private int[] ConvertBarcodeToBlockOfBytes(string Barcode)
    {
      List<int> list_data = new List<int>();
      //
      if (Barcode.Length > 0)
      {
        for (int i = 0; i < Barcode.Length; i++)
        {
          char chr = Barcode[i];
          if (chr == ' ')
          {
            /* do nothing */
          }
          else
          {
            int data = Convert.ToByte(chr);
            list_data.Add(data);
          }
          //
        }
      }
      return list_data.ToArray();
    }


    /// <summary>
    /// Add new request
    /// </summary>
    /// <param name="WriteCmd"></param>
    public void RegisterUserRequest(eREQUEST_MODE WriteCmd, int[] blockDataToWrite)
    {
      bool IsExitLoop = false;
      for (int i = 0; (i < list_UserRequestCmd.Count) && (IsExitLoop == false); i++)
      {
        if (list_UserRequestCmd[i]._registerWriteCmd == eREQUEST_MODE.NONE)
        {
          IsExitLoop = true;
          //
          RequestRegister register = new RequestRegister();
          register.id = list_UserRequestCmd[i].id;
          register._registerWriteCmd = WriteCmd;
          register._machineIdTobeRequested = 0;
          register._blockDataToWrite = blockDataToWrite;
          //
          list_UserRequestCmd[i] = register;
        }
      }
    }


    private int[] BuildWriteDataForPCControl(eREQUEST_MODE WriteCmd)
    {
      int[] write_values = new int[2];
      
      write_values[0] = 0;
      write_values[1] = 0;
      int write_value = PLC_Machinedata.PC_Control_status.value.Convert_to_Int();
      //
      switch (WriteCmd)
      {
        case eREQUEST_MODE.WRITE_PC_START_PB:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_START_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_STOP_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_STOP_PB:
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_START_PB));
          write_value |= (1 << (int)(ePC_ControlStatus.PC_STOP_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_ALARM_RESET_PB:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_ALARM_RESET_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_Man_mode:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_Man_mode));
          break;
        case eREQUEST_MODE.WRITE_PC_REJECT_1_HUONG:
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_Select_Reject_Type_23));
          break;
        case eREQUEST_MODE.WRITE_PC_REJECT_2_HUONG:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_Select_Reject_Type_23));
          break;



        case eREQUEST_MODE.WRITE_PC_Auto_mode:
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_Man_mode));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_START_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_STOP_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_TACH_CHAI_START_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_TACH_CHAI_STOP_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_CAN_START_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_CAN_STOP_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_REJECT_START_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_REJECT_STOP_PB));




          break;
        case eREQUEST_MODE.WRITE_PC_BT_TACH_CHAI_START_PB:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_BT_TACH_CHAI_START_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_TACH_CHAI_STOP_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_BT_TACH_CHAI_STOP_PB:
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_TACH_CHAI_START_PB));
          write_value |= (1 << (int)(ePC_ControlStatus.PC_BT_TACH_CHAI_STOP_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_BT_CAN_START_PB:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_BT_CAN_START_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_CAN_STOP_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_BT_CAN_STOP_PB:
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_CAN_START_PB));
          write_value |= (1 << (int)(ePC_ControlStatus.PC_BT_CAN_STOP_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_BT_REJECT_START_PB:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_BT_REJECT_START_PB));
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_REJECT_STOP_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_BT_REJECT_STOP_PB:
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_BT_REJECT_START_PB));
          write_value |= (1 << (int)(ePC_ControlStatus.PC_BT_REJECT_STOP_PB));
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Cyl_ON:         
           write_value |= (1 << (int)(ePC_ControlStatus.PC_Reject_Cyl_ON));
           write_value &= ~(1 << (int)(ePC_ControlStatus.PC_Reject_Cyl_OFF));
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Cyl_OFF:
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_Reject_Cyl_ON));
          write_value |= (1 << (int)(ePC_ControlStatus.PC_Reject_Cyl_OFF));
          break;
        case eREQUEST_MODE.WRITE_PC_Weigher_Enable:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_Weigher_Disable));
          break;
        case eREQUEST_MODE.WRITE_PC_Weigher_Disable :
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_Weigher_Disable));
          break;
        case eREQUEST_MODE.WRITE_PC_Barcode_NG:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_Barcode_NG));
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Disable :
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_Reject_Disable));
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Enable:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_Reject_Disable));
          break;
        case eREQUEST_MODE.WRITE_PC_Buzzer_ON:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_Buzzer_OFF));
          break;
        case eREQUEST_MODE.WRITE_PC_Buzzer_OFF:
          write_value &= ~(1 << (int)(ePC_ControlStatus.PC_Buzzer_OFF));
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Test:
          write_value |= (1 << (int)(ePC_ControlStatus.PC_Reject_Test));
          break;
        //------------------------next --> use write_values[1];
        case eREQUEST_MODE.WRITE_PC_Shutdown_OK:
          write_value |= (1 << ((int)(ePC_ControlStatus.PC_Shutdown_OK)));
          break;

        case eREQUEST_MODE.WRITE_PC_Counter_Reset_PB:
					write_value |= (1 << ((int)(ePC_ControlStatus.PC_Counter_Reset_PB)));
          break;
        case eREQUEST_MODE.WRITE_PC_Barcode_Disable:
          write_value |= (1 << ((int)(ePC_ControlStatus.PC_Barcode_Disable)));
          break;
        case eREQUEST_MODE.WRITE_PC_Barcode_Enable:
          write_value &= ~(1 << ((int)(ePC_ControlStatus.PC_Barcode_Disable)));
          break;
        case eREQUEST_MODE.WRITE_PC_AutoAssignCO_Enable:
          write_value |= (1 << ((int)(ePC_ControlStatus.PC_SwAutoManChangeoverByALC))); 
          break;
        case eREQUEST_MODE.WRITE_PC_AutoAssignCO_Disable:
          write_value &= ~(1 << ((int)(ePC_ControlStatus.PC_SwAutoManChangeoverByALC)));
          break;
        case eREQUEST_MODE.WRITE_PC_Nozzle_Reset_PB:
          write_value |= (1 << ((int)(ePC_ControlStatus.PC_Nozzle_Reset_PB)));//
          break;

        case eREQUEST_MODE.WRITE_PC_OpenDoor_ON_PB:
          write_value |= (1 << ((int)(ePC_ControlStatus.PC_OpenDoor_PB)));//
          break;
        case eREQUEST_MODE.WRITE_PC_OpenDoor_OFF_PB:
          write_value &= ~(1 << ((int)(ePC_ControlStatus.PC_OpenDoor_PB)));//
          break;

        default:
          break;
      }
      write_values[0] = write_value & 0x0000FFFF;
      write_values[1] = (write_value >> 16) & 0x0000FFFF;
      return write_values;
    }

    private void WriteData(object fxComm, RequestRegister _write_register)
    {
      eREQUEST_MODE WriteCmd = _write_register._registerWriteCmd;
      int LineIdToWrite = _write_register._lineId;
      int machineIdToWrite = _write_register._machineIdTobeRequested;
      int dataToWrite = _write_register._dataToWrite;

      int[] blockDataToWrite = _write_register._blockDataToWrite;//ConvertBarcodeToBlockOfBytes(_write_register._dataToWriteAsString);//_write_register._blockDataToWrite;
      /* find correct address */
      //
      int[] write_values = new int[8];
      switch (WriteCmd)
      {
        case eREQUEST_MODE.SETUP_RTC_PREPARE:
          write_values[0] = DateTime.Now.Second; //D8013
          write_values[1] = DateTime.Now.Minute; //D8014
          write_values[2] = int.Parse(DateTime.Now.ToString("HH"));//D8015
          write_values[3] = DateTime.Now.Day; //D8016
          write_values[4] = DateTime.Now.Month; //D8017
          write_values[5] = DateTime.Now.Year % 100; //D8018 -- 2 digit

          write_values[5] = DateTime.Now.Year;
          tcpCommUC1.WriteDeviceMemory_Binary_codes("D40", write_values, 6);
          //
          ResetCurrentRequestRegister();
          RegisterUserRequest(eREQUEST_MODE.SETUP_RTC_ENABLE, INVALID_DATA);

          break;

        case eREQUEST_MODE.SETUP_RTC_ENABLE:
          write_values[0] = (int)(WRITE_COMMAND.SETUP_RTC);

          tcpCommUC1.WriteDeviceMemory_Binary_codes("D60", write_values, 1);
          //
          ResetCurrentRequestRegister();
          break;

        //---------------------------------------------------------------------------------------------------------------
        case eREQUEST_MODE.WRITE_PC_Reject_Time_54:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Reject_Time_54.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Time_Box_Conti_57:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Reject_Time_Box_Conti_57.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Delay_Reject_58:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Delay_Reject_58.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Number_Box_59:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Reject_Number_Box_59.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Front_Machine_Run_Time_388:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Front_Machine_Run_Time_388.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Front_Machine_Stop_Time_389:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Front_Machine_Stop_Time_389.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Behind_Machine_Run_Time_390:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Behind_Machine_Run_Time_390.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Behind_Machine_Stop_Time_391:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Behind_Machine_Stop_Time_391.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;



        case eREQUEST_MODE.RESETUP_BUFFER_DATA_1:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.Buffer_PC_Data_1.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_BUFFER_DATA_2:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.Buffer_PC_Data_2.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_BUFFER_DATA_3:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.Buffer_PC_Data_3.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_BUFFER_DATA_4:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.Buffer_PC_Data_4.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_BUFFER_DATA_5:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.Buffer_PC_Data_5.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_BUFFER_DATA_6:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.Buffer_PC_Data_6.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_BUFFER_DATA_7:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.Buffer_PC_Data_6.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_BUFFER_DATA_8:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.Buffer_PC_Data_6.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;





        case eREQUEST_MODE.RESETUP_COVOYER_1_SPEED:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Btai_Vao_Speed.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        
        case eREQUEST_MODE.RESETUP_COVOYER_2_SPEED:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Btai_Can_Speed.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_COVOYER_3_SPEED:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Btai_Ra_Speed.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.RESETUP_COVOYER_Auto_Speed:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Conveyor_Auto_Speed.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;


        case eREQUEST_MODE.WRITE_PC_START_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          RegisterUserRequest(eREQUEST_MODE.WRITE_RESET_PC_CONTROL);
          break;

        case eREQUEST_MODE.WRITE_PC_STOP_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.WRITE_PC_ALARM_RESET_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.WRITE_PC_Man_mode:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_REJECT_1_HUONG:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_REJECT_2_HUONG:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Auto_mode:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_BT_TACH_CHAI_START_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.WRITE_PC_BT_TACH_CHAI_STOP_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.WRITE_PC_BT_CAN_START_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.WRITE_PC_BT_CAN_STOP_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.WRITE_PC_BT_REJECT_START_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_BT_REJECT_STOP_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Cyl_ON:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Cyl_OFF:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Weigher_Disable:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
          case eREQUEST_MODE.WRITE_PC_Weigher_Enable:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Barcode_NG:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Enable:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Disable:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Buzzer_OFF:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Buzzer_ON:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Counter_Reset_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
          //--------------------------
        case eREQUEST_MODE.WRITE_PC_Nozzle_Reset_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_OpenDoor_ON_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        //
        case eREQUEST_MODE.WRITE_PC_OpenDoor_OFF_PB:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        //-------------------------------
        case eREQUEST_MODE.WRITE_PC_Barcode_Disable:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Barcode_Enable:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_AutoAssignCO_Enable:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_AutoAssignCO_Disable:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Shutdown_OK:
          write_values = BuildWriteDataForPCControl(WriteCmd);
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Delay_Barcode:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Delay_Barcode.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Time:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Reject_Time_54.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Time_Box_Conti:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Reject_Time_Box_Conti_57.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Delay_Reject:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Delay_Reject_58.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Reject_Number_Box:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Reject_Number_Box_59.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        /////////////////////////////////////////////////////////
        case eREQUEST_MODE.WRITE_PC_Product_Type:
          write_values[0] = dataToWrite;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Product_Type.adressAsStr, write_values, 1);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Target_value:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Target_value.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_VALUE_BAO_BI:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Value_BaoBi.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_PC_Sai_so:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Sai_so.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        //---- Min 1T
        case eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Min_1T:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Min_1T.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        //---- Max 1T
        case eREQUEST_MODE.WRITE_PC_Gioi_han_tren_Max_1T:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Max_1T.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        //---- Min 2T
        case eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Min_2T:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Min_2T.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        //---- Max 2T
        case eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Max_2T:
          write_values[0] = dataToWrite & 0xFFFF;
          write_values[1] = (dataToWrite >> 16) & 0xFFFF;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Max_2T.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;

        case eREQUEST_MODE.WRITE_PC_BarcodeW:
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Barcode.adressAsStr, blockDataToWrite, blockDataToWrite.Length);
          ResetCurrentRequestRegister();
          break;
        case eREQUEST_MODE.WRITE_RESET_PC_CONTROL:
          write_values[0] = 0;
          write_values[1] = 0;
          tcpCommUC1.WriteDeviceMemory_Binary_codes(PLC_Machinedata.PC_Control_status.adressAsStr, write_values, 2);
          ResetCurrentRequestRegister();
          break;
        default:
          break;
      }
    }

    private bool CheckFoundWriteSlot()
    {
      bool IsFoundWriteSlot = false;

      for (int i = 0; (i < list_UserRequestCmd.Count) && (IsFoundWriteSlot == false); i++)
      {
        if (list_UserRequestCmd[i]._registerWriteCmd != eREQUEST_MODE.NONE)
        {
          IsFoundWriteSlot = true;
          //
          _write_register.id = list_UserRequestCmd[i].id;
          _write_register._registerWriteCmd = list_UserRequestCmd[i]._registerWriteCmd;
          _write_register._lineId = list_UserRequestCmd[i]._lineId;
          _write_register._machineIdTobeRequested = list_UserRequestCmd[i]._machineIdTobeRequested;
          _write_register._dataToWrite = list_UserRequestCmd[i]._dataToWrite;
          _write_register._blockDataToWrite = list_UserRequestCmd[i]._blockDataToWrite;
          //
        }/*if (list_registerWriteCmd[i]._registerWriteCmd == WRITE_MODE.NONE)*/
      }/*for (int i = 0; (i < list_registerWriteCmd.Count) && (IsExitLoop == false); i++)*/
      return IsFoundWriteSlot;
    }


    private void Timer_sendFx5U_Tick(object sender, EventArgs e)
    {
      timer_sendFx5U.Enabled = false;
      eDATA_GROUP_HMI_Data next_group = _saveDATA_GROUP_HMI_Data;

      if (tcpCommUC1 != null)
      {
        if (tcpCommUC1.IsEthConnected == true)
        {
          /* Check if we have something to write */
          bool IsFoundWriteSlot = false;

          IsFoundWriteSlot = CheckFoundWriteSlot();
          /* Cheking Write Slot exists or not */
          if (IsFoundWriteSlot == true)
          {
            WriteData(tcpCommUC1, _write_register);
          }
          else
          {
            _write_register._registerWriteCmd = eREQUEST_MODE.NONE;

            int calling_id = 0;
            int start_byte = START_DEVICE_TO_READ;
            //fxCommUC1.WritesDeviceMemory_1WordUnits(calling_id, "D300", [0], 32);
            int start_address = (int)(_saveDATA_GROUP_HMI_Data) * ETHERNET_SNMP_MAX_WORD + start_byte;
            string address = String.Format("D{0}", start_address);

            tcpCommUC1.Read_DeviceMemory(address, ETHERNET_SNMP_MAX_WORD, PROTOCOL_UNIT._x1_WORD);
            //
            next_group = (eDATA_GROUP_HMI_Data)((int)(next_group) + 1);
					}
        }
      }
      _current_group_DWORD = next_group;
      // timer_sendFx5U.Enabled = true;
      //return next_group;
    }


    private eDATA_GROUP_HMI_Data _saveDATA_GROUP_HMI_Data;

    public eDATA_GROUP_HMI_Data SendDataWithFx5uTcpComm(eDATA_GROUP_HMI_Data group)
    {
      _saveDATA_GROUP_HMI_Data = group;
      if (timer_sendFx5U.Enabled == false)
      {
        timer_sendFx5U.Enabled = true;
      }
      return _saveDATA_GROUP_HMI_Data;
    }





    private int formatDecimal(FX_DEVICE fx_device, int value)
    {
      int ret = value;
      if (fx_device == FX_DEVICE.D)
      {
        if ((value & 0x8000) == 0x8000)
        {
          ret = value - (0xFFFF + 1);
        }
      }
      else if (fx_device == FX_DEVICE.DW)
      {
      }
      return ret;
    }

    /// <summary>
    /// Receive data from PLC
    /// </summary>
    /// <param name="list_data_from_plc"></param>
    private void ProcessReceiveData(List<TcpComm.FX_DATA> list_data_from_plc)
    {
      /* Put to raw-data */
      for (int i = 0; i < list_data_from_plc.Count; i++)
      {
        List<TcpComm.FX_DATA> list_Raw_DataFounds = PLC_Rawdata.list_Raw_Data.FindAll(x => x.address == list_data_from_plc[i].address);
        if (list_Raw_DataFounds.Count > 0)
        {
          list_Raw_DataFounds[0].value = list_data_from_plc[i].value;
        }
      }/*for (int i = 0; i < list_data_from_plc.Count; i++)*/

      /* Put to machine-data */
      PLC_Machinedata.ApplyFromRawData(PLC_Rawdata);
    }
  }
  public enum eDATA_GROUP_HMI_Data
  {
    DATA_GROUP_0 = 0,
    DATA_GROUP_END = 1,
  }

  public class PLC_MachineDataWithAddress
  {
    public int idx = 0;
    public int nLength = 0;
    public object value = new object();
    public string adressAsStr = "";
    public int adressAsInt = 0;
    //
    public int[] value_to_write = new int[8];
    public int length_to_write = 0;
    public WRITE_MODE write_mode = WRITE_MODE.NONE;
    //
    public PLC_MachineDataWithAddress(int idx, int nLength)
    {
      this.idx = idx;
      this.nLength = nLength;
    }

    public enum WRITE_MODE
    {
      WRITE_WORD,
      WRITE_BIT_MOMENTARY,
      WRITE_BIT_INVERT,
      //
      WRITE_BIT_IN_WORD_MOMENTARY_ON,
      WRITE_BIT_IN_WORD_MOMENTARY_OFF,
      NONE
    }
  }

  public class PLC_MachineData
  {
    /// <summary>
    /// Mã hàng từ Barcode
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Barcode = new PLC_MachineDataWithAddress(0, 18);


    public PLC_MachineDataWithAddress PLC_WarningCode = new PLC_MachineDataWithAddress(18, 2);
    /// <summary>
    /// Giá trị cân 
    /// </summary>
    public PLC_MachineDataWithAddress PLC_ControlStatus = new PLC_MachineDataWithAddress(20, 2);
    /// <summary>
    /// Sai số
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Product_Current_ID = new PLC_MachineDataWithAddress(22, 2);
    /// <summary>
    /// Giá trị cân hiện tại
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Weight_Value = new PLC_MachineDataWithAddress(24, 2);
    /// <summary>
    /// Số lượng thùng OK
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Product_counter_OK = new PLC_MachineDataWithAddress(26, 2);
    /// <summary>
    /// Số lượng thùng NG
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Product_counter_NG = new PLC_MachineDataWithAddress(28, 2);
    /// <summary>
    /// Chu kỳ máy
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Cycle_Time = new PLC_MachineDataWithAddress(30, 2);
    /// <summary>
    /// Tốc độ máy
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Machine_Speed = new PLC_MachineDataWithAddress(32, 1);

    /// <summary>
    /// Tốc độ băng tải
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Conveyor_Auto_Speed = new PLC_MachineDataWithAddress(33, 1);
    
    /// <summary>
    /// Lỗi hệ thống
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Error_code = new PLC_MachineDataWithAddress(34, 2);

    /// <summary>
    /// PLC_Counter_Total
    /// </summary>
    public PLC_MachineDataWithAddress PLC_Counter_Total = new PLC_MachineDataWithAddress(36, 2);
    /// <summary>
    /// Giá tri cân liên tục
    /// </summary>
    public PLC_MachineDataWithAddress PLC_WeigherContinue = new PLC_MachineDataWithAddress(38, 2);
    /// <summary>
    /// Lệnh điều khiển từ máy tính
    /// </summary>
    public PLC_MachineDataWithAddress PC_Control_status = new PLC_MachineDataWithAddress(40, 2);

    /// <summary>
    /// Giá trị cài đặt
    /// </summary>
    public PLC_MachineDataWithAddress PC_Target_value = new PLC_MachineDataWithAddress(42, 2);

    /// <summary>
    /// Sai số
    /// </summary>
    public PLC_MachineDataWithAddress PC_Sai_so = new PLC_MachineDataWithAddress(44, 2);

    /// <summary>
    /// Giới hạn dưới 1T -- 46
    /// </summary>
    public PLC_MachineDataWithAddress PC_Min_1T = new PLC_MachineDataWithAddress(46, 2);

    /// <summary>
    /// Giới hạn trên 1T -- 48
    /// </summary>
    public PLC_MachineDataWithAddress PC_Max_1T = new PLC_MachineDataWithAddress(48, 2);


    /// <summary>
    /// Tốc độ băng tải tách chai
    /// </summary>
    public PLC_MachineDataWithAddress PC_Btai_Vao_Speed = new PLC_MachineDataWithAddress(50, 1);
    /// <summary>
    /// Tốc độ băng tải cân
    /// </summary>
    public PLC_MachineDataWithAddress PC_Btai_Can_Speed = new PLC_MachineDataWithAddress(51, 1);
    /// <summary>
    /// Tốc độ băng tải loại thùng
    /// </summary>
    public PLC_MachineDataWithAddress PC_Btai_Ra_Speed = new PLC_MachineDataWithAddress(52, 1);

    /// <summary>
    /// Thời gian delay trigger barcode
    /// </summary>
    public PLC_MachineDataWithAddress PC_Delay_Barcode = new PLC_MachineDataWithAddress(53, 1);

    /// <summary>
    /// Thời gian delay Reject sp lỗi D1054
    /// </summary>
    public PLC_MachineDataWithAddress PC_Reject_Time_54 = new PLC_MachineDataWithAddress(54, 1);
    /// <summary>
    /// Tốc độ chung của BT
    /// </summary>
    public PLC_MachineDataWithAddress PC_Conveyor_Auto_Speed = new PLC_MachineDataWithAddress(55, 1);

    /// <summary>
    /// Loại sản phẩm
    /// </summary>
    public PLC_MachineDataWithAddress PC_Product_Type = new PLC_MachineDataWithAddress(56, 1);

    /// <summary>
    /// thời gian thùng tiếp theo vào 57
    /// </summary>
    public PLC_MachineDataWithAddress PC_Reject_Time_Box_Conti_57 = new PLC_MachineDataWithAddress(57, 1);
    /// <summary>
    /// Delay_trigger_reject 58
    /// </summary>
    public PLC_MachineDataWithAddress PC_Delay_Reject_58 = new PLC_MachineDataWithAddress(58, 1);

    /// <summary>
    /// Số thùng nằm giữa khoảng reject
    /// </summary>
    public PLC_MachineDataWithAddress PC_Reject_Number_Box_59 = new PLC_MachineDataWithAddress(59, 1);

    //
    public PLC_MachineDataWithAddress PLC_ID_Slot_1 = new PLC_MachineDataWithAddress(60, 2);
    public PLC_MachineDataWithAddress PLC_ID_Slot_2 = new PLC_MachineDataWithAddress(62, 2);

    public PLC_MachineDataWithAddress PLC_ID_Slot_3 = new PLC_MachineDataWithAddress(64, 2);

    public PLC_MachineDataWithAddress PLC_ID_Slot_4 = new PLC_MachineDataWithAddress(66, 2);

    public PLC_MachineDataWithAddress PLC_ID_Slot_5 = new PLC_MachineDataWithAddress(68, 2);

    public PLC_MachineDataWithAddress PLC_ID_Slot_6 = new PLC_MachineDataWithAddress(70, 2);

    public PLC_MachineDataWithAddress PLC_ID_Slot_7 = new PLC_MachineDataWithAddress(72, 2);

    public PLC_MachineDataWithAddress PLC_ID_Slot_8 = new PLC_MachineDataWithAddress(74, 2);

    public PLC_MachineDataWithAddress PLC_ID_Slot_9 = new PLC_MachineDataWithAddress(76, 2);

    public PLC_MachineDataWithAddress PLC_ID_Slot_10 = new PLC_MachineDataWithAddress(78, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_1 = new PLC_MachineDataWithAddress(80, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_2 = new PLC_MachineDataWithAddress(82, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_3 = new PLC_MachineDataWithAddress(84, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_4 = new PLC_MachineDataWithAddress(86, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_5 = new PLC_MachineDataWithAddress(88, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_6 = new PLC_MachineDataWithAddress(90, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_7 = new PLC_MachineDataWithAddress(92, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_8 = new PLC_MachineDataWithAddress(94, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_9 = new PLC_MachineDataWithAddress(96, 2);

    public PLC_MachineDataWithAddress PLC_Weight_Slot_10 = new PLC_MachineDataWithAddress(98, 2);
    //
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_1 = new PLC_MachineDataWithAddress(100, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_2 = new PLC_MachineDataWithAddress(101, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_3 = new PLC_MachineDataWithAddress(102, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_4 = new PLC_MachineDataWithAddress(103, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_5 = new PLC_MachineDataWithAddress(104, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_6 = new PLC_MachineDataWithAddress(105, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_7 = new PLC_MachineDataWithAddress(106, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_8 = new PLC_MachineDataWithAddress(107, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_9 = new PLC_MachineDataWithAddress(108, 1);
    public PLC_MachineDataWithAddress PLC_StatusBarcode_ID_Slot_10 = new PLC_MachineDataWithAddress(109, 1);
    //
    public PLC_MachineDataWithAddress PC_Barcode = new PLC_MachineDataWithAddress(110, 20);
    //
    public PLC_MachineDataWithAddress Status_Reject_ID1 = new PLC_MachineDataWithAddress(130, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID2 = new PLC_MachineDataWithAddress(131, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID3 = new PLC_MachineDataWithAddress(132, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID4 = new PLC_MachineDataWithAddress(133, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID5 = new PLC_MachineDataWithAddress(134, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID6 = new PLC_MachineDataWithAddress(135, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID7 = new PLC_MachineDataWithAddress(136, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID8 = new PLC_MachineDataWithAddress(137, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID9 = new PLC_MachineDataWithAddress(138, 1);
    public PLC_MachineDataWithAddress Status_Reject_ID10 = new PLC_MachineDataWithAddress(139, 1);
    //
    public PLC_MachineDataWithAddress PLC_Barcode_ID_1 = new PLC_MachineDataWithAddress(140, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_2 = new PLC_MachineDataWithAddress(160, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_3 = new PLC_MachineDataWithAddress(180, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_4 = new PLC_MachineDataWithAddress(200, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_5 = new PLC_MachineDataWithAddress(220, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_6 = new PLC_MachineDataWithAddress(240, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_7 = new PLC_MachineDataWithAddress(260, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_8 = new PLC_MachineDataWithAddress(280, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_9 = new PLC_MachineDataWithAddress(300, 20);
    public PLC_MachineDataWithAddress PLC_Barcode_ID_10 = new PLC_MachineDataWithAddress(320, 20);
    //
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_1 = new PLC_MachineDataWithAddress(340, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_2 = new PLC_MachineDataWithAddress(341, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_3 = new PLC_MachineDataWithAddress(342, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_4 = new PLC_MachineDataWithAddress(343, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_5 = new PLC_MachineDataWithAddress(344, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_6 = new PLC_MachineDataWithAddress(345, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_7 = new PLC_MachineDataWithAddress(346, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_8 = new PLC_MachineDataWithAddress(347, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_9 = new PLC_MachineDataWithAddress(348, 1);
    public PLC_MachineDataWithAddress PLC_Nozzle_Slot_10 = new PLC_MachineDataWithAddress(349, 1);
    //
    public PLC_MachineDataWithAddress PLC_OverWeight = new PLC_MachineDataWithAddress(350, 2);

    /// <summary>
    /// Min_1T từ PLC
    /// </summary>
    public PLC_MachineDataWithAddress PLC_1T = new PLC_MachineDataWithAddress(352, 2);

    /// <summary>
    /// Min_2T từ PLC
    /// </summary>
    public PLC_MachineDataWithAddress PC_Min_2T = new PLC_MachineDataWithAddress(370, 2);
    /// <summary>
    /// Max_2T từ PLC
    /// </summary>
    public PLC_MachineDataWithAddress PC_Max_2T = new PLC_MachineDataWithAddress(372, 2);

    //
    public PLC_MachineDataWithAddress Buffer_PC_Data_1 = new PLC_MachineDataWithAddress(374, 2);
    public PLC_MachineDataWithAddress Buffer_PC_Data_2 = new PLC_MachineDataWithAddress(376, 2);
    public PLC_MachineDataWithAddress Buffer_PC_Data_3 = new PLC_MachineDataWithAddress(378, 2);
    public PLC_MachineDataWithAddress Buffer_PC_Data_4 = new PLC_MachineDataWithAddress(380, 2);
    public PLC_MachineDataWithAddress Buffer_PC_Data_5 = new PLC_MachineDataWithAddress(382, 2);
    public PLC_MachineDataWithAddress Buffer_PC_Data_6 = new PLC_MachineDataWithAddress(384, 2);
    public PLC_MachineDataWithAddress PC_Value_BaoBi = new PLC_MachineDataWithAddress(386, 2);

    public PLC_MachineDataWithAddress PC_Front_Machine_Run_Time_388 = new PLC_MachineDataWithAddress(388, 1);
    public PLC_MachineDataWithAddress PC_Front_Machine_Stop_Time_389 = new PLC_MachineDataWithAddress(389, 1);
    public PLC_MachineDataWithAddress PC_Behind_Machine_Run_Time_390 = new PLC_MachineDataWithAddress(390, 1);
    public PLC_MachineDataWithAddress PC_Behind_Machine_Stop_Time_391 = new PLC_MachineDataWithAddress(391, 1);




    //
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_1 = new PLC_MachineDataWithAddress(400, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_2 = new PLC_MachineDataWithAddress(402, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_3 = new PLC_MachineDataWithAddress(404, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_4 = new PLC_MachineDataWithAddress(406, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_5 = new PLC_MachineDataWithAddress(408, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_6 = new PLC_MachineDataWithAddress(410, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_7 = new PLC_MachineDataWithAddress(412, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_8 = new PLC_MachineDataWithAddress(414, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_9 = new PLC_MachineDataWithAddress(416, 2);
    public PLC_MachineDataWithAddress PLC_Gross_Weigher_Slot_10 = new PLC_MachineDataWithAddress(418, 2);

    //
    public PLC_MachineData(int start_address)
    {
      SettingData(start_address, PLC_Barcode);
      SettingData(start_address, PLC_WarningCode);
      SettingData(start_address, PLC_ControlStatus);
      SettingData(start_address, PLC_Product_Current_ID);
      SettingData(start_address, PLC_Weight_Value);
      SettingData(start_address, PLC_Product_counter_OK);
      SettingData(start_address, PLC_Product_counter_NG);
      SettingData(start_address, PLC_Cycle_Time);
      SettingData(start_address, PLC_Machine_Speed);
      SettingData(start_address, PLC_Conveyor_Auto_Speed);
      SettingData(start_address, PLC_Error_code);
      SettingData(start_address, PLC_Counter_Total);
      SettingData(start_address, PLC_WeigherContinue);
      SettingData(start_address, PC_Control_status);
      SettingData(start_address, PC_Target_value);
      SettingData(start_address, PC_Sai_so);
      SettingData(start_address, PC_Min_1T);
      SettingData(start_address, PC_Max_1T);
      SettingData(start_address, PC_Min_2T);
      SettingData(start_address, PC_Max_2T);
      SettingData(start_address, PC_Btai_Vao_Speed);
      SettingData(start_address, PC_Btai_Can_Speed);
      SettingData(start_address, PC_Btai_Ra_Speed);
      SettingData(start_address, PC_Delay_Barcode);
      SettingData(start_address, PC_Reject_Time_54);
      SettingData(start_address, PC_Conveyor_Auto_Speed);
      SettingData(start_address, PC_Product_Type);
      SettingData(start_address, PC_Reject_Time_Box_Conti_57);
      SettingData(start_address, PC_Delay_Reject_58);
      SettingData(start_address, PC_Reject_Number_Box_59);
      //
      SettingData(start_address, PLC_ID_Slot_1);
      SettingData(start_address, PLC_ID_Slot_2);
      SettingData(start_address, PLC_ID_Slot_3);
      SettingData(start_address, PLC_ID_Slot_4);
      SettingData(start_address, PLC_ID_Slot_5);
      SettingData(start_address, PLC_ID_Slot_6);
      SettingData(start_address, PLC_ID_Slot_7);
      SettingData(start_address, PLC_ID_Slot_8);
      SettingData(start_address, PLC_ID_Slot_9);
      SettingData(start_address, PLC_ID_Slot_10);
      //
      SettingData(start_address, PLC_Weight_Slot_1);
      SettingData(start_address, PLC_Weight_Slot_2);
      SettingData(start_address, PLC_Weight_Slot_3);
      SettingData(start_address, PLC_Weight_Slot_4);
      SettingData(start_address, PLC_Weight_Slot_5);
      SettingData(start_address, PLC_Weight_Slot_6);
      SettingData(start_address, PLC_Weight_Slot_7);
      SettingData(start_address, PLC_Weight_Slot_8);
      SettingData(start_address, PLC_Weight_Slot_9);
      SettingData(start_address, PLC_Weight_Slot_10);
      //
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_1);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_2);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_3);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_4);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_5);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_6);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_7);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_8);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_9);
      SettingData(start_address, PLC_StatusBarcode_ID_Slot_10);
      //
      SettingData(start_address, PC_Barcode);
      //
      SettingData(start_address, Status_Reject_ID1);
      SettingData(start_address, Status_Reject_ID2);
      SettingData(start_address, Status_Reject_ID3);
      SettingData(start_address, Status_Reject_ID4);
      SettingData(start_address, Status_Reject_ID5);
      SettingData(start_address, Status_Reject_ID6);
      SettingData(start_address, Status_Reject_ID7);
      SettingData(start_address, Status_Reject_ID8);
      SettingData(start_address, Status_Reject_ID9);
      SettingData(start_address, Status_Reject_ID10);
      //
      SettingData(start_address, PLC_Barcode_ID_1);
      SettingData(start_address, PLC_Barcode_ID_2);
      SettingData(start_address, PLC_Barcode_ID_3);
      SettingData(start_address, PLC_Barcode_ID_4);
      SettingData(start_address, PLC_Barcode_ID_5);
      SettingData(start_address, PLC_Barcode_ID_6);
      SettingData(start_address, PLC_Barcode_ID_7);
      SettingData(start_address, PLC_Barcode_ID_8);
      SettingData(start_address, PLC_Barcode_ID_9);
      SettingData(start_address, PLC_Barcode_ID_10);
      //
      SettingData(start_address, PLC_Nozzle_Slot_1);
      SettingData(start_address, PLC_Nozzle_Slot_2);
      SettingData(start_address, PLC_Nozzle_Slot_3);
      SettingData(start_address, PLC_Nozzle_Slot_4);
      SettingData(start_address, PLC_Nozzle_Slot_5);
      SettingData(start_address, PLC_Nozzle_Slot_6);
      SettingData(start_address, PLC_Nozzle_Slot_7);
      SettingData(start_address, PLC_Nozzle_Slot_8);
      SettingData(start_address, PLC_Nozzle_Slot_9);
      SettingData(start_address, PLC_Nozzle_Slot_10);
      //
      SettingData(start_address, PLC_OverWeight);
      SettingData(start_address, PLC_1T);
      //
      SettingData(start_address, Buffer_PC_Data_1);
      SettingData(start_address, Buffer_PC_Data_2);
      SettingData(start_address, Buffer_PC_Data_3);
      SettingData(start_address, Buffer_PC_Data_4);
      SettingData(start_address, Buffer_PC_Data_5);
      SettingData(start_address, Buffer_PC_Data_6);
      //
      SettingData(start_address, PC_Value_BaoBi);
      //
      SettingData(start_address, PLC_Gross_Weigher_Slot_1);
      SettingData(start_address, PLC_Gross_Weigher_Slot_2);
      SettingData(start_address, PLC_Gross_Weigher_Slot_3);
      SettingData(start_address, PLC_Gross_Weigher_Slot_4);
      SettingData(start_address, PLC_Gross_Weigher_Slot_5);
      SettingData(start_address, PLC_Gross_Weigher_Slot_6);
      SettingData(start_address, PLC_Gross_Weigher_Slot_7);
      SettingData(start_address, PLC_Gross_Weigher_Slot_8);
      SettingData(start_address, PLC_Gross_Weigher_Slot_9);
      SettingData(start_address, PLC_Gross_Weigher_Slot_10);
      //
      SettingData(start_address, PC_Front_Machine_Run_Time_388);
      SettingData(start_address, PC_Front_Machine_Stop_Time_389);
      SettingData(start_address, PC_Behind_Machine_Run_Time_390);
      SettingData(start_address, PC_Behind_Machine_Stop_Time_391);
    }

    private void SettingData(int start_address, PLC_MachineDataWithAddress PLC_MachineDataWithAddress)
    {
      PLC_MachineDataWithAddress.adressAsInt = start_address + PLC_MachineDataWithAddress.idx;
      PLC_MachineDataWithAddress.adressAsStr = String.Format("D{0}", PLC_MachineDataWithAddress.adressAsInt);
    }

    /// <summary>
    /// Receive raw data and convert to barcode
    /// </summary>
    /// <param name="rawData"></param>
    /// <param name="pLC_MachineDataWithAddress"></param>
    /// <returns></returns>
    private string Get_PLC_Barcode(PLCFx5U_RawData rawData, PLC_MachineDataWithAddress pLC_MachineDataWithAddress)
    {
      string barcode = "";
      
      int stopIdx = pLC_MachineDataWithAddress.idx + pLC_MachineDataWithAddress.nLength;

      bool IsExitLoop = false;
      bool IsStartGetData = false;
      int idx = pLC_MachineDataWithAddress.idx;
      
      for (int i = 0; (i < rawData.list_Raw_Data.Count) && (IsExitLoop == false); i++)
      {
        if (rawData.list_Raw_Data[i].address == pLC_MachineDataWithAddress.adressAsInt)
        {
          if (rawData.list_Raw_Data[i].address == this.PLC_Barcode_ID_1.adressAsInt)
          {
            int kk = 0;
          }
          //idx = 
          IsStartGetData = true;
        }
        if (IsStartGetData == true)
        {
          FX_DATA data = rawData.list_Raw_Data[i];
          byte data_value = Convert.ToByte(data.value & 0x00FF);
          try
          {
            char data_as_chr = Convert.ToChar(data_value);
            if (Char.IsLetterOrDigit(data_as_chr))
            {
              barcode += String.Format("{0}", data_as_chr);
            }
          }
          catch
          {
          }
          //
          idx++;
          if (idx >= stopIdx)
          {
            IsExitLoop = true;
          }
        }/*if (IsStartGetData == true)*/

      }/*for (int i = 0; (i < rawData.list_Raw_Data.Count) && (IsExitLoop == false); i++)*/
      if (IsExitLoop == true)
      {
        if (1140 == this.PLC_Barcode_ID_1.adressAsInt)
        {
          int kk = 0;
        }
      }
      return barcode;
    }


    private string Get_PC_Barcode(PLCFx5U_RawData rawData, int rawDataId)
    {
      string PC_TargetBarcode = "";
      int length = PC_Barcode.nLength;
      int raw_id = rawDataId;
      for (int i = 0; i < PC_Barcode.nLength; i++)
      {
        char data = Convert.ToChar(rawData.list_Raw_Data[raw_id++].value);
        bool IsLetterOrDigit = (Char.IsLetterOrDigit(data));
        if (IsLetterOrDigit == true)
        {
          PC_TargetBarcode += String.Format("{0}", data);
        }
      }
      return PC_TargetBarcode;
    }
    private int GetData(PLCFx5U_RawData rawData, int rawDataId, PLC_MachineDataWithAddress PLC_MachineDataWithAddress)// int int startIdx, int word_length)
    {
      int value = 0;
      if (PLC_MachineDataWithAddress.nLength == 1)
      {
        value = (rawData.list_Raw_Data[rawDataId].value);
      }
      else if (PLC_MachineDataWithAddress.nLength == 2)
      {
        if ((rawDataId + 1) < rawData.list_Raw_Data.Count)
        {
          value = (rawData.list_Raw_Data[rawDataId + 1].value * 65536) + rawData.list_Raw_Data[rawDataId].value;
        }
      }
      return value;
    }

    /// <summary>
    /// Apply from raw data to machine-data
    /// </summary>
    /// <param name="rawData"></param>
    public void ApplyFromRawData(PLCFx5U_RawData rawData)
    {
      //this.PLC_Barcode.value = Get_PLC_Barcode(rawData, this.PLC_Barcode);
      //this.PLC_Barcode_ID_1.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_1);
      //this.PLC_Barcode_ID_2.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_2);
      //this.PLC_Barcode_ID_3.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_3);
      //this.PLC_Barcode_ID_4.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_4);
      //this.PLC_Barcode_ID_5.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_5);
      //this.PLC_Barcode_ID_6.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_6);
      //this.PLC_Barcode_ID_7.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_7);
      //this.PLC_Barcode_ID_8.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_8);
      //this.PLC_Barcode_ID_9.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_9);
      //this.PLC_Barcode_ID_10.value = Get_PLC_Barcode(rawData, this.PLC_Barcode_ID_10);
      //
      for (int i = 0; i < rawData.list_Raw_Data.Count; i++)
      {
        FX_DATA PLCdata = rawData.list_Raw_Data[i];
        if (PLCdata.address == PLC_ControlStatus.adressAsInt)
        {
          this.PLC_ControlStatus.value = GetData(rawData, i, PLC_ControlStatus);
        }
        else if (PLCdata.address == PLC_Product_Current_ID.adressAsInt)
        {
          this.PLC_Product_Current_ID.value = GetData(rawData, i, PLC_Product_Current_ID);
        }
        else if (PLCdata.address == PLC_Weight_Value.adressAsInt)
        {
          this.PLC_Weight_Value.value = GetData(rawData, i, PLC_Weight_Value);
        }
        else if (PLCdata.address == PLC_Product_counter_OK.adressAsInt)
        {
          this.PLC_Product_counter_OK.value = GetData(rawData, i, PLC_Product_counter_OK);
        }
        else if (PLCdata.address == PLC_Product_counter_NG.adressAsInt)
        {
          this.PLC_Product_counter_NG.value = GetData(rawData, i, PLC_Product_counter_NG);
        }
        else if (PLCdata.address == PLC_Cycle_Time.adressAsInt)
        {
          this.PLC_Cycle_Time.value = GetData(rawData, i, PLC_Cycle_Time);
        }
        else if (PLCdata.address == PLC_Machine_Speed.adressAsInt)
        {
          this.PLC_Machine_Speed.value = GetData(rawData, i, PLC_Machine_Speed);
        }
        else if (PLCdata.address == PLC_Conveyor_Auto_Speed.adressAsInt)
        {
          this.PLC_Conveyor_Auto_Speed.value = GetData(rawData, i, PLC_Conveyor_Auto_Speed);
        }
        else if (PLCdata.address == PLC_Error_code.adressAsInt)
        {
          this.PLC_Error_code.value = GetData(rawData, i, PLC_Error_code);
        }//
        else if (PLCdata.address == PLC_WarningCode.adressAsInt)
        {
            this.PLC_WarningCode.value = GetData(rawData, i, PLC_WarningCode);
        }
        else if (PLCdata.address == PLC_Counter_Total.adressAsInt)
        {
          this.PLC_Counter_Total.value = GetData(rawData, i, PLC_Counter_Total);
        }
        else if (PLCdata.address == PLC_WeigherContinue.adressAsInt)
        {
          this.PLC_WeigherContinue.value = GetData(rawData, i, PLC_WeigherContinue);
        }
        else if (PLCdata.address == PC_Control_status.adressAsInt)
        {
          this.PC_Control_status.value = GetData(rawData, i, PC_Control_status);
        }
        else if (PLCdata.address == PC_Target_value.adressAsInt)
        {
          this.PC_Target_value.value = GetData(rawData, i, PC_Target_value);
        }
        else if (PLCdata.address == PC_Sai_so.adressAsInt)
        {
          this.PC_Sai_so.value = GetData(rawData, i, PC_Sai_so);
        }
        else if (PLCdata.address == PC_Min_1T.adressAsInt)
        {
          this.PC_Min_1T.value = GetData(rawData, i, PC_Min_1T);
        }
        else if (PLCdata.address == PC_Max_1T.adressAsInt)
        {
          this.PC_Max_1T.value = GetData(rawData, i, PC_Max_1T);
        }
        else if (PLCdata.address == PC_Min_2T.adressAsInt)
        {
          this.PC_Min_2T.value = GetData(rawData, i, PC_Min_2T);
        }
        else if (PLCdata.address == PC_Max_2T.adressAsInt)
        {
          this.PC_Max_2T.value = GetData(rawData, i, PC_Max_2T);
        }
        else if (PLCdata.address == PC_Btai_Vao_Speed.adressAsInt)
        {
          this.PC_Btai_Vao_Speed.value = GetData(rawData, i, PC_Btai_Vao_Speed);
        }
        else if (PLCdata.address == PC_Btai_Can_Speed.adressAsInt)
        {
          this.PC_Btai_Can_Speed.value = GetData(rawData, i, PC_Btai_Can_Speed);
        }
        else if (PLCdata.address == PC_Btai_Ra_Speed.adressAsInt)
        {
          this.PC_Btai_Ra_Speed.value = GetData(rawData, i, PC_Btai_Ra_Speed);
        }
        else if (PLCdata.address == PC_Delay_Barcode.adressAsInt)
        {
          this.PC_Delay_Barcode.value = GetData(rawData, i, PC_Delay_Barcode);
        }
        else if (PLCdata.address == PC_Reject_Time_54.adressAsInt)
        {
          this.PC_Reject_Time_54.value = GetData(rawData, i, PC_Reject_Time_54);
        }
        else if (PLCdata.address == PC_Conveyor_Auto_Speed.adressAsInt)
        {
          this.PC_Conveyor_Auto_Speed.value = GetData(rawData, i, PC_Conveyor_Auto_Speed);
        }
        else if (PLCdata.address == PC_Product_Type.adressAsInt)
        {
          this.PC_Product_Type.value = GetData(rawData, i, PC_Product_Type);
        }
        else if (PLCdata.address == PC_Reject_Time_Box_Conti_57.adressAsInt)
        {
          this.PC_Reject_Time_Box_Conti_57.value = GetData(rawData, i, PC_Reject_Time_Box_Conti_57);
        }//
        else if (PLCdata.address == PC_Delay_Reject_58.adressAsInt)
        {
          this.PC_Delay_Reject_58.value = GetData(rawData, i, PC_Delay_Reject_58);
        }//
        else if (PLCdata.address == PC_Reject_Number_Box_59.adressAsInt)
        {
          this.PC_Reject_Number_Box_59.value = GetData(rawData, i, PC_Reject_Number_Box_59);
        }
				else if (PLCdata.address == PC_Reject_Number_Box_59.adressAsInt)
				{
					this.PC_Reject_Number_Box_59.value = GetData(rawData, i, PC_Reject_Number_Box_59);
				}
				else if (PLCdata.address == PC_Front_Machine_Run_Time_388.adressAsInt)
				{
					this.PC_Front_Machine_Run_Time_388.value = GetData(rawData, i, PC_Front_Machine_Run_Time_388);
				}
				else if (PLCdata.address == PC_Front_Machine_Stop_Time_389.adressAsInt)
				{
					this.PC_Front_Machine_Stop_Time_389.value = GetData(rawData, i, PC_Front_Machine_Stop_Time_389);
				}
				else if (PLCdata.address == PC_Behind_Machine_Run_Time_390.adressAsInt)
				{
					this.PC_Behind_Machine_Run_Time_390.value = GetData(rawData, i, PC_Behind_Machine_Run_Time_390);
				}
				else if (PLCdata.address == PC_Behind_Machine_Stop_Time_391.adressAsInt)
				{
					this.PC_Behind_Machine_Stop_Time_391.value = GetData(rawData, i, PC_Behind_Machine_Stop_Time_391);
				}
				







				else if (PLCdata.address == PLC_ID_Slot_1.adressAsInt)
        {
          this.PLC_ID_Slot_1.value = GetData(rawData, i, PLC_ID_Slot_1);
        }
        else if (PLCdata.address == PLC_ID_Slot_2.adressAsInt)
        {
          this.PLC_ID_Slot_2.value = GetData(rawData, i, PLC_ID_Slot_2);
        }
        else if (PLCdata.address == PLC_ID_Slot_3.adressAsInt)
        {
          this.PLC_ID_Slot_3.value = GetData(rawData, i, PLC_ID_Slot_3);
        }
        else if (PLCdata.address == PLC_ID_Slot_4.adressAsInt)
        {
          this.PLC_ID_Slot_4.value = GetData(rawData, i, PLC_ID_Slot_4);
        }
        else if (PLCdata.address == PLC_ID_Slot_5.adressAsInt)
        {
          this.PLC_ID_Slot_5.value = GetData(rawData, i, PLC_ID_Slot_5);
        }
        else if (PLCdata.address == PLC_ID_Slot_6.adressAsInt)
        {
          this.PLC_ID_Slot_6.value = GetData(rawData, i, PLC_ID_Slot_6);
        }
        else if (PLCdata.address == PLC_ID_Slot_7.adressAsInt)
        {
          this.PLC_ID_Slot_7.value = GetData(rawData, i, PLC_ID_Slot_7);
        }
        else if (PLCdata.address == PLC_ID_Slot_8.adressAsInt)
        {
          this.PLC_ID_Slot_8.value = GetData(rawData, i, PLC_ID_Slot_8);
        }
        else if (PLCdata.address == PLC_ID_Slot_9.adressAsInt)
        {
          this.PLC_ID_Slot_9.value = GetData(rawData, i, PLC_ID_Slot_9);
        }
        else if (PLCdata.address == PLC_ID_Slot_10.adressAsInt)
        {
          this.PLC_ID_Slot_10.value = GetData(rawData, i, PLC_ID_Slot_10);
        }
        else if (PLCdata.address == PLC_Weight_Slot_1.adressAsInt)
        {
          this.PLC_Weight_Slot_1.value = GetData(rawData, i, PLC_Weight_Slot_1);
        }
        else if (PLCdata.address == PLC_Weight_Slot_2.adressAsInt)
        {
          this.PLC_Weight_Slot_2.value = GetData(rawData, i, PLC_Weight_Slot_2);
        }
        else if (PLCdata.address == PLC_Weight_Slot_3.adressAsInt)
        {
          this.PLC_Weight_Slot_3.value = GetData(rawData, i, PLC_Weight_Slot_3);
        }
        else if (PLCdata.address == PLC_Weight_Slot_4.adressAsInt)
        {
          this.PLC_Weight_Slot_4.value = GetData(rawData, i, PLC_Weight_Slot_4);
        }
        else if (PLCdata.address == PLC_Weight_Slot_5.adressAsInt)
        {
          this.PLC_Weight_Slot_5.value = GetData(rawData, i, PLC_Weight_Slot_5);
        }
        else if (PLCdata.address == PLC_Weight_Slot_6.adressAsInt)
        {
          this.PLC_Weight_Slot_6.value = GetData(rawData, i, PLC_Weight_Slot_6);
        }
        else if (PLCdata.address == PLC_Weight_Slot_7.adressAsInt)
        {
          this.PLC_Weight_Slot_7.value = GetData(rawData, i, PLC_Weight_Slot_7);
        }
        else if (PLCdata.address == PLC_Weight_Slot_8.adressAsInt)
        {
          this.PLC_Weight_Slot_8.value = GetData(rawData, i, PLC_Weight_Slot_8);
        }
        else if (PLCdata.address == PLC_Weight_Slot_9.adressAsInt)
        {
          this.PLC_Weight_Slot_9.value = GetData(rawData, i, PLC_Weight_Slot_9);
        }
        else if (PLCdata.address == PLC_Weight_Slot_10.adressAsInt)
        {
          this.PLC_Weight_Slot_10.value = GetData(rawData, i, PLC_Weight_Slot_10);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_1.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_1.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_1);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_2.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_2.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_2);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_3.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_3.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_3);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_4.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_4.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_4);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_5.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_5.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_5);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_6.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_6.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_6);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_7.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_7.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_7);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_8.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_8.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_8);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_9.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_9.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_9);
        }
        else if (PLCdata.address == PLC_StatusBarcode_ID_Slot_10.adressAsInt)
        {
          this.PLC_StatusBarcode_ID_Slot_10.value = GetData(rawData, i, PLC_StatusBarcode_ID_Slot_10);
        }
        else if (PLCdata.address == PC_Barcode.adressAsInt)
        {
          this.PC_Barcode.value = Get_PC_Barcode(rawData, i);//GetData(rawData, i, PLC_StatusBarcode_ID_Slot_10);
        }
        else if (PLCdata.address == Status_Reject_ID1.adressAsInt)
        {
          this.Status_Reject_ID1.value = GetData(rawData, i, Status_Reject_ID1);
        }
        else if (PLCdata.address == Status_Reject_ID2.adressAsInt)
        {
          this.Status_Reject_ID2.value = GetData(rawData, i, Status_Reject_ID2);
        }
        else if (PLCdata.address == Status_Reject_ID3.adressAsInt)
        {
          this.Status_Reject_ID3.value = GetData(rawData, i, Status_Reject_ID3);
        }
        else if (PLCdata.address == Status_Reject_ID4.adressAsInt)
        {
          this.Status_Reject_ID4.value = GetData(rawData, i, Status_Reject_ID4);
        }
        else if (PLCdata.address == Status_Reject_ID5.adressAsInt)
        {
          this.Status_Reject_ID5.value = GetData(rawData, i, Status_Reject_ID5);
        }
        else if (PLCdata.address == Status_Reject_ID6.adressAsInt)
        {
          this.Status_Reject_ID6.value = GetData(rawData, i, Status_Reject_ID6);
        }
        else if (PLCdata.address == Status_Reject_ID7.adressAsInt)
        {
          this.Status_Reject_ID7.value = GetData(rawData, i, Status_Reject_ID7);
        }
        else if (PLCdata.address == Status_Reject_ID8.adressAsInt)
        {
          this.Status_Reject_ID8.value = GetData(rawData, i, Status_Reject_ID8);
        }
        else if (PLCdata.address == Status_Reject_ID9.adressAsInt)
        {
          this.Status_Reject_ID9.value = GetData(rawData, i, Status_Reject_ID9);
        }
        else if (PLCdata.address == Status_Reject_ID10.adressAsInt)
        {
          this.Status_Reject_ID10.value = GetData(rawData, i, Status_Reject_ID10);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_1.adressAsInt)
        {
          this.PLC_Nozzle_Slot_1.value = GetData(rawData, i, PLC_Nozzle_Slot_1);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_2.adressAsInt)
        {
          this.PLC_Nozzle_Slot_2.value = GetData(rawData, i, PLC_Nozzle_Slot_2);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_3.adressAsInt)
        {
          this.PLC_Nozzle_Slot_3.value = GetData(rawData, i, PLC_Nozzle_Slot_3);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_4.adressAsInt)
        {
          this.PLC_Nozzle_Slot_4.value = GetData(rawData, i, PLC_Nozzle_Slot_4);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_5.adressAsInt)
        {
          this.PLC_Nozzle_Slot_5.value = GetData(rawData, i, PLC_Nozzle_Slot_5);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_6.adressAsInt)
        {
          this.PLC_Nozzle_Slot_6.value = GetData(rawData, i, PLC_Nozzle_Slot_6);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_7.adressAsInt)
        {
          this.PLC_Nozzle_Slot_7.value = GetData(rawData, i, PLC_Nozzle_Slot_7);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_8.adressAsInt)
        {
          this.PLC_Nozzle_Slot_8.value = GetData(rawData, i, PLC_Nozzle_Slot_8);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_9.adressAsInt)
        {
          this.PLC_Nozzle_Slot_9.value = GetData(rawData, i, PLC_Nozzle_Slot_9);
        }
        else if (PLCdata.address == PLC_Nozzle_Slot_10.adressAsInt)
        {
          this.PLC_Nozzle_Slot_10.value = GetData(rawData, i, PLC_Nozzle_Slot_10);
        }
        else if (PLCdata.address == PLC_OverWeight.adressAsInt)
        {
          this.PLC_OverWeight.value = GetData(rawData, i, PLC_OverWeight);
        }
        else if (PLCdata.address == PLC_1T.adressAsInt)
        {
          this.PLC_1T.value = GetData(rawData, i, PLC_1T);
        }
        else if (PLCdata.address == Buffer_PC_Data_1.adressAsInt)
        {
          this.Buffer_PC_Data_1.value = GetData(rawData, i, Buffer_PC_Data_1);
        }
        else if (PLCdata.address == Buffer_PC_Data_2.adressAsInt)
        {
          this.Buffer_PC_Data_2.value = GetData(rawData, i, Buffer_PC_Data_2);
        }
        else if (PLCdata.address == Buffer_PC_Data_3.adressAsInt)
        {
          this.Buffer_PC_Data_3.value = GetData(rawData, i, Buffer_PC_Data_3);
        }
        else if (PLCdata.address == Buffer_PC_Data_4.adressAsInt)
        {
          this.Buffer_PC_Data_4.value = GetData(rawData, i, Buffer_PC_Data_4);
        }
        else if (PLCdata.address == Buffer_PC_Data_5.adressAsInt)
        {
          this.Buffer_PC_Data_5.value = GetData(rawData, i, Buffer_PC_Data_5);
        }
        else if (PLCdata.address == Buffer_PC_Data_6.adressAsInt)
        {
          this.Buffer_PC_Data_6.value = GetData(rawData, i, Buffer_PC_Data_6);
        }
        //------------ PC_Value_BaoBi
        else if (PLCdata.address == PC_Value_BaoBi.adressAsInt)
        {
          this.PC_Value_BaoBi.value = GetData(rawData, i, PC_Value_BaoBi);
        }
        //Weigher_Gross_Slot_1
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_1.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_1.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_1);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_2.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_2.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_2);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_3.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_3.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_3);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_4.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_4.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_4);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_5.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_5.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_5);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_6.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_6.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_6);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_7.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_7.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_7);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_8.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_8.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_8);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_9.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_9.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_9);
        }
        else if (PLCdata.address == PLC_Gross_Weigher_Slot_10.adressAsInt)
        {
          this.PLC_Gross_Weigher_Slot_10.value = GetData(rawData, i, PLC_Gross_Weigher_Slot_10);
        }
      }/* for (int i = 0; i < rawData.list_Raw_Data.Count; i++)*/
    }
  }

  public class PLCFx5U_RawData
  {
    public PLCFx5U_RawData(int startAddress, int maxLength)
    {
      for (int i = 0; i < maxLength; i++)
      {
        TcpComm.FX_DATA data = new FX_DATA(FX_DEVICE.D, startAddress++);
        list_Raw_Data.Add(data);
      }
    }
    public List<TcpComm.FX_DATA> list_Raw_Data = new List<FX_DATA>();
  }

  public enum ePLC_ControlStatus //D1020
  {
    PLC_Machine_Run = 0,
    PLC_Machine_Stop = 1,
    PLC_Machine_Alarm = 2,
    PLC_Machine_Reset = 3,
    PLC_Btai_Vao_Run = 4,
    PLC_Btai_Can_Run = 5,
    PLC_Btai_Ra_Run = 6,
    
    PLC_Reject_CLY_ON = 7,
    //M1108
    PLC_Reject_SW_ON = 8, //nut nhấn reject M1108

    PLC_Barcode_NG = 9,
    PLC_PC_Shutdown_Request = 10,
    PLC_Product_NG = 11,
    PLC_Product_OK = 12,
    PLC_Buzzer_Off = 13, //-----
    PLC_Man_mode = 14,
    PLC_Barcode_Disable = 15,
		//---------------------------------------------------------------------------------
		PLC_Weigher_Disable = 16,   //D1021.0 -- M1116
		
		
    PLC_Reject_Enable_Disable = 17, //user cho phép reject //D1021.1
    PLC_THUNG_OR_CHAI = 18, //D1021.2
		PLC_SwAutoManChangeoverByALC_Request = 19, //D1021.3 M1119
		PLC_Status_20, ////D1021.4
    PLC_Status_21,//5
    PLC_Status_22,//6
    PC_Select_Reject_Type,//7
    PLC_Status_24,
    PLC_Status_25,
    PLC_Status_26,
    PLC_Status_27,
    PLC_Status_28,
    PLC_Status_29,
    PLC_Status_30,
    PLC_Status_31,

  }

  public enum ePC_ControlStatus
  {
    PC_START_PB = 0,
    PC_STOP_PB = 1,
    PC_ALARM_RESET_PB = 2,
    PC_Man_mode = 3,
    PC_BT_TACH_CHAI_START_PB,
    PC_BT_TACH_CHAI_STOP_PB,
    PC_BT_CAN_START_PB,
    PC_BT_CAN_STOP_PB,
    PC_BT_REJECT_START_PB,
    PC_BT_REJECT_STOP_PB,
    PC_Reject_Cyl_ON, //nut nhan test xy-lanh ON
    PC_Reject_Cyl_OFF,//nut nhan test xy-lanh OFF
    PC_Weigher_Disable,
    PC_Barcode_NG,
    PC_Buzzer_OFF,
    PC_Reject_Test = 15,
    //-----------------------------------
    PC_Shutdown_OK = 16, //0
    PC_Counter_Reset_PB = 17,//1
    PC_Barcode_Disable = 18,//2
    PC_SwAutoManChangeoverByALC = 19, //3
		PC_Reject_Disable = 20,//4
		PC_Nozzle_Reset_PB = 21,//5
    PC_OpenDoor_PB = 22,//6
    PC_Select_Reject_Type_23,
    PC_Status_24,
    PC_Status_25,
    PC_Status_26,
    PC_Status_27,
    PC_Status_28,
    PC_Status_29,
    PC_Status_30,
    PC_Status_31,

  }

  public enum eErrorType
  {
    Error_Barcode_Comm = 0,
    Error_IND570_Comm = 1,
    Error_Btai_Vao = 2,
    Error_Btai_Can = 3,
    Error_Btai_Ra = 4,
    Error_Emergency_Stop = 5,
		Error_Sensor_Reject_Front = 6, ///
		Error_Sensor_Reject_Behind = 7, ///

    Error_Btai_Ra_Overload = 8,//--
		Error_Door_open_Coy_CW = 9,//--
		Error_Door_open_Front = 10,//---
    Error_sensor_weigh_IN = 11,//--
    Error_sensor_weigh_OUT = 12,//

    Error_IND570_Alarm = 13,
    Error_IND570_Over_Cap = 14,//----
    Error_Barcode_Fail = 15, ///


    Error_Dynamic_OFF = 16,//---
    Error_PS_Air_Fail = 17,//--

		//Add new
		Error_Door_open_Behind = 18,//---
		Error_sensor_CYL_Front = 19,//---
		Error_sensor_CYL_Behind = 20,//---
		Error_sensor_PowerSave = 21,//---
		Error_CYL_reject = 22,//---
		Error_23 = 23,
		Error_24 = 24,
		Error_25 = 25,
		Error_26 = 26,
		Error_27 = 27,
		Error_28 = 28,
		Error_29 = 29,
		Error_30 = 30,
		Error_31 = 31,

		End
  }
  public enum eWarningType
  {
    Warning_Box_Fail = 0,
		/// <summary>
		/// D1018.1 == M1033
		/// </summary>
		Warining_Save_Power_Mode = 1, //Warning_01 = 1, //M1033
		Warning_DYN_Fail = 2, //Warning_02 = 2, // M1034
		Warning_LD_front = 3, //Warning_03 = 3, //M1035
		Warning_LD_Behind = 4, //Warning_04 = 4, //M1036
		Warning_05 = 5, //M1035 - Cảnh báo sensor đầu vào băng tải infeed bị che hoặc hư
		Warning_06 = 6,
    Warning_07 = 7,
    Warning_08 = 8,
    Warning_09 = 9,
    Warning_10 = 10,
    Warning_11 = 11,
    Warning_12 = 12,
    Warning_13 = 13,
    Warning_14 = 14,
    Warning_15 = 15,
    Warning_16 = 16,
    Warning_17 = 17,
    Warning_18 = 18,
    Warning_19 = 19,
    Warning_20 = 20,
    Warning_21 = 21,
    Warning_22 = 22,
    Warning_23 = 23,
    Warning_24 = 24,
    Warning_25 = 25,
    Warning_26 = 26,
    Warning_27 = 27,
    Warning_28 = 28,
    Warning_29 = 29,
    Warning_30 = 30,
    Warning_31 = 31,



    End
  }


}
