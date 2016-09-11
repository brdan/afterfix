using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Controls
{
    public partial class MsgGlobal : UserControl
    {
        int counter = 0; int waitTime = 300;  bool isClosing = false; bool clickCancel = false;
        public MsgGlobal()
        {
            InitializeComponent();
          
        }
        private void MsgGlobal_Load(object sender, EventArgs e)
        {
            this.Location = new Point(1024, 583);
        }
        public void Tell(string msg, int setWait = 300, int type = 0)
        {
            switch(type)
            {
                case 0: // general notification
                    lblIcon.Text = "i";
                    lblIcon.ForeColor = Color.FromArgb(34, 52, 70);
                    lblIcon.BackColor = Color.FromArgb(14, 32, 50);
                    BackColor = Color.FromArgb(4, 22, 40);
                    break;

                case 1: // error 
                    lblIcon.Text = "X";
                    lblIcon.ForeColor = Color.FromArgb(162, 37, 13);
                    lblIcon.BackColor = Color.FromArgb(182, 47, 33);
                    BackColor = Color.FromArgb(192, 57, 43);
                    break;

                case 2: // Success
                    lblIcon.Text = "O";
                    lblIcon.ForeColor = Color.FromArgb(1, 120, 93);
                    lblIcon.BackColor = Color.FromArgb(1, 130, 103);
                    BackColor = Color.FromArgb(12, 150, 123);
                    break;

                default:
                    break;
            }
            lblMsg.Text = msg;
            counter = 0; isClosing = false; waitTime = setWait;
            tmrAnimate.Start();
        }
        private void tmrAnimate_Tick(object sender, EventArgs e)
        {
            if (!clickCancel) // if the msg hasn't been clicked on (Cancelled) yet
            {
                if (this.Location.X > 736 && !isClosing)
                {
                    this.Location = new Point(this.Location.X - 8, this.Location.Y);
                }
                else
                {
                    if (counter < waitTime)
                    {
                        counter++;
                        isClosing = true;
                    }
                    else
                    {
                        //go back in
                        if (this.Location.X < 1024 && isClosing)
                        {
                            this.Location = new Point(this.Location.X + 8, this.Location.Y);
                        }
                    }
                }
            }
            else
            {
                //go back in
                if (this.Location.X < 1024 && isClosing)
                {
                    this.Location = new Point(this.Location.X + 8, this.Location.Y);
                }
            }
        }
        private void click(object sender, EventArgs e)
        {
            clickCancel = true;
        }
    }
}
