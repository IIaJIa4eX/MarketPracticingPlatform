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

namespace MarketPracticingPlatform.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
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


        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize(Roles = "IIaJIa4eX")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Ваша роль: администратор");
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
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthToken.ISSUER,
                    audience: AuthToken.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthToken.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            UserDataHandler udh = new UserDataHandler();

            udh.Email = identity.Name;
            udh.Token = encodedJwt;
            
            // сериализация ответа
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

            // если пользователя не найдено
            return null;
        }
    }





}
