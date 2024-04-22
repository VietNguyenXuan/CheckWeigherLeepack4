using GlacialComponents.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN.ManualMode
{
  public partial class DisplayPLCMemory : Form
  {
    private int numberOfWords = 50;
    public DisplayPLCMemory()
    {
      InitializeComponent();
    }

    private void DisplayData(GlacialList lstview, TcpComm.FX_DATA data)
    {
      bool isExitLoop = false;
      string address = data.device_as_string;
      for (int j = 0; j < lstview.Items.Count && (isExitLoop == false); j++)
      {
        if (lstview.Items[j].SubItems[0].Text == address)
        {
          lstview.Items[j].SubItems[1].Text = data.value.ToString();
          isExitLoop = true;
        }
      }
      if (isExitLoop == false)
      {
        GLItem gLItem = new GLItem();
        gLItem.SubItems[0].Text = address;
        gLItem.SubItems[1].Text = data.value.ToString();
        lstview.Items.Add(gLItem);
      }
    }
    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      
      for (int i = 0; i < rawdata.list_Raw_Data.Count; i++)
      {
        TcpComm.FX_DATA data = rawdata.list_Raw_Data[i];
        //
        string address = data.device_as_string;

        if (i < numberOfWords * 1)
        {
          DisplayData(this.glacialList1, data);
        }
        else if (i < numberOfWords * 2)
        {
          DisplayData(this.glacialList2, data);
        }
        else if (i < numberOfWords * 3)
        {
          DisplayData(this.glacialList3, data);
        }
        else if (i < numberOfWords * 4)
        {
          DisplayData(this.glacialList4, data);
        }
        else if (i < numberOfWords * 5)
        {
          DisplayData(this.glacialList5, data);
        }
        else if (i < numberOfWords * 6)
        {
          DisplayData(this.glacialList6, data);
        }
        else if (i < numberOfWords * 7)
        {
          DisplayData(this.glacialList7, data);
        }
        else if (i < numberOfWords * 8)
        {
          DisplayData(this.glacialList8, data);
        }
        else if (i < numberOfWords * 9)
        {
          DisplayData(this.glacialList9, data);
        }

      }
      //
      this.glacialList1.Refresh();
      this.glacialList2.Refresh();
      this.glacialList3.Refresh();
      this.glacialList4.Refresh();
      this.glacialList5.Refresh();
      this.glacialList6.Refresh();
      this.glacialList7.Refresh();
      this.glacialList8.Refresh();
      this.glacialList9.Refresh();

    }
  }
}
