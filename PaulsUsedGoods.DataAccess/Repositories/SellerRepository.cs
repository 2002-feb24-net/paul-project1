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
    class SellerRepository : ISellerRepository
    {
        private readonly UsedGoodsDbContext _dbContext;
        private readonly ILogger<ItemRepository> _logger;

        public SellerRepository(UsedGoodsDbContext dbContext, ILogger<ItemRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
// ! CLASS SPECIFIC
        public List<Domain.Model.Seller> GetSellersByName(string sellerName = null)
        {
            _logger.LogInformation($"Retrieving sellers with the name: {sellerName}");
            List<Context.Seller> sellerList = _dbContext.Sellers
                .Include(p => p.Item)
                .Include(p => p.Review)
                .ToList();
            if (sellerName != null)
            {
                sellerList = sellerList.FindAll(p => p.SellerName == sellerName);
            }
            return sellerList.Select(Mapper.MapSeller).ToList();
        }
        public Domain.Model.Seller GetSellerById(int sellerId)
        {
            _logger.LogInformation($"Retrieving seller id: {sellerId}");
            Context.Seller returnSeller = _dbContext.Sellers
                .Include(p => p.Item)
                .Include(p => p.Review)
                .First(p => p.SellerId == sellerId);
            return Mapper.MapSeller(returnSeller);
        }
        public void AddSeller(Domain.Model.Seller inputSeller)
        {
            if (inputSeller.Id != 0)
            {
                _logger.LogWarning($"Seller to be added has an ID ({inputSeller.Id}) already: ignoring.");
            }

            _logger.LogInformation("Adding seller");

            Context.Seller entity = Mapper.UnMapSeller(inputSeller);
            entity.SellerId = _dbContext.Sellers.Max(p => p.SellerId)+1;
            _dbContext.Add(entity);
        }
        public void DeleteSellerById(int sellerId)
        {
            _logger.LogInformation($"Deleting seller with ID {sellerId}");
            Context.Seller entity = _dbContext.Sellers.Find(sellerId);
            _dbContext.Remove(entity);
        }
        public void UpdateSeller(Domain.Model.Seller inputSeller)
        {
            _logger.LogInformation($"Updating seller with ID {inputSeller.Id}");
            Context.Seller currentEntity = _dbContext.Sellers.Find(inputSeller.Id);
            Context.Seller newEntity = Mapper.UnMapSeller(inputSeller);

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
