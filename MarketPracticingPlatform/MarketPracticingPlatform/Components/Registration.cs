using MarketPracticingPlatform.DBConnection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RegistrationViewComponent : ViewComponent
    {

        DataBaseConnection db;


        public RegistrationViewComponent(DataBaseConnection db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View("Registr");
        }

    }
}
