using Microsoft.AspNetCore.Mvc;

namespace MarketPracticingPlatform.Controllers
{
    public class MarketSearchController : Controller
    {
        readonly Data.DataBaseConnection.DBConnection _db;


        public MarketSearchController(Data.DataBaseConnection.DBConnection db)
        {
            this._db = db;
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