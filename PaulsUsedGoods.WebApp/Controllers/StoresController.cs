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
    public class StoresController : Controller
    {

        public IItemRepository RepoItem { get; }
        public IStoreRepository RepoStore { get; }
        public IOrderRepository RepoOrd { get; }
        public ITopicOptionRepository RepoTopi { get; }
        public ISellerRepository RepoSell { get; }
        public IPersonRepository RepoPers {get;}
        public IReviewRepository RepoRev {get;}

        public StoresController(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers, IReviewRepository repoRev)
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
            List<Domain.Model.Store> stores = RepoStore.GetStoresByName();
            List<StoreViewModel> realStores = new List<StoreViewModel>();
            foreach (var val in stores)
            {
                realStores.Add(new StoreViewModel
                {
                    StoreId = val.Id,
                    LocationName = val.Name,
                    ItemCount = val.Items.Count
                });
            }
            if (search != null)
            {
                return View(realStores.FindAll(p => p.LocationName.ToLower().Contains(search.ToLower())));
            }
            return View(realStores);
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
        public IActionResult Create([Bind("LocationName")] StoreViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var store = new Domain.Model.Store
                    {
                        Id = viewModel.StoreId,
                        Name = viewModel.LocationName,
                        Items = RepoItem.GetItemsByStoreName(viewModel.LocationName)
                            .FindAll(p => p.StoreId == (RepoStore.GetStoresByName(viewModel.LocationName)
                            .First(p => p.Name == viewModel.LocationName).Id)),
                    };

                    RepoStore.AddStore(store);
                    RepoStore.Save();

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
            Domain.Model.Store store = RepoStore.GetStoreById(id);
            var viewModel = new StoreViewModel
            {
                StoreId = store.Id,
                LocationName = store.Name,
                ItemCount = store.Items.Count
            };
            return View(viewModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [Bind("LocationName")] StoreViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Model.Store store = RepoStore.GetStoreById(id);
                    store.Name = viewModel.LocationName;
                    store.Items = RepoItem.GetItemsByStoreName(viewModel.LocationName)
                        .FindAll(p => p.StoreId == (RepoStore.GetStoresByName(viewModel.LocationName)
                        .First(p => p.Name == viewModel.LocationName).Id));
                    RepoStore.UpdateStore(store);
                    RepoStore.Save();

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
            Domain.Model.Store store = RepoStore.GetStoreById(id);
            var viewModel = new StoreViewModel
            {
                StoreId = store.Id,
                LocationName = store.Name,
                ItemCount = store.Items.Count
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
                RepoPers.DeletePeopleByStoreId(id);
                RepoItem.DeleteItemByStoreId(id);
                RepoStore.DeleteStoreById(id);
                RepoStore.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}