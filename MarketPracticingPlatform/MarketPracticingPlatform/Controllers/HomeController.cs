using MarketPracticingPlatform.Authentication_token;
using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        IUserDataService _GetUserServices;

        public HomeController(IUserDataService GetUserServices)
        {
            _GetUserServices = GetUserServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("UserAuthentication")]
        public async Task<UserAuthenticationDTO> UserAuthentication(UserDTO userDTO)
        {
            UserAuthenticationDTO tmp = _GetUserServices.GetUserAuthentication(userDTO);

            if(tmp.UserIdentity != null)
            {
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: AuthToken.ISSUER,
                        audience: AuthToken.AUDIENCE,
                        notBefore: now,
                        claims: tmp.UserIdentity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthToken.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                
                var option = new CookieOptions
                {
                    //option.Expires = DateTime.Now.AddHours(24);
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true
                };
                Response.Cookies.Append("Token", encodedJwt, option);
                Response.Cookies.Append("Username", userDTO.Email, option);
               // Response.Headers.Add("Authorization", "Bearer " + encodedJwt);
                var ss = Request.Headers.ToList();

                Response.ContentType = "application/json";
                return await Task.FromResult(new UserAuthenticationDTO { IsSuccess = tmp.IsSuccess, UserIdentity = null });
            }

            return await Task.FromResult(tmp);

        }


        [Route("LogOff")]
        public IActionResult LogOff()
        {
            Response.Cookies.Delete("Token");

            Response.Cookies.Delete("Username");

            return RedirectToAction("Index");
        }

    }





}
