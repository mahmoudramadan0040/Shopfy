namespace Shopfy.Models.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetOrderById(Guid orderId);
        IEnumerable<Order> GetOrderByCustomer(Guid customerId);
        Order CreateOrder(Order order);
        void DeleteOrder(Guid orderId);
        Order UpdateOrder(Order order);
        bool ChangeOrderStatus(Guid orderId , OrderStatus status);


    }
}
