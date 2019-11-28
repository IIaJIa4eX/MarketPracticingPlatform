using MarketPracticingPlatform.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RelativeProductsViewComponent : ViewComponent
    {


        readonly IProductDataService _GetProductServices;

        public RelativeProductsViewComponent(IProductDataService GetProductServices)
        {
            _GetProductServices = GetProductServices;
        }



        public async Task<IViewComponentResult> InvokeAsync(int id)
        {



            var relativeProducts = _GetProductServices.GetRelativeProductsByProductId(id, 5);

            return await Task.FromResult(View("RelativeProducts", relativeProducts));

        }

    }
}
