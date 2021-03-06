using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bookshelf.Controllers
{
    public class KeywordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KeywordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Keywords/FindBooks/5
        public async Task<IActionResult> FindBooks(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _context.Keywords
                .Include(b => b.BookKeyword)
                    .ThenInclude(e => e.Book)
                        .ThenInclude(f => f.AuthorsBooks)
                            .ThenInclude(g => g.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.KeywordID == id);

            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // GET: Keywords
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortParm"] = string.IsNullOrEmpty(sortOrder) ? "desc" : "";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var keywords = from k in _context.Keywords
                          select k;
            if (!string.IsNullOrEmpty(searchString))
            {
                keywords = keywords.Where(a => a.KeywordID.Contains(searchString));
            }
            if (sortOrder == "desc")
            {
                keywords = keywords.OrderByDescending(a => a.KeywordID);
            }
            else
            {
                keywords = keywords.OrderBy(a => a.KeywordID);
            }
            int pageSize = 5;
            return View(await PaginatedList<Keyword>.CreateAsync(keywords.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Keywords/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Keywords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("KeywordID")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keyword);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keyword);
        }

        // GET: Keywords/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _context.Keywords
                .FirstOrDefaultAsync(m => m.KeywordID == id);
            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // POST: Keywords/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var keyword = await _context.Keywords.FindAsync(id);
            _context.Keywords.Remove(keyword);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordExists(string id)
        {
            return _context.Keywords.Any(e => e.KeywordID == id);
        }
    }
}
