﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Service.ModelsDTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string ParentCategoryName { get; set; }

        public string Description { get; set; }

        public int ParentCategoryId { get; set; }
    }
}