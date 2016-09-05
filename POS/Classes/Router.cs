using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

namespace POS.Classes
{
    public static class Router
    {
        static frmLogin rLogin = new frmLogin();
        static frmDashboard rDashboard = new frmDashboard();
        static frmCustomers rCustomers = new frmCustomers();
        static frmStock rStock = new frmStock();
        public static frmNewOrder rOrder = new frmNewOrder();

        // STATE CHANGERS ================
        public static bool frmCustFromOrder = false;


        public static bool Login()
        {
            return rLogin.Index();
        }
        public static bool Dashboard()
        {
            return rDashboard.Index();
        }
        public static bool Customers()
        {
            return rCustomers.Index();
        }

        public static bool StockManagement()
        {
            return rStock.Index();
        }

        public static frmNewOrder NewOrder()
        {
            return rOrder.Index();
        }
    }
}