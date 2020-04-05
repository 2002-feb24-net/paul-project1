using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaulsUsedGoods.DataAccess.Context;

namespace PaulsUsedGoods.WebApp.Controllers
{
    public class TopicOptionsController : Controller
    {
        private readonly UsedGoodsDbContext _context;

        public TopicOptionsController(UsedGoodsDbContext context)
        {
            _context = context;
        }

        // GET: TopicOptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TopicOptions.ToListAsync());
        }

        // GET: TopicOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicOption = await _context.TopicOptions
                .FirstOrDefaultAsync(m => m.TopicOptionId == id);
            if (topicOption == null)
            {
                return NotFound();
            }

            return View(topicOption);
        }

        // GET: TopicOptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopicOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicOptionId,TopicName")] TopicOption topicOption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topicOption);
        }

        // GET: TopicOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicOption = await _context.TopicOptions.FindAsync(id);
            if (topicOption == null)
            {
                return NotFound();
            }
            return View(topicOption);
        }

        // POST: TopicOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicOptionId,TopicName")] TopicOption topicOption)
        {
            if (id != topicOption.TopicOptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicOptionExists(topicOption.TopicOptionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(topicOption);
        }

        // GET: TopicOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicOption = await _context.TopicOptions
                .FirstOrDefaultAsync(m => m.TopicOptionId == id);
            if (topicOption == null)
            {
                return NotFound();
            }

            return View(topicOption);
        }

        // POST: TopicOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topicOption = await _context.TopicOptions.FindAsync(id);
            _context.TopicOptions.Remove(topicOption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicOptionExists(int id)
        {
            return _context.TopicOptions.Any(e => e.TopicOptionId == id);
        }
    }
}
