using MarketPracticingPlatform.Service.ModelsDTO;

namespace MarketPracticingPlatform.Service.Interface
{
    public interface IUserDataService
    {

        UserAuthenticationDTO GetUserAuthentication(UserDTO userDTO);

        UserRegistrationDTO UserRegistration(UserDTO userDTO);


    }
}
