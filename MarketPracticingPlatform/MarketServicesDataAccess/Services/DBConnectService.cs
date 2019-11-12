using MarketPracticingPlatform.Sevice;
using MarketPracticingPlatform.Sevice.ModelsDTO;

namespace MarketPracticingPlatform.Services

{
    public class DBConnectService : IUserDataService
    {
        //public static void AddConnectService(this IServiceCollection services)
        //{
        //    services.AddScoped<GetDbData>();
        //}

        //GetDbData _getdata;

        //public DBConnectService(GetDbData getdata)
        //{
        //    _getdata = getdata;
        //}



    public UserAuthenticationDTO UserAuthentication(UserDTO userDTO, Data.DataBaseConnection.DBConnection db)
    {
            GetDbData _getdata = new GetDbData(db);
            UserAuthenticationDTO tmp = _getdata.GetAuthenticationData(userDTO);

        return tmp;
    }


    //public IActionResult UserCreation()
    //{
    //    return View("Index");
    //}

    }
}
