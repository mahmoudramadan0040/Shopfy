using Microsoft.EntityFrameworkCore;
using Shopfy.Models.Interfaces;

namespace Shopfy.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopfyDbContext _context;
        public OrderRepository(ShopfyDbContext context)
        {
            _context = context;
        }
        #region retrive orders using id or customerId or retrive all 
        #endregion
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.AsNoTracking();
        }

        public IEnumerable<Order> GetOrderByCustomer(Guid customerId)
        {
            var orders = _context.Orders.Where(c => c.CustomerId == customerId).AsNoTracking();
            return orders;
        }

        public Order GetOrderById(Guid orderId)
        {
            return _context.Orders.Where(o => o.OrderId == orderId).AsNoTracking().FirstOrDefault()
                ?? throw new NullReferenceException(); 
        }
        public bool ChangeOrderStatus(Guid orderId, OrderStatus status)
        {
            string[] OrderState= { "Processing", "Shipped", "Delivered" };
            var order = _context.Orders.FirstOrDefault(o => o.OrderId ==  orderId);
            if (order != null && OrderState.Contains(status.ToString()))
            {
                order.OrderStatus = status;
                _context.SaveChanges();
                return true;
            }else
            {
                return false;
            }
        }

        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public void DeleteOrder(Guid orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }

        }

        

        public Order UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return order;
        }
    }
}
