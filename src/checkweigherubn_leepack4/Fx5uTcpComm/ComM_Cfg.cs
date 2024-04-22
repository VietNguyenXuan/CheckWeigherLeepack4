using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpComm
{
    public class ComM_Cfg
    {
      /// <summary> 
      /// Message length in communication: 500 bytes
      /// </summary>  
      public UInt16 COMM_MESSAGE_LENGTH = 1024;//500;

      /// <summary> 
      /// Data buffer length for Ethernet: 1024 bytes
      /// </summary> 
      public UInt16 COMM_ETH_DATA_BUFFER_LEN = 1024;

      /// <summary> 
      /// Number of message payload: 65
      /// </summary>
      public UInt16 COMM_MAX_PAYLOAD_SIZE         = 65;

      public ComM_Cfg() { }
    }
}
