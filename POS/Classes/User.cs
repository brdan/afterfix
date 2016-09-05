using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Classes
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PIN { get; set; }

        public User()
        {
            ID = 0;
        }

        public static bool operator true(User user)
        {
            return (user.ID != 0);
        }
        public static bool operator false(User user)
        {
            return (user.ID == 0);
        }
    }
}
