using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Classes
{
    public static class Collections
    {
        public static User CurrentUser;

        public static List<User> Users = new List<User>();
        public static List<Customer> Customers = new List<Customer>();
        public static List<Category> Categories = new List<Category>();
        public static List<Subcategory> Subcategories = new List<Subcategory>();
        public static List<Product> Products = new List<Product>();
        public static List<Theme> Themes = new List<Theme>();
        public static List<Order> Orders = new List<Order>();
        public static List<OrderItem> OrderItems = new List<OrderItem>();
        public static List<Cart> Carts = new List<Cart>();

    }
}
