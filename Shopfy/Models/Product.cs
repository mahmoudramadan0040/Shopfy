using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopfy.Models
{
    public class Product:BaseEntity
    {
        public Guid ProductId { get; set; }

        [ForeignKey("CategoryId")]
        public Guid? CategoryId { get; set; }
/*        [ForeignKey("SubCategoryId")]
        public Guid SubCategoryId { get; set; }*/
        [Required(ErrorMessage = "Product Name is required")]
        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }
        [Required(ErrorMessage = "product price is required")]
        public int ProductPrice { get; set; }

        public int ProductQuantity { get; set; }
        public string? ProductThumbnail { get; set; }
        public ProductStatus? ProductStatus { get; set; }


        // product have multiple rate 
        public ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
        // product have multiple feed back 
        public ICollection<Feedback>? Feedbacks { get; set; } = new List<Feedback>();

        public ICollection<ProductImage>? ProductImages { get; set; }


    }
    public enum ProductStatus
    {
        Available,
        OutOfStock
    }
    public class ProductImage
    {
        public Guid ProductImageId { get; set; } // Primary key

        [ForeignKey("ProductId")]
        public Guid? ProductId { get; set; } // Foreign key

        public string? ImageUrl { get; set; } // Image URL
        /*public Product Product { get; set; } // Navigation property to Product*/
    }
}
