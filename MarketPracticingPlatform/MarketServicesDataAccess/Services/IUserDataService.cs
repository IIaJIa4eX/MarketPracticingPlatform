using MarketPracticingPlatform.Sevice.ModelsDTO;

namespace MarketPracticingPlatform.Services
{
    public interface IUserDataService
    {

        UserAuthenticationDTO UserAuthentication(UserDTO userDTO, Data.DataBaseConnection.DBConnection db);
        //IActionResult UserCreation();

    }
}
