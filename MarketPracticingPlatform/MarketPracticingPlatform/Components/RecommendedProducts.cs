using MarketPracticingPlatform.Data.DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RecommendedProductsViewComponent : ViewComponent
    {

        Data.DataBaseConnection.DBConnection _db;

        public RecommendedProductsViewComponent(Data.DataBaseConnection.DBConnection db)
        {
            this._db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            var additionalproducts = _db.MainSubProducts.Where(f => f.MainProductId == id).Take(5);

            var prd = new List<Product>();

            foreach(var item in additionalproducts)
            {
                Product prtmp = _db.Products.Where(f => f.ProductId == item.SubProductID).FirstOrDefault();
                if (prtmp != null)
                {
                    prd.Add(prtmp);
                }
            }

            return await Task.FromResult(View("RecommendedProducts", prd));

        }
        


    }

}
