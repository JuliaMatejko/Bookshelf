using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ClaimsPrincipal currentUser = User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LastNameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewData["TitleSortParm"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var userbooks = from ub in _context.UsersBooks
                            .Where(s => s.ApplicationUserID == currentUserID)
                            .Include(b => b.Book)
                                .ThenInclude(u => u.AuthorsBooks)
                                    .ThenInclude(a => a.Author)
                            select ub;

            if (!string.IsNullOrEmpty(searchString))
            {
                userbooks = userbooks.Where(a => a.Book.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "lastname_desc":
                    userbooks = userbooks.OrderByDescending(b => b.Book.AuthorsBooks.Select(s => s.Author.LastName).FirstOrDefault());
                    break;
                case "Title":
                    userbooks = userbooks.OrderBy(s => s.Book.Title);
                    break;
                case "title_desc":
                    userbooks = userbooks.OrderByDescending(s => s.Book.Title);
                    break;
                default:
                    userbooks = userbooks.OrderBy(b => b.Book.AuthorsBooks.Select(s => s.Author.LastName).FirstOrDefault());
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<UserBook>.CreateAsync(userbooks.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: UserBooks/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? bookId, string userId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            var userBook = await _context.UsersBooks.Where(s => s.ApplicationUserID == userId)
                        .Include(b => b.Book)
                            .ThenInclude(u => u.AuthorsBooks)
                                .ThenInclude(a => a.Author)
                         .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BookID == bookId);

            if (userBook == null)
            {
                return NotFound();
            }

            return View(userBook);
        }

        // GET: UserBooks/Create
        [Authorize]
        public async Task<IActionResult> Create(int bookid)
        {

            ClaimsPrincipal currentUser = User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userbookOrNull = await _context.UsersBooks.FindAsync(currentUserID, bookid);

            var book = await _context.Books.Where(b => b.BookID == bookid)
                       .Include(s => s.AuthorsBooks)
                       .ThenInclude(a => a.Author)
                       .AsNoTracking()
                       .FirstOrDefaultAsync();

            ViewBag.userId = currentUserID;
            ViewBag.book = book;

            if (userbookOrNull != null)
            {
                return View();
            }
            UserBook userBook = new();
            userBook.BookID = bookid;
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
        public async Task<IActionResult> Edit(int? bookId, string userId)
        {
            if (bookId == null)
            {
                return NotFound();
            }
            var userBook = await _context.UsersBooks.Where(s => s.ApplicationUserID == userId)
                        .Include(b => b.Book)
                            .ThenInclude(u => u.AuthorsBooks)
                                .ThenInclude(a => a.Author)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(s => s.BookID == bookId);
            if (userBook == null)
            {
                return NotFound();
            }
            return View(userBook);
        }

        // POST: UserBooks/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ApplicationUserID,BookID,Rating,Review,BookStatus")] UserBook userBook)
        {
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
                return RedirectToAction(nameof(Details), new { bookId = userBook.BookID, userId = userBook.ApplicationUserID });
            }
            return View(userBook);
        }

        // GET: UserBooks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string userId, int? bookId)
        {
            if (bookId == null || userId == null)
            {
                return NotFound();
            }

            var userBook = await _context.UsersBooks.Where(s => s.ApplicationUserID == userId)
                        .Include(b => b.Book)
                            .ThenInclude(u => u.AuthorsBooks)
                                .ThenInclude(a => a.Author)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(m => m.BookID == bookId);

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
        public async Task<IActionResult> DeleteConfirmed(string userId, int bookId)
        {
            var userBook = await _context.UsersBooks.FindAsync(userId, bookId);
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
