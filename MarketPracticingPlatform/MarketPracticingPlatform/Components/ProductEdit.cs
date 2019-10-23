﻿using MarketPracticingPlatform.DataBaseModels;
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
            prdDTO.Name = prd.Name;
            prdDTO.Description = prd.Description;
            prdDTO.Price = prd.Price;
            prdDTO.Manufacturer = prd.Manufacturer;

            prdDTO.Category = db.Categories.Where(f => f.CategoryId == prd.CategoryId).Select(x => x.Name).FirstOrDefault();

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

            prdDTO.Subproducts = subproducts;

            return await Task.FromResult(View("ProductEditInformation",prdDTO));

        }
    }

}
