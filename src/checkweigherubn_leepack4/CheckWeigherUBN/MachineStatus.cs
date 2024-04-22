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
  public partial class MachineStatus : UserControl
  {
    public MachineStatus()
    {
      InitializeComponent();
    }
    

    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      int machineStatus = machineData.PLC_ControlStatus.value.Convert_to_Int();
      bool IsRun = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Machine_Run);
      bool IsStop = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Machine_Stop);
      bool IsAlarm = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Machine_Alarm);
            //
            //this.lblRUN.Visible = IsRun;
            //this.lblSTOP.Visible = IsStop;
            //this.lblALARM.Visible = IsAlarm;

            if (IsRun == true)
            {
                lblRUN.ForeColor = Color.Black;
                lblRUN.BackColor = Color.Lime;
            }
            else
            {
                lblRUN.ForeColor = Color.DimGray;
                lblRUN.BackColor = Color.LightGray;
            }
            //
            if (IsStop == true)
            {
                lblSTOP.ForeColor = Color.White;
                lblSTOP.BackColor = Color.Red;
            }
            else
            {
                lblSTOP.ForeColor = Color.DimGray;
                lblSTOP.BackColor = Color.LightGray;
            }
            //
            if (IsAlarm == true)
            {
                lblALARM.ForeColor = Color.Black;
                lblALARM.BackColor = Color.Yellow;
            }
            else
            {
                lblALARM.ForeColor = Color.DimGray;
                lblALARM.BackColor = Color.LightGray;
            }
        }
    }
}
