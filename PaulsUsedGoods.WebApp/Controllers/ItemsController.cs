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
    public class ItemsController : Controller
    {
        private readonly UsedGoodsDbContext _context;

        public IItemRepository Repo { get; }
        public IStoreRepository RepoStore { get; }
        public IOrderRepository RepoOrd { get; }
        public ITopicOptionRepository RepoTopi { get; }
        public ISellerRepository RepoSell { get; }

        public ItemsController(IItemRepository repo,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell)
        {
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));
            RepoStore = repoStore ?? throw new ArgumentNullException(nameof(repoStore));
            RepoOrd = repoOrd ?? throw new ArgumentNullException(nameof(repoOrd));
            RepoTopi = repoTopi ?? throw new ArgumentNullException(nameof(repoTopi));
            RepoSell = repoSell ?? throw new ArgumentNullException(nameof(repoSell));
        }


        // GET: Items
        public ActionResult Index([FromQuery] string search = "")
        {
            List<Domain.Model.Item> items = Repo.GetItemsByName();
            List<ItemViewModel> realItems = new List<ItemViewModel>();
            foreach (var val in items)
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
            if (search != null)
            {
                return View(realItems.FindAll(p => p.ItemName.ToLower().Contains(search.ToLower()) || p.ItemDescription.ToLower().Contains(search.ToLower())));
            }
            return View(realItems);
        }
        // GET: Items/Create
        public IActionResult Create()
        {
            List<string> mySellers = new List<string> ();
            foreach (var val in RepoSell.GetSellersByName().ToList())
            {
                mySellers.Add(val.Name);
            }
            ViewData["Seller"] = new SelectList (mySellers);
            List<string> myTopics = new List<string> ();
            foreach (var val in RepoTopi.GetTopicByName().ToList())
            {
                myTopics.Add(val.Topic);
            }
            ViewData["Topic"] = new SelectList (myTopics);
            List<string> myStores = new List<string> ();
            foreach (var val in RepoStore.GetStoresByName().ToList())
            {
                myStores.Add(val.Name);
            }
            ViewData["Store"] = new SelectList (myStores);
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ItemName,StoreName,SellerName,TopicName,ItemName,ItemDescription,ItemPrice")] ItemViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var myStore = RepoStore.GetStoresByName().First(p => p.Name.ToLower() == viewModel.StoreName.ToLower()).Id;
                    var mySeller = RepoSell.GetSellersByName().First(p => p.Name.ToLower() == viewModel.SellerName.ToLower()).Id;
                    var myTopic = RepoTopi.GetTopicByName().First(p => p.Topic.ToLower() == viewModel.TopicName.ToLower()).Id;
                    var item = new Domain.Model.Item
                    {
                        Id = 0,
                        Name = viewModel.ItemName,
                        Description = viewModel.ItemDescription,
                        Price = viewModel.ItemPrice,
                        StoreId = myStore,
                        OrderId = viewModel.OrderId,
                        SellerId = mySeller,
                        TopicId = myTopic
                    };
                    Repo.AddItem(item);
                    Repo.Save();

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
            Domain.Model.Item item = Repo.GetItemById(id);
            var viewModel = new ItemViewModel
            {
                ItemId = item.Id,
                ItemName = item.Name,
                ItemDescription = item.Description,
                ItemPrice = item.Price,
                StoreName = RepoStore.GetStoreById(item.StoreId).Name,
                OrderId = item.OrderId,
                SellerName = RepoSell.GetSellerById(item.SellerId).Name,
                TopicName = RepoTopi.GetTopicById(item.TopicId).Topic
            };
            List<string> mySellers = new List<string> ();
            foreach (var val in RepoSell.GetSellersByName().ToList())
            {
                mySellers.Add(val.Name);
            }
            ViewData["Seller"] = new SelectList (mySellers);
            List<string> myTopics = new List<string> ();
            foreach (var val in RepoTopi.GetTopicByName().ToList())
            {
                myTopics.Add(val.Topic);
            }
            ViewData["Topic"] = new SelectList (myTopics);
            List<string> myStores = new List<string> ();
            foreach (var val in RepoStore.GetStoresByName().ToList())
            {
                myStores.Add(val.Name);
            }
            ViewData["Store"] = new SelectList (myStores);
            return View(viewModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [Bind("ItemName,StoreName,SellerName,TopicName,ItemName,ItemDescription,ItemPrice")] ItemViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Model.Item item = Repo.GetItemById(id);
                    item.Name = viewModel.ItemName;
                    item.Description = viewModel.ItemDescription;
                    item.Price = viewModel.ItemPrice;
                    item.StoreId = RepoStore.GetStoresByName(viewModel.StoreName).First(p => p.Name.ToLower() == viewModel.StoreName.ToLower()).Id;
                    item.OrderId = viewModel.OrderId;
                    item.SellerId = RepoSell.GetSellersByName(viewModel.SellerName).First(p => p.Name.ToLower() == viewModel.SellerName.ToLower()).Id;
                    item.TopicId = RepoTopi.GetTopicByName(viewModel.TopicName).First(p => p.Topic.ToLower() == viewModel.TopicName.ToLower()).Id;
                    Repo.UpdateItem(item);
                    Repo.Save();

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
            Domain.Model.Item item = Repo.GetItemById(id);
            var viewModel = new ItemViewModel
            {
                ItemId = item.Id,
                ItemName = item.Name,
                ItemDescription = item.Description,
                ItemPrice = item.Price,
                StoreName = RepoStore.GetStoreById(item.StoreId).Name,
                OrderId = item.OrderId,
                SellerName = RepoSell.GetSellerById(item.SellerId).Name,
                TopicName = RepoTopi.GetTopicById(item.TopicId).Topic
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
                Repo.DeleteItemById(id);
                Repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
