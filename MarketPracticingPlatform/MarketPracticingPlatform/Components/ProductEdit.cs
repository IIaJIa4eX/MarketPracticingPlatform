using MarketPracticingPlatform.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class ProductEditViewComponent : ViewComponent
    {

        readonly IProductDataService _GetProductServices;

        public ProductEditViewComponent(IProductDataService GetProductServices)
        {
            _GetProductServices = GetProductServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var tmp = _GetProductServices.GetProductInfoForEdit(productId);


            if(tmp == null)
            {
                ViewBag.ErrorMessage = $"Продукта с id {productId} не существует";
                return await Task.FromResult(View("EditError"));
            }

            return await Task.FromResult(View("ProductEditInformation", tmp));

        }
    }

}
