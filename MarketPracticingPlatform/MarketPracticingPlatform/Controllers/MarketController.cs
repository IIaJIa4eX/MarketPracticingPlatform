using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MarketPracticingPlatform.Controllers
{
    [Route("[controller]/[action]")]
    public class MarketController : Controller
    {
        readonly IProductDataService _GetProductServices;
        readonly ICategoryDataService _GetCategoryServices;

        public MarketController(ICategoryDataService GetCategoryServices, IProductDataService GetProductServices)
        {
            _GetProductServices = GetProductServices;
            _GetCategoryServices = GetCategoryServices;
        }

        [Authorize]
        public IActionResult Index()
        {
            var ss = Request.Headers.ToList();
            if (Request.Cookies.ContainsKey("Token"))
            {
                return View();
            }
            return RedirectToAction("Index", "Registration");
        }

        [HttpPost]
        public ProductRespondDTO ProductCreation(ProductDTO pdh)
        {
            ProductRespondDTO tmp = _GetProductServices.CreatProduct(pdh);

            return tmp;
        }

       

        [HttpPost]
        public CategoryCreationDTO CategoryCreation(CategoryDTO cdh)
        {

            CategoryCreationDTO tmp = _GetCategoryServices.CreateCategory(cdh);

            return tmp;

        }

        [HttpPost]
        public ProductRespondDTO EditProduct(ProductDTO prdDTO)
        {
            ProductRespondDTO tmp = _GetProductServices.EditProduct(prdDTO);

            return tmp;
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
        public IActionResult DeleteProduct(int productId)
        {
            _GetProductServices.DeleteProduct(productId);

            return RedirectToAction("Index","MarketSearch");
        }

    }
}