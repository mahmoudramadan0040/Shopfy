using Microsoft.EntityFrameworkCore;

namespace Shopfy.Models
{
    public class Order:BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public OrderStatus OrderStatus { get; set;  }
        public PaymentStatus PaymentStatus { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public List<OrderItem> OrderItems { get; set; } // Represents the order items



    }
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed
    }
    public enum OrderStatus
    {
        Processing,
        Shipped,
        Delivered
    }
    public class OrderItem
    {
        
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
