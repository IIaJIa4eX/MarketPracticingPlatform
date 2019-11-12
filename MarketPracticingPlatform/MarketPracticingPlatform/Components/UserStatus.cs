using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class UserStatusViewComponent : ViewComponent
    {
        //DataBaseConnection db;

        //public UserStatusViewComponent(DataBaseConnection db)
        //{
        //    this.db = db;
        //}



        public async Task<IViewComponentResult> InvokeAsync()
        {

            if (Request.Cookies.ContainsKey("Token"))
            {
                ViewData["UserNick"] = Request.Cookies["Username"];
                return await Task.FromResult(View("Authorized"));
            }
            else
            {
                return await Task.FromResult(View("LogIn"));

            }
          
        }

    }
}
