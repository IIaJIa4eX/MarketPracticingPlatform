using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using MarketPracticingPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPracticingPlatform.Controllers
{
    public class RegistrationController : Controller
    {

        DataBaseConnection db;


        public RegistrationController(DataBaseConnection db)
        {
            this.db = db;
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

            var emlcheck = db.Users.Where(f => f.Email == udh.Email).FirstOrDefault();

            if (emlcheck != null)
            {

                return View("Index");

            }

            User us = new User();
            us.Email = udh.Email;
            us.Password = udh.Password;
            us.Name = udh.Name;
            us.Number = udh.Number;

            db.Users.Add(us);
            db.SaveChanges();
            
            return View("Index");
        }


    }
}