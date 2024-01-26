namespace Shopfy.Models
{
    public class Order:BaseEntity
    {
        public Guid OrderId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string OrderStatus { get; set;  }
        public string ContactNumber { get; set; }
        public string Address { get; set; }



    }
}
