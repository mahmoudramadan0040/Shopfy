using Shopfy.Models.Interfaces;

namespace Shopfy.Models.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ShopfyDbContext _context;
        public RatingRepository(ShopfyDbContext context) {
            _context = context;
        }
        public Rating CreateRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            _context.SaveChanges();
            return rating;
        }

        public void DeleteRating(Rating rating)
        {
            _context.Ratings.Remove(rating);
            _context.SaveChanges();
        }

        public IEnumerable<Rating> GetAllRatingByProduct(Guid productId)
        {
            return _context.Ratings.Where( p => p.ProductId ==  productId);
        }

        public Rating UpdateRating(Rating rating)
        {
            _context.Ratings.Update(rating);
            _context.SaveChanges();
            return rating;
        }
    }
}
