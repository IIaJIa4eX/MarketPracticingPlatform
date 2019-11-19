using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
namespace MarketPracticingPlatform.Controllers
{

    public class JsTreeModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public bool children { get; set; } // if node has sub-nodes set true or not set false
    }


    public class TreeViewController : Controller
    {
        public JsonResult GetRoot()
        {
            List<JsTreeModel> items = GetTree();

            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetChildren(string id)
        {
            List<JsTreeModel> items = GetTree(id);

            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        static List<JsTreeModel> GetTree()
        {
            var items = new List<JsTreeModel>();

            // set items in here

            return items;
        }

        static List<JsTreeModel> GetTree(string id)
        {
            var items = new List<JsTreeModel>();

            // set items in here

            return items;
        }





    }

}
