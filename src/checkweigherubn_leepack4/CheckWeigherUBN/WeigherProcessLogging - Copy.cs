using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GlacialComponents.Controls;
namespace CheckWeigherUBN
{
  public partial class WeigherProcessLogging : UserControl
  {
    private const int COL_ID = 0;
    private const int COL_DATETIME = COL_ID + 1;
    private const int COL_BARCODE = COL_DATETIME + 1;
    private const int COL_DESCRIPTION = COL_BARCODE + 1;
    private const int COL_TARGET = COL_DESCRIPTION + 1;
    private const int COL_ACTUAL = COL_TARGET + 1;
    private const int COL_DIFF = COL_ACTUAL + 1;
    private const int COL_LOWERLIMIT = COL_DIFF + 1;
    private const int COL_UPPERLIMIT = COL_LOWERLIMIT + 1;
    private const int COL_STATUS = COL_UPPERLIMIT + 1;



    private ConfigurationTypes _configuration = null;
    public WeigherProcessLogging()
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
    public string UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      string current_Diff = "0";
      //
      int ProductType = machineData.PC_Product_Type.value.Convert_to_Int();
      int PLC_Product_Current_ID = machineData.PLC_Product_Current_ID.value.Convert_to_Int();
      string BarcodefromPLC = machineData.PLC_Barcode.value.Convert_to_String();
      //
      int PLC_ID_Slot_1 = machineData.PLC_ID_Slot_1.value.Convert_to_Int();
      int PLC_ID_Slot_2 = machineData.PLC_ID_Slot_2.value.Convert_to_Int();
      int PLC_ID_Slot_3 = machineData.PLC_ID_Slot_3.value.Convert_to_Int();
      int PLC_ID_Slot_4 = machineData.PLC_ID_Slot_4.value.Convert_to_Int();
      int PLC_ID_Slot_5 = machineData.PLC_ID_Slot_5.value.Convert_to_Int();
      int PLC_ID_Slot_6 = machineData.PLC_ID_Slot_6.value.Convert_to_Int();
      int PLC_ID_Slot_7 = machineData.PLC_ID_Slot_7.value.Convert_to_Int();
      int PLC_ID_Slot_8 = machineData.PLC_ID_Slot_8.value.Convert_to_Int();
      int PLC_ID_Slot_9 = machineData.PLC_ID_Slot_9.value.Convert_to_Int();
      int PLC_ID_Slot_10 = machineData.PLC_ID_Slot_10.value.Convert_to_Int();
      //
      int PLC_Weight_Slot_1 = machineData.PLC_Weight_Slot_1.value.Convert_to_Int();
      int PLC_Weight_Slot_2 = machineData.PLC_Weight_Slot_2.value.Convert_to_Int();
      int PLC_Weight_Slot_3 = machineData.PLC_Weight_Slot_3.value.Convert_to_Int();
      int PLC_Weight_Slot_4 = machineData.PLC_Weight_Slot_4.value.Convert_to_Int();
      int PLC_Weight_Slot_5 = machineData.PLC_Weight_Slot_5.value.Convert_to_Int();
      int PLC_Weight_Slot_6 = machineData.PLC_Weight_Slot_6.value.Convert_to_Int();
      int PLC_Weight_Slot_7 = machineData.PLC_Weight_Slot_7.value.Convert_to_Int();
      int PLC_Weight_Slot_8 = machineData.PLC_Weight_Slot_8.value.Convert_to_Int();
      int PLC_Weight_Slot_9 = machineData.PLC_Weight_Slot_9.value.Convert_to_Int();
      int PLC_Weight_Slot_10 = machineData.PLC_Weight_Slot_10.value.Convert_to_Int();
      //
      int StatusBarcode_ID_Slot_1 = machineData.PLC_StatusBarcode_ID_Slot_1.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_2 = machineData.PLC_StatusBarcode_ID_Slot_2.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_3 = machineData.PLC_StatusBarcode_ID_Slot_3.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_4 = machineData.PLC_StatusBarcode_ID_Slot_4.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_5 = machineData.PLC_StatusBarcode_ID_Slot_5.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_6 = machineData.PLC_StatusBarcode_ID_Slot_6.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_7 = machineData.PLC_StatusBarcode_ID_Slot_7.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_8 = machineData.PLC_StatusBarcode_ID_Slot_8.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_9 = machineData.PLC_StatusBarcode_ID_Slot_9.value.Convert_to_Int();
      int StatusBarcode_ID_Slot_10 = machineData.PLC_StatusBarcode_ID_Slot_10.value.Convert_to_Int();
      //
      //Search and found if exists
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_1, PLC_Weight_Slot_1, StatusBarcode_ID_Slot_1, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_2, PLC_Weight_Slot_2, StatusBarcode_ID_Slot_2, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_3, PLC_Weight_Slot_3, StatusBarcode_ID_Slot_3, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_4, PLC_Weight_Slot_4, StatusBarcode_ID_Slot_4, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_5, PLC_Weight_Slot_5, StatusBarcode_ID_Slot_5, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_6, PLC_Weight_Slot_6, StatusBarcode_ID_Slot_6, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_7, PLC_Weight_Slot_7, StatusBarcode_ID_Slot_7, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_8, PLC_Weight_Slot_8, StatusBarcode_ID_Slot_8, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_9, PLC_Weight_Slot_9, StatusBarcode_ID_Slot_9, ProductType, BarcodefromPLC);
      current_Diff = ProcessWeigherBufferData(PLC_ID_Slot_10, PLC_Weight_Slot_10, StatusBarcode_ID_Slot_10, ProductType, BarcodefromPLC);
      return current_Diff;
    }

    private bool IsFoundFromListview(int id)
    {
      bool IsFound = false;
      for (int i = 0; (i < this.glacialList1.Items.Count) && (IsFound == false); i++)
      {
        if (this.glacialList1.Items[i].SubItems[COL_ID].Text == id.ToString())
        {
          IsFound = true;
        }
      }
      return IsFound;
    }

    private string ProcessWeigherBufferData(int PLC_ID_Slot, int PLC_Weight_Slot, int StatusBarcode_ID_Slot, int ProductType, string BarcodeFromPLC)
    {
      string current_Diff = "0";
      /*1. Found from database */
      if (PLC_ID_Slot > 0)
      {
        List<DataLogType> list_DataFounds = _configuration.list_DataLogInShift.FindAll(x => x.CurrentID == PLC_ID_Slot);
        if (list_DataFounds.Count > 0)
        {
          //Found item from database
          //Check if we display Alread
          bool IsAlreadyAdded = IsFoundFromListview(PLC_ID_Slot);
          if (IsAlreadyAdded == false)
          {
            DataLogType dataLog = list_DataFounds[0];
            AddNewItem(dataLog);
          }
        }/*if (list_DataFounds.Count > 0)*/
        else
        {
          //Not found 
          DateTime dt = Utils.GetDateTimeFromClockByShift();
          //search barcode
          List<ProductManagementType> list_ProductFounds = _configuration.list_ProductManagement.FindAll(x => x.id == _configuration.LastProductId);
          if (list_ProductFounds.Count > 0)
          {
            //New comming
            ProductManagementType currentProduct = list_ProductFounds[0];
            //Calcuate value
            string Diff = "0";
            int TargetAsInt = currentProduct.Target.Convert_to_Int();
            int ActualAsInt = PLC_Weight_Slot.Convert_to_Int();
            int DiffAsInt = ActualAsInt - TargetAsInt;
            Diff = DiffAsInt.ToString();
            bool StatusByWeigher = false;
            if ((DiffAsInt >= currentProduct.LowerLimit.Convert_to_Int()) && (DiffAsInt <= currentProduct.UpperLimit.Convert_to_Int()))
            {
              StatusByWeigher = true;
            }
           // StatusByWeigher = 
            //Add new
            DataLogType dataLog = new DataLogType();
            dataLog.CurrentID = PLC_ID_Slot;
            dataLog.ProductType = currentProduct.id;
            dataLog.DateTime = dt.ToString("yyyy/MM/dd HH:mm:ss");
            //
            bool IsDisplayData = true;
            if (StatusBarcode_ID_Slot == 0)
            {
              if (BarcodeFromPLC != "")
              {
                if (BarcodeFromPLC.StartsWith("113"))
                {
                  dataLog.Barcode = "NO READ";
                  IsDisplayData = false;
                }
                else
                {
                  dataLog.Barcode = BarcodeFromPLC;
                }
              }
              else
              {
                dataLog.Barcode = "NO READ";
                IsDisplayData = false;
              }
            }
            else
            {
              dataLog.Barcode = BarcodeFromPLC;
            }
            //
            if (IsDisplayData == true)
            {
              dataLog.Description = currentProduct.Description;
              dataLog.Target = currentProduct.Target.Convert_to_Int();
              dataLog.Actual = PLC_Weight_Slot.Convert_to_Int();
              dataLog.Diff = Diff;
              dataLog.LowerLimit = currentProduct.LowerLimit.Convert_to_Int();
              dataLog.UpperLimit = currentProduct.UpperLimit.Convert_to_Int();
            }
            //
            if (StatusBarcode_ID_Slot == 0)
            {
              dataLog.Status = (int)(eWeigerStatus.NG);
            }
            else
            {
              dataLog.Status = (int)(eWeigerStatus.OK);
              //if (StatusByWeigher == false)
              //{
              //  dataLog.Status = (int)(eWeigerStatus.NG);
              //}
              //else
              //{
              //  dataLog.Status = (int)(eWeigerStatus.OK);
              //}
            }
            //save to database
            DataLogDB sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, true);
            object data_ret =  sql.Save(dataLog);
            //Add to list view
            if (data_ret != null)
            {
              if (data_ret is DataLogType)
              {
                DataLogType dataLogProduct = (DataLogType)(data_ret);
                _configuration.list_DataLogInShift.Add(dataLogProduct);

                //Add to listview
                AddNewItem((DataLogType)(dataLogProduct));
              }
            }
          }
          else
          {
            /* do nothing */
          }
        }
      }/* if (PLC_Weight_Slot > 0)*/
      return current_Diff;
    }

    private void AddNewItem(DataLogType product)
    {

      GLItem item = new GLItem();

      item.SubItems[COL_ID].Text = product.CurrentID.ToString();
      item.SubItems[COL_DATETIME].Text = product.DateTime;
      item.SubItems[COL_BARCODE].Text = product.Barcode;
      item.SubItems[COL_DESCRIPTION].Text = product.Description;
      item.SubItems[COL_TARGET].Text = product.Target.ToString();
      item.SubItems[COL_ACTUAL].Text = product.Actual.ToString();
      item.SubItems[COL_DIFF].Text = product.Diff.ToString();
      item.SubItems[COL_LOWERLIMIT].Text = product.LowerLimit.ToString();
      item.SubItems[COL_UPPERLIMIT].Text = product.UpperLimit.ToString();

      if (product.Status == (int)(eWeigerStatus.NG))
      {
        item.SubItems[COL_STATUS].Text = eWeigerStatus.NG.ToString();
      }
      else if (product.Status == (int)(eWeigerStatus.OK))
      {
        item.SubItems[COL_STATUS].Text = eWeigerStatus.OK.ToString();
      }
      else
      {

      }
      //
      this.glacialList1.Items.Insert(0, item);
      
      //Remove the first one
      if (_configuration.list_DataLogInShift.Count > _configuration.MaxProductDisplay)
      {
        _configuration.list_DataLogInShift.RemoveAt(0);
      }
      //remove the last listview Item
      if (this.glacialList1.Items.Count > _configuration.MaxProductDisplay)
      {
        _configuration.list_DataLogInShift.RemoveAt(this.glacialList1.Items.Count - 1);
      }
      //
      this.glacialList1.Refresh();
    }
    private enum eWeigerStatus
    {
      NG,
      OK,
    }
  }
}
