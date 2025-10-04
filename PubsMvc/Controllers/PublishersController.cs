using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PubsMvc.Models;

namespace PubsMvc.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IRepository<Publisher> _repository;

        public PublishersController(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var publishers = await _repository.GetAllAsync();
            return View(publishers);
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PubId,PubName,City,State,Country")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }

        // POST: Publishers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PubId,PubName,City,State,Country")] Publisher publisher)
        {
            if (id != publisher.PubId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(publisher);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
