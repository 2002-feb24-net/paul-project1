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
    public class OrderController : Controller
    {
        public IItemRepository RepoItem { get; }
        public IStoreRepository RepoStore { get; }
        public IOrderRepository RepoOrd { get; }
        public ITopicOptionRepository RepoTopi { get; }
        public ISellerRepository RepoSell { get; }
        public IPersonRepository RepoPers {get;}
        public IReviewRepository RepoRev {get;}

        public OrderController(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers, IReviewRepository repoRev)
        {
            RepoItem = repoItem ?? throw new ArgumentNullException(nameof(repoItem));
            RepoStore = repoStore ?? throw new ArgumentNullException(nameof(repoStore));
            RepoOrd = repoOrd ?? throw new ArgumentNullException(nameof(repoOrd));
            RepoTopi = repoTopi ?? throw new ArgumentNullException(nameof(repoTopi));
            RepoSell = repoSell ?? throw new ArgumentNullException(nameof(repoSell));
            RepoPers = repoPers ?? throw new ArgumentNullException(nameof(repoPers));
            RepoRev = repoRev ?? throw new ArgumentNullException(nameof(repoRev));
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind("Username,Password")] LogInViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Order),viewModel);
                }
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }

        public ActionResult Order([Bind("Username,Password")] LogInViewModel viewModel)
        {
            List<Domain.Model.Item> items = RepoItem.GetItemsByName();
            List<ItemViewModel> realItems = new List<ItemViewModel>();
            foreach (var val in items)
            {
                if (val.StoreId == (RepoPers.GetPeopleByName(viewModel.Username).First( p => p.Username.ToLower() == viewModel.Username.ToLower())).StoreId)
                {
                    realItems.Add(new ItemViewModel
                    {
                        ItemId = val.Id,
                        ItemName = val.Name,
                        ItemDescription = val.Description,
                        ItemPrice = val.Price,
                        StoreName = RepoStore.GetStoreById(val.StoreId).Name,
                        OrderId = val.OrderId,
                        SellerName = RepoSell.GetSellerById(val.SellerId).Name,
                        TopicName = RepoTopi.GetTopicById(val.TopicId).Topic
                    });
                }
            }

            var itemlogin = new ItemAnLogInViewModel
            {
                Items = realItems,
                LogInViewModel = viewModel,
                selectedItems = new List<ItemViewModel>()
            };
            return View(itemlogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order([FromRoute]int id, [Bind("LogInViewModel,Items,selectedItems")]ItemAnLogInViewModel viewModel)
        {
            var myOldItem = RepoItem.GetItemById(id);
            var myNewItem = new ItemViewModel
            {
                ItemId = myOldItem.Id,
                ItemName = myOldItem.Name,
                ItemDescription = myOldItem.Description,
                ItemPrice = myOldItem.Price,
                StoreName = RepoStore.GetStoreById(myOldItem.StoreId).Name,
                OrderId = myOldItem.OrderId,
                SellerName = RepoSell.GetSellerById(myOldItem.SellerId).Name,
                TopicName = RepoTopi.GetTopicById(myOldItem.TopicId).Topic
            };
            viewModel.selectedItems.Add(myNewItem);
            viewModel.Items.Remove(viewModel.Items.Find(p => p.ItemId == myNewItem.ItemId));

            return View(viewModel);
        }
    }
}