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
            Category cat = db.Categories.Where(f => f.Name == pdh.Category).FirstOrDefault();


            Product prod = new Product();
            prod.Name = pdh.Name;
            prod.Description = pdh.Description;
            prod.Price = pdh.Price;
            prod.Manufacturer = pdh.Manufacturer;
            prod.CategoryId = cat.CategoryId;

            db.Products.Add(prod);
            db.SaveChanges();


           // int parentid = cat.CategoryId;
            ProductCategory prdct = new ProductCategory();
            Category cattmp = new Category();


            //if (cat.ParentCategoryId == 0)
            //{
            //    prdct.CategoryId = cat.CategoryId;
            //    prdct.ProductId = prod.ProductId;

            //    db.ProductCategories.Add(prdct);
            //    db.SaveChanges();
            //}
            //else
            //{
            //    while (parentid != 0)
            //    {
            //        cattmp = db.Categories.Where(f => f.CategoryId == parentid).FirstOrDefault();

            //        prdct.CategoryId = cattmp.CategoryId;
            //        prdct.ProductId = prod.ProductId;

            //        parentid = cattmp.ParentCategoryId;

            //        db.ProductCategories.Add(prdct);

            //        db.SaveChanges();

            //    }
            //}
            int parentid = ParentCategorySearch(cat.ParentCategoryId);
            if (parentid == 0)
            {
                prdct.CategoryId = cat.CategoryId;
                    prdct.ProductId = prod.ProductId;

                    db.ProductCategories.Add(prdct);
                    db.SaveChanges();
            }



            return View("Index");
        }

        public int ParentCategorySearch(int id)
        {

            Category prid = db.Categories.FromSql($"SELECT * FROM Categories WHERE CategoryId = {id}").FirstOrDefault();

            if(prid.ParentCategoryId != 0)
            {
                 
                 return ParentCategorySearch(prid.ParentCategoryId);
            }

            return 0;
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
            prd.Name = prdDTO.Name;
            prd.Description = prdDTO.Description;
            prd.Manufacturer = prdDTO.Manufacturer;
            prd.Price = prdDTO.Price;

            string[] ss = new string[] { };

            if (!string.IsNullOrWhiteSpace(prdDTO.Subproducts))
            {
                ss = prdDTO.Subproducts.Trim().Split(",");
            }

            prd.CategoryId = db.Categories.Where(f => f.Name == prdDTO.Category).Select(x => x.CategoryId).FirstOrDefault();
            db.Update(prd);
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
                    List<MainSub_Products> mnsp = db.MainSubProducts.Where(f => f.MainProductId == prd.ProductId).ToList();
                    db.MainSubProducts.RemoveRange(mnsp);
                    db.SaveChanges();

                }

            }

            List<MainSub_Products> mnsbp = new List<MainSub_Products>();
            foreach (var id in ss)
            {
                mnsbp.Add(new MainSub_Products { MainProductId = prd.ProductId, SubProductID = Int32.Parse(id) });

            }

            db.MainSubProducts.AddRange(mnsbp);
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