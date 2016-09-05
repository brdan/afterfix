using POS.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
         
        }
        Customer viewedCustomer;
        public bool Index()
        {
            //Clears pane and begins to draw customers
            flp_customers.Controls.Clear();
            btnSelectCustomer.Hide();
            lblTitle.Text = "Customers";
            lblTitle.AccessibleName = "0";
            foreach (Customer c in Collections.Customers)
                paintCustomer(c);
            lblRecordCount.Text = Collections.Customers.Count.ToString();
            Tab.SelectTab("Search");
            this.Show();


            if (Router.frmCustFromOrder)
            {
                btnSelectCustomer.Show();
                lblTitle.Text = "Back to Order";
                lblTitle.AccessibleName = "3";
            }

            return true;
        }
        public void paintCustomer(Customer c)
        {
            Panel pnl_customer = new Panel();
            pnl_customer.Size = new Size(185, 194);
            pnl_customer.BackColor = Color.FromArgb(24, 42, 60);
            pnl_customer.Margin = new Padding(0, 0, 3, 3);
            pnl_customer.AccessibleName = c.ID.ToString();
            pnl_customer.MouseDown += Functions.MouseEnter_Effect;
            pnl_customer.MouseUp += Functions.MouseLeave_Effect;
            pnl_customer.Click += pnl_customer_Click;

            PictureBox pbx = new PictureBox();
            pbx.Size = new Size(60, 60);
            pbx.Location = new Point(((pnl_customer.Width / 2) - (pbx.Width / 2)), 14);
            pbx.Image = c.Avatar;
            pbx.SizeMode = PictureBoxSizeMode.StretchImage;
            pbx.MouseDown += Functions.MouseEnter_Effect;
            pbx.MouseUp += Functions.MouseLeave_Effect;
            pbx.Click += pnl_customer_Click;
            pnl_customer.Controls.Add(pbx);

            Label lblName = new Label();
            lblName.Text = c.FirstName + " " + c.LastName;
            lblName.Font = new Font("Segoe UI Light", 12.00f);
            lblName.ForeColor = Color.Gainsboro;
            lblName.Location = new Point(0, 83);
            lblName.Size = new Size(pnl_customer.Width, 25);
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            lblName.MouseDown += Functions.MouseEnter_Effect;
            lblName.MouseUp += Functions.MouseLeave_Effect;
            lblName.Click += pnl_customer_Click;
            pnl_customer.Controls.Add(lblName);

            Label lblEmail = new Label();
            lblEmail.Text = c.Email.Length <= 22 ? c.Email : c.Email.Substring(0, 22) + "...";
            lblEmail.Font = new Font("Segoe UI", 8.00f);
            lblEmail.ForeColor = Color.FromArgb(52, 152, 219);
            lblEmail.Location = new Point(0, 109);
            lblEmail.Size = new Size(pnl_customer.Width, 25);
            lblEmail.TextAlign = ContentAlignment.MiddleCenter;
            lblEmail.MouseDown += Functions.MouseEnter_Effect;
            lblEmail.MouseUp += Functions.MouseLeave_Effect;
            lblEmail.Click += pnl_customer_Click;
            pnl_customer.Controls.Add(lblEmail);

            Label lblMobile = new Label();
            lblMobile.Text = c.Mobile;
            lblMobile.Font = new Font("Segoe UI", 8.00f);
            lblMobile.ForeColor = Color.Gainsboro;
            lblMobile.Location = new Point(0, 147);
            lblMobile.Size = new Size(pnl_customer.Width, 16);
            lblMobile.TextAlign = ContentAlignment.MiddleCenter;
            lblMobile.MouseDown += Functions.MouseEnter_Effect;
            lblMobile.MouseUp += Functions.MouseLeave_Effect;
            lblMobile.Click += pnl_customer_Click;
            pnl_customer.Controls.Add(lblMobile);

            Label lblPhone = new Label();
            lblPhone.Text = c.Telephone;
            lblPhone.Font = new Font("Segoe UI", 8.00f);
            lblPhone.ForeColor = Color.Gainsboro;
            lblPhone.Location = new Point(0, 166);
            lblPhone.Size = new Size(pnl_customer.Width, 16);
            lblPhone.TextAlign = ContentAlignment.MiddleCenter;
            lblPhone.MouseDown += Functions.MouseEnter_Effect;
            lblPhone.MouseUp += Functions.MouseLeave_Effect;
            lblPhone.Click += pnl_customer_Click;
            pnl_customer.Controls.Add(lblPhone);

            flp_customers.Controls.Add(pnl_customer);
        }
        public void viewCustomer(Customer c)
        {
            pbAvatar.Image = c.Avatar;
            lblName.Text = c.FirstName + " " + c.LastName;
            lblGenderAgeDoB.Text = c.Gender + ", " + c.CalculateAge() + " (" + c.DateofBirth.Day + "-" + c.DateofBirth.Month + "-" + c.DateofBirth.Year + ")";
            lblAddressLine1.Text = c.Address1;
            lblAddressLine2.Text = c.Address2;
            lblAddressLine3.Text = c.Address3;
            lblTelephoneMobile.Text = c.Telephone + " / " + c.Mobile;
            lblCity.Text = c.City;
            lblCountry.Text = c.Country;
            lblPostcode.Text = c.Postcode;
            lblState.Text = c.State;
            lblEmail.Text = c.Email;
            lblNotes.Text = c.Notes;

            lblTitle.Text = "Viewing Customer...";
            lblTitle.AccessibleName = "1";

            if (c != viewedCustomer)
                viewedCustomer = c;

            Tab.SelectTab("View");

        }
        public bool formCheck()
        {
            bool checker = true;
            if (txtFirstName.Text.Length <= 0)
            {
                checker = false;
                Functions.HighlightTextBox(txtFirstName);
            }

            if (txtEmail.Text.Length > 0)
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".") && txtEmail.Text.Replace("@", string.Empty).Replace(".", string.Empty).Length <= 0)
                    checker = false;

            return checker;
        }

        private void pnl_customer_Click(object sender, EventArgs e)
        {

            Control p = (Control)sender;
            //In case anything other than the main container is clicked on.
            if (!(p is Panel))
                p = p.Parent;

            viewCustomer(Collections.Customers.First(c => c.ID == Convert.ToInt32(p.AccessibleName)));
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
                    //Back To Customer 
                    Tab.SelectTab("Search");
                    lblTitle.Text = "Customers";
                    lblTitle.AccessibleName = "0";
                    Index();
                    break;

                case 2:
                    Tab.SelectTab("View");
                    lblTitle.Text = "Viewing Customer...";
                    lblTitle.AccessibleName = "1";
                    break;

                case 3:
                    this.Hide();
                    break;
                default:
                    MessageBox.Show("All I know is ... that this is supposed to go back somewhere :p");
                    break;
            }
            
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            flp_customers.Controls.Clear();
            foreach (Customer c in Collections.Customers)
            {
                foreach (PropertyInfo prop in c.GetType().GetProperties())
                {
                    if (prop.GetValue(c, null).ToString().ToLower().Contains(txtSearch.Text.ToLower()) || (c.FirstName.ToLower() + " " + c.LastName.ToLower()).Contains(txtSearch.Text.ToLower()))
                    {
                        paintCustomer(c);
                        break;
                    }
                }
            }
        }
        private void frmCustomers_Load(object sender, EventArgs e)
        {
            Functions.GiveBorder(Tab, this, Color.FromArgb(44, 62, 80));
        }
        private void genderToggle(object sender, EventArgs e)
        {
            if (lblMale.Text == "MALE")
            {
                lblMale.BackColor = Color.FromArgb(142, 68, 173);
                lblMale.Text = "FEMALE";
            }
            else
            {
                lblMale.BackColor = Color.FromArgb(74, 163, 223);
                lblMale.Text = "MALE";
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Editing Customer...";
            lblTitle.AccessibleName = "2";

            btnSubmit.Text = "SAVE";
            btnSubmit.BackColor = Color.FromArgb(41, 128, 185);
            btnDelete.Show();

            pbCreateAvatar.Image = viewedCustomer.Avatar;
            txtFirstName.Text = viewedCustomer.FirstName;
            txtLastName.Text = viewedCustomer.LastName;
            #region Gender Selection
            if (viewedCustomer.Gender == "Male")
            {
                lblMale.Text = "MALE";
                lblMale.BackColor = Color.FromArgb(74, 163, 223);
            } else
            {
                lblMale.BackColor = Color.FromArgb(142, 68, 173);
                lblMale.Text = "FEMALE";
            }
            #endregion
            dtp_dob.Value = viewedCustomer.DateofBirth;
            rtbAddressLine1.Text = viewedCustomer.Address1;
            rtbAddressLine2.Text = viewedCustomer.Address2;
            rtbAddressLine3.Text = viewedCustomer.Address3;
            txtTelephone.Text = viewedCustomer.Telephone;
            txtMobile.Text = viewedCustomer.Mobile;
            txtCity.Text = viewedCustomer.City;
            txtPostcode.Text = viewedCustomer.Postcode;
            txtCountry.Text = viewedCustomer.Country;
            txtState.Text = viewedCustomer.State;
            txtEmail.Text = viewedCustomer.Email;
            rtbNotes.Text = viewedCustomer.Notes;

            Tab.SelectTab("Create");
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to delete the customer '" + viewedCustomer.FirstName + " " + viewedCustomer.LastName + "'?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SQL.DeleteCustomer(viewedCustomer.ID);
                Tab.SelectTab("Search");
                lblTitle.Text = "Customers";
                Index();
                lblTitle.AccessibleName = "0";
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (formCheck())
            {
                if (btnSubmit.Text == "CREATE")
                {
                    //Create
                    Customer newCustomer = new Customer();
                    newCustomer.Avatar = pbCreateAvatar.Image;
                    newCustomer.FirstName = txtFirstName.Text;
                    newCustomer.LastName = txtLastName.Text;
                    newCustomer.Gender = lblMale.Text == "MALE" ? "Male" : "Female";
                    newCustomer.DateofBirth = dtp_dob.Value;
                    newCustomer.Address1 = rtbAddressLine1.Text;
                    newCustomer.Address2 = rtbAddressLine2.Text;
                    newCustomer.Address3 = rtbAddressLine3.Text;
                    newCustomer.Telephone = txtTelephone.Text;
                    newCustomer.Mobile = txtMobile.Text;
                    newCustomer.City = txtCity.Text;
                    newCustomer.Postcode = txtPostcode.Text;
                    newCustomer.Country = txtCountry.Text;
                    newCustomer.State = txtState.Text;
                    newCustomer.Email = txtEmail.Text;
                    newCustomer.Notes = rtbNotes.Text;

                   viewCustomer(SQL.CreateCustomer(newCustomer));
                   

                }
                else
                {

                    //Save
                    viewedCustomer.Avatar = pbCreateAvatar.Image;
                    viewedCustomer.FirstName = txtFirstName.Text;
                    viewedCustomer.LastName = txtLastName.Text;
                    viewedCustomer.Gender = lblMale.Text == "MALE" ? "Male" : "Female";
                    viewedCustomer.DateofBirth = dtp_dob.Value;
                    viewedCustomer.Address1 = rtbAddressLine1.Text;
                    viewedCustomer.Address2 = rtbAddressLine2.Text;
                    viewedCustomer.Address3 = rtbAddressLine3.Text;
                    viewedCustomer.Telephone = txtTelephone.Text;
                    viewedCustomer.Mobile = txtMobile.Text;
                    viewedCustomer.City = txtCity.Text;
                    viewedCustomer.Postcode = txtPostcode.Text;
                    viewedCustomer.Country = txtCountry.Text;
                    viewedCustomer.State = txtState.Text;
                    viewedCustomer.Email = txtEmail.Text;
                    viewedCustomer.Notes = rtbNotes.Text;

                    SQL.SaveCustomer(viewedCustomer);
                    //after saving, redirect to view.

                    viewCustomer(viewedCustomer);
                }
            }
            else
            {
                MessageBox.Show("Form validation error, please ensure the form is correct.", "Form Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            //Letting the code know the operation
            lblTitle.Text = "Creating Customer...";
            lblTitle.AccessibleName = "1";

            //Clearing & Resetting Form
            txtFirstName.Clear();
            txtLastName.Clear();
            rtbAddressLine1.Clear();
            rtbAddressLine2.Clear();
            rtbAddressLine3.Clear();
            txtMobile.Clear();
            txtTelephone.Clear();
            txtCity.Clear();
            txtPostcode.Clear();
            txtCountry.Clear();
            txtState.Clear();
            txtEmail.Clear();
            rtbNotes.Clear();

            btnSubmit.Text = "CREATE";
            Tab.SelectTab("Create");
            btnSubmit.BackColor = Color.FromArgb(39, 174, 96);
            btnDelete.Hide();
            pbCreateAvatar.Image = POS.Properties.Resources.unknown;
            dtp_dob.Value = DateTime.Today;
            
        }
        private void lblUploadImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            ofd.ShowDialog();

            if (ofd.FileName.Length > 0)
                pbCreateAvatar.Image = Image.FromFile(ofd.FileName);
            pbCreateAvatar.ImageLocation = ofd.FileName;
        }

        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            Router.rOrder.setCustomer(viewedCustomer.ID);
            this.Hide();
            
        }
    }
}