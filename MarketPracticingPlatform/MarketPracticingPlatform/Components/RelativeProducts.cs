using MarketPracticingPlatform.Data.DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RelativeProductsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

           // var prd = _db.Products.Where(f => f.ProductId == id).FirstOrDefault();

            var relativeProducts = new List<Product>();// _db.Products.Where(f => f.CategoryId == prd.CategoryId && f.ProductId != prd.ProductId).Take(5);

            return await Task.FromResult(View("RelativeProducts", relativeProducts));

        }

    }
}
