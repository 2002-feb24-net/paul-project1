using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface IItemRepository
    {
// ! CLASS SPECIFIC
        List<Item> GetItemsByName(string itemName = null);
        List<Domain.Model.Item> GetItemsBySellerName(string sellerName = null);
        List<Domain.Model.Item> GetItemsByStoreName(string storeName = null);
        List<Domain.Model.Item> GetItemsByTopicName(string topicName = null);
        Item GetItemById(int itemId);
        void AddItem(Item inputItem);
        void DeleteItemById(int itemId);
        void DeleteItemBySellerId(int sellerId);
        void DeleteItemByStoreId(int storeId);
        void DeleteItemByOrderId(int orderId);
        void DeleteItemByTopicId(int topicId);
        void UpdateItem(Item inputItem);
// ! GENERAL COMMANDS
        void Save();
    }
}
