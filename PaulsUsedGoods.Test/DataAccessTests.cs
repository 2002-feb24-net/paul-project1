using System;
using Xunit;
using PaulsUsedGoods.DataAccess;

namespace PaulsUsedGoods.Test
{
    public class DataAccessTests
    {
// ! MAPPER ITEM
//! ***********************
        [Fact]
        public void MapItem_Test()
        {
            PaulsUsedGoods.DataAccess.Context.Item itemContext = new PaulsUsedGoods.DataAccess.Context.Item();
            itemContext.ItemId = 1;
            itemContext.StoreId = 1;
            itemContext.OrderId = null;
            itemContext.SellerId = 1;
            itemContext.TopicId = 1;
            itemContext.ItemName = "TestObjectName";
            itemContext.ItemDescription = "TestObjectDescription";
            itemContext.ItemPrice = 10.00;

            PaulsUsedGoods.Domain.Model.Item result = Mapper.MapItem(itemContext);

            PaulsUsedGoods.Domain.Model.Item predictedResult = new PaulsUsedGoods.Domain.Model.Item
            {
                Id = itemContext.ItemId,
                Name = itemContext.ItemName,
                Description = itemContext.ItemDescription,
                Price = itemContext.ItemPrice,
                StoreId = itemContext.StoreId,
                OrderId = itemContext.OrderId,
                SellerId = itemContext.SellerId,
                TopicId = itemContext.TopicId
            };

            Assert.True(EqualDomainItemTest(predictedResult,result));
        }

        public void UnMapItem_Test()
        {
            PaulsUsedGoods.Domain.Model.Item itemDomain = new PaulsUsedGoods.Domain.Model.Item();
            itemDomain.Id = 1;
            itemDomain.StoreId = 1;
            itemDomain.OrderId = null;
            itemDomain.SellerId = 1;
            itemDomain.TopicId = 1;
            itemDomain.Name = "TestObjectName";
            itemDomain.Description = "TestObjectDescription";
            itemDomain.Price = 10.00;

            PaulsUsedGoods.DataAccess.Context.Item result = Mapper.UnMapItem(itemDomain);

            PaulsUsedGoods.DataAccess.Context.Item predictedResult = new PaulsUsedGoods.DataAccess.Context.Item
            {
                ItemId = itemDomain.Id,
                ItemName = itemDomain.Name,
                ItemDescription = itemDomain.Description,
                ItemPrice = itemDomain.Price,
                StoreId = itemDomain.StoreId,
                OrderId = itemDomain.OrderId,
                SellerId = itemDomain.SellerId,
                TopicId = itemDomain.TopicId
            };

            Assert.True(EqualContextItemTest(result,predictedResult));
        }

        private bool EqualDomainItemTest (PaulsUsedGoods.Domain.Model.Item predict, PaulsUsedGoods.Domain.Model.Item result)
        {
            if(predict.Id != result.Id
            || predict.Name != result.Name
            || predict.Description != result.Description
            || predict.Price != result.Price
            || predict.StoreId != result.StoreId
            || predict.OrderId != result.OrderId
            || predict.SellerId != result.SellerId
            || predict.TopicId != result.TopicId
            )
            {
                return false;
            }
            return true;
        }

        private bool EqualContextItemTest (PaulsUsedGoods.DataAccess.Context.Item predict, PaulsUsedGoods.DataAccess.Context.Item result)
        {
            if(predict.ItemId != result.ItemId
            || predict.ItemName != result.ItemName
            || predict.ItemDescription != result.ItemDescription
            || predict.ItemPrice != result.ItemPrice
            || predict.StoreId != result.StoreId
            || predict.OrderId != result.OrderId
            || predict.SellerId != result.SellerId
            || predict.TopicId != result.TopicId
            )
            {
                return false;
            }
            return true;
        }

// ! MAPPER STORE
//! ***********************
                [Fact]
        public void MapStore_Test()
        {
            PaulsUsedGoods.DataAccess.Context.Store contextInst = new PaulsUsedGoods.DataAccess.Context.Store();
            contextInst.StoreId = 1;
            contextInst.LocationName = "TestName";

            PaulsUsedGoods.Domain.Model.Store result = Mapper.MapStore(contextInst);

            PaulsUsedGoods.Domain.Model.Store predictedResult = new PaulsUsedGoods.Domain.Model.Store
            {
                Id = 1,
                Name = "TestName"
            };

            Assert.True(EqualDomainStoreTest(predictedResult,result));
        }

        public void UnMapStore_Test()
        {
            PaulsUsedGoods.Domain.Model.Store domainInst = new PaulsUsedGoods.Domain.Model.Store();
            domainInst.Id = 1;
            domainInst.Name = "TestName";

            PaulsUsedGoods.DataAccess.Context.Store result = Mapper.UnMapStore(domainInst);

            PaulsUsedGoods.DataAccess.Context.Store predictedResult = new PaulsUsedGoods.DataAccess.Context.Store
            {
                StoreId = domainInst.Id,
                LocationName = domainInst.Name,
            };

            Assert.True(EqualContextStoreTest(result,predictedResult));
        }

        private bool EqualDomainStoreTest (PaulsUsedGoods.Domain.Model.Store predict, PaulsUsedGoods.Domain.Model.Store result)
        {
            if(predict.Id != result.Id
            || predict.Name != result.Name
            )
            {
                return false;
            }
            return true;
        }

        private bool EqualContextStoreTest (PaulsUsedGoods.DataAccess.Context.Store predict, PaulsUsedGoods.DataAccess.Context.Store result)
        {
            if(predict.StoreId != result.StoreId
            || predict.LocationName != result.LocationName
            )
            {
                return false;
            }
            return true;
        }
    }
}