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
        int counter = 0; bool isClosing = false; bool clickCancel = false;
        public MsgGlobal()
        {
            InitializeComponent();
          
        }

        private void MsgGlobal_Load(object sender, EventArgs e)
        {
            this.Location = new Point(1024, 583);
        }

        public void Tell(string msg, int code = 1)
        {
            char icon = 'i';
            switch (code)
            {
                case 1:
                    icon = 'i';
                    break;
                default:
                    break;
            }

            lblIcon.Text = icon.ToString();
            lblMsg.Text = msg;
            counter = 0; isClosing = false;
            tmrAnimate.Start();
        }

        private void tmrAnimate_Tick(object sender, EventArgs e)
        {
            if (!clickCancel)
            {
                if (this.Location.X > 736 && !isClosing)
                {
                    this.Location = new Point(this.Location.X - 8, this.Location.Y);
                }
                else
                {
                    if (counter < 300)
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
