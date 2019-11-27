using MarketPracticingPlatform.Data.DataBaseModels;
using MarketPracticingPlatform.Service.ModelsDTO;
using System.Collections.Generic;

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

        List<CategoryTreeNodeDTO> GetCategoryTreeNodes(int key, bool isRoot);
    }
}
