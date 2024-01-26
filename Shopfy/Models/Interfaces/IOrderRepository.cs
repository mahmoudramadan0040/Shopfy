namespace Shopfy.Models.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetOrderById(Guid orderId);
        IEnumerable<Order> GetOrderByCustomer(Guid customerId);
        void CreateOrder(Order order);
        void DeleteOrder(Guid orderId);
        void UpdateOrder(Order order);
        bool ChangeOrderStatus(Guid orderId);


    }
}
