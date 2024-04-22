using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckWeigherUBN.Dialogs;

namespace CheckWeigherUBN.ManualMode
{
  public partial class BufferSettings : UserControl
  {
    public delegate void SendBufferDataChanged(object sender, int buffer_id, int value);
    public event SendBufferDataChanged OnSendBufferDataChanged;
    //
    private int MAX_VALUE = 100;
    //
    private int _buffer_id = 0;
    private bool IsEnableUpdateData = true;

    private int LocationX = 0;
    private int LocationY = 0;
    private string _saveText = "";
    private bool IsKeepText = false;

    private string _title = "Data";


    public BufferSettings()
    {
      InitializeComponent();
      this.timer_delay.Interval = 250;
      this.timer_delay.Tick += timer_delay_Tick;
    }

    

    public string Title
    {
      get { return this._title; }
      set { this._title = value;
        this.lblData.Text = value;
            }
    }


    public void SetId(int buffer_id)
    {
      this._buffer_id = buffer_id;
      this.lblData.Text = String.Format("Data {0}", buffer_id);

      if (buffer_id == 1)
      {
        this.lblData.Text = $"{buffer_id}.Time reject";
        this.lblUnit.Text = "ms";
        //
        MAX_VALUE = 1000;
      }
      else if(buffer_id == 2)
      {
        this.lblData.Text = $"{buffer_id}.Thời gian kiểm tra không có túi đầu vào";
        this.lblUnit.Text = "s";
        MAX_VALUE = 60;
      }
      else if (buffer_id == 3)
      {
        this.lblData.Text = $"{buffer_id}.Delay trigger reject";
        this.lblUnit.Text = "ms";
        //
        MAX_VALUE = 1000;
      }
      else if (buffer_id == 4)
      {
        this.lblData.Text = $"{buffer_id}.Thời gian kiểm tra Overload ở đầu ra";
        this.lblUnit.Text = "s";

        MAX_VALUE = 60;
      }
      else if (buffer_id == 5)
      {
        this.lblData.Text = $"{buffer_id}.Thời gian gọi máy trước chạy";
        this.lblUnit.Text = "s";
        //
        MAX_VALUE = 60;
      }
      else if (buffer_id == 6)
      {
        this.lblData.Text = $"{buffer_id}.Thời gian gọi máy trước dừng";
        this.lblUnit.Text = "s";
        MAX_VALUE = 60;
      }
      else if (buffer_id == 7)
      {
        this.lblData.Text = $"{buffer_id}.Thời gian gọi máy sau chạy";
        this.lblUnit.Text = "s";
        MAX_VALUE = 60;
      }
      else if (buffer_id == 8)
      {
        this.lblData.Text = $"{buffer_id}.Thời gian gọi máy sau dừng";
        this.lblUnit.Text = "s";
        MAX_VALUE = 60;
      }
    }

    public void UpdateData(PLCFx5U_RawData rawdata, PLC_MachineData machineData)
    {
      if (IsEnableUpdateData == true)
      {
        if (this._buffer_id == 1)
        {
          this.txtBufferData.Text = String.Format("{0}", machineData.PC_Reject_Time_54.value.Convert_to_String());
        }
        else if (this._buffer_id == 2)
        {
          this.txtBufferData.Text = String.Format("{0}", machineData.PC_Reject_Time_Box_Conti_57.value.Convert_to_String());
        }
        else if (this._buffer_id == 3)
        {
          this.txtBufferData.Text = String.Format("{0}", machineData.PC_Delay_Reject_58.value.Convert_to_String());
        }
        else if (this._buffer_id == 4)
        {
          this.txtBufferData.Text = String.Format("{0}", machineData.PC_Reject_Number_Box_59.value.Convert_to_String());
        }
        else if (this._buffer_id == 5)
        {
          this.txtBufferData.Text = String.Format("{0}", machineData.PC_Front_Machine_Run_Time_388.value.Convert_to_String());
        }
        else if (this._buffer_id == 6)
        {
          this.txtBufferData.Text = String.Format("{0}", machineData.PC_Front_Machine_Stop_Time_389.value.Convert_to_String());
        }
        else if (this._buffer_id == 7)
        {
          this.txtBufferData.Text = String.Format("{0}", machineData.PC_Behind_Machine_Run_Time_390.value.Convert_to_String());
        }
        else if (this._buffer_id == 8)
        {
          this.txtBufferData.Text = String.Format("{0}", machineData.PC_Behind_Machine_Stop_Time_391.value.Convert_to_String());
        }

      }
    }

    private void btSetData_Click(object sender, EventArgs e)
    {
      try
      {
        int value = this.txtBufferData.Text.Convert_to_Int();// int.Parse(this.lblSpeedInput.Text);
        if ((value > MAX_VALUE) || (value < 0))
        {
          string message = String.Format("Nhập sai dữ liệu, giá trị nhập phải < {0} và > 0, vui lòng nhập lại", MAX_VALUE);
          //MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

          FrmWarning frmWarning = new FrmWarning(message);
          frmWarning.ShowDialog();
        }
        else
        {
          /* send to plc */
          if (OnSendBufferDataChanged != null)
          {
            OnSendBufferDataChanged(this, _buffer_id, value);
          }
          //
          this.timer_delay.Enabled = true;
        }
      }
      catch
      {
        MessageBox.Show("Nhập sai dữ liệu, vui lòng nhập lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				FrmWarning frmWarning = new FrmWarning("Nhập sai dữ liệu, vui lòng nhập lại");
				frmWarning.ShowDialog();
			}
    }


    private void DisplayNumericKeyboard(int locationX, int locationY)
    {
      //save Text before change
      IsKeepText = false;
      _saveText = this.txtBufferData.Text;
      //
      IsEnableUpdateData = false;
      //
      NumericKeyboard.frmKeyBoard frmKeyBoard = new NumericKeyboard.frmKeyBoard(locationX, locationY, NumericKeyboard.frmKeyBoard.eFromParent.MANUAL);
      frmKeyBoard.StartPosition = FormStartPosition.CenterScreen;
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
        this.txtBufferData.Text = _saveText;
      }

      //if ()
      //this.timer_delay.Enabled = true;
    }

    private void FrmKeyBoard_OnSendKeyPad(object sender, NumericKeyboard.DigitKeyPad.eKeyPad keyPad, NumericKeyboard.frmKeyBoard.eFromParent eFromParent)
    {
      string currentText = this.txtBufferData.Text;

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
        this.txtBufferData.Text = currentText;
      }
      catch
      {
      }
    }

    private void timer_delay_Tick(object sender, EventArgs e)
    {
      this.timer_delay.Enabled = false;
      IsEnableUpdateData = true;
    }

    private void txtBufferData_Click(object sender, EventArgs e)
    {
      IsEnableUpdateData = false;
      DisplayNumericKeyboard(LocationX, LocationY + 500);
    }

    public void UpdateLocation(int x, int y)
    {
      LocationX = x;
      LocationY = y;
    }

    private void txtBufferData_TextChanged(object sender, EventArgs e)
    {
      string value_input = this.txtBufferData.Text;
      try
      {
        this.txtBufferData.ForeColor = Color.Black;
        int value = int.Parse(value_input);
        if (value > MAX_VALUE)
        {
          this.txtBufferData.ForeColor = Color.Red;
        }

      }
      catch
      {
        this.txtBufferData.ForeColor = Color.Red;
      }
    }
  }
}
