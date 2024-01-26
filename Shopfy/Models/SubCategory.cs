using System.ComponentModel.DataAnnotations.Schema;

namespace Shopfy.Models
{
    public class SubCategory:BaseEntity
    {
        public Guid Id { get; set; }
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public string CateogrySubName { get; set; }
        public string CateogrySubDescription { get; set; }

        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
