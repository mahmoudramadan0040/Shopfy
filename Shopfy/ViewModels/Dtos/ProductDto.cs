namespace Shopfy.ViewModels.Dtos
{
    public record ProductDto(
        string ProductName,
        string ProductDescription,
        int ProductPrice,
        int ProductQuantity,
        string ProductStatus,
        DateTime? CreatedDate,
        DateTime? UpdateDate
        );
}
