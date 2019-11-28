using System.Collections.Generic;

namespace MarketPracticingPlatform.Data.DataBaseModels
{
    public class Category
    {

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }



        public List<ProductCategory> ProductCategories { get; set; }

        public Category()
        {
            ProductCategories = new List<ProductCategory>();

        }

    }
}
