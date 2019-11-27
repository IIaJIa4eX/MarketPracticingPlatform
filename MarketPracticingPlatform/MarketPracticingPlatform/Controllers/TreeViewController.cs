using MarketPracticingPlatform.Service.Interface;
using Microsoft.AspNetCore.Mvc;



namespace MarketPracticingPlatform.Controllers
{

    [Route("TreeView")]
    public class TreeViewController : Controller
    {
        readonly ICategoryDataService _GetCategoryServices;


        public TreeViewController(ICategoryDataService GetCategoryServices)
        {
            _GetCategoryServices = GetCategoryServices;
        }


        [HttpGet]
        [Route("GetTreeNodes")]
        public  JsonResult GetTreeNodes(int key, bool isRoot)
        {

            var cat = _GetCategoryServices.GetCategoryTreeNodes(key, isRoot);

            return new JsonResult(cat);
        }
    }

    }


