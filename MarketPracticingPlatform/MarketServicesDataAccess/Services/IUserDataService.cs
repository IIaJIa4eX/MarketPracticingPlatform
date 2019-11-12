using MarketPracticingPlatform.Sevice.ModelsDTO;

namespace MarketPracticingPlatform.Services
{
    public interface IUserDataService
    {

        UserAuthenticationDTO GetUserAuthentication(UserDTO userDTO);
        //IActionResult UserCreation();

    }
}
