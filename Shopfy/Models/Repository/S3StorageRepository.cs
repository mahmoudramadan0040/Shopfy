using Amazon.S3;
using Amazon.S3.Model;
using Shopfy.Models.Interfaces;

namespace Shopfy.Models.Repository
{
    public class S3StorageRepository : IStorageRepository
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<S3StorageRepository> _logger;
        private readonly string bucketName;
        private const string FOLDER_NAME = "Images";

        public S3StorageRepository(
                IConfiguration configuration,
                IAmazonS3 s3Client,
                ILogger<S3StorageRepository> logger

            )
        {
            _s3Client = s3Client;
            _configuration = configuration;
            bucketName = _configuration.GetValue<string>("AWS:ShopfyBucket") ?? "";
            _logger = logger;
        }

        public Task<string> AddImage(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task DeleteImages(ICollection<ProductImage> images)
        {
            throw new NotImplementedException();
        }

        


        /* public async Task<string> AddImage(IFormFile file, string readerName)
         {
             string fileName = file.FileName;
             string objectKey = $"{FOLDER_NAME}/{readerName}/{fileName}";
             try
             {
                 using (Stream fileToUpload = file.OpenReadStream())
                 {
                     var putObjectRequest = new PutObjectRequest();
                     putObjectRequest.BucketName = bucketName;
                     putObjectRequest.Key = objectKey;
                     putObjectRequest.InputStream = fileToUpload;
                     putObjectRequest.ContentType = file.ContentType;

                     var response = await _s3Client.PutObjectAsync(putObjectRequest);
                     return GeneratePreSignedURL(objectKey);
                 }
             }
             catch (Exception ex)
             {
                 _logger.LogError("Error when upload image to s3 bucket : {ex}", ex);
                 return "";
             }


         }*/

        /*public string GeneratePreSignedURL(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                Verb = HttpVerb.GET,
                
            };

            string url = _s3Client.GetPreSignedURL(request);
            return url;
        }*/

        /*public async Task<byte[]> GetItem(string objectKey)
        {
            GetObjectResponse response = await _s3Client.GetObjectAsync(bucketName, objectKey);
            MemoryStream memoryStream = new MemoryStream();

            using Stream responseStream = response.ResponseStream; 
            responseStream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }*/
    }
}
