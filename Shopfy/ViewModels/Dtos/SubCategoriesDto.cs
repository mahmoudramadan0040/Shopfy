namespace Shopfy.ViewModels.Dtos
{
    public record SubCategoriesDto
    {
        public Guid Id { get; set; }
        public string CateogrySubName { get; set; }
        public string CateogrySubDescription { get; set; }
    }
}
