using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class ProductInfoViewComponent : ViewComponent
    {

        readonly IProductDataService _GetProductServices;

        public ProductInfoViewComponent(IProductDataService GetProductServices)
        {
            _GetProductServices = GetProductServices;
        }




        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {

            Product prd = _GetProductServices.GetProductByID(productId);

            if(prd != null)
            {
                return await Task.FromResult(View("ProductInformation", prd));
            }

            return await Task.FromResult(View("ProductError"));
        }

    }
}
