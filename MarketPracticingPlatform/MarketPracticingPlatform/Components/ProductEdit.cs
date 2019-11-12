using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class ProductEditViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(int productid)
        {
            //var prd = _db.Products.Where(f => f.ProductId == productid).FirstOrDefault();

            //if(prd == null)
            //{
            //    ViewBag.ErrorMessage = $"Продукта с id {productid} не существует";
            //    return await Task.FromResult(View("EditError"));
            //}


            //ProductDTO prdDTO = new ProductDTO
            //{
            //    ProductId = prd.ProductId,
            //    ProductName = prd.Name,
            //    ProductDescription = prd.Description,
            //    ProductPrice = prd.Price,
            //    ProductManufacturerName = prd.Manufacturer,

            //    CategoryName = _db.Categories.Where(f => f.CategoryId == prd.CategoryId).Select(x => x.Name).FirstOrDefault()
            //};

            //var subprods = _db.MainSubProducts.Where(f => f.MainProductId == productid);

            //string subproducts = "";

            //if (subprods.Count() != 0)
            //{

            //    foreach (var item in subprods)
            //    {
            //        subproducts += item.SubProductID.ToString() + ",";

            //    }

            //    prdDTO.Subproducts = subproducts.Substring(0, subproducts.Length - 1);
            //}
            //else
            //{

            //    prdDTO.Subproducts = subproducts;

            //}
            return await Task.FromResult(View("ProductEditInformation"/*,prdDTO*/));

        }
    }

}
