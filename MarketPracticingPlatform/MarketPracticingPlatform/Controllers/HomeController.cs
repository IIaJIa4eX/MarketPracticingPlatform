using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarketPracticingPlatform.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using MarketPracticingPlatform.Authentication_token;
using MarketPracticingPlatform.DBConnection;
using MarketPracticingPlatform.DataBaseModels;
using Microsoft.AspNetCore.Authorization;
using MarketPracticingPlatform.CookieHandler;

namespace MarketPracticingPlatform.Controllers
{
    [Route("Home")]
    public class HomeController :  Controller
    {


        DataBaseConnection db;

        public HomeController(DataBaseConnection db)
        {
            this.db = db;
            
        }

        public IActionResult Index()
        {


            return View();

        }



        [HttpPost]
        [Route("Token")]
        public async Task Token()
        {
            var useremail = Request.Form["username"];
            var password = Request.Form["password"];
            var identity = GetIdentity(useremail, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthToken.ISSUER,
                    audience: AuthToken.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthToken.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddHours(24);
            option.SameSite = SameSiteMode.Strict;
            option.HttpOnly = true;
            Response.Cookies.Append("Token", encodedJwt, option);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };


            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {

            User user = db.Users.Where(f => f.Email == email && f.Password == password).FirstOrDefault();

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Name)

                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }





}
