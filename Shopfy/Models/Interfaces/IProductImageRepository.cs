namespace Shopfy.Models.Interfaces
{
    public interface IProductImageRepository
    {
        public void InsertThumbnail();
        public void InsertImages(List<ProductImage> productImages);
        public void DeleteImage();
        public void UpdateImage(ProductImage image);

    }
}
