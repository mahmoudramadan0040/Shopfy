namespace Shopfy.ViewModels.Dtos
{
    public record CategoriesDto(
        Guid Id ,
        string CategoryName,
        string CategoryDescription,
        DateTime? CreatedDate,
        DateTime? UpdatedDate
        );
    
}
