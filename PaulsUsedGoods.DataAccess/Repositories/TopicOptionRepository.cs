using System;
using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;
using PaulsUsedGoods.DataAccess.Context;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PaulsUsedGoods.Domain.Interfaces;

namespace PaulsUsedGoods.DataAccess.Repositories
{
    public class TopicOptionRepository : ITopicOptionRepository
    {

        private readonly UsedGoodsDbContext _dbContext;
        private readonly ILogger<ItemRepository> _logger;

        public TopicOptionRepository(UsedGoodsDbContext dbContext, ILogger<ItemRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // ! CLASS SPECIFIC
        public List<Domain.Model.TopicOption> GetTopicByName(string topicName = null)
        {
            _logger.LogInformation($"Retrieving topics with the name: {topicName}");
            List<Context.TopicOption> topicList = _dbContext.TopicOptions
                .Include(p => p.Item)
                .ToList();
            if (topicName != null)
            {
                topicList = topicList.FindAll(p => p.TopicName.ToLower() == topicName.ToLower());
            }
            return topicList.Select(Mapper.MapTopic).ToList();
        }
        public Domain.Model.TopicOption GetTopicById(int topicId)
        {
            _logger.LogInformation($"Retrieving topic id: {topicId}");
            Context.TopicOption returnTopic = _dbContext.TopicOptions
                .Include(p => p.Item)
                .First(p => p.TopicOptionId == topicId);
            return Mapper.MapTopic(returnTopic);
        }
        public void AddTopic(Domain.Model.TopicOption inputTopic)
        {
            if (inputTopic.Id != 0)
            {
                _logger.LogWarning($"Topic to be added has an ID ({inputTopic.Id}) already: ignoring.");
            }

            _logger.LogInformation("Adding topic");

            Context.TopicOption entity = Mapper.UnMapTopic(inputTopic);
            entity.TopicOptionId = 0;
            _dbContext.Add(entity);
        }
        public void DeleteTopicById(int topicId)
        {
            _logger.LogInformation($"Deleting topic with ID {topicId}");
            Context.TopicOption entity = _dbContext.TopicOptions.Find(topicId);
            _dbContext.Remove(entity);
        }
        public void DeleteTopicByName(string topicName)
        {
            _logger.LogInformation($"Deleting topic with name {topicName}");
            Context.TopicOption entity = _dbContext.TopicOptions.First(p => p.TopicName == topicName);
            _dbContext.Remove(entity);
        }
// ! GENERAL COMMANDS
        public void Save()
        {
            _logger.LogInformation("Saving changes to the database");
            _dbContext.SaveChanges();
        }
    }
}
