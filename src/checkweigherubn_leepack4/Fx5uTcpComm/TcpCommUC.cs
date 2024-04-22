#define SUPPORT_BINARY
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Collections;
namespace TcpComm
{
  public partial class TcpCommUC : UserControl
  {  

    public delegate void NotifyStatus(object ent, STATUS status);
    public event NotifyStatus OnNotifyStatus;

    public delegate void ReadData(object ent, byte[] data_from_plc, bool IsMessageIdOK, bool IsCorrectCRC);
    public event ReadData OnReadData;
    //
    public delegate void ReadDeviceData(object ent, int index, List<FX_DATA> list_data, bool IsCorrectChecksum);
    public event ReadDeviceData OnReadDeviceData;

    /// <summary> 
    /// SocketTCP: plc disconnnect to computer event
    /// </summary> 
    public delegate void SocketTcpDisconnect(object sender, int plc_id);
    public event SocketTcpDisconnect OnSocketTcpDisconnect;

    /// <summary> 
    /// SocketTCP: received data from plc
    /// </summary> 
    public delegate void SocketTcpReceived(object sender, int plc_id, byte[] byte_received, int length);
    public event SocketTcpReceived OnSocketTcpReceived;


    private FX_DATA current_data = new FX_DATA();

    private const int COMM_MESSAGE_LENGTH = 128;

    byte byte_tmp0 = 0;
    byte byte_tmp1 = 0;
    byte byte_tmp2 = 0;
    byte byte_tmp3 = 0;


    private int _test_length = (-1);
    //
    private string _IPAddress = "192.168.3.250";//"192.168.0.1";
    private ushort _Port = 2000;

    
    
    private ETHERNET_PROTOCOL _ethernet_protocol = ETHERNET_PROTOCOL.SLMP_ASCII_CODES;
    private MASTER_SLAVE _master_slave = MASTER_SLAVE.MASTER; //defaut is master
    /// <summary> 
    /// Count times that slave not respond
    /// </summary> 
    private int nCountNotRespond = 0;
    /// <summary> 
    /// Maximum count times that slave not respond
    /// </summary> 
    private const int MAX_COUNT_NOT_RESPOND = 300;//300; //timebase is 10ms
    //
    private BackgroundWorker backgroundWorker1_Initialize;
    private BackgroundWorker backgroundWorker2_Communication;
    //private BackgroundWorker backgroundWorker3_ReadDeviceCyclic;

    private byte[] values_to_send = new byte[COMM_MESSAGE_LENGTH + 2];
    
    private int index = 0;

    private int _tcpComIndex = 1;

    //private TRANSCEIVER transceive_process = TRANSCEIVER.DO_NOTHING;
    
    private int message_id = (-1);
    //List<FX_DATA> list_data_aa = new List<FX_DATA>();
    private List<Error> list_error = new List<Error>();

    private SocketTcpServer _socketTcpServer;

    public string IPAddress
    {
      get { return _IPAddress; }
      set { _IPAddress = value; }
    }
    public ushort Port
    {
      get { return _Port; }
      set { _Port = value; }
    }

    public int Index
    {
      get { return _tcpComIndex; }
      set { _tcpComIndex = value; }
    }
    //public uint Timeout
    //{
    //  get { return _respond_timeout; }
    //  set { _respond_timeout = value; }
    //}
    public bool IsEthConnected
    {
      get { return IsEthConnectDevice; }
    }
    public UInt16 MessageLength
    {
      get { return Config.COMM_MESSAGE_LENGTH; }
      //set { Config.COMM_MESSAGE_LENGTH = value; }
    }

    public TcpCommUC()
    {
      InitializeComponent();
      //
      Build_Error();
      //
      Initialize_BackgroundWorker();
    }

    public bool GetConnectStatus()
    {
      return IsEthConnected;
    }

    private void Initialize_BackgroundWorker()
    {
      /* setup backgroundWorker1_Initialize */
      backgroundWorker1_Initialize = new BackgroundWorker();
      backgroundWorker1_Initialize.WorkerReportsProgress = true;
      backgroundWorker1_Initialize.WorkerSupportsCancellation = true;

      backgroundWorker1_Initialize.DoWork += backgroundWorker1_Initialize_DoWork;
      backgroundWorker1_Initialize.ProgressChanged += backgroundWorker1_Initialize_ProgressChanged;
      backgroundWorker1_Initialize.RunWorkerCompleted += backgroundWorker1_Initialize_RunWorkerCompleted;
      //backgroundWorker1_Communication
      /* setup backgroundWorker1_Communication */
      backgroundWorker2_Communication = new BackgroundWorker();
      backgroundWorker2_Communication.WorkerReportsProgress = true;
      backgroundWorker2_Communication.WorkerSupportsCancellation = true;

      backgroundWorker2_Communication.DoWork += backgroundWorker2_Communication_DoWork;
      backgroundWorker2_Communication.ProgressChanged += backgroundWorker2_Communication_ProgressChanged;
      backgroundWorker2_Communication.RunWorkerCompleted += backgroundWorker2_Communication_RunWorkerCompleted;
      //
      //backgroundWorker3_ReadDeviceCyclic = new BackgroundWorker();
      //backgroundWorker3_ReadDeviceCyclic.WorkerReportsProgress = true;
      //backgroundWorker3_ReadDeviceCyclic.WorkerSupportsCancellation = true;

      //backgroundWorker3_ReadDeviceCyclic.DoWork += backgroundWorker3_ReadDeviceCyclic_DoWork;
      //backgroundWorker3_ReadDeviceCyclic.ProgressChanged += backgroundWorker3_ReadDeviceCyclic_ProgressChanged;
      //backgroundWorker3_ReadDeviceCyclic.RunWorkerCompleted += backgroundWorker3_ReadDeviceCyclic_RunWorkerCompleted;
    }


    
    /****************************************************************************
 * ******************* ETHERNET COMMUICATION PART **************************
 * *************************************************************************/

    /// <summary> 
    /// Flag to save connect status: TRUE: OK; FALSE: FAIL.
    /// </summary> 
    private bool IsEthConnectDevice = false;
    /// <summary> 
    /// Flag to enable communication: TRUE: OK; FALSE: FAIL.
    /// </summary> 
    private bool IsEthEnableCommunication = false;

    /// <summary> 
    /// Flag to notify that process is in connect device
    /// </summary> 
    private bool IsInProcess_ConnectDevice = false;

    #region ComM Variables & DataTypes
    /*******************************************************************************/
    /*******************************************************************************
     * GLOBAL VARIABLES
     *******************************************************************************/
    private ComM_Cfg Config = new ComM_Cfg();
    // static ComM_Cfg ComMConfig = new ComM_Cfg();
    private ComM_EthMessageType TxMsgJustSend = new ComM_EthMessageType();


    private ComM_ReturnType RetValues = new ComM_ReturnType();
    /*******************************************************************************
     * INTERNAL DEFINITIONS TYPES
     *******************************************************************************/
    public enum ComM_BooleanType
    {
      COMM_TRUE,
      COMM_FALSE
    };
    /* internal ComM Eth message type */
    public struct ComM_EthMessageControlType
    {
      public ComM_EthMessageType Message;  /* payload data */
      public ComM_BooleanType Empty;    /* flag of empty */
    };
    /* buffer of COM port */
    public struct ComM_COMBufferType
    {
      public byte[] Buffer;
      public UInt16 Pointer;
      public bool IsCorrectCRC;
      public ushort transceive_process;
    };
    /* ComM state machine */
    public enum ComM_StateMachineType
    {
      COMM_CHECK_INCOMMING_DATA,
      COMM_TRANSMIT_MSG,
    };
    /******************************************************************************/
    /* DEFINITION OF LOCAL VARIABLE                                               */
    /******************************************************************************/
    private const int COMM_MAX_PAYLOAD_SIZE = 65;
    /* Tx Message Ethernet */
    private ComM_EthMessageControlType[] iTxEthPayload = new ComM_EthMessageControlType[COMM_MAX_PAYLOAD_SIZE];
    /* Rx Message Ethernet */
    private ComM_EthMessageControlType[] iRxEthPayload = new ComM_EthMessageControlType[COMM_MAX_PAYLOAD_SIZE];

    /* rx buffer */
    //private int receive_len_1 = 0;
    //private int receive_len_2 = 0;
    //private int receive_len_3 = 0;
    //

    private ComM_COMBufferType[] iRxBuffer = new ComM_COMBufferType[1];

    /* internal state processing */
    private ComM_StateMachineType iStateSM = ComM_StateMachineType.COMM_CHECK_INCOMMING_DATA;

    /* local Ethernet MBmaster  */
    private Master MBmaster;
    

    #endregion

    #region ComM
    private void WriteMultipleRegister(ushort ID, ushort startAddress, byte[] values)
    {
      byte unit = 0;
      //MBmaster.WriteMultipleRegister(ID, unit, startAddress, values);
      MBmaster.Write_data(ID, values);
    }
    private void ComM_iSendTxMessage(ComM_EthMessageType Msg)
    {
      //save message has just send
      TxMsgJustSend = Msg;
      WriteMultipleRegister(Msg.MessageId, 0x00, Msg.Data);
    }

    private void ComM_iMessageProcessing(ComM_COMBufferType RxBuff)
    {
      ComM_BooleanType keepGoing = ComM_BooleanType.COMM_TRUE;
      UInt16 i;
      /* coppy message to rx payload */
      for (i = 0; i < Config.COMM_MAX_PAYLOAD_SIZE && keepGoing == ComM_BooleanType.COMM_TRUE; i++)
      {
        /* check payload is empty */
        if (iRxEthPayload[i].Empty == ComM_BooleanType.COMM_TRUE)
        {
          iRxEthPayload[i].Message.Data = RxBuff.Buffer;
          iRxEthPayload[i].Message.MessageId = RxBuff.transceive_process;
          iRxEthPayload[i].Message.IsCorrectCRC = RxBuff.IsCorrectCRC;
          /* set not empty */
          iRxEthPayload[i].Empty = ComM_BooleanType.COMM_FALSE;
          /* exit loop check */
          keepGoing = ComM_BooleanType.COMM_FALSE;
        }
      }
    }
    public void SetIndex(int index)
    {
      _tcpComIndex = index;
    }

    private byte bool_to_byte(bool value)
    {
      if (value) return 1;
      else return 0;
    }
    private Error FindErrorByCode(int error_code)
    {
      Error error = null;
      bool IsExitLoop = false;

      for (int i = 0; (i < list_error.Count) && (IsExitLoop == false); i++)
      {
        if (list_error[i]._error_code == error_code)
        {
          error = list_error[i];
          IsExitLoop = true;
        }
      }
      return error;
    }

    private void Build_Error()
    {
      list_error.Clear();
      list_error.Add(new Error(0x0055, 
                              "CPU module requested other device to write data during RUN when write was not permitted during RUN.",
                              "Write data when write is permitted during RUN. Stop CPU module and then write data. data when write is permitted during RUN"));

      list_error.Add(new Error(0xC051,
                                "Maximum number of bit devices for which data can be read/written all at once is outside the allowable range.",
                                "Correct number of bit devices that can be read or written all at once, and send to CPU module again"));

      list_error.Add(new Error(0xC052,
                                  "Maximum number of word devices for which data can be read/ written all at once is outside the allowable range.",
                                  "Correct number of word devices that can read or write all at once, and send to CPU module again"));

      list_error.Add(new Error(0xC053,
                                  "Maximum number of bit devices for which data can be random read/written all at once is outside the allowable range.",
                                  "Correct number of bit devices that can be random read or written all at once, and send to CPU module again."));

      list_error.Add(new Error(0xC054,
                                  "Maximum number of word devices for which data can be random read/written all at once is outside the allowable range.",
                                  "Correct number of word devices that can be random read or written all at once, and send to CPU module again."));

      list_error.Add(new Error(0xC056,
                                  "Read or write request exceeds maximum address.",
                                  "Correct starting address or number of read and write points, and send to CPU module again. (Be careful not to exceed the maximum address.)"));

      list_error.Add(new Error(0xC059,
                                  "Error in command or subcommand specification. There is a command or subcommand that cannot be used by the CPU module. ",
                                  "Reconsider request contents. Send command or subcommand that can be used by the CPU module."));

      list_error.Add(new Error(0xC05B,
                                  "CPU module cannot read or write from/to specified device.",
                                  "Reconsider device to read or write."));

      list_error.Add(new Error(0xC05C,
                                  "Error in request contents. (Reading or writing by bit unit for word device, etc.)",
                                  "Correct request content, and send to CPU module again. (Subcommand correction, etc.)"));

      list_error.Add(new Error(0xC05F,
                                  "There is a request that cannot be executed for the target CPU module",
                                  "Correct network No., request station No., request destination module I/O No., or request destination module station No. Correct contents of write request and/or read request."));

      list_error.Add(new Error(0xC060,
                                  "Error in request contents. (Error in specification of data for bit device, etc.)",
                                  "Correct request content, and send to CPU module again. (Data correction, etc.)"));

      list_error.Add(new Error(0xC061,
                                  "Request data length does not match the number of data in the character section (part of text).",
                                  "After reconsidering and correcting content of text or length of request data in the header, send to CPU module again."));

      list_error.Add(new Error(0xC200,
                                  "Error in remote password.",
                                  "Correct remote password, and re-execute remote password lock and unlock."));
      list_error.Add(new Error(0xC204,
                                  "Different device requested remote password to be unlocked. ",
                                  "Request remote password lock from device that requested unlock of remote password."));
      
    }

    public void SendData(byte[] data_to_send, int len)
    {
      values_to_send = data_to_send;
      message_id = 0;
      
      //
      ComM_EthMessageType TxMsg = new ComM_EthMessageType();
      TxMsg.MessageId = (ushort)message_id;
      TxMsg.Data = values_to_send;
      ComM_AddMessageToQueue(TxMsg);
    }
    //private int calculate_link_time(int data_length)
    //{
    //}
    private bool IsSameDataToSend(byte[] data_to_send_1, byte[] data_to_send_2, int data_len)
    {
      bool ret = false;
      bool IsFoundDiff = false;
      try
      {
        if ((data_to_send_1.Length == data_len) && (data_to_send_2.Length == data_len))
        {
          for (int i = 0; (i < data_len) && (IsFoundDiff == false); i++)
          {
            if (data_to_send_1[i] == data_to_send_2[i])
            {
            }
            else
            {
              IsFoundDiff = true;
            }
          }
          //
          ret = (IsFoundDiff == false);
        }
      }
      catch
      {
      }
      return ret;
    }
    private ComM_ReturnType ComM_AddMessageToQueue(ComM_EthMessageType Msg)
    {
      ComM_ReturnType retVal = ComM_ReturnType.COMM_FAILED;
      UInt16 LocalCounter;
      ComM_BooleanType keepGoing;
      /* check if device is connect or not */
      if ((IsEthConnectDevice == true) && (IsEthEnableCommunication == true))
      {
        /* check tx queue is overload */
        keepGoing = ComM_BooleanType.COMM_TRUE;
        LocalCounter = 0;
        /* found the same message */
        bool IsFoundSameMessage = false;
        for (int i = 0; (i < Config.COMM_MAX_PAYLOAD_SIZE) && (IsFoundSameMessage == false); i++)
        {
          if ((iTxEthPayload[i].Message.Data.Length == Msg.Data.Length) &&
              (iTxEthPayload[i].Empty == ComM_BooleanType.COMM_FALSE) &&
              (IsSameDataToSend(iTxEthPayload[i].Message.Data, Msg.Data, Msg.Data.Length))
              )
          {
            IsFoundSameMessage = true;
          }
        }
        if (IsFoundSameMessage == false)
        {
          /* loop check payload is available */
          while (keepGoing == ComM_BooleanType.COMM_TRUE && LocalCounter < Config.COMM_MAX_PAYLOAD_SIZE)
          {
            /* check flag Empty */
            if (iTxEthPayload[LocalCounter].Empty == ComM_BooleanType.COMM_TRUE)
            {
              /* exit loop check */
              keepGoing = ComM_BooleanType.COMM_FALSE;
            }
            else
            {
              /* do nothing */
              LocalCounter++;
            }
          }
          /* check payload is available slot */
          if (keepGoing == ComM_BooleanType.COMM_FALSE)
          {
            /* set tx queue message is not empty */
            iTxEthPayload[LocalCounter].Message.MessageId = Msg.MessageId;
            iTxEthPayload[LocalCounter].Message = Msg;
            iTxEthPayload[LocalCounter].Empty = ComM_BooleanType.COMM_FALSE;
          }
          else
          {
            /* return overload TX queue */
            retVal = ComM_ReturnType.COMM_OVERLOAD_TX;
          }
        }
        else
        {
          retVal = ComM_ReturnType.COMM_OK;
        }
        
      }/*if (IsEthConnectDevice == true)*/
      return retVal;
    }


    private ComM_EthMessageType ComM_ReadMsgFromQueue()
    {
      ComM_EthMessageType retVal;
      UInt16 LocalCounter;
      ComM_BooleanType keepGoing;
      /* check rx queue is available data */
      keepGoing = ComM_BooleanType.COMM_TRUE;
      LocalCounter = 0;
      /* loop check payload is available */
      while (keepGoing == ComM_BooleanType.COMM_TRUE && LocalCounter < Config.COMM_MAX_PAYLOAD_SIZE)
      {
        /* check flag Empty */
        if (iRxEthPayload[LocalCounter].Empty == ComM_BooleanType.COMM_FALSE)
        {
          /* exit loop check */
          keepGoing = ComM_BooleanType.COMM_FALSE;
        }
        else
        {
          /* do nothing */
          LocalCounter++;
        }

      }
      /* check payload is available slot */
      if (keepGoing == ComM_BooleanType.COMM_FALSE)
      {
        /* set tx queue message is not empty */
        iRxEthPayload[LocalCounter].Empty = ComM_BooleanType.COMM_TRUE;
        /* copy port name */
        retVal = iRxEthPayload[LocalCounter].Message;
      }
      else
      {
        /* return overload TX queue */
        retVal = null;
      }
      return retVal;
    }

    private void ComM_MainFunction()
    {
      UInt16 LocalCounter;
      switch (iStateSM)
      {
        case ComM_StateMachineType.COMM_CHECK_INCOMMING_DATA:
          /* check length of data received */
          if (iRxBuffer[0].Pointer > 0)
          {
            /* coppy message to rx payload */
            ComM_iMessageProcessing(iRxBuffer[0]);
            /* Processing data */
            byte[] data = new byte[Config.COMM_MESSAGE_LENGTH];
            ComM_EthMessageType RxMsg = null;
            RxMsg = ComM_ReadMsgFromQueue();

            //
            List<FX_DATA> list_data_aa = new List<FX_DATA>();
            if (RxMsg != null)
            {
              Array.Copy(RxMsg.Data, 0, data, 0, data.Length);
              list_data_aa = Filter_CorrectData(ProcessDataFromPLC(data));
            }
            //
            if (list_data_aa.Count > 0)
            {
              if (OnReadDeviceData != null)
              {
                OnReadDeviceData(this, _tcpComIndex, list_data_aa, false);
              }
            }

            /* Clear pointer */
            iRxBuffer[0].Pointer = 0;
            iStateSM = ComM_StateMachineType.COMM_TRANSMIT_MSG;
          }
          else
          {
            if (nCountNotRespond >= MAX_COUNT_NOT_RESPOND)
            {              
              //
              iStateSM = ComM_StateMachineType.COMM_TRANSMIT_MSG;
              //
              nCountNotRespond = 0;


              ReInit(_ethernet_protocol);

              if (IsEthConnectDevice == true)
              {
                if (OnNotifyStatus != null)
                {
                  OnNotifyStatus(this, STATUS.INIT_OK);
                }
              }
              //Init(_ethernet_protocol);
              nCountNotRespond = 0;
              
            }
            else
            {
              nCountNotRespond++;
            }
          }


          //if (iRxBuffer[0].Pointer > 0)
          //{
          //  if (receive_len_1 == 0)
          //  {
          //    receive_len_1 = iRxBuffer[0].Pointer;
          //  }
          //  else
          //  {
          //    if (receive_len_2 == 0)
          //    {
          //      receive_len_2 = iRxBuffer[0].Pointer;
          //    }
          //    else
          //    {
          //      if (receive_len_3 == 0)
          //      {
          //        receive_len_3 = iRxBuffer[0].Pointer;
          //      }
          //    }
          //  }
          //  //
          //  if ((receive_len_1 > 0) && (receive_len_1 == receive_len_2) && (receive_len_2 == receive_len_3))
          //  {
          //    /* coppy message to rx payload */
          //    ComM_iMessageProcessing(iRxBuffer[0]);
          //    /* Processing data */
          //    byte[] data = new byte[Config.COMM_MESSAGE_LENGTH];
          //    ComM_EthMessageType RxMsg = null;
          //    RxMsg = ComM_ReadMsgFromQueue();
          //    {
          //      if (RxMsg != null)
          //      {
          //        Array.Copy(RxMsg.Data, 0, data, 0, data.Length);
          //        list_data_aa = ProcessDataFromPLC(data);
          //      }
          //    }


          //    /* Clear pointer */
          //    iRxBuffer[0].Pointer = 0;
          //    iStateSM = ComM_StateMachineType.COMM_TRANSMIT_MSG;
          //  }
          //  else
          //  {
          //    if ((receive_len_1 > 0) && (receive_len_2 > 0) && (receive_len_3 > 0))
          //    {
          //      /* Clear pointer */
          //      iRxBuffer[0].Pointer = 0;
          //      iStateSM = ComM_StateMachineType.COMM_TRANSMIT_MSG;
          //      receive_len_1 = 0;
          //      receive_len_2 = 0;
          //      receive_len_3 = 0;
          //      list_data_aa.Clear();
          //    }
          //  }
          //}
          //else
          //{
          //  iStateSM = ComM_StateMachineType.COMM_TRANSMIT_MSG;
          //  receive_len_1 = 0;
          //  receive_len_2 = 0;
          //  receive_len_3 = 0;
          //  list_data_aa.Clear();
          //}
          break;
        case ComM_StateMachineType.COMM_TRANSMIT_MSG:
          
          bool IsExitLoop = false;
          for (LocalCounter = 0; (LocalCounter < Config.COMM_MAX_PAYLOAD_SIZE) && (IsExitLoop == false); LocalCounter++)
          {
            /* check tx payload available data */
            if (iTxEthPayload[LocalCounter].Empty == ComM_BooleanType.COMM_FALSE)
            {
              ComM_iSendTxMessage(iTxEthPayload[LocalCounter].Message);
              //
              iTxEthPayload[LocalCounter].Empty = ComM_BooleanType.COMM_TRUE;
              IsExitLoop = true;
            }
          }
          /* detect something to sending, switch to waiting message */
          if (IsExitLoop == true)
          {
            iStateSM = ComM_StateMachineType.COMM_CHECK_INCOMMING_DATA;
          }
          else
          {
            iStateSM = ComM_StateMachineType.COMM_TRANSMIT_MSG;
            //Read_DeviceMemory("D2000", 10, PROTOCOL_UNIT._x1_WORD);
          }
          nCountNotRespond = 0;
          break;
        default: break;
      }
    }
    #region backgroundWorker3_ReadDeviceCyclic
    //private void backgroundWorker3_ReadDeviceCyclic_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //{
    //  if (backgroundWorker3_ReadDeviceCyclic.IsBusy == false)
    //  {
    //    backgroundWorker3_ReadDeviceCyclic.RunWorkerAsync();
    //  }
    //}
    //private void backgroundWorker3_ReadDeviceCyclic_ProgressChanged(object sender, ProgressChangedEventArgs e)
    //{
    //  if (!backgroundWorker3_ReadDeviceCyclic.CancellationPending)
    //  {
    //    if (e.ProgressPercentage == 100)
    //    {
          
    //    }
    //    else
    //    {
          
    //    }
    //  }
    //}
    //private void backgroundWorker3_ReadDeviceCyclic_DoWork(object sender, DoWorkEventArgs e)
    //{
    //  backgroundWorker3_ReadDeviceCyclic.ReportProgress(0);
    //  if ((IsEthEnableCommunication == true) && (IsEthConnectDevice == true))
    //  {
    //    Read_DeviceMemory("D1000", 2, PROTOCOL_UNIT._x1_WORD);
    //    Thread.Sleep(10);
    //  }
    //  else
    //  {
    //  }
      
    //  backgroundWorker3_ReadDeviceCyclic.ReportProgress(100);
    //}
    #endregion



    #region Process for backgroundWorker2_Communication
    private void backgroundWorker2_Communication_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      //if (list_data_aa.Count > 0)
      //{
      //  if (OnReadDeviceData != null)
      //  {
      //    OnReadDeviceData(this, _tcpComIndex, list_data_aa, false);
      //  }
      //  list_data_aa.Clear();
      //}
      //if (backgroundWorker2_Communication.IsBusy == false)
      //{
      //  backgroundWorker2_Communication.RunWorkerAsync();
      //}

    }

    private void backgroundWorker2_Communication_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (!backgroundWorker2_Communication.CancellationPending)
      {
        if (e.ProgressPercentage == 100)
        {
          
        }
        else
        {
          
        }
      }
    }

    private void timer_communication_Tick(object sender, EventArgs e)
    {
      if ((IsEthEnableCommunication == true) && (IsEthConnectDevice == true))
      {
        ComM_MainFunction();
        //
        //Thread.Sleep(1);
      }
      else
      {
      }
    }

    private void backgroundWorker2_Communication_DoWork(object sender, DoWorkEventArgs e)
    {
      backgroundWorker2_Communication.ReportProgress(0);
      //if ((IsEthEnableCommunication == true) && (IsEthConnectDevice == true))
      //{
      //  ComM_MainFunction();
      //  //
      //  Thread.Sleep(10);
      //}
      //else
      //{
      //}
      backgroundWorker2_Communication.ReportProgress(100);
    }
    #endregion



    #region Process for backgroundWorker1_Initialize

    
    private void backgroundWorker1_Initialize_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (IsEthConnectDevice == true)
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.INIT_OK);
        }

      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.INIT_FAILED);
        }
      }

      IsEthEnableCommunication = IsEthConnectDevice;
      
      /* try to reconnect again */
      if (IsEthConnectDevice == false)
      {
        if (backgroundWorker1_Initialize.IsBusy == false)
        {
          backgroundWorker1_Initialize.RunWorkerAsync();
        }
      }
      else
      {
        IsInProcess_ConnectDevice = false;
      }
    }

    private void backgroundWorker1_Initialize_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (e.ProgressPercentage == 0)
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.TRY_INIT_AGAIN);//TRY_INIT_AGAIN
        }
      }
      //if (!backgroundWorker1_Initialize.CancellationPending)
      //{

      //}
    }

    private void backgroundWorker1_Initialize_DoWork(object sender, DoWorkEventArgs e)
    {
      backgroundWorker1_Initialize.ReportProgress(0);
      if (_master_slave == MASTER_SLAVE.MASTER)
      {
        IsInProcess_ConnectDevice = true;
        //
        ComM_ReturnType status = ComM_EthInit(_IPAddress, _Port);
        //
        IsEthConnectDevice = (status == ComM_ReturnType.COMM_OK);
        IsEthEnableCommunication = (status == ComM_ReturnType.COMM_OK);
        //
        if (IsEthConnectDevice == false)
        {
          Thread.Sleep(100);
        }
        if (IsEthConnectDevice == true)
        {
          timer_communication.Enabled = true;
        }
      }
      else
      {
      }
      backgroundWorker1_Initialize.ReportProgress(100);
    }
    #endregion

    private void timer1_Tick(object sender, EventArgs e)
    {
      if ((IsEthEnableCommunication == true) && (IsEthConnectDevice == true))
      {
        //Read_DeviceMemory("D1000", 2, PROTOCOL_UNIT._x1_WORD);
      }
    }
    public void DeInit()
    {
      if (backgroundWorker1_Initialize.IsBusy)
      {
        backgroundWorker1_Initialize.CancelAsync();
      }
      //
      if (backgroundWorker2_Communication.IsBusy)
      {
        backgroundWorker2_Communication.CancelAsync();
      }
      //
      IsEthConnectDevice = false;
      IsEthEnableCommunication = false;
      //
      //if (backgroundWorker3_ReadDeviceCyclic.IsBusy)
      //{
      //  backgroundWorker3_ReadDeviceCyclic.CancelAsync();
      //}
      if (_master_slave == MASTER_SLAVE.SLAVE)
      {
        if (_socketTcpServer != null)
        {
          _socketTcpServer.disconnect();
        }
      }

      if (OnNotifyStatus != null)
      {
        OnNotifyStatus(this, STATUS.CLOSE_DONE);
      }
    }

    public void ReInit(string IP, ushort Port, ETHERNET_PROTOCOL ethernet_protocol)
    {
      _IPAddress = IP;
      _Port = Port;
      _ethernet_protocol = ethernet_protocol;
      //calling Init 
      ReInit(ethernet_protocol);
    }

    public void ReInit(ETHERNET_PROTOCOL ethernet_protocol)
    {
      _ethernet_protocol = ethernet_protocol;
      //
      IsEthConnectDevice = false; //reset value at the begin of init
      IsEthEnableCommunication = false; //reset value at the begin of init
      nCountNotRespond = 0;
      //
      if (backgroundWorker1_Initialize.IsBusy == false)
      {
        backgroundWorker1_Initialize.RunWorkerAsync();
      }

      

      /* calling init at first time */
      //ComM_ReturnType status = ComM_EthInit(_IPAddress, _Port);

      //
      //if (status != ComM_ReturnType.COMM_OK)
      //{
      //  /* switch to background connect */
      //  if (backgroundWorker1_Initialize.IsBusy)
      //  {
      //    backgroundWorker1_Initialize.CancelAsync();
      //  }
      //  else
      //  {
      //    /* switch to connect by background */
      //    backgroundWorker1_Initialize.RunWorkerAsync();
      //  }
      //}

      /* Notify status after init */
      //if (OnNotifyStatus != null)
      //{
      //  if (status == ComM_ReturnType.COMM_OK)
      //  {
      //    OnNotifyStatus(this, STATUS.INIT_OK);
      //  }
      //  else
      //  {
      //    OnNotifyStatus(this, STATUS.INIT_FAILED);
      //  }
      //}
      //IsEthConnectDevice = (status == ComM_ReturnType.COMM_OK);
      //IsEthEnableCommunication = IsEthConnectDevice;
      ////timer_sending_cycle.Enabled = true;
      //timer_communication.Enabled = true;
      ////
      //if (backgroundWorker2_Communication.IsBusy == false)
      //{
      //  //backgroundWorker2_Communication.RunWorkerAsync();
      //}
    }


    public void Init(string IP, ushort Port, ETHERNET_PROTOCOL ethernet_protocol)
    {
      _IPAddress = IP;
      _Port = Port;
      _ethernet_protocol = ethernet_protocol;
      _master_slave = MASTER_SLAVE.MASTER;
      //calling Init 
      Init(ethernet_protocol);
    }

    public void Init(string IP, ushort Port, ETHERNET_PROTOCOL ethernet_protocol, MASTER_SLAVE master_slave)
    {
      _IPAddress = IP;
      _Port = Port;
      _ethernet_protocol = ethernet_protocol;
      _master_slave = master_slave;
      //calling Init 
      Init(ethernet_protocol);
    }

    public void Init(ETHERNET_PROTOCOL ethernet_protocol, MASTER_SLAVE master_slave)
    {
      _master_slave = master_slave;
      //calling Init 
      Init(ethernet_protocol);
    }

    public void Init(ETHERNET_PROTOCOL ethernet_protocol)
    {
      _ethernet_protocol = ethernet_protocol;
      //
      IsEthConnectDevice = false; //reset value at the begin of init
      IsEthEnableCommunication = false; //reset value at the begin of init
      nCountNotRespond = 0;
      //
        /* calling init at first time */
      ComM_ReturnType status = ComM_EthInit(_IPAddress, _Port);

      //

      IsEthConnectDevice = (status == ComM_ReturnType.COMM_OK);
      IsEthEnableCommunication = IsEthConnectDevice;
      //timer_sending_cycle.Enabled = true;
      timer_communication.Enabled = true;

      /* Notify status after init */
      if (OnNotifyStatus != null)
      {
        if (status == ComM_ReturnType.COMM_OK)
        {
          OnNotifyStatus(this, STATUS.INIT_OK);
        }
        else
        {
          OnNotifyStatus(this, STATUS.INIT_FAILED);
        }
      }
      

      if (_master_slave == MASTER_SLAVE.MASTER)
      {
        /* switch to using backgroundWorker1 */
        if (status != ComM_ReturnType.COMM_OK)
        {
          /* switch to background connect */
          if (backgroundWorker1_Initialize.IsBusy)
          {
            backgroundWorker1_Initialize.CancelAsync();
          }
          else
          {
            /* switch to connect by background */
            backgroundWorker1_Initialize.RunWorkerAsync();
          }
        }
      }
      else
      {
        /* do nothing */
      }





      //
      //if (backgroundWorker2_Communication.IsBusy == false)
      //{
      //  //backgroundWorker2_Communication.RunWorkerAsync();
      //}
      //
      //if (backgroundWorker3_ReadDeviceCyclic.IsBusy == false)
      //{
      //  backgroundWorker3_ReadDeviceCyclic.RunWorkerAsync();
      //}
    }

    /*******************************************************************************
      ** Function name: ComM_Init( void )
      ** Description  : The function shall be init module, include hardware and software
      ** Parameter    : Notif - list of notification function callback
      **                NodeID - network node ID
      ** Return value : ComM_ReturnType 
      ** Remarks      : global variables used, side effects
      *******************************************************************************/
    private ComM_ReturnType ComM_EthInit(string IP, ushort Port)
    {
      UInt16 LocalCounter;
      ComM_ReturnType ret = ComM_ReturnType.COMM_FAILED;
      //

      IsEthConnectDevice = false; //reset value at the begin of init
      IsEthEnableCommunication = false; //reset value at the begin of init
      nCountNotRespond = 0;
      //
      //timer_sending_cycle.Enabled = true;

      /* set all flags of Empty rx & tx queue payload to true */
      for (LocalCounter = 0; LocalCounter < Config.COMM_MAX_PAYLOAD_SIZE; LocalCounter++)
      {
        /* tx queue */
        iTxEthPayload[LocalCounter].Empty = ComM_BooleanType.COMM_TRUE;
        iTxEthPayload[LocalCounter].Message = new ComM_EthMessageType();
        /* rx queue */
        iRxEthPayload[LocalCounter].Empty = ComM_BooleanType.COMM_TRUE;
        iRxEthPayload[LocalCounter].Message = new ComM_EthMessageType();
      }
      iRxBuffer[0].Buffer = new byte[Config.COMM_ETH_DATA_BUFFER_LEN];
      //
      iStateSM = ComM_StateMachineType.COMM_TRANSMIT_MSG;
      //
      /* ethernet config */
      try
      {
        if (_master_slave == MASTER_SLAVE.MASTER)
        {
          // Create new master and add event functions
          if (MBmaster == null)
          {
            MBmaster = new Master(IP, Port); //connect to ethernt device at this line.
            MBmaster.DataLen = Config.COMM_ETH_DATA_BUFFER_LEN;
            MBmaster.OnResponseData += new Master.ResponseData(MBmaster_OnResponseData);
            MBmaster.OnException += new Master.ExceptionData(MBmaster_OnException);
          }
          else
          {
            MBmaster.OnResponseData -= new Master.ResponseData(MBmaster_OnResponseData);
            MBmaster.OnException -= new Master.ExceptionData(MBmaster_OnException);
            //
            //MBmaster = new Master(IP, Port); //connect to ethernt device at this line.
            MBmaster.connect(IP, Port);
            MBmaster.DataLen = Config.COMM_ETH_DATA_BUFFER_LEN;
            MBmaster.OnResponseData += new Master.ResponseData(MBmaster_OnResponseData);
            MBmaster.OnException += new Master.ExceptionData(MBmaster_OnException);
          }
          //
          if (MBmaster.connected == true)
          {
            Thread.Sleep(100);
            Console.WriteLine("Connect to device sucessfull!!!");
            ret = ComM_ReturnType.COMM_OK;

          }
          else
          {
            Console.WriteLine("Connect to device fail!!!");
            try
            {
              if (MBmaster != null) MBmaster.disconnect();
            }
            catch
            {
            }
            ret = ComM_ReturnType.COMM_FAILED;
          }
        }
        else if (_master_slave == MASTER_SLAVE.SLAVE)
        {
          try
          {
            if (_socketTcpServer == null)
            {
              string my_computer_ip_address = _IPAddress;
              _socketTcpServer = new SocketTcpServer(my_computer_ip_address); //connect to ethernet device at this line.
            }
            else/*if (_slave == null)*/
            {
              /* do nothing */
            }
            timer_get_SocketTCP_data.Enabled = true;
          }
          catch (Exception e)
          {
            string mess = e.Message;
            int mm = 0;
          }
        }
        else /*else if (_master_slave == MASTER_SLAVE.SLAVE)*/
        {
          /* do nothing */
        }
      }
      catch (SystemException error)
      {
        //IsEthConnectDevice = false;
        //int mmm = 0;



      }
      /* init success */
      return ret;
    }

    


    private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception)
    {
      if (exception == 254)
      {
        int mmm = 0;

        //this.MBmaster.disconnect();
        //this.MBmaster.connect_new_1(_IPAddress, _Port);
        //if (backgroundWorker1_Initialize.IsBusy == )
        //if (IsInProcess_ConnectDevice == false)
        //{
        //  Init(_ethernet_protocol);
        //}
      }
    }
    // ------------------------------------------------------------------------
    // Event for response data
    // ------------------------------------------------------------------------
    private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values, bool IsCorrectCRC)
    {
      // ------------------------------------------------------------------
      // Seperate calling threads
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new Master.ResponseData(MBmaster_OnResponseData), new object[] { ID, unit, function, values, IsCorrectCRC });
        return;
      }
      try
      {
        iRxBuffer[0].transceive_process = ID;
        iRxBuffer[0].IsCorrectCRC = IsCorrectCRC;
       if (iRxBuffer[0].Pointer >= Config.COMM_ETH_DATA_BUFFER_LEN)
       {
         iRxBuffer[0].Pointer = 0;
       }
        for (int LocalCounter = 0; LocalCounter < values.Length; LocalCounter++)
        {
          if (iRxBuffer[0].Pointer < Config.COMM_ETH_DATA_BUFFER_LEN)
          {
            iRxBuffer[0].Buffer[iRxBuffer[0].Pointer] = values[LocalCounter];
            iRxBuffer[0].Pointer += 1;
          }
        }
      }
      catch
      {
        iRxBuffer[0].Pointer = 0;
      }
    }
    //private void BackgroundProcessing()
    //{
    //  while (!ShutDown)
    //  {
    //    ComM_MainFunction();
    //    if (WaitTimeOut > 0)
    //    {
    //      WaitTimeOut--;
    //    }
    //    Thread.Sleep(1);
    //  }
    //}
    private int GetByteFromFloat(double fdata, ref byte data0, ref byte data1, ref byte data2, ref byte data3)
    {
      byte[] arr = new byte[4];
      float value = (float)fdata;
      arr = BitConverter.GetBytes(value);
      data0 = arr[3];
      data1 = arr[2];
      data2 = arr[1];
      data3 = arr[0];
      return 0;
    }
    //private void save_values_int_to_byte(int value)
    //{
    //  values_to_send[index++] = (byte)(value >> 8);
    //  values_to_send[index++] = (byte)(value);
    //}

    private int GetByteFromFloat(double fdata, int id)
    {
      GetByteFromFloat(fdata, ref byte_tmp0, ref byte_tmp1, ref byte_tmp2, ref byte_tmp3);
      values_to_send[id] = byte_tmp0;
      values_to_send[id + 1] = byte_tmp1;
      values_to_send[id + 2] = byte_tmp2;
      values_to_send[id + 3] = byte_tmp3;

      index = index + 4;
      return 0;
    }

    private void TcpCommUC_Resize(object sender, EventArgs e)
    {
      this.Width = 32;
      this.Height = 32;
    }

    
    
    #endregion
    
    #region FX5U_SLMP Protocol 
    private byte[] Build_Header(SLMP_HEADER header_request)
    {
#if SUPPORT_BINARY
      List<byte> list_header_bytes = new List<byte>();
      if (header_request == SLMP_HEADER.SEND_TO_PLC)
      {
        if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_BINARY_CODES)
        {
          /* sub_header: 2 bytes */
          list_header_bytes.Add(0x50);
          list_header_bytes.Add(0x00);

          /* page 25:
           *  The stations of network number 240 to 255 cannot be accessed.
              FX5CPU cannot perform multi-drop connection.
              FX5CPU cannot perform connection via network.
           */
          /* network number: 1 bytes - page 24(use specification No.1 to access FX5CPU)*/
          list_header_bytes.Add(0x00); //OK
          /* request destination station number  - page 24(use specification No.1 to access FX5CPU)*/
          list_header_bytes.Add(0xFF); //OK

          /* request destination module I/O number - page 25 --> Own station: 0x03FF */
          list_header_bytes.Add(0xFF); //OK
          list_header_bytes.Add(0x03); //OK


          /* request destination multidrop station number - page 26  -- Other than above */
          list_header_bytes.Add(0x00); //OK
        }
        else if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_ASCII_CODES)
        {
          /* sub_header: 4 bytes */
          list_header_bytes.Add(Convert.ToByte('5'));
          list_header_bytes.Add(Convert.ToByte('0'));
          list_header_bytes.Add(Convert.ToByte('0'));
          list_header_bytes.Add(Convert.ToByte('0'));
          /* network number: 2 bytes */
          list_header_bytes.Add(Convert.ToByte('0')); //OK
          list_header_bytes.Add(Convert.ToByte('0')); //OK
          /* request destination station number */
          list_header_bytes.Add(Convert.ToByte('F')); //OK
          list_header_bytes.Add(Convert.ToByte('F')); //OK
          /* request destination module I/O number */
          list_header_bytes.Add(Convert.ToByte('0')); //OK
          list_header_bytes.Add(Convert.ToByte('3')); //OK
          list_header_bytes.Add(Convert.ToByte('F')); //OK
          list_header_bytes.Add(Convert.ToByte('F')); //OK
          /* request destination multidrop station number */
          list_header_bytes.Add(Convert.ToByte('0')); //OK
          list_header_bytes.Add(Convert.ToByte('0')); //OK
        }
      }
      else if (header_request == SLMP_HEADER.RECEIVE_FROM_PLC)
      {
      }
      return list_header_bytes.ToArray();
#else
      

      //byte[] header_byte = new byte[14];
      //if (header_request == SLMP_HEADER.SEND_TO_PLC)
      //{
      //  /* sub_header: 4 bytes */
      //  header_byte[0] = Convert.ToByte('5');
      //  header_byte[1] = Convert.ToByte('0');
      //  header_byte[2] = Convert.ToByte('0');
      //  header_byte[3] = Convert.ToByte('0');
      //  /* network number: 2 bytes */
      //  header_byte[4] = Convert.ToByte('0'); //OK
      //  header_byte[5] = Convert.ToByte('0'); //OK
      //  /* request destination station number */
      //  header_byte[6] = Convert.ToByte('F'); //OK
      //  header_byte[7] = Convert.ToByte('F'); //OK
      //  /* request destination module I/O number */
      //  header_byte[8] = Convert.ToByte('0'); //OK
      //  header_byte[9] = Convert.ToByte('3'); //OK
      //  header_byte[10] = Convert.ToByte('F'); //OK
      //  header_byte[11] = Convert.ToByte('F'); //OK
      //  /* request destination multidrop station number */
      //  header_byte[12] = Convert.ToByte('0'); //OK
      //  header_byte[13] = Convert.ToByte('0'); //OK
      //}
      //else if (header_request == SLMP_HEADER.RECEIVE_FROM_PLC)
      //{
      //}
      //return header_byte;
      
#endif
    }
    private string convert_SLMP_ErrorCode(string error_code)
    {
      string error_description = "";
      try
      {
        int error_code_as_int = int.Parse(String.Format("0x{0}", error_code));
        if (error_code_as_int == 0x0055)
        {
          error_description = "CPU module requested other device to write data during RUN when write was not permitted during RUN.";
        }
        else if ((error_code_as_int >= 0x4000) && (error_code_as_int <= 0x4FFF))
        {
          error_description = "Errors detected by CPU module.(Errors that occurred in other than SLMP communication function)";
        }
        else if (error_code_as_int == 0xC051)
        {
          error_description = "Maximum number of bit devices for which data can be read/written all at once is outside the allowable range";
        }
        else if (error_code_as_int == 0xC052)
        {
          error_description = "Maximum number of word devices for which data can be read/written all at once is outside the allowable range.";
        }
        else if (error_code_as_int == 0xC053)
        {
          error_description = "Maximum number of bit devices for which data can be random read/written all at once is outside the allowable range.";
        }
        else if (error_code_as_int == 0xC054)
        {
          error_description = "Maximum number of word devices for which data can be random read/written all at once is outside the allowable range.";
        }
        else if (error_code_as_int == 0xC056)
        {
          error_description = "Read or write request exceeds maximum address.";
        }
        else if (error_code_as_int == 0xC059)
        {
          error_description = "Error in command or subcommand specification. There is a command or subcommand that cannot be used by the CPU module";
        }
        else if (error_code_as_int == 0xC05B)
        {
          error_description = "CPU module cannot read or write from/to specified device.";
        }
        else if (error_code_as_int == 0xC05C)
        {
          error_description = "Error in request contents. (Reading or writing by bit unit for word device, etc.)";
        }
        else if (error_code_as_int == 0xC05F)
        {
          error_description = "There is a request that cannot be executed for the target CPU module";
        }
        else if (error_code_as_int == 0xC060)
        {
          error_description = "Error in request contents. (Error in specification of data for bit device, etc.)";
        }
        else if (error_code_as_int == 0xC061)
        {
          error_description = "Request data length does not match the number of data in the character section (part of text).";
        }
        else if (error_code_as_int == 0xC200)
        {
          error_description = "Error in remote password.";
        }
        else if (error_code_as_int == 0xC204)
        {
          error_description = "Different device requested remote password to be unlocked.";
        }
        else
        {
          error_description = "unknown error";
        }
      }
      catch
      {
      }
      
      return error_description;
    }
    private int bool_to_int(bool bool_value)
    {
      int ret = 0;
      if (bool_value == true)
      {
        ret = 1;
      }
      return ret;
    }

    private List<FX_DATA> Filter_CorrectData(List<FX_DATA> list_FX_DATA)
    {
      List<FX_DATA> list_correctData = new List<FX_DATA>();
      for (int i = 0; i < list_FX_DATA.Count; i++)
      {
        if (list_FX_DATA[i].fx_device != FX_DEVICE.ERROR_DATA)
        {
          list_correctData.Add(list_FX_DATA[i]);
        }
      }
      return list_correctData;
    }
    private List<FX_DATA> ProcessDataFromPLC(byte[] data_from_plc)
    {
      List<FX_DATA> list_FX_DATA = new List<FX_DATA>();
      if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_ASCII_CODES)
      {
        list_FX_DATA = ProcessDataFromPLC_ASCII(data_from_plc);
      }
      else if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_BINARY_CODES)
      {
        list_FX_DATA  = ProcessDataFromPLC_Binary(data_from_plc);
      }
      else
      {
      }
      return list_FX_DATA;
    }


    private List<FX_DATA> ProcessDataFromPLC_Binary(byte[] data_from_plc)
    {
      bool IsSubHeader_OK = false;
      bool IsNetwork_Number_OK = false;
      bool IsRequest_destination_station_number_OK = false;
      bool IsRequest_destination_module_IO_number_OK = false;
      bool IsRequest_destination_multi_drop_station_number_OK = false;
      bool IsEnCode_OK = false;
      int EndCode = 0xFF;
      int byte_idx = 0;
      int Response_data_length = 0;
      List<FX_DATA> list_data = new List<FX_DATA>();
      Error error_des = null;
      try
      {
        /* Get & check hearder */
        if (data_from_plc.Length >= 2)
        {
          IsSubHeader_OK = ((data_from_plc[byte_idx++] == 0xD0) && //0
                            (data_from_plc[byte_idx++] == 0x00)); //1


        }
        /* Get & check sub_header */
        if ((data_from_plc.Length >= 3) && (IsSubHeader_OK == true))
        {
          IsNetwork_Number_OK = ((data_from_plc[byte_idx++] == 0x00)); //2
        }
        /* Get & check Network_Number */
        if ((data_from_plc.Length >= 4) && (IsNetwork_Number_OK == true))
        {
          IsRequest_destination_station_number_OK = ((data_from_plc[byte_idx++] == 0xFF)); //3
        }
        /* Get & check destination_station_number */
        if ((data_from_plc.Length >= 6) && (IsRequest_destination_station_number_OK == true))
        {
          IsRequest_destination_module_IO_number_OK = ((data_from_plc[byte_idx++] == 0xFF) && //4
                            (data_from_plc[byte_idx++] == 0x03)); //5
        }
        if ((data_from_plc.Length >= 7) && (IsRequest_destination_module_IO_number_OK == true))
        {
          IsRequest_destination_multi_drop_station_number_OK = ((data_from_plc[byte_idx++] == 0x00)); //6
        }
        if ((data_from_plc.Length >= 9) && (IsRequest_destination_multi_drop_station_number_OK == true))
        {
          byte data_length_LSB = data_from_plc[byte_idx++];//7
          byte data_length_MSB = data_from_plc[byte_idx++];//8
          Response_data_length = (data_length_MSB << 8) | data_length_LSB;
        }

        if (Response_data_length > 0)
        {
          //int uu = 0;
          if (data_from_plc.Length >= 11)
          {
            byte EndCode_LSB = data_from_plc[byte_idx++];//9
            byte EndCode_MSB = data_from_plc[byte_idx++];//10
            EndCode = (EndCode_MSB << 8) | EndCode_LSB;
            Response_data_length = Response_data_length - 2;
          }
        }
        IsEnCode_OK = (EndCode == 0);
        if (IsEnCode_OK == false)
        {
          /* page 29 -- Error information
           * The request destination network number, request destination station number, request destination module I/O number, and
            request destination multi-drop station number of the station which responded with errors are stored.
           */
          error_des = FindErrorByCode(EndCode);

          if (current_data.fx_command == FX_COMMAND.WW)
          {
            int mmm = 0;

          }
          else if (current_data.fx_command == FX_COMMAND.BW)
          {
          }
        }
        else /*if (IsEnCode_OK == false)*/
        {
          if (Response_data_length > 0)
          {
            if (current_data.fx_command == FX_COMMAND.BR) /* request is READ_BITS */
            {
              if (current_data.protocol_unit == PROTOCOL_UNIT._x1_BIT)
              {
                int nCount = 0;
                for (int i = 0; i < Response_data_length; i++)
                {
                  int byte_data_from_plc = data_from_plc[byte_idx + i];
                  int value_1 = (byte_data_from_plc >> 4) & 0x0F;
                  int value_2 = (byte_data_from_plc) & 0x0F;

                  for (int j = 0; j < 2; j++)
                  {
                    nCount++;
                    if (nCount <= current_data.max_device)
                    {
                      FX_DATA fx_data = new FX_DATA();
                      //save to current_data
                      fx_data.fx_command = current_data.fx_command;
                      fx_data.fx_device = current_data.fx_device;
                      fx_data.address = current_data.address + ((2 * i) + j);
                      if (j == 0)
                      {
                        fx_data.value = value_1;
                      }
                      else if (j == 1)
                      {
                        fx_data.value = value_2;
                      }
                      fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
                      //add to list 
                      list_data.Add(fx_data);
                    }/*if (nCount <= current_data.max_device)*/
                  }/*for (int j = 0; j < 2; j++)*/
                }/*for (int i = 0; i < Response_data_length; i++)*/

              }
              else /* (current_data.protocol_unit == PROTOCOL_UNIT._x16_BITS) */
              {
                for (int i = 0; i < Response_data_length; i++)
                {
                  int byte_data_from_plc = data_from_plc[byte_idx + i];
                  int value_1 = (byte_data_from_plc >> 4) & 0x0F;
                  int value_2 = (byte_data_from_plc) & 0x0F;

                  for (int j = 1; j >= 0; j--)
                  {
                    int value_from_plc = 0;
                    if (j == 1)
                    {
                      value_from_plc = value_1;
                    }
                    else if (j == 0)
                    {
                      value_from_plc = value_2;
                    }

                    for (int data_bit_idx = 3; data_bit_idx >= 0; data_bit_idx--)
                    {
                      
                      FX_DATA fx_data = new FX_DATA();
                      //save to current_data
                      fx_data.fx_command = current_data.fx_command;
                      fx_data.fx_device = current_data.fx_device;

                      fx_data.address = current_data.address + (8 * i) + (4 * j) + data_bit_idx;
                      if (data_bit_idx == 3)
                      {
                        fx_data.value = bool_to_int((value_from_plc & 0x08) == (0x08));
                      }
                      else if (data_bit_idx == 2)
                      {
                        fx_data.value = bool_to_int((value_from_plc & 0x04) == (0x04));
                      }
                      else if (data_bit_idx == 1)
                      {
                        fx_data.value = bool_to_int((value_from_plc & 0x02) == (0x02));
                      }
                      else /*(data_bit_idx == 0)*/
                      {
                        fx_data.value = bool_to_int((value_from_plc & 0x01) == (0x01));
                      }

                      fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
                      //add to list 
                      list_data.Add(fx_data);
                    }
                  }/*for (int j = 0; j < 2; j++)*/
                }/*for (int i = 0; i < Response_data_length; i++)*/
              }
            }
            else if (current_data.fx_command == FX_COMMAND.WR) /* request is READ_WORD */
            {
              for (int i = 0; i < Response_data_length; i += 2)
              {
                
                int value_LSB = data_from_plc[byte_idx + i];
                int value_MSB = data_from_plc[byte_idx + (i + 1)];
                int value_from_plc = (value_MSB << 8) | (value_LSB);
                //
                int data_word_idx = (i / 2);
                //
                FX_DATA fx_data = new FX_DATA();
                //save to current_data
                fx_data.fx_command = current_data.fx_command;
                fx_data.fx_device = current_data.fx_device;
                fx_data.address = current_data.address + data_word_idx;
                fx_data.value = value_from_plc;
                fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
                //add to list 
                list_data.Add(fx_data);
              }
            }
          }
          else /*if (Response_data_length > 0)*/
          {
            if (current_data.fx_command == FX_COMMAND.BW)
            {
              FX_DATA fx_data = new FX_DATA();
              //save to current_data
              fx_data.fx_command = current_data.fx_command;
              fx_data.fx_device = FX_DEVICE.ACK;
              fx_data.address = current_data.address;

              fx_data.value = 0x7F;
              fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(current_data.fx_device), convertAddressToString(fx_data.address));
              //add to list 
              list_data.Add(fx_data);
            }
            if (OnNotifyStatus != null)
            {
              OnNotifyStatus(this, STATUS.WRITE_DATA_OK);
            }
          }
        }
      }
      catch (Exception error)
      {
        string errr = "";//error.Message();
      }

      /* ever thing done */
      if ((IsSubHeader_OK == true) &&
          (IsNetwork_Number_OK == true) &&
          (IsRequest_destination_station_number_OK == true) &&
          (IsRequest_destination_module_IO_number_OK == true) &&
          (IsRequest_destination_multi_drop_station_number_OK == true) &&
          (IsEnCode_OK == true))
      {
        /* do nothing */
      }
      else /* something error ==> feedback error */
      {
        FX_DATA fx_data = new FX_DATA();
        //save to current_data
        fx_data.fx_command = current_data.fx_command;
        fx_data.fx_device = FX_DEVICE.ERROR_DATA;
        fx_data.address = (-1);
        fx_data.value = (-1);
        fx_data.device_as_string = "";//convert_SLMP_ErrorCode(EndCode);
        //add to list
        list_data.Add(fx_data);
      }
      return list_data;
    }
    private List<FX_DATA> ProcessDataFromPLC_ASCII(byte[] data_from_plc)
    {
      bool IsSubHeader_OK = false;
      bool IsNetwork_Number_OK = false;
      bool IsRequest_destination_station_number_OK = false;
      bool IsRequest_destination_module_IO_number_OK = false;
      bool IsRequest_destination_multi_drop_station_number_OK = false;
      bool IsEnCode_OK = false;
      string EndCode = "";
      int byte_idx = 0;
      int Response_data_length = 0;
      List<FX_DATA> list_data = new List<FX_DATA>();
      try
      {
        /* Get & check hearder */
        if (data_from_plc.Length >= 4)
        {
          IsSubHeader_OK = ((data_from_plc[byte_idx++] == 'D') && //0
                            (data_from_plc[byte_idx++] == '0') && //1
                            (data_from_plc[byte_idx++] == '0') && //2
                            (data_from_plc[byte_idx++] == '0'));  //3

        }
        /* Get & check sub_header */
        if ((data_from_plc.Length >= 6) && (IsSubHeader_OK == true))
        {
          IsNetwork_Number_OK = ((data_from_plc[byte_idx++] == '0') && //4
                                (data_from_plc[byte_idx++] == '0'));//5
        }
        /* Get & check Network_Number */
        if ((data_from_plc.Length >= 8)&& (IsNetwork_Number_OK == true))
        {
          IsRequest_destination_station_number_OK = ((data_from_plc[byte_idx++] == 'F') && //6
                                                  (data_from_plc[byte_idx++] == 'F'));//7
        }
        /* Get & check destination_station_number */
        if ((data_from_plc.Length >= 12) && (IsRequest_destination_station_number_OK == true))
        {
          IsRequest_destination_module_IO_number_OK = ((data_from_plc[byte_idx++] == '0') && //8
                            (data_from_plc[byte_idx++] == '3') && //9
                            (data_from_plc[byte_idx++] == 'F') && //10
                            (data_from_plc[byte_idx++] == 'F'));  //11
        }
        if ((data_from_plc.Length >= 14) && (IsRequest_destination_module_IO_number_OK == true))
        {
          IsRequest_destination_multi_drop_station_number_OK = ((data_from_plc[byte_idx++] == '0') && //12
                            (data_from_plc[byte_idx++] == '0')); //13
        }
        if ((data_from_plc.Length >= 18) && (IsRequest_destination_multi_drop_station_number_OK == true))
        {
          string Response_data_length_as_str = String.Format("{0}{1}{2}{3}", Convert.ToChar(data_from_plc[byte_idx++]),
                                                                              Convert.ToChar(data_from_plc[byte_idx++]),
                                                                              Convert.ToChar(data_from_plc[byte_idx++]),
                                                                              Convert.ToChar(data_from_plc[byte_idx++]));
          try
          {
            Response_data_length = int.Parse(Response_data_length_as_str, System.Globalization.NumberStyles.HexNumber);
          }
          catch
          {
          }
          int nn = 0;
        }

        if (Response_data_length > 0)
        {
          //int uu = 0;
          if (data_from_plc.Length >= 22)
          {
            EndCode = String.Format("{0}{1}{2}{3}", Convert.ToChar(data_from_plc[byte_idx++]),
                                                    Convert.ToChar(data_from_plc[byte_idx++]),
                                                    Convert.ToChar(data_from_plc[byte_idx++]),
                                                    Convert.ToChar(data_from_plc[byte_idx++]));
            Response_data_length = Response_data_length - 4;
          }
        }
        IsEnCode_OK = (EndCode == "0000");
        if (IsEnCode_OK == false)
        {
          /* do nothing */
        }
        else /*if (IsEnCode_OK == false)*/
        {
          if (Response_data_length > 0)
          {
            if (current_data.fx_command == FX_COMMAND.BR)
            {
              if (current_data.protocol_unit == PROTOCOL_UNIT._x1_BIT)
              {
                for (int i = 0; i < Response_data_length; i++)
                {
                  FX_DATA fx_data = new FX_DATA();
                  //save to current_data
                  fx_data.fx_command = current_data.fx_command;
                  fx_data.fx_device = current_data.fx_device;
                  fx_data.address = current_data.address + i;

                  fx_data.value = convertHexToDecimal(data_from_plc[byte_idx + i]);
                  fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
                  //add to list 
                  list_data.Add(fx_data);
                }
              }
              else /* (current_data.protocol_unit == PROTOCOL_UNIT._x16_BITS) */
              {
                for (int i = 0; i < Response_data_length; i++)
                {
                  int data_word_idx = (i / 4);
                  int data_byte_idx = (i % 4);
                  byte byte_data_from_plc = data_from_plc[byte_idx + i];
                  int decimal_data_from_plc = convertHexToDecimal(byte_data_from_plc);

                  for (int data_bit_idx = 0; data_bit_idx < 4; data_bit_idx++)
                  {
                    //
                    FX_DATA fx_data = new FX_DATA();
                    //save to current_data
                    fx_data.fx_command = current_data.fx_command;
                    fx_data.fx_device = current_data.fx_device;
                    fx_data.address = current_data.address + (data_word_idx * 16) + ((3 - data_byte_idx) * 4) + (3 - data_bit_idx);

                    if (data_bit_idx == 0)
                    {
                      fx_data.value = bool_to_int((decimal_data_from_plc & 0x08) == (0x08));
                    }
                    else if (data_bit_idx == 1)
                    {
                      fx_data.value = bool_to_int((decimal_data_from_plc & 0x04) == (0x04));
                    }
                    else if (data_bit_idx == 2)
                    {
                      fx_data.value = bool_to_int((decimal_data_from_plc & 0x02) == (0x02));
                    }
                    else /*(data_bit_idx == 3)*/
                    {
                      fx_data.value = bool_to_int((decimal_data_from_plc & 0x01) == (0x01));
                    }
                    fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
                    //add to list 
                    list_data.Add(fx_data);
                  }
                }
              }
            }
            else if (current_data.fx_command == FX_COMMAND.WR)
            {
              for (int i = 0; i < Response_data_length; i += 4)
              {
                int data_word_idx = (i / 4);
                int data_byte_idx = (i % 4);
                byte byte_data_from_plc = data_from_plc[byte_idx + i];
                string hex_in_string = String.Format("{0}{1}{2}{3}", Convert.ToChar(data_from_plc[byte_idx + i]),
                                                                        Convert.ToChar(data_from_plc[byte_idx + (i + 1)]),
                                                                        Convert.ToChar(data_from_plc[byte_idx + (i + 2)]),
                                                                        Convert.ToChar(data_from_plc[byte_idx + (i + 3)])
                                                                        );
                int decimal_data_from_plc = int.Parse(hex_in_string, System.Globalization.NumberStyles.HexNumber);
                FX_DATA fx_data = new FX_DATA();
                //save to current_data
                fx_data.fx_command = current_data.fx_command;
                fx_data.fx_device = current_data.fx_device;
                fx_data.address = current_data.address + data_word_idx;
                fx_data.value = decimal_data_from_plc;
                fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(fx_data.fx_device), convertAddressToString(fx_data.address));
                //add to list 
                list_data.Add(fx_data);
              }
            }
          }
          else /*if (Response_data_length > 0)*/
          {
            if (current_data.fx_command == FX_COMMAND.BW)
            {
              FX_DATA fx_data = new FX_DATA();
              //save to current_data
              fx_data.fx_command = current_data.fx_command;
              fx_data.fx_device = FX_DEVICE.ACK;
              fx_data.address = current_data.address;

              fx_data.value = 0x7F;
              fx_data.device_as_string = String.Format("{0}{1}", convertDeviceToChar(current_data.fx_device), convertAddressToString(fx_data.address));
              //add to list 
              list_data.Add(fx_data);
            }
            if (OnNotifyStatus != null)
            {
              OnNotifyStatus(this, STATUS.WRITE_DATA_OK);
            }
          }
        }        
      }
      catch (Exception error)
      {
        string errr = "";//error.Message();
      }

      /* ever thing done */
      if ((IsSubHeader_OK == true) &&
          (IsNetwork_Number_OK == true) &&
          (IsRequest_destination_station_number_OK == true) &&
          (IsRequest_destination_module_IO_number_OK == true) &&
          (IsRequest_destination_multi_drop_station_number_OK == true) &&
          (IsEnCode_OK == true))
      {
        /* do nothing */
      }
      else /* something error ==> feedback error */
      {
        FX_DATA fx_data = new FX_DATA();
        //save to current_data
        fx_data.fx_command = current_data.fx_command;
        fx_data.fx_device = FX_DEVICE.ERROR_DATA;
        fx_data.address = (-1);
        fx_data.value = (-1);
        fx_data.device_as_string = convert_SLMP_ErrorCode(EndCode);
        //add to list
        list_data.Add(fx_data);
      }
      return list_data;
    }

    private byte[] get_bytes_from_word_data(int[] values, int data_length)
    {
      /* 
       * See example Page 54
       */
      List<byte> list_bytes = new List<byte>();
      
      /* start to convert */
      for (int i = 0; i < data_length; i++)
      {
        byte msb = Convert.ToByte((values[i] >> 8) & 0x00FF);
        byte lsb = Convert.ToByte(values[i] & 0x00FF);
        //
        list_bytes.AddRange(new byte[]{lsb, msb});
      }
      return list_bytes.ToArray();
    }

    private byte[] get_bytes_from_bits_data(bool[] bool_values, int data_length)
    {
      /*Page 32 : 
       * When the bit device memory is handled in 1-bit (1-point) units, one point is specified by 4-bits 
       * and a specified number of devices starting from the specified start device*/
      List<byte> list_bytes = new List<byte>();
      /* convert bool_values to list */
      List<bool> list_bool_input = new List<bool>();

      //int l
      for (int idx = 0; idx < bool_values.Length; idx++)
      {
        if (idx < data_length)
        {
          list_bool_input.Add(bool_values[idx]);
        }
      }
        //if (bool_values.Length >= data_length)
        //{
        //  for (int idx = 0; idx <= data_length; idx++)
        //  {
        //    list_bool_input.Add(bool_values[idx]);
        //  }
        //}
        //else /*if (bool_values.Length > data_length)*/
        //{
        //  for (int idx = 0; idx <= data_length; idx++)
        //  {
        //    if (idx < bool_values.Length)
        //    {
        //      list_bool_input.Add(bool_values[idx]);
        //    }
        //    else
        //    {
        //      list_bool_input.Add(false);
        //    }
        //  }
        //}

        //for (int i = 0; i < data_length; i++)
        //{
        //  list_bool_input.Add(bool_values[i]);
        //}
        /* for dummy bit, we create dummy byte */
        if ((data_length % 2) != 0)
        {
          /* 0 is shown as a dummy when the number of points is an odd number.*/
          list_bool_input.Add(false);
        }


      /* start to convert */
      for (int i = 0; i < (list_bool_input.Count - 1); i+=2)
      {
        int msb = bool_to_int(list_bool_input[i]);
        int lsb = bool_to_int(list_bool_input[i + 1]);
        //
        int data = (msb << 4) | (lsb);
        //
        list_bytes.Add(Convert.ToByte(data));
      }
      return list_bytes.ToArray();
    }

    private byte get_device_code(FX_DEVICE fx_device)
    {
      byte device_code = 0;
      if (fx_device == FX_DEVICE.X)
      {
        device_code = 0x9C;
      }
      else if (fx_device == FX_DEVICE.Y)
      {
        device_code = 0x9D;
      }
      else if (fx_device == FX_DEVICE.M)
      {
        device_code = 0x90;
      }
      else if (fx_device == FX_DEVICE.L)
      {
        device_code = 0x92;
      }
      else if (fx_device == FX_DEVICE.D)
      {
        device_code = 0xA8;
      }
      else
      {
        /* do nothing */
      }
      return device_code;
    }

    private byte[] get_sub_command(PROTOCOL_UNIT protocol_unit)
    {
      List<byte> list_bytes = new List<byte>();  
      if (protocol_unit == PROTOCOL_UNIT._x1_BIT)
      {
        list_bytes.AddRange(new byte[] { 0x01, 0x00 });
      }
      else if (protocol_unit == PROTOCOL_UNIT._x16_BITS)
      {
        list_bytes.AddRange(new byte[] { 0x00, 0x00 });
      }
      else if (protocol_unit == PROTOCOL_UNIT._x1_WORD)
      {
        list_bytes.AddRange(new byte[] { 0x00, 0x00 });
      }
      else
      {
        /* do nothing */
      }
      return list_bytes.ToArray();
    }
    #region WRITE DATA IN BINARY MODE

    public void WriteDeviceMemory_Binary_codes(string start_address, int[] values, int number_of_words_in_decimal)
    {
      /* convert string to device format 
       * get the first character
       *ex: M100 --> fx_device = FX_DEVICE.M
       */
      start_address = start_address.Trim();
      char device_str = start_address[0];
      char device_str_2 = ' ';
      if (start_address.Length > 1)
      {
        device_str_2 = start_address[1];
      }
      FX_DEVICE fx_device = convertCharToDevice(device_str, device_str_2);

      /* start to building data */
      if ((fx_device == FX_DEVICE.M) ||
          (fx_device == FX_DEVICE.X) ||
          (fx_device == FX_DEVICE.Y) ||
          (fx_device == FX_DEVICE.L) ||
          (fx_device == FX_DEVICE.D)
          )
      {
        PROTOCOL_UNIT protocol_unit = PROTOCOL_UNIT._x1_WORD;
        List<byte> list_data_to_send = new List<byte>();
        /*         * 
         * idx = 0 --> Header 7 bytes 
         */
        byte[] header_bytes = Build_Header(SLMP_HEADER.SEND_TO_PLC);
        list_data_to_send.AddRange(header_bytes);

        /*
         * idx = 7 -- request data length: 2 bytes -- see page 27; 32
         */
        //int number_of_byte_need = number_of_words_in_decimal * 2;
        //int request_data_length = 6 + number_of_byte_need; /* (reserved + command + sub_command) + number_of_byte_need */

        //byte request_data_length_MSB = Convert.ToByte(request_data_length >> 8);
        //byte request_data_length_LSB = Convert.ToByte(request_data_length);
        //list_data_to_send.AddRange(new byte[] { request_data_length_LSB, request_data_length_MSB });
        list_data_to_send.AddRange(new byte[] { 0x00, 0x00 });
        //logging list_data_to_send, this value will be calcuate again 
        int request_data_length = list_data_to_send.Count;
        int idx_request_data_length_LSB = (list_data_to_send.Count - 2);
        int idx_request_data_length_MSB = (list_data_to_send.Count - 1);


        /* 
         * idx = 9 --> reserved 2 bytes - page 28
         */
        list_data_to_send.AddRange(new byte[] { 0x00, 0x00 });

        /* 
         * idx = 11 --> command code: 2 bytes: Device Write (Batch) 1401H - page 52 ;54
         */
        list_data_to_send.AddRange(new byte[] { 0x01, 0x14 });

        /* 
         * idx = 13 --> sub_command: 2 bytes - page 52 
         */
        byte[] sub_command = get_sub_command(PROTOCOL_UNIT._x1_WORD);
        list_data_to_send.AddRange(sub_command);

        /* idx = 15 --> Head device No: 3 bytes (start_address) - page 49, 50*/
        /* Number of device: 2 bytes 
           - When writing data in bit units: 1 to 1792 points ASCII  <---> 1 to 3584 points (BIN) 
           - When writing data in word units: 1 to 480 points ASCII  <---> 1 to 960 points (BIN) 
         */
        byte[] start_address_as_bytes = format_device_address_as_bytes(fx_device, start_address);
        list_data_to_send.AddRange(start_address_as_bytes);

        /* idx = 18 -- Device code 1 bytes - page 46 */
        byte device_code = get_device_code(fx_device);
        list_data_to_send.Add(device_code);
        /*
         * idx = 19 --> number of devices will be written --- 2 bytes
         */
        byte[] length_as_bytes = format_length_as_bytes(number_of_words_in_decimal);
        list_data_to_send.AddRange(length_as_bytes);

        /*
         *idx = 21 --> data to be written -- see page 32
         */
        byte[] data_tobe_written = get_bytes_from_word_data(values, number_of_words_in_decimal);
        list_data_to_send.AddRange(data_tobe_written);
        //
        //adding request_data_length
        request_data_length = list_data_to_send.Count - request_data_length;
        byte request_data_length_MSB = Convert.ToByte(request_data_length >> 8);
        byte request_data_length_LSB = Convert.ToByte(request_data_length);

        list_data_to_send[idx_request_data_length_LSB] = request_data_length_LSB;
        list_data_to_send[idx_request_data_length_MSB] = request_data_length_MSB;
        //


        /* saving to current_data */
        if (protocol_unit == PROTOCOL_UNIT._x1_WORD)
        {
          current_data.fx_command = FX_COMMAND.WR;
        }
        else
        {
          current_data.fx_command = FX_COMMAND.BR;
        }
        current_data.fx_device = fx_device;
        current_data.address = GetAddress(start_address);
        current_data.value = 0xFF;
        current_data.protocol_unit = protocol_unit;
        current_data.max_device = number_of_words_in_decimal;

        /* calling sending data */
        this.SendData(list_data_to_send.ToArray(), list_data_to_send.ToArray().Length); //read device
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.WRONG_FORMAT_DEVICE_OR_DEVICE_NOT_SUPPORT);
        }

      }
    }
    //
    public void WriteDeviceMemory_Binary_codes(string start_address, bool[] bool_values, int number_of_bits_in_decimal)
    {
      /* convert string to device format 
       * get the first character
       *ex: M100 --> fx_device = FX_DEVICE.M
       */
      start_address = start_address.Trim();
      char device_str = start_address[0];
      char device_str_2 = ' ';
      if (start_address.Length > 1)
      {
        device_str_2 = start_address[1];
      }
      FX_DEVICE fx_device = convertCharToDevice(device_str, device_str_2);

      /* start to building data */
      if ((fx_device == FX_DEVICE.M) ||
          (fx_device == FX_DEVICE.X) ||
          (fx_device == FX_DEVICE.Y) ||
          (fx_device == FX_DEVICE.L) ||
          (fx_device == FX_DEVICE.D)
          )
      {
        PROTOCOL_UNIT protocol_unit = PROTOCOL_UNIT._x1_BIT;
        List<byte> list_data_to_send = new List<byte>();
        /*         * 
         * idx = 0 --> Header 7 bytes 
         */
        byte[] header_bytes = Build_Header(SLMP_HEADER.SEND_TO_PLC);
        list_data_to_send.AddRange(header_bytes);

        /*
         * idx = 7 -- request data length: 2 bytes -- see page 32
         */
        //int number_of_byte_need = 0;
        //if (number_of_bits_in_decimal % 2 == 0)
        //{
        //  number_of_byte_need = number_of_bits_in_decimal / 2;
        //}
        //else
        //{
        //  number_of_byte_need = (number_of_bits_in_decimal + 1) / 2;
        //}
        //number_of_byte_need = 4;
        
        //int request_data_length = 6 + number_of_byte_need; /* (reserved + command + sub_command) + number_of_byte_need */
        //if (_test_length > 0)
        //{
        //  request_data_length = _test_length;
        //}
        list_data_to_send.AddRange(new byte[] { 0x00, 0x00 });
        //logging list_data_to_send, this value will be calcuate again 
        int request_data_length = list_data_to_send.Count;
        int idx_request_data_length_LSB = (list_data_to_send.Count - 2);
        int idx_request_data_length_MSB = (list_data_to_send.Count - 1);

        /* 
         * idx = 9 --> reserved 2 bytes 
         */
        list_data_to_send.AddRange(new byte[] { 0x10, 0x00 });

        /****** Start to building MESSAGE FORMAT ***************/
        //01H 14H 01H 00H 64H 00H 00H 90H 08H 00H 11H 00H 11H 00H -- example
        //list_data_to_send.AddRange(new byte[] { 0x01, 0x14, 0x01, 0x00, 0x64, 0x00, 0x00, 0x90, 0x08, 0x00, 0x11, 0x00, 0x11, 0x00 });
      

        /* 
         * idx = 11 --> command code: 2 bytes: Device Write (Batch) 1401H - page 52
         */
        list_data_to_send.AddRange(new byte[] { 0x01, 0x14 });

        /* 
         * idx = 13 --> sub_command: 2 bytes - page 52 
         */
        byte[] sub_command = get_sub_command(protocol_unit);
        list_data_to_send.AddRange(sub_command);
        //if (protocol_unit == PROTOCOL_UNIT._x1_BIT)
        //{
        //  list_data_to_send.AddRange(new byte[] { 0x01, 0x00 });
        //}
        //else if (protocol_unit == PROTOCOL_UNIT._x16_BITS)
        //{
        //  list_data_to_send.AddRange(new byte[] { 0x00, 0x00 });
        //}
        //else if (protocol_unit == PROTOCOL_UNIT._x1_WORD)
        //{
        //  list_data_to_send.AddRange(new byte[] { 0x00, 0x00 });
        //}

        /* idx = 15 --> Head device No: 3 bytes (start_address) - page 49, 50*/
        /* Number of device: 2 bytes 
           - When writing data in bit units: 1 to 1792 points ASCII  <---> 1 to 3584 points (BIN) 
           - When writing data in word units: 1 to 480 points ASCII  <---> 1 to 960 points (BIN) 
         */
        byte[] start_address_as_bytes = format_device_address_as_bytes(fx_device, start_address);
        list_data_to_send.AddRange(start_address_as_bytes);


        /* idx = 18 -- Device code 1 bytes - page 46 */
        byte device_code = get_device_code(fx_device);
        list_data_to_send.Add(device_code);
        /*
         * idx = 19 --> number of bit will be written --- 2 bytes
         */
        byte[] length_as_bytes = format_length_as_bytes(number_of_bits_in_decimal);
        list_data_to_send.AddRange(length_as_bytes);

        /*
         *idx = 21 --> data to be written -- see page 32
         */
        byte[] data_tobe_written = get_bytes_from_bits_data(bool_values, number_of_bits_in_decimal);
        list_data_to_send.AddRange(data_tobe_written);

        //adding request_data_length
        request_data_length = list_data_to_send.Count - request_data_length;
        byte request_data_length_MSB = Convert.ToByte(request_data_length >> 8);
        byte request_data_length_LSB = Convert.ToByte(request_data_length);
        
        list_data_to_send[idx_request_data_length_LSB] = request_data_length_LSB;
        list_data_to_send[idx_request_data_length_MSB] = request_data_length_MSB;


        /* saving to current_data */
        if (protocol_unit == PROTOCOL_UNIT._x1_WORD)
        {
          current_data.fx_command = FX_COMMAND.WW;
        }
        else
        {
          current_data.fx_command = FX_COMMAND.BW;
        }
        current_data.fx_device = fx_device;
        current_data.address = GetAddress(start_address);
        current_data.value = 0xFF;
        current_data.protocol_unit = protocol_unit;
        current_data.max_device = number_of_bits_in_decimal;

        /* calling sending data */
        this.SendData(list_data_to_send.ToArray(), list_data_to_send.ToArray().Length); //read device
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.WRONG_FORMAT_DEVICE_OR_DEVICE_NOT_SUPPORT);
        }

      }
    }

    public void WriteDeviceMemory_Binary_codes(string start_address, bool bool_value)
    {
      bool[] bool_values = new bool[1];
      bool_values[0] = bool_value;
      WriteDeviceMemory_Binary_codes(start_address, bool_values, 1);
    }
    #endregion


    #region WRITE DATA IN ASCII MODE
    public void WriteDeviceMemory_ASCII_codes(string start_address, bool[] bool_values, int length)
    {
      start_address = start_address.Trim();
      char device_str = start_address[0];
      char device_str_2 = ' ';
      if (start_address.Length > 1)
      {
        device_str_2 = start_address[1];
      }
      FX_DEVICE fx_device = convertCharToDevice(device_str, device_str_2);
      if ((fx_device == FX_DEVICE.M) || (fx_device == FX_DEVICE.X) || (fx_device == FX_DEVICE.Y)
          )
      {
        int len_init = 42 + length;

        byte[] data_to_send = new byte[len_init];
        byte[] header_bytes = Build_Header(SLMP_HEADER.SEND_TO_PLC);


        Array.Copy(header_bytes, data_to_send, header_bytes.Length);
        /* request data length: 25 bytes ==> change to Hex 0x19 ==> change to ASCII */
        data_to_send[14] = Convert.ToByte('0');
        data_to_send[15] = Convert.ToByte('0');
        data_to_send[16] = Convert.ToByte('0');
        data_to_send[17] = Convert.ToByte('0');

        int byte_idx = 18;
        /* reserved */
        data_to_send[byte_idx++] = Convert.ToByte('0'); //18
        data_to_send[byte_idx++] = Convert.ToByte('0'); //19
        data_to_send[byte_idx++] = Convert.ToByte('1'); //20
        data_to_send[byte_idx++] = Convert.ToByte('0'); //21

        /* command code: 4 bytes: Device Write (Batch) 1401H */
        data_to_send[byte_idx++] = Convert.ToByte('1'); //22
        data_to_send[byte_idx++] = Convert.ToByte('4'); //23
        data_to_send[byte_idx++] = Convert.ToByte('0'); //24
        data_to_send[byte_idx++] = Convert.ToByte('1'); //25

        /* sub_command: 4 bytes */
        data_to_send[byte_idx++] = Convert.ToByte('0'); //26
        data_to_send[byte_idx++] = Convert.ToByte('0'); //27
        data_to_send[byte_idx++] = Convert.ToByte('0'); //28
        data_to_send[byte_idx++] = Convert.ToByte('1'); //29
        /*  Device code 2 bytes */
        data_to_send[byte_idx++] = Convert.ToByte(device_str); //30
        data_to_send[byte_idx++] = Convert.ToByte('*'); //31

        /* Head device No: 6 bytes (start_address)*/
        int head_byte_idx = byte_idx;//32
        data_to_send[byte_idx++] = Convert.ToByte('0'); //32
        data_to_send[byte_idx++] = Convert.ToByte('0'); //33
        data_to_send[byte_idx++] = Convert.ToByte('0'); //34
        data_to_send[byte_idx++] = Convert.ToByte('0'); //35
        data_to_send[byte_idx++] = Convert.ToByte('0'); //36
        data_to_send[byte_idx++] = Convert.ToByte('0'); //37
        string address = start_address.Substring(1, (start_address.Length - 1));
        address = address.PadLeft(6, '0');
        int idx = 0;
        for (int i = 0; i < address.Length; i++)
        {
          char add_chr = address[i];
          byte byte_value = Convert.ToByte(add_chr);
          data_to_send[head_byte_idx + i] = byte_value;
        }
        /* Number of device: 4 bytes 
           - When reading data in bit units: 1 to 1792 points
           - When reading data in word units: 1 to 480 points
         */
        int number_device_byte_idx = byte_idx;
        data_to_send[byte_idx++] = Convert.ToByte('0'); //38
        data_to_send[byte_idx++] = Convert.ToByte('0'); //39
        data_to_send[byte_idx++] = Convert.ToByte('0'); //40
        data_to_send[byte_idx++] = Convert.ToByte('0'); //41       
        //
        string str_length = convertLengthToString(length);
        idx = 0;
        for (int i = 0; i < str_length.Length; i++)
        {
          data_to_send[number_device_byte_idx + idx] = Convert.ToByte(convertCharToHex(str_length[idx]));
          idx += 1;
        }
        /*
         * Write data
         */
        for (int i = 0; i < length; i++)
        {
          data_to_send[byte_idx++] = (Convert.ToByte(convertDecimalToHex(bool_to_int(bool_values[i]))));
        }
        /**/
        int total_data_length = (byte_idx - 18);
        string total_data_length_as_str = convertLengthToString(total_data_length);
        data_to_send[14] = Convert.ToByte(total_data_length_as_str[0]);
        data_to_send[15] = Convert.ToByte(total_data_length_as_str[1]);
        data_to_send[16] = Convert.ToByte(total_data_length_as_str[2]);
        data_to_send[17] = Convert.ToByte(total_data_length_as_str[3]);
        //
        //save to current_data
        current_data.fx_command = FX_COMMAND.BW;
        current_data.fx_device = fx_device;
        current_data.address = GetAddress(start_address);
        current_data.value = 0xFF;
        current_data.protocol_unit = PROTOCOL_UNIT._x1_BIT;
        /* calling sending data */
        this.SendData(data_to_send, data_to_send.Length); //write device
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.WRONG_FORMAT_DEVICE_OR_DEVICE_NOT_SUPPORT);
        }

      }
    }

    

    public void WriteDeviceMemory_ASCII_codes(string start_address, bool bool_value)
    {
      bool[] bool_values = new bool[1];
      bool_values[0] = bool_value;
      WriteDeviceMemory_ASCII_codes(start_address, bool_values, 1);
    }
        #endregion


        #region READ DATA IN BINARY MODE

        /// <summary>
        /// ReadsDeviceMemory_Binary_codes(
        /// </summary>
        /// <param name="protocol_unit"></param>
        /// <param name="start_address"></param>
        /// <param name="number_of_bits_in_decimal"></param>
        private void ReadsDeviceMemory_Binary_codes(PROTOCOL_UNIT protocol_unit, string start_address, int number_of_bits_in_decimal)
    {
      start_address = start_address.Trim();
        char device_str = start_address[0];
      char device_str_2 = ' ';
      if (start_address.Length > 1)
      {
        device_str_2 = start_address[1];
      }
      FX_DEVICE fx_device = convertCharToDevice(device_str, device_str_2);
      if ((fx_device == FX_DEVICE.M) ||
          (fx_device == FX_DEVICE.X) ||
          (fx_device == FX_DEVICE.Y) ||
          (fx_device == FX_DEVICE.L) ||
          (fx_device == FX_DEVICE.D)
          )
      {
        byte[] data_to_send = new byte[42];

        List<byte> list_data_to_send = new List<byte>();

        byte[] header_bytes = Build_Header(SLMP_HEADER.SEND_TO_PLC);
        list_data_to_send.AddRange(header_bytes);


        //Array.Copy(header_bytes, data_to_send, header_bytes.Length);

        /* request data length: 24 bytes ==> change to Hex 0x18 ==> change to ASCII */
        list_data_to_send.AddRange(new byte[] { 0x0C, 0x00});

        /* reserved 2 bytes */
        list_data_to_send.AddRange(new byte[] { 0x10, 0x00 });

        /* command code: 2 bytes: Device Read (Batch) 0401H - page 48*/
        list_data_to_send.AddRange(new byte[] {  0x01, 0x04});

        /* sub_command: 2 bytes - page 49*/
        if (protocol_unit == PROTOCOL_UNIT._x1_BIT)
        {
          //ASCII: 480 words (7680 points)
          //BIN: 960 words (15360 points)
          list_data_to_send.AddRange(new byte[] { 0x01, 0x00 });
        }
        else if (protocol_unit == PROTOCOL_UNIT._x16_BITS)
        {
          list_data_to_send.AddRange(new byte[] { 0x00, 0x00 });          
        }
        else if (protocol_unit == PROTOCOL_UNIT._x1_WORD)
        {
          list_data_to_send.AddRange(new byte[] { 0x00, 0x00 });
        }

        /* Head device No: 3 bytes (start_address) - page 49, 50*/
        /* read device in bits unit */
        byte[] start_address_as_bytes = format_device_address_as_bytes(fx_device, start_address);
        list_data_to_send.AddRange(start_address_as_bytes);


        /*  Device code 1 bytes - page 46 */
        byte device_code = get_device_code(fx_device);
        list_data_to_send.Add(device_code);
        
        /* 
         * number of device will be read 
         */
        byte[] length_as_bytes = format_length_as_bytes(number_of_bits_in_decimal);
        list_data_to_send.AddRange(length_as_bytes);
        //

        /*saving to current_data*/
        if (protocol_unit == PROTOCOL_UNIT._x1_WORD)
        {
          current_data.fx_command = FX_COMMAND.WR;
        }
        else
        {
          current_data.fx_command = FX_COMMAND.BR;
        }
        current_data.fx_device = fx_device;
        current_data.address = GetAddress(start_address);
        current_data.value = 0xFF;
        current_data.protocol_unit = protocol_unit;
        current_data.max_device = number_of_bits_in_decimal;

        /* calling sending data */
        this.SendData(list_data_to_send.ToArray(), list_data_to_send.ToArray().Length); //read device
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.WRONG_FORMAT_DEVICE_OR_DEVICE_NOT_SUPPORT);
        }

      }
    }
    #endregion
    public void ReadsDeviceMemory_ASCII_codes(PROTOCOL_UNIT protocol_unit, string start_address, int number_of_bits_in_decimal)
    {
      start_address = start_address.Trim();
      char device_str = start_address[0];
      char device_str_2 = ' ';
      if (start_address.Length > 1)
      {
        device_str_2 = start_address[1];
      }
      FX_DEVICE fx_device = convertCharToDevice(device_str, device_str_2);
      if ((fx_device == FX_DEVICE.M) || 
          (fx_device == FX_DEVICE.X) || 
          (fx_device == FX_DEVICE.Y) ||
          (fx_device == FX_DEVICE.L) ||
          (fx_device == FX_DEVICE.D) 
          )
      {
        byte[] data_to_send = new byte[42];
        byte[] header_bytes = Build_Header(SLMP_HEADER.SEND_TO_PLC);

        
        Array.Copy(header_bytes, data_to_send, header_bytes.Length);
        /* request data length: 24 bytes ==> change to Hex 0x18 ==> change to ASCII */
        data_to_send[14] = Convert.ToByte('0');
        data_to_send[15] = Convert.ToByte('0');
        data_to_send[16] = Convert.ToByte('1');
        data_to_send[17] = Convert.ToByte('8');
        /* reserved */
        data_to_send[18] = Convert.ToByte('0');
        data_to_send[19] = Convert.ToByte('0');
        data_to_send[20] = Convert.ToByte('1');
        data_to_send[21] = Convert.ToByte('0');
        /* command code: 4 bytes: Device Read (Batch) 0401H */
        data_to_send[22] = Convert.ToByte('0');
        data_to_send[23] = Convert.ToByte('4');
        data_to_send[24] = Convert.ToByte('0');
        data_to_send[25] = Convert.ToByte('1');

        /* sub_command: 4 bytes */
        if (protocol_unit == PROTOCOL_UNIT._x1_BIT)            
        {
          //ASCII: 480 words (7680 points)
          //BIN: 960 words (15360 points)
          data_to_send[26] = Convert.ToByte('0');
          data_to_send[27] = Convert.ToByte('0');
          data_to_send[28] = Convert.ToByte('0');
          data_to_send[29] = Convert.ToByte('1');
        }
        else if (protocol_unit == PROTOCOL_UNIT._x16_BITS)
        {
          data_to_send[26] = Convert.ToByte('0');
          data_to_send[27] = Convert.ToByte('0');
          data_to_send[28] = Convert.ToByte('0');
          data_to_send[29] = Convert.ToByte('0');
        }
        else if (protocol_unit == PROTOCOL_UNIT._x1_WORD)
        {
          data_to_send[26] = Convert.ToByte('0');
          data_to_send[27] = Convert.ToByte('0');
          data_to_send[28] = Convert.ToByte('0');
          data_to_send[29] = Convert.ToByte('0');
        }
        /*  Device code 2 bytes */
        data_to_send[30] = Convert.ToByte(device_str);
        data_to_send[31] = Convert.ToByte('*');

        /* Head device No: 6 bytes (start_address)*/
        data_to_send[32] = Convert.ToByte('0');
        data_to_send[33] = Convert.ToByte('0');
        data_to_send[34] = Convert.ToByte('0');
        data_to_send[35] = Convert.ToByte('0');
        data_to_send[36] = Convert.ToByte('0');
        data_to_send[37] = Convert.ToByte('0');        
        int idx = 0;
        string device_address = format_device_address(fx_device, start_address);
        for (int i = 0; i < device_address.Length; i++)
        {
          char add_chr = device_address[i];
          byte byte_value = Convert.ToByte(add_chr);
          data_to_send[32 + idx] = byte_value;
          idx += 1;
        }
        /* Number of device: 4 bytes 
           - When reading data in bit units: 1 to 1792 points
           - When reading data in word units: 1 to 480 points
         */
        data_to_send[38] = Convert.ToByte('0');
        data_to_send[39] = Convert.ToByte('0');
        data_to_send[40] = Convert.ToByte('0');
        data_to_send[41] = Convert.ToByte('0');
        //
        string str_length = convertLengthToString(number_of_bits_in_decimal);
        idx = 0;
        for (int i = 0; i < str_length.Length; i++)
        {
          data_to_send[38 + idx] = Convert.ToByte(convertCharToHex(str_length[idx]));
          idx += 1;
        }


        /**/
        //save to current_data
        if (protocol_unit == PROTOCOL_UNIT._x1_WORD)
        {
          current_data.fx_command = FX_COMMAND.WR;
        }
        else
        {
          current_data.fx_command = FX_COMMAND.BR;
        }
        current_data.fx_device = fx_device;
        current_data.address = GetAddress(start_address);
        current_data.value = 0xFF;
        current_data.protocol_unit = protocol_unit;
        /* calling sending data */
        this.SendData(data_to_send, data_to_send.Length); //read device
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.WRONG_FORMAT_DEVICE_OR_DEVICE_NOT_SUPPORT);
        }

      }
    }

    private string format_device_address(FX_DEVICE fx_device, string start_address)
    {
      string str_ret = "";
      if ((fx_device == FX_DEVICE.D) || (fx_device == FX_DEVICE.M))
      {
        string device_address = start_address.Substring(1);
        str_ret = device_address.PadLeft(6, '0');
      }
      return str_ret;
    }

    private byte[] format_device_address_as_bytes(FX_DEVICE fx_device, string start_address)
    {
      byte[] bytes_ret = new byte[3];

      if ((fx_device == FX_DEVICE.D) || (fx_device == FX_DEVICE.M))
      {
        string device_address = start_address.Substring(1);
        int device_address_as_int = int.Parse(device_address);

        int device_address_as_int_MSB = ((device_address_as_int >> 16) & 0xFFFF);
        int device_address_as_int_LSB = device_address_as_int & 0xFFFF;
        //
        bytes_ret[0] = Convert.ToByte(device_address_as_int_LSB & 0xFF);
        bytes_ret[1] = Convert.ToByte((device_address_as_int_LSB >> 8)& 0xFF);
        bytes_ret[2] = Convert.ToByte((device_address_as_int_MSB & 0xFF));
      }
      return bytes_ret;
    }
    
    private byte[] format_length_as_bytes(int length)
    {
      byte[] bytes_ret = new byte[2];
      bytes_ret[0] = Convert.ToByte(length & 0xFF);
      bytes_ret[1] = Convert.ToByte((length >> 8)& 0xFF);
      return bytes_ret;
    }

    public void Write_DeviceMemory(string start_address, int[] decimal_values, int length)
    {
      if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_ASCII_CODES)
      {
        //WriteDeviceMemory_ASCII_codes(start_address, bool_values, length);
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.PROTOCOL_NOT_YET_SUPPORT);
        }
      }
      else if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_BINARY_CODES)
      {
        WriteDeviceMemory_Binary_codes(start_address, decimal_values, length);
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.PROTOCOL_NOT_YET_SUPPORT);
        }
      }
    }

    public void Write_DeviceMemory(string start_address, bool[] bool_values, int length)
    {
      if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_ASCII_CODES)
      {
        WriteDeviceMemory_ASCII_codes(start_address, bool_values, length);
      }
      else if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_BINARY_CODES)
      {
        WriteDeviceMemory_Binary_codes(start_address, bool_values, length);
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.PROTOCOL_NOT_YET_SUPPORT);
        }
      }
    }

    public void Write_DeviceMemory(string start_address, int decimal_value)
    {
      if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_ASCII_CODES)
      {
        //WriteDeviceMemory_ASCII_codes(start_address, decimal_value);
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.PROTOCOL_NOT_YET_SUPPORT);
        }
      }
      else if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_BINARY_CODES)
      {
        int[] decimal_values = new int[1];
        decimal_values[0] = decimal_value;
        WriteDeviceMemory_Binary_codes(start_address, decimal_values, 1);
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.PROTOCOL_NOT_YET_SUPPORT);
        }
      }
    }

    public void Write_DeviceMemory(string start_address, bool bool_value, int test_length)
    {
      _test_length = test_length;
      Write_DeviceMemory(start_address, bool_value);
    }

    public void Write_DeviceMemory(string start_address, bool bool_value)
    {
      if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_ASCII_CODES)
      {
        WriteDeviceMemory_ASCII_codes(start_address, bool_value);
      }
      else if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_BINARY_CODES)
      {
        WriteDeviceMemory_Binary_codes(start_address, bool_value);
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.PROTOCOL_NOT_YET_SUPPORT);
        }
      }
    }
        /// <summary>
        /// Read_DeviceMemory("M10", 10, PROTOCOL_UNIT.SLMP_BINARY_CODES);
        /// </summary>
        /// <param name="start_address"></param>
        /// <param name="number_of_bits_in_decimal"></param>
        /// <param name="protocol_unit"></param>
        public void Read_DeviceMemory(string start_address, int number_of_bits_in_decimal, PROTOCOL_UNIT protocol_unit)
    {
      if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_ASCII_CODES)
      {
        ReadsDeviceMemory_ASCII_codes(protocol_unit, start_address, number_of_bits_in_decimal);
      }
      else if (_ethernet_protocol == ETHERNET_PROTOCOL.SLMP_BINARY_CODES)
      {
        ReadsDeviceMemory_Binary_codes(protocol_unit, start_address, number_of_bits_in_decimal);
      }
      else
      {
        if (OnNotifyStatus != null)
        {
          OnNotifyStatus(this, STATUS.PROTOCOL_NOT_YET_SUPPORT);
        }
      }
    }
    private int GetAddress(string address)
    {
      string str = address.Substring(1, address.Length - 1);
      int value = int.Parse(str);
      return (value);
    }
    private string convertLengthToString(int len)
    {
      //string str = len.ToString();
      string str = String.Format(len.ToString("X"));
      str = str.PadLeft(4, '0');
      return str;
    }
    private int convertHexToDecimal(int hex_value)
    {
      int ret = 0;
      if (hex_value == 0x30) ret = 0;
      else if (hex_value == 0x31) ret = 1;
      else if (hex_value == 0x32) ret = 2;
      else if (hex_value == 0x33) ret = 3;
      else if (hex_value == 0x34) ret = 4;
      else if (hex_value == 0x35) ret = 5;
      else if (hex_value == 0x36) ret = 6;
      else if (hex_value == 0x37) ret = 7;
      else if (hex_value == 0x38) ret = 8;
      else if (hex_value == 0x39) ret = 9;
      else if (hex_value == 0x41) ret = 10;
      else if (hex_value == 0x42) ret = 11;
      else if (hex_value == 0x43) ret = 12;
      else if (hex_value == 0x44) ret = 13;
      else if (hex_value == 0x45) ret = 14;
      else if (hex_value == 0x46) ret = 15;
      return ret;
    }
    private int convertCharToDecimal(char char_value)
    {
      int ret = 0;
      if (char_value == '0') ret = 0;
      else if (char_value == '1') ret = 1;
      else if (char_value == '2') ret = 2;
      else if (char_value == '3') ret = 3;
      else if (char_value == '4') ret = 4;
      else if (char_value == '5') ret = 5;
      else if (char_value == '6') ret = 6;
      else if (char_value == '7') ret = 7;
      else if (char_value == '8') ret = 8;
      else if (char_value == '9') ret = 9;
      else if (char_value.ToString().ToUpper() == "A") ret = 10;
      else if (char_value.ToString().ToUpper() == "B") ret = 11;
      else if (char_value.ToString().ToUpper() == "C") ret = 12;
      else if (char_value.ToString().ToUpper() == "D") ret = 13;
      else if (char_value.ToString().ToUpper() == "E") ret = 14;
      else if (char_value.ToString().ToUpper() == "F") ret = 15;
      return ret;
    }
    private int convertCharToHex(char char_value)
    {
      int ret = 0;
      if (char_value == '0') ret = 0x30;
      else if (char_value == '1') ret = 0x31;
      else if (char_value == '2') ret = 0x32;
      else if (char_value == '3') ret = 0x33;
      else if (char_value == '4') ret = 0x34;
      else if (char_value == '5') ret = 0x35;
      else if (char_value == '6') ret = 0x36;
      else if (char_value == '7') ret = 0x37;
      else if (char_value == '8') ret = 0x38;
      else if (char_value == '9') ret = 0x39;
      else if (char_value.ToString().ToUpper() == "A") ret = 0x41;
      else if (char_value.ToString().ToUpper() == "B") ret = 0x42;
      else if (char_value.ToString().ToUpper() == "C") ret = 0x43;
      else if (char_value.ToString().ToUpper() == "D") ret = 0x44;
      else if (char_value.ToString().ToUpper() == "E") ret = 0x45;
      else if (char_value.ToString().ToUpper() == "F") ret = 0x46;
      return ret;
    }
    //private char convertHexToChar(int hex_value)
    //{
    //  char ret = 0;
    //  if (hex_value == 0x30) ret = 0;
    //  else if (hex_value == 0x31) ret = 1;
    //  else if (hex_value == 0x32) ret = 2;
    //  else if (hex_value == 0x33) ret = 3;
    //  else if (hex_value == 0x34) ret = 4;
    //  else if (hex_value == 0x35) ret = 5;
    //  else if (hex_value == 0x36) ret = 6;
    //  else if (hex_value == 0x37) ret = 7;
    //  else if (hex_value == 0x38) ret = 8;
    //  else if (hex_value == 0x39) ret = 9;
    //  else if (hex_value == 0x41) ret = 10;
    //  else if (hex_value == 0x42) ret = 11;
    //  else if (hex_value == 0x43) ret = 12;
    //  else if (hex_value == 0x44) ret = 13;
    //  else if (hex_value == 0x45) ret = 14;
    //  else if (hex_value == 0x46) ret = 15;
    //  return ret;
    //}
    private int convertDecimalToHex(int decimal_value)
    {
      int ret = 0;
      if (decimal_value == 0) ret = 0x30;
      else if (decimal_value == 1) ret = 0x31;
      else if (decimal_value == 2) ret = 0x32;
      else if (decimal_value == 3) ret = 0x33;
      else if (decimal_value == 4) ret = 0x34;
      else if (decimal_value == 5) ret = 0x35;
      else if (decimal_value == 6) ret = 0x36;
      else if (decimal_value == 7) ret = 0x37;
      else if (decimal_value == 8) ret = 0x38;
      else if (decimal_value == 9) ret = 0x39;
      else if (decimal_value == 10) ret = 0x41;
      else if (decimal_value == 11) ret = 0x42;
      else if (decimal_value == 12) ret = 0x43;
      else if (decimal_value == 13) ret = 0x44;
      else if (decimal_value == 14) ret = 0x45;
      else if (decimal_value == 15) ret = 0x46;
      return ret;
    }
    private string convertAddressToString(int start_address)
    {
      string str = start_address.ToString();
      str = str.PadLeft(4, '0');
      //if (str.Length == 1)
      //{
      //  str = String.Format("000{0}", str);
      //}
      //else if (str.Length == 2)
      //{
      //  str = String.Format("00{0}", str);
      //}
      //else if (str.Length == 3)
      //{
      //  str = String.Format("0{0}", str);
      //}
      //else if (str.Length == 4)
      //{
      //  str = String.Format("{0}", str);
      //}
      return str;
    }
    private char convertDeviceToChar(FX_DEVICE device)
    {
      char data = ' ';
      if (device == FX_DEVICE.X)
      {
        data = 'X';
      }
      else if (device == FX_DEVICE.Y)
      {
        data = 'Y';
      }
      else if (device == FX_DEVICE.M)
      {
        data = 'M';
      }
      else if (device == FX_DEVICE.D)
      {
        data = 'D';
      }
      return data;
    }
    private FX_DEVICE convertCharToDevice(char deivce_as_char, char device_str_2)
    {
      FX_DEVICE device = FX_DEVICE.NONE;
      if (deivce_as_char == 'M')
      {
        device = FX_DEVICE.M;
      }
      else if (deivce_as_char == 'X')
      {
        device = FX_DEVICE.X;
      }
      else if (deivce_as_char == 'Y')
      {
        device = FX_DEVICE.Y;
      }
      else if (deivce_as_char == 'D')
      {
        device = FX_DEVICE.D;
      }
      else if (deivce_as_char == 'L')
      {
        device = FX_DEVICE.L;
      }
      else if (deivce_as_char == 'T')
      {
        if (device_str_2 == 'S')
        {
          device = FX_DEVICE.TS;
        }
        else if (device_str_2 == 'C')
        {
          device = FX_DEVICE.TC;
        }
        else if (device_str_2 == 'C')
        {
          device = FX_DEVICE.TN;
        }
      }
      else
      {
        device = FX_DEVICE.NONE;
      }
      return device;
    }
    

    
    #endregion


    private enum SLMP_HEADER
    {
      SEND_TO_PLC,
      RECEIVE_FROM_PLC
    }
    private const int READ_BUFFER_SIZE = 255;
    private byte[] readBuffer = new byte[READ_BUFFER_SIZE];

    private void timer_get_SocketTCP_data_Tick(object sender, EventArgs e)
    {
      timer_get_SocketTCP_data.Enabled = false;
      if (_socketTcpServer != null)
      {
        PLCwithID[] PLC_with_IDs = _socketTcpServer.PLC_with_IDs;
        for (int i = 0; i < PLC_with_IDs.Length; i++)
        {
          if (PLC_with_IDs[i].data_length > 0)
          {
            if (OnSocketTcpReceived != null)
            {
              OnSocketTcpReceived(this, PLC_with_IDs[i].id, PLC_with_IDs[i].data_received, PLC_with_IDs[i].data_length);
            }
          }
          else /*if (PLC_with_IDs[i].data_length > 0)*/
          {
            if (PLC_with_IDs[i].connect == false)
            {
              if (OnSocketTcpDisconnect != null)
              {
                OnSocketTcpDisconnect(this, PLC_with_IDs[i].id);
              }
            }
          }
        }
        byte[] aaa = _socketTcpServer.ReceivedBuffer;
        int length = _socketTcpServer.ReceivedBufferLength;
        if (length > 0)
        {
          int mmm = 0;
        }
      }
      timer_get_SocketTCP_data.Enabled = true;
    }

    

    
   

    
  }

  public class Error
  {
    public int _error_code;
    public string _error_description;
    public string _action;

    public Error()
    {
    }
    public Error(int error_code, string error_description, string action)
    {
      _error_code = error_code;
      _error_description = error_description;
      _action = action;
    }
  }

  public enum STATUS
  {
    OK = 0x00,
    FAILED ,
    TIME_OUT,
    NO_RESPOND,
    TRY_AGAIN,
    TRY_INIT_AGAIN,

    READ_DATA_OK,
    WRITE_DATA_OK,
    INIT_OK,
    INIT_FAILED,
    READ_DATA_FAILED_ID,
    WRONG_FORMAT_DEVICE_OR_DEVICE_NOT_SUPPORT,
    PROTOCOL_NOT_YET_SUPPORT,
    //
    CLOSE_DONE,
  }


  



  public enum FX_DEVICE
  {
    X = 0,
    Y = 1,
    M,
    D,
    DW,
    L,
    TS,
    TC,
    TN,
    ACK,
    T,
    C,
    /* FAIL DATA */
    ERROR_DATA,
    NONE,

  }


  public enum FX_COMMAND
  {
    BR, /* Device Read (Batch): Bits : Reads Device Memory in 1-Bit Units */
    WR, /* Device Read (Batch): Words : Reads Device Memory in 1-Bit Units */
    BW,
    WW,
    NONE
  }

  public class FX_DATA
  {
    public FX_COMMAND fx_command = FX_COMMAND.NONE;
    public FX_DEVICE fx_device = FX_DEVICE.NONE;
    public int address = 0;
    public int value = 0;
    public string device_as_string = "";
    public int max_device = 0;
    //
    public PROTOCOL_UNIT protocol_unit = PROTOCOL_UNIT.NONE;
    public CHECKSUM_STATUS checksumStatus = CHECKSUM_STATUS.NONE;
    public Error error = null;

    public FX_DATA()
    {
    }

    public FX_DATA(FX_DEVICE fx_device, int address)
    {
      this.fx_device = fx_device;
      this.address = address;
      this.device_as_string = String.Format("{0}{1}", this.fx_device.ToString(), this.address.ToString());
    }
  }

  public enum PROTOCOL_UNIT
  {
    NONE,
    _x1_BIT,
    _x16_BITS,
    _x1_WORD
  }
  public enum CHECKSUM_STATUS
  {
    NONE,
    OK,
    FAIL,
  }
  public enum ETHERNET_PROTOCOL
  {
    SLMP_ASCII_CODES,
    SLMP_BINARY_CODES,
    SOCKET_TCP_ACTIVE,
    SOCKET_TCP_PASSIVE
  }

  public enum MASTER_SLAVE
  {
    MASTER = 0,
    SLAVE = 1
  }
}
