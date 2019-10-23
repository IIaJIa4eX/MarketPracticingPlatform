using MarketPracticingPlatform.DBConnection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RecommendedProductsViewComponent : ViewComponent
    {

        DataBaseConnection db;

        public RecommendedProductsViewComponent(DataBaseConnection db)
        {
            this.db = db;

        }




        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

           

            return await Task.FromResult(View("RecommendedProducts"));
        }
        


    }

}
