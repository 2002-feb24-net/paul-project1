using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;


namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface IOrderRepository
    {
// ! CLASS SPECIFIC
        List<Order> GetOrdersByName(string orderName = null);
        Order GetOrderById(int orderId);
        void AddOrder(Order inputOrder);
        void DeleteOrderById(int orderId);
// ! GENERAL COMMANDS
        void Save();
    }
}
