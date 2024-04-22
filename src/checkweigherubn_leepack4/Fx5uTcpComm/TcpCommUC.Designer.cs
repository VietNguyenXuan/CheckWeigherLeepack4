namespace TcpComm
{
  partial class TcpCommUC
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.timer_sending_cycle = new System.Windows.Forms.Timer(this.components);
      this.timer_communication = new System.Windows.Forms.Timer(this.components);
      this.timer_get_SocketTCP_data = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Image = global::TcpComm.Properties.Resources.PLC_icon_1;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(32, 31);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // timer_sending_cycle
      // 
      this.timer_sending_cycle.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // timer_communication
      // 
      this.timer_communication.Interval = 10;
      this.timer_communication.Tick += new System.EventHandler(this.timer_communication_Tick);
      // 
      // timer_get_SocketTCP_data
      // 
      this.timer_get_SocketTCP_data.Interval = 10;
      this.timer_get_SocketTCP_data.Tick += new System.EventHandler(this.timer_get_SocketTCP_data_Tick);
      // 
      // TcpCommUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pictureBox1);
      this.Name = "TcpCommUC";
      this.Size = new System.Drawing.Size(32, 31);
      this.Resize += new System.EventHandler(this.TcpCommUC_Resize);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Timer timer_sending_cycle;
    private System.Windows.Forms.Timer timer_communication;
    private System.Windows.Forms.Timer timer_get_SocketTCP_data;
  }
}
