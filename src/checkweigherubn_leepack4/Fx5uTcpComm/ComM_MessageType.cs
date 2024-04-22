using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;

namespace TcpComm
{
    public class ComM_EthMessageType
    {
      public ushort MessageId;             /* ID of Message */
      public bool IsCorrectCRC;
      public byte[] Data = new byte[64];   /* 32 bytes User data */
      public ComM_EthMessageType() { }
    }
}
