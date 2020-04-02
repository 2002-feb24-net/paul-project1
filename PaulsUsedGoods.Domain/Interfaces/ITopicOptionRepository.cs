using PaulsUsedGoods.Domain.Model;

namespace PaulsUsedGoods.Domain.Interfaces
{
    interface ITopicOptionRepository
    {
// ! CLASS SPECIFIC
        TopicOption GetTopics();
        TopicOption GetTopicByName(string topicName = null);
        TopicOption GetTopicById(int topicId);
        void AddTopic(TopicOption inputTopic);
        void DeleteTopicById(int topicId);
        void DeleteTopicByName(string topicName);
// ! GENERAL COMMANDS
        void Save();
    }
}
