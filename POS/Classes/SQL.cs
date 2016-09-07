using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS.Classes
{
    public static class SQL
    {
        /*as one of your MASTER refining, think of using single-functions and utilising the oftype() + interface features to 
         figure out which type the code is dealing with, and what its fields are :P*/
        static string cnxString = @"Data Source=MOHAMED-PC\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True";
        public static void Init()
        {
            //Initial Connection (Used for everything)

            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            
            //Emergency Start-Up Queries For Testing Should Start Here
            //using (SqlCommand cmd = new SqlCommand("UPDATE customers SET avatar=@img WHERE id=1", cnx))
            //{
            //    cmd.Parameters.Add(new SqlParameter("img", Functions.imageToByteArray(f)));
            //    cmd.ExecuteNonQuery();
            //    System.Windows.Forms.MessageBox.Show("Test");
            //}
            //And End Here---------------------------------------------


            //Users
            #region Fetching Users
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM users", cnx))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        User x = new User();
                        x.ID = Convert.ToInt32(dr[0]);
                        x.Username = dr[1].ToString();
                        x.Password = dr[2].ToString();
                        x.PIN = Convert.ToInt32(dr[3]);

                        Collections.Users.Add(x);
                    }
                }
            }
            #endregion

            //Customers
            #region Customers
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM customers", cnx))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        Customer x = new Customer();
                        int i = 0;
                        var allProps = x.GetType().GetProperties();
                        foreach (var prop in allProps)
                        {

                            //Just for data-modification needs
                            var data = dr[i];

                            //Image Exceptions
                            if (prop.PropertyType == typeof(Image))
                            {
                                try
                                {
                                    data = Functions.byteArrayToImage((byte[])data);
                                }
                                catch (Exception)
                                {
                                    //if anything happens, give it default image;
                                    data = POS.Properties.Resources.unknown;
                                }

                                prop.SetValue(x, data);
                            }

                            //Default import (string assumed, typically)
                            else
                                prop.SetValue(x, Convert.ChangeType(dr[i], prop.PropertyType));

                            //Incrementation
                            i++;
                        }
                        Collections.Customers.Add(x);
                    }
                }
            }
            #endregion

            //Categories
            #region Categories
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM categories", cnx))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Category x = new Category();
                        int i = 0;
                        var allProps = x.GetType().GetProperties();
                        foreach (var prop in allProps)
                        {
                            //Just for data-modification needs
                            var data = dr[i];

                            //Default import (string assumed, typically)

                            //if it's a colour
                            if (prop.PropertyType == typeof(Color))
                                data = Color.FromArgb(Convert.ToInt32(data));
                            prop.SetValue(x, Convert.ChangeType(data, prop.PropertyType));

                            //Incrementation
                            i++;
                        }
                        Collections.Categories.Add(x);
                    }
                }
            }
            #endregion

            //Subcategories
            #region Subcategories
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM subcategories", cnx))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Subcategory sc = new Subcategory();
                        int i = 0;
                        var allProps = sc.GetType().GetProperties();
                        foreach (var prop in allProps)
                        {
                            var data = dr[i];
                            if (prop.PropertyType == typeof(Image))
                            {
                                try
                                {
                                    data = Functions.byteArrayToImage((byte[])data);
                                }
                                catch (Exception)
                                {
                                    data = POS.Properties.Resources.unknown;
                                }
                                prop.SetValue(sc, data);
                            }
                            else if (prop.PropertyType == typeof(Color))
                            {
                                data = Color.FromArgb(Convert.ToInt32(data));
                                prop.SetValue(sc, Convert.ChangeType(data, prop.PropertyType));
                            }
                            else if (data.GetType() == typeof(DBNull))
                            {
                                data = "0";
                            }
                            else
                                prop.SetValue(sc, Convert.ChangeType(dr[i], prop.PropertyType));
                            i++;
                        }
                        Collections.Subcategories.Add(sc);
                    }
                }
            }
            #endregion

            //Products
            #region Products
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM products", cnx))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Product p = new Product();
                        int i = 0;
                        var allProps = p.GetType().GetProperties();
                        foreach (var prop in allProps)
                        {
                            var data = dr[i];
                            if (prop.PropertyType == typeof(Image))
                            {
                                try
                                {
                                    data = Functions.byteArrayToImage((byte[])data);
                                }
                                catch (Exception)
                                {
                                    data = POS.Properties.Resources.unknown;
                                }
                                prop.SetValue(p, data);
                            }
                            else if (prop.PropertyType == typeof(Color))
                            {
                                data = Color.FromArgb(Convert.ToInt32(data));
                                prop.SetValue(p, Convert.ChangeType(data, prop.PropertyType));
                            }
                            else if (data.GetType() == typeof(DBNull))
                            {
                                data = "0";
                            }
                            else
                                prop.SetValue(p, Convert.ChangeType(dr[i], prop.PropertyType));
                            i++;
                        }
                        Collections.Products.Add(p);
                    }
                }
            }
            #endregion

            //Settings
            #region Settings
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM settings", cnx))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Settings.Setting.Add(dr[0].ToString(), dr[1].ToString());
                    }
                }
            }
            #endregion

            //Themes (Commented-Out ...for now)
            #region Themes
            //using (SqlCommand cmd = new SqlCommand("SELECT * FROM themes", cnx))
            //{
            //    using (SqlDataReader dr = cmd.ExecuteReader())
            //    {
            //        while (dr.Read())
            //        {
            //            Theme t = new Theme();
            //            int i = 0;
            //            var allProps = t.GetType().GetProperties();
            //            foreach (var prop in allProps)
            //            {
            //                var data = dr[i];                      
            //                if (prop.PropertyType == typeof(Color))
            //                {
            //                    data = Color.FromArgb(Convert.ToInt32(data));
            //                    prop.SetValue(t, Convert.ChangeType(data, prop.PropertyType));
            //                }
            //                else if (data.GetType() == typeof(DBNull))
            //                {
            //                    data = "0";
            //                }
            //                else
            //                    prop.SetValue(t, Convert.ChangeType(dr[i], prop.PropertyType));
            //                i++;
            //            }
            //            Collections.Themes.Add(t);
            //        }
            //    }
            //}
            #endregion

            //The order items first - so I can assign them to carts, then orders when I load the order next
            #region Order Items
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM carts", cnx))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        OrderItem oI = new OrderItem();
                        oI.ID= Convert.ToInt32(dr["id"]);
                        oI.OrderID = Convert.ToInt32(dr["order_id"]);
                        oI.Qty = Convert.ToInt16(dr["qty"]);
                        oI.Description = dr["description"].ToString();
                        oI.ItemPrice = Convert.ToDecimal(dr["price"]);
                        oI.SubItems = Functions.ToList(dr["sub_items"].ToString());
                        Collections.OrderItems.Add(oI);
                    }
                }
            }
            #endregion

            /* Retrive orders independently of their carts, then match the carts with the order ID, 
            the carts should contain all items */
            #region Orders
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM orders", cnx))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Order x = new Order();
                        int i = 0;
                        var allProps = x.GetType().GetProperties();
                        foreach (var prop in allProps)
                        {
                            var data = dr[i];
                            if (data != DBNull.Value)
                            {
                                prop.SetValue(x, Convert.ChangeType(data, prop.PropertyType));
                            }
                            i++;
                        }
                        //Filter out its Cart
                        Cart c = new Cart();
                        c.OrderID = x.ID;
                        c.Items = Collections.OrderItems.Where(oi => oi.OrderID == c.OrderID).ToList();


                        Collections.Orders.Add(x);
                        Collections.Carts.Add(c);
                    }
                }
            }
            #endregion  

            cnx.Close();
        }

        static void loopCustomers()
        {
            foreach (Customer c in Collections.Customers)
                System.Windows.Forms.MessageBox.Show(String.Format("ID:{0}\r\nName:{1}", c.ID, c.FirstName));
        } //a dev-func :p you'll prolly wanna delete this after

        // Utils
        public static int NumRows(string query)
        {
            int rows = 0;
            using (SqlConnection cnx = new SqlConnection(cnxString))
            {
                cnx.Open();

                using (SqlDataReader dr = new SqlCommand(query, cnx).ExecuteReader())
                    while (dr.Read())
                        rows++;
                cnx.Close();
            }

            return rows;
        }

        // Create Functions
        public static Customer CreateCustomer(Customer c)
        {
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            Customer toReturn;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO customers(firstname,lastname,gender,dob,avatar,email,telephone,mobile,address1,address2,address3,city,postcode,state,country,notes) VALUES(@firstname,@lastname,@gender,@dob,@avatar,@email,@telephone,@mobile,@address1,@address2,@address3,@city,@postcode,@state,@country,@notes); SELECT SCOPE_IDENTITY();", cnx))
            {
                cmd.Parameters.Add(new SqlParameter("firstname", c.FirstName));
                cmd.Parameters.Add(new SqlParameter("lastname", c.LastName));
                cmd.Parameters.Add(new SqlParameter("gender", c.Gender));
                cmd.Parameters.Add(new SqlParameter("dob", c.DateofBirth));
                cmd.Parameters.Add(new SqlParameter("avatar", Functions.imageToByteArray(c.Avatar)));
                cmd.Parameters.Add(new SqlParameter("email", c.Email));
                cmd.Parameters.Add(new SqlParameter("telephone", c.Telephone));
                cmd.Parameters.Add(new SqlParameter("mobile", c.Mobile));
                cmd.Parameters.Add(new SqlParameter("address1", c.Address1));
                cmd.Parameters.Add(new SqlParameter("address2", c.Address2));
                cmd.Parameters.Add(new SqlParameter("address3", c.Address3));
                cmd.Parameters.Add(new SqlParameter("city", c.City));
                cmd.Parameters.Add(new SqlParameter("postcode", c.Postcode));
                cmd.Parameters.Add(new SqlParameter("state", c.State));
                cmd.Parameters.Add(new SqlParameter("country", c.Country));
                cmd.Parameters.Add(new SqlParameter("notes", c.Notes));

                c.ID = (int)(decimal)cmd.ExecuteScalar();

                //Add to objects with newly assigned ID
                Collections.Customers.Add(c);
                toReturn = c;
                System.Windows.Forms.MessageBox.Show("Customer created successfully!", "Customer created", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                //BE SURE TO TEST THOROUGHLY THAT IF A CUSTOMER WAS BEING EDITED AND WAS BEING HELD IN 'VIEWEDCUSTOMER' THAT THE CODE
                //ISN'T PASSING IT THROUGH BY REFERENCE AND CHANGING THE VALUES OF IT, BUT RATHER CREATING A NEW INSTANCE!!!! (this is important)

                //first run the program, edit a customer and make sure it's stored in 'viewedcustomer',
                //then create a customer and see if the ID of the first mentioned customer has changed.
            }

            c = null; //remember not to reference this after u nullify it because it no longer is set an an object reference, so it will crash.
            //loopCustomers(); - was for testing affected data after newly created records and to monitor object variable conflicts etc.
            cnx.Close();
            return toReturn;
        }
        public static Category CreateCategory(Category c)
        {
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            Category toReturn;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO categories(name, sort_display, sort_print, colour) VALUES(@name,@sort_display,@sort_print,@colour); SELECT SCOPE_IDENTITY();", cnx))
            {
                cmd.Parameters.Add(new SqlParameter("name", c.Name));
                cmd.Parameters.Add(new SqlParameter("sort_display", c.SortDisplay));
                cmd.Parameters.Add(new SqlParameter("sort_print", c.SortPrint));
                cmd.Parameters.Add(new SqlParameter("colour", c.Colour.ToArgb().ToString()));

                c.ID = (int)(decimal)cmd.ExecuteScalar();

                Collections.Categories.Add(c);
                toReturn = c;
                System.Windows.Forms.MessageBox.Show("Category created successfully!", "Category created", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            c = null;
            cnx.Close();
            return toReturn;
        }
        public static Subcategory CreateSubCategory(Subcategory sc)
        {
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            Subcategory toReturn;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO subcategories(category_id,name,sort_display,sort_print,colour,retail_price,cost_price,ask_price,weight,weight_price,reorder,expiry_date,popup_msg,vat,instock_qty,image,reorder_lvl) VALUES(@category_id,@name,@sort_display,@sort_print,@colour,@retail_price,@cost_price,@ask_price,@weight,@weight_price,@reorder,@expiry_date,@popup_msg,@vat,@instock_qty,@image, @reorder_lvl); SELECT SCOPE_IDENTITY()", cnx))
            {
                cmd.Parameters.Add(new SqlParameter("category_id", sc.CategoryID));
                cmd.Parameters.Add(new SqlParameter("name", sc.Name));
                cmd.Parameters.Add(new SqlParameter("sort_display", sc.SortDisplay));
                cmd.Parameters.Add(new SqlParameter("sort_print", sc.SortPrint));
                cmd.Parameters.Add(new SqlParameter("colour", sc.Colour.ToArgb().ToString()));
                cmd.Parameters.Add(new SqlParameter("retail_price", sc.RetailPrice));
                cmd.Parameters.Add(new SqlParameter("cost_price", sc.CostPrice));
                cmd.Parameters.Add(new SqlParameter("ask_price", sc.AskPrice));
                cmd.Parameters.Add(new SqlParameter("weight", sc.Weight));
                cmd.Parameters.Add(new SqlParameter("weight_price", sc.WeightPrice));
                cmd.Parameters.Add(new SqlParameter("reorder", sc.Reorder));
                cmd.Parameters.Add(new SqlParameter("expiry_date", sc.ExpiryDate));
                cmd.Parameters.Add(new SqlParameter("popup_msg", sc.PopupMsg));
                cmd.Parameters.Add(new SqlParameter("vat", sc.VAT));
                cmd.Parameters.Add(new SqlParameter("instock_qty", sc.InStockQty));
                cmd.Parameters.Add(new SqlParameter("image", Functions.imageToByteArray(sc.Picture)));
                cmd.Parameters.Add(new SqlParameter("reorder_lvl", sc.ReorderLevel));

                sc.ID = (int)(decimal)cmd.ExecuteScalar();

                Collections.Subcategories.Add(sc);
                toReturn = sc;
                System.Windows.Forms.MessageBox.Show("Sub-Category created successfully!", "Sub-Category created", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            sc = null;
            cnx.Close();
            return toReturn;
        }
        public static Product CreateProduct(Product p)
        {
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            Product toReturn;
            using (SqlCommand cmd = new SqlCommand("INSERT INTO products(category_id,subcategory_id,name,sort_display,sort_print,colour,retail_price,cost_price,ask_price,weight,weight_price,reorder,expiry_date,popup_msg,vat,instock_qty,image) VALUES(@category_id,@subcategory_id,@name,@sort_display,@sort_print,@colour,@retail_price,@cost_price,@ask_price,@weight,@weight_price,@reorder,@expiry_date,@popup_msg,@vat,@instock_qty,@image); SELECT SCOPE_IDENTITY()", cnx))
            {
                cmd.Parameters.Add(new SqlParameter("category_id", p.CategoryID));
                cmd.Parameters.Add(new SqlParameter("subcategory_id", p.SubcategoryID));
                cmd.Parameters.Add(new SqlParameter("name", p.Name));
                cmd.Parameters.Add(new SqlParameter("sort_display", p.SortDisplay));
                cmd.Parameters.Add(new SqlParameter("sort_print", p.SortPrint));
                cmd.Parameters.Add(new SqlParameter("colour", p.Colour.ToArgb().ToString()));
                cmd.Parameters.Add(new SqlParameter("retail_price", p.RetailPrice));
                cmd.Parameters.Add(new SqlParameter("cost_price", p.CostPrice));
                cmd.Parameters.Add(new SqlParameter("ask_price", p.AskPrice));
                cmd.Parameters.Add(new SqlParameter("weight", p.Weight));
                cmd.Parameters.Add(new SqlParameter("weight_price", p.WeightPrice));
                cmd.Parameters.Add(new SqlParameter("reorder", p.Reorder));
                cmd.Parameters.Add(new SqlParameter("expiry_date", p.ExpiryDate));
                cmd.Parameters.Add(new SqlParameter("popup_msg", p.PopupMsg));
                cmd.Parameters.Add(new SqlParameter("vat", p.VAT));
                cmd.Parameters.Add(new SqlParameter("instock_qty", p.InStockQty));
                cmd.Parameters.Add(new SqlParameter("image", Functions.imageToByteArray(p.Picture)));
                cmd.Parameters.Add(new SqlParameter("reorder_lvl", Functions.imageToByteArray(p.Picture)));

                p.ID = (int)(decimal)cmd.ExecuteScalar();
                Collections.Products.Add(p);
                toReturn = p;
                System.Windows.Forms.MessageBox.Show("Product created successfully!", "Product created", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            p = null;
            cnx.Close();
            return toReturn;
        }
        public static Order CreateOrder(Order o, Cart c)
        {
            using (SqlConnection cnx = new SqlConnection(cnxString))
            {
                cnx.Open();

                #region Inserting the Order
                using (SqlCommand cmd = new SqlCommand("INSERT INTO orders(customer_id, order_type, number_of_guests, order_date, order_status, employee_id, table_id, total_price, total_paid, notes, discounts) VALUES(@customer_id, @order_type, @number_of_guests, @order_date, @order_status, @employee_id, @table_id, @total_price, @total_paid, @notes, @discounts) SELECT SCOPE_IDENTITY()", cnx))
                {
                    cmd.Parameters.Add(new SqlParameter("customer_id", o.CustomerID));
                    cmd.Parameters.Add(new SqlParameter("order_type", o.OrderType));
                    cmd.Parameters.Add(new SqlParameter("number_of_guests", o.NumberOfGuests));
                    cmd.Parameters.Add(new SqlParameter("order_date", o.OrderDate));
                    cmd.Parameters.Add(new SqlParameter("order_status", o.OrderStatus));
                    cmd.Parameters.Add(new SqlParameter("employee_id", o.EmployeeID));
                    cmd.Parameters.Add(new SqlParameter("table_id", o.TableID));
                    cmd.Parameters.Add(new SqlParameter("total_price", Functions.Monify(o.TotalPrice.ToString())));
                    cmd.Parameters.Add(new SqlParameter("total_paid", Functions.Monify(o.TotalPaid.ToString())));
                    cmd.Parameters.Add(new SqlParameter("notes", o.Notes));

                    if (o.Discounts != null)
                        cmd.Parameters.Add(new SqlParameter("discounts", o.Discounts));
                    else
                        cmd.Parameters.Add(new SqlParameter("discounts", DBNull.Value));

                    o.ID = (int)(decimal)cmd.ExecuteScalar();
                    Collections.Orders.Add(o);
                }

                #endregion

                #region Inserting the Cart

                foreach (OrderItem oI in c.Items.ToList())
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO carts(order_id,qty,description,price,sub_items) VALUES(@order_id,@qty,@description,@price,@sub_items) SELECT SCOPE_IDENTITY()", cnx))
                    {
                        cmd.Parameters.Add(new SqlParameter("order_id", o.ID));
                        cmd.Parameters.Add(new SqlParameter("qty", oI.Qty));
                        cmd.Parameters.Add(new SqlParameter("description", oI.Description));
                        cmd.Parameters.Add(new SqlParameter("price", Functions.Monify(oI.ItemPrice.ToString())));

                        string str = "";
                        foreach (SubItem sI in oI.SubItems)
                        {
                            //mod@Modifier1@1.99^mod@Modifier2@2.99^mod@Modifier3@3.99^dis@Some Discount@1.
                            if (sI.DiscountOrModifier)
                                str += "dis@";
                            else str += "mod@";
                            str += sI.Description + "@";
                            str += sI.Price;

                            if (!sI.Equals(oI.SubItems.Last()))
                                str += "^";
                        }
                        cmd.Parameters.Add(new SqlParameter("sub_items", str));
                        oI.ID = (int)(decimal)cmd.ExecuteScalar();
                        c.OrderID = o.ID;
                        Collections.Carts.Add(c);
                    }


                #endregion


                cnx.Close();
            }
            return o;
        }

        // Save Functions
        public static void SaveCustomer(Customer c)
        {
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE customers SET firstname=@firstname,lastname=@lastname,gender=@gender,dob=@dob,avatar=@avatar,email=@email,telephone=@telephone,mobile=@mobile,address1=@address1,address2=@address2,address3=@address3,city=@city,postcode=@postcode,state=@state,country=@country,notes=@notes WHERE id=@id", cnx))
                {
                    //The ID
                    cmd.Parameters.Add(new SqlParameter("id", c.ID));

                    cmd.Parameters.Add(new SqlParameter("firstname", c.FirstName));
                    cmd.Parameters.Add(new SqlParameter("lastname", c.LastName));
                    cmd.Parameters.Add(new SqlParameter("gender", c.Gender));
                    cmd.Parameters.Add(new SqlParameter("dob", c.DateofBirth));
                    cmd.Parameters.Add(new SqlParameter("avatar", Functions.imageToByteArray(c.Avatar)));
                    cmd.Parameters.Add(new SqlParameter("email", c.Email));
                    cmd.Parameters.Add(new SqlParameter("telephone", c.Telephone));
                    cmd.Parameters.Add(new SqlParameter("mobile", c.Mobile));
                    cmd.Parameters.Add(new SqlParameter("address1", c.Address1));
                    cmd.Parameters.Add(new SqlParameter("address2", c.Address2));
                    cmd.Parameters.Add(new SqlParameter("address3", c.Address3));
                    cmd.Parameters.Add(new SqlParameter("city", c.City));
                    cmd.Parameters.Add(new SqlParameter("postcode", c.Postcode));
                    cmd.Parameters.Add(new SqlParameter("state", c.State));
                    cmd.Parameters.Add(new SqlParameter("country", c.Country));
                    cmd.Parameters.Add(new SqlParameter("notes", c.Notes));

                    cmd.ExecuteNonQuery();

                    System.Windows.Forms.MessageBox.Show("Customer has been edited.", "Completed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("There was an error while trying to save this customer.\r\n" + ex.Message, "Customer not edited", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            cnx.Close();
        }
        public static void SaveCategory(Category c)
        {
            //First store it locally
            Category reference = Collections.Categories.First(x => x.ID == c.ID);
            reference.Name = c.Name;
            reference.Colour = c.Colour;
            reference.SortDisplay = c.SortDisplay;
            reference.SortPrint = c.SortPrint;


            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE categories SET name=@name,sort_display=@sort_display,@sort_print=@sort_print,colour=@colour WHERE id=" + c.ID, cnx))
            {
                cmd.Parameters.Add(new SqlParameter("name", c.Name));
                cmd.Parameters.Add(new SqlParameter("sort_display", c.SortDisplay));
                cmd.Parameters.Add(new SqlParameter("sort_print", c.SortPrint));
                cmd.Parameters.Add(new SqlParameter("colour", c.Colour.ToArgb().ToString()));

                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Category has been edited.", "Completed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                c = null;
            }
            cnx.Close();
        }
        public static void SaveSubCategory(Subcategory sc)
        {
            //First store it locally
            Subcategory reference = Collections.Subcategories.First(x => x.ID == sc.ID);
            reference.CategoryID = sc.CategoryID;
            reference.Name = sc.Name;
            reference.SortDisplay = sc.SortDisplay;
            reference.SortPrint = sc.SortPrint;
            reference.Colour = sc.Colour;
            reference.RetailPrice = sc.RetailPrice;
            reference.CostPrice = sc.CostPrice;
            reference.AskPrice = sc.AskPrice;
            reference.Weight = sc.Weight;
            reference.WeightPrice = sc.WeightPrice;
            reference.Picture = sc.Picture;
            reference.Reorder = sc.Reorder;
            reference.ExpiryDate = sc.ExpiryDate;
            reference.PopupMsg = sc.PopupMsg;
            reference.VAT = sc.VAT;
            reference.InStockQty = sc.InStockQty;
            reference.ReorderLevel = sc.ReorderLevel;


            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE subcategories SET category_id=@category_id,name=@name,sort_display=@sort_display,sort_print=@sort_print,colour=@colour,retail_price=@retail_price,cost_price=@cost_price,ask_price=@ask_price,weight=@weight,weight_price=@weight_price,reorder=@reorder,expiry_date=@expiry_date,popup_msg=@popup_msg,vat=@Vat,instock_qty=@instock_qty,image=@image,reorder_lvl=@reorder_lvl WHERE id=" + sc.ID, cnx))
            {
                cmd.Parameters.Add(new SqlParameter("category_id", sc.CategoryID));
                cmd.Parameters.Add(new SqlParameter("name", sc.Name));
                cmd.Parameters.Add(new SqlParameter("sort_display", sc.SortDisplay));
                cmd.Parameters.Add(new SqlParameter("sort_print", sc.SortPrint));
                cmd.Parameters.Add(new SqlParameter("colour", sc.Colour.ToArgb().ToString()));
                cmd.Parameters.Add(new SqlParameter("retail_price", sc.RetailPrice));
                cmd.Parameters.Add(new SqlParameter("cost_price", sc.CostPrice));
                cmd.Parameters.Add(new SqlParameter("ask_price", sc.AskPrice));
                cmd.Parameters.Add(new SqlParameter("weight", sc.Weight));
                cmd.Parameters.Add(new SqlParameter("weight_price", sc.WeightPrice));
                cmd.Parameters.Add(new SqlParameter("reorder", sc.Reorder));
                cmd.Parameters.Add(new SqlParameter("expiry_date", sc.ExpiryDate));
                cmd.Parameters.Add(new SqlParameter("popup_msg", sc.PopupMsg));
                cmd.Parameters.Add(new SqlParameter("vat", sc.VAT));
                cmd.Parameters.Add(new SqlParameter("instock_qty", sc.InStockQty));
                cmd.Parameters.Add(new SqlParameter("image", Functions.imageToByteArray(sc.Picture)));
                cmd.Parameters.Add(new SqlParameter("reorder_lvl", sc.ReorderLevel));


                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Sub-Category has been edited.", "Completed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                sc = null;
            }
            cnx.Close();
        }
        public static void SaveProduct(Product p)
        {
            //First store it locally
            Product reference = Collections.Products.First(x => x.ID == p.ID);
            reference.CategoryID = p.CategoryID;
            reference.SubcategoryID = p.SubcategoryID;
            reference.Name = p.Name;
            reference.SortDisplay = p.SortDisplay;
            reference.SortPrint = p.SortPrint;
            reference.Colour = p.Colour;
            reference.RetailPrice = p.RetailPrice;
            reference.CostPrice = p.CostPrice;
            reference.AskPrice = p.AskPrice;
            reference.Weight = p.Weight;
            reference.WeightPrice = p.WeightPrice;
            reference.Reorder = p.Reorder;
            reference.ExpiryDate = p.ExpiryDate;
            reference.Picture = p.Picture;
            reference.PopupMsg = p.PopupMsg;
            reference.VAT = p.VAT;
            reference.InStockQty = p.InStockQty;
            reference.ReorderLevel = p.ReorderLevel;


            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE products SET category_id=@category_id,subcategory_id=@subcategory_id,name=@name,sort_display=@sort_display,sort_print=@sort_print,colour=@colour,retail_price=@retail_price,cost_price=@cost_price,ask_price=@ask_price,weight=@weight,weight_price=@weight_price,reorder=@reorder,expiry_date=@expiry_date,popup_msg=@popup_msg,vat=@Vat,instock_qty=@instock_qty,image=@image WHERE id=" + p.ID, cnx))
            {
                cmd.Parameters.Add(new SqlParameter("category_id", p.CategoryID));
                cmd.Parameters.Add(new SqlParameter("subcategory_id", p.SubcategoryID));
                cmd.Parameters.Add(new SqlParameter("name", p.Name));
                cmd.Parameters.Add(new SqlParameter("sort_display", p.SortDisplay));
                cmd.Parameters.Add(new SqlParameter("sort_print", p.SortPrint));
                cmd.Parameters.Add(new SqlParameter("colour", p.Colour.ToArgb().ToString()));
                cmd.Parameters.Add(new SqlParameter("retail_price", p.RetailPrice));
                cmd.Parameters.Add(new SqlParameter("cost_price", p.CostPrice));
                cmd.Parameters.Add(new SqlParameter("ask_price", p.AskPrice));
                cmd.Parameters.Add(new SqlParameter("weight", p.Weight));
                cmd.Parameters.Add(new SqlParameter("weight_price", p.WeightPrice));
                cmd.Parameters.Add(new SqlParameter("reorder", p.Reorder));
                cmd.Parameters.Add(new SqlParameter("expiry_date", p.ExpiryDate));
                cmd.Parameters.Add(new SqlParameter("popup_msg", p.PopupMsg));
                cmd.Parameters.Add(new SqlParameter("vat", p.VAT));
                cmd.Parameters.Add(new SqlParameter("instock_qty", p.InStockQty));
                cmd.Parameters.Add(new SqlParameter("image", Functions.imageToByteArray(p.Picture)));
                cmd.Parameters.Add(new SqlParameter("reorder_lvl", p.ReorderLevel));


                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Product has been edited.", "Completed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                p = null;
            }
            cnx.Close();
        }
        public static void SaveOrder(Order o)
        {
            using (SqlConnection cnx = new SqlConnection(cnxString))
            {
                cnx.Open();

                #region Updating the Order
                using (SqlCommand cmd = new SqlCommand("UPDATE orders SET customer_id=@customer_id, order_type=@order_type, number_of_guests=@number_of_guests, order_date=@order_date, order_status=@order_status, employee_id=@employee_id, table_id=@table_id, total_price=@total_price, total_paid=@total_paid, notes=@notes, discounts=@discounts WHERE id=" + o.ID, cnx))
                {
                    cmd.Parameters.Add(new SqlParameter("customer_id", o.CustomerID));
                    cmd.Parameters.Add(new SqlParameter("order_type", o.OrderType));
                    cmd.Parameters.Add(new SqlParameter("number_of_guests", o.NumberOfGuests));
                    cmd.Parameters.Add(new SqlParameter("order_date", o.OrderDate));
                    cmd.Parameters.Add(new SqlParameter("order_status", o.OrderStatus));
                    cmd.Parameters.Add(new SqlParameter("employee_id", o.EmployeeID));
                    cmd.Parameters.Add(new SqlParameter("table_id", o.TableID));
                    cmd.Parameters.Add(new SqlParameter("total_price", Functions.Monify(o.TotalPrice.ToString())));
                    cmd.Parameters.Add(new SqlParameter("total_paid", Functions.Monify(o.TotalPaid.ToString())));
                    cmd.Parameters.Add(new SqlParameter("notes", o.Notes));

                    if (o.Discounts != null)
                        cmd.Parameters.Add(new SqlParameter("discounts", o.Discounts));
                    else
                        cmd.Parameters.Add(new SqlParameter("discounts", DBNull.Value));

                    cmd.ExecuteNonQuery();
                }

                #endregion

                #region Updating the Cart
                string query = null;
                foreach (OrderItem oI in o.getCart().Items.ToList())
                {
                    
                    if (oI.ID == 0)
                        query = "INSERT INTO carts(order_id, qty, description, price, sub_items) VALUES(@order_id, @qty, @description, @price, @sub_items) SELECT SCOPE_IDENTITY()";
                    else
                        query = "UPDATE carts SET qty=@qty,description=@description,price=@price,sub_items=@sub_items WHERE id=" + oI.ID + " AND order_id=" + o.ID;


                    using (SqlCommand cmd = new SqlCommand(query, cnx))
                    {
                        if (oI.ID == 0)
                            cmd.Parameters.Add(new SqlParameter("order_id", o.ID));

                        cmd.Parameters.Add(new SqlParameter("qty", oI.Qty));
                        cmd.Parameters.Add(new SqlParameter("description", oI.Description));
                        cmd.Parameters.Add(new SqlParameter("price", Functions.Monify(oI.ItemPrice.ToString())));

                        string str = "";
                        foreach (SubItem sI in oI.SubItems)
                        {
                            //mod@Modifier1@1.99^mod@Modifier2@2.99^mod@Modifier3@3.99^dis@Some Discount@1.
                            if (sI.DiscountOrModifier)
                                str += "dis@";
                            else str += "mod@";
                            str += sI.Description + "@";
                            str += sI.Price;

                            if (!sI.Equals(oI.SubItems.Last()))
                                str += "^";
                        }
                        cmd.Parameters.Add(new SqlParameter("sub_items", str));

                        if (oI.ID == 0)
                            oI.ID = (int)(decimal)cmd.ExecuteScalar();
                        else
                            cmd.ExecuteNonQuery();

                    }
                }

                #endregion

                cnx.Close();
            }
        }

        // Delete Functions
        public static void DeleteCustomer(int ID)
        {
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM customers WHERE id=@id", cnx))
                {
                    cmd.Parameters.Add(new SqlParameter("id", ID));
                    cmd.ExecuteNonQuery();
                }
                //now remove from collection
                Collections.Customers.Remove(Collections.Customers.First(c => c.ID == ID));
                System.Windows.Forms.MessageBox.Show("Customer deleted successfully!", "Customer Deleted", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("There was an error while trying to delete this customer. " + ex.Message);
            }
            cnx.Close();
        }
        public static void DeleteCategory(int ID)
        {
            //First locally
            Collections.Categories.Remove(Collections.Categories.First(n => n.ID == ID)); //remove from app-db

            //DB time
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            using(SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id=" + ID, cnx))
            {
                cmd.ExecuteNonQuery();
            }
            System.Windows.Forms.MessageBox.Show("Category has been deleted successfully", "Category deleted", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        public static void DeleteSubCategory(int ID)
        {
            //First locally
            Collections.Subcategories.Remove(Collections.Subcategories.First(n => n.ID == ID)); //remove from app-db

            //DB time
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM subcategories WHERE id=" + ID, cnx))
            {
                cmd.ExecuteNonQuery();
            }
            System.Windows.Forms.MessageBox.Show("Sub-category has been deleted successfully", "Sub-category deleted", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        public static void DeleteProduct(int ID)
        {
            //First locally
            Collections.Products.Remove(Collections.Products.First(n => n.ID == ID)); //remove from app-db

            //DB time
            SqlConnection cnx = new SqlConnection(cnxString); cnx.Open();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM products WHERE id=" + ID, cnx))
            {
                cmd.ExecuteNonQuery();
            }
            System.Windows.Forms.MessageBox.Show("Product has been deleted successfully", "Product deleted", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}