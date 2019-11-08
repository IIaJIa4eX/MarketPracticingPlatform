﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int SoldOut { get; set; }

        public string Subproducts { get; set; }

        public string Manufacturer { get; set; }
    }
}
