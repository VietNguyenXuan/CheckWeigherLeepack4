
namespace CheckWeigherUBN.Alarms
{
  partial class FrmAlarmViews
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btExit1 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btReport = new System.Windows.Forms.Button();
			this.btLoad = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.cbShift = new System.Windows.Forms.ComboBox();
			this.errorLog1 = new CheckWeigherUBN.ErrorLog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.timer_delay = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Controls.Add(this.btExit1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(952, 70);
			this.panel1.TabIndex = 32;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(791, 70);
			this.label3.TabIndex = 23;
			this.label3.Text = "               Alarm views";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.Transparent;
			this.lblStatus.Location = new System.Drawing.Point(164, 4);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(16, 15);
			this.lblStatus.TabIndex = 20;
			this.lblStatus.Text = "...";
			// 
			// btExit1
			// 
			this.btExit1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btExit1.Dock = System.Windows.Forms.DockStyle.Right;
			this.btExit1.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btExit1.FlatAppearance.BorderSize = 5;
			this.btExit1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btExit1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btExit1.ForeColor = System.Drawing.Color.White;
			this.btExit1.Image = global::CheckWeigherUBN.Properties.Resources.Shutdown_32px;
			this.btExit1.Location = new System.Drawing.Point(791, 0);
			this.btExit1.Name = "btExit1";
			this.btExit1.Size = new System.Drawing.Size(161, 70);
			this.btExit1.TabIndex = 17;
			this.btExit1.Text = "Thoát";
			this.btExit1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btExit1.UseVisualStyleBackColor = false;
			this.btExit1.Click += new System.EventHandler(this.btExit1_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btReport);
			this.panel2.Controls.Add(this.btLoad);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.dateTimePicker3);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.cbShift);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel2.Location = new System.Drawing.Point(0, 70);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(952, 98);
			this.panel2.TabIndex = 33;
			// 
			// btReport
			// 
			this.btReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btReport.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btReport.FlatAppearance.BorderSize = 5;
			this.btReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btReport.ForeColor = System.Drawing.Color.White;
			this.btReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btReport.Location = new System.Drawing.Point(656, 7);
			this.btReport.Name = "btReport";
			this.btReport.Size = new System.Drawing.Size(135, 81);
			this.btReport.TabIndex = 39;
			this.btReport.Text = "Report";
			this.btReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btReport.UseVisualStyleBackColor = false;
			this.btReport.Click += new System.EventHandler(this.btReport_Click);
			// 
			// btLoad
			// 
			this.btLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btLoad.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btLoad.FlatAppearance.BorderSize = 5;
			this.btLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btLoad.ForeColor = System.Drawing.Color.White;
			this.btLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btLoad.Location = new System.Drawing.Point(515, 7);
			this.btLoad.Name = "btLoad";
			this.btLoad.Size = new System.Drawing.Size(135, 81);
			this.btLoad.TabIndex = 38;
			this.btLoad.Text = "Load";
			this.btLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btLoad.UseVisualStyleBackColor = false;
			this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(23, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 29);
			this.label2.TabIndex = 16;
			this.label2.Text = "Shift";
			// 
			// dateTimePicker3
			// 
			this.dateTimePicker3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dateTimePicker3.Location = new System.Drawing.Point(93, 6);
			this.dateTimePicker3.Name = "dateTimePicker3";
			this.dateTimePicker3.Size = new System.Drawing.Size(389, 35);
			this.dateTimePicker3.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(23, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 29);
			this.label1.TabIndex = 9;
			this.label1.Text = "Date";
			// 
			// cbShift
			// 
			this.cbShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbShift.FormattingEnabled = true;
			this.cbShift.Items.AddRange(new object[] {
            "ALL",
            "1",
            "2",
            "3"});
			this.cbShift.Location = new System.Drawing.Point(93, 51);
			this.cbShift.Name = "cbShift";
			this.cbShift.Size = new System.Drawing.Size(389, 37);
			this.cbShift.TabIndex = 8;
			this.cbShift.Text = "ALL";
			// 
			// errorLog1
			// 
			this.errorLog1.BackColor = System.Drawing.Color.White;
			this.errorLog1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorLog1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.errorLog1.Location = new System.Drawing.Point(0, 168);
			this.errorLog1.Margin = new System.Windows.Forms.Padding(7);
			this.errorLog1.Name = "errorLog1";
			this.errorLog1.Size = new System.Drawing.Size(952, 535);
			this.errorLog1.TabIndex = 34;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Excel files|*.xlsx";
			this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// timer_delay
			// 
			this.timer_delay.Interval = 250;
			this.timer_delay.Tick += new System.EventHandler(this.timer_delay_Tick);
			// 
			// FrmAlarmViews
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(952, 703);
			this.ControlBox = false;
			this.Controls.Add(this.errorLog1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "FrmAlarmViews";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AlarmViews";
			this.Load += new System.EventHandler(this.FrmAlarmViews_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel panel2;
    private ErrorLog errorLog1;
    private System.Windows.Forms.ComboBox cbShift;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker dateTimePicker3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btExit1;
    private System.Windows.Forms.Button btLoad;
		private System.Windows.Forms.Button btReport;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Timer timer_delay;
	}
}