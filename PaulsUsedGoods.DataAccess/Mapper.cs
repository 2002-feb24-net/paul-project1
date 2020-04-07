using System;
using System.Linq;
using System.Collections.Generic;
using PaulsUsedGoods.DataAccess.Context;
using PaulsUsedGoods.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace PaulsUsedGoods.DataAccess
{
    public class Mapper
    {
        public static Domain.Model.Item MapItem(Context.Item item)
        {
            return new Domain.Model.Item
            {
                Id = item.ItemId,
                Name = item.ItemName,
                Description = item.ItemDescription,
                Price = item.ItemPrice,
                StoreId = item.StoreId,
                OrderId = item.OrderId,
                SellerId = item.SellerId,
                TopicId  = item.TopicId
            };
        }

        public static Context.Item UnMapItem(Domain.Model.Item item)
        {
            return new Context.Item
            {
                ItemId = item.Id,
                StoreId = item.StoreId,
                OrderId = item.OrderId,
                SellerId = item.SellerId,
                TopicId = item.TopicId,
                ItemName = item.Name,
                ItemDescription = item.Description,
                ItemPrice = item.Price,
            };
        }

        public static Domain.Model.Store MapStore(Context.Store store)
        {
            return new Domain.Model.Store
            {
                Id = store.StoreId,
                Name = store.LocationName,
                Items = store.Item.Select(MapItem).ToList()
            };
        }

        public static Context.Store UnMapStore(Domain.Model.Store store)
        {
            return new Context.Store
            {
                StoreId = store.Id,
                LocationName = store.Name,
            };
        }

        public static Domain.Model.TopicOption MapTopic(Context.TopicOption topic)
        {
            return new Domain.Model.TopicOption
            {
                Id = topic.TopicOptionId,
                Topic = topic.TopicName,
            };
        }

        public static Context.TopicOption UnMapTopic(Domain.Model.TopicOption topic)
        {
            List<Context.Item> myItems = new List<Context.Item>();
            var myTopic = new Context.TopicOption
            {
                TopicOptionId = topic.Id,
                TopicName = topic.Topic,
                Item = myItems
            };
            foreach (var val in topic.Items)
            {
                myTopic.Item.Add(UnMapItem(val));
            }
            return myTopic;
        }

        public static Domain.Model.Order MapOrder(Context.Order order)
        {
            if (order == null)
            {
                return new Domain.Model.Order
                {
                    Id = 0,
                    Date = DateTime.Now,
                    Price = 0,
                    Username = "",
                    UserId = 0,
                    Items = new List<Domain.Model.Item>()
                };
            }
            else
            {
                return new Domain.Model.Order
                {
                    Id = order.OrderId,
                    Date = order.OrderDate,
                    Price = order.TotalOrderPrice,
                    Username = order.Person.Username,
                    UserId = order.PersonId,
                    Items = order.Item.Select(MapItem).ToList(),
                };
            }
        }

        public static Context.Order UnMapOrder(Domain.Model.Order order)
        {
            return new Context.Order
            {
                OrderId = order.Id,
                OrderDate = order.Date,
                TotalOrderPrice = order.Price,
                PersonId = order.UserId,
                Item = order.Items.Select(UnMapItem).ToList()
            };
        }

        public static Domain.Model.Seller MapSeller(Context.Seller seller)
        {
            return new Domain.Model.Seller
            {
                Id = seller.SellerId,
                Name = seller.SellerName,
                Items = seller.Item.Select(MapItem).ToList(),
                Reviews = seller.Review.Select(MapReview).ToList()
            };
        }

        public static Context.Seller UnMapSeller(Domain.Model.Seller seller)
        {
            return new Context.Seller
            {
                SellerId = seller.Id,
                SellerName = seller.Name,
            };
        }

        public static Domain.Model.Review MapReview(Context.Review review)
        {
            return new Domain.Model.Review
            {
                Id = review.ReviewId,
                Score = review.Score,
                Comment = review.Comment,
                PersonId = review.PersonId,
                SellerId = review.SellerId
            };
        }

        public static Context.Review UnMapReview(Domain.Model.Review review)
        {
            return new Context.Review
            {
                ReviewId = review.Id,
                PersonId = review.PersonId,
                SellerId = review.SellerId,
                Score = review.Score,
                Comment = review.Comment,
            };
        }

        public static Domain.Model.Person MapPerson(Context.Person person)
        {
            return new Domain.Model.Person
            {
                Id = person.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Username = person.Username,
                EmployeeTag = person.Employee,
                Password = person.Password,
                StoreId = person.StoreId
            };
        }

        public static Context.Person UnMapPerson(Domain.Model.Person person)
        {
            return new Context.Person
            {
                PersonId = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Username = person.Username,
                Employee = person.EmployeeTag,
                Password = person.Password,
                StoreId = person.StoreId
            };
        }

    }
}
