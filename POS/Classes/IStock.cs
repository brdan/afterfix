using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Classes
{
    public interface IStock
    {
        int ID { get; set; }
        string Name { get; set; }
        int SortDisplay { get; set; }
        int SortPrint { get; set; }
        Color Colour { get; set; }

    }

    public class Category : IStock
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SortDisplay { get; set; }
        public int SortPrint { get; set; }
        public Color Colour { get; set; }

        public Category()
        {
            ID = 0;
        }
    }
    public class Subcategory : IStock
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int SortDisplay { get; set; }
        public int SortPrint { get; set; }
        public Color Colour { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool AskPrice { get; set; }
        public decimal Weight { get; set; }
        public decimal WeightPrice { get; set; }
        public bool Reorder { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string PopupMsg { get; set; }
        public bool VAT { get; set; }
        public int InStockQty { get; set; }
        public Image Picture { get; set; }
        public int ReorderLevel { get; set; }


        public Subcategory()
        {
            CategoryID = 0;
        }
        public Category GetParent()
        {
            if (CategoryID != 0)
                return Collections.Categories.First(x => x.ID == CategoryID);
            else return null;
        }
    }
    public class Product : IStock
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int SubcategoryID { get; set; }
        public string Name { get; set; }
        public int SortDisplay { get; set; }
        public int SortPrint { get; set; }
        public Color Colour { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool AskPrice { get; set; }
        public decimal Weight { get; set; }
        public decimal WeightPrice { get; set; }
        public bool Reorder { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string PopupMsg { get; set; }
        public bool VAT { get; set; }
        public int InStockQty { get; set; }
        public Image Picture { get; set; }
        public int ReorderLevel { get; set; }


        public Product()
        {
            CategoryID = 0;
            SubcategoryID = 0;
        }
    }

}