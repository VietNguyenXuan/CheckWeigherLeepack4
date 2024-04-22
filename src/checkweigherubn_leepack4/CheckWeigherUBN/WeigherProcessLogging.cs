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
    public delegate void UpdateCurrentWeight(string currentDiff, string NetWeight, string GrossWeight);
    public event UpdateCurrentWeight OnUpdateCurrentWeight;
    //
    private const int COL_ID = 0;
    private const int COL_DATETIME = COL_ID + 1;
    private const int COL_NOZZLE = COL_DATETIME + 1;
    private const int COL_TARGET = COL_NOZZLE + 1;
    private const int COL_ACTUAL = COL_TARGET + 1;
    private const int COL_GROSS = COL_ACTUAL + 1;
    private const int COL_DIFF = COL_GROSS + 1;
    private const int COL_MIN_1T = COL_DIFF + 1;
    private const int COL_MAX_1T = COL_MIN_1T + 1;
    private const int COL_MIN_2T = COL_MAX_1T + 1;
    private const int COL_MAX_2T = COL_MIN_2T + 1;
    private const int COL_PM = COL_MAX_2T + 1;
    private const int COL_STATUS = COL_PM + 1;
    private const int COL_REJECTED_SW = COL_STATUS + 1;


    private ConfigurationTypes _configuration = null;
		private eShift _previousShift = eShift.SHIFT_ALL;
    private int nCountDelayByShiftChange = 0;
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
      _previousShift = Shift.GetShiftFromClock();
		}

    public void ClearAll()
    {
      this.glacialList1.Items.Clear();
			this.glacialList1.Refresh();

		}

    
		/// <summary>
		/// Receive and process data from plc
		/// </summary>
		/// <param name="rawdata"></param>
		/// <param name="machineData"></param>
		public string UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      bool is_enable_process_data = true;
      string current_Diff = "";
			if (is_enable_process_data == true)
      {
				int ProductType = machineData.PC_Product_Type.value.Convert_to_Int();
				int PLC_Product_Current_ID = machineData.PLC_Product_Current_ID.value.Convert_to_Int();
				string BarcodefromPLC = machineData.PLC_Barcode.value.Convert_to_String();
        //--------------
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


				int PLC_Net_Weight_Slot_1 = machineData.PLC_Weight_Slot_1.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_2 = machineData.PLC_Weight_Slot_2.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_3 = machineData.PLC_Weight_Slot_3.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_4 = machineData.PLC_Weight_Slot_4.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_5 = machineData.PLC_Weight_Slot_5.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_6 = machineData.PLC_Weight_Slot_6.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_7 = machineData.PLC_Weight_Slot_7.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_8 = machineData.PLC_Weight_Slot_8.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_9 = machineData.PLC_Weight_Slot_9.value.Convert_to_Int();
        int PLC_Net_Weight_Slot_10 = machineData.PLC_Weight_Slot_10.value.Convert_to_Int();
        //
        int PLC_Gross_Weight_Slot_1 = machineData.PLC_Gross_Weigher_Slot_1.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_2 = machineData.PLC_Gross_Weigher_Slot_2.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_3 = machineData.PLC_Gross_Weigher_Slot_3.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_4 = machineData.PLC_Gross_Weigher_Slot_4.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_5 = machineData.PLC_Gross_Weigher_Slot_5.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_6 = machineData.PLC_Gross_Weigher_Slot_6.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_7 = machineData.PLC_Gross_Weigher_Slot_7.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_8 = machineData.PLC_Gross_Weigher_Slot_8.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_9 = machineData.PLC_Gross_Weigher_Slot_9.value.Convert_to_Int();
        int PLC_Gross_Weight_Slot_10 = machineData.PLC_Gross_Weigher_Slot_10.value.Convert_to_Int();


        //
        int Status_ID_Slot_1 = machineData.PLC_StatusBarcode_ID_Slot_1.value.Convert_to_Int();
        int Status_ID_Slot_2 = machineData.PLC_StatusBarcode_ID_Slot_2.value.Convert_to_Int();
        int Status_ID_Slot_3 = machineData.PLC_StatusBarcode_ID_Slot_3.value.Convert_to_Int();
        int Status_ID_Slot_4 = machineData.PLC_StatusBarcode_ID_Slot_4.value.Convert_to_Int();
        int Status_ID_Slot_5 = machineData.PLC_StatusBarcode_ID_Slot_5.value.Convert_to_Int();
        int Status_ID_Slot_6 = machineData.PLC_StatusBarcode_ID_Slot_6.value.Convert_to_Int();
        int Status_ID_Slot_7 = machineData.PLC_StatusBarcode_ID_Slot_7.value.Convert_to_Int();
        int Status_ID_Slot_8 = machineData.PLC_StatusBarcode_ID_Slot_8.value.Convert_to_Int();
        int Status_ID_Slot_9 = machineData.PLC_StatusBarcode_ID_Slot_9.value.Convert_to_Int();
        int Status_ID_Slot_10 = machineData.PLC_StatusBarcode_ID_Slot_10.value.Convert_to_Int();
        //
        bool Status_Reject_D_Slot_1 = machineData.Status_Reject_ID1.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_2 = machineData.Status_Reject_ID2.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_3 = machineData.Status_Reject_ID3.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_4 = machineData.Status_Reject_ID4.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_5 = machineData.Status_Reject_ID5.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_6 = machineData.Status_Reject_ID6.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_7 = machineData.Status_Reject_ID7.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_8 = machineData.Status_Reject_ID8.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_9 = machineData.Status_Reject_ID9.value.Convert_to_Int() > 0;
        bool Status_Reject_D_Slot_10 = machineData.Status_Reject_ID10.value.Convert_to_Int() > 0;
        //
        string PLC_Barcode_ID_1 = machineData.PLC_Barcode_ID_1.value.Convert_to_String();
        string PLC_Barcode_ID_2 = machineData.PLC_Barcode_ID_2.value.Convert_to_String();
        string PLC_Barcode_ID_3 = machineData.PLC_Barcode_ID_3.value.Convert_to_String();
        string PLC_Barcode_ID_4 = machineData.PLC_Barcode_ID_4.value.Convert_to_String();
        string PLC_Barcode_ID_5 = machineData.PLC_Barcode_ID_5.value.Convert_to_String();
        string PLC_Barcode_ID_6 = machineData.PLC_Barcode_ID_6.value.Convert_to_String();
        string PLC_Barcode_ID_7 = machineData.PLC_Barcode_ID_7.value.Convert_to_String();
        string PLC_Barcode_ID_8 = machineData.PLC_Barcode_ID_8.value.Convert_to_String();
        string PLC_Barcode_ID_9 = machineData.PLC_Barcode_ID_9.value.Convert_to_String();
        string PLC_Barcode_ID_10 = machineData.PLC_Barcode_ID_10.value.Convert_to_String();

        int PLC_Nozzle_Slot_1 = machineData.PLC_Nozzle_Slot_1.value.Convert_to_Int();
        int PLC_Nozzle_Slot_2 = machineData.PLC_Nozzle_Slot_2.value.Convert_to_Int();
        int PLC_Nozzle_Slot_3 = machineData.PLC_Nozzle_Slot_3.value.Convert_to_Int();
        int PLC_Nozzle_Slot_4 = machineData.PLC_Nozzle_Slot_4.value.Convert_to_Int();
        int PLC_Nozzle_Slot_5 = machineData.PLC_Nozzle_Slot_5.value.Convert_to_Int();
        int PLC_Nozzle_Slot_6 = machineData.PLC_Nozzle_Slot_6.value.Convert_to_Int();
        int PLC_Nozzle_Slot_7 = machineData.PLC_Nozzle_Slot_7.value.Convert_to_Int();
        int PLC_Nozzle_Slot_8 = machineData.PLC_Nozzle_Slot_8.value.Convert_to_Int();
        int PLC_Nozzle_Slot_9 = machineData.PLC_Nozzle_Slot_9.value.Convert_to_Int();
        int PLC_Nozzle_Slot_10 = machineData.PLC_Nozzle_Slot_10.value.Convert_to_Int();
        //


        //Search and found if exists
        int id = 0;
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_1, PLC_Net_Weight_Slot_1, Status_ID_Slot_1, ProductType, PLC_Barcode_ID_1, Status_Reject_D_Slot_1.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_1, PLC_Gross_Weight_Slot_1);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_2, PLC_Net_Weight_Slot_2, Status_ID_Slot_2, ProductType, PLC_Barcode_ID_2, Status_Reject_D_Slot_2.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_2, PLC_Gross_Weight_Slot_2);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_3, PLC_Net_Weight_Slot_3, Status_ID_Slot_3, ProductType, PLC_Barcode_ID_3, Status_Reject_D_Slot_3.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_3, PLC_Gross_Weight_Slot_3);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_4, PLC_Net_Weight_Slot_4, Status_ID_Slot_4, ProductType, PLC_Barcode_ID_4, Status_Reject_D_Slot_4.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_4, PLC_Gross_Weight_Slot_4);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_5, PLC_Net_Weight_Slot_5, Status_ID_Slot_5, ProductType, PLC_Barcode_ID_5, Status_Reject_D_Slot_5.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_5, PLC_Gross_Weight_Slot_5);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_6, PLC_Net_Weight_Slot_6, Status_ID_Slot_6, ProductType, PLC_Barcode_ID_6, Status_Reject_D_Slot_6.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_6, PLC_Gross_Weight_Slot_6);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_7, PLC_Net_Weight_Slot_7, Status_ID_Slot_7, ProductType, PLC_Barcode_ID_7, Status_Reject_D_Slot_7.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_7, PLC_Gross_Weight_Slot_7);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_8, PLC_Net_Weight_Slot_8, Status_ID_Slot_8, ProductType, PLC_Barcode_ID_8, Status_Reject_D_Slot_8.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_8, PLC_Gross_Weight_Slot_8);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_9, PLC_Net_Weight_Slot_9, Status_ID_Slot_9, ProductType, PLC_Barcode_ID_9, Status_Reject_D_Slot_9.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_9, PLC_Gross_Weight_Slot_9);
        current_Diff = ProcessWeigherBufferData(id++, PLC_ID_Slot_10, PLC_Net_Weight_Slot_10, Status_ID_Slot_10, ProductType, PLC_Barcode_ID_10, Status_Reject_D_Slot_10.ConvertToCylinderRejectMode(), PLC_Nozzle_Slot_10, PLC_Gross_Weight_Slot_10);
      }
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

    //private ProductManagementType FindProductByBarcode(string barcodefromPLC)
    //{
    //  ProductManagementType product = null;
    //  List<ProductManagementType> list_Product = _configuration.list_ProductManagement.FindAll(x => x.Barcode.Trim() == barcodefromPLC);
    //  if (list_Product.Count > 0)
    //  {
    //    product = list_Product[0];
    //  }
    //  return product;
    //}



    private string ProcessWeigherBufferData(int id, int PLC_ID_Slot, int PLC_Net_Weight_Slot, int Status_ID_Slot, int productIdFromPLC, string BarcodeFromPLC, eMode CylinderRejectMode, int PLC_Nozzle, int PLC_Gross_Weight_Slot)
    {
      string current_Diff = "0";
      /*1. Found from database */
      if (PLC_ID_Slot > 0)
      {
        int ShiftId = (int)(Shift.GetShiftFromClock());
        List<DataLogType> list_DataFounds = _configuration.list_DataLogInShift.FindAll(x => (x.CurrentID == PLC_ID_Slot) && (x.ShiftId == ShiftId));// && (x.ProductId == ProductType));
        //Túi cũ
        if (list_DataFounds.Count > 0)
        {
          //Found item from database
          //Check if we display Alread
          bool IsAlreadyAdded = IsFoundFromListview(PLC_ID_Slot);
          if (IsAlreadyAdded == false)
          {
            DataLogType dataLog = list_DataFounds[0];
            AddNewItemOrUpdateItem(dataLog, false); //Found item from database
          }
          else
          {
            /* find product by SKU*/
            List<ProductManagementType> list_ProductFounds = _configuration.list_ProductManagement.FindAll(x => x.id == _configuration.LastProductId);
            if (list_ProductFounds.Count > 0)
            {
              ProductManagementType currentProduct = list_ProductFounds[0];
              //
              double Target = currentProduct.Target;
              int ActualAsInt = PLC_Net_Weight_Slot.Convert_to_Int();
              double Diff = (double)(ActualAsInt) - Target;
              //
              DataLogType dataLog_update = list_DataFounds[0];
              dataLog_update.Nozzle_Slot = PLC_Nozzle;
              dataLog_update.CurrentID = PLC_ID_Slot;
              dataLog_update.RejectSW = (int)(CylinderRejectMode);
              dataLog_update.Description = currentProduct.Description;
              dataLog_update.Target = currentProduct.Target;
              dataLog_update.Actual = PLC_Net_Weight_Slot.Convert_to_Int();
              dataLog_update.GrossWeight = PLC_Gross_Weight_Slot.Convert_to_Int();
              dataLog_update.Diff = Diff;
              dataLog_update.LowerLimit_1T = currentProduct.LowerLimit_1T;
              dataLog_update.UpperLimit_1T = currentProduct.UpperLimit_1T;
              dataLog_update.LowerLimit_2T = currentProduct.LowerLimit_2T;
              dataLog_update.UpperLimit_2T = currentProduct.UpperLimit_2T;
              dataLog_update.FGs = currentProduct.FGs;
              dataLog_update.ProductId = currentProduct.id;
              dataLog_update.gPackageMaterial = currentProduct.gPackageMaterial;
              //--------------------------------
              if (Status_ID_Slot == 0)
              {
                dataLog_update.Status = (int)(eWeigerStatus.NG);
              }
              else if (Status_ID_Slot == 1)
              {
                dataLog_update.Status = (int)(eWeigerStatus.OK);
              }
              else if (Status_ID_Slot == 2)
              {
                dataLog_update.Status = (int)(eWeigerStatus._1T);
              }
              else if (Status_ID_Slot == 3)
              {
                dataLog_update.Status = (int)(eWeigerStatus.Over);
              }
							else if (Status_ID_Slot == 4)
							{
								dataLog_update.Status = (int)(eWeigerStatus.CW_Disable);
							}
							else if (Status_ID_Slot == 5)
							{
								dataLog_update.Status = (int)(eWeigerStatus.MAN);
							}
							//----------------
							//
							AddNewItemOrUpdateItem(dataLog_update, true);
            }
          }
        }/*if (list_DataFounds.Count > 0)*/
        else
        {
          //kiểm tra shift-change
          if ((int)_previousShift != ShiftId)
          {
            int mm = 0;
          }
          bool is_enabe_add_new = true;
					DataLogType data_found_from_db = _configuration.list_DataLogInShift.Find(x => (x.CurrentID == PLC_ID_Slot));
          if (data_found_from_db != null)
          {
            //nếu đã có từ shift trước đó --> bỏ qua ko tạo mới
            if (data_found_from_db.ShiftId != ShiftId)
            {
              is_enabe_add_new = false; 
						}
          }



					if (is_enabe_add_new == true)
          {
            //Túi mới  
            DateTime dt = Utils.GetDateTimeFromClockByShift();
            //search barcode
            int prodduct_id = productIdFromPLC;
            if (_configuration.list_ProductManagement.Exists(x => x.id == prodduct_id) == false)
            {
              prodduct_id = _configuration.LastProductId;
            }

            List<ProductManagementType> list_ProductFounds = _configuration.list_ProductManagement.FindAll(x => x.id == productIdFromPLC);
            if (list_ProductFounds.Count > 0)
            {
              //New comming
              ProductManagementType currentProduct = list_ProductFounds[0];
              //Calcuate value
              double TargetAsDouble = currentProduct.Target;
              int ActualAsInt = PLC_Gross_Weight_Slot.Convert_to_Int();
              double Diff = (double)(ActualAsInt) - TargetAsDouble;

              //Add new
              DataLogType dataLog = new DataLogType();
              dataLog.CurrentID = PLC_ID_Slot;
              dataLog.ProductId = currentProduct.id;
              dataLog.DateTime = dt.ToString("yyyy/MM/dd HH:mm:ss");
              dataLog.ShiftId = ShiftId;
              dataLog.RejectSW = (int)(CylinderRejectMode);
              bool IsDisplayData = true;

              //--------------------------------
              if (Status_ID_Slot == 0)
              {
                dataLog.Status = (int)(eWeigerStatus.NG);
              }
              else if (Status_ID_Slot == 1)
              {
                dataLog.Status = (int)(eWeigerStatus.OK);
              }
              else if (Status_ID_Slot == 2)
              {
                dataLog.Status = (int)(eWeigerStatus._1T);
              }
              else if (Status_ID_Slot == 3)
              {
                dataLog.Status = (int)(eWeigerStatus.Over);
              }
							else if (Status_ID_Slot == 4)
							{
								dataLog.Status = (int)(eWeigerStatus.CW_Disable);
							}
							else if (Status_ID_Slot == 5)
							{
								dataLog.Status = (int)(eWeigerStatus.MAN);
							}
							//---------------------------------------

							if (IsDisplayData == true)
              {
                dataLog.Nozzle_Slot = PLC_Nozzle;
                dataLog.Description = currentProduct.Description;
                dataLog.Target = currentProduct.Target.Convert_to_Int();
                dataLog.Actual = PLC_Net_Weight_Slot.Convert_to_Int();
                dataLog.GrossWeight = PLC_Gross_Weight_Slot.Convert_to_Int();
                dataLog.Diff = Diff;
                dataLog.LowerLimit_1T = currentProduct.LowerLimit_1T;
                dataLog.UpperLimit_1T = currentProduct.UpperLimit_1T;
                dataLog.LowerLimit_2T = currentProduct.LowerLimit_2T;
                dataLog.UpperLimit_2T = currentProduct.UpperLimit_2T;
                dataLog.FGs = currentProduct.FGs;
                dataLog.gPackageMaterial = currentProduct.gPackageMaterial;
              }
              else
              {
                dataLog.Actual = PLC_Net_Weight_Slot.Convert_to_Int();
              }

              current_Diff = Diff.ToString();


              //save to database
              DataLogDB sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, true);
              object data_ret = sql.Save(dataLog);
              //Add to list view
              if (data_ret != null)
              {
                if (data_ret is DataLogType)
                {
                  DataLogType dataLogProduct = (DataLogType)(data_ret);
                  _configuration.list_DataLogInShift.Add(dataLogProduct);

                  //Add to listview
                  AddNewItemOrUpdateItem((DataLogType)(dataLogProduct), false);
                }
              }
            }
            else
            {
              /* do nothing */
            }
          }
        }
      }/* if (PLC_Weight_Slot > 0)*/
      return current_Diff;
    }
   
   

    public enum eFirstRowData
    {
      Diff,
      Actual,
      Gross,
      Target,
      Min_2T_Value,
      Max_2T_Value,
      Status
    }
    //public string GetFirstRowData_Diff()
    //{
    //  string ret = "";
    //  if (this.glacialList1.Items.Count > 0)
    //  {
    //    ret = this.glacialList1.Items[0].SubItems[COL_DIFF].Text;
    //  }
    //  return ret;
    //}
    public string GetFirstRowData(eFirstRowData firstRowData)
		{
			string ret = "";
			if (this.glacialList1.Items.Count > 0)
			{
        if (firstRowData == eFirstRowData.Actual)
        {
          ret = this.glacialList1.Items[0].SubItems[COL_ACTUAL].Text;
        }
        else if (firstRowData == eFirstRowData.Gross)
        {
          ret = this.glacialList1.Items[0].SubItems[COL_GROSS].Text;
        }
        else if (firstRowData == eFirstRowData.Diff)
        {
          ret = this.glacialList1.Items[0].SubItems[COL_DIFF].Text;
        }
        else if (firstRowData == eFirstRowData.Target)
        {
          ret = this.glacialList1.Items[0].SubItems[COL_TARGET].Text;
        }
        else if (firstRowData == eFirstRowData.Min_2T_Value)
        {
          ret = this.glacialList1.Items[0].SubItems[COL_MIN_2T].Text;
        }
        else if (firstRowData == eFirstRowData.Max_2T_Value)
        {
          ret = this.glacialList1.Items[0].SubItems[COL_MAX_2T].Text;
        }
        else if (firstRowData == eFirstRowData.Status)
        {
          ret = this.glacialList1.Items[0].SubItems[COL_STATUS].Text;
        }
      }
			return ret;
		}
		//public string GetFirstRowData_Gross()
		//{
		//	string ret = "";
		//	if (this.glacialList1.Items.Count > 0)
		//	{
		//		ret = this.glacialList1.Items[0].SubItems[COL_GROSS].Text;
		//	}
		//	return ret;
		//}









		/// <summary>
		/// Check if need update data
		/// </summary>
		/// <param name="item"></param>
		/// <param name="product"></param>
		/// <returns></returns>
		private bool IsNeedUpdateData(GLItem item, DataLogType product)
    {
      bool ret = (item.SubItems[COL_DATETIME].Text != product.DateTime);
      //ret |= item.SubItems[COL_NOZZLE].Text != product.Nozzle_Slot.ToString();
      ret |= item.SubItems[COL_TARGET].Text != product.Target.ToString();
      ret |= item.SubItems[COL_ACTUAL].Text != product.Actual.ToString();
      ret |= item.SubItems[COL_GROSS].Text != product.GrossWeight.ToString();
      ret |= item.SubItems[COL_DIFF].Text != product.Diff.ToString();
      ret |= item.SubItems[COL_MIN_1T].Text != product.LowerLimit_1T.ToString();
      ret |= item.SubItems[COL_MAX_1T].Text != product.UpperLimit_1T.ToString();
      ret |= item.SubItems[COL_MIN_2T].Text != product.LowerLimit_2T.ToString();
      ret |= item.SubItems[COL_MAX_2T].Text != product.UpperLimit_2T.ToString();
      ret |= item.SubItems[COL_PM].Text != product.gPackageMaterial.ToString();
      return ret;
    }

    private void AddNewItemOrUpdateItem(DataLogType product, bool IsUppdate)
    {

      GLItem item = new GLItem();

      bool IsDataChangeByUpdate = false;
      if (IsUppdate == true)
      {
        bool IsExit = false;
        for (int i = 0; (i < this.glacialList1.Items.Count) && (IsExit == false); i++)
        {
          item = this.glacialList1.Items[i];
          if (item.SubItems[COL_ID].Text == product.CurrentID.ToString())
          {
            IsDataChangeByUpdate = IsNeedUpdateData(item, product);
            IsExit = true;
          }
        }
      }/*if (IsUppdate == true)*/
      else
      {
        IsDataChangeByUpdate = true;
      }

      
      //---- put data to listview
      item.SubItems[COL_ID].Text = product.CurrentID.ToString();
      item.SubItems[COL_DATETIME].Text = product.DateTime;
      //item.SubItems[COL_NOZZLE].Text = product.Nozzle_Slot.ToString();
      item.SubItems[COL_TARGET].Text = product.Target.ToString();
      item.SubItems[COL_ACTUAL].Text = product.Actual.ToString();
      item.SubItems[COL_GROSS].Text = product.GrossWeight.ToString();
      item.SubItems[COL_DIFF].Text = product.Diff.ToString();
      item.SubItems[COL_MIN_1T].Text = product.LowerLimit_1T.ToString();
      item.SubItems[COL_MAX_1T].Text = product.UpperLimit_1T.ToString();
      item.SubItems[COL_MIN_2T].Text = product.LowerLimit_2T.ToString();
      item.SubItems[COL_MAX_2T].Text = product.UpperLimit_2T.ToString();
      item.SubItems[COL_PM].Text = product.gPackageMaterial.ToString();
      //----------
      if (product.Status == (int)(eWeigerStatus.NG))
      {
        item.SubItems[COL_STATUS].Text = eWeigerStatus.NG.ToString();
        item.BackColor = Color.Red;
        item.ForeColor = Color.White;
      }
      else if (product.Status == (int)(eWeigerStatus.OK))
      {
        item.SubItems[COL_STATUS].Text = eWeigerStatus.OK.ToString();
        item.BackColor = Color.White;
        item.ForeColor = Color.Black;
      }
      else if (product.Status == (int)(eWeigerStatus._1T))
      {
        item.SubItems[COL_STATUS].Text = "1T";
        item.BackColor = Color.Yellow;
        item.ForeColor = Color.Black;
      }
      else if (product.Status == (int)(eWeigerStatus.Over))
      {
        item.SubItems[COL_STATUS].Text = "Over";
        item.BackColor = Color.Orange;
        item.ForeColor = Color.Black;
      }
			else if (product.Status == (int)(eWeigerStatus.CW_Disable))
			{
				item.SubItems[COL_STATUS].Text = "CW Disable";
				item.BackColor = Color.Red;
				item.ForeColor = Color.White;
			}
			else if (product.Status == (int)(eWeigerStatus.MAN))
			{
				item.SubItems[COL_STATUS].Text = "MAN";
				item.BackColor = Color.Red;
				item.ForeColor = Color.White;
			}
			else
      {
      }
      //-----------------------------------------------------------------
      if (product.RejectSW == (int)(eMode.CYLINDER_REJECT_ENABLE))
      {
        item.SubItems[COL_REJECTED_SW].Text = "ENABLE";
      }
      else
      {
        item.SubItems[COL_REJECTED_SW].Text = "DISABLE";
      }
      //
      if (IsUppdate == false)
      {
        this.glacialList1.Items.Insert(0, item);
      }

      //Remove the first one
      if (_configuration.list_DataLogInShift.Count > _configuration.MaxProductDisplay)
      {
        _configuration.list_DataLogInShift.RemoveAt(0);
      }
      //remove the last listview Item
      if (this.glacialList1.Items.Count > _configuration.MaxProductDisplay)
      {
        this.glacialList1.Items.RemoveAt(this.glacialList1.Items.Count - 1);
      }

     
     
      //
      this.glacialList1.Refresh();
			//
			if ((OnUpdateCurrentWeight != null) && (IsDataChangeByUpdate == true))
			{
				OnUpdateCurrentWeight(item.SubItems[COL_DIFF].Text, item.SubItems[COL_ACTUAL].Text, item.SubItems[COL_GROSS].Text);
			}

		}

    
  }
}
