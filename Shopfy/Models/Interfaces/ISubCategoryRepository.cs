namespace Shopfy.Models.Interfaces
{
    public interface ISubCategoryRepository
    {
        IList<SubCategory> GetSubCategories();
        IEnumerable<Product> GetAllProductsBySubCategoryId(Guid subCategoryId);
        SubCategory CreateSubCategory(SubCategory subCategory);
        SubCategory GetSubCategory(Guid subcategoryId);
        void DeleteSubCategory(Guid subCategoryId);
        SubCategory UpdateSubCategory(SubCategory subCategory);

    }
}
