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
    public class SellersController : Controller
    {

        public IItemRepository RepoItem { get; }
        public IStoreRepository RepoStore { get; }
        public IOrderRepository RepoOrd { get; }
        public ITopicOptionRepository RepoTopi { get; }
        public ISellerRepository RepoSell { get; }
        public IPersonRepository RepoPers {get;}
        public IReviewRepository RepoRev {get;}

        public SellersController(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers, IReviewRepository repoRev)
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
            List<Domain.Model.Seller> sellers = RepoSell.GetSellersByName();
            List<SellerViewModel> realSellers = new List<SellerViewModel>();
            foreach (var val in sellers)
            {
                realSellers.Add(new SellerViewModel
                {
                    SellerId = val.Id,
                    SellerName = val.Name,
                    Items = val.Items.Count,
                    AverageReview = val.Rating
                });
            }
            if (search != null)
            {
                return View(realSellers.FindAll(p => p.SellerName.ToLower().Contains(search.ToLower())));
            }
            return View(realSellers);
        }
        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SellerName,Items,AverageReview")] SellerViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var seller = new Domain.Model.Seller
                    {
                        Id = viewModel.SellerId,
                        Name = viewModel.SellerName,
                        Items = RepoItem.GetItemsBySellerName(viewModel.SellerName)
                            .FindAll(p => p.SellerId == (RepoSell.GetSellersByName(viewModel.SellerName)
                            .First(p => p.Name == viewModel.SellerName).Id)),
                        Reviews = RepoRev.GetReviewBySellerName(viewModel.SellerName)
                            .FindAll(p => p.SellerId == (RepoSell.GetSellersByName(viewModel.SellerName)
                            .First(p => p.Name == viewModel.SellerName).Id))
                    };

                    RepoSell.AddSeller(seller);
                    RepoSell.Save();

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
            Domain.Model.Seller seller = RepoSell.GetSellerById(id);
            var viewModel = new SellerViewModel
            {
                SellerId = seller.Id,
                SellerName = seller.Name,
                Items = seller.Items.Count,
                AverageReview = seller.Rating
            };
            return View(viewModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [Bind("SellerName,Items,AverageReview")] SellerViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Model.Seller seller = RepoSell.GetSellerById(id);
                    seller.Name = viewModel.SellerName;
                    seller.Items = RepoItem.GetItemsBySellerName(viewModel.SellerName)
                        .FindAll(p => p.SellerId == (RepoSell.GetSellersByName(viewModel.SellerName)
                        .First(p => p.Name == viewModel.SellerName).Id));
                    seller.Reviews = RepoRev.GetReviewBySellerName(viewModel.SellerName)
                        .FindAll(p => p.SellerId == (RepoSell.GetSellersByName(viewModel.SellerName)
                        .First(p => p.Name == viewModel.SellerName).Id));
                    RepoSell.UpdateSeller(seller);
                    RepoSell.Save();

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
            Domain.Model.Seller seller = RepoSell.GetSellerById(id);
            var viewModel = new SellerViewModel
            {
                SellerId = seller.Id,
                SellerName = seller.Name,
                Items = seller.Items.Count,
                AverageReview = seller.Rating
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
                RepoRev.DeleteReviewBySellerId(id);
                RepoItem.DeleteItemBySellerId(id);
                RepoSell.DeleteSellerById(id);
                RepoSell.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}