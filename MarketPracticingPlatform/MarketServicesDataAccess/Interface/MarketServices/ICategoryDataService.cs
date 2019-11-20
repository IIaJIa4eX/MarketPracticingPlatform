using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPracticingPlatform.Service.Interface
{
    public interface ICategoryDataService
    {
        CategorySearchDTO SearchByCategoryName(string categoryName);

        CategoryCreationDTO CreateCategory(CategoryDTO categoryDTO);

        List<Category> GetAllCategories();

        Category GetCategoryById(int categoryId);

        List<Category> GetChildrenByCategoryId(int categoryId);

    }
}
