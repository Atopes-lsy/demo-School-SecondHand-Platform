using System.Collections.Generic;
using System.Linq;
using School二手Platform.Models;

namespace School二手Platform.Repositories
{
    public class MockOrderRepository : IOrderRepository
    {
        private static readonly List<Order> _orders = new();
        private static int _nextId = 1;

        public void CreateOrder(Order order)
        {
            order.Id = _nextId++;
            _orders.Add(order);
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;
            }
        }

        public List<Order> GetOrdersByBuyer(int buyerId)
        {
            return _orders.Where(o => o.BuyerId == buyerId).ToList();
        }

        public List<Order> GetOrdersBySeller(int sellerId)
        {
            return _orders.Where(o => o.SellerId == sellerId).ToList();
        }
    }
}
