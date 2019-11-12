using MarketPracticingPlatform.Data.DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class ProductInfoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int productid)
        {

            Product prd = null;// db.Products.Where(f => f.ProductId == productid).FirstOrDefault();
            if(prd != null)
            {
                return await Task.FromResult(View("ProductInformation", prd));
            }

            return await Task.FromResult(View("ProductError"));
        }

    }
}
