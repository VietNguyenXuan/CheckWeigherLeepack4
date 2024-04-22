using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TcpComm
{
  /// <summary>
  /// Modbus TCP common driver class. This class implements a modbus TCP master driver.
  /// It supports the following commands:
  /// 
  /// Read coils
  /// Read discrete inputs
  /// Write single coil
  /// Write multiple cooils
  /// Read holding register
  /// Read input register
  /// Write single register
  /// Write multiple register
  /// 
  /// All commands can be sent in synchronous or asynchronous mode. If a value is accessed
  /// in synchronous mode the program will stop and wait for slave to response. If the 
  /// slave didn't answer within a specified time a timeout exception is called.
  /// The class uses multi threading for both synchronous and asynchronous access. For
  /// the communication two lines are created. This is necessary because the synchronous
  /// thread has to wait for a previous command to finish.
  /// 
  /// </summary>
  public class Master
  {
    // ------------------------------------------------------------------------
    // Constants for access
    private const byte fctReadCoil = 1;
    private const byte fctReadDiscreteInputs = 2;
    private const byte fctReadHoldingRegister = 3;
    private const byte fctReadInputRegister = 4;
    private const byte fctWriteSingleCoil = 5;
    private const byte fctWriteSingleRegister = 6;
    private const byte fctWriteMultipleCoils = 15;
    private const byte fctWriteMultipleRegister = 16;
    private const byte fctReadWriteMultipleRegister = 23;

    /// <summary>Constant for exception illegal function.</summary>
    public const byte excIllegalFunction = 1;
    /// <summary>Constant for exception illegal data address.</summary>
    public const byte excIllegalDataAdr = 2;
    /// <summary>Constant for exception illegal data value.</summary>
    public const byte excIllegalDataVal = 3;
    /// <summary>Constant for exception slave device failure.</summary>
    public const byte excSlaveDeviceFailure = 4;
    /// <summary>Constant for exception acknowledge.</summary>
    public const byte excAck = 5;
    /// <summary>Constant for exception slave is busy/booting up.</summary>
    public const byte excSlaveIsBusy = 6;
    /// <summary>Constant for exception gate path unavailable.</summary>
    public const byte excGatePathUnavailable = 10;
    /// <summary>Constant for exception not connected.</summary>
    public const byte excExceptionNotConnected = 253;
    /// <summary>Constant for exception connection lost.</summary>
    public const byte excExceptionConnectionLost = 254;
    /// <summary>Constant for exception response timeout.</summary>
    public const byte excExceptionTimeout = 255;
    /// <summary>Constant for exception wrong offset.</summary>
    private const byte excExceptionOffset = 128;
    /// <summary>Constant for exception send failt.</summary>
    private const byte excSendFailt = 100;

    // ------------------------------------------------------------------------
    // Private declarations
    private static ushort _timeout = 500;
    private static ushort _refresh = 10;
    private static bool _connected = false;

    private Socket tcpAsyCl;
    private byte[] tcpAsyClBuffer = new byte[2048];

    private Socket tcpSynCl;
    private byte[] tcpSynClBuffer = new byte[2048];

    private int _datalen = 500;

    // ------------------------------------------------------------------------
    /// <summary>Response data event. This event is called when new data arrives</summary>
    public delegate void ResponseData(ushort id, byte unit, byte function, byte[] data, bool IsCorrectCRC);
    /// <summary>Response data event. This event is called when new data arrives</summary>
    public event ResponseData OnResponseData;
    /// <summary>Exception data event. This event is called when the data is incorrect</summary>
    public delegate void ExceptionData(ushort id, byte unit, byte function, byte exception);
    /// <summary>Exception data event. This event is called when the data is incorrect</summary>
    public event ExceptionData OnException;

    // ------------------------------------------------------------------------
    /// <summary>Response timeout. If the slave didn't answers within in this time an exception is called.</summary>
    /// <value>The default value is 500ms.</value>
    public ushort timeout
    {
      get { return _timeout; }
      set { _timeout = value; }
    }

    // ------------------------------------------------------------------------
    /// <summary>Refresh timer for slave answer. The class is polling for answer every X ms.</summary>
    /// <value>The default value is 10ms.</value>
    public ushort refresh
    {
      get { return _refresh; }
      set { _refresh = value; }
    }

    public int DataLen
    {
      get { return _datalen; }
      set { _datalen = value; }
    }
    // ------------------------------------------------------------------------
    /// <summary>Shows if a connection is active.</summary>
    public bool connected
    {
      get { return _connected; }
    }

    // ------------------------------------------------------------------------
    /// <summary>Create master instance without parameters.</summary>
    public Master()
    {
    }

    // ------------------------------------------------------------------------
    /// <summary>Create master instance with parameters.</summary>
    /// <param name="ip">IP adress of modbus slave.</param>
    /// <param name="port">Port number of modbus slave. Usually port 502 is used.</param>
    public Master(string ip, ushort port)
    {
      connect_new_1(ip, port);
      //connect(ip, port);
      //CheckConnectivityForProxyHost_Test(ip, port);
    }

    /******** Link:
     */
    private bool CheckConnectivityForProxyHost_Test(string hostName, int port)
    {
      if (string.IsNullOrEmpty(hostName))
        return false;

      try
      {
        IPAddress _ip;
        if (IPAddress.TryParse(hostName, out _ip) == false)
        {
          IPHostEntry hst = Dns.GetHostEntry(hostName);
          hostName = hst.AddressList[0].ToString();
        }
        // ----------------------------------------------------------------
        // Connect asynchronous client
        tcpAsyCl = new Socket(IPAddress.Parse(hostName).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //tcpAsyCl.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(hostName), port);

        CallWithTimeout(ConnectToProxyServers, 5000, tcpAsyCl, ipEndPoint);

        if (tcpAsyCl != null && tcpAsyCl.Connected)
        {
          _connected = true;
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
        }

        
        // ----------------------------------------------------------------
        // Connect synchronous client
        //tcpSynCl = new Socket(IPAddress.Parse(ip).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //tcpSynCl.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        //tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
        //tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
        //tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
        //_connected = true;
      }
      catch (System.IO.IOException error)
      {
        _connected = false;
        throw (error);
      }
      finally
      {
        try
        {
          if (tcpAsyCl != null)
          {
            tcpAsyCl.Shutdown(SocketShutdown.Both);
          }
        }
        catch (Exception ex)
        {
        }
        finally
        {
          if (tcpAsyCl != null)
            tcpAsyCl.Close();
        }
      }

      //bool isUp = false;
      //Socket testSocket = null;

      //try
      //{
      //  testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      //  IPAddress ip = null;
      //  if (testSocket != null && IPAddress.TryParse(hostName, out ip)) // Pass a Correct IP
      //  {
      //    IPEndPoint ipEndPoint = new IPEndPoint(ip, port);

      //    isUp = false;
      //    //time out 2 Sec
      //    CallWithTimeout(ConnectToProxyServers, 2000, testSocket, ipEndPoint);

      //    if (testSocket != null && testSocket.Connected)
      //    {
      //      isUp = true;
      //      //testSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
      //      //testSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
      //      //testSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);


      //    }
      //  }
      //}
      //catch (Exception ex)
      //{
      //  isUp = false;
      //}
      //finally
      //{
      //  try
      //  {
      //    if (testSocket != null)
      //    {
      //      testSocket.Shutdown(SocketShutdown.Both);
      //    }
      //  }
      //  catch (Exception ex)
      //  {
      //  }
      //  finally
      //  {
      //    if (testSocket != null)
      //      testSocket.Close();
      //  }
      //}
      return _connected;
    }

    private bool CheckConnectivityForProxyHost(string hostName, int port)
    {
       if (string.IsNullOrEmpty(hostName))
           return false;

       bool isUp = false;
       Socket testSocket = null;

       try
       {
         //testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
         //IPAddress.Parse(hostName).AddressFamily
         testSocket = new Socket(IPAddress.Parse(hostName).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
         IPAddress ip = null;
         if (testSocket != null && IPAddress.TryParse(hostName, out ip)) // Pass a Correct IP
         {
           IPEndPoint ipEndPoint = new IPEndPoint(ip, port);

          isUp = false;
          //time out 2 Sec
          CallWithTimeout(ConnectToProxyServers, 500, testSocket, ipEndPoint);

           if (testSocket != null && testSocket.Connected)
           {
              isUp = true;
              //testSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
              //testSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
              //testSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
              

           }
         }

         // ----------------------------------------------------------------
         // Connect asynchronous client
         //tcpAsyCl = new Socket(IPAddress.Parse(ip).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
         //tcpAsyCl.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
         //tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
         //tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
         //tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
       }
       catch (Exception ex)
       {
         isUp = false;
       }
       finally
       {
         try
         {
           if (testSocket != null)
           {
               testSocket.Shutdown(SocketShutdown.Both);
           }
         }
         catch (Exception ex)
         {
         }
         finally
         {
           if (testSocket != null)
               testSocket.Close();
         }
       }
       return isUp;
    }

    private void CallWithTimeout(Action<Socket, IPEndPoint> action,
        int timeoutMilliseconds, Socket socket, IPEndPoint ipendPoint)
    {
      try
      {
        Action wrappedAction = () =>
        {
          action(socket, ipendPoint);
        };

        IAsyncResult result = wrappedAction.BeginInvoke(null, null);

        if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
        {
          wrappedAction.EndInvoke(result);
        }

      }
      catch (Exception ex)
      {
      }
    }
    private void ConnectToProxyServers(Socket testSocket, IPEndPoint ipEndPoint)
    {
      try
      {
        if (testSocket == null || ipEndPoint == null)
          return;

        testSocket.Connect(ipEndPoint);
        //testSocket.Connect(ipEndPoint, 2000);
        //tcpAsyCl.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
      }
      catch (Exception ex)
      {
      }
    }

    public bool CheckConnectivity(string ip, ushort port)
    {
      bool IsConnect = false;
      try
      {
        IsConnect = CheckConnectivityForProxyHost(ip, port);
      }
      catch (System.IO.IOException error)
      {
        IsConnect = false;
      }
      return IsConnect;
    }
    /******************************************************************************/
    // ------------------------------------------------------------------------
    /// <summary>Start connection to slave.</summary>
    /// <param name="ip">IP adress of modbus slave.</param>
    /// <param name="port">Port number of modbus slave. Usually port 502 is used.</param>
    public void connect(string ip, ushort port)
    {
      try
      {
        IPAddress _ip;
        if (IPAddress.TryParse(ip, out _ip) == false)
        {
          IPHostEntry hst = Dns.GetHostEntry(ip);
          ip = hst.AddressList[0].ToString();
        }
        //bool IsConnect = CheckConnectivity(ip, port);
        bool IsConnect = true;
        if (IsConnect == true)
        {
          // ----------------------------------------------------------------
          // Connect asynchronous client
          tcpAsyCl = new Socket(IPAddress.Parse(ip).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
          tcpAsyCl.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
          // ----------------------------------------------------------------
          // Connect synchronous client
          //tcpSynCl = new Socket(IPAddress.Parse(ip).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
          //tcpSynCl.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
          //tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
          //tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
          //tcpSynCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
          _connected = true;
        }
        else
        {
          /* do nothing */
        }
      }
      catch (System.IO.IOException error)
      {
        _connected = false;
        throw (error);
      }
    }
    //public bool Reconnnect(string ip, ushort port)
    //{

    //}
    public bool connect_new_1(string ip, ushort port)
    {
      try
      {
        IPAddress _ip;
        if (IPAddress.TryParse(ip, out _ip) == false)
        {
          IPHostEntry hst = Dns.GetHostEntry(ip);
          ip = hst.AddressList[0].ToString();
        }
        // ----------------------------------------------------------------
        // Connect asynchronous client
        tcpAsyCl = new Socket(IPAddress.Parse(ip).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //tcpAsyCl.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        
        // Connect using a timeout (5 seconds)

        IAsyncResult result = tcpAsyCl.BeginConnect(ip, port, null, null);

        bool success = result.AsyncWaitHandle.WaitOne(500, true);

        if (!success)
        {
          // NOTE, MUST CLOSE THE SOCKET
          _connected = false;
          tcpAsyCl.Close();
          //throw new ApplicationException("Failed to connect server.");
        }
        else
        {
          _connected = true;
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
          tcpAsyCl.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
        }
      }
      catch (System.IO.IOException error)
      {
        _connected = false;
        throw (error);
      }
      return _connected;
    }
    // ------------------------------------------------------------------------
    /// <summary>Stop connection to slave.</summary>
    public void disconnect()
    {
      Dispose();
    }

    // ------------------------------------------------------------------------
    /// <summary>Destroy master instance.</summary>
    ~Master()
    {
      Dispose();
    }

    // ------------------------------------------------------------------------
    /// <summary>Destroy master instance</summary>
    public void Dispose()
    {
      if (tcpAsyCl != null)
      {
        if (tcpAsyCl.Connected)
        {
          try { tcpAsyCl.Shutdown(SocketShutdown.Both); }
          catch { }
          tcpAsyCl.Close();
        }
        tcpAsyCl = null;
      }
      if (tcpSynCl != null)
      {
        if (tcpSynCl.Connected)
        {
          try { tcpSynCl.Shutdown(SocketShutdown.Both); }
          catch { }
          tcpSynCl.Close();
        }
        tcpSynCl = null;
      }
    }

    internal void CallException(ushort id, byte unit, byte function, byte exception)
    {
      //if ((tcpAsyCl == null) || (tcpSynCl == null)) return;
      //if (tcpAsyCl == null) return;
      //if (exception == excExceptionConnectionLost)
      //{
      //  tcpSynCl = null;
      //  tcpAsyCl = null;
      //}
      if (OnException != null)
      {
        OnException(id, unit, function, exception);
      }
    }

    internal static UInt16 SwapUInt16(UInt16 inValue)
    {
      return (UInt16)(((inValue & 0xff00) >> 8) |
               ((inValue & 0x00ff) << 8));
    }

    // ------------------------------------------------------------------------
    /// <summary>Read coils from slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    public void ReadCoils(ushort id, byte unit, ushort startAddress, ushort numInputs)
    {
      WriteAsyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadCoil), id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read coils from slave synchronous.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    /// <param name="values">Contains the result of function.</param>
    public void ReadCoils(ushort id, byte unit, ushort startAddress, ushort numInputs, ref byte[] values)
    {
      values = WriteSyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadCoil), id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read discrete inputs from slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    public void ReadDiscreteInputs(ushort id, byte unit, ushort startAddress, ushort numInputs)
    {
      WriteAsyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadDiscreteInputs), id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read discrete inputs from slave synchronous.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    /// <param name="values">Contains the result of function.</param>
    public void ReadDiscreteInputs(ushort id, byte unit, ushort startAddress, ushort numInputs, ref byte[] values)
    {
      values = WriteSyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadDiscreteInputs), id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read holding registers from slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    public void ReadHoldingRegister(ushort id, byte unit, ushort startAddress, ushort numInputs)
    {
      WriteAsyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadHoldingRegister), id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read holding registers from slave synchronous.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    /// <param name="values">Contains the result of function.</param>
    public void ReadHoldingRegister(ushort id, byte unit, ushort startAddress, ushort numInputs, ref byte[] values)
    {
      values = WriteSyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadHoldingRegister), id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read input registers from slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    public void ReadInputRegister(ushort id, byte unit, ushort startAddress, ushort numInputs)
    {
      WriteAsyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadInputRegister), id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read input registers from slave synchronous.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    /// <param name="values">Contains the result of function.</param>
    public void ReadInputRegister(ushort id, byte unit, ushort startAddress, ushort numInputs, ref byte[] values)
    {
      values = WriteSyncData(CreateReadHeader(id, unit, startAddress, numInputs, fctReadInputRegister), id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Write single coil in slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="OnOff">Specifys if the coil should be switched on or off.</param>
    public void WriteSingleCoils(ushort id, byte unit, ushort startAddress, bool OnOff)
    {
      byte[] data;
      data = CreateWriteHeader(id, unit, startAddress, 1, 1, fctWriteSingleCoil);
      if (OnOff == true) data[10] = 255;
      else data[10] = 0;
      WriteAsyncData(data, id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Write single coil in slave synchronous.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="OnOff">Specifys if the coil should be switched on or off.</param>
    /// <param name="result">Contains the result of the synchronous write.</param>
    public void WriteSingleCoils(ushort id, byte unit, ushort startAddress, bool OnOff, ref byte[] result)
    {
      byte[] data;
      data = CreateWriteHeader(id, unit, startAddress, 1, 1, fctWriteSingleCoil);
      if (OnOff == true) data[10] = 255;
      else data[10] = 0;
      result = WriteSyncData(data, id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Write multiple coils in slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numBits">Specifys number of bits.</param>
    /// <param name="values">Contains the bit information in byte format.</param>
    public void WriteMultipleCoils(ushort id, byte unit, ushort startAddress, ushort numBits, byte[] values)
    {
      byte numBytes = Convert.ToByte(values.Length);
      byte[] data;
      data = CreateWriteHeader(id, unit, startAddress, numBits, (byte)(numBytes + 2), fctWriteMultipleCoils);
      Array.Copy(values, 0, data, 13, numBytes);
      WriteAsyncData(data, id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Write multiple coils in slave synchronous.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address from where the data read begins.</param>
    /// <param name="numBits">Specifys number of bits.</param>
    /// <param name="values">Contains the bit information in byte format.</param>
    /// <param name="result">Contains the result of the synchronous write.</param>
    public void WriteMultipleCoils(ushort id, byte unit, ushort startAddress, ushort numBits, byte[] values, ref byte[] result)
    {
      byte numBytes = Convert.ToByte(values.Length);
      byte[] data;
      data = CreateWriteHeader(id, unit, startAddress, numBits, (byte)(numBytes + 2), fctWriteMultipleCoils);
      Array.Copy(values, 0, data, 13, numBytes);
      result = WriteSyncData(data, id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Write single register in slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address to where the data is written.</param>
    /// <param name="values">Contains the register information.</param>
    public void WriteSingleRegister(ushort id, byte unit, ushort startAddress, byte[] values)
    {
      byte[] data;
      data = CreateWriteHeader(id, unit, startAddress, 1, 1, fctWriteSingleRegister);
      data[10] = values[0];
      data[11] = values[1];
      WriteAsyncData(data, id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Write single register in slave synchronous.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address to where the data is written.</param>
    /// <param name="values">Contains the register information.</param>
    /// <param name="result">Contains the result of the synchronous write.</param>
    public void WriteSingleRegister(ushort id, byte unit, ushort startAddress, byte[] values, ref byte[] result)
    {
      byte[] data;
      data = CreateWriteHeader(id, unit, startAddress, 1, 1, fctWriteSingleRegister);
      data[10] = values[0];
      data[11] = values[1];
      result = WriteSyncData(data, id);
    }
    public void Write_data(ushort id, byte[] values)
    {
      WriteAsyncData(values, id);
    }
    //public void Write_data_FX5U(ushort id, byte unit, ushort startAddress, byte[] values)
    //{
    //  byte[] data_to_send = new byte[42];
    //  /* sub_hearder */
    //  data_to_send[0] = Convert.ToByte('5');
    //  data_to_send[1] = Convert.ToByte('0');
    //  data_to_send[2] = Convert.ToByte('0');
    //  data_to_send[3] = Convert.ToByte('0');
    //  /* network number */
    //  data_to_send[4] = Convert.ToByte('0'); //OK
    //  data_to_send[5] = Convert.ToByte('0'); //OK
    //  /* request destination station number */
    //  data_to_send[6] = Convert.ToByte('F'); //OK
    //  data_to_send[7] = Convert.ToByte('F'); //OK
    //  /* request destination module I/O number */
    //  data_to_send[8] = Convert.ToByte('0'); //OK
    //  data_to_send[9] = Convert.ToByte('3'); //OK
    //  data_to_send[10] = Convert.ToByte('F'); //OK
    //  data_to_send[11] = Convert.ToByte('F'); //OK
    //  /* request destination multidrop station number */
    //  data_to_send[12] = Convert.ToByte('0'); //OK
    //  data_to_send[13] = Convert.ToByte('0'); //OK

    //  /* request data length: 24 bytes ==> change to Hex ==> change to ASCII */
    //  data_to_send[14] = Convert.ToByte('0');
    //  data_to_send[15] = Convert.ToByte('0');
    //  data_to_send[16] = Convert.ToByte('1');
    //  data_to_send[17] = Convert.ToByte('8');
    //  /* reserved */
    //  data_to_send[18] = Convert.ToByte('0');
    //  data_to_send[19] = Convert.ToByte('0');
    //  data_to_send[20] = Convert.ToByte('1');
    //  data_to_send[21] = Convert.ToByte('0');
    //  /* command */
    //  data_to_send[22] = Convert.ToByte('0');
    //  data_to_send[23] = Convert.ToByte('4');
    //  data_to_send[24] = Convert.ToByte('0');
    //  data_to_send[25] = Convert.ToByte('1');

    //  /* sub_command */
    //  data_to_send[26] = Convert.ToByte('0');
    //  data_to_send[27] = Convert.ToByte('0');
    //  data_to_send[28] = Convert.ToByte('0');
    //  data_to_send[29] = Convert.ToByte('1');

    //  /*  Device code */
    //  data_to_send[30] = Convert.ToByte('M');
    //  data_to_send[31] = Convert.ToByte('*');
    //  /* Head device No */
    //  data_to_send[32] = Convert.ToByte('0');
    //  data_to_send[33] = Convert.ToByte('0');
    //  data_to_send[34] = Convert.ToByte('0');
    //  data_to_send[35] = Convert.ToByte('0');
    //  data_to_send[36] = Convert.ToByte('0');
    //  data_to_send[37] = Convert.ToByte('0');
    //  //
    //  data_to_send[38] = Convert.ToByte('0');
    //  data_to_send[39] = Convert.ToByte('0');
    //  data_to_send[40] = Convert.ToByte('0');
    //  data_to_send[41] = Convert.ToByte('8');
    //  //
    //  //data_to_send[42] = Convert.ToByte('i');
    //  //data_to_send[43] = Convert.ToByte('j');
    //  //data_to_send[44] = Convert.ToByte('k');
    //  //data_to_send[45] = Convert.ToByte('l');
    //  //
    //  WriteAsyncData(data_to_send, id);
    //}

    //public void Write_data_FX5U(ushort id, byte unit, ushort startAddress, byte[] values)
    //{
    //  byte[] data_to_send = new byte[46];
    //  /* sub_hearder */
    //  data_to_send[0] = Convert.ToByte('5');
    //  data_to_send[1] = Convert.ToByte('0');
    //  data_to_send[2] = Convert.ToByte('0');
    //  data_to_send[3] = Convert.ToByte('0');
    //  /* network number */
    //  data_to_send[4] = Convert.ToByte('0'); //OK
    //  data_to_send[5] = Convert.ToByte('0'); //OK
    //  /* request destination station number */
    //  data_to_send[6] = Convert.ToByte('F'); //OK
    //  data_to_send[7] = Convert.ToByte('F'); //OK
    //  /* request destination module I/O number */
    //  data_to_send[8] = Convert.ToByte('0'); //OK
    //  data_to_send[9] = Convert.ToByte('3'); //OK
    //  data_to_send[10] = Convert.ToByte('F'); //OK
    //  data_to_send[11] = Convert.ToByte('F'); //OK
    //  /* request destination multidrop station number */
    //  data_to_send[12] = Convert.ToByte('0'); //OK
    //  data_to_send[13] = Convert.ToByte('0'); //OK

    //  /* request data length: 28 bytes ==> change to Hex ==> change to ASCII */
    //  data_to_send[14] = Convert.ToByte('0');
    //  data_to_send[15] = Convert.ToByte('0');
    //  data_to_send[16] = Convert.ToByte('1');
    //  data_to_send[17] = Convert.ToByte('C');
    //  /* reserved */
    //  data_to_send[18] = Convert.ToByte('0');
    //  data_to_send[19] = Convert.ToByte('0');
    //  data_to_send[20] = Convert.ToByte('1');
    //  data_to_send[21] = Convert.ToByte('0');
    //  /* command */
    //  //data_to_send[22] = Convert.ToByte('0');
    //  //data_to_send[23] = Convert.ToByte('4');
    //  //data_to_send[24] = Convert.ToByte('0');
    //  //data_to_send[25] = Convert.ToByte('1');
    //  data_to_send[22] = Convert.ToByte('0');
    //  data_to_send[23] = Convert.ToByte('6');
    //  data_to_send[24] = Convert.ToByte('1');
    //  data_to_send[25] = Convert.ToByte('9');

    //  /* sub_command */
    //  //data_to_send[26] = Convert.ToByte('0');
    //  //data_to_send[27] = Convert.ToByte('0');
    //  //data_to_send[28] = Convert.ToByte('0');
    //  //data_to_send[29] = Convert.ToByte('1');
    //  data_to_send[26] = Convert.ToByte('0');
    //  data_to_send[27] = Convert.ToByte('0');
    //  data_to_send[28] = Convert.ToByte('0');
    //  data_to_send[29] = Convert.ToByte('0');

    //  /* Number of looback data*/
    //  data_to_send[30] = Convert.ToByte('0');
    //  data_to_send[31] = Convert.ToByte('0');
    //  data_to_send[32] = Convert.ToByte('1');
    //  data_to_send[33] = Convert.ToByte('2');
    //  /**/
    //  data_to_send[34] = Convert.ToByte('a');
    //  data_to_send[35] = Convert.ToByte('b');
    //  data_to_send[36] = Convert.ToByte('c');
    //  data_to_send[37] = Convert.ToByte('d');
    //  data_to_send[38] = Convert.ToByte('e');
    //  data_to_send[39] = Convert.ToByte('f');
    //  data_to_send[40] = Convert.ToByte('g');
    //  data_to_send[41] = Convert.ToByte('h');
    //  data_to_send[42] = Convert.ToByte('i');
    //  data_to_send[43] = Convert.ToByte('j');
    //  data_to_send[44] = Convert.ToByte('k');
    //  data_to_send[45] = Convert.ToByte('l');
    //  //
    //  WriteAsyncData(data_to_send, id);
    //}
    // ------------------------------------------------------------------------
    /// <summary>Write multiple registers in slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address to where the data is written.</param>
    /// <param name="values">Contains the register information.</param>
    public void WriteMultipleRegister(ushort id, byte unit, ushort startAddress, byte[] values)
    {
      ushort numBytes = Convert.ToUInt16(values.Length);
      if (numBytes % 2 > 0) numBytes++;
      byte[] data;

      data = CreateWriteHeader(id, unit, startAddress, Convert.ToUInt16(numBytes / 2), Convert.ToUInt16(numBytes + 2), fctWriteMultipleRegister);
      //data[2] = 0;

      Array.Copy(values, 0, data, 13, values.Length);
      /* calculate checksum */
      byte checksum = 0;
      try
      {
        for (int i = 4; i < 77; i++)
        {
          data[2] ^= data[i];
          data[3] ^= data[i];
        }
      }
      catch
      {
      }
      //data[2] = (byte)(checksum >> 8);
      //data[3] = (byte)(checksum);
      WriteAsyncData(data, id);
      int mmm = 0;
      //ushort numBytes = Convert.ToUInt16(values.Length);
      //if (numBytes % 2 > 0) numBytes++;
      //byte[] data;

      //data = CreateWriteHeader(id, unit, startAddress, Convert.ToUInt16(numBytes / 2), Convert.ToUInt16(numBytes + 2), fctWriteMultipleRegister);
      //values[0] = data[0];
      //values[1] = data[1];
      //WriteAsyncData(values, id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Write multiple registers in slave synchronous.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startAddress">Address to where the data is written.</param>
    /// <param name="values">Contains the register information.</param>
    /// <param name="result">Contains the result of the synchronous write.</param>
    public void WriteMultipleRegister(ushort id, byte unit, ushort startAddress, byte[] values, ref byte[] result)
    {
      ushort numBytes = Convert.ToUInt16(values.Length);
      if (numBytes % 2 > 0) numBytes++;
      byte[] data;

      data = CreateWriteHeader(id, unit, startAddress, Convert.ToUInt16(numBytes / 2), Convert.ToUInt16(numBytes + 2), fctWriteMultipleRegister);
      Array.Copy(values, 0, data, 13, values.Length);
      result = WriteSyncData(data, id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read/Write multiple registers in slave asynchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startReadAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    /// <param name="startWriteAddress">Address to where the data is written.</param>
    /// <param name="values">Contains the register information.</param>
    public void ReadWriteMultipleRegister(ushort id, byte unit, ushort startReadAddress, ushort numInputs, ushort startWriteAddress, byte[] values)
    {
      ushort numBytes = Convert.ToUInt16(values.Length);
      if (numBytes % 2 > 0) numBytes++;
      byte[] data;

      data = CreateReadWriteHeader(id, unit, startReadAddress, numInputs, startWriteAddress, Convert.ToUInt16(numBytes / 2));
      Array.Copy(values, 0, data, 17, values.Length);
      WriteAsyncData(data, id);
    }

    // ------------------------------------------------------------------------
    /// <summary>Read/Write multiple registers in slave synchronous. The result is given in the response function.</summary>
    /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
    /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
    /// <param name="startReadAddress">Address from where the data read begins.</param>
    /// <param name="numInputs">Length of data.</param>
    /// <param name="startWriteAddress">Address to where the data is written.</param>
    /// <param name="values">Contains the register information.</param>
    /// <param name="result">Contains the result of the synchronous command.</param>
    public void ReadWriteMultipleRegister(ushort id, byte unit, ushort startReadAddress, ushort numInputs, ushort startWriteAddress, byte[] values, ref byte[] result)
    {
      ushort numBytes = Convert.ToUInt16(values.Length);
      if (numBytes % 2 > 0) numBytes++;
      byte[] data;

      data = CreateReadWriteHeader(id, unit, startReadAddress, numInputs, startWriteAddress, Convert.ToUInt16(numBytes / 2));
      Array.Copy(values, 0, data, 17, values.Length);
      result = WriteSyncData(data, id);
    }

    // ------------------------------------------------------------------------
    // Create modbus header for read action
    private byte[] CreateReadHeader(ushort id, byte unit, ushort startAddress, ushort length, byte function)
    {
      byte[] data = new byte[12];

      byte[] _id = BitConverter.GetBytes((short)id);
      data[0] = _id[1];			    // Slave id high byte
      data[1] = _id[0];				// Slave id low byte
      data[5] = 6;					// Message size
      data[6] = unit;					// Slave address
      data[7] = function;				// Function code
      byte[] _adr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddress));
      data[8] = _adr[0];				// Start address
      data[9] = _adr[1];				// Start address
      byte[] _length = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)length));
      data[10] = _length[0];			// Number of data to read
      data[11] = _length[1];			// Number of data to read
      return data;
    }

    // ------------------------------------------------------------------------
    // Create modbus header for write action
    private byte[] CreateWriteHeader(ushort id, byte unit, ushort startAddress, ushort numData, ushort numBytes, byte function)
    {
      byte[] data = new byte[numBytes + 11];

      byte[] _id = BitConverter.GetBytes((short)id);
      data[0] = _id[1];				// Slave id high byte
      data[1] = _id[0];				// Slave id low byte
      data[2] = 0; //checksum
      data[3] = 0; //checksum
      byte[] _size = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(5 + numBytes)));
      data[4] = _size[0];				// Complete message size in bytes
      data[5] = _size[1];				// Complete message size in bytes
      data[6] = unit;					// Slave address
      data[7] = function;				// Function code
      byte[] _adr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddress));
      data[8] = _adr[0];				// Start address
      data[9] = _adr[1];				// Start address
      if (function >= fctWriteMultipleCoils)
      {
        byte[] _cnt = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numData));
        data[10] = _cnt[0];			// Number of bytes
        data[11] = _cnt[1];			// Number of bytes
        data[12] = (byte)(numBytes - 2);
      }
      return data;
    }

    // ------------------------------------------------------------------------
    // Create modbus header for read/write action
    private byte[] CreateReadWriteHeader(ushort id, byte unit, ushort startReadAddress, ushort numRead, ushort startWriteAddress, ushort numWrite)
    {
      byte[] data = new byte[numWrite * 2 + 17];

      byte[] _id = BitConverter.GetBytes((short)id);
      data[0] = _id[1];						// Slave id high byte
      data[1] = _id[0];						// Slave id low byte
      byte[] _size = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(11 + numWrite * 2)));
      data[4] = _size[0];						// Complete message size in bytes
      data[5] = _size[1];						// Complete message size in bytes
      data[6] = unit;							// Slave address
      data[7] = fctReadWriteMultipleRegister;	// Function code
      byte[] _adr_read = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startReadAddress));
      data[8] = _adr_read[0];					// Start read address
      data[9] = _adr_read[1];					// Start read address
      byte[] _cnt_read = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numRead));
      data[10] = _cnt_read[0];				// Number of bytes to read
      data[11] = _cnt_read[1];				// Number of bytes to read
      byte[] _adr_write = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startWriteAddress));
      data[12] = _adr_write[0];				// Start write address
      data[13] = _adr_write[1];				// Start write address
      byte[] _cnt_write = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numWrite));
      data[14] = _cnt_write[0];				// Number of bytes to write
      data[15] = _cnt_write[1];				// Number of bytes to write
      data[16] = (byte)(numWrite * 2);

      return data;
    }

    // ------------------------------------------------------------------------
    // Write asynchronous data
    private void WriteAsyncData(byte[] write_data, ushort id)
    {
      if ((tcpAsyCl != null) && (tcpAsyCl.Connected))
      {
        SocketError errorCode = SocketError.SocketError;
        try
        { //
          tcpAsyCl.BeginSend(write_data, 0, write_data.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
          tcpAsyCl.BeginReceive(tcpAsyClBuffer, 0, tcpAsyClBuffer.Length, SocketFlags.None, out errorCode, new AsyncCallback(OnReceive), tcpAsyCl);
        }
        catch (SystemException)
        {
          
          //CallException(id, write_data[6], write_data[7], excExceptionConnectionLost);
        }
        _connected = tcpAsyCl.Connected;
      }
      else
      {
        if (tcpAsyCl != null)
        {
          _connected = tcpAsyCl.Connected;
        }
        else
        {
          _connected = false;
        }
      }
      //

      if (_connected == false)
      {
        CallException(id, write_data[6], write_data[7], excExceptionConnectionLost);
      }
    }

    // ------------------------------------------------------------------------
    // Write asynchronous data acknowledge
    private void OnSend(System.IAsyncResult result)
    {
      if (result.IsCompleted == false)
      {
        CallException(0xFFFF, 0xFF, 0xFF, excSendFailt);
      }
    }

    
    // ------------------------------------------------------------------------
    // Write asynchronous data response
    private void OnReceive(System.IAsyncResult result)
    {
      if (result.IsCompleted == false)
      {
        CallException(0xFF, 0xFF, 0xFF, excExceptionConnectionLost);
      }
      ushort id = SwapUInt16(BitConverter.ToUInt16(tcpAsyClBuffer, 0));
      byte unit = tcpAsyClBuffer[6];
      byte function = tcpAsyClBuffer[7];
      byte[] data;

      // ------------------------------------------------------------
      if (function == fctWriteMultipleRegister)
      {
        data = new byte[64];
        Array.Copy(tcpAsyClBuffer, 13, data, 0, 64);
      }
      //Write response data
      //if ((function >= fctWriteSingleCoil) && (function != fctReadWriteMultipleRegister))
      //{
      //  data = new byte[2];
      //  Array.Copy(tcpAsyClBuffer, 10, data, 0, 2);
      //}
      // ------------------------------------------------------------
      // Read response data
      else
      {
        //data = new byte[tcpAsyClBuffer[8]];
        //int length
        //int data_length_byte_0 = convertHexToDecimal(tcpAsyClBuffer[15]);
        //int data_length_byte_0 = convertHexToDecimal(tcpAsyClBuffer[15]);
        //int data_length_byte_0 = convertHexToDecimal(tcpAsyClBuffer[15]);
        //int data_length_byte_0 = convertHexToDecimal(tcpAsyClBuffer[15]);
        //data = new byte[tcpAsyClBuffer[8]];
        data = new byte[DataLen];
        Array.Copy(tcpAsyClBuffer, 0, data, 0, DataLen);
      }
      // ------------------------------------------------------------
      // Response data is slave exception
      if (function > excExceptionOffset)
      {
        function -= excExceptionOffset;
        CallException(id, unit, function, tcpAsyClBuffer[8]);
      }
      // ------------------------------------------------------------
      // Response data is regular data
      else if (OnResponseData != null)
      {
        bool IsCorrectCRC = false;
        byte CRCHight = 0;
        byte CRCLow = 0;
        try
        {
          for (int i = 4; i < 77; i++)
          {
            CRCHight ^= tcpAsyClBuffer[i];
            CRCLow ^= tcpAsyClBuffer[i];
          }
          //IsCorrectCRC = (CRCHight == tcpAsyClBuffer[2]) && (CRCLow == tcpAsyClBuffer[3]);
          IsCorrectCRC = (tcpAsyClBuffer[1] == tcpAsyClBuffer[2]) && (CRCLow == tcpAsyClBuffer[3]);
        }
        catch
        {
        }
        OnResponseData(id, unit, function, data, IsCorrectCRC);
      }
    }

    // ------------------------------------------------------------------------
    // Write data and and wait for response
    private byte[] WriteSyncData(byte[] write_data, ushort id)
    {

      if (tcpSynCl.Connected)
      {
        try
        {
          tcpSynCl.Send(write_data, 0, write_data.Length, SocketFlags.None);
          int result = tcpSynCl.Receive(tcpSynClBuffer, 0, tcpSynClBuffer.Length, SocketFlags.None);

          byte unit = tcpSynClBuffer[6];
          byte function = tcpSynClBuffer[7];
          byte[] data;

          if (result == 0) CallException(id, unit, write_data[7], excExceptionConnectionLost);

          // ------------------------------------------------------------
          // Response data is slave exception
          if (function > excExceptionOffset)
          {
            function -= excExceptionOffset;
            CallException(id, unit, function, tcpSynClBuffer[8]);
            return null;
          }
          // ------------------------------------------------------------
          // Write response data
          else if ((function >= fctWriteSingleCoil) && (function != fctReadWriteMultipleRegister))
          {
            data = new byte[2];
            Array.Copy(tcpSynClBuffer, 10, data, 0, 2);
          }
          // ------------------------------------------------------------
          // Read response data
          else
          {
            data = new byte[tcpSynClBuffer[8]];
            Array.Copy(tcpSynClBuffer, 9, data, 0, tcpSynClBuffer[8]);
          }
          return data;
        }
        catch (SystemException)
        {
          CallException(id, write_data[6], write_data[7], excExceptionConnectionLost);
        }
      }
      else CallException(id, write_data[6], write_data[7], excExceptionConnectionLost);
      return null;
    }
  }/*public class Master*/

  
  public class PLCwithID
  {
    public TcpListener listener;
    public int id;
    public bool connect;
    //
    public byte[] data_received;
    public int data_length;
    public bool isNewDataReceived;
  }

  public class SocketTcpServer
  {

    private static ushort _timeout = 500;
    private static ushort _refresh = 10;
    
    private const int READ_BUFFER_SIZE = 255;
    private static byte[] _readBuffer = new byte[READ_BUFFER_SIZE];
    private static int _received_length = 0;
    //const int PORT_NUM = 2010;
    private string _computer_id_address = "192.168.3.25";

    //private TcpClient client;
    
    private string strName;

    private TcpListener listener_1;
    private TcpListener listener_2;

    private Thread listenerThread;

    

    private bool _de_init = false;
    // Thread signal.
    public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

    private int _slave_id = 0;


    private static int _slave_id_1 = 1;
    private static int _slave_id_2 = 2;
    private static bool _connected_1 = false;
    private static bool _connected_2 = false;

    private static PLCwithID[] _plcWithIDs = new PLCwithID[2];
    // ------------------------------------------------------------------------
    /// <summary>Shows if a connection is active.</summary>
    public bool connected
    {
      get { return _connected_1; }
    }

    public byte[] ReceivedBuffer
    {
      get { return _readBuffer; }
    }
    public int ReceivedBufferLength
    {
      get { return _received_length; }
    }

    public PLCwithID[] PLC_with_IDs
    {
      get {return _plcWithIDs;}
    }

    public static void DoBeginAcceptTcpClient_All(PLCwithID slaveWithId)
    {
      // Set the event to nonsignaled state.
      tcpClientConnected.Reset();

      // Start to listen for connections from a client.
      Console.WriteLine(String.Format("Waiting for a connection from ...{0}", slaveWithId.id));

      // Accept the connection. 
      // BeginAcceptSocket() creates the accepted socket.
      slaveWithId.listener.BeginAcceptTcpClient(
          new AsyncCallback(DoAcceptTcpClientCallback_All),
          slaveWithId);

      // Wait until a connection is made and processed before 
      // continuing.
      //tcpClientConnected.WaitOne(3000);
      tcpClientConnected.WaitOne(2000);
    }

    // Process the client connection.
    public static void DoAcceptTcpClientCallback_All(IAsyncResult ar)
    {
      PLCwithID slave_with_id = (PLCwithID)ar.AsyncState;
      // Get the listener that handles the client request.
      TcpListener listener = slave_with_id.listener;

      // End the operation and display the received data on the console.
      TcpClient client = listener.EndAcceptTcpClient(ar);

      // Process the connection here. (Add the client to a
      // server table, read data, etc.)
      Console.WriteLine(String.Format("Client connected completed: {0}", slave_with_id.id));
     // _connected_1 = true;
      slave_with_id.connect = true;

      // Signal the calling thread to continue.
      tcpClientConnected.Set();
      //
      SocketTcpServer aaa = new SocketTcpServer();
      aaa.Create_Client(client, slave_with_id.id);
    }



    // Accept one client connection asynchronously.
    public static void DoBeginAcceptTcpClient(TcpListener listener)
    {
      // Set the event to nonsignaled state.
      tcpClientConnected.Reset();

      // Start to listen for connections from a client.
      Console.WriteLine(String.Format("Waiting for a connection from ...{0}", _slave_id_1));

      // Accept the connection. 
      // BeginAcceptSocket() creates the accepted socket.
      listener.BeginAcceptTcpClient(
          new AsyncCallback(DoAcceptTcpClientCallback),
          listener);

      // Wait until a connection is made and processed before 
      // continuing.
      //tcpClientConnected.WaitOne(3000);
      tcpClientConnected.WaitOne(2000);
    }
    
    // Process the client connection.
    public static void DoAcceptTcpClientCallback(IAsyncResult ar)
    {
      // Get the listener that handles the client request.
      TcpListener listener = (TcpListener)ar.AsyncState;

      // End the operation and display the received data on the console.
      TcpClient client = listener.EndAcceptTcpClient(ar);

      // Process the connection here. (Add the client to a
      // server table, read data, etc.)
      Console.WriteLine(String.Format("Client connected completed: {0}", _slave_id_1));
      _connected_1 = true;
      
      // Signal the calling thread to continue.
      tcpClientConnected.Set();
      //
      SocketTcpServer aaa = new SocketTcpServer();
      aaa.Create_Client(client, _slave_id_1);
    }

    // Accept one client connection asynchronously.
    public static void DoBeginAcceptTcpClient_2(TcpListener listener)
    {
      // Set the event to nonsignaled state.
      tcpClientConnected.Reset();

      // Start to listen for connections from a client.
      Console.WriteLine(String.Format("Waiting for a connection from ...{0}", _slave_id_2));

      // Accept the connection. 
      // BeginAcceptSocket() creates the accepted socket.
      listener.BeginAcceptTcpClient(
          new AsyncCallback(DoAcceptTcpClientCallback_2),
          listener);

      // Wait until a connection is made and processed before 
      // continuing.
      //tcpClientConnected.WaitOne(3000);
      tcpClientConnected.WaitOne(2000);
    }

    // Process the client connection.
    public static void DoAcceptTcpClientCallback_2(IAsyncResult ar)
    {
      // Get the listener that handles the client request.
      TcpListener listener = (TcpListener)ar.AsyncState;

      // End the operation and display the received data on the console.
      TcpClient client = listener.EndAcceptTcpClient(ar);

      // Process the connection here. (Add the client to a
      // server table, read data, etc.)
      Console.WriteLine(String.Format("Client connected completed: {0}", _slave_id_2));
      _connected_2 = true;

      // Signal the calling thread to continue.
      tcpClientConnected.Set();
      //
      SocketTcpServer aaa = new SocketTcpServer();
      aaa.Create_Client(client, _slave_id_2);
    }



    private void Create_Client(TcpClient client, int slave_id)
    {
      try
      {
        UserConnection UserConnection_client = new UserConnection(client, slave_id);
        // Create an event handler to allow the UserConnection to communicate
        UserConnection_client.OnSocketTcpReceived +=new UserConnection.SocketTcpReceived(UserConnection_client_OnSocketTcpReceived); //new TcpComm.SocketTcpReceived(UserConnection_client_OnSocketTcpReceived);
        UserConnection_client.OnSocketTcpDisconnect +=new UserConnection.SocketTcpDisconnect(UserConnection_client_OnSocketTcpDisconnect); //new TcpComm.SocketTcpDisconnect(UserConnection_client_OnSocketTcpDisconnect);
        //
      }
      catch (Exception e)
      {
        string mm = e.Message;
        int kkk = 0;
      }
    }

    private void DoListen()
    {
      try
      {
        // Listen for new connections.
        //listener = new TcpListener(System.Net.IPAddress.Any, PORT_NUM);
        string ip = "192.168.3.25";
        //

        System.Net.IPAddress _ip;
        if (IPAddress.TryParse(ip, out _ip) == false)
        {
          IPHostEntry hst = Dns.GetHostEntry(ip);
          ip = hst.AddressList[0].ToString();
        }
        listener_1 = new TcpListener(System.Net.IPAddress.Parse(ip), 2000);
        listener_2 = new TcpListener(System.Net.IPAddress.Parse(ip), 2001);

        //listener = new TcpListener(System.Net.IPAddress.Any, 2000);//IPAddress Parse
        listener_1.Start();
        listener_2.Start();
        do
        {
          if (_connected_1 == false)
          {
            DoBeginAcceptTcpClient(listener_1);
          }
          if (_connected_2 == false)
          {
            DoBeginAcceptTcpClient_2(listener_2);
          }

        } while (_de_init == false);//(_connected == false);
      }
      catch (Exception e)
      {
        string aaa = e.Message;
        int kk = 0;
      }
    }

    private void DoListenTest()
    {
      try
      {
        // Listen for new connections.
        //listener = new TcpListener(System.Net.IPAddress.Any, PORT_NUM);
        string ip = _computer_id_address;//"192.168.3.25";
        //

        System.Net.IPAddress _ip;
        if (IPAddress.TryParse(ip, out _ip) == false)
        {
          IPHostEntry hst = Dns.GetHostEntry(ip);
          ip = hst.AddressList[0].ToString();
        }

        for (int i = 0; i < _plcWithIDs.Length; i++)
        {
          _plcWithIDs[i].listener = new TcpListener(System.Net.IPAddress.Parse(ip), (2000 + i));
          _plcWithIDs[i].listener.Start();
        }
        do
        {
          for (int idx = 0; idx < _plcWithIDs.Length; idx++)
          {
            if (_plcWithIDs[idx].connect == false)
            {
              DoBeginAcceptTcpClient_All(_plcWithIDs[idx]);
            }
          }
          //if (idx == 0)
          //{
          //  if (SlaveWithIDs[0].connect == false)
          //  {
          //    DoBeginAcceptTcpClient_All(listener_1);
          //  }
          //}
          //if (idx == 1)
          //{
          //  if (SlaveWithIDs[idx].connect == false)
          //  {
          //    DoBeginAcceptTcpClient_All(listener_1);
          //  }
          //}
          //if (_connected_2 == false)
          //{
          //  DoBeginAcceptTcpClient_2(listener_2);
          //}

        } while (_de_init == false);//(_connected == false);



        //listener_1 = new TcpListener(System.Net.IPAddress.Parse(ip), 2000);
        //listener_2 = new TcpListener(System.Net.IPAddress.Parse(ip), 2001);

        ////listener = new TcpListener(System.Net.IPAddress.Any, 2000);//IPAddress Parse
        //listener_1.Start();
        //listener_2.Start();
        //do
        //{
        //  if (_connected_1 == false)
        //  {
        //    DoBeginAcceptTcpClient(listener_1);
        //  }
        //  if (_connected_2 == false)
        //  {
        //    DoBeginAcceptTcpClient_2(listener_2);
        //  }

        //} while (_de_init == false);//(_connected == false);
      }
      catch (Exception e)
      {
        string aaa = e.Message;
        int kk = 0;
      }
    }

    public SocketTcpServer()
    {
    }

    public SocketTcpServer(string computer_ip_address)
    {
      _computer_id_address = computer_ip_address;
      //
      for (int i = 0; i < _plcWithIDs.Length; i++)
      {
        _plcWithIDs[i] = new PLCwithID();
        _plcWithIDs[i].id = (i + 1);
        _plcWithIDs[i].connect = false;
      }
        if (listener_1 == null)
        {
          //listener_1 = new TcpListener(System.Net.IPAddress.Any, 2000);
        }
        else
        {
          /* do nothing */
        }

      listenerThread = new Thread(new ThreadStart(DoListenTest));
      //listenerThread = new Thread(new ThreadStart(DoListen));
      listenerThread.Start();
    }


    
    // ------------------------------------------------------------------------
    /// <summary>Stop connection to slave.</summary>
    public void disconnect()
    {
      Dispose();
    }

    // ------------------------------------------------------------------------
    /// <summary>Destroy master instance.</summary>
    ~SocketTcpServer()
    {
      Dispose();
    }
    
    

    
    /*
         This is the event handler for the UserConnection when it receives a full line.
        Parse the cammand and parameters and take appropriate action.
         */
    private void OnLineReceived(UserConnection sender, string data)
    {
      //if (OnSocketTcpDisconnect != null)
      //{
      //  OnSocketTcpDisconnect(sender);
      //}
    }

    private bool compareTwoArray(byte[] a1, byte[] a2)
    {
      bool ret = false;
      if (a1.Length != a2.Length)
      {
        ret = false;
      }
      else
      {
        bool IsDiff = false;
        for (int i = 0; (i < a1.Length) && (IsDiff == false); i++)
        {
          IsDiff = (a1[i] != a2[i]);
        }
        ret = !IsDiff;
      }
      return ret;
    }

    private void UserConnection_client_OnSocketTcpReceived(object sender, int slave_id, byte[] byte_received, int length)
    {
      try
      {
        bool IsExitLoop = false;

        for (int i = 0; (i < _plcWithIDs.Length) && (IsExitLoop == false); i++)
        {
          if (_plcWithIDs[i].id == slave_id)
          {
            IsExitLoop = true;
            ////Check if new data is received
            //bool isNewDataReceived = false;
            //isNewDataReceived = (_plcWithIDs[i].data_received == null);
            //if (isNewDataReceived == false)
            //{
            //  byte[] previous_data_received = new byte[_plcWithIDs[i].data_received.Length];
            //  Array.Copy(_plcWithIDs[i].data_received, previous_data_received, length);
            //}
            //else
            //{
            //  //
            //  _plcWithIDs[i].data_received = new byte[length];
            //  Array.Copy(byte_received, _plcWithIDs[i].data_received, length);
            //}

            _plcWithIDs[i].data_received = new byte[length];
            Array.Copy(byte_received, _plcWithIDs[i].data_received, length);

            //
            _plcWithIDs[i].data_length = length;
          }
        }

        if (slave_id == 1)
        {
          Array.Copy(byte_received, _readBuffer, length);
          _received_length = length;
          //
          //for(int )
        }
        else if (slave_id == 2)
        {
          Array.Copy(byte_received, _readBuffer, length);
          _received_length = length;
        }
      }
      catch
      {
      }
    }

    private void UserConnection_client_OnSocketTcpDisconnect(object sender, int slave_id)
    {
      if (slave_id == 1)
      {
        _connected_1 = false;
      }
      else if (slave_id == 2)
      {
        _connected_2 = false;
      }
      bool IsExitLoop = false;
      for (int i = 0; (i < _plcWithIDs.Length) && (IsExitLoop == false); i++)
      {
        if (_plcWithIDs[i].id == slave_id)
        {
          _plcWithIDs[i].connect = false;
          _plcWithIDs[i].data_length = 0;
          IsExitLoop = true;
        }
      }
      //if (OnSocketTcpDisconnect != null)
      //{
      //  OnSocketTcpDisconnect(sender);
      //}
    }
    // ------------------------------------------------------------------------
    /// <summary>Destroy master instance</summary>
    public void Dispose()
    {
      _de_init = true;
    }

  }
}
