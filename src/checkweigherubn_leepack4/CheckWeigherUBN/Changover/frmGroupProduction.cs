using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckWeigherUBN.Changover
{
  public partial class frmGroupProduction : Form
  {
    public delegate void SendRequestChangeSku(object sender, ProductManagementType product);
    public event SendRequestChangeSku OnSendRequestChangeSku;


    private ConfigurationTypes _configuration = new ConfigurationTypes();
    private bool _is_have_sku = false;

    private Color default_color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
    private Color clicked_color = Color.Orange;
    public frmGroupProduction(ConfigurationTypes configuration)
    {
      InitializeComponent();
      this._configuration = configuration;

      _is_have_sku = false;

      //
      //ProductManagementType product = this._configuration.list_ProductManagement.Find(x => x.id == _configuration.LastProductId);
      //_is_have_sku = (product != null);

      //this.btSKU.Text = _is_have_sku? product.SKU:"Chọn SKU";

      this.btSKU.Text =  "Chọn SKU";
      //
      this.Load += FrmGroupProduction_Load;
      this.btExit.Click += BtExit_Click;
      this.btSKU.Click += BtSKU_Click;
      this.btOK.Click += BtOK_Click;
    }

    private void BtOK_Click(object sender, EventArgs e)
    {
      bool is_exit = false;
      _is_have_sku = (this.btSKU.Text != "Chọn SKU");
      if (_is_have_sku == false)
      {
        ProductManagementType product_by_sku = null;
        for (int i = 0; i < this.flowLayoutPanel1.Controls.Count && is_exit == false; i++)
        {
          if (this.flowLayoutPanel1.Controls[i] is Button)
          {
            Button localButton = (Button)(this.flowLayoutPanel1.Controls[i]);
            if (localButton != null)
            {
              if (localButton.BackColor == clicked_color)
              {
                is_exit = true;
                if (localButton.Tag != null)
                {
                  if (localButton.Tag is ProductManagementType)
                  {
                    product_by_sku = (ProductManagementType)(localButton.Tag);
                    this.btSKU.Text = product_by_sku.SKU;
                  }
                }
              }
              // localButton.BackColor = default_color;
            }
          }
        }/* for (int i = 0; i < this.flowLayoutPanel1.Controls.Count && is_exit == false; i++)*/
        //

        if (product_by_sku != null)
        {
          this.flowLayoutPanel1.Controls.Clear();
          List<ProductManagementType> products = _configuration.list_ProductManagement.FindAll(x => x.SKU == product_by_sku.SKU);
          for (int i = 0; i < products.Count; i++)
          {
            CreateButton(products[i]);
          }
        }


      }
      else
      {
        ProductManagementType product_by_Fgs = null;
        for (int i = 0; i < this.flowLayoutPanel1.Controls.Count && is_exit == false; i++)
        {
          if (this.flowLayoutPanel1.Controls[i] is Button)
          {
            Button localButton = (Button)(this.flowLayoutPanel1.Controls[i]);
            if (localButton != null)
            {
              if (localButton.BackColor == clicked_color)
              {
                is_exit = true;
                if (localButton.Tag != null)
                {
                  if (localButton.Tag is ProductManagementType)
                  {
                    product_by_Fgs = (ProductManagementType)(localButton.Tag);
                    //---------------------
                    Dialogs.FrmConfirmation frmConfirmation = new Dialogs.FrmConfirmation($"Vui lòng xác nhận bạn muốn chuyển sang sản phẩm", product_by_Fgs.Description, Dialogs.FrmConfirmation.eConfirmationType.Choose_to_change_Product, product_by_Fgs);
                    frmConfirmation.OnSendOKClickedWithTag += FrmConfirmation_OnSendOKClickedWithTag;
                    frmConfirmation.ShowDialog();
                  }
                }
              }
              // localButton.BackColor = default_color;
            }
          }
        }/* for (int i = 0; i < this.flowLayoutPanel1.Controls.Count && is_exit == false; i++)*/




       
      }
    }

    private void FrmConfirmation_OnSendOKClickedWithTag(object sender, Dialogs.FrmConfirmation.eConfirmationType eConfirmationType, object tag)
    {
      if ((OnSendRequestChangeSku != null) && (eConfirmationType == Dialogs.FrmConfirmation.eConfirmationType.Choose_to_change_Product))
      {
        if (tag != null)
        {
          if (tag is ProductManagementType)
          {
            OnSendRequestChangeSku(this, (ProductManagementType)(tag));
          }
        }
      }
      this.Close();
    }

  

    private void BtSKU_Click(object sender, EventArgs e)
    {
      DisplayAll_SKUs();
    }

    private void DisplayAll_SKUs()
    {
      //
      this.btSKU.Text = "Chọn SKU";
      //-----------------------------------------------------------
      this.flowLayoutPanel1.Controls.Clear();

			// List<ProductManagementType> products = _configuration.list_ProductManagement.GroupBy(x => x.SKU).Select(y => y.First()).ToList();
			//// List<string> production_skus = _configuration.list_ProductManagement.GroupBy(x => x.SKU).Select(x => x.Key).ToList();//remove duplcaite//
			// for (int i = 0; i < products.Count; i++)
			// {
			//   CreateButtonWithSKU(products[i]);
			// }

			//List<ProductManagementType> product_by_groupsA = _configuration.list_ProductManagement.GroupBy(x => GetFirst(x.SKU)).ToList();


			List<ProductManagementType> product_by_groups = _configuration.list_ProductManagement.GroupBy(x => GetFirst(x.SKU)).Select(y => y.First()).ToList();
			foreach (ProductManagementType product in product_by_groups)
			{
				List<ProductManagementType> products_by_sku = _configuration.list_ProductManagement.FindAll(x => GetFirst(x.SKU) == GetFirst(product.SKU));
				products_by_sku = products_by_sku.GroupBy(x => x.SKU).Select(y => y.First()).ToList();
				foreach (ProductManagementType productA in products_by_sku)
				{
					CreateButtonWithSKU(productA);
				}
				CreatePanelSeperate();
			}
		}

		private void CreatePanelSeperate()
		{
			Panel panel3 = new System.Windows.Forms.Panel();
			panel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
			panel3.Location = new System.Drawing.Point(3, 3);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(1225, 8);
			panel3.TabIndex = 0;
      //
      panel3.BackColor = Color.CadetBlue;
			//
			flowLayoutPanel1.Controls.Add(panel3);
		}

		private string GetFirst(string description)
		{
			bool is_exit = false;
			string ret = "";
			for (int i = 0; i < description.Length && (is_exit == false); i++)
			{
				if (description[i] == ' ')
				{
					is_exit = true;
				}
				else
				{
					ret += description[i];
				}
			}
			return ret;
		}

		private void BtExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void FrmGroupProduction_Load(object sender, EventArgs e)
    {
      DisplayAll_SKUs();
    }
    

    private void CreateButton(ProductManagementType product)
    {
      Button button3 = new System.Windows.Forms.Button();
      button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
      button3.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
      button3.FlatAppearance.BorderSize = 5;
      button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
      button3.ForeColor = System.Drawing.Color.White;
      button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      button3.Location = new System.Drawing.Point(3, 3);
      button3.Name = product.FGs;
      button3.Size = new System.Drawing.Size(180, 110);
      button3.TabIndex = 16;
    
      button3.Text = $"{product.Description}\r\n{product.FGs}";
      button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      button3.UseVisualStyleBackColor = false;
      button3.Tag = product;
      button3.Click += Button3_Click;

      this.flowLayoutPanel1.Controls.Add(button3);
    }

   

    //private class 
    private void CreateButtonWithSKU(ProductManagementType product)
    {
      Button button = new System.Windows.Forms.Button();
      button.BackColor = default_color;
      button.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
      button.FlatAppearance.BorderSize = 5;
      button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular);
      button.ForeColor = System.Drawing.Color.White;
      button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      button.Location = new System.Drawing.Point(3, 3);
      button.Name = product.SKU;
      button.Size = new System.Drawing.Size(150, 75);
      button.TabIndex = 16;

      button.Text = product.SKU;
      button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      button.UseVisualStyleBackColor = false;

      button.Tag = product;
      button.Click += Button_Click;
      this.flowLayoutPanel1.Controls.Add(button);
    }

    private void ActiveColor(object sender)
    {
      if (sender is Button)
      {
        Button button = (Button)sender;
        for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
        {
          if (this.flowLayoutPanel1.Controls[i] is Button)
          {
            Button localButton = (Button)(this.flowLayoutPanel1.Controls[i]);
            if (localButton != null)
            {
              localButton.BackColor = default_color;
            }
          }
        }

        //------------------------------------
        if (button.Tag != null)
        {
          button.BackColor = Color.Orange;
        }
      }

      //btOK.PerformClick();
      BtOK();

		}
    private void Button_Click(object sender, EventArgs e)
    {
      ActiveColor(sender);
    }

    private void Button3_Click(object sender, EventArgs e)
    {
      ActiveColor(sender);
    }

		private void btBack_Click(object sender, EventArgs e)
		{
			DisplayAll_SKUs();
		}



		private void BtOK()
		{
			bool is_exit = false;
			_is_have_sku = (this.btSKU.Text != "Chọn SKU");
			if (_is_have_sku == false)
			{
				ProductManagementType product_by_sku = null;
				for (int i = 0; i < this.flowLayoutPanel1.Controls.Count && is_exit == false; i++)
				{
					if (this.flowLayoutPanel1.Controls[i] is Button)
					{
						Button localButton = (Button)(this.flowLayoutPanel1.Controls[i]);
						if (localButton != null)
						{
							if (localButton.BackColor == clicked_color)
							{
								is_exit = true;
								if (localButton.Tag != null)
								{
									if (localButton.Tag is ProductManagementType)
									{
										product_by_sku = (ProductManagementType)(localButton.Tag);
										this.btSKU.Text = product_by_sku.SKU;
									}
								}
							}
							// localButton.BackColor = default_color;
						}
					}
				}/* for (int i = 0; i < this.flowLayoutPanel1.Controls.Count && is_exit == false; i++)*/
				//

				if (product_by_sku != null)
				{
					this.flowLayoutPanel1.Controls.Clear();
					List<ProductManagementType> products = _configuration.list_ProductManagement.FindAll(x => x.SKU == product_by_sku.SKU);
					for (int i = 0; i < products.Count; i++)
					{
						CreateButton(products[i]);
					}
				}


			}
			else
			{
				ProductManagementType product_by_Fgs = null;
				for (int i = 0; i < this.flowLayoutPanel1.Controls.Count && is_exit == false; i++)
				{
					if (this.flowLayoutPanel1.Controls[i] is Button)
					{
						Button localButton = (Button)(this.flowLayoutPanel1.Controls[i]);
						if (localButton != null)
						{
							if (localButton.BackColor == clicked_color)
							{
								is_exit = true;
								if (localButton.Tag != null)
								{
									if (localButton.Tag is ProductManagementType)
									{
										product_by_Fgs = (ProductManagementType)(localButton.Tag);
										//---------------------
										Dialogs.FrmConfirmation frmConfirmation = new Dialogs.FrmConfirmation($"Vui lòng xác nhận bạn muốn chuyển sang sản phẩm", product_by_Fgs.Description, Dialogs.FrmConfirmation.eConfirmationType.Choose_to_change_Product, product_by_Fgs);
										frmConfirmation.OnSendOKClickedWithTag += FrmConfirmation_OnSendOKClickedWithTag;
										frmConfirmation.ShowDialog();
									}
								}
							}
							// localButton.BackColor = default_color;
						}
					}
				}/* for (int i = 0; i < this.flowLayoutPanel1.Controls.Count && is_exit == false; i++)*/

			}
		}

















	}
}
