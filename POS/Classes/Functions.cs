using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace POS.Classes
{
    public static class Functions
    {
        //Graphics 
        public static async void HighlightTextBox(TextBox txt)
        {
            SolidBrush myBrush = new SolidBrush(Color.Red);
            Graphics formGraphics;
            formGraphics = txt.Parent.Parent.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(txt.Parent.Location.X - 1, txt.Parent.Location.Y - 1, txt.Parent.Width + 2, txt.Parent.Height + 2));
            
            await Task.Delay(3000);
            txt.Parent.Parent.Invalidate();

        }
        public static void GiveBorder(TabControl p, Control c, Color f)
        {
            //Left
            Color rgb = f;
            Panel pnlLeft = new Panel();
            pnlLeft.BackColor = rgb;
            pnlLeft.Size = new Size(5, p.Height);
            pnlLeft.Location = new Point(p.Location.X, p.Location.Y);
            c.Controls.Add(pnlLeft);
            pnlLeft.BringToFront();

            //Top
            Panel pnlTop = new Panel();
            pnlTop.BackColor = rgb;
            pnlTop.Size = new Size(p.Width, 22);
            pnlTop.Location = new Point(p.Location.X, p.Location.Y);
            c.Controls.Add(pnlTop);
            pnlTop.BringToFront();

            //Right
            Panel pnlRight = new Panel();
            pnlRight.BackColor = rgb;
            pnlRight.Size = new Size(5, p.Height);
            pnlRight.Location = new Point(p.Location.X + p.Width - 5, p.Location.Y);
            c.Controls.Add(pnlRight);
            pnlRight.BringToFront();

            //Bottom
            Panel pnlBottom = new Panel();
            pnlBottom.BackColor = rgb;
            pnlBottom.Size = new Size(p.Width, 5);
            pnlBottom.Location = new Point(p.Location.X, p.Location.Y + p.Height - 5);
            c.Controls.Add(pnlBottom);
            pnlBottom.BringToFront();
        }
        public static void RoundifyPBX(ref PictureBox p)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, p.Width - 3, p.Height - 3);
            Region rg = new Region(gp);
            p.Region = rg;
        }
        public static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public static void DisableTextBox(TextBox txt)
        {
        
        }

        // Utilities
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static void MouseEnter_Effect(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            if (!(c is Panel))
                c = c.Parent;
            c.BackColor = Color.FromArgb(14, 32, 50);
        }
        public static void MouseLeave_Effect(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            if (!(c is Panel))
                c = c.Parent;
            c.BackColor = Color.FromArgb(24, 42, 60);
        }
        public static IStock GetParent(IStock stock, bool deep = false)
        {
            IStock toReturn = null;
            if (stock.GetType() == typeof(Product) || stock.GetType() == typeof(Subcategory))
            {
                if (stock.GetType() == typeof(Subcategory))
                {
                    toReturn = Collections.Categories.Where(c => c.ID.ToString() == GetPropValue(stock, "CategoryID").ToString()).ToList().Count > 0 ? Collections.Categories.First(c => c.ID.ToString() == GetPropValue(stock, "CategoryID").ToString()) : null;
                }
                else if (stock.GetType() == typeof(Product))
                {
                    if (deep)
                    {
                        toReturn = Collections.Categories.Where(c => c.ID.ToString() == GetPropValue(stock, "CategoryID").ToString()).ToList().Count > 0 ? Collections.Categories.First(c => c.ID.ToString() == GetPropValue(stock, "CategoryID").ToString()) : null;
                    }
                    else
                    {
                        toReturn = Collections.Subcategories.Where(sc => sc.ID.ToString() == GetPropValue(stock, "SubCategoryID").ToString()).ToList().Count > 0 ? Collections.Subcategories.First(sc => sc.ID.ToString() == GetPropValue(stock, "SubcategoryID").ToString()) : null;
                    }
                }
            }
           
            return toReturn;
        }
        public static List<IStock> GetChildren(IStock c)
        {
            List<IStock> children = new List<IStock>();
            if (c is Category)
            {
                foreach (Subcategory sc in Collections.Subcategories.Where(x => x.CategoryID == c.ID))
                    children.Add(sc);

                foreach (Product pr in Collections.Products.Where(x => x.CategoryID == c.ID))
                    children.Add(pr);
        
            }
            else if (c is Subcategory)
            {
                foreach (Product pr in Collections.Products.Where(x => x.SubcategoryID == c.ID))
                    children.Add(pr);
            }
            return children;
        }
        public static string Price(decimal p)
        {

            return Math.Round(p, 2).ToString("0.00");
        }

        //Verifiers 
        public static bool VerifyUserUPass(string Username, string Password)
        {
            
            return true;
        }
        public static bool VerifyUserCard(string CardString)
        {

            return true;
        }
        public static User VerifyUserPIN(int pin)
        {
            User user = new User();
            try
            {
                user = Collections.Users.First(u => u.PIN == pin);
            }
            catch (Exception)
            {
                //MessageBox.Show("User not verified properly."); doing this on the view-level
            }
            return user;
        }

        //String Modifiers
        public static string VerifyEmpty(string str)
        {
            if (str.Length <= 0)
                return "N/A";
            else
                return str;
        }
        public static List<SubItem> SubItemsToList(string str)
        {
            List<SubItem> sIs = null;

            if (str != "")
            { 
                /*
                 * mod@Modifier1@1.99^
                 * mod@Modifier2@2.99^
                 * mod@Modifier3@3.99^
                 * dis@Some Discount@1.00
                 */
                sIs = new List<SubItem>(); 
                string[] sub_item_info = str.Split('^');
                foreach (string info in sub_item_info)
                {
                    string type = info.Substring(0, 3);

                    SubItem sI = new SubItem();
                    sI.DiscountOrModifier = type == "dis" ? true : false;
                    sI.Description = info.Substring(type.Length + 1, (info.IndexOf('@', info.IndexOf('@') + 1) - 4));
                    sI.Price = Math.Round(Convert.ToDecimal(info.Substring(type.Length + sI.Description.Length + 2, info.Length - (type.Length + sI.Description.Length + 2))), 2, MidpointRounding.AwayFromZero);
                    sIs.Add(sI);
                }
            }
            return sIs;
        }
        public static string ListToSubItems(List<SubItem> subItems)
        {
            string str = "";
            foreach (SubItem sI in subItems)
            {
                if (sI.DiscountOrModifier)
                    str += "dis@";
                else str += "mod@";
                str += sI.Description + "@";
                str += sI.Price;

                if (!sI.Equals(subItems.Last()))
                    str += "^";
            }
            return str;
        }
        public static Dictionary<string, string> StringToDict(string str)
        {

            Dictionary<string, string> Payments = new Dictionary<string, string>();
            if (str != "")
            {
                //Cash|2.99^Voucher|2.30
                string[] payments = str.Split('^');

                foreach(string payment in payments)
                {
                    string key = payment.Substring(0, payment.IndexOf('|'));
                    string value = payment.Substring(key.Length + 1, payment.Length - (key.Length + 1));

                    Payments.Add(key, value);
                }
            }

            return Payments;
        }
        public static string DictToString(Dictionary<string,string> Dict)
        {
            string str = "";

            if (Dict.Keys.Count > 0)
                foreach (var pair in Dict)
                {
                    var key = pair.Key;
                    var value = pair.Value;

                    str += key + "|" + value;
                }

            return str;
        }


        public static string Monify (string money)
        {
            return Convert.ToDecimal(money).ToString("0#.00");
        }


    }
}
