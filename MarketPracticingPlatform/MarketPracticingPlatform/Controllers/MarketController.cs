using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using MarketPracticingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPracticingPlatform.Controllers
{
    [Route("[controller]/[action]")]
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
        public IActionResult ProductCreation(ProductDTO pdh)
        {
            Category cat = db.Categories.Where(f => f.Name == pdh.CategoryName).FirstOrDefault();


            Product prod = new Product();
            prod.Name = pdh.ProductName;
            prod.Description = pdh.ProductDescription;
            prod.Price = pdh.ProductPrice;
            prod.Manufacturer = pdh.ProductManufacturerName;
            prod.CategoryId = cat.CategoryId;

            db.Products.Add(prod);
            db.SaveChanges();


            int parentid = cat.ParentCategoryId;

            if (cat.ParentCategoryId == 0)
            {

                db.ProductCategories.Add(new ProductCategory { CategoryId = cat.CategoryId, ProductId = prod.ProductId });

            }
            else
            {
                Category cattmp = new Category();

                while (parentid != 0)
                {
                    cattmp = db.Categories.Where(f => f.CategoryId == parentid).FirstOrDefault();

                    db.ProductCategories.Add(new ProductCategory { CategoryId = cattmp.CategoryId, ProductId = prod.ProductId });

                    parentid = cattmp.ParentCategoryId;                                 
                }

                db.ProductCategories.Add(new ProductCategory { CategoryId = cat.CategoryId, ProductId = prod.ProductId });

            }

            db.SaveChanges();

            return View("Index");
        }

       

        [HttpPost]
        public IActionResult CategoryCreation(CategoryDTO cdh)
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

        [HttpPost]
        public IActionResult EditProduct(ProductDTO prdDTO)
        {
            Product prd = db.Products.Where(f => f.ProductId == prdDTO.ProductId).FirstOrDefault();

            prd.ProductId = prdDTO.ProductId;
            prd.Name = prdDTO.ProductName;
            prd.Description = prdDTO.ProductDescription;
            prd.Manufacturer = prdDTO.ProductManufacturerName;
            prd.Price = prdDTO.ProductPrice;

            string[] ss = new string[] { };

            if (!string.IsNullOrWhiteSpace(prdDTO.Subproducts))
            {
                ss = prdDTO.Subproducts.Trim().Split(",");
            }

            prd.CategoryId = db.Categories.Where(f => f.Name == prdDTO.CategoryName).Select(x => x.CategoryId).FirstOrDefault();

            db.SaveChanges();

            var msp = db.MainSubProducts.Where(f => f.MainProductId == prd.ProductId).Select(x => x.SubProductID.ToString()).ToArray();

            if (msp.Count() != 0)
            {

                if (ss.SequenceEqual(msp))
                {
                    return RedirectToAction("Index", "MarketSearch");
                }
                else
                {

                    string query = "DELETE FROM `mainsubproducts` WHERE `MainProductId` = {0}";
                    db.Database.ExecuteSqlCommand(query, prd.ProductId);

                }

            }

            foreach (var id in ss)
            {

                db.MainSubProducts.Add(new MainSub_Products { MainProductId = prd.ProductId, SubProductID = Int32.Parse(id) });

            }

            db.SaveChanges();

            return RedirectToAction("Index", "MarketSearch"); ;
        }



        [HttpPost]
        public IActionResult ShowProductEdit(ProductDTO pdh)
        {

            if (pdh.ProductId != 0)
            {
                if (Request.Cookies.ContainsKey("Token"))
                {
                    ViewBag.Productid = pdh.ProductId;
                    return View("ProductEditInformation");
                }

                return RedirectToAction("Index", "Registration");
            }

            return View("Index");
        }


        [HttpPost]
        public IActionResult DeleteProduct(int ProductId)
        {
            Product prd = db.Products.Where(f => f.ProductId == ProductId).FirstOrDefault();

            db.Products.Remove(prd);

            db.SaveChanges();

            return RedirectToAction("Index","MarketSearch");
        }

    }
}