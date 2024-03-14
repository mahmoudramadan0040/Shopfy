namespace Shopfy.Models.Interfaces
{
    public interface IProductImageRepository
    {
       
        public void InsertImages(List<ProductImage> productImages);
        public void DeleteImage(Guid productId);
        public void UpdateImage(List<ProductImage> productImages);
        

    }
}
