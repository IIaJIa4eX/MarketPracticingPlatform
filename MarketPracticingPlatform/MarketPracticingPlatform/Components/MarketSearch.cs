using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class MarketSearchViewComponent : ViewComponent
    {

        ICategoryDataService _GetCategoryServices;

        public MarketSearchViewComponent(ICategoryDataService CategoryServices)
        {
            _GetCategoryServices = CategoryServices;
        }



        public async Task<IViewComponentResult> InvokeAsync()
        {

            string categoryname = null;

            if (Request.Method == "POST")
            {
                categoryname = Request.Form["CategoryForSearch"];

            }

            CategorySearchDTO tmp = _GetCategoryServices.SearchByCategoryName(categoryname);


            return await Task.FromResult(View("ProductsSearch", tmp));
        }

    }
}
