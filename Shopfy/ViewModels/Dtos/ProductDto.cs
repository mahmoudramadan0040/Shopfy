using Shopfy.Models;

namespace Shopfy.ViewModels.Dtos
{
    public record ProductDto(
        string ProductName,
        string ProductDescription,
        int ProductPrice,
        int ProductQuantity,
        string ProductStatus,
        IFormFile ProductThumbnail,
        IFormFile[] ProductImages
        );
}
