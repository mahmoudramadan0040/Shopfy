using System.ComponentModel.DataAnnotations;

namespace Shopfy.Models
{
    public class Customer:BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        // customer have one or many rating
        // customer can rate one product 
        public ICollection<Rating>? Ratings { get; set; }
    }
}
