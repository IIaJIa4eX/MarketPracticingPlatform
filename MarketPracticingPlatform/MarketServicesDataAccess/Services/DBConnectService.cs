using MarketPracticingPlatform.Data.DataBaseConnection;
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
        readonly DBConnection _db;

        public DBConnectService(DBConnection db)
        {
            _db = db;
        }



        public UserAuthenticationDTO GetUserAuthentication(UserDTO userDTO)
    {
            GetDbData _getdata = new GetDbData(_db);
            UserAuthenticationDTO tmp = _getdata.GetAuthenticationData(userDTO);

        return tmp;
    }


    //public IActionResult UserCreation()
    //{
    //    return View("Index");
    //}

    }
}
