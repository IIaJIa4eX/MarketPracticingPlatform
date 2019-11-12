using DbCotext.DataBaseConnection;
using MarketPracticingPlatform.Sevice.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Sevice
{


    public class GetDbData
    {

        DBConnection _db;

        public GetDbData(DBConnection db)
        {
            _db = db;
        }

        public GetDbData()
        {

        }


        public UserAuthenticationDTO GetAuthenticationData(UserDTO userDTO)
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
