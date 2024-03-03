using Microsoft.EntityFrameworkCore;
using Shopfy.Models.Interfaces;

namespace Shopfy.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopfyDbContext _context;

        public ProductRepository(
            ShopfyDbContext shopfyDbContext
            
            ) {
            _context = shopfyDbContext;
            
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _context.Products.AsNoTracking();
        }

        public Product? GetProductById(Guid id)
        {
            var product = _context.Products
                .Where(e => e.ProductId == id).Include(image => image.ProductImages).AsNoTracking().FirstOrDefault();
            return product;
        }

        public Product? GetProductByName(string name)
        {
            return _context.Products
                .Where(e => e.ProductName == name).AsNoTracking().FirstOrDefault();
        }

        public Product UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }
        public Product CreateProduct(Product product)
        {

            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

    }
}
