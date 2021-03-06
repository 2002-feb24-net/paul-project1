﻿using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface IReviewRepository
    {
// ! CLASS SPECIFIC
        List<Review> GetReviewByUserName(string reviewName = null);
        List<Domain.Model.Review> GetReviewBySellerName(string reviewSellerName = null);
        Review GetReviewById(int reviewId);
        void AddReview(Review inputReview);
        void DeleteReviewById(int reviewId);
        void DeleteReviewBySellerId(int sellerId);
        void UpdateReview(Review inputReview);
// // ! RELATED TO ORDERS
//         List<Order> GetOrderByName(string personName = null);
//         Order GetOrderById(int orderId);
// ! GENERAL COMMANDS
        void Save();
    }
}
