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
    public class TopicOptionsController : Controller
    {

        public IItemRepository RepoItem { get; }
        public IStoreRepository RepoStore { get; }
        public IOrderRepository RepoOrd { get; }
        public ITopicOptionRepository RepoTopi { get; }
        public ISellerRepository RepoSell { get; }
        public IPersonRepository RepoPers {get;}
        public IReviewRepository RepoRev {get;}

        public TopicOptionsController(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers, IReviewRepository repoRev)
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
            List<Domain.Model.TopicOption> topics = RepoTopi.GetTopicByName();
            List<TopicOptionViewModel> realTopics = new List<TopicOptionViewModel>();
            foreach (var val in topics)
            {
                realTopics.Add(new TopicOptionViewModel
                {
                    TopicOptionId = val.Id,
                    TopicName = val.Topic
                });
            }
            if (search != null)
            {
                return View(realTopics.FindAll(p => p.TopicName.ToLower().Contains(search.ToLower())));
            }
            return View(realTopics);
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
        public IActionResult Create([Bind("TopicName")] TopicOptionViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var topic = new Domain.Model.TopicOption
                    {
                        Id = viewModel.TopicOptionId,
                        Topic = viewModel.TopicName,
                        Items = RepoItem.GetItemsByTopicName(viewModel.TopicName)
                            // .FindAll(p => p.TopicId == (RepoTopi.GetTopicByName(viewModel.TopicName)
                            // .First(p => p.Topic == viewModel.TopicName).Id)),
                    };

                    RepoTopi.AddTopic(topic);
                    RepoTopi.Save();

                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Items/Delete/5
        public IActionResult Delete(int id)
        {
            Domain.Model.TopicOption topic = RepoTopi.GetTopicById(id);
            var viewModel = new TopicOptionViewModel
            {
                TopicOptionId = topic.Id,
                TopicName = topic.Topic
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
                RepoItem.DeleteItemByTopicId(id);
                RepoTopi.DeleteTopicById(id);
                RepoTopi.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}