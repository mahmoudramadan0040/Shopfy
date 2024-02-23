namespace Shopfy.Models.Interfaces
{
    public interface IStorageRepository
    {
        Task<string> AddImage(IFormFile file, string readerName);
        Task<byte[]> GetItem(string objectKey);
        string GeneratePreSignedURL(string objectKey);

    }
}
