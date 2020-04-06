using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface IItemRepository
    {
// ! CLASS SPECIFIC
        List<Item> GetItemsByName(string itemName = null);
        List<Domain.Model.Item> GetItemsBySellerName(string sellerName = null);
        Item GetItemById(int itemId);
        void AddItem(Item inputItem);
        void DeleteItemById(int itemId);
        void DeleteItemBySellerId(int sellerId);
        void UpdateItem(Item inputItem);
// ! GENERAL COMMANDS
        void Save();
    }
}
