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
  public partial class WeigherDisplay : UserControl
  {
    private ConfigurationTypes _configuration = null;
    public WeigherDisplay()
    {
      InitializeComponent();
      this.lblDiff.Text = "0";
    }
    /// <summary>
    /// Update configuration from main
    /// </summary>
    /// <param name="configuration"></param>
    public void UpdateConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
    }
    public void ClearAll()
    {
      this.lblWeigherStatus.Text = "0";

		}
		/// <summary>
		/// Display Gross and Net value
		/// </summary>
		/// <param name="currentDiff"></param>
		/// <param name="Net_Weight"></param>
		/// <param name="Gross_Weight"></param>
		public void UpdateCurrentDiffAndProcessedWeight(string currentDiff, string Net_Weight, string Gross_Weight, string target_value, string min_value, string max_value, string status)
    {
      this.lblDiff.Text = String.Format("{0}", currentDiff);
      this.lblGrossWeight.Text = String.Format("{0}", Gross_Weight);
      this.lblTarget.Text = String.Format("{0}", target_value);
      this.lblMinValue.Text = String.Format("{0}", min_value);
      this.lblMaxValue.Text = String.Format("{0}", max_value);
      //
      this.lblWeigherStatus.Text = status;
      if (status == "NG")
      {
        this.lblWeigherStatus.BackColor = Color.Red;
        this.lblWeigherStatus.ForeColor = Color.White;
      }
      else if (status == "OK")
      {
        this.lblWeigherStatus.BackColor = Color.White;
        this.lblWeigherStatus.ForeColor = Color.Black;
      }
      else if (status == "1T")
      {
        this.lblWeigherStatus.BackColor = Color.Yellow;
        this.lblWeigherStatus.ForeColor = Color.Black;
      }
      else if (status == "Over")
      {
        this.lblWeigherStatus.BackColor = Color.Orange;
        this.lblWeigherStatus.ForeColor = Color.Black;
      }
      else if (status == "CW Disable")
      {
        this.lblWeigherStatus.BackColor = Color.Red;
        this.lblWeigherStatus.ForeColor = Color.White;
      }
      else if (status == "MAN")
      {
        this.lblWeigherStatus.BackColor = Color.Red;
        this.lblWeigherStatus.ForeColor = Color.White;
      }
      //
      DisplayWeightValueToGauge(Gross_Weight.Convert_to_Int());
    }

    private void DisplayWeightValueToGauge(int Net_WeightInGram)
    {
      if (Net_WeightInGram < 0)
      {
          this.aquaGauge1.Value = 0;
          this.aquaGauge1.ThresholdPercent = 1;
          this.lblNet_Weight.Text = String.Format("{0}", Net_WeightInGram);
      }
      else
      {
        this.lblNet_Weight.Text = String.Format("{0}", Net_WeightInGram);
        this.aquaGauge1.Value = (float)Net_WeightInGram / 1000;
        if(Net_WeightInGram < 10)
        {
          this.aquaGauge1.ThresholdPercent = 1;
        }
        else
        {
          this.aquaGauge1.ThresholdPercent = (((Net_WeightInGram / 1000) * 100) / 20);
        }   
      }
    }

    private ProductManagementType FindCurrentProduct()
    {
      ProductManagementType current_product = null;
      List<ProductManagementType> list_ProductManagement = _configuration.list_ProductManagement.FindAll(x => x.id == _configuration.LastProductId);
      if (list_ProductManagement.Count > 0)
      {
        current_product = list_ProductManagement[0];
      }
      return current_product;
    }
    /// <summary>
    /// Receive and process data from plc
    /// </summary>
    /// <param name="rawdata"></param>
    /// <param name="machineData"></param>
    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      
      int PC_Sai_so = machineData.PC_Sai_so.value.Convert_to_Int();
      int PC_Gioi_han_duoi = machineData.PC_Min_2T.value.Convert_to_Int();
      int PC_Gioi_han_tren = machineData.PC_Max_2T.value.Convert_to_Int();
      int PC_Target = machineData.PC_Target_value.value.Convert_to_Int();
      this.aquaGauge1.ThresholdPercent = PC_Target/5000;

      //---------------------------
      //this.lblTarget.Text = String.Format("{0}", machineData.PC_Target_value.value.Convert_to_Int());
      //this.lblMinValue.Text = String.Format("{0}", machineData.PC_Min_2T.value.Convert_to_Int());
      //this.lblMaxValue.Text = String.Format("{0}", machineData.PC_Max_2T.value.Convert_to_Int());


      //compare with Database and Display
      //ProductManagementType current_product = FindCurrentProduct();
      //if (current_product == null)
      //{
      //  this.lblDiff.ForeColor = Color.Red;
      //  this.lblMinValue.ForeColor = Color.Red;
      //  this.lblMaxValue.ForeColor = Color.Red;
      //}
      //else
      //{
      //  if (PC_Sai_so == current_product.Diff)
      //  {
      //    this.lblDiff.ForeColor = System.Drawing.Color.Blue;
      //  }
      //  else
      //  {
      //    this.lblDiff.ForeColor = Color.Red;
      //  }
      //  //PC_Gioi_han_duoi
      //  if (PC_Gioi_han_duoi == current_product.LowerLimit_2T)
      //  {
      //    this.lblMinValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
          
      //  }
      //  else
      //  {
      //    this.lblMinValue.ForeColor = Color.Red;
      //  }
      //  //
      //  if (PC_Gioi_han_duoi == current_product.LowerLimit_2T)
      //  {
      //    this.lblMaxValue.ForeColor = System.Drawing.Color.Maroon;
      //  }
      //  else
      //  {
      //    this.lblMaxValue.ForeColor = Color.Red;
      //  }
      //}
      //----- Display ActualWeight for Dynamic value
      int PLC_Weight_Value = machineData.PLC_Weight_Value.value.Convert_to_Int();
      if ((PLC_Weight_Value & 0x8000) == 0x8000)
      {
        int current_value = PLC_Weight_Value - 0xFFFF;
       // this.lblActualWeight.Text = String.Format("{0}", current_value);
      }
      else
      {
        //this.lblActualWeight.Text = String.Format("{0}", PLC_Weight_Value);
      }

      //----- Display ActualWeight for Continuesly value
      int PLC_WeigherContinue = machineData.PLC_WeigherContinue.value.Convert_to_Int();
      if ((PLC_WeigherContinue & 0x8000) == 0x8000)
      {
        int PLC_WeigherContinueTmp = PLC_WeigherContinue - 0xFFFF;
        //DisplayWeightValueToGauge(PLC_WeigherContinueTmp);
      }
      else
      {
        //DisplayWeightValueToGauge(PLC_WeigherContinue);
      }

      //Wigher Pass fail
      //int machineStatus = machineData.PLC_ControlStatus.value.Convert_to_Int();
      //bool PLC_Product_NG = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Product_NG);
      //if (PLC_Product_NG == false)
      //{
      //  this.lblWeigherStatus.Text = "PASS";
      //  this.lblWeigherStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      //}
      //else
      //{
      //  this.lblWeigherStatus.Text = "FAIL";
      //  this.lblWeigherStatus.BackColor = Color.Red;
      //}



      //
      this.machineStatus1.UpdateData(rawdata, machineData);
    }

		private void grouper5_Load(object sender, EventArgs e)
		{

		}
	}
}
