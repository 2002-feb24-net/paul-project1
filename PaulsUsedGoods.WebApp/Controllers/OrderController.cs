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
using PaulsUsedGoods.WebApp.Logic;
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
        public IOrder MyOrder {get;}

        public OrderController(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers, IReviewRepository repoRev, IOrder order)
        {
            RepoItem = repoItem ?? throw new ArgumentNullException(nameof(repoItem));
            RepoStore = repoStore ?? throw new ArgumentNullException(nameof(repoStore));
            RepoOrd = repoOrd ?? throw new ArgumentNullException(nameof(repoOrd));
            RepoTopi = repoTopi ?? throw new ArgumentNullException(nameof(repoTopi));
            RepoSell = repoSell ?? throw new ArgumentNullException(nameof(repoSell));
            RepoPers = repoPers ?? throw new ArgumentNullException(nameof(repoPers));
            RepoRev = repoRev ?? throw new ArgumentNullException(nameof(repoRev));
            MyOrder = order ?? throw new ArgumentNullException(nameof(Order));
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
        public ActionResult LogIn([Bind("Username,Password")] LogInViewModel viewModelLog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MyOrder.Username = viewModelLog.Username;
                    var itemlogin = PopulateItemList.Populate(RepoItem,RepoStore, RepoOrd, RepoTopi,RepoSell, RepoPers, RepoRev, MyOrder); // new ItemAnLogInViewModel

                    if(RepoPers.GetPeopleByName(viewModelLog.Username).First(p => p.Username == viewModelLog.Username).Password == viewModelLog.Password)
                    {
                        return View("Order",itemlogin);
                    }
                }
                return View(viewModelLog);
            }
            catch
            {
                return View(viewModelLog);
            }
        }

        public ActionResult Order([Bind("LogInViewModel,Items,selectedItems")]ItemAnLogInViewModel viewModelLog)
        {
            return View(viewModelLog);
        }

        public ActionResult AddToOrder(int id)
        {
            MyOrder.AddToCurrentOrder(id);
            return View("Order",PopulateItemList.Populate(RepoItem,RepoStore, RepoOrd, RepoTopi,RepoSell, RepoPers, RepoRev, MyOrder));
        }


        [HttpPost, ActionName("Order")]
        [ValidateAntiForgeryToken]
        public ActionResult Order([FromRoute]int id, [Bind("LogInViewModel,Items,selectedItems")]ItemAnLogInViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel = (ItemAnLogInViewModel ) TempData["Login"];
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
                }
            return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }

        public ActionResult ViewCart(int id = 0)
        {
            double price = 0;
            List<Domain.Model.Item> orderItemsList = new List<Domain.Model.Item>();
            foreach (var val in MyOrder.itemsInOrder)
            {
                orderItemsList.Add(RepoItem.GetItemById(val));
                price = price + RepoItem.GetItemById(val).Price;
            }
            if (id > 0)
            {
                MyOrder.itemsInOrder.Add(id);
                orderItemsList.Add(RepoItem.GetItemById(id));
                price = price + RepoItem.GetItemById(id).Price;
            }
            var viewModel = new DetailedOrderViewModel
            {
                PersonName = MyOrder.Username,
                StoreName = RepoStore.GetStoreById(RepoPers.GetPeopleByName(MyOrder.Username).First(p => p.Username.ToLower() == MyOrder.Username.ToLower()).StoreId).Name,
                DateOfOrder = DateTime.Now,
                Price = price,
                ItemList = orderItemsList,
                SuggestedItem = GetSuggestedItem.Suggest(RepoItem,RepoStore, RepoOrd, RepoTopi,RepoSell, RepoPers, RepoRev, MyOrder)
            };
            return View(viewModel);
        }

        public ActionResult Finalize()
        {
            try
            {
                double price = 0;
                List<Domain.Model.Item> orderItemsList = new List<Domain.Model.Item>();
                foreach (var val in MyOrder.itemsInOrder)
                {
                    orderItemsList.Add(RepoItem.GetItemById(val));
                    price = price + RepoItem.GetItemById(val).Price;
                }
                Domain.Model.Order myNewOrder= new Domain.Model.Order
                {
                    UserId = RepoPers.GetPeopleByName(MyOrder.Username).First().Id,
                    Date = DateTime.Now,
                    Price = price,
                    Items = orderItemsList
                };
                RepoOrd.AddOrder(myNewOrder);
                RepoOrd.Save();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index", "Home");
        }

    }
}