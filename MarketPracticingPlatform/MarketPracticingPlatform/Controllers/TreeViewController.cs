using System.Collections.Generic;
using System.Linq;
using MarketPracticingPlatform.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarketPracticingPlatform.Controllers
{


    public class Cat
    {
        public int id { get; set; }

        public string text { get; set; }

        public bool children { get; set; }
    }

    [Route("TreeView")]
    public class TreeViewController : Controller
    {
        readonly ICategoryDataService _GetCategoryServices;


        public TreeViewController(ICategoryDataService GetCategoryServices)
        {
            _GetCategoryServices = GetCategoryServices;
        }


        [HttpGet]
        [Route("GetChildren")]
        public  JsonResult GetChildren(int key, bool isRoot)
        {

            var cat = _GetCategoryServices.GetCategoryById(key);

            var cats = _GetCategoryServices.GetChildrenByCategoryId(key);

            List<Cat> caats = new List<Cat>();
            
            foreach(var item in cats)
            {
                caats.Add(new Cat { id = item.CategoryId, text = item.Name, children = true});
            }

            if (isRoot)
            {
                var first = new[]
                {
            new
            {
                id = cat.CategoryId,
                text = cat.Name,
                state = new
                {
                    opened = false,
                },
                children = true
                
            }
        }
                .ToList(); 

                return Json(first);
            }

            var next = caats
            .ToList();

            return new JsonResult (next);
        }

    }

}
