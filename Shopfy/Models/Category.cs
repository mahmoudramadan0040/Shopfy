using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shopfy.Models
{
    public class Category:BaseEntity
    {
        public Guid Id { get; set; }
        
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public ICollection<SubCategory>? SubCategorys { get; set;}
        public ICollection<Product>? Products { get; set;} 

    }
}
