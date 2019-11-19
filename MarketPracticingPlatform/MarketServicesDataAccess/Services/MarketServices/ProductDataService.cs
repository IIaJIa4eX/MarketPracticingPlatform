using MarketPracticingPlatform.Data.DataBaseConnection;
using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketPracticingPlatform.Service.Services
{
    public class ProductDataService : IProductDataService
    {


        readonly DBConnection _db;

        public ProductDataService(DBConnection db)
        {
            _db = db;
        }


        public ProductRespondDTO EditProduct(ProductDTO prdDTO)
        {

            Product prd = _db.Products.Where(f => f.ProductId == prdDTO.ProductId).FirstOrDefault();

            if(prd == null)
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Этот продукт был удалён, " + "<a href="+"Index"+"> Создайте новый <a>" };
            }

            if (string.IsNullOrWhiteSpace(prdDTO.ProductName))
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не ввели название продукта"};
            }

            if (string.IsNullOrWhiteSpace(prdDTO.ProductDescription))
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не ввели описание продукта" };
            }

            if (string.IsNullOrWhiteSpace(prdDTO.ProductManufacturerName))
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не ввели название производителя продукта" };
            }

            if (prdDTO.ProductPrice == 0)
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не ввели стоимость продукта" };
            }

            if (string.IsNullOrWhiteSpace(prdDTO.CategoryName))
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не ввели название категории продукта" };
            }

            var prdNameCheck = _db.Products.Where(f => f.Name == prdDTO.ProductName).FirstOrDefault();

            if(prdNameCheck != null && (prdNameCheck.ProductId != prdDTO.ProductId))
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Продукт с таким названием уже существует" };
            }

            prd.ProductId = prdDTO.ProductId;
            prd.Name = prdDTO.ProductName;
            prd.Description = prdDTO.ProductDescription;
            prd.Manufacturer = prdDTO.ProductManufacturerName;
            prd.Price = prdDTO.ProductPrice;

            string[] ss = new string[] { };

            if (!string.IsNullOrWhiteSpace(prdDTO.Subproducts))
            {
                ss = prdDTO.Subproducts.Trim().Split(",");
            }

            prd.CategoryId = _db.Categories.Where(f => f.Name == prdDTO.CategoryName).Select(x => x.CategoryId).FirstOrDefault();

            _db.SaveChanges();

            var msp = _db.MainSubProducts.Where(f => f.MainProductId == prd.ProductId).Select(x => x.SubProductID.ToString()).ToArray();

            if (msp.Count() != 0)
            {

                if (ss.SequenceEqual(msp))
                {
                    return new ProductRespondDTO { IsSuccess = true };
                }
                else
                {

                    string query = "DELETE FROM `mainsubproducts` WHERE `MainProductId` = {0}";
                    _db.Database.ExecuteSqlCommand(query, prd.ProductId);

                }

            }

            foreach (var id in ss)
            {

                _db.MainSubProducts.Add(new MainSub_Products { MainProductId = prd.ProductId, SubProductID = Int32.Parse(id) });

            }

            _db.SaveChanges();

            return new ProductRespondDTO { IsSuccess = true };

        }


        public ProductRespondDTO CreatProduct(ProductDTO prdDTO)
        {

           
            if (string.IsNullOrWhiteSpace(prdDTO.ProductName))
            {

                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не указали имя продукта" };

            }

            var productName = _db.Products.Where(f => f.Name == prdDTO.ProductName).FirstOrDefault();

            if(productName != null)
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Продукт с таким названием уже сущестует"};
            }

            if (string.IsNullOrWhiteSpace(prdDTO.ProductDescription))
            {

                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не указали описание продукта" };

            }

            if (string.IsNullOrWhiteSpace(prdDTO.ProductManufacturerName))
            {

                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не указали производителя продукта" };

            }

            if (prdDTO.ProductPrice == 0)
            {

                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Вы не указали цену продукта" };

            }

            var cat = _db.Categories.Where(f => f.Name == prdDTO.CategoryName).FirstOrDefault();

            if (cat == null)
            {
                return new ProductRespondDTO { IsSuccess = false, ErrorMessage = "Категории с таким названием не существует" };
            }


            Product prod = new Product
            {
                Name = prdDTO.ProductName,
                Description = prdDTO.ProductDescription,
                Price = prdDTO.ProductPrice,
                Manufacturer = prdDTO.ProductManufacturerName,
                CategoryId = cat.CategoryId
            };

            _db.Products.Add(prod);
            _db.SaveChanges();


            int? parentid = cat.ParentCategoryId;

            if (cat.ParentCategoryId == 0) //TODO Заменить на null
            {

                _db.ProductCategories.Add(new ProductCategory { CategoryId = cat.CategoryId, ProductId = prod.ProductId });

            }
            else
            {
                Category cattmp = new Category();

                while (parentid != 0)
                {
                    cattmp = _db.Categories.Where(f => f.CategoryId == parentid).FirstOrDefault();

                    if(cattmp == null)
                    {
                        break;
                    }

                    _db.ProductCategories.Add(new ProductCategory { CategoryId = cattmp.CategoryId, ProductId = prod.ProductId });

                    parentid = cattmp.ParentCategoryId;
                }

                _db.ProductCategories.Add(new ProductCategory { CategoryId = cat.CategoryId, ProductId = prod.ProductId });

            }

            _db.SaveChanges();

            return new ProductRespondDTO { IsSuccess = true};
        }


        public Product GetProductByID(int productId)
        {
            Product prd = _db.Products.Where(f => f.ProductId == productId).FirstOrDefault();

            return prd;
        }


        public List<Product> GetProductsByMainProductId(int mainPrdId, int numberOfProducts)
        {

            if(numberOfProducts == 0)
            {
                var allSubProducts = _db.MainSubProducts.Where(f => f.MainProductId == mainPrdId).ToList();

                if(allSubProducts == null)
                {
                    return null;
                }

                var allprd = new List<Product>();

                foreach (var item in allSubProducts)
                {
                    Product prtmp = _db.Products.Where(f => f.ProductId == item.SubProductID).FirstOrDefault();
                    if (prtmp != null)
                    {
                        allprd.Add(prtmp);
                    }
                }

                return allprd;
            }

            var subProducts = _db.MainSubProducts.Where(f => f.MainProductId == mainPrdId).Take(numberOfProducts);

            if (subProducts == null)
            {
                return null;
            }

            var prd = new List<Product>();

            foreach (var item in subProducts)
            {
                Product prtmp = _db.Products.Where(f => f.ProductId == item.SubProductID).FirstOrDefault();
                if (prtmp != null)
                {
                    prd.Add(prtmp);
                }
            }

            return prd;
        }


        public void DeleteProduct(int productId)
        {
            Product prd = _db.Products.Where(f => f.ProductId == productId).FirstOrDefault();

            _db.Products.Remove(prd);

            _db.SaveChanges();
        }


        public ProductDTO GetProductInfoForEdit(int productId)
        {

            var prd = _db.Products.Where(f => f.ProductId == productId).FirstOrDefault();


            if(prd == null)
            {
                return null;
            }

            ProductDTO prdDTO = new ProductDTO
            {
                ProductId = prd.ProductId,
                ProductName = prd.Name,
                ProductDescription = prd.Description,
                ProductPrice = prd.Price,
                ProductManufacturerName = prd.Manufacturer,

                CategoryName = _db.Categories.Where(f => f.CategoryId == prd.CategoryId).Select(x => x.Name).FirstOrDefault()
            };

            var subprods = _db.MainSubProducts.Where(f => f.MainProductId == productId);

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

            return prdDTO;
        }


        public List<Product> GetRelativeProductsByProductId(int productId, int numberOfProducts)
        {
            var prd = _db.Products.Where(f => f.ProductId == productId).FirstOrDefault();

            var relativeProducts = _db.Products.Where(f => f.CategoryId == prd.CategoryId && f.ProductId != prd.ProductId).Take(numberOfProducts).ToList();

            return relativeProducts;
        }
    }
}
