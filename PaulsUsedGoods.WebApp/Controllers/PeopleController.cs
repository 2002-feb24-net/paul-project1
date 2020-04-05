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
    public class PeopleController : Controller
    {

        public IItemRepository RepoItem { get; }
        public IStoreRepository RepoStore { get; }
        public IOrderRepository RepoOrd { get; }
        public ITopicOptionRepository RepoTopi { get; }
        public ISellerRepository RepoSell { get; }
        public IPersonRepository RepoPers {get;}

        public PeopleController(IItemRepository repoItem,IStoreRepository repoStore, IOrderRepository repoOrd, ITopicOptionRepository repoTopi,ISellerRepository repoSell, IPersonRepository repoPers)
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
            List<Domain.Model.Person> people = RepoPers.GetPeopleByName();
            List<PersonViewModel> realPeople = new List<PersonViewModel>();
            foreach (var val in people)
            {
                realPeople.Add(new PersonViewModel
                {
                    PersonId = val.Id,
                    FirstName = val.FirstName,
                    LastName = val.LastName,
                    Username = val.Username,
                    Password = val.Password,
                    Employee = val.EmployeeTag,
                    StoreName = RepoStore.GetStoreById(val.StoreId).Name,
                });
            }
            if (search != null)
            {
                return View(realPeople.FindAll(p => p.Username.ToLower().Contains(search.ToLower())));
            }
            return View(realPeople);
        }
        // GET: Items/Create
        public IActionResult Create()
        {
            List<string> myStores = new List<string> ();
            foreach (var val in RepoStore.GetStoresByName().ToList())
            {
                myStores.Add(val.Name);
            }
            ViewData["StoreName"] = new SelectList (myStores);
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Username,Password,Employee,StoreName")] PersonViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var person = new Domain.Model.Person
                    {
                        Id = viewModel.PersonId,
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        Username = viewModel.Username,
                        Password = viewModel.Password,
                        EmployeeTag = false,
                        StoreId = RepoStore.GetStoresByName(viewModel.StoreName).First( p => p.Name.ToLower() == viewModel.StoreName.ToLower()).Id,
                    };
                    List<string> myStores = new List<string> ();
                    foreach (var val in RepoStore.GetStoresByName().ToList())
                    {
                        myStores.Add(val.Name);
                    }
                    ViewData["StoreName"] = new SelectList (myStores);

                    RepoPers.AddPerson(person);
                    RepoPers.Save();

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
            Domain.Model.Person person = RepoPers.GetPersonById(id);
            var viewModel = new PersonViewModel
            {
                PersonId = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Username = person.Username,
                Password = person.Password,
                Employee = person.EmployeeTag,
                StoreName = RepoStore.GetStoreById(person.StoreId).Name,
            };
            List<string> myStores = new List<string> ();
            foreach (var val in RepoStore.GetStoresByName().ToList())
            {
                myStores.Add(val.Name);
            }
            ViewData["StoreName"] = new SelectList (myStores);
            return View(viewModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [Bind("FirstName,LastName,Username,Password,Employee,StoreName")] PersonViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Model.Person person = RepoPers.GetPersonById(id);
                    person.FirstName = viewModel.FirstName;
                    person.LastName = viewModel.LastName;
                    person.Username = viewModel.Username;
                    person.Password = viewModel.Password;
                    person.EmployeeTag = viewModel.Employee;
                    person.StoreId = RepoStore.GetStoresByName(viewModel.StoreName).First( p => p.Name.ToLower() == viewModel.StoreName.ToLower()).Id;
                    RepoPers.UpdatePerson(person);
                    RepoPers.Save();

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
            Domain.Model.Person person = RepoPers.GetPersonById(id);
            var viewModel = new PersonViewModel
            {
                PersonId = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Username = person.Username,
                Password = person.Password,
                Employee = person.EmployeeTag,
                StoreName = RepoStore.GetStoreById(person.StoreId).Name,
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
                RepoPers.DeletePersonById(id);
                RepoPers.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
