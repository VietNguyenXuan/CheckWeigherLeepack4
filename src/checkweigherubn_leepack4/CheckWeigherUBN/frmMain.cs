//#define ENABLE_DEMO_MODE
using CheckWeigherUBN.ASingleInstanceApp;
using CheckWeigherUBN.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN
{
	public partial class frmMain : Form
	{
		private string plcIPAddress = "192.168.3.250";//"192.168.3.1";
		private ushort portNumber = 2000;
		//
		private PLCFxCommunication plcComm;

		private ConfigurationTypes _configuration = null;

		private frmManual frmManual = null;
		private bool IsfrmManualShow = false;
		private bool IsStopCheckingProductFromPLC = false;
		//
		private frmDisplayErrorConnection frmDisplayErrorConnection = null;
		private bool IsfrmfrmErrorConnectionShow = false;
		private bool IsNeedShutdown = false;

		/// <summary>
		/// Kiểm tra việc xuất report tự động
		/// </summary>
		private bool IsEnableExportBeforeShiftChange = false;
		//
		private frmSettings frmSettings = null;

		private Timer _delay_open_door = new Timer();
		//
		public frmMain()
		{
			InitializeComponent();
			this.operationInformation1.Dock = DockStyle.Fill;
			//
			this.errorLog1.OnRequestAlarmReset += ErrorLog1_OnRequestAlarmReset;
			this.operationInformation1.OnSendClickToResetCounter += OperationInformation1_OnSendClickToResetCounter;
			this.operationInformation1.OnSendClickToOpenDoor += OperationInformation1_OnSendClickToOpenDoor;
			this.operationInformation1.OnSendChangeProduct += OperationInformation1_OnSendChangeProduct;
			this.operationInformation1.OnSendChoseProduct += OperationInformation1_OnSendChoseProduct;
			this.operationInformation1.OnRequestStopCheckingProductFromPLC += OperationInformation1_OnRequestStopCheckingProductFromPLC;
			this.operationInformation1.OnRequestBuzzerOnOff += OperationInformation1_OnRequestBuzzerOnOff;
			this.weigherProcessLogging1.OnUpdateCurrentWeight += WeigherProcessLogging1_OnUpdateCurrentWeight;
			//
			if (IsDevelopComputer() == false)
			{
				this.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.WindowState = FormWindowState.Normal;
				this.timer_check_export_excel.Interval = 1000;
			}
			this.errorConnection1.Visible = false;

			this._delay_open_door.Interval = 1000;
			this._delay_open_door.Tick += _delay_open_door_Tick;

		}



		private void OperationInformation1_OnRequestStopCheckingProductFromPLC(object sender)
		{
			IsStopCheckingProductFromPLC = true;
		}

		private bool IsDevelopComputer()
		{
			string a = Environment.MachineName;

			bool ret = ((Environment.MachineName == "DESKTOP-EGML8TG") ||
									(Environment.MachineName == "DESKTOP-LFAH4O1") ||
									(Environment.MachineName == "DESKTOP-4RA6CN2") ||
									(Environment.MachineName == "DESKTOP-1S87R13") ||
									(Environment.MachineName == "DESKTOP-9DCEBM8") || //Laptop của Viet
									(Environment.MachineName == "DESKTOP-VIPBB6Q")
									);
			return ret;
			//return true;
		}

		private int[] ConvertBarcodeToBlockOfBytes(ProductManagementType product)
		{
			List<int> list_data = new List<int>();
			//
			if (product.FGs.Length > 0)
			{
				for (int i = 0; i < product.FGs.Length; i++)
				{
					char chr = product.FGs[i];
					if (chr == ' ')
					{
						/* do nothing */
					}
					else
					{
						int data = Convert.ToByte(chr);
						list_data.Add(data);
					}
					//
				}
			}
			return list_data.ToArray();
		}

		private void FrmSettings_OnRequestSendUpdateParameter(NumericKeyboard.frmKeyBoard.eFromParent eFromParent, int value)
		{
			//SETTINGS_PC_Delay_Barcode,
			//SETTINGS_PC_Reject_Time,
			//SETTINGS_PC_Reject_Time_Box_Conti,
			//SETTINGS_PC_Delay_Reject,
			//SETTINGS_PC_Reject_Number_Box

			if (plcComm != null)
			{
				if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Barcode)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Delay_Barcode, value);
				}
				else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Time, value);
				}
				else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Time_Box_Conti)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Time_Box_Conti, value);
				}
				else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Delay_Reject)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Delay_Reject, value);
				}
				else if (eFromParent == NumericKeyboard.frmKeyBoard.eFromParent.SETTINGS_PC_Reject_Number_Box)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Number_Box, value);
				}
			}/* if (plcComm != null)*/
		}

		private void OperationInformation1_OnSendChangeProduct(object sender, ProductManagementType product)
		{
			if (plcComm != null)
			{
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Product_Type, product.id);
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Target_value, product.Target.Convert_to_Int());
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Sai_so, product.Diff.Convert_to_Int());
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Min_1T, product.LowerLimit_1T.Convert_to_Int());
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Gioi_han_tren_Max_1T, product.UpperLimit_1T.Convert_to_Int());
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Min_2T, product.LowerLimit_2T.Convert_to_Int());
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Max_2T, product.UpperLimit_2T.Convert_to_Int());
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_VALUE_BAO_BI, product.gPackageMaterial.Convert_to_Int());
				//
				int[] dataToWrite = ConvertBarcodeToBlockOfBytes(product);
				if (dataToWrite.Length > 0)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BarcodeW, dataToWrite);
				}
				//
				IsStopCheckingProductFromPLC = false;
				//----------------------------------------
				this.weigherProcessLogging1.ClearAll();
				this.weigherDisplay1.ClearAll();



			}
		}

		private void OperationInformation1_OnRequestBuzzerOnOff(object sender, bool value)
		{
			if (plcComm != null)
			{
				if (value == true)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Buzzer_OFF);
				}
				else
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Buzzer_ON);
				}
			}
		}


		private void OperationInformation1_OnSendClickToResetCounter(object sender)
		{
			if (plcComm != null)
			{
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Counter_Reset_PB);
			}
		}


		private void OperationInformation1_OnSendClickToOpenDoor(object sender)
		{
			if (plcComm != null)
			{
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_OpenDoor_ON_PB);
				this._delay_open_door.Enabled = true;
			}
		}

		private void _delay_open_door_Tick(object sender, EventArgs e)
		{
			this._delay_open_door.Enabled = false;
			if (plcComm != null)
			{
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_OpenDoor_OFF_PB);
			}
		}


		private void ErrorLog1_OnRequestAlarmReset(object sender)
		{
			if (plcComm != null)
			{
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_ALARM_RESET_PB);
			}
		}

		private void SendShutdownOK()
		{
			if (plcComm != null)
			{
				plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Shutdown_OK);
			}
		}

		private void FrmManual_OnSendBufferDataChanged(object sender, int buffer_id, int value)
		{
			if (plcComm != null)
			{
				if (buffer_id == 1)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Time_54, value);
				}
				else if (buffer_id == 2)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Time_Box_Conti_57, value);
				}
				else if (buffer_id == 3)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Delay_Reject_58, value);
				}
				else if (buffer_id == 4)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Number_Box_59, value);
				}
				else if (buffer_id == 5)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Front_Machine_Run_Time_388, value);
				}
				else if (buffer_id == 6)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Front_Machine_Stop_Time_389, value);
				}
				else if (buffer_id == 7)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Behind_Machine_Run_Time_390, value);
				}
				else if (buffer_id == 8)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Behind_Machine_Stop_Time_391, value);
				}

			}
		}

		private void FrmManual_OnSendSpeedValueSetup(object sender, eDeviceType eConveyor, int value)
		{
			if (plcComm != null)
			{
				if (eConveyor == eDeviceType.BT_TACH_CHAI)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.RESETUP_COVOYER_1_SPEED, value);
				}
				else if (eConveyor == eDeviceType.BT_CAN)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.RESETUP_COVOYER_2_SPEED, value);
				}
				else if (eConveyor == eDeviceType.BT_REJECT)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.RESETUP_COVOYER_3_SPEED, value);
				}
				else if (eConveyor == eDeviceType.BT_ALL)
				{
					plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.RESETUP_COVOYER_Auto_Speed, value);
				}
			}


		}


		private void FrmProduct_OnUpdateProductDone(object sender, ProductManagementType product)
		{
			if ((plcComm != null) && (product != null))
			{
				/* check if this is current product*/
				if (product.id == _configuration.LastProductId)
				{
					OperationInformation1_OnSendChangeProduct(sender, product);
				}/*if (product.id == _configuration.LastProductId)*/
			}
		}

		private void FrmProduct_OnSendImportDataFromExcelDone(object sender)
		{
			this.operationInformation1.DisplayProductItems();
		}

		private void CheckAndResendData(PLC_MachineData machineData)
		{
			if (IsStopCheckingProductFromPLC == false)
			{
				/****************** RESET value in AUTOMODE *****************/
				int machineStatus = machineData.PLC_ControlStatus.value.Convert_to_Int();
				int PC_ControlStatus = machineData.PC_Control_status.value.Convert_to_Int();
				bool IsManMode = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Man_mode);
				eMode eSystemMode = IsManMode.ConvertToSystemMode();
				//
				if (eSystemMode == eMode.AUTO) // in auto mode
				{

					//if (PC_ControlStatus.ToBoolean((int)ePC_ControlStatus.PC_START_PB) == true)
					//{
					//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_START_PB);
					//}
					//if (PC_ControlStatus.ToBoolean((int)ePC_ControlStatus.PC_STOP_PB) == true)
					//{
					//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_STOP_PB);
					//}
					//if (PC_ControlStatus.ToBoolean((int)ePC_ControlStatus.PC_BT_TACH_CHAI_START_PB) == true)
					//{
					//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_TACH_CHAI_START_PB);
					//}
					//if (PC_ControlStatus.ToBoolean((int)ePC_ControlStatus.PC_BT_TACH_CHAI_STOP_PB) == true)
					//{
					//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_TACH_CHAI_STOP_PB);
					//}
					//if (PC_ControlStatus.ToBoolean((int)ePC_ControlStatus.PC_BT_CAN_START_PB) == true)
					//{
					//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_CAN_START_PB);
					//}
					//if (PC_ControlStatus.ToBoolean((int)ePC_ControlStatus.PC_BT_CAN_STOP_PB) == true)
					//{
					//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_CAN_STOP_PB);
					//}
					//if (PC_ControlStatus.ToBoolean((int)ePC_ControlStatus.PC_BT_REJECT_START_PB) == true)
					//{
					//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_REJECT_START_PB);
					//}
					//if (PC_ControlStatus.ToBoolean((int)ePC_ControlStatus.PC_BT_REJECT_STOP_PB) == true)
					//{
					//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_REJECT_STOP_PB);
					//}
				}
				// if (machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Machine_Run);






				/****************** WRITE MODE *****************/
				if (machineData.PC_Product_Type.value.Convert_to_Int() != _configuration.LastProductId)
				{
					if (plcComm != null)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Product_Type, _configuration.LastProductId);
					}
				}
				/* check current product_type*/
				bool IsExit = false;
				for (int i = 0; (i < _configuration.list_ProductManagement.Count) && (IsExit == false); i++)
				{
					ProductManagementType product = _configuration.list_ProductManagement[i];
					if (product.id == _configuration.LastProductId)
					{
						IsExit = true;
						//
						if (machineData.PC_Product_Type.value.Convert_to_Int() != product.id)
						{
							plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Product_Type, product.id);
						}
						else if (machineData.PC_Target_value.value.Convert_to_Int() != product.Target.Convert_to_Int())
						{
							plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Target_value, product.Target.Convert_to_Int());
						}
						//else if (machineData.PC_Sai_so.value.Convert_to_Int() != product.Diff.Convert_to_Int())
						//{
						//  plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Sai_so, product.Diff.Convert_to_Int());
						//}
						else if (machineData.PC_Min_1T.value.Convert_to_Int() != product.LowerLimit_1T.Convert_to_Int())
						{
							plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Min_1T, product.LowerLimit_1T.Convert_to_Int());
						}
						else if (machineData.PC_Max_1T.value.Convert_to_Int() != product.UpperLimit_1T.Convert_to_Int())
						{
							plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Gioi_han_tren_Max_1T, product.UpperLimit_1T.Convert_to_Int());
						}
						// Min 2T
						else if (machineData.PC_Min_2T.value.Convert_to_Int() != product.LowerLimit_2T.Convert_to_Int())
						{
							plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Min_2T, product.LowerLimit_2T.Convert_to_Int());
						}
						// Max 2T
						else if (machineData.PC_Max_2T.value.Convert_to_Int() != product.UpperLimit_2T.Convert_to_Int())
						{
							plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Gioi_han_duoi_Max_2T, product.LowerLimit_2T.Convert_to_Int());
						}
						else if (machineData.PC_Value_BaoBi.value.Convert_to_Int() != product.gPackageMaterial.Convert_to_Int())
						{
							plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_VALUE_BAO_BI, product.LowerLimit_2T.Convert_to_Int());
						}
						//
						//else if (machineData.PC_Barcode.value.Convert_to_String() != product.Barcode)
						//{
						//  int[] dataToWrite = ConvertBarcodeToBlockOfBytes(product);
						//  if (dataToWrite.Length > 0)
						//  {
						//    plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BarcodeW, dataToWrite);
						//  }
						//}
					}
				}
			}/*if (IsStopCheckingProductFromPLC == true)*/
		}


		/// <summary>
		/// User in frmManualMode and click START/STOP
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eSwitchmodeRequest"></param>
		private void FrmManual_OnSendModeChange(object sender, eDeviceType eDeviceType, eMode eSwitchmodeRequest)
		{
			if (plcComm != null)
			{
				if (eDeviceType == eDeviceType.BUTTON_CYLINDER_REJECT)
				{
					if (eSwitchmodeRequest == eMode.BUTTON_CYLINDER_REJECT_ON)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Cyl_ON);
					}
					else if (eSwitchmodeRequest == eMode.BUTTON_CYLINDER_REJECT_OFF)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Cyl_OFF);
					}
				}
				else if ((eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_REJECT))
				{
					if (eSwitchmodeRequest == eMode.REJECT_ENABLE)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Enable);
					}
					else if (eSwitchmodeRequest == eMode.REJECT_DISABLE)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Reject_Disable);
					}
				}
				else if (eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_WEIGHER)
				{
					if (eSwitchmodeRequest == eMode.WEIGHER_ENABLE)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Weigher_Enable);
					}
					else if (eSwitchmodeRequest == eMode.WEIGHER_DISABLE)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Weigher_Disable);
					}
				}
				else if (eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_BARCODE)
				{
					if (eSwitchmodeRequest == eMode.BARCODE_ENABLE)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Barcode_Enable);
					}
					else if (eSwitchmodeRequest == eMode.BARCODE_DISABLE)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Barcode_Disable);
					}
				}
				else if (eDeviceType == eDeviceType.SWITCH_ENABLE_DISABLE_AUTO_ASSIGN_CO)
				{
					if (eSwitchmodeRequest == eMode.AUTO_ASSIGN_CO_ENABLE)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_AutoAssignCO_Enable);
					}
					else if (eSwitchmodeRequest == eMode.AUTO_ASSIGN_CO_DISABLE)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_AutoAssignCO_Disable);
					}
				}
				if (eDeviceType == eDeviceType.SWITCH_BUZZER)
				{
					if (eSwitchmodeRequest == eMode.BUZZER_ON)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Buzzer_ON);
					}
					else if (eSwitchmodeRequest == eMode.BUZZER_OFF)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Buzzer_OFF);
					}
				}
				else if (eDeviceType == eDeviceType.SWITCH_AUTO_MAN)
				{
					if (eSwitchmodeRequest == eMode.AUTO)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Auto_mode);
					}
					else if (eSwitchmodeRequest == eMode.MANUAL)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_Man_mode);
					}
				}
				else if (eDeviceType == eDeviceType.RADIO_CHON_HUONG_REJECT)
				{
					if (eSwitchmodeRequest == eMode.RADIO_CHON_HUONG_REJECT_1_HUONG)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_REJECT_1_HUONG);
					}
					else if (eSwitchmodeRequest == eMode.RADIO_CHON_HUONG_REJECT_2_HUONG)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_REJECT_2_HUONG);
					}
				}

			}
		}

		private void FrmManual_OnSendRequestStartStopPB(object sender, eDeviceType eConveyor, eConveyorStatus conveyorStatusRequest)
		{
			if (plcComm != null)
			{
				if (eConveyor == eDeviceType.BT_TACH_CHAI)
				{
					if (conveyorStatusRequest == eConveyorStatus.RUN)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_TACH_CHAI_START_PB);
					}
					else if (conveyorStatusRequest == eConveyorStatus.STOP)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_TACH_CHAI_STOP_PB);
					}
				}
				else if (eConveyor == eDeviceType.BT_CAN)
				{
					if (conveyorStatusRequest == eConveyorStatus.RUN)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_CAN_START_PB);
					}
					else
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_CAN_STOP_PB);
					}
				}
				else if (eConveyor == eDeviceType.BT_REJECT)
				{
					if (conveyorStatusRequest == eConveyorStatus.RUN)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_REJECT_START_PB);
					}
					else
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_BT_REJECT_STOP_PB);
					}
				}
				else if (eConveyor == eDeviceType.BT_ALL)
				{
					if (conveyorStatusRequest == eConveyorStatus.RUN)
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_START_PB);
					}
					else
					{
						plcComm.RegisterUserRequest(PLCFxCommunication.eREQUEST_MODE.WRITE_PC_STOP_PB);
					}
				}
			}
		}

		private void WeigherProcessLogging1_OnUpdateCurrentWeight(string currentDiff, string Net_Weight, string Gross_Weight)
		{
			string diff = this.weigherProcessLogging1.GetFirstRowData(WeigherProcessLogging.eFirstRowData.Diff);
			string actual = this.weigherProcessLogging1.GetFirstRowData(WeigherProcessLogging.eFirstRowData.Actual);
			string gross = this.weigherProcessLogging1.GetFirstRowData(WeigherProcessLogging.eFirstRowData.Gross);
      string target = this.weigherProcessLogging1.GetFirstRowData(WeigherProcessLogging.eFirstRowData.Target);
      string min = this.weigherProcessLogging1.GetFirstRowData(WeigherProcessLogging.eFirstRowData.Min_2T_Value);
      string max = this.weigherProcessLogging1.GetFirstRowData(WeigherProcessLogging.eFirstRowData.Max_2T_Value);
      string status = this.weigherProcessLogging1.GetFirstRowData(WeigherProcessLogging.eFirstRowData.Max_2T_Value);

      this.weigherDisplay1.UpdateCurrentDiffAndProcessedWeight(diff, actual, gross, target, min, max, status);
		}

		private void UpdateConfiguration()
		{
			//
			this.weigherDisplay1.UpdateConfiguration(_configuration);
			this.ConvoyerDisplay1.UpdateConfiguration(_configuration);
			this.weigherProcessLogging1.UpdateConfiguration(_configuration);
			this.errorLog1.UpdateConfiguration(_configuration);
			this.operationInformation1.UpdateConfiguration(_configuration);
		}

		private int nCountDemo = 0;
		private void timer1_demo_Tick(object sender, EventArgs e)
		{
			timer1_demo.Enabled = false;
			int i = 0;
			PLCFx5U_RawData rawdata = new PLCFx5U_RawData(1000, 100);
			PLC_MachineData machineData = new PLC_MachineData(1000);
			//


			Random rnd = new Random();
			machineData.PLC_ID_Slot_1.value = nCountDemo;
			machineData.PLC_Weight_Slot_1.value = rnd.Next(1000, 1999);
			machineData.PLC_Gross_Weigher_Slot_1.value = rnd.Next(2000, 3000);
			machineData.PLC_StatusBarcode_ID_Slot_1.value = rnd.Next(0, 3);
			machineData.PLC_Nozzle_Slot_1.value = rnd.Next(0, 12);
			machineData.PC_Product_Type.value = _configuration.LastProductId; //demo
			//
			//machineData.PLC_Nozzle_Slot_1.value = rnd.Next(0, 12);


			this.weigherProcessLogging1.UpdateData(rawdata, machineData); //demo mode
																																		//
			nCountDemo++;
			timer1_demo.Enabled = true;
		}


		private void timer_delay_shutdown_Tick(object sender, EventArgs e)
		{
			timer_delay_shutdown.Enabled = false;
			//
			if (plcComm != null)
			{
				plcComm.StopCommunication();
			}
			//
			try
			{
				var psi = new ProcessStartInfo("shutdown", "/s /t 0");
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;
				Process.Start(psi);
			}
			catch
			{
			}
		}

		private void PlcComm_OnReadDataDone(object sender, PLCFx5U_RawData rawdata, PLC_MachineData machineData)
		{
#if ENABLE_DEMO_MODE
      
#else
			//shutdown
			IsNeedShutdown = machineData.PLC_ControlStatus.value.Convert_to_Int().ToBoolean((int)(ePLC_ControlStatus.PLC_PC_Shutdown_Request));
			if (IsNeedShutdown == true)
			{
				//Send shutdwon ok
				SendShutdownOK();
				//
				timer_delay_shutdown.Enabled = true;
			}
			else
			{
				//bool IsNeedUpdateBottleOrBox = false;
				//bool IsFor_BOTTLE = machineData.PLC_ControlStatus.value.Convert_to_Int().ToBoolean((int)(ePLC_ControlStatus.PLC_THUNG_OR_CHAI));
				//if ((IsFor_BOTTLE == true) && (_configuration.ProductCheckType == (int)(eProductCheck.BOX)))
				//{
				//  IsNeedUpdateBottleOrBox = true;
				//}
				//else if ((IsFor_BOTTLE == false) && (_configuration.ProductCheckType == (int)(eProductCheck.BOTTLE)))
				//{
				//  IsNeedUpdateBottleOrBox = true;
				//}
				//else
				//{
				//  IsNeedUpdateBottleOrBox = false;
				//}
				//if (IsNeedUpdateBottleOrBox == true)
				//{
				//AutoSynchonizeProductionfromPLC(IsFor_BOTTLE);
				//}

				//Display to HMI
				this.ConvoyerDisplay1.UpdateData(rawdata, machineData);
				this.weigherProcessLogging1.UpdateData(rawdata, machineData);
				this.weigherDisplay1.UpdateData(rawdata, machineData);
				this.errorLog1.UpdateData(rawdata, machineData);
				this.operationInformation1.UpdateData(rawdata, machineData);
				//
				if (IsfrmManualShow == true)
				{
					if (frmManual != null)
					{
						frmManual.UpdateData(rawdata, machineData);
					}
				}

				if (frmSettings != null)
				{
					frmSettings.UpdateData(rawdata, machineData);
				}
			}
			/***************************************************/
			//CheckAndResendData(machineData);
			//Show down
#endif
		}



		private void PlcComm_OnNotifyCommunicationStatus(object sender, TcpComm.STATUS status)
		{
			if (status == TcpComm.STATUS.INIT_OK)
			{
				this.weigherProcessLogging1.Dock = DockStyle.Fill;
				this.errorConnection1.Visible = false;
			}
			else if ((status == TcpComm.STATUS.INIT_FAILED) || (status == TcpComm.STATUS.TIME_OUT))
			{
				if (this.timer1_demo.Enabled == false)
				{
					this.errorConnection1.Visible = true;
					this.errorConnection1.BringToFront();
					this.weigherProcessLogging1.Dock = DockStyle.None;
				}

			}
			else if (status == TcpComm.STATUS.READ_DATA_OK)
			{
				if (this.errorConnection1.Visible == true)
				{
					this.errorConnection1.Visible = false;
					this.weigherProcessLogging1.Dock = DockStyle.Fill;
				}
			}
		}

		private void LoadAllProductionFromDatabase()
		{
			ProductManagementDB sqlProductManagementDB = new ProductManagementDB();
			/****** Load ProductManagement ********/
			object objRet = sqlProductManagementDB.LoadAll();
			if (objRet is List<ProductManagementType>)
			{
				List<ProductManagementType> list_ProductManagement = (List<ProductManagementType>)(objRet);
				if (list_ProductManagement.Count > 0)
				{
					_configuration.list_ProductManagement = list_ProductManagement;
				}
			}
		}


		//private void AutoSynchonizeProductionfromPLC(bool IsFor_BOTTLE)
		//{
		//  ConfigurationDB sqlConfigurationDB = new ConfigurationDB();
		//  if ((IsFor_BOTTLE == true) && (_configuration.ProductCheckType == (int)(eProductCheck.BOX)))
		//  {
		//    _configuration.ProductCheckType = (int)(eProductCheck.BOTTLE);
		//    sqlConfigurationDB.Save(_configuration);
		//    //
		//    LoadAllProductionFromDatabase();
		//  }
		//  else if ((IsFor_BOTTLE == false) && (_configuration.ProductCheckType == (int)(eProductCheck.BOTTLE)))
		//  {
		//    _configuration.ProductCheckType = (int)(eProductCheck.BOX);
		//    sqlConfigurationDB.Save(_configuration);
		//    //
		//    LoadAllProductionFromDatabase();
		//  }
		//  else
		//  {
		//    /* do nothing */
		//  }
		//}


		private void LoadConfiguration()
		{
			ConfigurationDB sqlConfigurationDB = new ConfigurationDB();
			object objRet = sqlConfigurationDB.LoadAll();
			bool IsNeedCreateNewConfiguration = true;
			//


			UserDB sqlUserDB = new UserDB();
			CommunicationsDB sqlCommunicationsDB = new CommunicationsDB();
			UserGroupDB sqlUserGroupDB = new UserGroupDB();
			ProductManagementDB sqlProductManagementDB = new ProductManagementDB();

			/* Check and create new column if need */
			sqlUserDB.SetupDatabase(ConfigurationDB.eTABLE.User);
			sqlConfigurationDB.SetupDatabase(ConfigurationDB.eTABLE.Configuration);
			sqlCommunicationsDB.SetupDatabase(ConfigurationDB.eTABLE.Communications);
			sqlUserGroupDB.SetupDatabase(ConfigurationDB.eTABLE.UserGroup);
			sqlProductManagementDB.SetupDatabase(ConfigurationDB.eTABLE.ProductManagement);
			//
			if (objRet is List<ConfigurationTypes>)
			{
				List<ConfigurationTypes> list_configuration = (List<ConfigurationTypes>)(objRet);
				if (list_configuration.Count > 0)
				{
					_configuration = list_configuration[0];
					IsNeedCreateNewConfiguration = false;
				}
			}/*if (objRet is List<ConfigurationTypes>)*/
			//IsNeedCreateNewConfiguration = true;
			if (IsNeedCreateNewConfiguration == true)
			{
				_configuration = new ConfigurationTypes();
				_configuration.DatabasePath = System.IO.Path.Combine(Application.StartupPath, "Database");//String.Format ("{0}//{1}", Application.StartupPath, "Database");
				_configuration.TemplatePath = System.IO.Path.Combine(Application.StartupPath, "Template");//String.Format("{0}//{1}", Application.StartupPath, "Template");
																																																	//Create new folder
				FileUtils.CreateNewFolderIfNotExists(_configuration.DatabasePath);
				FileUtils.CreateNewFolderIfNotExists(_configuration.TemplatePath);
				//
				//Save to database
				_configuration = (ConfigurationTypes)(sqlConfigurationDB.Save(_configuration));
			}



			if (_configuration != null)
			{
				if (_configuration.ReportPath == "")
				{
					string folderparent = System.IO.Path.GetDirectoryName(Application.StartupPath);
					_configuration.ReportPath = System.IO.Path.Combine(folderparent, "Report");
					FileUtils.CreateNewFolderContainsFileIfnotExists(_configuration.ReportPath);
					//Save to database
					_configuration = (ConfigurationTypes)(sqlConfigurationDB.Save(_configuration));
				}





				/****** Load UserGroup ********/
				objRet = sqlUserGroupDB.LoadAll();
				if (objRet is List<UserGroupType>)
				{
					_configuration.list_UserGroup = (List<UserGroupType>)(objRet);
				}
				/****** Load User ********/
				objRet = sqlUserDB.LoadAll();
				if (objRet is List<UserType>)
				{
					_configuration.list_User = (List<UserType>)(objRet);
				}

				/****** Load Communications  ********/
				objRet = sqlCommunicationsDB.LoadAll();
				if (objRet is List<CommunicationsType>)
				{
					List<CommunicationsType> list_Communications = (List<CommunicationsType>)(objRet);
					if (list_Communications.Count > 0)
					{
						_configuration.Communication = list_Communications[0];
					}
				}

				/****** Load ProductManagement ********/
				LoadAllProductionFromDatabase();
				//objRet = sqlProductManagementDB.LoadAll();
				//if (objRet is List<ProductManagementType>)
				//{
				//  List<ProductManagementType> list_ProductManagement = (List<ProductManagementType>)(objRet);
				//  if (list_ProductManagement.Count > 0)
				//  {
				//    _configuration.list_ProductManagement = list_ProductManagement.FindAll(x=>x.ProductCheckType == _configuration.ProductCheckType);
				//  }
				//}
				/*********************************************************************/
				/*1. Create Group Admin if need */
				if (_configuration.list_UserGroup.Count == 0)
				{
					UserGroupType userGroup = new UserGroupType
					{
						RoleId = (int)(eUserGroupRole.Admin),
						roleEnable = 0,
						Password = "",
					};
					objRet = sqlUserGroupDB.Save(userGroup);
					if (objRet is UserGroupType)
					{
						_configuration.list_UserGroup.Add((UserGroupType)(objRet));
					}
				}

				/* 2. Check and create dfault admin: vuletech123; pass: 2709 */
				bool IsHaveSuperAdmin = _configuration.list_User.Exists(x => ("vuletech123" == x.UserName) && ("2709" == x.Password));
				if (IsHaveSuperAdmin == false)
				{
					UserType superAdmin = new UserType()
					{
						UserName = "vuletech123",
						RoleId = (int)(eUserGroupRole.Admin),
						Password = "2709"
					};
					objRet = sqlUserDB.Save(superAdmin);
					if (objRet is UserType)
					{
						_configuration.list_User.Add((UserType)(objRet));
					}
				}/*if (IsHaveSuperAdmin == false)*/

				/*3. Setup data first time*/
				bool IsFoundLastProduct = _configuration.list_ProductManagement.Exists(x => x.id == _configuration.LastProductId);
				if (IsFoundLastProduct == true)
				{
					/* do nothing */
				}
				else
				{
					if (_configuration.list_ProductManagement.Count > 0)
					{
						_configuration.LastProductId = _configuration.list_ProductManagement[0].id;
						//save it
						sqlConfigurationDB.Save(_configuration);
					}
				}

				//
				_configuration.DatabasePath = String.Format("{0}\\Database", Application.StartupPath);
				_configuration.TemplatePath = String.Format("{0}\\Template", Application.StartupPath);
			}
			int mm = 0;
		}


		private void frmMain_Load(object sender, EventArgs e)
		{
			int width = this.Width;
			int height = this.Height;
			//
			if (_configuration == null)
			{
				LoadConfiguration();
			}

			if (_configuration != null)
			{
				if (_configuration.Communication == null)
				{
					_configuration.Communication = new CommunicationsType();
					_configuration.Communication.PLC_IpAddress = "192.168.3.110"; //default
					_configuration.Communication.PortNumber = 2000; //default
																													//
					CommunicationsDB sqlCommunicationsDB = new CommunicationsDB();
					sqlCommunicationsDB.Save(_configuration.Communication);
				}
				else
				{
					if (_configuration.Communication.PLC_IpAddress != "")
					{
						plcIPAddress = _configuration.Communication.PLC_IpAddress;
					}
					//
					if (_configuration.Communication.PortNumber > 0)
					{
						portNumber = (ushort)_configuration.Communication.PortNumber;
					}
				}
				//Create user login
				if (IsDevelopComputer() == true)
				{
					_configuration.currentUserLogin = _configuration.list_User.Find(x => x.RoleId == (int)(UserType.eRole.Admin));
					//_configuration.currentUserLogin = _configuration.list_User.Find(x => x.RoleId == (int)(UserType.eRole.OP_shift_1));
				}
				else
				{
					CreateLoginOpByShift();
					//_configuration.currentUserLogin = _configuration.list_User.FindLast(x => x.RoleId == (int)(UserType.eRole.OP_shift_1));
				}
				if (_configuration.currentUserLogin == null)
				{
					_configuration.currentUserLogin = new UserType();
					_configuration.currentUserLogin.RoleId = (int)(UserType.eRole.OP_shift_1);
					_configuration.currentUserLogin.UserName = "OP_shift_1";
				}

				EnableDisableMenuShutDown();


				//Load all DataLogType from currentDate startup
				DateTime currentDatetime = Utils.GetDateTimeFromClockByShift();
				DataLogDB sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, true);
				_configuration.list_DataLogInShift = sql.LoadAllByDateTime(currentDatetime);


				//
				UpdateConfiguration();
			}
			//
			//this.splitContainer3.Panel2.Controls.Remove(this.ConvoyerDisplay1);
			this.ConvoyerDisplay1.Visible = false;
			//
			plcComm = new PLCFxCommunication(plcIPAddress, portNumber);
			plcComm.OnNotifyCommunicationStatus += PlcComm_OnNotifyCommunicationStatus;
			plcComm.OnReadDataDone += PlcComm_OnReadDataDone;
			//

			//
#if ENABLE_DEMO_MODE
      nCountDemo = _configuration.list_DataLogInShift.Count;
      timer1_demo.Enabled = true;
#else
			plcComm.StartCommunication();
			//
			this.timer_check_export_excel.Enabled = true;
#endif
		}

		private void menuShutdown_Click(object sender, EventArgs e)
		{
			if (_configuration.currentUserLogin != null)
			{
				if (_configuration.currentUserLogin.RoleId == (int)(UserType.eRole.Admin))
				{
					Application.Exit();
				}
			}

			//plcComm.ReadHoldingRegister();
		}

		private void menuSettings_Click(object sender, EventArgs e)
		{
			bool IsPemissionOK = CheckPemission(ePemission.SETTING_Cai_dat_thong_so_truyen_thong);
			if (IsPemissionOK == true)
			{
				frmSettings = new frmSettings(_configuration);
				frmSettings.OnRequestSendUpdateParameter += FrmSettings_OnRequestSendUpdateParameter;
				frmSettings.ShowDialog();
				frmSettings.BringToFront();
			}
		}



		private bool CheckPemission(ePemission ePemission, bool IsNeedShowMessage = true)
		{
			bool ret = Utils.CheckPemission(_configuration, ePemission);
			if (ret == false)
			{
				if (IsNeedShowMessage == true)
				{
					ShowMessageLogin();
				}
			}
			return ret;
		}



		private void ShowMessageLogin()
		{
			//MessageBox.Show("Bạn không được phép truy cập trang này", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			FrmWarning frmWarning = new FrmWarning("Bạn không được phép truy cập trang này !");
			frmWarning.ShowDialog();
		}

		private void CreateLoginOpByShift()
		{
			SHIFT curShift = Utils.Get_SHIFT_FromClock();
			if (curShift == SHIFT.SHIFT_1)
			{
				_configuration.currentUserLogin = _configuration.list_User.FindLast(x => x.RoleId == (int)(UserType.eRole.OP_shift_1));
			}
			else if (curShift == SHIFT.SHIFT_2)
			{
				_configuration.currentUserLogin = _configuration.list_User.FindLast(x => x.RoleId == (int)(UserType.eRole.OP_shift_2));
			}
			else
			{
				_configuration.currentUserLogin = _configuration.list_User.FindLast(x => x.RoleId == (int)(UserType.eRole.OP_shift_3));
			}
		}
		private void menuLogin_Click(object sender, EventArgs e)
		{
			if (_configuration.currentUserLogin != null)
			{
				if (_configuration.currentUserLogin.RoleId == (int)(UserType.eRole.Admin))
				{
					CreateLoginOpByShift();
					//
					this.menuLogin.Text = "Đăng nhập";
					//
					EnableDisableMenuShutDown();
				}
				else
				{
					frmLogin frmLogin = new frmLogin(_configuration);
					frmLogin.OnSendLoginOK += new frmLogin.SendLoginOK(frmLogin_OnSendLoginOK);
					frmLogin.ShowDialog();
					frmLogin.BringToFront();
				}
			}/*if (_configuration.currentUserLogin != null)*/
		}

		void frmLogin_OnSendLoginOK(object sender)
		{
			if (_configuration.currentUserLogin != null)
			{
				this.menuLogin.Text = _configuration.currentUserLogin.UserName;
				EnableDisableMenuShutDown();
			}
		}

		private void EnableDisableMenuShutDown()
		{
			if (_configuration.currentUserLogin.RoleId == (int)(UserType.eRole.Admin))
			{
				this.menuShutdown.Enabled = true;
			}
			else
			{
				this.menuShutdown.Enabled = false;
			}
		}

		private void menuProduct_Click(object sender, EventArgs e)
		{
			bool IsOK_1 = CheckPemission(ePemission.PRODUCT_Cai_dat_du_lieu_can_chinh_sua_thong_tin_da_load, false);
			bool IsOK_2 = CheckPemission(ePemission.PRODUCT_Cai_dat_du_lieu_can_import_database_tu_excel, false);
			bool IsOK = IsOK_1 && IsOK_2;
			if (IsOK == true)
			{
				frmProduct frmProduct = new frmProduct(_configuration);
				frmProduct.OnUpdateProductDone += FrmProduct_OnUpdateProductDone;
				frmProduct.OnSendImportDataFromExcelDone += FrmProduct_OnSendImportDataFromExcelDone;
				frmProduct.ShowDialog();
				frmProduct.BringToFront();
			}
			else
			{
				ShowMessageLogin();
			}
		}



		private void menuExportExcel_Click(object sender, EventArgs e)
		{
			if (CheckPemission(ePemission.REPORT_xem_va_xuat_report))
			{
				//frmReport frmExportExcel = new frmReport(_configuration);
				//frmExportExcel.ShowDialog();
				ExcelHandle.frmExportExcelByDateTime frmExportExcelByDateTime = new ExcelHandle.frmExportExcelByDateTime();
				frmExportExcelByDateTime.UpdateConfiguration(_configuration);
				frmExportExcelByDateTime.ShowDialog();
			}
		}

		private void menuManual_Click(object sender, EventArgs e)
		{
			bool IsOK_1 = CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_CHuyen_che_do_man_auto_va_chay_che_do_auto, false);
			bool IsOK_2 = CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_Disable_Buzzer, false);
			bool IsOK_3 = CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_Disable_checkweigher, false);
			bool IsOK_4 = CheckPemission(ePemission.MANUAL_CAI_DAT_MANUAL_Disable_Cylinder, false);
			bool IsOK = IsOK_1 && IsOK_2 && IsOK_3 && IsOK_4;

			//
			if (IsOK == true)
			{
				if (IsfrmManualShow == false)
				{
					IsfrmManualShow = true;
					//
					frmManual = new frmManual(_configuration);
					frmManual.FormClosed += FrmManual_FormClosed;
					frmManual.OnSendSpeedValueSetup += FrmManual_OnSendSpeedValueSetup;
					frmManual.OnSendModeChangeTo += FrmManual_OnSendModeChange;
					frmManual.OnSendRequestStartStopPB += FrmManual_OnSendRequestStartStopPB;
					frmManual.OnSendRequestDemoMode += FrmManual_OnSendRequestDemoMode;
					frmManual.OnSendBufferDataChanged += FrmManual_OnSendBufferDataChanged;
					//frmManual.OnSendRequestDemoExportExcelClicked += FrmManual_OnSendRequestDemoExportExcelClicked;
					frmManual.ShowDialog();
				}
				else
				{

				}
			}
			else
			{
				ShowMessageLogin();
			}
		}



		private void FrmManual_OnSendRequestDemoMode(object sender)
		{
			this.timer1_demo.Enabled = true;
			this.errorConnection1.Visible = false;


			eShift eCurShift = Shift.GetShiftFromClock();
			DateTime dt_now = Utils.GetDateTimeFromClockByShift();
			//
			DataLogDB sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
			List<DataLogType> list_datalog = sql.LoadAllByDateShift(dt_now, (int)(eCurShift));
			nCountDemo = list_datalog.Count;
			//Create Alarms
			//AlarmDB alarmDBsql = new AlarmDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
			//AlarmType alarm = new AlarmType()
			//{
			//  AlarmCode = 1,
			//  AlarmCode
			//};
			eErrorType error_code = (eErrorType.Error_Door_open_Front);
			string description = Utils.GetErrorDescription(error_code);
			SaveAlarm((int)error_code, description);




		}

		private void SaveAlarm(int error_code, string description)
		{
			AlarmDB alarmDBsql = new AlarmDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
			eShift eCurShift = Shift.GetShiftFromClock();
			DateTime dt_now = Utils.GetDateTimeFromClockByShift();
			AlarmType alarm = new AlarmType
			{
				AlarmCode = error_code,
				Description = description,
				DateTime = dt_now.ToString("yyyy/MM/dd HH:mm:ss"),
				ShiftId = (int)(eCurShift)
			};
			alarmDBsql.Save(alarm);
		}


		private void FrmManual_FormClosed(object sender, FormClosedEventArgs e)
		{
			IsfrmManualShow = false;
			frmManual = null;
			this.timer1_demo.Enabled = false;
		}

		private void timer_clock_Tick(object sender, EventArgs e)
		{
			timer_clock.Enabled = false;
			this.menuClockDisplay.Text = DateTime.Now.ToString("dd/MM/yyyy   HH:mm:ss"); //"                                                      dd-MM-yyyy HH:mm:ss"
			timer_clock.Enabled = true;
		}


		/// <summary>
		/// Xuất excel tự động
		/// </summary>
		private void StartToExportExcelBeforeShiftChange()
		{

			if (this.backgroundWorker1.IsBusy == false)
			{
				this.backgroundWorker1.RunWorkerAsync();
			}
		}

		private bool IsEnterDemoExportExcel = false;
		//
		private void FrmManual_OnSendRequestDemoExportExcelClicked(object sender)
		{
			IsEnterDemoExportExcel = true;
		}
		//
		private void timer_check_export_excel_Tick(object sender, EventArgs e)
		{
			this.timer_check_export_excel.Enabled = false;
			eShift eCurShift = Shift.GetShiftFromClock();
			TimeSpan timeSpanBeforeExport = new TimeSpan(0, 5, 0); //5 phút trước khi export
			DateTime dt_ShiftEnd = Shift.GetShiftEndFromClock();
			DateTime dt_StartTimeToExport = dt_ShiftEnd.Subtract(timeSpanBeforeExport);
			//
			if (IsEnterDemoExportExcel == true)
			{
				StartToExportExcelBeforeShiftChange();
				IsEnterDemoExportExcel = false;
			}
			//
			if ((eCurShift == eShift.SHIFT_1) || (eCurShift == eShift.SHIFT_2))
			{
				DateTime dt_now = DateTime.Now;
				if ((dt_now >= dt_StartTimeToExport) && (dt_now < dt_ShiftEnd))
				{
					if (IsEnableExportBeforeShiftChange == false)
					{
						IsEnableExportBeforeShiftChange = true;
						//
						StartToExportExcelBeforeShiftChange();
					}
				}
				else
				{
					IsEnableExportBeforeShiftChange = false;
				}
			}
			else if (eCurShift == eShift.SHIFT_3)
			{
				DateTime dt_now = DateTime.Now;
				string time = dt_now.ToString("HH:mm:ss");
				if ((String.Compare(time, Shift.Shift3_Start) >= 0) &&
					(String.Compare(time, Shift.Shift3_Midnight) <= 0))
				{
					/* do nothing */
				}
				else
				{
					string time_StartTimeToExport = dt_StartTimeToExport.ToString("HH:mm:ss");
					string time_dt_ShiftEnd = dt_ShiftEnd.ToString("HH:mm:ss");
					//
					if ((String.Compare(time, time_StartTimeToExport) >= 0) &&
					(String.Compare(time, time_dt_ShiftEnd) <= 0))
					{
						if (IsEnableExportBeforeShiftChange == false)
						{
							IsEnableExportBeforeShiftChange = true;
							//
							StartToExportExcelBeforeShiftChange();
						}/*if (IsEnableExportBeforeShiftChange == false)*/
					}
					else
					{
						IsEnableExportBeforeShiftChange = false;
					}
				}
			}/*else if (eCurShift == eShift.SHIFT_3)*/


			//DateTime dt_now = 
			//
			this.timer_check_export_excel.Enabled = true;
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			eShift eCurShift = Shift.GetShiftFromClock();
			DateTime datetime = Utils.GetDateTimeFromClockByShift();
			//
			DataLogDB dataLogDB_sql = new DataLogDB(_configuration.TemplatePath, _configuration.DatabasePath, false);
			List<DataLogType> _list_weigher_data_from_database = dataLogDB_sql.LoadAllByDateShift(datetime, (int)(eCurShift));

			//
			string file_name = String.Format("Report_{0}_Shift{1}", datetime.ToString("yyyyMMdd"), (int)(eCurShift));
			string folderparent = System.IO.Path.GetDirectoryName(Application.StartupPath);
			string output_file_name = $"{_configuration.ReportPath}\\{file_name}";//String.Format("{0}\\Report\\{1}", folderparent, file_name);

			bool IsFolderExists = FileUtils.CreateNewFolderContainsFileIfnotExists(output_file_name);
			if (IsFolderExists == true)
			{
				ReportExcel reportExcel = new ReportExcel(_configuration, output_file_name, _list_weigher_data_from_database);
				object ret = reportExcel.Execute();
				if (ret != null)
				{
					if (ret is bool)
					{
						//_IsExportExcel_OK = (bool)(ret);
					}
				}/*ReportExcel reportExcel = new ReportExcel(_configuration, output_file_name, _list_weigher_data_from_database);*/
			}/*if (IsFolderExists == true)*/

		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{

		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

		}

		private void btClickToViewAlarms_Click(object sender, EventArgs e)
		{
			Alarms.FrmAlarmViews frmAlarms = new Alarms.FrmAlarmViews(_configuration);
			frmAlarms.ShowDialog();
		}
		private void btClickToViewAlarm_Click(object sender, EventArgs e)
		{
			Alarms.FrmAlarmViews frmAlarms = new Alarms.FrmAlarmViews(_configuration);
			frmAlarms.ShowDialog();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			//this.Close();
		}



		protected override void WndProc(ref Message message)
		{
			if (message.Msg == SingleInstance.WM_SHOWFIRSTINSTANCE)
			{
				ShowWindow();
			}
			base.WndProc(ref message);
		}

		public void ShowWindow()
		{
			Win32Api.ShowToFront(this.Handle);
		}

		private void errorLog1_Load(object sender, EventArgs e)
		{

		}









		//

	}
}
