namespace Shopfy.Models.Interfaces
{
    public interface IRatingRepository
    {
        void CreateRating(Rating rating);
        void UpdateRating(Rating rating);
        void DeleteRating(Rating rating);
        IEnumerable<Rating> GetAllRatingByProduct(Guid productId);

    }
}
