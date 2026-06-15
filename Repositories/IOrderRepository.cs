using System.Collections.Generic;
using School二手Platform.Models;

namespace School二手Platform.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        void UpdateOrderStatus(int orderId, string status);
        List<Order> GetOrdersByBuyer(int buyerId);
        List<Order> GetOrdersBySeller(int sellerId);
    }
}
