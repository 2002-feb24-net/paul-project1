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
    public class ItemRepository : IItemRepository
    {
        private readonly UsedGoodsDbContext _dbContext;
        private readonly ILogger<ItemRepository> _logger;

        public ItemRepository(UsedGoodsDbContext dbContext, ILogger<ItemRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
// ! CLASS SPECIFIC
        public List<Domain.Model.Item> GetItemsByName(string itemName = null)
        {
            _logger.LogInformation($"Retrieving items with the name: {itemName}");
            List<Context.Item> itemList = _dbContext.Items
                .Include(p => p.Order)
                .Include(p => p.Store)
                .Include(p => p.Seller)
                .Include(p => p.TopicOption)
                .ToList();
            if (itemName != null)
            {
                itemList = itemList.FindAll(p => p.ItemName.ToLower() == itemName.ToLower());
            }
            return itemList.Select(Mapper.MapItem).ToList();
        }

        public List<Domain.Model.Item> GetItemsBySellerName(string sellerName = null)
        {
            _logger.LogInformation($"Retrieving items with the seller name: {sellerName}");
            List<Context.Item> itemList = _dbContext.Items
                .Include(p => p.Order)
                .Include(p => p.Store)
                .Include(p => p.Seller)
                .Include(p => p.TopicOption)
                .ToList();
            if (sellerName != null)
            {
                itemList = itemList.FindAll(p => p.Seller.SellerName.ToLower() == sellerName.ToLower());
            }
            return itemList.Select(Mapper.MapItem).ToList();
        }

        public List<Domain.Model.Item> GetItemsByStoreName(string storeName = null)
        {
            _logger.LogInformation($"Retrieving items with the store name: {storeName}");
            List<Context.Item> itemList = _dbContext.Items
                .Include(p => p.Order)
                .Include(p => p.Store)
                .Include(p => p.Seller)
                .Include(p => p.TopicOption)
                .ToList();
            if (storeName != null)
            {
                itemList = itemList.FindAll(p => p.Store.LocationName.ToLower() == storeName.ToLower());
            }
            return itemList.Select(Mapper.MapItem).ToList();
        }
        public Domain.Model.Item GetItemById(int itemId)
        {
            _logger.LogInformation($"Retrieving item id: {itemId}");
            Context.Item returnItem = _dbContext.Items
                .Include(p => p.Store)
                .Include(p => p.Seller)
                .Include(p => p.TopicOption)
                .First(p => p.ItemId == itemId);
            return Mapper.MapItem(returnItem);
        }
        public void AddItem(Domain.Model.Item inputItem)
        {
            if (inputItem.Id != 0)
            {
                _logger.LogWarning($"Item to be added has an ID ({inputItem.Id}) already: ignoring.");
            }

            _logger.LogInformation("Adding item");

            Context.Item entity = Mapper.UnMapItem(inputItem);
            entity.ItemId = 0;
            _dbContext.Add(entity);
        }
        public void DeleteItemById(int itemId)
        {
            _logger.LogInformation($"Deleting item with ID {itemId}");
            Context.Item entity = _dbContext.Items.Find(itemId);
            _dbContext.Remove(entity);
        }
        public void DeleteItemBySellerId(int sellerId)
        {
            _logger.LogInformation($"Deleting item with seller ID {sellerId}");
            List<Context.Item> entity = _dbContext.Items.ToList().FindAll(p => p.SellerId == sellerId);
            foreach (var val in entity)
            {
                _dbContext.Remove(val);
            }
        }
        public void DeleteItemByStoreId(int storeId)
        {
            _logger.LogInformation($"Deleting item with store ID {storeId}");
            List<Context.Item> entity = _dbContext.Items.ToList().FindAll(p => p.StoreId == storeId);
            foreach (var val in entity)
            {
                _dbContext.Remove(val);
            }
        }
        public void UpdateItem(Domain.Model.Item inputItem)
        {
            _logger.LogInformation($"Updating item with ID {inputItem.Id}");
            Context.Item currentEntity = _dbContext.Items.Find(inputItem.Id);
            Context.Item newEntity = Mapper.UnMapItem(inputItem);

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
