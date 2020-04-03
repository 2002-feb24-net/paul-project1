using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface ITopicOptionRepository
    {
// ! CLASS SPECIFIC
        List<TopicOption> GetTopicByName(string topicName = null);
        TopicOption GetTopicById(int topicId);
        void AddTopic(TopicOption inputTopic);
        void DeleteTopicById(int topicId);
        void DeleteTopicByName(string topicName);
// ! GENERAL COMMANDS
        void Save();
    }
}
