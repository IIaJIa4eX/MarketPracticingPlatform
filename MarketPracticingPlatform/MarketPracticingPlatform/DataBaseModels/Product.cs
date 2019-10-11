using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.DataBaseModels
{
    public class Product
    {
        
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool SoldOut { get; set; }

        public string Manufacturer { get; set; } 

        public List<ProductCategory> ProductCategories { get; set; }

        public Product()
        {
            ProductCategories = new List<ProductCategory>();
        }


    }
}
