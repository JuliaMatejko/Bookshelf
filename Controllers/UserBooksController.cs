using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Bookshelf.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserBooks
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = await _context.UsersBooks.Where(s => s.ApplicationUserID == currentUserID)
                        .Include(b => b.Book)
                            .ThenInclude(u => u.AuthorsBooks)
                                .ThenInclude(a => a.Author)
                         .AsNoTracking()
                         .ToListAsync();
            return View(model);
        }

        // GET: UserBooks/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBook = await _context.UsersBooks
                .Include(u => u.ApplicationUser)
                .Include(u => u.Book)
                .FirstOrDefaultAsync(m => m.ApplicationUserID == id);
            if (userBook == null)
            {
                return NotFound();
            }

            return View(userBook);
        }

        // GET: UserBooks/Create
        [Authorize]
        public IActionResult Create(int bookid)
        {
            UserBook userBook = new();
            userBook.BookID = bookid;

            ClaimsPrincipal currentUser = User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userid = currentUserID;
            return View(userBook);
        }

        // POST: UserBooks/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationUserID,BookID,Rating,Review,BookStatus")] UserBook userBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userBook);
        }

        // GET: UserBooks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBook = await _context.UsersBooks.FindAsync(id);
            if (userBook == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", userBook.ApplicationUserID);
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", userBook.BookID);
            return View(userBook);
        }

        // POST: UserBooks/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ApplicationUserID,BookID,Rating,Review,BookStatus")] UserBook userBook)
        {
            if (id != userBook.ApplicationUserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBookExists(userBook.ApplicationUserID))
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
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", userBook.ApplicationUserID);
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Title", userBook.BookID);
            return View(userBook);
        }

        // GET: UserBooks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBook = await _context.UsersBooks
                .Include(u => u.ApplicationUser)
                .Include(u => u.Book)
                .FirstOrDefaultAsync(m => m.ApplicationUserID == id);
            if (userBook == null)
            {
                return NotFound();
            }

            return View(userBook);
        }

        // POST: UserBooks/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userBook = await _context.UsersBooks.FindAsync(id);
            _context.UsersBooks.Remove(userBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBookExists(string id)
        {
            return _context.UsersBooks.Any(e => e.ApplicationUserID == id);
        }
    }
}
