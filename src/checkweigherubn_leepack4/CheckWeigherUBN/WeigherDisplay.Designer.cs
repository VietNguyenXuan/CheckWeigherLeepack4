namespace CheckWeigherUBN
{
  partial class WeigherDisplay
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeigherDisplay));
			this.lblWeigherStatus = new System.Windows.Forms.Label();
			this.aquaGauge1 = new AquaControls.AquaGauge();
			this.grouper2 = new CodeVendor.Controls.Grouper();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblActualG_Kg = new System.Windows.Forms.Label();
			this.lblGrossWeight = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.grouper3 = new CodeVendor.Controls.Grouper();
			this.label1 = new System.Windows.Forms.Label();
			this.lblDiff = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.grouper5 = new CodeVendor.Controls.Grouper();
			this.label6 = new System.Windows.Forms.Label();
			this.lblMaxValue = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblMinValue = new System.Windows.Forms.Label();
			this.lblMin = new System.Windows.Forms.Label();
			this.lblTarget_Kg_g = new System.Windows.Forms.Label();
			this.lblTarget = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.grouper1 = new CodeVendor.Controls.Grouper();
			this.label5 = new System.Windows.Forms.Label();
			this.lblg_kg = new System.Windows.Forms.Label();
			this.lblNet_Weight = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.machineStatus1 = new CheckWeigherUBN.MachineStatus();
			this.grouper2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.grouper3.SuspendLayout();
			this.grouper5.SuspendLayout();
			this.grouper1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblWeigherStatus
			// 
			this.lblWeigherStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.lblWeigherStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWeigherStatus.ForeColor = System.Drawing.Color.White;
			this.lblWeigherStatus.Location = new System.Drawing.Point(9, 9);
			this.lblWeigherStatus.Name = "lblWeigherStatus";
			this.lblWeigherStatus.Size = new System.Drawing.Size(173, 58);
			this.lblWeigherStatus.TabIndex = 69;
			this.lblWeigherStatus.Text = "PASS";
			this.lblWeigherStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// aquaGauge1
			// 
			this.aquaGauge1.BackColor = System.Drawing.Color.Transparent;
			this.aquaGauge1.DialColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.aquaGauge1.DialText = null;
			this.aquaGauge1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.aquaGauge1.Glossiness = 11.36364F;
			this.aquaGauge1.Location = new System.Drawing.Point(0, 71);
			this.aquaGauge1.Margin = new System.Windows.Forms.Padding(4);
			this.aquaGauge1.MaxValue = 20F;
			this.aquaGauge1.MinValue = 0F;
			this.aquaGauge1.Name = "aquaGauge1";
			this.aquaGauge1.RecommendedValue = 0F;
			this.aquaGauge1.Size = new System.Drawing.Size(182, 182);
			this.aquaGauge1.TabIndex = 63;
			this.aquaGauge1.ThresholdPercent = 1F;
			this.aquaGauge1.Value = 0F;
			// 
			// grouper2
			// 
			this.grouper2.BackgroundColor = System.Drawing.Color.White;
			this.grouper2.BackgroundGradientColor = System.Drawing.Color.White;
			this.grouper2.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
			this.grouper2.BorderColor = System.Drawing.Color.Black;
			this.grouper2.BorderThickness = 1F;
			this.grouper2.Controls.Add(this.pictureBox1);
			this.grouper2.Controls.Add(this.lblActualG_Kg);
			this.grouper2.Controls.Add(this.lblGrossWeight);
			this.grouper2.Controls.Add(this.label4);
			this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
			this.grouper2.GroupImage = null;
			this.grouper2.GroupTitle = "";
			this.grouper2.Location = new System.Drawing.Point(189, -3);
			this.grouper2.Name = "grouper2";
			this.grouper2.Padding = new System.Windows.Forms.Padding(20);
			this.grouper2.PaintGroupBox = false;
			this.grouper2.RoundCorners = 10;
			this.grouper2.ShadowColor = System.Drawing.SystemColors.Desktop;
			this.grouper2.ShadowControl = true;
			this.grouper2.ShadowThickness = 3;
			this.grouper2.Size = new System.Drawing.Size(253, 123);
			this.grouper2.TabIndex = 70;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(10, 52);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(190, 4);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// lblActualG_Kg
			// 
			this.lblActualG_Kg.AutoSize = true;
			this.lblActualG_Kg.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblActualG_Kg.Location = new System.Drawing.Point(224, 75);
			this.lblActualG_Kg.Name = "lblActualG_Kg";
			this.lblActualG_Kg.Size = new System.Drawing.Size(31, 33);
			this.lblActualG_Kg.TabIndex = 6;
			this.lblActualG_Kg.Text = "g";
			// 
			// lblGrossWeight
			// 
			this.lblGrossWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGrossWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.lblGrossWeight.Location = new System.Drawing.Point(22, 60);
			this.lblGrossWeight.Name = "lblGrossWeight";
			this.lblGrossWeight.Size = new System.Drawing.Size(186, 56);
			this.lblGrossWeight.TabIndex = 4;
			this.lblGrossWeight.Text = "0";
			this.lblGrossWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(0, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(212, 37);
			this.label4.TabIndex = 2;
			this.label4.Text = "Gross Weight";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// grouper3
			// 
			this.grouper3.BackgroundColor = System.Drawing.Color.White;
			this.grouper3.BackgroundGradientColor = System.Drawing.Color.White;
			this.grouper3.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
			this.grouper3.BorderColor = System.Drawing.Color.Black;
			this.grouper3.BorderThickness = 1F;
			this.grouper3.Controls.Add(this.label1);
			this.grouper3.Controls.Add(this.lblDiff);
			this.grouper3.Controls.Add(this.label3);
			this.grouper3.CustomGroupBoxColor = System.Drawing.Color.White;
			this.grouper3.GroupImage = null;
			this.grouper3.GroupTitle = "";
			this.grouper3.Location = new System.Drawing.Point(189, 113);
			this.grouper3.Name = "grouper3";
			this.grouper3.Padding = new System.Windows.Forms.Padding(20);
			this.grouper3.PaintGroupBox = false;
			this.grouper3.RoundCorners = 10;
			this.grouper3.ShadowColor = System.Drawing.SystemColors.Desktop;
			this.grouper3.ShadowControl = true;
			this.grouper3.ShadowThickness = 3;
			this.grouper3.Size = new System.Drawing.Size(253, 72);
			this.grouper3.TabIndex = 67;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(224, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 33);
			this.label1.TabIndex = 19;
			this.label1.Text = "g";
			// 
			// lblDiff
			// 
			this.lblDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDiff.ForeColor = System.Drawing.Color.Red;
			this.lblDiff.Location = new System.Drawing.Point(79, 23);
			this.lblDiff.Name = "lblDiff";
			this.lblDiff.Size = new System.Drawing.Size(145, 31);
			this.lblDiff.TabIndex = 17;
			this.lblDiff.Text = "0";
			this.lblDiff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(-1, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 37);
			this.label3.TabIndex = 16;
			this.label3.Text = "Diff:";
			// 
			// grouper5
			// 
			this.grouper5.BackgroundColor = System.Drawing.Color.White;
			this.grouper5.BackgroundGradientColor = System.Drawing.Color.White;
			this.grouper5.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
			this.grouper5.BorderColor = System.Drawing.Color.Black;
			this.grouper5.BorderThickness = 1F;
			this.grouper5.Controls.Add(this.label6);
			this.grouper5.Controls.Add(this.lblMaxValue);
			this.grouper5.Controls.Add(this.label8);
			this.grouper5.Controls.Add(this.label2);
			this.grouper5.Controls.Add(this.lblMinValue);
			this.grouper5.Controls.Add(this.lblMin);
			this.grouper5.Controls.Add(this.lblTarget_Kg_g);
			this.grouper5.Controls.Add(this.lblTarget);
			this.grouper5.Controls.Add(this.label27);
			this.grouper5.CustomGroupBoxColor = System.Drawing.Color.White;
			this.grouper5.GroupImage = null;
			this.grouper5.GroupTitle = "";
			this.grouper5.Location = new System.Drawing.Point(189, 179);
			this.grouper5.Name = "grouper5";
			this.grouper5.Padding = new System.Windows.Forms.Padding(20);
			this.grouper5.PaintGroupBox = false;
			this.grouper5.RoundCorners = 10;
			this.grouper5.ShadowColor = System.Drawing.SystemColors.Desktop;
			this.grouper5.ShadowControl = true;
			this.grouper5.ShadowThickness = 3;
			this.grouper5.Size = new System.Drawing.Size(253, 157);
			this.grouper5.TabIndex = 65;
			this.grouper5.Load += new System.EventHandler(this.grouper5_Load);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Black;
			this.label6.Location = new System.Drawing.Point(224, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(31, 33);
			this.label6.TabIndex = 24;
			this.label6.Text = "g";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMaxValue
			// 
			this.lblMaxValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMaxValue.ForeColor = System.Drawing.Color.Maroon;
			this.lblMaxValue.Location = new System.Drawing.Point(112, 113);
			this.lblMaxValue.Name = "lblMaxValue";
			this.lblMaxValue.Size = new System.Drawing.Size(112, 31);
			this.lblMaxValue.TabIndex = 23;
			this.lblMaxValue.Text = "0";
			this.lblMaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(2, 113);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(85, 37);
			this.label8.TabIndex = 22;
			this.label8.Text = "Max:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(224, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 33);
			this.label2.TabIndex = 21;
			this.label2.Text = "g";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMinValue
			// 
			this.lblMinValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMinValue.ForeColor = System.Drawing.Color.DodgerBlue;
			this.lblMinValue.Location = new System.Drawing.Point(70, 21);
			this.lblMinValue.Name = "lblMinValue";
			this.lblMinValue.Size = new System.Drawing.Size(154, 31);
			this.lblMinValue.TabIndex = 20;
			this.lblMinValue.Text = "0";
			this.lblMinValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMin
			// 
			this.lblMin.AutoSize = true;
			this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMin.Location = new System.Drawing.Point(2, 21);
			this.lblMin.Name = "lblMin";
			this.lblMin.Size = new System.Drawing.Size(77, 37);
			this.lblMin.TabIndex = 19;
			this.lblMin.Text = "Min:";
			this.lblMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTarget_Kg_g
			// 
			this.lblTarget_Kg_g.AutoSize = true;
			this.lblTarget_Kg_g.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTarget_Kg_g.Location = new System.Drawing.Point(224, 66);
			this.lblTarget_Kg_g.Name = "lblTarget_Kg_g";
			this.lblTarget_Kg_g.Size = new System.Drawing.Size(31, 33);
			this.lblTarget_Kg_g.TabIndex = 18;
			this.lblTarget_Kg_g.Text = "g";
			this.lblTarget_Kg_g.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblTarget
			// 
			this.lblTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTarget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.lblTarget.Location = new System.Drawing.Point(109, 68);
			this.lblTarget.Name = "lblTarget";
			this.lblTarget.Size = new System.Drawing.Size(115, 31);
			this.lblTarget.TabIndex = 17;
			this.lblTarget.Text = "0";
			this.lblTarget.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label27.Location = new System.Drawing.Point(2, 67);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(119, 37);
			this.label27.TabIndex = 16;
			this.label27.Text = "Target:";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grouper1
			// 
			this.grouper1.BackgroundColor = System.Drawing.Color.White;
			this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
			this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
			this.grouper1.BorderColor = System.Drawing.Color.Black;
			this.grouper1.BorderThickness = 1F;
			this.grouper1.Controls.Add(this.label5);
			this.grouper1.Controls.Add(this.lblg_kg);
			this.grouper1.Controls.Add(this.lblNet_Weight);
			this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
			this.grouper1.GroupImage = null;
			this.grouper1.GroupTitle = "";
			this.grouper1.Location = new System.Drawing.Point(9, 251);
			this.grouper1.Name = "grouper1";
			this.grouper1.Padding = new System.Windows.Forms.Padding(20);
			this.grouper1.PaintGroupBox = false;
			this.grouper1.RoundCorners = 10;
			this.grouper1.ShadowColor = System.Drawing.SystemColors.Desktop;
			this.grouper1.ShadowControl = true;
			this.grouper1.ShadowThickness = 3;
			this.grouper1.Size = new System.Drawing.Size(173, 79);
			this.grouper1.TabIndex = 64;
			this.grouper1.Visible = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(10, 18);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(69, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "Net weight";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblg_kg
			// 
			this.lblg_kg.AutoSize = true;
			this.lblg_kg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblg_kg.Location = new System.Drawing.Point(149, 48);
			this.lblg_kg.Name = "lblg_kg";
			this.lblg_kg.Size = new System.Drawing.Size(19, 20);
			this.lblg_kg.TabIndex = 6;
			this.lblg_kg.Text = "g";
			this.lblg_kg.Visible = false;
			// 
			// lblNet_Weight
			// 
			this.lblNet_Weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNet_Weight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.lblNet_Weight.Location = new System.Drawing.Point(7, 34);
			this.lblNet_Weight.Name = "lblNet_Weight";
			this.lblNet_Weight.Size = new System.Drawing.Size(143, 45);
			this.lblNet_Weight.TabIndex = 4;
			this.lblNet_Weight.Text = "0";
			this.lblNet_Weight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblNet_Weight.Visible = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// machineStatus1
			// 
			this.machineStatus1.BackColor = System.Drawing.Color.White;
			this.machineStatus1.Location = new System.Drawing.Point(189, 338);
			this.machineStatus1.Margin = new System.Windows.Forms.Padding(4);
			this.machineStatus1.Name = "machineStatus1";
			this.machineStatus1.Size = new System.Drawing.Size(253, 35);
			this.machineStatus1.TabIndex = 68;
			// 
			// WeigherDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.aquaGauge1);
			this.Controls.Add(this.grouper2);
			this.Controls.Add(this.grouper3);
			this.Controls.Add(this.lblWeigherStatus);
			this.Controls.Add(this.machineStatus1);
			this.Controls.Add(this.grouper5);
			this.Controls.Add(this.grouper1);
			this.Name = "WeigherDisplay";
			this.Size = new System.Drawing.Size(445, 390);
			this.grouper2.ResumeLayout(false);
			this.grouper2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.grouper3.ResumeLayout(false);
			this.grouper3.PerformLayout();
			this.grouper5.ResumeLayout(false);
			this.grouper5.PerformLayout();
			this.grouper1.ResumeLayout(false);
			this.grouper1.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion
    private CodeVendor.Controls.Grouper grouper5;
    private CodeVendor.Controls.Grouper grouper1;
    private System.Windows.Forms.Label lblg_kg;
    private System.Windows.Forms.Label lblNet_Weight;
    private AquaControls.AquaGauge aquaGauge1;
    private MachineStatus machineStatus1;
    private System.Windows.Forms.Label lblWeigherStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblTarget_Kg_g;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblDiff;
        private System.Windows.Forms.Label label3;
        private CodeVendor.Controls.Grouper grouper3;
        private System.Windows.Forms.Label label1;
        private CodeVendor.Controls.Grouper grouper2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblActualG_Kg;
        private System.Windows.Forms.Label lblGrossWeight;
        private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	}
}
