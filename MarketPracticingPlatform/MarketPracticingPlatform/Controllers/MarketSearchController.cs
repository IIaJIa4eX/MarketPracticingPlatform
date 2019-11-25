using MarketPracticingPlatform.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MarketPracticingPlatform.Controllers
{
    public class MarketSearchController : Controller
    {
        readonly ICategoryDataService _GetCategoryServices;

        public MarketSearchController(ICategoryDataService GetCategoryServices)
        {
            _GetCategoryServices = GetCategoryServices;
        }


       //  [Authorize]
        public IActionResult Index()
        {
            if (!Request.Cookies.ContainsKey("Token"))
            {

                return RedirectToAction("Index", "Registration");
                
            }

           var cats = _GetCategoryServices.GetAllCategories();


            return View(cats);

        }

        public IActionResult ShowProductInfo(int id)
        {
            if (Request.Cookies.ContainsKey("Token"))
            {
                ViewBag.Productid = id;
                return View("ProductInfo");
            }

            return RedirectToAction("Index", "Registration");


        }
    }
}