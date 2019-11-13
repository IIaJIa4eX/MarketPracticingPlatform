using MarketPracticingPlatform.Data.DataBaseConnection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPracticingPlatform.Sevice.Services.MarketService.CategoryService
{
    public class CategoryDataService : ICategoryDataService
    {
        readonly DBConnection _db;

        public CategoryDataService(DBConnection db)
        {
            _db = db;
        }

    }
}
