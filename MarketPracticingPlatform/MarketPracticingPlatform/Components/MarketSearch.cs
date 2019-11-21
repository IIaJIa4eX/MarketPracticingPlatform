using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System;
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


            if (Request.Method == "POST")
            {

                if (Request.Form.ContainsKey("CategoryForSearch"))
                {
                    string categoryName = Request.Form["CategoryForSearch"];
                    
                    if (!string.IsNullOrWhiteSpace(categoryName))
                    {
                        CategorySearchDTO tmp = _GetCategoryServices.SearchProductsByCategoryName(categoryName);
                        return await Task.FromResult(View("ProductsSearch", tmp));
                    }
                }

                if(Request.Form.ContainsKey("CategoryId"))
                {
                    int categoryId = Int32.Parse(Request.Form["CategoryId"]);

                    if (categoryId != 0)
                    {
                        CategorySearchDTO tmp = _GetCategoryServices.SearchProductsByCategoryId(categoryId);
                        return await Task.FromResult(View("ProductsSearch", tmp));
                    }
                }
                
            }

            CategorySearchDTO def = new CategorySearchDTO() { BackRequestMessageInfo = "Введите или выберите название категории"};

            return await Task.FromResult(View("ProductsSearch", def));
        }

    }
}
