using System.Collections.Generic;

namespace MarketPracticingPlatform.Data.DataBaseModels
{
    public class Product
    {
        
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int SoldOut { get; set; }

        public string Manufacturer { get; set; } 

        public int CategoryId { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }


        public Product()
        {
            ProductCategories = new List<ProductCategory>();
        }


    }
}
