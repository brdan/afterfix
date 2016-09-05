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
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace POS
{
    public partial class frmSplash : Form
    {

        public frmSplash()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {            
            SQL.Init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value < 300)
            {
                progressBar1.Value += 1;
            } else
            {
                timer1.Stop();
                
                Router.Login();
                Thread.Sleep(50);
                this.Hide();
            }
        }
    }
}
