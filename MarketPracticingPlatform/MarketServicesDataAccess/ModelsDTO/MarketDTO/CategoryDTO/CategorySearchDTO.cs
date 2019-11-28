using MarketPracticingPlatform.Data.DataBaseModels;
using System.Collections.Generic;


namespace MarketPracticingPlatform.Service.ModelsDTO
{
    public class CategorySearchDTO
    {

        public bool IsSearchSuccess { get; set; }

        public string BackRequestMessageInfo { get; set; }

        public List<Product> Products { get; set; }

    }
}
