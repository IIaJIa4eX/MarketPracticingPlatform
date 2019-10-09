using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Authentication_token
{
    public class AuthToken
    {
        public const string ISSUER = "IIaJIa4eX"; // издатель токена
        public const string AUDIENCE = "http://localhost"; // потребитель токена
        const string KEY = "GH3B5JK7DF8_helpmeoutofhere!";   // ключ для шифрации
        public const int LIFETIME = 10; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
