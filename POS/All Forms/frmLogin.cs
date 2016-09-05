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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        public bool Index(bool popup = false)
        {
            if (popup)
                this.ShowDialog();
            else
                this.Show();

            return true;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Functions.GiveBorder(Tab, this, Color.FromArgb(34, 52, 70));
            btnPassword.PerformClick();
        }

        private void btns_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btnPassword.BackColor = Color.FromArgb(34, 52, 70);
            btnSwipe.BackColor = Color.FromArgb(34, 52, 70);
            btnPin.BackColor = Color.FromArgb(34, 52, 70);
            btn.BackColor = Color.FromArgb(24, 42, 60);
            lblTitle.Text = btn.Name.Substring(3, btn.Name.Length - 3).ToUpper();
            Tab.SelectTab(btn.Name.Substring(3, btn.Name.Length - 3));

        }

        private void Password_Click(object sender, EventArgs e)
        {

        }

        private void pinNum(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "X":
                    txtPin.Clear();
                    break;
                case "OK":
                    if (txtPin.Text.Length > 0)
                    {
                        User u = Functions.VerifyUserPIN(Convert.ToInt32(txtPin.Text));
                        if (u)
                        {
                            //set as logged in user
                            Collections.CurrentUser = u;

                            Router.Dashboard();
                            Thread.Sleep(50);
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Couldn't sign you in - user did not verify!", "Verification Failed");
                        }
                    }
                    break;


                default:
                    if (txtPin.Text.Length < 4)
                        txtPin.AppendText(btn.Text);
                    break;
            }
        }
    }
}