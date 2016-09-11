using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace POS.Classes
{
    public class Order
    {
        //customer_id,order_type, amount_of_guests,visit_date,order_status,employee_id,table_id,total_price,total_paid
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string OrderType { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public int EmployeeID { get; set; }
        public int TableID { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPaid { get; set; }
        public Dictionary<string,string> PaymentMethods { get; set; }
        public string Notes { get; set; }
        public Dictionary<string,string> Discounts { get; set; }

        public Order(string type = "")
        {
            ID = 0;
            CustomerID = 0;
            OrderStatus = 1;
            OrderDate = DateTime.Today;
            TotalPaid = 0.00M;
            TableID = 0;
            NumberOfGuests = 0;
            OrderType = type;
            Notes = "";
            Discounts = null;
            PaymentMethods = new Dictionary<string, string>(); // we need to instantiate them at least, otherwise they dont exist
            Discounts = new Dictionary<string, string>();      // ""
        }

        public Cart MatchCart()
        {
            Cart ca = new Cart();
            try
            {
                ca = Collections.Carts.First(c => c.OrderID == ID);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Something is wrong with the cart for this order...", "Something is wrong", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                ca.OrderID = ID;
                Collections.Carts.Add(ca);
            }
            return ca;
        }
        public decimal SigmaDiscounts()
        {
            decimal order = 0;
            if (Discounts.Count > 0)
                foreach (var pair in Discounts.Values)
                    order += Convert.ToDecimal(pair);

            return order;
        }
    }

    public class OrderItem
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public decimal ItemPrice { get; set; }
        public List<SubItem> SubItems = new List<SubItem>();
        public decimal GetDiscount()
        {
            decimal sigma = 0;

            foreach (SubItem sI in SubItems)
                if (sI.DiscountOrModifier)
                    sigma += sI.Price;

            return sigma;
        }
    }

    public class Cart
    {
        public int OrderID { get; set; } //this keeps it unique, PK
        public List<OrderItem> Items = new List<OrderItem>();
        
        public decimal SigmaDiscounts()
        {
            decimal sigma = 0;
            foreach (OrderItem oI in Items)
                sigma += oI.GetDiscount();

            return sigma; //returns all item discounts in the cart
        }
    }

    public class SubItem
    {
        public bool DiscountOrModifier { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}