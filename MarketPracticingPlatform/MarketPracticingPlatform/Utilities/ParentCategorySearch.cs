using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Utilities
{
    public  class ParentCategorySearch
    {


        DataBaseConnection db;// = new DataBaseConnection();

        public ParentCategorySearch(DataBaseConnection db)
        {
            this.db = db;

        }


        public List<int> arr = new List<int>();


        public List<int> GetCategoriesID(int id)
        {
            Category cat = new Category();

            if (id != 0)
            {
                cat = db.Categories.Where(f => f.CategoryId == id).FirstOrDefault();

                if (cat.ParentCategoryId != 0)
                {
                    arr.Add(cat.ParentCategoryId);

                    GetCategoriesID(cat.ParentCategoryId);
                }

                return arr;
            }

            return arr;
        }
    }
}
