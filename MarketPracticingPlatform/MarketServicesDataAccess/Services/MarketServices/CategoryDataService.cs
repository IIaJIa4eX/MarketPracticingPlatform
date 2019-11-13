using MarketPracticingPlatform.Data.DataBaseConnection;
using MarketPracticingPlatform.Service.Interface;

namespace MarketPracticingPlatform.Service.Services
{
    public class CategoryDataService : ICategoryDataService
    {
        readonly DBConnection _db;

        public CategoryDataService(DBConnection db)
        {
            _db = db;
        }

    }
}
