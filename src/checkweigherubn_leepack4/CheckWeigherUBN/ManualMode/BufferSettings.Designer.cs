namespace CheckWeigherUBN.ManualMode
{
  partial class BufferSettings
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
			this.lblData = new System.Windows.Forms.Label();
			this.btSetData = new System.Windows.Forms.Button();
			this.lblUnit = new System.Windows.Forms.Label();
			this.txtBufferData = new System.Windows.Forms.TextBox();
			this.timer_delay = new System.Windows.Forms.Timer(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblData
			// 
			this.lblData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblData.BackColor = System.Drawing.Color.Teal;
			this.lblData.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblData.ForeColor = System.Drawing.Color.White;
			this.lblData.Location = new System.Drawing.Point(3, 0);
			this.lblData.Name = "lblData";
			this.lblData.Size = new System.Drawing.Size(371, 33);
			this.lblData.TabIndex = 6;
			this.lblData.Text = "Data";
			this.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btSetData
			// 
			this.btSetData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btSetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
			this.btSetData.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			this.btSetData.FlatAppearance.BorderSize = 4;
			this.btSetData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btSetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btSetData.ForeColor = System.Drawing.Color.White;
			this.btSetData.Location = new System.Drawing.Point(263, 0);
			this.btSetData.Margin = new System.Windows.Forms.Padding(0);
			this.btSetData.Name = "btSetData";
			this.btSetData.Size = new System.Drawing.Size(108, 39);
			this.btSetData.TabIndex = 9;
			this.btSetData.Text = "SET";
			this.btSetData.UseVisualStyleBackColor = false;
			this.btSetData.Click += new System.EventHandler(this.btSetData_Click);
			// 
			// lblUnit
			// 
			this.lblUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblUnit.AutoSize = true;
			this.lblUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUnit.ForeColor = System.Drawing.Color.Black;
			this.lblUnit.Location = new System.Drawing.Point(204, 0);
			this.lblUnit.Name = "lblUnit";
			this.lblUnit.Size = new System.Drawing.Size(56, 39);
			this.lblUnit.TabIndex = 8;
			this.lblUnit.Text = "ms";
			this.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtBufferData
			// 
			this.txtBufferData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBufferData.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBufferData.Location = new System.Drawing.Point(102, 3);
			this.txtBufferData.Name = "txtBufferData";
			this.txtBufferData.Size = new System.Drawing.Size(96, 35);
			this.txtBufferData.TabIndex = 0;
			this.txtBufferData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtBufferData.Click += new System.EventHandler(this.txtBufferData_Click);
			this.txtBufferData.TextChanged += new System.EventHandler(this.txtBufferData_TextChanged);
			// 
			// timer_delay
			// 
			this.timer_delay.Interval = 200;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.lblData, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.58974F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.41026F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(377, 78);
			this.tableLayoutPanel1.TabIndex = 10;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.68464F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.49326F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.71159F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.7037F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.txtBufferData, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.btSetData, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblUnit, 2, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 36);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(371, 39);
			this.tableLayoutPanel2.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 39);
			this.label1.TabIndex = 10;
			this.label1.Text = "Value";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BufferSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "BufferSettings";
			this.Size = new System.Drawing.Size(377, 78);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblData;
    private System.Windows.Forms.TextBox txtBufferData;
    private System.Windows.Forms.Label lblUnit;
    private System.Windows.Forms.Button btSetData;
    private System.Windows.Forms.Timer timer_delay;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label1;
	}
}
