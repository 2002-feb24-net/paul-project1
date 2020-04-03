using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface IItemRepository
    {
// ! CLASS SPECIFIC
        List<Item> GetItemsByName(string itemName = null);
        Item GetItemById(int itemId);
        void AddItem(Item inputItem);
        void DeleteItemById(int itemId);
        void UpdateItem(Item inputItem);
// ! GENERAL COMMANDS
        void Save();
    }
}
