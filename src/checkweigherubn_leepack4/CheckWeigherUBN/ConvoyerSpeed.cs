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
  public partial class ConvoyerSpeed : UserControl
  {
    private int id = 0;
    private eConvoyer _eConvoyer = eConvoyer.CONVOYER_1;

    //private Color _ProgressColor1 = Color.FromArgb(92, 92, 92);
    //private Color _ProgressColor2 = Color.FromArgb(92, 92, 92);
    //private Color _CircularColor = Color.FromArgb(0x34, 0x34, 0x34); //black


    //public int Id
    //{
    //  get { return id; }
    //  set
    //  {
    //    id = value;
    //    this.lblConvoyerTitle.Text = String.Format("Băng tải {0}", this.id);
    //    _eConvoyer = (eConvoyer)(this.id);
    //  }
    //}
    //public Color ProgressColor1
    //{
    //  get { return this.circularProgressBar1.ProgressColor1; }
    //  set
    //  {
    //    this.circularProgressBar1.ProgressColor1 = value;
    //  }
    //}

    //public Color ProgressColor2
    //{
    //  get { return this.circularProgressBar1.ProgressColor2; }
    //  set
    //  {
    //    this.circularProgressBar1.ProgressColor2 = value;
    //  }
    //}

    //public Color CircularColor
    //{
    //  get { return this.circularProgressBar1.CircularColor; }
    //  set
    //  {
    //    this.circularProgressBar1.CircularColor = value;
    //  }
    //}
    public void UpdateSpeed(int value)
    {
      this.circularProgressBar1.Value = value;
    }

    public ConvoyerSpeed()
    {
      InitializeComponent();
    }

    

    private enum eConvoyer
    {
      CONVOYER_1 = 1,
      CONVOYER_2 = 2,
      CONVOYER_3 = 3,
    }
  }
}
