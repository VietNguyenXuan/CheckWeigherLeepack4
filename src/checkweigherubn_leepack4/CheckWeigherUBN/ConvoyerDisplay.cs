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
  public partial class ConvoyerDisplay : UserControl
  {
    private ConfigurationTypes _configuration = null;

    public ConvoyerDisplay()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Update configuration from main
    /// </summary>
    /// <param name="configuration"></param>
    public void UpdateConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
     
    }

    /// <summary>
    /// Receive and process data from plc
    /// </summary>
    /// <param name="rawdata"></param>
    /// <param name="machineData"></param>
    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      int speed_1 = machineData.PC_Btai_Vao_Speed.value.Convert_to_Int();
      int speed_2 = machineData.PC_Btai_Can_Speed.value.Convert_to_Int();
      int speed_3 = machineData.PC_Btai_Ra_Speed.value.Convert_to_Int();

      this.convoyerSpeed1.UpdateSpeed(speed_1);
      this.convoyerSpeed2.UpdateSpeed(speed_2);
      this.convoyerSpeed3.UpdateSpeed(speed_3);
    }

    
  }
}
