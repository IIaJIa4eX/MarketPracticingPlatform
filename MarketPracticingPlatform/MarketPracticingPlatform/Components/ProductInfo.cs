using MarketPracticingPlatform.Data.DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class ProductInfoViewComponent : ViewComponent
    {

        Data.DataBaseConnection.DBConnection db;

        public ProductInfoViewComponent(Data.DataBaseConnection.DBConnection db)
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
