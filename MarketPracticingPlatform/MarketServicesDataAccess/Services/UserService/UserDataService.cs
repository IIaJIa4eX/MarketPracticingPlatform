using MarketPracticingPlatform.Data.DataBaseConnection;
using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MarketPracticingPlatform.Service.Services

{
    public class UserDataService : IUserDataService
    {

        readonly DBConnection _db;

        public UserDataService(DBConnection db)
        {
            _db = db;
        }


        public UserRegistrationDTO UserRegistration(UserDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Email))
            {

                return new UserRegistrationDTO { IsSuccess = false, ErrorMessage = "Эмейл должен быть введён" };

            }

            if (string.IsNullOrWhiteSpace(userDTO.Password))
            {

                return new UserRegistrationDTO { IsSuccess = false, ErrorMessage = "Пароль должен быть введён" };

            }

            var emlcheck = _db.Users.Where(f => f.Email == userDTO.Email).FirstOrDefault();

            if (emlcheck != null)
            {

                return new UserRegistrationDTO { IsSuccess = false, ErrorMessage = "Юзер с таким Email уже существует" };

            }

            User us = new User
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                Name = userDTO.Name,
                Number = userDTO.Number
            };

            _db.Users.Add(us);
            _db.SaveChanges();

            return new UserRegistrationDTO { IsSuccess = true};
        }


    


        public UserAuthenticationDTO GetUserAuthentication(UserDTO userDTO)
        {

            if (string.IsNullOrWhiteSpace(userDTO.Email) || string.IsNullOrWhiteSpace(userDTO.Password))
            {

                return (new UserAuthenticationDTO { IsSuccess = false, ErrorMessage = "Все поля должны быть заполнены" });
            }


            var identity = GetIdentity(userDTO.Email, userDTO.Password);


            if (identity == null)
            {

                return (new UserAuthenticationDTO { IsSuccess = false, ErrorMessage = "Вы неправильно ввели имя пользователя или пароль" });
            }


            return (new UserAuthenticationDTO { IsSuccess = true, UserIdentity = identity });


        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {

            var user = _db.Users.Where(f => f.Email == email && f.Password == password).FirstOrDefault();

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Name),
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

