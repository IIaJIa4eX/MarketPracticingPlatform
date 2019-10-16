using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using MarketPracticingPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketPracticingPlatform.Controllers
{
    public class MarketController : Controller
    {

        DataBaseConnection db;

        public MarketController(DataBaseConnection db)
        {
            this.db = db;

        }


        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("Token"))
            {
                return View();
            }
            return RedirectToAction("Index", "Registration");
        }
     

        [HttpPost]
        public IActionResult ProductCreation(ProductDataHandler pdh)
        {


            Product prod = new Product();
            prod.Name = pdh.Name;
            prod.Description = pdh.Description;
            prod.Price = pdh.Price;
            prod.Manufacturer = pdh.Manufacturer;

            db.Products.Add(prod);
            db.SaveChanges();

            Category cat = db.Categories.Where(f => f.Name == pdh.Category).FirstOrDefault();

            ProductCategory prct = new ProductCategory();

            prct.CategoryId = cat.CategoryId;
            prct.ProductId = prod.ProductId;

            db.ProductCategories.Add(prct);
            db.SaveChanges();


            return View("Index");
        }

        [HttpPost]
        public IActionResult CategoryCreation(CategoryDataHandler cdh)
        {
            Category parentCat = db.Categories.Where(f => f.Name == cdh.ParentCategoryName).FirstOrDefault();

            Category cat = new Category();

            cat.Name = cdh.Name;
            cat.Description = cdh.Description;

            if (parentCat == null)
            {
                cat.ParentCategoryId = 0;
            }
            else
            {
                cat.ParentCategoryId = parentCat.CategoryId;
            }

            db.Categories.Add(cat);
            db.SaveChanges();

            return View("Index");
        }

    }
}