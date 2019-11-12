using MarketPracticingPlatform.Sevice;
using MarketPracticingPlatform.Sevice.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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



    public UserAuthenticationDTO UserAuthentication(UserDTO userDTO, MarketPracticingPlatform.Data.DataBaseConnection.DBConnection db)
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
