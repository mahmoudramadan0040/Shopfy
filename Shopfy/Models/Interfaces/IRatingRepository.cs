namespace Shopfy.Models.Interfaces
{
    public interface IRatingRepository
    {
        Rating CreateRating(Rating rating);
        Rating UpdateRating(Rating rating);
        void DeleteRating(Rating rating);
        IEnumerable<Rating> GetAllRatingByProduct(Guid productId);

    }
}
