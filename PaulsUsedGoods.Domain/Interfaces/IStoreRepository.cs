using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface IStoreRepository
    {
// ! CLASS SPECIFIC
        List<Store> GetStoresByName(string storeName = null);
        Store GetStoreById(int storeId);
        void AddStore(Store inputStore);
        void DeleteStoreById(int storeId);
        void UpdateStore(Store inputStore);
// // ! ITEMS WITHIN THE INDIVIDUAL STORE ONLY
//         List<Item> GetItemsByName(string itemName = null);
//         Item GetItemById(int itemId);
//         void AddItem(Item inputItem);
//         void DeleteItemById(int itemId);
//         void UpdateItem(Item inputItem);
// // ! TOPIC RELATED
//         TopicOption GetTopics();
//         void AddTopic(TopicOption inputTopic);
//         void DeleteTopicById(int topicId);
//         void DeleteTopicByName(string topicName);
// // ! TOPIC + ITEM RELATED
//         List<Item> GetItemsByTopic(string topicName = null);
//         List<Item> GetItemsByTopicId(int topicId);
// ! GENERAL COMMANDS
        void Save();
    }
}
