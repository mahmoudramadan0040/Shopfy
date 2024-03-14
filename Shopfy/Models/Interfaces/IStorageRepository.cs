namespace Shopfy.Models.Interfaces
{
    public interface IStorageRepository
    {
        Task<string> AddImage(IFormFile file);
        Task DeleteImages (ICollection<ProductImage> images);


    }
}
