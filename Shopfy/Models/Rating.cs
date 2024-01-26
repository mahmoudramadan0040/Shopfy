namespace Shopfy.Models
{
    public class Rating:BaseEntity
    {
        public Guid RatingId { get; set; }
        public int Score { get; set; }

        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }


    }
}
