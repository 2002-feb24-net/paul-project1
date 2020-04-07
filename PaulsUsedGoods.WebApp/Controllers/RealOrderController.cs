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
    public class RealOrderController : Controller
    {

        public IItemRepository RepoItem { get; }
        public IStoreRepository RepoStore { get; }
        public IOrderRepository RepoOrd { get; }
        public ITopicOptionRepository RepoTopi { get; }
        public ISellerRepository RepoSell { get; }
        public IPersonRepository RepoPers {get;}

        public RealOrderController(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers)
        {
            RepoItem = repoItem ?? throw new ArgumentNullException(nameof(repoItem));
            RepoStore = repoStore ?? throw new ArgumentNullException(nameof(repoStore));
            RepoOrd = repoOrd ?? throw new ArgumentNullException(nameof(repoOrd));
            RepoTopi = repoTopi ?? throw new ArgumentNullException(nameof(repoTopi));
            RepoSell = repoSell ?? throw new ArgumentNullException(nameof(repoSell));
            RepoPers = repoPers ?? throw new ArgumentNullException(nameof(repoPers));
        }


        // GET: Items
        public ActionResult Index([FromQuery] string search = "")
        {
            List<Domain.Model.Order> orders = RepoOrd.GetOrdersByName();
            List<OrderViewModel> realOrders = new List<OrderViewModel>();
            foreach (var val in orders)
            {
                realOrders.Add(new OrderViewModel
                {
                    OrderId = val.Id,
                    PersonName = val.Username,
                    StoreName = RepoStore.GetStoreById(RepoPers.GetPersonById(val.UserId).StoreId).Name,
                    OrderDate = val.Date,
                    TotalOrderPrice = val.Price
                });
            }
            if (search != null)
            {
                return View(realOrders.FindAll(p => p.PersonName.ToLower().Contains(search.ToLower()) || (RepoStore.GetStoreById(RepoPers.GetPeopleByName(p.PersonName).First().StoreId).Name.ToLower()).Contains(search.ToLower())));
            }
            return View(realOrders);
        }

        public ActionResult Details(int id)
        {
            Domain.Model.Order order = RepoOrd.GetOrderById(id);
            var viewModel = new OrderViewModel
            {
                OrderId = order.Id,
                SortMethod = "Default",
                PersonName = order.Username,
                StoreName = RepoStore.GetStoreById(RepoPers.GetPersonById(order.UserId).StoreId).Name,
                OrderDate = order.Date,
                TotalOrderPrice = order.Price,
                Items = order.Items.Select(y => new ItemViewModel
                {
                    ItemId = y.Id,
                    ItemName = y.Name,
                    ItemDescription = y.Description,
                    ItemPrice = y.Price,
                    StoreName = RepoStore.GetStoreById(y.StoreId).Name,
                    OrderId = y.OrderId,
                    SellerName = RepoSell.GetSellerById(y.SellerId).Name,
                    TopicName = RepoTopi.GetTopicById(y.TopicId).Topic
                }).ToList()
            };
            List<string> mySorters = new List<string> {"Default","Old to New","New to Old","Low Price to High Price", "High Price to Low Price"};
            ViewData["Sorter"] = new SelectList (mySorters);
            return View(viewModel);
        }

        // GET: Items/Create
        public IActionResult Delete(int id)
        {
            Domain.Model.Order order = RepoOrd.GetOrderById(id);
            var viewModel = new OrderViewModel
            {
                OrderId = order.Id,
                PersonName = order.Username,
                StoreName = RepoStore.GetStoreById(RepoPers.GetPersonById(order.UserId).StoreId).Name,
                OrderDate = order.Date,
                TotalOrderPrice = order.Price
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
                RepoItem.DeleteItemByOrderId(id);
                RepoOrd.DeleteOrderById(id);
                RepoOrd.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}