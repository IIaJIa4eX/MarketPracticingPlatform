﻿using DbCotext.DataBaseConnection;
using MarketServicesDataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketServicesDataAccess
{


    public class GetDbData
    {

        DBConnection db;

        GetDbData(DBConnection connect, HttpContext request)
        {
            db = connect;
        }


        public async Task<UserAuthenticationDTO> GetAuthenticationData(UserDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Email) || string.IsNullOrWhiteSpace(userDTO.Password))
            {

                return await Task.FromResult(new UserAuthenticationDTO { IsSuccess = false, ErrorMessage = "Все поля должны быть заполнены" });
            }


            var identity = GetIdentity(userDTO.Email, userDTO.Password);


            if (identity == null)
            {

                return await Task.FromResult(new UserAuthenticationDTO { IsSuccess = false, ErrorMessage = "Вы неправильно ввели имя пользователя или пароль" });
            }


            return await Task.FromResult(new UserAuthenticationDTO { IsSuccess = true });


        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {

            var user = db.Users.Where(f => f.Email == email && f.Password == password).FirstOrDefault();

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