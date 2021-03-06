﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class LoginViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (Request.Cookies.ContainsKey("Token"))
            {
                return await Task.FromResult(View("LoggedIn"));
            }
            return await Task.FromResult(View("Authorization"));
        }
    }
}
