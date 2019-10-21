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


            return ViewComponent("MarketSearch", new { categoryname = category });


        }
    }
}