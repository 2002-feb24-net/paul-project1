using System;
using PaulsUsedGoods.Domain.Interfaces;
using System.Collections.Generic;
using PaulsUsedGoods.WebApp.ViewModels;
using System.Linq;

namespace PaulsUsedGoods.WebApp.Logic
{
    public static class GetSuggestedItem
    {
        public static ItemViewModel Suggest(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers, IReviewRepository repoRev, IOrder order)
        {
            List<Domain.Model.Item> items = repoItem.GetItemsByName();
            List<ItemViewModel> realItems = new List<ItemViewModel>();
            foreach (var val in items)
            {
                if (val.StoreId == (repoPers.GetPeopleByName(order.Username).First( p => p.Username.ToLower() == order.Username.ToLower())).StoreId)
                {
                    if (!(val.OrderId == null || val.OrderId == 0))
                    {
                        continue;
                    }
                    if (order.itemsInOrder.Contains(val.Id))
                    {
                        continue;
                    }
                    realItems.Add(new ItemViewModel
                    {
                        ItemId = val.Id,
                        ItemName = val.Name,
                        ItemDescription = val.Description,
                        ItemPrice = val.Price,
                        StoreName = repoStore.GetStoreById(val.StoreId).Name,
                        OrderId = val.OrderId,
                        SellerName = repoSell.GetSellerById(val.SellerId).Name,
                        TopicName = repoTopi.GetTopicById(val.TopicId).Topic
                    });
                }
            }

            Random rand = new Random();
            int i = rand.Next(0,realItems.Count);
            return realItems[i];
        }
    }
}