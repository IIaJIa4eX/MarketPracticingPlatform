using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPracticingPlatform.Controllers
{
    public class MarketSearchController : Controller
    {
        DataBaseConnection db;


        public MarketSearchController(DataBaseConnection db)
        {
            this.db = db;
        }


     
        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("Token"))
            {
                return View();
            }
            return RedirectToAction("Index","Registration");
        }

        [HttpPost]
        public IActionResult SearchByCategory(string category)
        {

            if (!string.IsNullOrWhiteSpace(category))
            {
                Category cat = db.Categories.Where(f => f.Name == category).FirstOrDefault();
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

                    return View("Index", prd);

                }


                return View("Index");

            }


            return View("Index");
        }
    }
}