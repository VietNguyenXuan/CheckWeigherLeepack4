using System;
using System.Collections.Generic;
using System.Text;

namespace TcpComm
{
    //public class ComM_ReturnType
    //{
    //   public UInt16 COMM_OK             = 0;
    //   public UInt16 COMM_FAILED         = 1;
    //   public UInt16 COMM_PORT_INVALID   = 2;
    //   public UInt16 COMM_RX_AVAILABLE   = 3;
    //   public UInt16 COMM_RX_EMPTY       = 4;
    //   public UInt16 COMM_TX_AVAILABLE   = 5;
    //   public UInt16 COMM_OVERLOAD_TX    = 6;
    //   public ComM_ReturnType() { }
    //}
  public enum ComM_ReturnType
  {
    COMM_OK = 0,
    COMM_FAILED = 1,
    //
    COMM_PORT_INVALID = 2,
    COMM_RX_AVAILABLE = 3,
    COMM_RX_EMPTY = 4,
    COMM_TX_AVAILABLE = 5,
    COMM_OVERLOAD_TX = 6,
  }
}
