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
    public class OrderRepository : IOrderRepository
    {

        private readonly UsedGoodsDbContext _dbContext;
        private readonly ILogger<ItemRepository> _logger;
        public IItemRepository Repo { get; }

        public OrderRepository(UsedGoodsDbContext dbContext, ILogger<ItemRepository> logger, IItemRepository repo)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
// ! CLASS SPECIFIC
        public List<Domain.Model.Order> GetOrdersByName(string orderName = null)
        {
            _logger.LogInformation($"Retrieving Orders with the name: {orderName}");
            List<Context.Order> orderList = _dbContext.Orders
                .Include(p => p.Person)
                .Include(p => p.Item)
                .ToList();
            if (orderName != null)
            {
                orderList = orderList.FindAll(p => p.Person.Username.ToLower() == orderName.ToLower());
            }
            return orderList.Select(Mapper.MapOrder).ToList();
        }
        public Domain.Model.Order GetOrderById(int orderId)
        {
            _logger.LogInformation($"Retrieving order id: {orderId}");
            if (orderId == 0)
            {
                _logger.LogInformation($"Order id is 0, returning null for order.");
                return null;
            }
            Context.Order returnOrder = _dbContext.Orders
                .Include(p => p.Person)
                .Include(p => p.Item)
                .FirstOrDefault(p => p.OrderId == orderId);
            return Mapper.MapOrder(returnOrder);
        }
        public void AddOrder(Domain.Model.Order inputOrder)
        {
            if (inputOrder.Id != 0)
            {
                _logger.LogWarning($"Order to be added has an ID ({inputOrder.Id}) already: ignoring.");
            }

            _logger.LogInformation("Adding order");

            Context.Order entity = new Context.Order
            {
                PersonId = inputOrder.UserId,
                OrderDate = inputOrder.Date,
                TotalOrderPrice = inputOrder.Price,
            };
            entity.OrderId = 0; //_dbContext.Orders.Max(p => p.OrderId)+1;
            _dbContext.Add(entity);
            Save();
            foreach (var val in inputOrder.Items)
            {
                val.OrderId = _dbContext.Orders.Max(p => p.OrderId);
                Repo.UpdateItem(val);
            }
        }
        public void DeleteOrderById(int orderId)
        {
            _logger.LogInformation($"Deleting order with ID {orderId}");
            Context.Order entity = _dbContext.Orders.Find(orderId);
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
