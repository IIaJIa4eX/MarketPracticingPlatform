﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class CategoryCreationViewComponent : ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult(View("CategoryCreationForm"));

        }


    }
}
