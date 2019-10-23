using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class ProductInfoViewComponent : ViewComponent
    {

        DataBaseConnection db;

        public ProductInfoViewComponent(DataBaseConnection db)
        {
            this.db = db;
        }



        public async Task<IViewComponentResult> InvokeAsync(int productid)
        {

            Product prd = db.Products.Where(f => f.ProductId == productid).FirstOrDefault();
            if(prd != null)
            {
                return await Task.FromResult(View("ProductInformation", prd));
            }

            

            return await Task.FromResult(View("ProductError"));
        }



    }
}
