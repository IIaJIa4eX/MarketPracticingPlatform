using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Models
{
    public class UserAuthenticationDTO
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string Error { get; set; }

        public bool Success { get; set; }

    }
}
