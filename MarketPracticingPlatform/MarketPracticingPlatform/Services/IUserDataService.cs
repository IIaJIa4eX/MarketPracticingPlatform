using MarketPracticingPlatform.Sevice.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Services
{
    public interface IUserDataService
    {

        UserAuthenticationDTO UserAuthentication(UserDTO userDTO, MarketPracticingPlatform.Data.DataBaseConnection.DBConnection db);
        //IActionResult UserCreation();

    }
}
