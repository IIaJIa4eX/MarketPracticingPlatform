﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using MarketPracticingPlatform.Models;
using MarketPracticingPlatform.Utilities;
using Microsoft.AspNetCore.Mvc;

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



        //public IActionResult ProductCreation(ProductDTO pdh)
        //{
        //    ParentCategorySearch pcs = new ParentCategorySearch(db);

        //    Product prod = new Product();
        //    prod.Name = pdh.Name;
        //    prod.Description = pdh.Description;
        //    prod.Price = pdh.Price;
        //    prod.Manufacturer = pdh.Manufacturer;

        //    db.Products.Add(prod);
        //    db.SaveChanges();


        //    Category cat = db.Categories.Where(f => f.Name == pdh.Category).FirstOrDefault();

        //    List<int> catids = pcs.GetCategoriesIDs(cat.CategoryId);

        //    if (catids.Count == 0)
        //    {
        //        ProductCategory prdct = new ProductCategory();

        //        prdct.CategoryId = cat.CategoryId;
        //        prdct.ProductId = prod.ProductId;

        //        db.ProductCategories.AddRange(prdct);

        //        db.SaveChanges();
        //    }
        //    else
        //    {

        //        var prct = new List<ProductCategory>();

        //        prct.Add(new ProductCategory { CategoryId = cat.CategoryId, ProductId = prod.ProductId });

        //        foreach (var id in catids)
        //        {

        //            prct.Add(new ProductCategory { CategoryId = id, ProductId = prod.ProductId });

        //        }

        //        db.ProductCategories.AddRange(prct);

        //        db.SaveChanges();
        //    }

        //    return View("Index");
        //}

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


            int parentid = cat.CategoryId;
            ProductCategory prdct = new ProductCategory();
            Category cattmp = new Category();


            if (cat.ParentCategoryId == 0)
            {
                prdct.CategoryId = cat.CategoryId;
                prdct.ProductId = prod.ProductId;

                db.ProductCategories.Add(prdct);
                db.SaveChanges();
            }
            else
            {
                while (parentid != 0)
                {
                    cattmp = db.Categories.Where(f => f.CategoryId == parentid).FirstOrDefault();

                    prdct.CategoryId = cattmp.CategoryId;
                    prdct.ProductId = prod.ProductId;

                    parentid = cattmp.ParentCategoryId;

                    db.ProductCategories.Add(prdct);

                    db.SaveChanges();

                }
            }


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
            prd.Name = prdDTO.Name;
            prd.Description = prdDTO.Description;
            prd.Manufacturer = prdDTO.Manufacturer;
            prd.Price = prdDTO.Price;

            string[] ss = prdDTO.Subproducts.Trim().Split(",");

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