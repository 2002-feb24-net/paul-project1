using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface IOrder
    {
        List<int> itemsInOrder {get; set;}
        void AddToCurrentOrder(int id);
        string Username {get; set;}
    }
}