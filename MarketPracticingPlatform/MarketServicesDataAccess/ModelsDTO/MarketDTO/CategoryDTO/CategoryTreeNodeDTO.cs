using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPracticingPlatform.Service.ModelsDTO
{
    public class CategoryTreeNodeDTO
    {
            public int id { get; set; }

            public string text { get; set; }

            public bool children { get; set; }
    }
}
