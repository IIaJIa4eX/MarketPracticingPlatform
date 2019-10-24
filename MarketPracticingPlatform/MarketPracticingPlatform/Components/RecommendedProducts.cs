using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RecommendedProductsViewComponent : ViewComponent
    {

        DataBaseConnection db;

        public RecommendedProductsViewComponent(DataBaseConnection db)
        {
            this.db = db;

        }




        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            var additionalproducts = db.MainSubProducts.Where(f => f.MainProductId == id).Take(5);

            var prd = new List<Product>();

            foreach(var item in additionalproducts)
            {
                Product prtmp = db.Products.Where(f => f.ProductId == item.SubProductID).FirstOrDefault();
                if (prtmp != null)
                {
                    prd.Add(prtmp);
                }
            }

            return await Task.FromResult(View("RecommendedProducts", prd));

        }
        


    }

}
