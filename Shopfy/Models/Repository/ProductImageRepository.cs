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
        public void InsertThumbnail()
        {
            throw new NotImplementedException();
        }
        public void DeleteImage()
        {
            throw new NotImplementedException();
        }

       

        public void UpdateImage(ProductImage image)
        {
            throw new NotImplementedException();
        }

        public void InsertImage()
        {
            throw new NotImplementedException();
        }

        
    }
}
