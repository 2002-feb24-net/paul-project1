using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PaulsUsedGoods.DataAccess.Context;
using PaulsUsedGoods.Domain.Interfaces;
using PaulsUsedGoods.WebApp.Controllers;
using PaulsUsedGoods.WebApp.ViewModels;

namespace PaulsUsedGoods.WebApp.Controllers
{
    public class ReviewsController : Controller
    {

        public IItemRepository RepoItem { get; }
        public IStoreRepository RepoStore { get; }
        public IOrderRepository RepoOrd { get; }
        public ITopicOptionRepository RepoTopi { get; }
        public ISellerRepository RepoSell { get; }
        public IPersonRepository RepoPers {get;}
        public IReviewRepository RepoRev {get;}

        public ReviewsController(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers, IReviewRepository repoRev)
        {
            RepoItem = repoItem ?? throw new ArgumentNullException(nameof(repoItem));
            RepoStore = repoStore ?? throw new ArgumentNullException(nameof(repoStore));
            RepoOrd = repoOrd ?? throw new ArgumentNullException(nameof(repoOrd));
            RepoTopi = repoTopi ?? throw new ArgumentNullException(nameof(repoTopi));
            RepoSell = repoSell ?? throw new ArgumentNullException(nameof(repoSell));
            RepoPers = repoPers ?? throw new ArgumentNullException(nameof(repoPers));
            RepoRev = repoRev ?? throw new ArgumentNullException(nameof(repoRev));
        }


        // GET: Items
        public ActionResult Index([FromQuery] string search = "")
        {
            List<Domain.Model.Review> reviews = RepoRev.GetReviewByUserName();
            List<ReviewViewModel> realReviews = new List<ReviewViewModel>();
            foreach (var val in reviews)
            {
                realReviews.Add(new ReviewViewModel
                {
                    ReviewId = val.Id,
                    UserName = RepoPers.GetPersonById(val.PersonId).Username,
                    Password = RepoPers.GetPersonById(val.PersonId).Password,
                    SellerName = RepoSell.GetSellerById(val.SellerId).Name,
                    Score = val.Score,
                    Comment = val.Comment
                });
            }
            if (search != null)
            {
                return View(realReviews.FindAll(p => p.UserName.ToLower().Contains(search.ToLower()) || p.SellerName.ToLower().Contains(search.ToLower())));
            }
            return View(realReviews);
        }
        // GET: Items/Create
        public IActionResult Create()
        {
            List<string> myPeople = new List<string> ();
            foreach (var val in RepoPers.GetPeopleByName().ToList())
            {
                myPeople.Add(val.Username);
            }
            ViewData["UserName"] = new SelectList (myPeople);
            List<string> mySellers = new List<string> ();
            foreach (var val in RepoSell.GetSellersByName().ToList())
            {
                mySellers.Add(val.Name);
            }
            ViewData["SellerName"] = new SelectList (mySellers);
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserName,Password,SellerName,Score,Comment")] ReviewViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(viewModel.Password != RepoPers.GetPeopleByName(viewModel.UserName).First(p => p.Username.ToLower() == viewModel.UserName.ToLower()).Password)
                    {
                        return View(viewModel);
                    }
                    var review = new Domain.Model.Review
                    {
                        Id = viewModel.ReviewId,
                        Comment = viewModel.Comment,
                        Score = viewModel.Score,
                        PersonId = RepoPers.GetPeopleByName(viewModel.UserName).First(p => p.Username.ToLower() == viewModel.UserName.ToLower()).Id,
                        SellerId = RepoSell.GetSellersByName(viewModel.SellerName).First(p => p.Name.ToLower() == viewModel.SellerName.ToLower()).Id
                    };
                    List<string> mySellers = new List<string> ();
                    foreach (var val in RepoSell.GetSellersByName().ToList())
                    {
                        mySellers.Add(val.Name);
                    }
                    ViewData["SellerName"] = new SelectList (mySellers);
                    List<string> myPeople = new List<string> ();
                    foreach (var val in RepoPers.GetPeopleByName().ToList())
                    {
                        myPeople.Add(val.Username);
                    }
                    ViewData["UserName"] = new SelectList (myPeople);

                    RepoRev.AddReview(review);
                    RepoRev.Save();

                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Items/Edit/5
        public IActionResult Edit(int id)
        {
            // we pass the current values into the Edit view
            // so that the input fields can be pre-populated instead of blank
            // (important for good UX)
            Domain.Model.Review review = RepoRev.GetReviewById(id);
            var viewModel = new ReviewViewModel
            {
                ReviewId = review.Id,
                UserName = RepoPers.GetPersonById(review.PersonId).Username,
                Password = RepoPers.GetPersonById(review.PersonId).Password,
                SellerName = RepoSell.GetSellerById(review.SellerId).Name,
                Score = review.Score,
                Comment = review.Comment
            };
            List<string> mySellers = new List<string> ();
            foreach (var val in RepoSell.GetSellersByName().ToList())
            {
                mySellers.Add(val.Name);
            }
            ViewData["SellerName"] = new SelectList (mySellers);
            List<string> myPeople = new List<string> ();
            foreach (var val in RepoPers.GetPeopleByName().ToList())
            {
                myPeople.Add(val.Username);
            }
            ViewData["UserName"] = new SelectList (myPeople);
            return View(viewModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [Bind("UserName,SellerName,Score,Comment,Password")] ReviewViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Model.Review review = RepoRev.GetReviewById(id);
                    review.Comment = viewModel.Comment;
                    review.Score = viewModel.Score;
                    review.PersonId = RepoPers.GetPeopleByName(viewModel.UserName).First(p => p.Username.ToLower() == viewModel.UserName.ToLower()).Id;
                    review.SellerId = RepoSell.GetSellersByName(viewModel.SellerName).First(p => p.Name.ToLower() == viewModel.SellerName.ToLower()).Id;
                    RepoRev.UpdateReview(review);
                    RepoRev.Save();

                    return RedirectToAction(nameof(Index));
                }
                    return View(viewModel);
                }
            catch (Exception)
            {
                return View(viewModel);
            }
        }

        // GET: Items/Delete/5
        public IActionResult Delete(int id)
        {
            Domain.Model.Review review = RepoRev.GetReviewById(id);
            var viewModel = new ReviewViewModel
            {
                ReviewId = review.Id,
                UserName = RepoPers.GetPersonById(review.PersonId).Username,
                Password = RepoPers.GetPersonById(review.PersonId).Password,
                SellerName = RepoSell.GetSellerById(review.SellerId).Name,
                Score = review.Score,
                Comment = review.Comment
            };
            return View(viewModel);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [BindNever]IFormCollection collection)
        {
            try
            {
                RepoRev.DeleteReviewById(id);
                RepoRev.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
