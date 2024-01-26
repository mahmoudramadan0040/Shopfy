using Microsoft.EntityFrameworkCore;
using Shopfy.Models.Interfaces;

namespace Shopfy.Models.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        public readonly ShopfyDbContext _context;
        public SubCategoryRepository(ShopfyDbContext shopfyDbContext)
        {
            _context = shopfyDbContext;
        }
        public SubCategory CreateSubCategory(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            _context.SaveChanges();
            return subCategory;
        }

        public void DeleteSubCategory(Guid subCategoryId)
        {
            var subcategory = _context.SubCategories.FirstOrDefault(c => c.Id == subCategoryId)
                 ?? throw new ArgumentNullException();
            _context.SubCategories.Remove(subcategory);
        }

        public IEnumerable<Product> GetAllProductsBySubCategoryId(Guid subCategoryId)
        {
            var products = _context.SubCategories
                .Where(c => c.Id == subCategoryId)
                .Include(p => p.Products);
            return (IEnumerable<Product>)products;
        }

        public IList<SubCategory> GetSubCategories()
        {
            return (IList<SubCategory>) _context.SubCategories.AsNoTracking();
        }
        public SubCategory GetSubCategory(Guid subCategoryId)
        {
            return _context.SubCategories.FirstOrDefault(c => c.Id == subCategoryId) ?? throw new ArgumentNullException(nameof(subCategoryId));
        }
        
        public SubCategory UpdateSubCategory(SubCategory subCategory)
        {
            
            _context.SubCategories.Update(subCategory);
            return subCategory;
        }
    }
}
