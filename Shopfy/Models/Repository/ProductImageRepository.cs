using Shopfy.Models.Interfaces;

namespace Shopfy.Models.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ShopfyDbContext _context;
        public ProductImageRepository(ShopfyDbContext shopfyDbContext)
        {
            _context = shopfyDbContext;
        }
        public void InsertImages(List<ProductImage> images)
        {

            _context.ProductImages.AddRange(images);
        }
        
        public void DeleteImage(Guid productId)
        {
            // remove data from database
            var images = _context.ProductImages.Where(p => p.ProductId == productId)
                ?? throw new ArgumentNullException(); 
            _context.ProductImages.RemoveRange(images);
           
        }

       

        public void UpdateImage(List<ProductImage> productImages)
        {
            _context.ProductImages.UpdateRange(productImages);
        }



       
    }
}
