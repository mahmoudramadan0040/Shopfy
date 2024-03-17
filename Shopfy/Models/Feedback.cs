using System.ComponentModel.DataAnnotations.Schema;

namespace Shopfy.Models
{
    public class Feedback:BaseEntity
    {
        public Guid FeedbackId { get; set; }
        public string? Comment { get; set; }
        [ForeignKey("CutomerId")]
        public Guid? CutomerId { get; set; }
        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

    }
}
