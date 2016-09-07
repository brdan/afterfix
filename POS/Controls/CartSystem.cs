using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using POS.Classes;

namespace POS.Controls
{
    public partial class CartSystem : UserControl
    {
        // Privates and Constants
        bool cartColourPattern = true;
        bool pnlOptionsVisible = false;
        bool cartScrollDirection;
        int coolValue = 0;
        int selectedItemIndex = -1;
        decimal _totalPrice;
        Control selectedItem = null;
        Cart TempCart = new Cart();

        // Publics
        public event EventHandler ItemEdit;
        public event EventHandler ItemDiscount;
        public event EventHandler ItemModify;
        public decimal totalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;

                string moneyString = Settings.Setting.ContainsKey("currency") ? Settings.Setting["currency"] + Functions.Monify(_totalPrice.ToString()) : Functions.Monify(_totalPrice.ToString());

                lblTotalPrice.Text = moneyString;
                lblTotalPrice2.Text = moneyString;
            }
        }

        public CartSystem()
        {
            InitializeComponent();
        }
        public dynamic GetSelectedItem()
        {
            dynamic item = new System.Dynamic.ExpandoObject();
            item.Qty = selectedItem.Controls[0].Text;
            item.Description = selectedItem.Controls[1].Text;
            item.Price = selectedItem.Controls[2].Text.Substring(1);
            return item;
        }
        public void Deselect(Control c)
        {
            flp_cart.Controls[flp_cart.Controls.IndexOf(c)].Controls.OfType<Control>().ToList().ForEach(sc => sc.BackColor = Color.FromArgb(Convert.ToInt32(c.Tag)));
            selectedItemIndex = -1;
        }
        public void AddItem(Product p)
        {
            Color pattern = cartColourPattern ? Color.FromArgb(50, 68, 86) : Color.FromArgb(60, 78, 96);
            cartColourPattern = !cartColourPattern;

            //Item Container
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Size = new Size(268, 60);
            flp.AutoSize = true;
            flp.Margin = new Padding(0, 0, 0, 0);
            flp.AccessibleName = p.ID.ToString();
            flp.AccessibleDescription = "item";
            flp.Tag = pattern.ToArgb().ToString();

            //Item Qty Label
            Label lblQty = new Label();
            lblQty.Font = new Font("Segoe UI", 9.00f);
            lblQty.Text = "1";
            lblQty.ForeColor = Color.Gainsboro;
            lblQty.BackColor = pattern;
            lblQty.Margin = new Padding(0, 0, 0, 0);
            lblQty.Padding = new Padding(5, 5, 5, 5);
            lblQty.AutoSize = true;
            lblQty.TextAlign = ContentAlignment.MiddleCenter;
            lblQty.MinimumSize = new Size(35, 60);
            lblQty.MaximumSize = new Size(35, 0);
            lblQty.Dock = DockStyle.Left;
            lblQty.Click += item_Click;

            //Item Name Label
            Label lblName = new Label();
            lblName.Font = new Font("Segoe UI", 8.00f, FontStyle.Bold);
            lblName.Text = p.Name.ToUpper();
            lblName.ForeColor = Color.Gainsboro;
            lblName.BackColor = pattern;
            lblName.Margin = new Padding(0, 0, 0, 0);
            lblName.Padding = new Padding(5, 5, 5, 5);
            lblName.AutoSize = true;
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            lblName.MinimumSize = new Size(169, 60);
            lblName.MaximumSize = new Size(169, 0);
            lblName.UseMnemonic = false;
            lblName.Click += item_Click;

            //Item Price Label
            Label lblPrice = new Label();
            lblPrice.Font = new Font("Segoe UI", 10.00f, FontStyle.Bold);
            lblPrice.Text = Settings.Setting["currency"] + Functions.Monify(p.CostPrice.ToString());
            lblPrice.TextAlign = ContentAlignment.MiddleCenter;
            lblPrice.ForeColor = Color.Gainsboro;
            lblPrice.BackColor = pattern;
            lblPrice.Margin = new Padding(0, 0, 0, 0);
            lblPrice.Padding = new Padding(5, 5, 5, 5);
            lblPrice.AutoSize = true;
            lblPrice.MinimumSize = new Size(64, 60);
            lblPrice.MaximumSize = new Size(64, 0);
            lblPrice.Dock = DockStyle.Right;
            lblPrice.Click += item_Click;

            flp.Controls.Add(lblQty);
            flp.Controls.Add(lblName);
            flp.Controls.Add(lblPrice);
            flp_cart.Controls.Add(flp);

            OrderItem newItem = new OrderItem();
            newItem.Description = p.Name;
            newItem.ItemPrice = p.CostPrice;
            TempCart.Items.Add(newItem);

            //Update Prices
            totalPrice += newItem.ItemPrice;


        }
        public void EditItem(string qty, string description, string price)
        {
            if (selectedItemIndex == -1)
            {
                MessageBox.Show("You must select an item to add this to...", "No item selected");
            }
            else
            { 
                //subtract based on greater value
                decimal difference = 0.00M;
                if (Convert.ToDecimal(price) > Convert.ToDecimal(GetSelectedItem().Price))
                {
                    difference = Convert.ToDecimal(price) - Convert.ToDecimal(GetSelectedItem().Price);
                    totalPrice += difference;
                }
                else
                {
                    difference = Convert.ToDecimal(GetSelectedItem().Price) - Convert.ToDecimal(price);
                    totalPrice -= difference;
                }
                selectedItem.Controls[0].Text = qty;
                selectedItem.Controls[1].Text = description;
                selectedItem.Controls[2].Text = Settings.Setting["currency"] + Functions.Monify(price);
            }
        }
        public void AddSubItem(bool DiscountOrModifier, string textString, string priceString, bool isPercent = false)
        {
            if (selectedItemIndex == -1)
            {
                MessageBox.Show("You must select an item to add this to...", "No item selected");
            }
            else
            {
                //Adding Discounts & Modifications

                selectedItem = flp_cart.Controls[selectedItemIndex];
                flp_cart.Refresh();
                flp_cart.PerformLayout();

                //If sub-box doesn't exist, quickly create it.
                #region Creates Sub-Box if it's non-existent
                try
                {
                    if (flp_cart.Controls[selectedItemIndex + 1].Margin.Left != 20)
                    {
                        FlowLayoutPanel flp = new FlowLayoutPanel();
                        flp.AutoSize = true;
                        flp.Margin = new Padding(20, 0, 0, 0);
                        flp_cart.Controls.Add(flp);
                        flp_cart.Controls.SetChildIndex(flp, selectedItemIndex + 1);

                    }
                }
                catch (Exception)
                {
                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    flp.AutoSize = true;
                    flp.Margin = new Padding(20, 0, 0, 0);
                    flp_cart.Controls.Add(flp);
                    flp_cart.Controls.SetChildIndex(flp, selectedItemIndex + 1);
                }
                #endregion

                #region Price Calculations
                // The above code checks if the selected item has a "sub-item's box" - if it does, it adds the sub-item accordingly; if not, it creates it, then adds the item.
                decimal newParentItemPrice = 0.00M;
                //Updating prices according to the added/deducted price on the item, also will need to update the item's `Price` field accordingly
                if (DiscountOrModifier)
                {
                    //deduct
                    decimal parentItemPrice = Convert.ToDecimal(selectedItem.Controls[2].Text.Substring(1));

                    //is price fixed or percentage? 
                    decimal priceToDeduct = isPercent ? (Convert.ToDecimal(priceString) * parentItemPrice) / 100 : Convert.ToDecimal(priceString);

                    newParentItemPrice = parentItemPrice - priceToDeduct;
                    if (newParentItemPrice < 0.00M)
                        newParentItemPrice = 0.00M;

                    totalPrice -= (parentItemPrice - newParentItemPrice); //this way no negative value issues
                }
                else
                {
                    decimal parentItemPrice = Convert.ToDecimal(selectedItem.Controls[2].Text.Substring(1));

                    //is price fixed or percentage? 
                    decimal priceToAdd = isPercent ? (Convert.ToDecimal(priceString) * parentItemPrice) / 100 : Convert.ToDecimal(priceString);

                    newParentItemPrice = Convert.ToDecimal(priceToAdd) + parentItemPrice;
                    totalPrice += Convert.ToDecimal(priceToAdd);
                }
                #endregion

                #region Designs the three labels based on parameters, then adds it to the Sub-Box
                Control parent = flp_cart.Controls[selectedItemIndex + 1];
                //the three labels
                Label lblIcon = new Label();
                lblIcon.BackColor = Color.FromArgb(14, 32, 50);
                lblIcon.ForeColor = Color.Gainsboro;
                lblIcon.Font = new Font("Heydings Icons", 13.00f);
                lblIcon.Margin = new Padding(0, 0, 0, 0);
                lblIcon.Padding = new Padding(1, 1, 1, 1);
                lblIcon.Size = new Size(30, 25);
                lblIcon.Dock = DockStyle.Left;
                lblIcon.Text = DiscountOrModifier ? "t" : "a";
                lblIcon.TextAlign = ContentAlignment.MiddleCenter;
                lblIcon.Click += subItem_Click;

                Label lblName = new Label();
                lblName.BackColor = Color.FromArgb(24, 42, 60);
                lblName.ForeColor = Color.Gainsboro;
                lblName.Font = new Font("Segoe UI", 9.00f);
                lblName.Margin = new Padding(0, 0, 0, 0);
                lblName.Padding = new Padding(2, 2, 2, 2);
                lblName.Text = textString;
                lblName.TextAlign = ContentAlignment.MiddleLeft;
                lblName.AutoSize = true;
                lblName.Dock = DockStyle.Left;
                lblName.MaximumSize = new Size(160, 0);
                lblName.MinimumSize = new Size(160, 25);
                lblName.Click += subItem_Click;

                Label lblPrice = new Label();
                lblPrice.BackColor = Color.FromArgb(34, 52, 70);
                lblPrice.ForeColor = DiscountOrModifier ? Color.FromArgb(231, 76, 60) : Color.FromArgb(46, 204, 113);
                string str = DiscountOrModifier ? "-" : "+";

                decimal displayPrice = isPercent ? ((Convert.ToDecimal(priceString) * Convert.ToDecimal(selectedItem.Controls[2].Text.Substring(1))) / 100) : Convert.ToDecimal(priceString);

                lblPrice.Text = str + Settings.Setting["currency"] + Functions.Monify(displayPrice.ToString());
                Font font = new Font("Segoe UI", 10.00f);
                if (lblPrice.Text.Length > 5)
                    font = new Font("Segoe UI", 9.00f);
                else if (lblPrice.Text.Length > 7)
                    font = new Font("Segoe UI", 5.00f);
                lblPrice.Font = font;
                lblPrice.Dock = DockStyle.Left;
                lblPrice.TextAlign = ContentAlignment.MiddleLeft;
                lblPrice.Margin = new Padding(0, 0, 0, 0);
                lblPrice.BackColor = Color.FromArgb(24, 42, 60);
                lblPrice.Padding = new Padding(2, 2, 2, 2);
                lblPrice.MaximumSize = new Size(63, 0);
                lblPrice.MinimumSize = new Size(63, 25);
                lblPrice.AutoSize = true;
                lblPrice.Click += subItem_Click;


                //add controls
                parent.Controls.Add(lblIcon);
                parent.Controls.Add(lblName);
                parent.Controls.Add(lblPrice);
                #endregion

                #region Finally, updating price visual and changing the colour to indicate price influence
                flp_cart.Controls[selectedItemIndex].Controls[2].Text = Settings.Setting["currency"] + Functions.Monify(newParentItemPrice.ToString());
                flp_cart.Controls[selectedItemIndex].Controls[2].ForeColor = Color.FromArgb(232, 126, 4);
                #endregion 
            }
        }
        public void RemoveDiscounts(int clause = 1)
        {
            //1: just the item, 2: all items, 3: order discounts, 4: wipe the whole thing clean of discounts
            switch (clause)
            {
                case 1:

                    break;

                default:
                    break;
            }
        }
        public int ItemCount()
        {
            int counter = 0;
            foreach (Control c in flp_cart.Controls)
                if (c.AccessibleDescription == "item")
                    counter++;

            return counter;
        }
        public Cart GetCart()
        {
            Cart c = new Cart();

            foreach(Control cartItem in flp_cart.Controls)
            {
                if(cartItem.AccessibleDescription == "item")
                {
                    // So this is a main Item, create it
                    OrderItem oI = new OrderItem(); // Creation will add ID

                    oI.Qty = Convert.ToInt16(cartItem.Controls[0].Text);
                    oI.Description = cartItem.Controls[1].Text;
                    oI.ItemPrice = Convert.ToDecimal(cartItem.Controls[2].Text.Substring(1));

                    #region Adding Sub-items to the OrderItem
                    // Check if sub-items exist
                    // But first check in the 'if' statement that there even IS any control that follows the previous one, to prevent index exception
                    if (flp_cart.Controls.Count > (flp_cart.Controls.IndexOf(cartItem) + 1) && flp_cart.Controls[flp_cart.Controls.IndexOf(cartItem) + 1].Margin.Left == 20)
                    {
                        Control subBox = flp_cart.Controls[flp_cart.Controls.IndexOf(cartItem) + 1];

                        for (int i = 0; i < subBox.Controls.Count / 3; i += 3)
                        {
                            SubItem sI = new SubItem();
                            sI.DiscountOrModifier = subBox.Controls[i].Text == "t" ? true : false; // The icon
                            sI.Description = subBox.Controls[i + 1].Text; // The Description
                            sI.Price = Convert.ToDecimal(subBox.Controls[i + 2].Text.Substring(2)); // The Price, substring 2 because of currency and +/- symbols
                            oI.SubItems.Add(sI);   
                        }
                    }
                    #endregion

                    c.Items.Add(oI);
                }
            }

            //When all the items and order items have been added, all that's left is the order ID attachment
            return c;
        }


        private void item_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            int lastSelectedIndex = -1;
            //If something is already selected, clear all selection
            if (selectedItemIndex != -1)
            {
                lastSelectedIndex = selectedItemIndex;
                Deselect(flp_cart.Controls[selectedItemIndex]);
            }

            //If nothing is selected (after clearing)
            if (selectedItemIndex == -1)
            {
                if (flp_cart.Controls.IndexOf(lbl.Parent) != lastSelectedIndex)
                {
                    //flp_cart.Controls[flp_cart.Controls.IndexOf(lbl.Parent)].Controls.OfType<Control>().ToList().ForEach(c => c.BackColor = Color.FromArgb(41, 128, 185));
                    selectedItemIndex = flp_cart.Controls.IndexOf(lbl.Parent);
                    lbl.Parent.Controls.OfType<Control>().ToList().ForEach(c => c.BackColor = Color.FromArgb(41, 128, 185));
                    pnlOptionsVisible = false;
                    tmrOptions.Start();

                }
                else
                {
                    lastSelectedIndex = -1;
                    pnlOptionsVisible = true;
                    tmrOptions.Start();
                }
            }

        }
        private void subItem_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;

            MessageBox.Show("You've clicked on a sub-item");
        }

        #region Options animation and Control load
        private void tmrOptions_Tick(object sender, EventArgs e)
        {
            if (!pnlOptionsVisible)
                //going up
                if (pnlOptions.Height < 122)
                    pnlOptions.Height += 2;
                else
                {
                    tmrOptions.Stop();
                    pnlOptions.Visible = true;
                    panel3.Show();
                }

            else if (pnlOptionsVisible)
                //going down
            {
                panel3.Hide(); //hide first here instead of after animation, simply looks prettier :)
                if (pnlOptions.Height > 0)
                    pnlOptions.Height -= 2;
                else
                {
                    tmrOptions.Stop();
                    pnlOptionsVisible = false;

                }
            }
        }
        #endregion

        #region Scrolling Functionality
        private void btnScroll_MouseDown(object sender, MouseEventArgs e)
        {
            Label btn = (Label)sender;
            cartScrollDirection = btn.Text == "▲" ? true : false;
            tmrScroll.Start();
        }
        private void btnScroll_MouseUp(object sender, MouseEventArgs e)
        {
            tmrScroll.Stop();
            coolValue = cartScrollDirection ? flp_cart.VerticalScroll.Value - 50 : flp_cart.VerticalScroll.Value + 50;
            tmrCool.Start();
        }
        private void tmrScroll_Tick(object sender, EventArgs e)
        {
            try
            {
                //up
                if (cartScrollDirection)

                    flp_cart.VerticalScroll.Value -= 10;


                //down
                else if (!cartScrollDirection)
                {
                    flp_cart.VerticalScroll.Value += 10;
                }
            }
            catch (Exception)
            {

            }
            flp_cart.PerformLayout();
            flp_cart.Refresh();
        }
        private void tmrCool_Tick(object sender, EventArgs e)
        {
            try
            {
                if (cartScrollDirection)
                {
                    if ((flp_cart.VerticalScroll.Value - coolValue) >= 30)
                    {
                        flp_cart.VerticalScroll.Value -= 5;
                    }
                    else if ((flp_cart.VerticalScroll.Value - coolValue) > 12)
                    {
                        flp_cart.VerticalScroll.Value -= 2;
                    }
                    else
                    {
                        tmrCool.Stop();
                    }
                }

                //cool down
                else
                {

                    if ((coolValue - flp_cart.VerticalScroll.Value) >= 30)
                    {
                        flp_cart.VerticalScroll.Value += 5;
                    }
                    else if ((coolValue - flp_cart.VerticalScroll.Value) > 8)
                    {
                        flp_cart.VerticalScroll.Value += 2;
                    }
                    else
                    {
                        tmrCool.Stop();
                    }

                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Item Options (Delete, Edit, Discount, Modify)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedItemIndex != -1)
            {
                selectedItem = flp_cart.Controls[selectedItemIndex];
                int thisItem = flp_cart.Controls.IndexOf(selectedItem);
                try
                {
                    //Remove sub-items
                    if (flp_cart.Controls[selectedItemIndex + 1].Margin.Left == 20)
                    {
                        flp_cart.Controls.Remove(flp_cart.Controls[selectedItemIndex + 1]);
                    }
                }
                catch (Exception)
                {
                }

                //Deduct price
                decimal price = Convert.ToDecimal(selectedItem.Controls[2].Text.Substring(1));
                totalPrice -= price;

                //Deselect
                Deselect(selectedItem);

                //Visually Remove
                flp_cart.Controls.Remove(selectedItem);

                //Selecting the subsequent product; if none exists, options slide down.
                if (flp_cart.Controls.Count > 0)
                {
                    if (thisItem != flp_cart.Controls.Count)
                    {
                        //making sure what I'm selecting isn't a freakin sub-box
                        
                            selectedItemIndex = thisItem;
                    }
                    else
                    {
                        if (flp_cart.Controls[thisItem - 1].Margin.Left == 0)
                            selectedItemIndex = thisItem - 1;
                        else
                        {
                            //one more step
                            if (flp_cart.Controls[thisItem - 2].Margin.Left == 0)
                                selectedItemIndex = thisItem - 2;
                        }
                    }

                    flp_cart.Controls[selectedItemIndex].Controls.OfType<Control>().ToList().ForEach(c => c.BackColor = Color.FromArgb(41, 128, 185));
                }
                else
                {
                    pnlOptionsVisible = true;
                    tmrOptions.Start();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedItemIndex != -1)
            {
                selectedItem = flp_cart.Controls[selectedItemIndex];

                if (ItemEdit != null)
                    ItemEdit(new object(), new EventArgs());
            }
        }
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (selectedItemIndex != -1)
            {
                selectedItem = flp_cart.Controls[selectedItemIndex];

                if (ItemDiscount != null)
                    ItemDiscount(new object(), new EventArgs());
            }
        }
        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (selectedItemIndex != -1)
            {
                selectedItem = flp_cart.Controls[selectedItemIndex];

                if (ItemModify != null)
                    ItemModify(new object(), new EventArgs());
            }
        }
        #endregion



    }
}