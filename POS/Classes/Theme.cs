using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Classes
{
    public class Theme
    {
        public string ThemeName { get; set; }

        public System.Drawing.Color PrimaryColour { get; set; }
        public System.Drawing.Color SecondaryColour { get; set; }
        public System.Drawing.Color InputColour { get; set; }
        public System.Drawing.Color FontColour { get; set; }
        public System.Drawing.Color CurrencyColour { get; set; }
        public System.Drawing.Color ButtonColour { get; set; }

        public Theme()
        {
            ThemeName = "Undefined";
        }

        public void ApplyTheme(Form f)
        {
            f.BackColor = PrimaryColour;
            foreach(TextBox txt in f.Controls)
            {
                f.BackColor = InputColour;
                f.ForeColor = FontColour;
            }

            foreach(TabPage tp in f.Controls)
            {
                tp.BackColor = SecondaryColour;
            }

            foreach(Button btn in f.Controls)
            {
                btn.BackColor = ButtonColour;
            }
           
            foreach(Label lbl in f.Controls)
            {
                lbl.ForeColor = FontColour;
            }
        }
    }
}
