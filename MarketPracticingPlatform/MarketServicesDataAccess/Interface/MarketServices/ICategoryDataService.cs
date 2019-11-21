using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPracticingPlatform.Service.Interface
{
    public interface ICategoryDataService
    {
        CategorySearchDTO SearchProductsByCategoryName(string categoryName);

        CategorySearchDTO SearchProductsByCategoryId(int categoryId);

        CategoryCreationDTO CreateCategory(CategoryDTO categoryDTO);

        List<Category> GetAllCategories();

        Category GetCategoryById(int categoryId);

        List<Category> GetCategoriesByParentCategoryId(int? parentCategoryId);

        JsonResult GetCategoryTreeNodes(int key, bool isRoot);
    }
}
