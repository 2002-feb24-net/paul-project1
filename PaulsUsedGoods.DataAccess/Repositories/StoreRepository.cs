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
    class StoreRepository : IStoreRepository
    {

        private readonly UsedGoodsDbContext _dbContext;
        private readonly ILogger<ItemRepository> _logger;

        public StoreRepository(UsedGoodsDbContext dbContext, ILogger<ItemRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

// ! CLASS SPECIFIC
        public List<Domain.Model.Store> GetStoresByName(string storeName = null)
        {
            _logger.LogInformation($"Retrieving stores with the name: {storeName}");
            List<Context.Store> storeList = _dbContext.Stores
                .Include(p => p.Item)
                .Include(p => p.Person)
                .ToList();
            if (storeName != null)
            {
                storeList = storeList.FindAll(p => p.LocationName == storeName);
            }
            return storeList.Select(Mapper.MapStore).ToList();
        }
        public Domain.Model.Store GetStoreById(int storeId)
        {
            _logger.LogInformation($"Retrieving store id: {storeId}");
            Context.Store returnStore = _dbContext.Stores
                .Include(p => p.Item)
                .Include(p => p.Person)
                .First(p => p.StoreId == storeId);
            return Mapper.MapStore(returnStore);
        }
        public void AddStore(Domain.Model.Store inputStore)
        {
            if (inputStore.Id != 0)
            {
                _logger.LogWarning($"Store to be added has an ID ({inputStore.Id}) already: ignoring.");
            }

            _logger.LogInformation("Adding store");

            Context.Store entity = Mapper.UnMapStore(inputStore);
            entity.StoreId = _dbContext.Stores.Max(p => p.StoreId)+1;
            _dbContext.Add(entity);
        }
        public void DeleteStoreById(int storeId)
        {
            _logger.LogInformation($"Deleting store with ID {storeId}");
            Context.Store entity = _dbContext.Stores.Find(storeId);
            _dbContext.Remove(entity);
        }
        public void UpdateStore(Domain.Model.Store inputStore)
        {
            _logger.LogInformation($"Updating store with ID {inputStore.Id}");
            Context.Store currentEntity = _dbContext.Stores.Find(inputStore.Id);
            Context.Store newEntity = Mapper.UnMapStore(inputStore);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }
// ! GENERAL COMMANDS
        public void Save()
        {
            _logger.LogInformation("Saving changes to the database");
            _dbContext.SaveChanges();
        }
    }
}
