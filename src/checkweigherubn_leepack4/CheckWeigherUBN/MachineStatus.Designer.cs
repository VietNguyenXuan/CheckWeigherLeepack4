namespace CheckWeigherUBN
{
  partial class MachineStatus
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
			this.lblRUN = new System.Windows.Forms.Label();
			this.lblSTOP = new System.Windows.Forms.Label();
			this.lblALARM = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblRUN
			// 
			this.lblRUN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblRUN.BackColor = System.Drawing.Color.LightGray;
			this.lblRUN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRUN.ForeColor = System.Drawing.Color.DimGray;
			this.lblRUN.Location = new System.Drawing.Point(0, 0);
			this.lblRUN.Margin = new System.Windows.Forms.Padding(0);
			this.lblRUN.Name = "lblRUN";
			this.lblRUN.Size = new System.Drawing.Size(130, 40);
			this.lblRUN.TabIndex = 0;
			this.lblRUN.Text = "RUN";
			this.lblRUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSTOP
			// 
			this.lblSTOP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSTOP.BackColor = System.Drawing.Color.LightGray;
			this.lblSTOP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSTOP.ForeColor = System.Drawing.Color.DimGray;
			this.lblSTOP.Location = new System.Drawing.Point(130, 0);
			this.lblSTOP.Margin = new System.Windows.Forms.Padding(0);
			this.lblSTOP.Name = "lblSTOP";
			this.lblSTOP.Size = new System.Drawing.Size(130, 40);
			this.lblSTOP.TabIndex = 1;
			this.lblSTOP.Text = "STOP";
			this.lblSTOP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblALARM
			// 
			this.lblALARM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblALARM.BackColor = System.Drawing.Color.LightGray;
			this.lblALARM.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblALARM.ForeColor = System.Drawing.Color.Gray;
			this.lblALARM.Location = new System.Drawing.Point(260, 0);
			this.lblALARM.Margin = new System.Windows.Forms.Padding(0);
			this.lblALARM.Name = "lblALARM";
			this.lblALARM.Size = new System.Drawing.Size(131, 40);
			this.lblALARM.TabIndex = 2;
			this.lblALARM.Text = "ALARM";
			this.lblALARM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Controls.Add(this.lblRUN, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblALARM, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblSTOP, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(391, 40);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// MachineStatus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "MachineStatus";
			this.Size = new System.Drawing.Size(391, 40);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblRUN;
    private System.Windows.Forms.Label lblSTOP;
    private System.Windows.Forms.Label lblALARM;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}
