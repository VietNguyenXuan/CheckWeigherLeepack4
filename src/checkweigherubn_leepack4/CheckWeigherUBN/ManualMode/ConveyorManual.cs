
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckWeigherUBN.Dialogs;

namespace CheckWeigherUBN
{
  public partial class ConveyorManual : UserControl
  {
    public delegate void SendSpeedValueSetup(object sender, eDeviceType eConveyor, int value);
    public event SendSpeedValueSetup OnSendSpeedValueSetup;
    //
    public delegate void SendRequestStartStopPB(object sender, eDeviceType eConveyor, eConveyorStatus conveyorStatusRequest);
    public event SendRequestStartStopPB OnSendRequestStartStopPB;

    //
    private eDeviceType _eConveyor = eDeviceType.BT_CAN;
    private eConveyorStatus _eCurrentConveyorStatus = eConveyorStatus.RUN;
    private bool IsEnableUpdateConveyorStatus = true;
    //
    private int LocationX = 0;
    private int LocationY = 0;
    private string _saveText = "";
    private bool IsKeepText = false;

    //
    public ConveyorManual()
    {
      InitializeComponent();
    }

    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      bool IsRUN = false;
      int machineStatus = machineData.PLC_ControlStatus.value.Convert_to_Int();
      if (_eConveyor == eDeviceType.BT_ALL)
      {
          this.conveyorSpeed.Value = machineData.PLC_Conveyor_Auto_Speed.value.Convert_to_Int();
          if (conveyorSpeed.Value < 40)
          {
              conveyorSpeed.ProgressColor1 = Color.OliveDrab;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          if (conveyorSpeed.Value >= 40 && conveyorSpeed.Value < 84)
          {
              conveyorSpeed.ProgressColor1 = Color.Gold;
              conveyorSpeed.ProgressColor2= Color.White;
          }
          if (conveyorSpeed.Value >= 84)
          {
              conveyorSpeed.ProgressColor1 = Color.Red;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          IsRUN = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Machine_Run);
      }
      else if (_eConveyor == eDeviceType.BT_TACH_CHAI)
      {
          this.conveyorSpeed.Value = machineData.PC_Btai_Vao_Speed.value.Convert_to_Int();
          if (conveyorSpeed.Value < 40)
          {
              conveyorSpeed.ProgressColor1 = Color.OliveDrab;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          if (conveyorSpeed.Value >= 40 && conveyorSpeed.Value < 84)
          {
              conveyorSpeed.ProgressColor1 = Color.Gold;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          if (conveyorSpeed.Value >= 84)
          {
              conveyorSpeed.ProgressColor1 = Color.Red;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          IsRUN = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Btai_Vao_Run);
      }
      else if (_eConveyor == eDeviceType.BT_CAN)
      {
          this.conveyorSpeed.Value = machineData.PC_Btai_Can_Speed.value.Convert_to_Int();
          if (conveyorSpeed.Value < 40)
          {
              conveyorSpeed.ProgressColor1 = Color.OliveDrab;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          if (conveyorSpeed.Value >= 40 && conveyorSpeed.Value < 84)
          {
              conveyorSpeed.ProgressColor1 = Color.Gold;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          if (conveyorSpeed.Value >= 84)
          {
              conveyorSpeed.ProgressColor1 = Color.Red;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          IsRUN = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Btai_Can_Run);
      }
      else if (_eConveyor == eDeviceType.BT_REJECT)
      {
          this.conveyorSpeed.Value = machineData.PC_Btai_Ra_Speed.value.Convert_to_Int();
          if (conveyorSpeed.Value < 40)
          {
              conveyorSpeed.ProgressColor1 = Color.OliveDrab;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          if (conveyorSpeed.Value >= 40 && conveyorSpeed.Value < 84)
          {
              conveyorSpeed.ProgressColor1 = Color.Gold;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          if (conveyorSpeed.Value >= 84)
          {
              conveyorSpeed.ProgressColor1 = Color.Red;
              conveyorSpeed.ProgressColor2 = Color.White;
          }
          IsRUN = machineStatus.ToBoolean((int)ePLC_ControlStatus.PLC_Btai_Ra_Run);
      }
      if (IsEnableUpdateConveyorStatus == true)
      {
        _eCurrentConveyorStatus = IsRUN.ConvertConveyorStatus();
        if (_eCurrentConveyorStatus == eConveyorStatus.RUN)
        {
          this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
          this.lblStatus.ForeColor = System.Drawing.Color.White;
          this.lblStatus.Text = "RUN";
          //
          this.btSTART.Image = global::CheckWeigherUBN.Properties.Resources.STOP_Button_1_ON;
        }
        else
        {
          this.lblStatus.BackColor = System.Drawing.Color.Red;
          this.lblStatus.ForeColor = System.Drawing.Color.White;
          this.lblStatus.Text = "STOP";
          this.btSTART.Image = global::CheckWeigherUBN.Properties.Resources.START_Button_1_ON;
        }
      }
    }

    public void SetConveyor(eDeviceType eConveyor)
    {
      _eConveyor = eConveyor;
      if (_eConveyor == eDeviceType.BT_ALL)
      {
        this.lblTitle.Text = String.Format("Hệ thống");
        //this.Size.Width = 145;//145, 145
        //this.Size.Height = 145;
      }
      else if (_eConveyor == eDeviceType.BT_TACH_CHAI)
      {
        this.lblTitle.Text = String.Format("Băng tải tách chai");
      }
      else if (_eConveyor == eDeviceType.BT_CAN)
      {
        this.lblTitle.Text = String.Format("Băng tải cân");
      }
      else if (_eConveyor == eDeviceType.BT_REJECT)
      {
        this.lblTitle.Text = String.Format("Băng tải loại chai");
      }
    }
   

    private void timer_STOP_Button_Tick(object sender, EventArgs e)
    {
      this.timer_STOP_Button.Enabled = false;
      //this.pictureBox_STOP.Image = global::CheckWeigherUBN.Properties.Resources.STOP_Button_1_OFF;
    }

    
    private void lblSpeedInput_Click(object sender, EventArgs e)
    {
      Utils utils = new Utils();
      //
      if (_eConveyor == eDeviceType.BT_TACH_CHAI)
      {
        DisplayNumericKeyboard(LocationX + 245, LocationY + 90);
      }
      else if (_eConveyor == eDeviceType.BT_CAN)
      {
        DisplayNumericKeyboard(LocationX + 245, LocationY + 90);
      }
      else if (_eConveyor == eDeviceType.BT_REJECT)
      {
        DisplayNumericKeyboard(LocationX - 200, LocationY + 90);
      }
			else if (_eConveyor == eDeviceType.BT_ALL)
			{
				DisplayNumericKeyboard(LocationX + 300, LocationY + 90);
			}
		}

    private void DisplayNumericKeyboard(int locationX, int locationY)
    {
      //save Text before change
      IsKeepText = false;
      _saveText = this.lblSpeedInput.Text;
      //
      NumericKeyboard.frmKeyBoard frmKeyBoard = new NumericKeyboard.frmKeyBoard(locationX, locationY, NumericKeyboard.frmKeyBoard.eFromParent.MANUAL);
      frmKeyBoard.OnSendKeyPad += FrmKeyBoard_OnSendKeyPad;
      frmKeyBoard.FormClosed += FrmKeyBoard_FormClosed;
      frmKeyBoard.ShowDialog();
    }
    private void FrmKeyBoard_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (IsKeepText == true)
      {
        /* do nothing */
      }
      else
      {
        this.lblSpeedInput.Text = _saveText;
      }
    }

    private void FrmKeyBoard_OnSendKeyPad(object sender, NumericKeyboard.DigitKeyPad.eKeyPad keyPad, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      string currentText = this.lblSpeedInput.Text;
      
      try
      {
        if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_0) currentText += "0";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_1) currentText += "1";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_2) currentText += "2";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_3) currentText += "3";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_4) currentText += "4";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_5) currentText += "5";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_6) currentText += "6";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_7) currentText += "7";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_8) currentText += "8";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.KEY_9) currentText += "9";
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.BACKSPACE)
        {
          if (currentText.Length > 0)
          {
            currentText = currentText.Remove(currentText.Length - 1);
          }
        }
        else if (keyPad == NumericKeyboard.DigitKeyPad.eKeyPad.ENTER)
        {
          IsKeepText = true;
        }
        //
        this.lblSpeedInput.Text = currentText;
      }
      catch
      {
      }
      //
      
    }

    public void UpdateLocation(int x, int y)
    {
      LocationX = x;
      LocationY = y;
    }

    private void btSetValue_Click(object sender, EventArgs e)
    {
      try
      {
        int value = int.Parse(this.lblSpeedInput.Text);
        if ((value > 100) || (value < 0))
        {
          //MessageBox.Show("Giá trị nhập phải < 100, vui lòng nhập lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          FrmWarning frmWarning = new FrmWarning("Giá trị nhập phải < 100, vui lòng nhập lại");
          frmWarning.ShowDialog();
        }
        else 
        {
          /* send to plc */
          if (OnSendSpeedValueSetup != null)
          {
            OnSendSpeedValueSetup(this, _eConveyor, value);
          }
        }
      }
      catch
      {
        //MessageBox.Show("Nhập sai dữ liệu, vui lòng nhập lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				FrmWarning frmWarning = new FrmWarning("Nhập sai dữ liệu, vui lòng nhập lại !");
				frmWarning.ShowDialog();
			}
    }

    

    private void lblSpeedInput_TextChanged(object sender, EventArgs e)
    {
      string value_input = this.lblSpeedInput.Text;
      try
      {
        this.lblSpeedInput.ForeColor = Color.Black;
        int value = int.Parse(value_input);//.Convert_to_Int();
        if (value > 100)
        {
          this.lblSpeedInput.ForeColor = Color.Red;
        }
        
      }
      catch
      {
        this.lblSpeedInput.ForeColor = Color.Red;
      }
    }

    private void btSTART_Click(object sender, EventArgs e)
    {

      if (OnSendRequestStartStopPB != null)
      {
        if (_eCurrentConveyorStatus == eConveyorStatus.RUN)
        {
          OnSendRequestStartStopPB(this, _eConveyor, eConveyorStatus.STOP);
        }
        else if (_eCurrentConveyorStatus == eConveyorStatus.STOP)
        {
          OnSendRequestStartStopPB(this, _eConveyor, eConveyorStatus.RUN);
        }
      }
    }

    private void timer_delay_Tick(object sender, EventArgs e)
    {
      timer_delay.Enabled = false;
      IsEnableUpdateConveyorStatus = true;
    }
  }
}
