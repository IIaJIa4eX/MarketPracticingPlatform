using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Models
{
    public class ProductDataHandler
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int SoldOut { get; set; }

        public string Manufacturer { get; set; }
    }
}
