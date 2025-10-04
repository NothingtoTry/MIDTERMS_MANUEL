using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PubsMvc.Models;

namespace PubsMvc.Controllers
{
    public class TitlesController : Controller
    {
        private readonly pubsContext _context;

        public TitlesController(pubsContext context)
        {
            _context = context;
        }

        // GET: Titles
        public async Task<IActionResult> Index()
        {
            var titles = await _context.Titles
                .Include(t => t.Pub)   // eager load Publisher info
                .ToListAsync();
            return View(titles);
        }

        // GET: Titles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var title = await _context.Titles
                .Include(t => t.Pub)
                .FirstOrDefaultAsync(m => m.TitleId == id);

            if (title == null) return NotFound();

            return View(title);
        }

        // GET: Titles/Create
        public IActionResult Create()
        {
            ViewBag.Publishers = _context.Publishers.ToList();
            return View();
        }

        // POST: Titles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TitleId,Title1,Type,PubId,Price,Advance,Royalty,YtdSales,Notes,Pubdate")] Title title)
        {
            if (ModelState.IsValid)
            {
                _context.Add(title);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Publishers = _context.Publishers.ToList();
            return View(title);
        }

        // GET: Titles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var title = await _context.Titles.FindAsync(id);
            if (title == null) return NotFound();

            ViewBag.Publishers = _context.Publishers.ToList();
            return View(title);
        }

        // POST: Titles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TitleId,Title1,Type,PubId,Price,Advance,Royalty,YtdSales,Notes,Pubdate")] Title title)
        {
            if (id != title.TitleId) return NotFound();


            if (title.Pubdate == null || title.Pubdate == DateTime.MinValue)
            {
                title.Pubdate = DateTime.Now; // or any default like new DateTime(2000, 1, 1)
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(title);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TitleExists(title.TitleId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Publishers = _context.Publishers.ToList();
            return View(title);
        }

        // GET: Titles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var title = await _context.Titles
                .Include(t => t.Pub)
                .FirstOrDefaultAsync(m => m.TitleId == id);

            if (title == null) return NotFound();

            return View(title);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var title = await _context.Titles.FindAsync(id);
            if (title != null)
            {
                _context.Titles.Remove(title);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TitleExists(string id)
        {
            return _context.Titles.Any(e => e.TitleId == id);
        }
    }
}
