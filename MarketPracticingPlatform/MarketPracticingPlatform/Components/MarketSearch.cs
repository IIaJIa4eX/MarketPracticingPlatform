using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class MarketSearchViewComponent : ViewComponent
    {

        DataBaseConnection db;

        public MarketSearchViewComponent(DataBaseConnection db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            string categoryname = null;

            if (Request.Method == "POST")
            {

                categoryname = Request.Form["CategoryForSearch"];


            }

            if (!string.IsNullOrWhiteSpace(categoryname))
            {
                Category cat = db.Categories.Where(f => f.Name == categoryname).FirstOrDefault();
                if (cat != null)
                {
                    var prdcat = db.ProductCategories.Where(f => f.CategoryId == cat.CategoryId);
                    Product temp = new Product();
                    List<Product> prd = new List<Product>();

                    foreach (var item in prdcat)
                    {
                        temp = db.Products.Where(f => f.ProductId == item.ProductId).FirstOrDefault();
                        prd.Add(temp);
                    }


                    return await Task.FromResult(View("ProductsSearch", prd));

                }

                ViewData["Message"] = "Такой категории нет";
                return await Task.FromResult(View("ProductsSearch"));

            }

            ViewData["Message"] = "Введите название категории";
            return await Task.FromResult(View("ProductsSearch"));
        }

    }
}
