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
            if (!Request.Cookies.ContainsKey("Token"))
            {

                return RedirectToAction("Index", "Registration");
                
            }

           // Category cats = db.Categories;

            return View();

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