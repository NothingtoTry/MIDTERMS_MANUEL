using Microsoft.AspNetCore.Mvc;
using PubsMvc.Models;

namespace PubsMvc.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorsController(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAllAsync();
            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuId,AuLname,AuFname,Phone,Address,City,State,Zip,Contract")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorRepository.AddAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AuId,AuLname,AuFname,Phone,Address,City,State,Zip,Contract")] Author author)
        {
            if (id != author.AuId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _authorRepository.UpdateAsync(author);
                }
                catch
                {
                    if (await _authorRepository.GetByIdAsync(author.AuId) == null)
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _authorRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
