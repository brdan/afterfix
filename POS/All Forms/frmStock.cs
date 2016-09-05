using POS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class frmStock : Form
    {
        /*
        Lbl title index
        0 - Goes to dashboard
        1 - Goes to stock
        2 - Goes to categories
        3 - Goes to subcategories


        */


        public frmStock()
        {
            InitializeComponent();
        }
        Category catEdited = new Category(); //holds the ID of the category being edited, not saving object to save memory and to avoid references overwriting. >>>lol
        Subcategory scEdited = new Subcategory();
        Product pEdited = new Product();

        public bool Index()
        {
            Tab.SelectTab("Dashboard");


            //Reset colours of buttons, nothing is pressed
            btnCategories.BackColor = Color.FromArgb(34, 52, 70);
            btnSubCategories.BackColor = Color.FromArgb(34, 52, 70);
            btnProducts.BackColor = Color.FromArgb(34, 52, 70);

            refreshCategory();


            this.Show();
            return true;
        }
        public void refreshCategory()
        {
            //Clear all drop down stuff ad re-fill
            cbScCat.Items.Clear();
            cbProdCat.Items.Clear();
            cbProdSubcat.Items.Clear();
            foreach (Category x in Collections.Categories)
            {
                cbScCat.Items.Add(x.Name);
                cbProdCat.Items.Add(x.Name);
            }
            foreach (Subcategory sc in Collections.Subcategories)
                cbProdSubcat.Items.Add(sc.Name);

            cbScCat.Items.Add("None");
            cbProdSubcat.Items.Add("None");
            cbProdCat.Items.Add("None");
        }

        public void drawCategory(Category c)
        {
            //alternative views, dgv's, panels with images etc. < (not for categories dude wth)
            Button btnCategory = new Button();
            btnCategory.Size = new Size(153, 57);
            btnCategory.Text = c.Name;
            btnCategory.UseMnemonic = false;
            btnCategory.AccessibleName = c.ID.ToString();
            btnCategory.Font = new Font("Segoe UI Light", 10.00f);
            btnCategory.ForeColor = Color.LightGray;
            btnCategory.BackColor = c.Colour;
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.Click += BtnCategory_Click;

            flp_categories.Controls.Add(btnCategory);
        }
        public void drawSubCategory(Subcategory sc)
        {
            Button btnSubCategory = new Button();
            btnSubCategory.Size = new Size(153, 57);
            btnSubCategory.Text = sc.Name;
            btnSubCategory.UseMnemonic = false;
            btnSubCategory.AccessibleName = sc.ID.ToString();
            btnSubCategory.Font = new Font("Segoe UI Light", 10.00f);
            btnSubCategory.ForeColor = Color.LightGray;
            btnSubCategory.BackColor = sc.Colour;
            btnSubCategory.FlatStyle = FlatStyle.Flat;
            btnSubCategory.Click += BtnSubCategory_Click;

            flp_subcategories.Controls.Add(btnSubCategory);
        }
        public void drawProduct(Product p)
        {
            Button btnProduct = new Button();
            btnProduct.Size = new Size(153, 57);
            btnProduct.Text = p.Name;
            btnProduct.UseMnemonic = false;
            btnProduct.AccessibleName = p.ID.ToString();
            btnProduct.Font = new Font("Segoe UI Light", 10.00f);
            btnProduct.ForeColor = Color.LightGray;
            btnProduct.BackColor = p.Colour;
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.Click += BtnProduct_Click;

            flp_products.Controls.Add(btnProduct);
        }
        public void viewCategory(Category c)
        {
            lblTitle.Text = "Editing '" + c.Name + "'...";
            lblTitle.AccessibleName = "2";

            catEdited.ID = c.ID;
            txtCatName.Text = c.Name;
            pnlCatColour.BackColor = c.Colour;
            nudCatSortDisplay.Value = c.SortDisplay;
            nudCatSortReceipt.Value = c.SortPrint;
            btnCatSubmit.Width = 167;
            btnCatSubmit.BackColor = Color.FromArgb(41, 128, 185);
            btnCatSubmit.Text = "SAVE";
            cat_sub_tab.SelectTab(0);
        }
        public void viewSubCategory(Subcategory sc)
        {
            lblTitle.Text = "Editing '" + sc.Name + "'...";
            lblTitle.AccessibleName = "3";

            scEdited.ID = sc.ID;
            cbScCat.Text = Collections.Categories.Where(c => c.ID == sc.CategoryID).ToList().Count > 0 ? Collections.Categories.First(c => c.ID == sc.CategoryID).Name : "None";
            txtScName.Text = sc.Name;
            nudScSortDisplay.Value = sc.SortDisplay;
            nudScSortPrint.Value = sc.SortPrint;
            nudScInStockQty.Value = sc.InStockQty;
            pbSc.Image = sc.Picture;
            nudScReOrderLvl.Value = sc.ReorderLevel;
            pnlScColour.BackColor = sc.Colour;
            txtScRetailPrice.Text = Functions.Price(sc.RetailPrice);
            txtScCostPrice.Text = Functions.Price(sc.CostPrice);
            cbScAskPrice.Checked = sc.AskPrice;
            txtScWeight.Text = Functions.Price(sc.Weight);
            txtScWeightPrice.Text = Functions.Price(sc.WeightPrice);
            cbScAlert.Checked = sc.Reorder;
            dtpScExpiry.Value = sc.ExpiryDate;
            rtbScPopup.Text = sc.PopupMsg;
            if (sc.VAT)
            {
                lblScVAT.BackColor = Color.FromArgb(25, 179, 149);
                lblScVAT.Text = "YES";
            }
            else
            {
                lblScVAT.BackColor = Color.FromArgb(231, 79, 63);
                lblScVAT.Text = "NO";
            }

            btnScSubmit.Width = 167;
            btnScSubmit.BackColor = Color.FromArgb(41, 128, 185);
            btnScSubmit.Text = "SAVE";
            sc_sub_tab.SelectTab(0);
        }
        public void viewProduct(Product p)
        {
            lblTitle.Text = "Editing '" + p.Name + "'...";
            lblTitle.AccessibleName = "4";

            pEdited.ID = p.ID;
            cbProdCat.Text = Collections.Categories.Where(c => c.ID == p.CategoryID).ToList().Count > 0 ? Collections.Categories.First(c => c.ID == p.CategoryID).Name : "None";
            cbProdSubcat.Text = Collections.Subcategories.Where(c => c.ID == p.SubcategoryID).ToList().Count > 0 ? Collections.Subcategories.First(c => c.ID == p.SubcategoryID).Name : "None";
            txtProdName.Text = p.Name;
            nudProdSortDisplay.Value = p.SortDisplay;
            nudProdSortPrint.Value = p.SortPrint;
            pnlProdColour.BackColor = p.Colour;
            txtProdRetailPrice.Text = Functions.Price(p.RetailPrice);
            txtProdCostPrice.Text = Functions.Price(p.CostPrice);
            cbProdAskPrice.Checked = p.AskPrice;
            txtProdWeight.Text = p.Weight.ToString("0.##");
            txtProdWeightPrice.Text = Functions.Price(p.WeightPrice);
            cbProdAlert.Checked = p.Reorder;
            dtpProdExpiryDate.Value = p.ExpiryDate;
            rtbProdPopupMsg.Text = p.PopupMsg;
            if (p.VAT)
            {
                lblProdVAT.BackColor = Color.FromArgb(25, 179, 149);
                lblProdVAT.Text = "YES";
            }
            else
            {
                lblProdVAT.BackColor = Color.FromArgb(231, 79, 63);
                lblProdVAT.Text = "NO";
            }

            nudProdInStockQty.Value = p.InStockQty;
            pbProduct.Image = p.Picture;
            nudProdReOrderLvl.Value = p.ReorderLevel;

            btnProdSubmit.Width = 167;
            btnProdSubmit.BackColor = Color.FromArgb(41, 128, 185);
            btnProdSubmit.Text = "SAVE";
            product_sub_tab.SelectTab(0);
        }


        // Generated categories, sub-categories and products being clicked on (individually)

        private void BtnCategory_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            viewCategory(Collections.Categories.First(c => c.ID.ToString() == btn.AccessibleName));
        }
        private void BtnSubCategory_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            viewSubCategory(Collections.Subcategories.First(c => c.ID.ToString() == btn.AccessibleName));
        }
        private void BtnProduct_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            viewProduct(Collections.Products.First(c => c.ID.ToString() == btn.AccessibleName));
        }


        // The main buttons at the top, CATEGORY | SUB-CATEGORY | PRODUCT
        private void top_btns_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btnCategories.BackColor = Color.FromArgb(34, 52, 70);
            btnSubCategories.BackColor = Color.FromArgb(34, 52, 70);
            btnProducts.BackColor = Color.FromArgb(34, 52, 70);
            btn.BackColor = Color.FromArgb(24, 42, 60);

            switch (btn.Text)
            {
                case "CATEGORIES":
                    cat_sub_tab.SelectTab("ViewCategories");
                    if (Collections.Categories.Count != flp_categories.Controls.Count)
                    {
                        lblCatCount.Text = Collections.Categories.Count.ToString();
                        flp_categories.Controls.Clear();
                        foreach (Category c in Collections.Categories.OrderBy(n => n.SortDisplay))
                            drawCategory(c);
                    }
                    break;
                case "SUB-CATEGORIES":
                    sc_sub_tab.SelectTab("ViewSubCategories");
                    if (Collections.Subcategories.Count != flp_subcategories.Controls.Count)
                    {
                        lblScCount.Text = Collections.Subcategories.Count.ToString();
                        flp_subcategories.Controls.Clear();
                        foreach (Subcategory sc in Collections.Subcategories.OrderBy(n => n.SortDisplay))
                            drawSubCategory(sc);
                    }
                    break;
                case "PRODUCTS":
                    product_sub_tab.SelectTab("ViewProducts");
                    if (Collections.Products.Count != flp_products.Controls.Count)
                    {
                        lblPCount.Text = Collections.Products.Count.ToString();
                        flp_products.Controls.Clear();
                        foreach (Product p in Collections.Products.OrderBy(n => n.SortDisplay))
                            drawProduct(p);
                    }
                    break;
                default:
                    break;
            }
            lblTitle.Text = "Viewing " + btn.AccessibleName + "...";
            lblTitle.AccessibleName = "1";
            Tab.SelectTab(btn.Name.Substring(3, btn.Name.Length - 3));
        }

        private void frmStock_Load(object sender, EventArgs e)
        {
            Functions.GiveBorder(Tab, this, Color.FromArgb(44, 62, 80));

            Functions.GiveBorder(cat_sub_tab, Categories, Color.FromArgb(44, 62, 80));
            Functions.GiveBorder(sc_sub_tab, SubCategories, Color.FromArgb(44, 62, 80));
            Functions.GiveBorder(product_sub_tab, Products, Color.FromArgb(44, 62, 80));
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(lblTitle.AccessibleName))
            {
                case 0:
                    Router.Dashboard();
                    Thread.Sleep(50);
                    this.Hide();
                    break;

                case 1:
                    Tab.SelectTab("Dashboard");
                    lblTitle.Text = "Stock Management";
                    lblTitle.AccessibleName = "0";
                    Index();
                    break;

                case 2:
                    btnCategories.PerformClick();
                    break;

                case 3:
                    btnSubCategories.PerformClick();
                    break;

                case 4:
                    btnProducts.PerformClick();
                    break;
                default:
                    MessageBox.Show("All I know is ... that this is supposed to go back somewhere :p");
                    break;
            }

        }

        //The Create Buttons For All Stock
        private void btnCreateSubCategory_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            TabControl t = (TabControl)btn.Parent.Parent;

            //Set button back
            btnScSubmit.Width = 264;
            btnScSubmit.BackColor = Color.FromArgb(39, 174, 96);
            btnScSubmit.Text = "CREATE";
            t.SelectTab(0);

            lblTitle.AccessibleName = "3";
            lblTitle.Text = "Creating a " + btn.AccessibleName + "...";

            //Cleanup the form
            pbSc.Image = POS.Properties.Resources.unknown;
            cbScCat.SelectedIndex = -1;
            txtScName.Clear();

            nudScSortDisplay.Value = 0;
            nudScSortPrint.Value = 0;
            nudScInStockQty.Value = 0;

            pnlScColour.BackColor = Color.Gainsboro;
            txtScRetailPrice.Clear();
            txtScCostPrice.Clear();
            cbScAskPrice.Checked = false;
            txtScWeight.Clear();
            txtScWeightPrice.Clear();
            dtpScExpiry.Value = DateTime.Today;
            rtbScPopup.Clear();

            //depends on settings
            lblScVAT.Text = "YES";
            lblScVAT.BackColor = Color.FromArgb(25, 179, 149);
        }

        private void btnCreateProduct_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            TabControl t = (TabControl)btn.Parent.Parent;

            //Set button back
            btnProdSubmit.Width = 264;
            btnProdSubmit.BackColor = Color.FromArgb(39, 174, 96);
            btnProdSubmit.Text = "CREATE";
            t.SelectTab(0);

            lblTitle.AccessibleName = "4";
            lblTitle.Text = "Creating a " + btn.AccessibleName + "...";

            //Cleanup the form
            pbProduct.Image = POS.Properties.Resources.unknown;
            cbProdCat.SelectedIndex = -1;
            cbProdSubcat.SelectedIndex = -1;
            txtProdName.Clear();

            nudProdSortDisplay.Value = 0;
            nudProdSortPrint.Value = 0;
            nudProdInStockQty.Value = 0;

            pnlProdColour.BackColor = Color.Gainsboro;
            txtProdRetailPrice.Clear();
            txtProdCostPrice.Clear();
            cbProdAskPrice.Checked = false;
            txtProdWeight.Clear();
            txtProdWeightPrice.Clear();
            dtpProdExpiryDate.Value = DateTime.Today;
            rtbProdPopupMsg.Clear();

            //depends on settings
            lblProdVAT.Text = "YES";
            lblProdVAT.BackColor = Color.FromArgb(25, 179, 149);

        }
        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            TabControl t = (TabControl)btn.Parent.Parent;

            //Set button back
            btnCatSubmit.Width = 264;
            btnCatSubmit.BackColor = Color.FromArgb(39, 174, 96);
            btnCatSubmit.Text = "CREATE";
            t.SelectTab(0);

            lblTitle.AccessibleName = "2";
            lblTitle.Text = "Creating a " + btn.AccessibleName + "...";

            //Cleanup the form
            txtCatName.Clear();
            nudCatSortDisplay.Value = 0;
            nudCatSortReceipt.Value = 0;
            pnlCatColour.BackColor = Color.Gainsboro;
        }

        // Colour Selection
        private void pnlCatColour_Click(object sender, EventArgs e)
        {
            catColour.ShowDialog();
            pnlCatColour.BackColor = catColour.Color;
        }
        private void pnlScColour_Click(object sender, EventArgs e)
        {
            scColour.ShowDialog();
            pnlScColour.BackColor = scColour.Color;
        }
        private void pnlProdColour_Click(object sender, EventArgs e)
        {
            pColour.ShowDialog();
            pnlProdColour.BackColor = pColour.Color;

        }

        // Delete buttons
        private void btnCatDelete_Click(object sender, EventArgs e)
        {
            if (btnCatSubmit.Text == "SAVE" && catEdited.ID != 0)
            {
                if (MessageBox.Show("Are you sure you wish to delete this category?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Cannot delete if products of sub-categories depend on it
                    if (Functions.GetChildren(Collections.Categories.First(c => c.ID == catEdited.ID)).Count > 0)
                    {
                        MessageBox.Show("You cannot delete this category because it has a Sub-category or a Product that depends on it.", "Could not delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else {
                        SQL.DeleteCategory(catEdited.ID);

                        txtCatName.Clear();
                        pnlCatColour.BackColor = Color.Gainsboro;
                        nudCatSortDisplay.Value = 0;
                        nudCatSortReceipt.Value = 0;
                        btnCatSubmit.Width = 264;
                        btnCategories.PerformClick();
                        refreshCategory();
                    }
                }
            }
        }
        private void btnScDelete_Click(object sender, EventArgs e)
        {
            if (btnScSubmit.Text == "SAVE" && scEdited.ID != 0)
            {
                if (MessageBox.Show("Are you sure you wish to delete this sub-category?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (Functions.GetChildren(Collections.Subcategories.First(x=>x.ID == scEdited.ID)).Count > 0)
                    {
                        MessageBox.Show("You cannot delete this sub-category because it has a Product that depends on it.", "Could not delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        SQL.DeleteSubCategory(scEdited.ID);

                        cbScCat.SelectedIndex = -1;
                        txtScName.Clear();
                        nudScSortDisplay.Value = 0;
                        nudScSortPrint.Value = 0;
                        nudScReOrderLvl.Value = 0;
                        nudScInStockQty.Value = 0;
                        pbSc.Image = POS.Properties.Resources.unknown;

                        pnlScColour.BackColor = Color.Gainsboro;
                        txtScRetailPrice.Clear();
                        txtScCostPrice.Clear();
                        cbScAskPrice.Checked = false;
                        txtScWeight.Clear();
                        txtScWeightPrice.Clear();

                        dtpScExpiry.Value = DateTime.Today;
                        rtbScPopup.Clear();
                        lblScVAT.BackColor = Color.FromArgb(25, 179, 149);
                        lblScVAT.Text = "YES";

                        cbScAlert.Checked = false;



                        btnScSubmit.Width = 264;
                        btnSubCategories.PerformClick();
                        refreshCategory();
                    }
                }
            }
        }
        private void btnProdDelete_Click(object sender, EventArgs e)
        {
            if (btnProdSubmit.Text == "SAVE" && pEdited.ID != 0)
            {
                if (MessageBox.Show("Are you sure you wish to delete this product?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    SQL.DeleteProduct(pEdited.ID);

                    cbProdCat.SelectedIndex = -1;
                    cbProdSubcat.SelectedIndex = -1;
                    txtProdName.Clear();
                    nudProdSortDisplay.Value = 0;
                    nudProdSortPrint.Value = 0;
                    nudProdReOrderLvl.Value = 0;
                    nudProdInStockQty.Value = 0;
                    pbProduct.Image = POS.Properties.Resources.unknown;

                    pnlProdColour.BackColor = Color.Gainsboro;
                    txtProdRetailPrice.Clear();
                    txtProdCostPrice.Clear();
                    cbProdAskPrice.Checked = false;
                    txtProdWeight.Clear();
                    txtProdWeightPrice.Clear();

                    dtpProdExpiryDate.Value = DateTime.Today;
                    rtbProdPopupMsg.Clear();
                    lblProdVAT.BackColor = Color.FromArgb(25, 179, 149);
                    lblProdVAT.Text = "YES";

                    cbProdAlert.Checked = false;



                    btnProdSubmit.Width = 264;
                    btnProducts.PerformClick();
                    refreshCategory();
                }
            }
        }

        // Submit Buttons
        private void btnCatSubmit_Click(object sender, EventArgs e)
        {
            //First validation the form
            bool valid = true;
            //the box is not empty, and the category name is taken by a category other than the one currently being edited, it's invalid.
            if (txtCatName.Text == "None" || txtCatName.Text.Trim() == "" && txtCatName.Text.Trim().Length == 0 || (Collections.Categories.Where(x => x.Name == txtCatName.Name).ToList().Count > 0 && Collections.Categories.First(x => x.Name == txtCatName.Name).ID != catEdited.ID))
            {
                Functions.HighlightTextBox(txtCatName);
                valid = false;
                MessageBox.Show("This name is taken or invalid, please choose another name.", "Category name invalid", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            if (valid)
            {
                //Creating a category
                if (btnCatSubmit.Text == "CREATE")
                {
                    Category x = new Category();
                    x.Name = txtCatName.Text;
                    x.Colour = pnlCatColour.BackColor;
                    x.SortDisplay = (int)nudCatSortDisplay.Value;
                    x.SortPrint = (int)nudCatSortReceipt.Value;
                    SQL.CreateCategory(x);
                }

                //Editing/Saving a category
                else if (btnCatSubmit.Text == "SAVE")
                {
                    catEdited.Name = txtCatName.Text;
                    catEdited.Colour = pnlCatColour.BackColor; //?
                    catEdited.SortDisplay = (int)nudCatSortDisplay.Value;
                    catEdited.SortPrint = (int)nudCatSortReceipt.Value;
                    SQL.SaveCategory(catEdited);

                    flp_categories.Controls.Clear();
                    btnCategories.PerformClick();

                }

                //Clear the form and return to viewing categories
                txtCatName.Clear();
                pnlCatColour.BackColor = Color.Gainsboro;
                nudCatSortDisplay.Value = 0;
                nudCatSortReceipt.Value = 0;
                btnCatSubmit.Width = 264;
                btnCategories.PerformClick();
                refreshCategory();
            }

        }
        private void btnScSubmit_Click(object sender, EventArgs e)
        {
            //Ensure the field is not empty - and that it's not a name that already exists, unless that sub-category is currently being edited.
            if (txtScName.Text.Trim().Length != 0 || (Collections.Subcategories.Where(x => x.Name == txtScName.Text).ToList().Count > 0 && scEdited.Name != txtScName.Text))
            {
                if (btnScSubmit.Text == "CREATE")
                {
                    Subcategory sc = new Subcategory();
                    sc.CategoryID = Collections.Categories.Where(x => x.Name == cbScCat.Text).ToList().Count > 0 ? Collections.Categories.First(x => x.Name == cbScCat.Text).ID : 0;
                    sc.Name = txtScName.Text;
                    sc.SortDisplay = (int)nudScSortDisplay.Value;
                    sc.SortPrint = (int)nudScSortPrint.Value;
                    sc.Colour = pnlScColour.BackColor;
                    sc.RetailPrice = txtScRetailPrice.Text.Length > 0 ? Convert.ToDecimal(Functions.Price(Convert.ToDecimal(txtScRetailPrice.Text))) : 0;
                    sc.CostPrice = txtScCostPrice.Text.Length > 0 ? Convert.ToDecimal(Functions.Price(Convert.ToDecimal(txtScCostPrice.Text))) : 0;
                    sc.AskPrice = cbScAskPrice.Checked ? true : false;
                    sc.Weight = txtScWeight.Text.Length > 0 ? Convert.ToDecimal(Functions.Price(Convert.ToDecimal(txtScWeight.Text))) : 0;
                    sc.WeightPrice = txtScWeightPrice.Text.Length > 0 ? Convert.ToDecimal(Functions.Price(Convert.ToDecimal(txtScWeightPrice.Text))) : 0;
                    sc.Reorder = cbScAlert.Checked ? true : false;
                    sc.ExpiryDate = dtpScExpiry.Value;
                    sc.PopupMsg = rtbScPopup.Text;
                    sc.VAT = lblScVAT.Text == "YES" ? true : false;
                    sc.InStockQty = (int)nudScInStockQty.Value;
                    sc.Picture = pbSc.Image;
                    sc.ReorderLevel = (int)nudScReOrderLvl.Value;
                    SQL.CreateSubCategory(sc);
                }
                else
                {

                    scEdited.CategoryID = Collections.Categories.Where(x => x.Name == cbScCat.Text).ToList().Count > 0 ? Collections.Categories.First(x => x.Name == cbScCat.Text).ID : 0;
                    scEdited.Name = txtScName.Text;
                    scEdited.SortDisplay = (int)nudScSortDisplay.Value;
                    scEdited.SortPrint = (int)nudScSortPrint.Value;
                    scEdited.Colour = pnlScColour.BackColor;
                    scEdited.RetailPrice = txtScRetailPrice.Text.Length > 0 ? Convert.ToDecimal(Functions.Price(Convert.ToDecimal(txtScRetailPrice.Text))) : 0;
                    scEdited.CostPrice = txtScCostPrice.Text.Length > 0 ? Convert.ToDecimal(Functions.Price(Convert.ToDecimal(txtScCostPrice.Text))) : 0;
                    scEdited.AskPrice = cbScAskPrice.Checked ? true : false;
                    scEdited.Weight = txtScWeight.Text.Length > 0 ? Convert.ToDecimal(Functions.Price(Convert.ToDecimal(txtScWeight.Text))) : 0;
                    scEdited.WeightPrice = txtScWeightPrice.Text.Length > 0 ? Convert.ToDecimal(Functions.Price(Convert.ToDecimal(txtScWeightPrice.Text))) : 0;
                    scEdited.Reorder = cbScAlert.Checked ? true : false;
                    scEdited.ExpiryDate = dtpScExpiry.Value;
                    scEdited.PopupMsg = rtbScPopup.Text;
                    scEdited.VAT = lblScVAT.Text == "YES" ? true : false;
                    scEdited.InStockQty = (int)nudScInStockQty.Value;
                    scEdited.Picture = pbSc.Image;
                    scEdited.ReorderLevel = (int)nudScReOrderLvl.Value;
                    SQL.SaveSubCategory(scEdited);

                    flp_subcategories.Controls.Clear();
                    btnSubCategories.PerformClick();
                }
                //Clear the form and return to viewing categories

                cbScCat.SelectedIndex = -1;
                txtScName.Clear();
                nudScSortDisplay.Value = 0;
                nudScSortPrint.Value = 0;
                nudScReOrderLvl.Value = 0;
                nudScInStockQty.Value = 0;
                pbSc.Image = POS.Properties.Resources.unknown;

                pnlScColour.BackColor = Color.Gainsboro;
                txtScRetailPrice.Clear();
                txtScCostPrice.Clear();
                cbScAskPrice.Checked = false;
                txtScWeight.Clear();
                txtScWeightPrice.Clear();

                dtpScExpiry.Value = DateTime.Today;
                rtbScPopup.Clear();
                lblScVAT.BackColor = Color.FromArgb(25, 179, 149);
                lblScVAT.Text = "YES";

                cbScAlert.Checked = false;



                btnScSubmit.Width = 264;
                btnSubCategories.PerformClick();
                refreshCategory();
            }
            else
            {
                Functions.HighlightTextBox(txtScName);
                MessageBox.Show("Make sure you properly fill in the required fields?", "Form invalid", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void btnProdSubmit_Click(object sender, EventArgs e)
        {
            //Ensure the field is not empty - and that it's not a name that already exists, unless that sub-category is currently being edited.
            if (txtProdName.Text.Trim().Length != 0 || (Collections.Products.Where(x => x.Name == txtProdName.Text).ToList().Count > 0 && pEdited.Name != txtProdName.Text))
            {
                if (btnProdSubmit.Text == "CREATE")
                {
                    Product p = new Product();
                    p.CategoryID = Collections.Categories.Where(x => x.Name == cbProdCat.Text).ToList().Count > 0 ? Collections.Categories.First(x => x.Name == cbProdCat.Text).ID : 0;
                    p.SubcategoryID = Collections.Subcategories.Where(x => x.Name == cbProdSubcat.Text).ToList().Count > 0 ? Collections.Subcategories.First(x => x.Name == cbProdSubcat.Text).ID : 0;
                    p.Name = txtProdName.Text;
                    p.SortDisplay = (int)nudProdSortDisplay.Value;
                    p.SortPrint = (int)nudProdSortPrint.Value;
                    p.Colour = pnlProdColour.BackColor;
                    p.RetailPrice = txtProdRetailPrice.Text.Length > 0 ? Convert.ToDecimal(txtProdRetailPrice.Text) : 0;
                    p.CostPrice = txtScCostPrice.Text.Length > 0 ? Convert.ToDecimal(txtScCostPrice.Text) : 0;
                    p.AskPrice = cbProdAskPrice.Checked ? true : false;
                    p.Weight = txtProdWeight.Text.Length > 0 ? Convert.ToDecimal(txtProdWeight.Text) : 0;
                    p.WeightPrice = txtProdWeightPrice.Text.Length > 0 ? Convert.ToDecimal(txtProdWeightPrice.Text) : 0;
                    p.Reorder = cbProdAlert.Checked ? true : false;
                    p.ExpiryDate = dtpProdExpiryDate.Value;
                    p.PopupMsg = rtbProdPopupMsg.Text;
                    p.VAT = lblProdVAT.Text == "YES" ? true : false;
                    p.InStockQty = (int)nudProdInStockQty.Value;
                    p.Picture = pbProduct.Image;
                    p.ReorderLevel = (int)nudProdReOrderLvl.Value;
                    SQL.CreateProduct(p);
                }
                else
                {
                    pEdited.CategoryID = Collections.Categories.Where(x => x.Name == cbProdCat.Text).ToList().Count > 0 ? Collections.Categories.First(x => x.Name == cbProdCat.Text).ID : 0;
                    pEdited.SubcategoryID = Collections.Subcategories.Where(x => x.Name == cbProdSubcat.Text).ToList().Count > 0 ? Collections.Subcategories.First(x => x.Name == cbProdSubcat.Text).ID : 0;
                    pEdited.Name = txtProdName.Text;
                    pEdited.SortDisplay = (int)nudProdSortDisplay.Value;
                    pEdited.SortPrint = (int)nudProdSortPrint.Value;
                    pEdited.Colour = pnlProdColour.BackColor;
                    pEdited.RetailPrice = txtProdRetailPrice.Text.Length > 0 ? Convert.ToDecimal(txtProdRetailPrice.Text) : 0;
                    pEdited.CostPrice = txtProdCostPrice.Text.Length > 0 ? Convert.ToDecimal(txtProdCostPrice.Text) : 0;
                    pEdited.AskPrice = cbProdAskPrice.Checked ? true : false;
                    pEdited.Weight = txtProdWeight.Text.Length > 0 ? Convert.ToDecimal(txtProdWeight.Text) : 0;
                    pEdited.WeightPrice = txtProdWeightPrice.Text.Length > 0 ? Convert.ToDecimal(txtProdWeightPrice.Text) : 0;
                    pEdited.Reorder = cbProdAlert.Checked ? true : false;
                    pEdited.ExpiryDate = dtpProdExpiryDate.Value;
                    pEdited.PopupMsg = rtbProdPopupMsg.Text;
                    pEdited.VAT = lblProdVAT.Text == "YES" ? true : false;
                    pEdited.InStockQty = (int)nudProdInStockQty.Value;
                    pEdited.Picture = pbProduct.Image;
                    pEdited.ReorderLevel = (int)nudProdReOrderLvl.Value;
                    SQL.SaveProduct(pEdited);

                    flp_products.Controls.Clear();
                    btnProducts.PerformClick();
                }
                //Clear the form and return to viewing categories

                cbProdCat.SelectedIndex = -1;
                cbProdSubcat.SelectedIndex = -1;
                txtProdName.Clear();
                nudProdSortDisplay.Value = 0;
                nudProdSortPrint.Value = 0;
                nudProdReOrderLvl.Value = 0;
                nudProdInStockQty.Value = 0;
                pbProduct.Image = POS.Properties.Resources.unknown;

                pnlProdColour.BackColor = Color.Gainsboro;
                txtProdRetailPrice.Clear();
                txtProdCostPrice.Clear();
                cbProdAskPrice.Checked = false;
                txtProdWeight.Clear();
                txtProdWeightPrice.Clear();

                dtpProdExpiryDate.Value = DateTime.Today;
                rtbProdPopupMsg.Clear();
                lblProdVAT.BackColor = Color.FromArgb(25, 179, 149);
                lblProdVAT.Text = "YES";

                cbProdAlert.Checked = false;



                btnProdSubmit.Width = 264;
                btnProducts.PerformClick();
                refreshCategory();
            }
            else
            {
                Functions.HighlightTextBox(txtProdName);
                MessageBox.Show("Make sure you properly fill in the required fields?", "Form invalid", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // VAT links
        private void lblProdVAT_Click(object sender, EventArgs e)
        {
            if (lblProdVAT.Text == "YES")
            {
                //switch to no
                lblProdVAT.BackColor = Color.FromArgb(231, 79, 63);
                lblProdVAT.Text = "NO";
            }
            else
            {
                lblProdVAT.BackColor = Color.FromArgb(25, 179, 149);
                lblProdVAT.Text = "YES";
            }
        }
        private void lblScVAT_Click(object sender, EventArgs e)
        {
            if (lblScVAT.Text == "YES")
            {
                //switch to no
                lblScVAT.BackColor = Color.FromArgb(231, 79, 63);
                lblScVAT.Text = "NO";
            }
            else
            {
                lblScVAT.BackColor = Color.FromArgb(25, 179, 149);
                lblScVAT.Text = "YES";
            }
        }

        // Uploading image links 
        private void lblScUploadImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            ofd.ShowDialog();

            if (ofd.FileName.Length > 0)
                pbSc.Image = Image.FromFile(ofd.FileName);
            pbSc.ImageLocation = ofd.FileName;
        }
        private void lblPUploadImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            ofd.ShowDialog();

            if (ofd.FileName.Length > 0)
                pbProduct.Image = Image.FromFile(ofd.FileName);
            pbProduct.ImageLocation = ofd.FileName;
        }

        private void cbProdCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProdSubcat.Items.Clear();
            if (cbProdCat.Text != "" && cbProdCat.Text != "None")
                foreach (Subcategory sc in Collections.Subcategories.Where(x => x.CategoryID == Collections.Categories.First(c => c.Name == cbProdCat.Text).ID))
                {
                    cbProdSubcat.Items.Add(sc.Name);
                }
            cbProdSubcat.Items.Add("None");
        }
        private void cbProdSubcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the sC has a category, select it.
            if (cbProdSubcat.Text != "None")
            {
                try
                {
                    cbProdCat.Text = Functions.GetParent(Collections.Subcategories.First(sc => sc.Name == cbProdSubcat.Name)).Name;
                }
                catch (Exception) { }
                //load hereditary details
                Subcategory parent = Collections.Subcategories.First(sc => sc.Name == cbProdSubcat.Text);

                nudProdSortDisplay.Value = parent.SortDisplay;
                nudProdSortPrint.Value = parent.SortPrint;
                pnlProdColour.BackColor = parent.Colour;
                txtProdRetailPrice.Text = Functions.Price(parent.RetailPrice);
                txtProdCostPrice.Text = Functions.Price(parent.CostPrice);
                cbProdAskPrice.Checked = parent.AskPrice;
                txtProdWeight.Text = parent.Weight.ToString("0.##");
                nudProdReOrderLvl.Value = parent.ReorderLevel;
                cbProdAlert.Checked = parent.Reorder;
                dtpProdExpiryDate.Value = parent.ExpiryDate;
                rtbProdPopupMsg.Text = parent.PopupMsg;
                if (parent.VAT)
                {
                    lblProdVAT.BackColor = Color.FromArgb(25, 179, 149);
                    lblProdVAT.Text = "YES";
                }
                else
                {
                    lblProdVAT.BackColor = Color.FromArgb(231, 79, 63);
                    lblProdVAT.Text = "NO";
                }
                nudProdInStockQty.Value = parent.InStockQty;
            }
        }

        private void numbersOnlyFields_Type(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
         (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}