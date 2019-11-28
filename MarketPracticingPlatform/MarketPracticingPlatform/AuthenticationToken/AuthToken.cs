using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MarketPracticingPlatform.Authentication_token
{
    public class AuthToken
    {
        public const string ISSUER = "IIaJIa4eX"; 
        public const string AUDIENCE = "http://localhost";
        const string KEY = "GH3B5JK7DF8_helpmeoutofhere!";  
        public const int LIFETIME = 5;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
