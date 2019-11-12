using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Sevice.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MarketPracticingPlatform.Controllers
{
    public class RegistrationController : Controller
    {

        Data.DataBaseConnection.DBConnection _db;


        public RegistrationController(Data.DataBaseConnection.DBConnection db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {



            return View();
        }


        [HttpPost("UserCreation")]
        public IActionResult UserCreation(UserDTO udh)
        {
           

            if (udh.Email == "" || udh.Email == null)
            {

                return View("Index");

            }

            if (udh.Password == "" || udh.Password == null)
            {

                return View("Index");

            }

            var emlcheck = _db.Users.Where(f => f.Email == udh.Email).FirstOrDefault();

            if (emlcheck != null)
            {

                return View("Index");

            }

            User us = new User
            {
                Email = udh.Email,
                Password = udh.Password,
                Name = udh.Name,
                Number = udh.Number
            };

            _db.Users.Add(us);
            _db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }


    }
}