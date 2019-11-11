using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DbCotext.DataBaseModels
{
    public class MainSub_Products
    {

        public int id { get; set;}

        public int MainProductId { get; set; }

        public int SubProductID { get; set; }
    }
}
