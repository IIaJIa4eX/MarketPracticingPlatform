using MarketPracticingPlatform.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace MarketPracticingPlatform.Controllers
{
    public class MarketSearchController : Controller
    {
        readonly ICategoryDataService _GetCategoryServices;

        public MarketSearchController(ICategoryDataService GetCategoryServices)
        {
            _GetCategoryServices = GetCategoryServices;
        }

       
        public IActionResult Index()
        {

            var cats = _GetCategoryServices.GetAllCategories();


            return View(cats);

        }

       
        public IActionResult ShowProductInfo(int id)
        {

                ViewBag.Productid = id;
                return View("ProductInfo");
            
        }
    }
}