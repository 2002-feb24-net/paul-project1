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
    public class ReviewRepository : IReviewRepository
    {
        private readonly UsedGoodsDbContext _dbContext;
        private readonly ILogger<ItemRepository> _logger;

        public ReviewRepository(UsedGoodsDbContext dbContext, ILogger<ItemRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
// ! CLASS SPECIFIC
        public List<Domain.Model.Review> GetReviewByUserName(string reviewUserName = null)
        {
            _logger.LogInformation($"Retrieving reviews with the name: {reviewUserName}");
            List<Context.Review> reviewList = _dbContext.Reviews
                .Include(p => p.Person)
                .Include(p => p.Seller)
                .ToList();
            if (reviewUserName != null)
            {
                reviewList = reviewList.FindAll(p => p.Person.Username.ToLower() == reviewUserName.ToLower());
            }
            return reviewList.Select(Mapper.MapReview).ToList();
        }
        public List<Domain.Model.Review> GetReviewBySellerName(string reviewSellerName = null)
        {
            _logger.LogInformation($"Retrieving reviews with the name: {reviewSellerName}");
            List<Context.Review> reviewList = _dbContext.Reviews
                .Include(p => p.Person)
                .Include(p => p.Seller)
                .ToList();
            if (reviewSellerName != null)
            {
                reviewList = reviewList.FindAll(p => p.Seller.SellerName == reviewSellerName);
            }
            return reviewList.Select(Mapper.MapReview).ToList();
        }
        public Domain.Model.Review GetReviewById(int reviewId)
        {
            _logger.LogInformation($"Retrieving review id: {reviewId}");
            Context.Review returnReview = _dbContext.Reviews
                .Include(p => p.Person)
                .Include(p => p.Seller)
                .First(p => p.ReviewId == reviewId);
            return Mapper.MapReview(returnReview);
        }
        public void AddReview(Domain.Model.Review inputReview)
        {
            if (inputReview.Id != 0)
            {
                _logger.LogWarning($"Review to be added has an ID ({inputReview.Id}) already: ignoring.");
            }

            _logger.LogInformation("Adding review");

            Context.Review entity = Mapper.UnMapReview(inputReview);
            entity.ReviewId = 0;
            _dbContext.Add(entity);
        }
        public void DeleteReviewById(int reviewId)
        {
            _logger.LogInformation($"Deleting review with ID {reviewId}");
            Context.Review entity = _dbContext.Reviews.Find(reviewId);
            _dbContext.Remove(entity);
        }
        public void UpdateReview(Domain.Model.Review inputReview)
        {
            _logger.LogInformation($"Updating review with ID {inputReview.Id}");
            Context.Review currentEntity = _dbContext.Reviews.Find(inputReview.Id);
            Context.Review newEntity = Mapper.UnMapReview(inputReview);

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
