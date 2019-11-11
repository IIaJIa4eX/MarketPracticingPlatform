using MarketPracticingPlatform.DataBaseModels;
using MarketPracticingPlatform.DBConnection;
using MarketPracticingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPracticingPlatform.Components
{
    public class ProductEditViewComponent : ViewComponent
    {
        DataBaseConnection db;

        public ProductEditViewComponent(DataBaseConnection db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int productid)
        {
            Product prd = db.Products.Where(f => f.ProductId == productid).FirstOrDefault();

            if(prd == null)
            {
                ViewBag.ErrorMessage = $"Продукта с id {productid} не существует";
                return await Task.FromResult(View("EditError"));
            }
           

            ProductDTO prdDTO = new ProductDTO();
            prdDTO.ProductId = prd.ProductId;
            prdDTO.ProductName = prd.Name;
            prdDTO.ProductDescription = prd.Description;
            prdDTO.ProductPrice = prd.Price;
            prdDTO.ProductManufacturerName = prd.Manufacturer;

            prdDTO.CategoryName = db.Categories.Where(f => f.CategoryId == prd.CategoryId).Select(x => x.Name).FirstOrDefault();

            var subprods = db.MainSubProducts.Where(f => f.MainProductId == productid);

            string subproducts = "";

            if (subprods.Count() != 0)
            {

                foreach (var item in subprods)
                {
                    subproducts += item.SubProductID.ToString() + ",";

                }

                prdDTO.Subproducts = subproducts.Substring(0, subproducts.Length - 1);
            }
            else
            {

                prdDTO.Subproducts = subproducts;

            }
            return await Task.FromResult(View("ProductEditInformation",prdDTO));

        }
    }

}
