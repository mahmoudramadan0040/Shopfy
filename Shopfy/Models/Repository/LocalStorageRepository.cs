using Shopfy.Models.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Shopfy.Models.Repository
{
    public class LocalStorageRepository : IStorageRepository

    {
        
        private readonly IHttpContextAccessor _httpContextAccessor; // Inject this if not already done
        private readonly ILogger<LocalStorageRepository> _logger;
        public LocalStorageRepository(
            IHttpContextAccessor httpContextAccessor, 
            ILogger<LocalStorageRepository> logger
            )
        {
            
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }
        public async Task<string> AddImage(IFormFile file, string readerName)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("Invalid file or empty file content.");
                // Create the directory if it doesn't exist
                _logger.LogInformation("before the combine path ");

                var uploadPath = Path.Combine("Resources", "Images");
                _logger.LogDebug(uploadPath.ToString());
                Directory.CreateDirectory(uploadPath);
                // Generate a unique file name (you can customize this logic)
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);
                // Save the file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // Get the base URL of the application
                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

                // Construct the accessible URL
                var imageUrl = $"{baseUrl}/Resources/Images/{fileName}";

                return imageUrl;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., logging, error messages, etc.)
                throw new ApplicationException($"Error storing file: {ex}");
            }

        }

        public string GeneratePreSignedURL(string objectKey)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetItem(string objectKey)
        {
            throw new NotImplementedException();
        }
    }
}
