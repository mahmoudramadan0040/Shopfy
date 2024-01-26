using Shopfy.Models.Interfaces;

namespace Shopfy.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public bool ChangeOrderStatus(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public void CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrderByCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrderById(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
