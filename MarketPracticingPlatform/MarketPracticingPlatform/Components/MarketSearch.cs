using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class MarketSearchViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            string categoryname = null;

            if (Request.Method == "POST")
            {
                categoryname = Request.Form["CategoryForSearch"];

            }

            //if (!string.IsNullOrWhiteSpace(categoryname))
            //{
            //    var cat = _db.Categories.Where(f => f.Name == categoryname).FirstOrDefault();
            //    if (cat != null)
            //    {
            //        var prdcat = _db.ProductCategories.Where(f => f.CategoryId == cat.CategoryId).ToList();
                    
            //        List<Product> prd = new List<Product>();

            //        foreach (var item in prdcat)
            //        {
            //            //prd.Add(db.Products.Where(f => f.ProductId == item.ProductId).FirstOrDefault());
            //        }


            //        return await Task.FromResult(View("ProductsSearch", prd));

            //    }

            //    ViewData["Message"] = "Такой категории нет";
            //    return await Task.FromResult(View("ProductsSearch"));

            //}

            ViewData["Message"] = "Введите название категории";
            return await Task.FromResult(View("ProductsSearch"));
        }

    }
}
