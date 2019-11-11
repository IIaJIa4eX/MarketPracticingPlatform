using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public string ProductDescription { get; set; }

        public int ProductPrice { get; set; }

        public int SoldOut { get; set; }

        public string Subproducts { get; set; }

        public string ProductManufacturerName { get; set; }
    }
}
