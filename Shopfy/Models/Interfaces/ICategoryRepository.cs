using Shopfy.ViewModels.Dtos;

namespace Shopfy.Models.Interfaces
{
    public interface ICategoryRepository
    {
        // get all categories 
        IEnumerable<Category> AllCategories();
        // get one cateogry based on id 
        Category? GetCategoryById(Guid id);
        Category? GetCategoryByName(string name);

        IEnumerable<SubCategory> GetAllSubCategoriesByCategoryId(Guid categoryId);
        IEnumerable<Category> GetAllCategoriesWithSubCategory();
        IEnumerable<Product> GetAllProductsByCategoryId(Guid categoryId);
        // delete one category 
        void DeleteCategoryById(Guid id);
        // create category
        Category CreateCategory(Category category);
        // update category 
        Category UpdateCategory(Category category);
        
    }
}
