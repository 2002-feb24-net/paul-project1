using System;
using System.Linq;
using System.Collections.Generic;
using PaulsUsedGoods.DataAccess.Context;

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
                Topic = MapTopic(item.TopicOption),
                Store = MapStore(item.Store),
                Order = MapOrder(item.Order),
                Seller = MapSeller(item.Seller)
            };
        }

        public static Context.Item UnMapItem(Domain.Model.Item item)
        {
            return new Context.Item
            {
                ItemId = item.Id,
                StoreId = item.Store.Id,
                OrderId = item.Order.Id,
                SellerId = item.Seller.Id,
                TopicId = item.Topic.Id,
                ItemName = item.Name,
                ItemDescription = item.Description,
                ItemPrice = item.Price,
                Store = UnMapStore(item.Store),
                Order = UnMapOrder(item.Order),
                Seller = UnMapSeller(item.Seller),
                TopicOption = UnMapTopic(item.Topic)
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
                Item = store.Items.Select(UnMapItem).ToList()
            };
        }

        public static Domain.Model.TopicOption MapTopic(Context.TopicOption topic)
        {
            return new Domain.Model.TopicOption
            {
                Id = topic.TopicOptionId,
                Topic = topic.TopicName,
                Items = topic.Item.Select(MapItem).ToList()
            };
        }

        public static Context.TopicOption UnMapTopic(Domain.Model.TopicOption topic)
        {
            return new Context.TopicOption
            {
                TopicOptionId = topic.Id,
                TopicName = topic.Topic,
                Item = topic.Items.Select(UnMapItem).ToList()
            };
        }

        public static Domain.Model.Order MapOrder(Context.Order order)
        {
            return new Domain.Model.Order
            {
                Id = order.OrderId,
                Date = order.OrderDate,
                Price = order.TotalOrderPrice,
                Items = order.Item.Select(MapItem).ToList()
            };
        }

        public static Context.Order UnMapOrder(Domain.Model.Order order)
        {
            return new Context.Order
            {
                OrderId = order.Id,
                OrderDate = order.Date,
                TotalOrderPrice = order.Price,
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
                Item = seller.Items.Select(UnMapItem).ToList(),
                Review = seller.Reviews.Select(UnMapReview).ToList()
            };
        }

        public static Domain.Model.Review MapReview(Context.Review review)
        {
            return new Domain.Model.Review
            {
                Id = review.ReviewId,
                Score = review.Score,
                Comment = review.Comment,
                Person = MapPerson(review.Person),
                Seller = MapSeller(review.Seller)
            };
        }

        public static Context.Review UnMapReview(Domain.Model.Review review)
        {
            return new Context.Review
            {
                ReviewId = review.Id,
                PersonId = review.Person.Id,
                SellerId = review.Seller.Id,
                Score = review.Score,
                Comment = review.Comment,
                Person = UnMapPerson(review.Person),
                Seller = UnMapSeller(review.Seller)
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
                EmployeeTag = person.Employee
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
                Employee = person.EmployeeTag
            };
        }

    }
}
