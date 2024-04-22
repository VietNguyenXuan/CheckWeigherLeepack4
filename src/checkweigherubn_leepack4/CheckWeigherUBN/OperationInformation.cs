#define CHECK_WEIGHER_YUJENG_BOTTLE
//#define CHECK_WEIGHER_MESPACK_CARTONER_BOX
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckWeigherUBN.Dialogs;
using static CheckWeigherUBN.Dialogs.FrmConfirmation;

namespace CheckWeigherUBN
{
  public partial class OperationInformation : UserControl
  {
    public delegate void RequestBuzzerOnOff(object sender, bool value);
    public event RequestBuzzerOnOff OnRequestBuzzerOnOff;


    public delegate void RequestStopCheckingProductFromPLC(object sender);
    public event RequestStopCheckingProductFromPLC OnRequestStopCheckingProductFromPLC;

    public delegate void SendChangeProduct(object sender, ProductManagementType product);
    public event SendChangeProduct OnSendChangeProduct;

    public delegate void SendChoseProduct(object sender);
    public event SendChoseProduct OnSendChoseProduct;

    //
    public delegate void SendClickToResetCounter(object sender);
    public event SendClickToResetCounter OnSendClickToResetCounter;
    //
    public delegate void SendClickToOpenDoor(object sender);
    public event SendClickToResetCounter OnSendClickToOpenDoor;
    //
    private ConfigurationTypes _configuration = null;
    private bool IsAddItem_SKU_OK = false;
    private bool IsAddItem_FGs_OK = false;

    private bool IsEnableUpdateBuzzerStatus = true;

    private eBuzzerStatus currentBuzzerStatus = eBuzzerStatus.ON;

    public OperationInformation()
    {
      InitializeComponent();
      this.btChangeProduct.Click += new System.EventHandler(this.btChangeProduct_Click);
      this.btChoseProduct.Click += BtChoseProduct_Click;
    }

    private void BtChoseProduct_Click(object sender, EventArgs e)
    {
			if (CheckPemission(ePemission.MAIN_Load_va_xac_nhan_chuyen_doi_san_pham))
			{
				if (OnSendChoseProduct != null)
				{
					OnSendChoseProduct(this);
				}
			}
		
    }

    /// <summary>
    /// Update configuration from main
    /// </summary>
    /// <param name="configuration"></param>
    public void UpdateConfiguration(ConfigurationTypes configuration)
    {
      _configuration = configuration;
      //
      DisplayProductItems();
    }

    public void DisplayProductItems()
    {
      IsAddItem_SKU_OK = false;
      //
      this.cbSKU.Items.Clear();
      for (int i = 0; i < this._configuration.list_ProductManagement.Count; i++)
      {
        ProductManagementType product = this._configuration.list_ProductManagement[i];
        //
        if (this.cbSKU.Items.Contains(product.SKU) == false)
        {
          this.cbSKU.Items.Add(product.SKU);
        }

        if (product.id == _configuration.LastProductId)
        {
          this.cbSKU.Text = product.SKU;
          AddFgsToItems(product, true);
        }
      }

      IsAddItem_SKU_OK = true;
    }

    private void AddFgsToItems(ProductManagementType product, bool IsDisplayLastProduct)
    {
      IsAddItem_FGs_OK = false;
      this.cbFGs.Items.Clear();
      this.cbFGs.Text = "";
      List<ProductManagementType> list_ProductManagemenBySku = this._configuration.list_ProductManagement.FindAll(x => x.SKU == product.SKU);
      for (int j = 0; j < list_ProductManagemenBySku.Count; j++)
      {
        ProductManagementType productBySku = list_ProductManagemenBySku[j];
        if (this.cbFGs.Items.Contains(productBySku.Description) == false)
        {
          this.cbFGs.Items.Add(productBySku.Description);
        }
        
        //
        if (IsDisplayLastProduct == true)
        {
          if (productBySku.id == _configuration.LastProductId)
          {
            this.cbFGs.Text = productBySku.Description;
          }
        }
      }/*for (int j = 0; j < this._configuration.list_ProductManagement.Count; j++)*/
      IsAddItem_FGs_OK = true;
    }


    private string GetDescription(ProductManagementType product)
    {
      string des = String.Format("{0}({1}g)", product.Description, product.Target);
      return des;
    }
    private string GetBarcodeFromPLC(PLC_MachineData machineData)
    {
      string barcode = machineData.PLC_Barcode.value.Convert_to_String();
      return barcode;
    }

    private string GetBarcodeFromDatabase()
    {
      string ret = "";
      try
      {
        ProductManagementType product = _configuration.list_ProductManagement.FindLast(x => x.id ==  _configuration.LastProductId);
        if (product != null)
        {
          ret = product.Barcode;
        }
      }
      catch
      {
      }
      return ret;
    }

    /// <summary>
    /// Update data from PLC
    /// </summary>
    /// <param name="rawdata"></param>
    /// <param name="machineData"></param>
    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      //
      this.lblTotal.Text = String.Format("{0}", machineData.PLC_Counter_Total.value.Convert_to_Int().ToString());
      this.lblCounterOK.Text = String.Format("{0}", machineData.PLC_Product_counter_OK.value.Convert_to_Int().ToString());
      this.lblCounterNG.Text = String.Format("{0}", machineData.PLC_Product_counter_NG.value.Convert_to_Int().ToString());
      this.lblLineSpeed.Text = String.Format("{0}", machineData.PLC_Machine_Speed.value.Convert_to_Int().ToString());  
      this.lblOverWeight.Text = String.Format("{0}", machineData.PLC_OverWeight.value.Convert_to_Int().ToString());
      this.lbl_1T.Text = String.Format("{0}", machineData.PLC_1T.value.Convert_to_Int().ToString());

			// this.lblConveyorSpeed.Text = String.Format("{0}", machineData.PLC_Conveyor_Auto_Speed.value.Convert_to_Int().ToString());
			//
			int machineStatus = machineData.PLC_ControlStatus.value.Convert_to_Int();
      bool PLC_Barcode_NG = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Barcode_NG);
#if CHECK_WEIGHER_MESPACK_CARTONER_BOX
      SetBarcodeStatus(PLC_Barcode_NG);

      //Display Barcode from PLC
      if (_configuration.ProductCheckType == (int)(eProductCheck.BOX))
      {
        this.lblBarcodeActual.Text = GetBarcodeFromPLC(machineData);
      }
      else
      {
        this.lblBarcodeActual.Text = GetBarcodeFromDatabase();
      }
#endif
      //Display AutoManual mode
      eMode mode =  machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Man_mode).ConvertToSystemMode();
      this.lblMode.Text = mode.ToString();

      //----------- Buzzer
      eMode BuzzerMode = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Buzzer_Off).ConvertToBuzzerMode();
      if (BuzzerMode == eMode.BUZZER_ON)
      {
        this.lblBuzzer.Text = "BUZZER ON";
        this.lblBuzzer.BackColor = Color.Green;
        this.lblBuzzer.ForeColor = Color.White;
      }
      else /* if (BuzzerMode == eMode.BUZZER_OFF)*/
      {
        this.lblBuzzer.Text = "BUZZER OFF";
        this.lblBuzzer.BackColor = Color.Red;
        this.lblBuzzer.ForeColor = Color.White;
      }
      //if (BuzzerMode == eMode.BUZZER_ON)
      //{
      //	this.lblBuzzer.Text = "BUZZER OFF";
      //	this.lblBuzzer.BackColor = Color.Red;
      //	this.lblBuzzer.ForeColor = Color.White;
      //}
      //else /* if (BuzzerMode == eMode.BUZZER_OFF)*/
      //{
      //	this.lblBuzzer.Text = "BUZZER ON";
      //	this.lblBuzzer.BackColor = Color.Green;
      //	this.lblBuzzer.ForeColor = Color.White;
      //}
#if CHECK_WEIGHER_YUJENG_BOTTLE
      //----------- AutoAssign Changeover
      eMode AutoAssignChangover = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_SwAutoManChangeoverByALC_Request).ConvertToAutoAssignChangeoverMode();
      if (AutoAssignChangover == eMode.AUTO_ASSIGN_CO_ENABLE)
      {
        //this.lblBarcode.Text = "ASSIGN-C/O ENABLE";
				this.lblBarcode.Text = "REJECT ENABLE";
				this.lblBarcode.BackColor = Color.Green;
        this.lblBarcode.ForeColor = Color.White;
      }
      else /* if (BarcodeMode == eMode.BARCODE_DISABLE) */
      {
        //this.lblBarcode.Text = "ASSIGN-C/O DISABLE";
				this.lblBarcode.Text = "REJECT DISABLE";
				this.lblBarcode.BackColor = Color.Red;
        this.lblBarcode.ForeColor = Color.White;
      }
#else
      //----------- Barcode
      eMode BarcodeMode = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Barcode_Disable).ConvertToBarcodeMode();
      if (BarcodeMode == eMode.BARCODE_ENABLE)
      {
        this.lblBarcode.Text = "BARCODE ENABLE";
        this.lblBarcode.BackColor = Color.Green;
        this.lblBarcode.ForeColor = Color.White;
      }
      else /* if (BarcodeMode == eMode.BARCODE_DISABLE) */
      {
        this.lblBarcode.Text = "BARCODE DISABLE";
        this.lblBarcode.BackColor = Color.Red;
        this.lblBarcode.ForeColor = Color.White;
      }
#endif

      //----------- Weigher
      eMode WeigherMode = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Weigher_Disable).ConvertToWeigherMode();
      //if (WeigherMode == eMode.WEIGHER_ENABLE)
      //{
      //  this.lblWeigher.Text = "WEIGHER ENABLE";
      //  this.lblWeigher.BackColor = Color.Green;
      //  this.lblWeigher.ForeColor = Color.White;
      //}
      //else /* if (WeigherMode == eMode.WEIGHER_DISABLE) */
      //{
      //  this.lblWeigher.Text = "WEIGHER DISABLE";
      //  this.lblWeigher.BackColor = Color.Red;
      //  this.lblWeigher.ForeColor = Color.White;
      //}

			if (WeigherMode == eMode.WEIGHER_ENABLE)
			{
				this.lblWeigher.Text = "WEIGHER DISABLE";
				this.lblWeigher.BackColor = Color.Red;
				this.lblWeigher.ForeColor = Color.White;
			}
			else /* if (WeigherMode == eMode.WEIGHER_DISABLE) */
			{
				
				this.lblWeigher.Text = "WEIGHER ENABLE";
				this.lblWeigher.BackColor = Color.Green;
				this.lblWeigher.ForeColor = Color.White;
			}

			//-------------- lblCylinderReject
			//eMode CylinderRejectMode = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Reject_SW_ON).ConvertToCylinderRejectMode();//PLC_Reject_Enable_Disable
			eMode CylinderRejectMode = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Reject_Enable_Disable).ConvertToCylinderRejectMode();
      if (CylinderRejectMode == eMode.CYLINDER_REJECT_ENABLE)
      {
        //this.lblCylinderReject.Text = "REJECT ENABLE";
        //this.lblCylinderReject.BackColor = Color.Green;
        //this.lblCylinderReject.ForeColor = Color.White;
      }
      else /* if (CylinderRejectMode == eMode.CYLINDER_REJECT_DISABLE)*/
      {
        //this.lblCylinderReject.Text = "REJECT DISABLE";
        //this.lblCylinderReject.BackColor = Color.Red;
        //this.lblCylinderReject.ForeColor = Color.White;
      }


      //
      //
      if (IsEnableUpdateBuzzerStatus == true)
      {
        bool IsBuzzerOff = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Buzzer_Off);
        if (IsBuzzerOff == true)
        {
          currentBuzzerStatus = eBuzzerStatus.OFF;
          this.btBuzzerOnOff.Text = "BUZZER ON";
        }
        else
        {
          currentBuzzerStatus = eBuzzerStatus.ON;
          this.btBuzzerOnOff.Text = "BUZZER OFF";
        }
      }
    }

		private void btClickToResetCounter_Click(object sender, EventArgs e)
		{
      if (CheckPemission(ePemission.MAIN_Reset_counter))
      {
				//DialogResult dialogResult = MessageBox.Show("Xác nhận bạn muốn xóa counter về 0?", "Confirmation", MessageBoxButtons.OKCancel);
				//if (dialogResult == DialogResult.OK)
				//{
				//  if (OnSendClickToResetCounter != null)
				//  {
				//    OnSendClickToResetCounter(this);
				//  }
				//}

				Dialogs.FrmConfirmation frmConfirmation = new FrmConfirmation("Xác nhận bạn muốn xóa counter về 0 ?", eConfirmationType.ResetCounter);
				frmConfirmation.OnSendOKClicked += FrmConfirmation_OnSendOKClicked;
				frmConfirmation.ShowDialog();
			}


      
    }


		private void btClickToOpenDoors_Click(object sender, EventArgs e)
		{
			//if (CheckPemission(ePemission.MAIN_ResetNozzle))
			{
				//DialogResult dialogResult = MessageBox.Show("Xác nhận bạn muốn Mở cửa?", "Confirmation", MessageBoxButtons.OKCancel);
			//	if (dialogResult == DialogResult.OK)
			//	{
				
			//	}
        Dialogs.FrmConfirmation frmConfirmation = new FrmConfirmation("Xác nhận bạn muốn mở cửa ?", eConfirmationType.Mo_cua);
				frmConfirmation.OnSendOKClicked += FrmConfirmation_OnSendOKClicked;
				frmConfirmation.ShowDialog();







			}
		}

		private void FrmConfirmation_OnSendOKClicked(object sender, eConfirmationType eConfirmationType)
		{
      if (eConfirmationType == eConfirmationType.Mo_cua)
      {
        if (OnSendClickToOpenDoor != null)
        {
          OnSendClickToOpenDoor(this);
        }
      }
      else if (eConfirmationType == eConfirmationType.ResetCounter)
      {
				if (OnSendClickToResetCounter != null)
				{
					OnSendClickToResetCounter(this);
				}
			}
      else if (eConfirmationType == eConfirmationType.ChangeProduct)
      {
        StartActiveChangeSKU();
      }
    }

    private void StartActiveChangeSKU()
    {
      if (OnRequestStopCheckingProductFromPLC != null)
      {
        OnRequestStopCheckingProductFromPLC(this);
      }
      //
      /* Find correct item */
      bool IsExit = false;
      for (int i = 0; (i < _configuration.list_ProductManagement.Count) && (IsExit == false); i++)
      {
        ProductManagementType product = _configuration.list_ProductManagement[i];
        string des = product.SKU;
        if (des == this.cbSKU.Text)
        {
          List<ProductManagementType> list_ProductManagemenBySku = _configuration.list_ProductManagement.FindAll(x => x.SKU == product.SKU);
          for (int j = 0; (j < list_ProductManagemenBySku.Count) && (IsExit == false); j++)
          {
            ProductManagementType productBySku = list_ProductManagemenBySku[j];
            if (productBySku.Description == this.cbFGs.Text)
            {
              IsExit = true;

              //Save Last
              _configuration.LastProductId = productBySku.id;
              ConfigurationDB sql = new ConfigurationDB();
              sql.Save(_configuration);

              //send to PLC
              if (OnSendChangeProduct != null)
              {
                OnSendChangeProduct(this, productBySku);
              }
            }
          }/*for (int j = 0; (j < list_ProductManagemenBySku.Count) && (IsExit == false); j++)*/
        }/*if (des == this.cbSKU.Text)*/
      }/*for (int i = 0; (i < _configuration.list_ProductManagement.Count) && (IsExit == false); i++)*/
    }


    public void SetNewProductionAndSensActiveChangeSku(ProductManagementType product)
    {
      this.cbSKU.Text = product.SKU;
      this.cbFGs.Text = product.Description;

      StartActiveChangeSKU();
     // this.
    }

   
      

		//    private void btClickToResetCounter_Click(object sender, EventArgs e)
		//{
		//  DialogResult dialogResult = MessageBox.Show("Xác nhận bạn muốn xóa counter về 0?", "Confirmation", MessageBoxButtons.OKCancel);
		//  if (dialogResult == DialogResult.OK)
		//  {
		//    OnSendClickToResetCounter(this);
		//  }
		//}


		private void cbSKU_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (IsAddItem_SKU_OK == true)
      {
        /* Find correct item */
        bool IsExit = false;
        for (int i = 0; (i < _configuration.list_ProductManagement.Count) && (IsExit == false); i++)
        {
          ProductManagementType product = this._configuration.list_ProductManagement[i];
          string des = product.SKU;//GetDescription(product);
          if (des == this.cbSKU.Text)
          {
            IsExit = true;
            AddFgsToItems(product, false);
          }/*if (des == this.comboBox1.Text)*/
        }
      }
    }
#if CHECK_WEIGHER_MESPACK_CARTONER_BOX
    private void SetBarcodeStatus(bool value)
    {
      if (value == false)
      {
        this.lblBarcodeStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
        this.lblBarcodeStatus.ForeColor = Color.White;
        this.lblBarcodeStatus.Text = "PASS";
      }
      else
      {
        this.lblBarcodeStatus.BackColor = System.Drawing.Color.Red;
        this.lblBarcodeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblBarcodeStatus.ForeColor = Color.White;
        this.lblBarcodeStatus.Text = "FAIL";
      }
    }
#endif
    private void btChangeProduct_1_Click(object sender, EventArgs e)
      {
        if (CheckPemission(ePemission.MAIN_Load_va_xac_nhan_chuyen_doi_san_pham))
        {
          DialogResult dialogResult = MessageBox.Show("Bạn muốn thay đổi sản phẩm?", "Warning", MessageBoxButtons.OKCancel);
          if (dialogResult == DialogResult.OK)
          {
            if (OnRequestStopCheckingProductFromPLC != null)
            {
              OnRequestStopCheckingProductFromPLC(this);
            }
            //
            /* Find correct item */
            bool IsExit = false;
            for (int i = 0; (i < _configuration.list_ProductManagement.Count) && (IsExit == false); i++)
            {
              ProductManagementType product = _configuration.list_ProductManagement[i];
              string des = product.SKU;
              if (des == this.cbSKU.Text)
              {
                List<ProductManagementType> list_ProductManagemenBySku = _configuration.list_ProductManagement.FindAll(x => x.SKU == product.SKU);
                for (int j = 0; (j < list_ProductManagemenBySku.Count) && (IsExit == false); j++)
                {
                  ProductManagementType productBySku = list_ProductManagemenBySku[j];
                  if (productBySku.Description == this.cbFGs.Text)
                  {
                    IsExit = true;

                    //Save Last
                    _configuration.LastProductId = product.id;
                    ConfigurationDB sql = new ConfigurationDB();
                    sql.Save(_configuration);

                    //send to PLC
                    if (OnSendChangeProduct != null)
                    {
                      OnSendChangeProduct(this, productBySku);
                    }
                  }
                }/*for (int j = 0; (j < list_ProductManagemenBySku.Count) && (IsExit == false); j++)*/
              }/*if (des == this.cbSKU.Text)*/
            }
          }
        }
      }

		private void btChangeProduct_Click(object sender, EventArgs e)
		{
			if (CheckPemission(ePemission.MAIN_Load_va_xac_nhan_chuyen_doi_san_pham))
			{

        Dialogs.FrmConfirmation frmConfirmation = new FrmConfirmation("Bạn muốn thay đổi sản phẩm ?", eConfirmationType.ChangeProduct);
				frmConfirmation.OnSendOKClicked += FrmConfirmation_OnSendOKClicked;
				frmConfirmation.ShowDialog();
			}
		}

		private void btBuzzerOnOff_1_Click(object sender, EventArgs e)
    {
      
    }
		private void btBuzzerOnOff_Click(object sender, EventArgs e)
		{
			if ((CheckPemission(ePemission.MAIN_Tat_Buzzer)) == true)
			{
				IsEnableUpdateBuzzerStatus = false;

				if (OnRequestBuzzerOnOff != null)
				{
					if (currentBuzzerStatus == eBuzzerStatus.ON)
					{
						OnRequestBuzzerOnOff(this, false);
					}
					else
					{
						OnRequestBuzzerOnOff(this, true);
					}
					this.timer_delay.Enabled = true;
				}
			}
		}
		private bool CheckPemission(ePemission ePemission)
    {
      bool ret = Utils.CheckPemission(_configuration, ePemission);
      if (ret == false)
      {
        ShowMessageLogin();
      }
      return ret;
    }
    private void ShowMessageLogin()
    {
      //MessageBox.Show("Bạn không được phép truy cập trang này", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      Dialogs.FrmWarning frmConfirmation = new FrmWarning("Bạn không được phép truy cập trang này");
      frmConfirmation.ShowDialog();
    }

    private void timer_delay_Tick(object sender, EventArgs e)
    {
      this.timer_delay.Enabled = false;
      IsEnableUpdateBuzzerStatus = true;
    }
    private enum eBuzzerStatus
    {
      ON,
      OFF
    }

        private void cbSKU_Click(object sender, EventArgs e)
        {
            cbSKU.DroppedDown = true;
        }

        private void cbFGs_Click(object sender, EventArgs e)
        {
            cbFGs.DroppedDown = true;
        }


  }
}
