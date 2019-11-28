using MarketPracticingPlatform.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RecommendedProductsViewComponent : ViewComponent
    {

        readonly IProductDataService _GetProductServices;

        public RecommendedProductsViewComponent(IProductDataService GetProductServices)
        {
            _GetProductServices = GetProductServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int mainPrdId)
        {
            var prd = _GetProductServices.GetProductsByMainProductId(mainPrdId, 5);

            if(prd == null)
            {
                return await Task.FromResult(View("Error"));
            }

            return await Task.FromResult(View("RecommendedProducts", prd));
        }
    }
}
