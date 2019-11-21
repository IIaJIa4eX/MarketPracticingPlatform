using MarketPracticingPlatform.Data.DataBaseConnection;
using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.Interface;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MarketPracticingPlatform.Service.Services
{


    public class CategoryDataService : ICategoryDataService
    {
        readonly DBConnection _db;

        public CategoryDataService(DBConnection db)
        {
            _db = db;
        }

        public CategorySearchDTO SearchProductsByCategoryName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return new CategorySearchDTO
                {
                    IsSearchSuccess = false,
                    BackRequestMessageInfo = "Введите название категории",
                    Products = null
                };
            }

            var cat = _db.Categories.Where(f => f.Name == categoryName).FirstOrDefault();

            if (cat != null)
            {
                var prdcat = _db.ProductCategories.Where(f => f.CategoryId == cat.CategoryId).ToList();

                List<Product> prd = new List<Product>();

                foreach (var item in prdcat)
                {
                    prd.Add(_db.Products.Where(f => f.ProductId == item.ProductId).FirstOrDefault());
                }

                return new CategorySearchDTO
                {
                    IsSearchSuccess = true,
                    BackRequestMessageInfo = $"Поиск по запросу {categoryName}",
                    Products = prd
                };

            }

            return new CategorySearchDTO
            {
                IsSearchSuccess = false,
                BackRequestMessageInfo = $"По запросу '{categoryName}' ничего не найдено",
                Products = null
            };
        }



        public CategoryCreationDTO CreateCategory(CategoryDTO categoryDTO)
        {
            if (string.IsNullOrWhiteSpace(categoryDTO.Name))
            {
                return new CategoryCreationDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "Вы не ввели название категории"
                };
            }

            if (string.IsNullOrWhiteSpace(categoryDTO.Description))
            {
                return new CategoryCreationDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "Вы не ввели описание категории"
                };
            }

            var catName = _db.Categories.Where(f => f.Name == categoryDTO.Name).FirstOrDefault();

            if (catName != null)
            {
                return new CategoryCreationDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "Категория с таким именем уже существует"
                };
            }

            var parentCat = _db.Categories.Where(f => f.Name == categoryDTO.ParentCategoryName).FirstOrDefault();

            if (parentCat == null && !string.IsNullOrWhiteSpace(categoryDTO.ParentCategoryName))
            {

                return new CategoryCreationDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "Родительской категории с таким названием не существует"
                };

            }

            Category cat = new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };

            if (parentCat != null)
            {
                cat.ParentCategoryId = parentCat.CategoryId;
            }


            _db.Categories.Add(cat);
            _db.SaveChanges();

            return new CategoryCreationDTO
            {
                IsSuccess = true
            };

        }


        public List<Category> GetAllCategories()
        {


            var cats = _db.Categories.ToList();
            return cats;
        }


        public Category GetCategoryById(int categoryId)
        {
            var cat = _db.Categories.Where(f => f.CategoryId == categoryId).FirstOrDefault();

            return cat;
        }


        public List<Category> GetCategoriesByParentCategoryId(int? parentCategoryId)
        {
            var cats = _db.Categories.Where(f => f.ParentCategoryId == parentCategoryId).ToList();

            return cats;
        }


        public JsonResult GetCategoryTreeNodes(int key, bool isRoot)
        {

            List<CategoryTreeNodeDTO> catTmp = new List<CategoryTreeNodeDTO>();

            if (isRoot)
            {
                var rootcats = GetCategoriesByParentCategoryId(null);

                foreach (var item in rootcats)
                {
                    catTmp.Add(new CategoryTreeNodeDTO { id = item.CategoryId, text = item.Name, children = true });
                }

                var first = catTmp;

                return new JsonResult(first);
            }

            var cats = GetCategoriesByParentCategoryId(key);

            foreach (var item in cats)
            {
                catTmp.Add(new CategoryTreeNodeDTO { id = item.CategoryId, text = item.Name, children = true });
            }

            var next = catTmp;

            return new JsonResult(next);

        }



        public CategorySearchDTO SearchProductsByCategoryId(int categoryId)
        {
            if (categoryId == 0)
            {
                return new CategorySearchDTO
                {
                    IsSearchSuccess = false,
                    BackRequestMessageInfo = "Введите название категории",
                    Products = null
                };
            }

            var cat = _db.Categories.Where(f => f.CategoryId == categoryId).FirstOrDefault();

            if (cat != null)
            {
                var prdcat = _db.ProductCategories.Where(f => f.CategoryId == cat.CategoryId).ToList();

                List<Product> prd = new List<Product>();

                foreach (var item in prdcat)
                {
                    prd.Add(_db.Products.Where(f => f.ProductId == item.ProductId).FirstOrDefault());
                }

                return new CategorySearchDTO
                {
                    IsSearchSuccess = true,
                    BackRequestMessageInfo = $"Поиск по запросу {cat.Name}",
                    Products = prd
                };

            }

            return new CategorySearchDTO
            {
                IsSearchSuccess = false,
                BackRequestMessageInfo = $"По Вашему запросу ничего не найдено",
                Products = null
            };
        }
    }
}
