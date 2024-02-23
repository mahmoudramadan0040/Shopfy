using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Shopfy.Models
{
    public class Category:BaseEntity
    {
        public Guid Id { get; set; }
        
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<SubCategory>? SubCategorys { get; set;}
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Product>? Products { get; set;} 

    }
}
