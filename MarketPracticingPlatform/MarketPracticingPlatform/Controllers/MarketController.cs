using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Sevice.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MarketPracticingPlatform.Controllers
{
    [Route("[controller]/[action]")]
    public class MarketController : Controller
    {
        //IProductDataService _prod; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public MarketController(/*IProductDataService prod*/) //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            //_prod = prod;
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
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //_prod.CreateProduct(pdh);
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //var cat = _db.Categories.Where(f => f.Name == pdh.CategoryName).FirstOrDefault();


            //Product prod = new Product
            //{
            //    Name = pdh.ProductName,
            //    Description = pdh.ProductDescription,
            //    Price = pdh.ProductPrice,
            //    Manufacturer = pdh.ProductManufacturerName,
            //    CategoryId = cat.CategoryId
            //};

            //_db.Products.Add(prod);
            //_db.SaveChanges();


            //int parentid = cat.ParentCategoryId;

            //if (cat.ParentCategoryId == 0)
            //{

            //    _db.ProductCategories.Add(new ProductCategory { CategoryId = cat.CategoryId, ProductId = prod.ProductId });

            //}
            //else
            //{
            //    Category cattmp = new Category();

            //    while (parentid != 0)
            //    {
            //        cattmp = _db.Categories.Where(f => f.CategoryId == parentid).FirstOrDefault();

            //        _db.ProductCategories.Add(new ProductCategory { CategoryId = cattmp.CategoryId, ProductId = prod.ProductId });

            //        parentid = cattmp.ParentCategoryId;                                 
            //    }

            //    _db.ProductCategories.Add(new ProductCategory { CategoryId = cat.CategoryId, ProductId = prod.ProductId });

            //}

            //_db.SaveChanges();

            return View("Index");
        }

       

        [HttpPost]
        public IActionResult CategoryCreation(CategoryDTO cdh)
        {
            //Category parentCat = _db.Categories.Where(f => f.Name == cdh.ParentCategoryName).FirstOrDefault();

            //Category cat = new Category
            //{
            //    Name = cdh.Name,
            //    Description = cdh.Description
            //};

            //if (parentCat == null)
            //{
            //    cat.ParentCategoryId = 0;
            //}
            //else
            //{
            //    cat.ParentCategoryId = parentCat.CategoryId;
            //}

            //_db.Categories.Add(cat);
            //_db.SaveChanges();

            return View("Index");
        }

        [HttpPost]
        public IActionResult EditProduct(ProductDTO prdDTO)
        {
            //Product prd = _db.Products.Where(f => f.ProductId == prdDTO.ProductId).FirstOrDefault();

            //prd.ProductId = prdDTO.ProductId;
            //prd.Name = prdDTO.ProductName;
            //prd.Description = prdDTO.ProductDescription;
            //prd.Manufacturer = prdDTO.ProductManufacturerName;
            //prd.Price = prdDTO.ProductPrice;

            //string[] ss = new string[] { };

            //if (!string.IsNullOrWhiteSpace(prdDTO.Subproducts))
            //{
            //    ss = prdDTO.Subproducts.Trim().Split(",");
            //}

            //prd.CategoryId = _db.Categories.Where(f => f.Name == prdDTO.CategoryName).Select(x => x.CategoryId).FirstOrDefault();

            //_db.SaveChanges();

            //var msp = _db.MainSubProducts.Where(f => f.MainProductId == prd.ProductId).Select(x => x.SubProductID.ToString()).ToArray();

            //if (msp.Count() != 0)
            //{

            //    if (ss.SequenceEqual(msp))
            //    {
            //        return RedirectToAction("Index", "MarketSearch");
            //    }
            //    else
            //    {

            //        string query = "DELETE FROM `mainsubproducts` WHERE `MainProductId` = {0}";
            //        _db.Database.ExecuteSqlCommand(query, prd.ProductId);

            //    }

            //}

            //foreach (var id in ss)
            //{

            //    _db.MainSubProducts.Add(new MainSub_Products { MainProductId = prd.ProductId, SubProductID = Int32.Parse(id) });

            //}

            //_db.SaveChanges();

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
            //Product prd = _db.Products.Where(f => f.ProductId == ProductId).FirstOrDefault();

            //_db.Products.Remove(prd);

            //_db.SaveChanges();

            return RedirectToAction("Index","MarketSearch");
        }

    }
}