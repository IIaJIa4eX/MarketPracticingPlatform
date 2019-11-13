using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Services;
using MarketPracticingPlatform.Sevice.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MarketPracticingPlatform.Controllers
{
    [Route("Registration")]
    public class RegistrationController : Controller
    {

        IUserDataService _GetUserServices;

        public RegistrationController(IUserDataService GetUserServices)
        {
            _GetUserServices = GetUserServices;
        }

        public IActionResult Index()
        {

            return View();

        }


        [HttpPost("UserCreation")]
        public UserRegistrationDTO UserCreation(UserDTO userDTO)
        {
            var tmp = _GetUserServices.UserRegistration(userDTO);

            return tmp;

        }

    }
}