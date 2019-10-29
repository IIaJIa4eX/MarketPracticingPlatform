using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RelativeProductsViewComponent : ViewComponent
    {

        DataBaseConnection db;

        public RelativeProductsViewComponent(DataBaseConnection db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            Product prd = db.Products.Where(f => f.ProductId == id).FirstOrDefault();

            var relativeProducts = db.Products.Where(f => f.CategoryId == prd.CategoryId && f.ProductId != prd.ProductId).Take(5);

            return await Task.FromResult(View("RelativeProducts", relativeProducts));

        }

    }
}
