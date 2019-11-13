using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Service.ModelsDTO
{
    public class UserAuthenticationDTO
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccess { get; set; } = false;

        public ClaimsIdentity UserIdentity { get; set; } = null;

    }
}
