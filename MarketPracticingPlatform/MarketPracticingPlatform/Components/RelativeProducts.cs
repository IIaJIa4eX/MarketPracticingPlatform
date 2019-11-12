using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RelativeProductsViewComponent : ViewComponent
    {

        Data.DataBaseConnection.DBConnection _db;

        public RelativeProductsViewComponent(Data.DataBaseConnection.DBConnection db)
        {
            this._db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            var prd = _db.Products.Where(f => f.ProductId == id).FirstOrDefault();

            var relativeProducts = _db.Products.Where(f => f.CategoryId == prd.CategoryId && f.ProductId != prd.ProductId).Take(5);

            return await Task.FromResult(View("RelativeProducts", relativeProducts));

        }

    }
}
