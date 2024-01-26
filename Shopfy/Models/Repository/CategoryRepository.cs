using Microsoft.EntityFrameworkCore;
using Shopfy.Models.Interfaces;

namespace Shopfy.Models.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopfyDbContext _context;
       
        public CategoryRepository
            (
            ShopfyDbContext shopfyDbContext
            )
        {
            _context = shopfyDbContext;
            
        }
        public IEnumerable<Category> AllCategories()
        {   
            return _context.Categories.AsNoTracking();
        }

        public Category CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void DeleteCategoryById(Guid id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategoriesWithSubCategory()
        {
            return _context.Categories.Include(e => e.SubCategorys);
        }
        public IEnumerable<SubCategory> GetAllSubCategoriesByCategoryId(Guid categoryId)
        {
            return _context.Categories.FirstOrDefault(e => e.Id == categoryId).SubCategorys;
        }
        public IEnumerable<Product> GetAllProductsByCategoryId(Guid categoryId)
        {
            return _context.Products.Where(e => e.CategoryId == categoryId).ToList();
        }

        public Category? GetCategoryById(Guid id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public Category? GetCategoryByName(string name)
        {
            return _context.Categories.Where(e => e.CategoryName == name).FirstOrDefault();
        }

        public Category UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}
