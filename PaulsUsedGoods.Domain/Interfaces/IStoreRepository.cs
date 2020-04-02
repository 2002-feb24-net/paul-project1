using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    interface IStoreRepository
    {
// ! ITEMS WITHIN THE INDIVIDUAL STORE ONLY
        List<Item> GetItemsByName(string itemName = null);
        Item GetItemById(int itemId);
        void AddItem(Item inputItem);
        void DeleteItemById(int itemId);
        void UpdateItem(Item inputItem);
// ! TOPIC RELATED
        TopicOption GetTopics();
        void AddTopic(TopicOption inputTopic);
        void DeleteTopicById(int topicId);
        void DeleteTopicByName(string topicName);
// ! TOPIC + ITEM RELATED
        List<Item> GetItemsByTopic(string topicName = null);
        List<Item> GetItemsByTopicId(int topicId);
// ! GENERAL COMMANDS
        void Save();
    }
}
