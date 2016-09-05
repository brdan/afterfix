using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Classes
{
    public class Customer
    {
        //Ensure additional fields are added in chronological order OR with matching order to Init() as it is expected to behave so.
        //Basically properties loop as 'i', which also fits into the dr as dr[i], so there's a problem if there's a chronological mismatch <:

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public Image Avatar { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }

        public int CalculateAge()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - DateofBirth.Year;
            if (now < DateofBirth.AddYears(age)) age--;

            return age;
        }
    }
}