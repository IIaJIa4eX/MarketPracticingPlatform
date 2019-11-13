using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPracticingPlatform.Service.ModelsDTO
{
    public class CategorySearchDTO
    {

        public string CategoryName { get; set; }

        public bool IsSearchSuccess { get; set; }

        public string BackRequestMessageInfo { get; set; } 

    }
}
