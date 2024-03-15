using Shopfy.Models;

namespace Shopfy.ViewModels.Dtos
{
    public class OrderDto
    {
        public Guid CustomerId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public List<OrderItem> OrderItems { get; set; } // Represents the order items

    }
}
