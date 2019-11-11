using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DbCotext.DataBaseModels
{
    public class Category
    {

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentCategoryId { get; set; }



        public List<ProductCategory> ProductCategories { get; set; }

        public Category()
        {
            ProductCategories = new List<ProductCategory>();

        }

    }
}
