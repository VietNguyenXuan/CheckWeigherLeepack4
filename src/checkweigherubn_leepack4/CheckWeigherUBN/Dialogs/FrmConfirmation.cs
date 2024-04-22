using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN.Dialogs
{
	public partial class FrmConfirmation : Form
	{
		public delegate void SendOKClicked(object sender, eConfirmationType eConfirmationType);
		public event SendOKClicked OnSendOKClicked;

    public delegate void SendOKClickedWithTag(object sender, eConfirmationType eConfirmationType, object tag);
    public event SendOKClickedWithTag OnSendOKClickedWithTag;


    private eConfirmationType _eConfirmationType = eConfirmationType.None;
		private object _tag;
		public FrmConfirmation(string title, eConfirmationType confirmationType)
		{
			InitializeComponent();
			this.label1.Text = title;
			this.label2.Text = "";
			this._eConfirmationType = confirmationType;
		}

    public FrmConfirmation(string title, string text2, eConfirmationType confirmationType, object tag)
    {
      InitializeComponent();
      this.label1.Text = title;
			this.label2.Text = text2;
      this._eConfirmationType = confirmationType;
			this._tag = tag;
    }


    private void btExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public enum eConfirmationType
		{
			None,
			Mo_cua,
			ResetCounter,
			ChangeProduct,
			ExportDataAlarms,
			Choose_to_change_Product
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			if (OnSendOKClicked != null)
			{
				OnSendOKClicked(this, this._eConfirmationType);
			}
      if (OnSendOKClickedWithTag != null)
      {
        OnSendOKClickedWithTag(this, this._eConfirmationType, this._tag);
      }
      this.Close() ;
		}
	}
}
