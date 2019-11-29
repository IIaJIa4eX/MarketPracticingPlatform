using MarketPracticingPlatform.Attributes;
using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPracticingPlatform.Controllers
{
    [Route("[controller]/[action]")]

    public class MarketController : Controller
    {
        readonly IProductDataService _GetProductServices;
        readonly ICategoryDataService _GetCategoryServices;

        public MarketController(ICategoryDataService GetCategoryServices, IProductDataService GetProductServices)
        {
            _GetProductServices = GetProductServices;
            _GetCategoryServices = GetCategoryServices;
        }

        [UnAuthorizedRedirect]
        public IActionResult Index()
        {

                return View();
            
        }

        [Authorize]
        [HttpPost]
        public ProductRespondDTO ProductCreation(ProductDTO pdh)
        {
            ProductRespondDTO tmp = _GetProductServices.CreatProduct(pdh);

            return tmp;
        }


        [Authorize]
        [HttpPost]
        public CategoryCreationDTO CategoryCreation(CategoryDTO cdh)
        {

            CategoryCreationDTO tmp = _GetCategoryServices.CreateCategory(cdh);

            return tmp;

        }

        [Authorize]
        [HttpPost]
        public ProductRespondDTO EditProduct(ProductDTO prdDTO)
        {
            ProductRespondDTO tmp = _GetProductServices.EditProduct(prdDTO);

            return tmp;
        }


        [UnAuthorizedRedirect]
        [HttpPost]
        public IActionResult ShowProductEdit(ProductDTO pdh)
        {

            if (pdh.ProductId != 0)
            {

                    ViewBag.Productid = pdh.ProductId;
                    return View("ProductEditInformation");
            }

            return View("Index");
        }

        [UnAuthorizedRedirect]
        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            _GetProductServices.DeleteProduct(productId);

            return RedirectToAction("Index","MarketSearch");
        }

    }
}