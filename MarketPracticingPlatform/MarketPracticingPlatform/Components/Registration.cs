using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class RegistrationViewComponent : ViewComponent
    {
        readonly Data.DataBaseConnection.DBConnection db;


        public RegistrationViewComponent(Data.DataBaseConnection.DBConnection db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            return await Task.FromResult(View("Registr"));
        }

    }
}
