using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("Index")]
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