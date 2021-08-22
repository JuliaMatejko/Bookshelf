using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data;
using Bookshelf.Models;
using Bookshelf.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Bookshelf.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books
                        .Include(b => b.AuthorsBooks)
                            .ThenInclude(e => e.Author)
                        .AsNoTracking()
                        .ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorsBooks)
                    .ThenInclude(e => e.Author)
                 .Include(b => b.BooksKeywords)
                    .ThenInclude(e => e.Keyword)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BookID == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        private void PopulateAssignedAuthorData(Book book)
        {
            var allAuthors = _context.Authors;
            var authorsOfBooks = new HashSet<int>(book.AuthorsBooks.Select(c => c.AuthorID));
            var viewModel = new List<AssignedAuthorData>();
            foreach (var author in allAuthors)
            {
                viewModel.Add(new AssignedAuthorData
                {
                    AuthorID = author.AuthorID,
                    FullName = author.FullName,
                    Assigned = authorsOfBooks.Contains(author.AuthorID)
                });
            }
            ViewData["Authors"] = viewModel;
        }

        private void PopulateAssignedKeywordData(Book book)
        {
            var allKeywords = _context.Kewords;
            var bookKeywords = new HashSet<string>(book.BooksKeywords.Select(c => c.KeywordID));
            var viewModel = new List<AssignedKeywordData>();
            foreach (var keyword in allKeywords)
            {
                viewModel.Add(new AssignedKeywordData
                {
                    KeywordID = keyword.KeywordID,
                    Assigned = bookKeywords.Contains(keyword.KeywordID)
                });
            }
            ViewData["Keywords"] = viewModel;
        }

        // GET: Books/Create
        [Authorize]
        public IActionResult Create()
        {
            var book = new Book();
            book.AuthorsBooks = new List<AuthorBook>();
            book.BooksKeywords = new List<BookKeyword>();
            PopulateAssignedAuthorData(book);
            PopulateAssignedKeywordData(book);
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Title,AddedOn,Description,AuthorsBooks,BooksKeywords")] Book book, int[] selectedAuthors, string[] selectedKeywords)
        {
            if (selectedAuthors != null)
            {
                book.AuthorsBooks = new List<AuthorBook>();
                foreach (var author in selectedAuthors)
                {
                    var authorToAdd = new AuthorBook { BookID = book.BookID, AuthorID = author };
                    book.AuthorsBooks.Add(authorToAdd);
                }
            }
            if (selectedKeywords != null)
            {
                book.BooksKeywords = new List<BookKeyword>();
                foreach (var keyword in selectedKeywords)
                {
                    var keywordToAdd = new BookKeyword { BookID = book.BookID, KeywordID = keyword };
                    book.BooksKeywords.Add(keywordToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ;
            PopulateAssignedAuthorData(book);
            PopulateAssignedKeywordData(book);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorsBooks)
                    .ThenInclude(e => e.Author)
                 .Include(b => b.BooksKeywords)
                    .ThenInclude(e => e.Keyword)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }
            PopulateAssignedAuthorData(book);
            PopulateAssignedKeywordData(book);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int? id, int[] selectedAuthors, string[] selectedKeywords)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Books
                               .Include(b => b.AuthorsBooks)
                                    .ThenInclude(e => e.Author)
                               .Include(b => b.BooksKeywords)
                                    .ThenInclude(e => e.Keyword)
                               .FirstOrDefaultAsync(s => s.BookID == id);

            if (await TryUpdateModelAsync(
                bookToUpdate,
                "",
                i => i.Title, i => i.AddedOn, i => i.Description))
            {
                UpdateBookAuthors(selectedAuthors, bookToUpdate);
                UpdateBookKeywords(selectedKeywords, bookToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /*ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateBookAuthors(selectedAuthors, bookToUpdate);
            UpdateBookKeywords(selectedKeywords, bookToUpdate);
            PopulateAssignedAuthorData(bookToUpdate);
            PopulateAssignedKeywordData(bookToUpdate);
            return View(bookToUpdate);
        }

        private void UpdateBookAuthors(int[] selectedAuthors, Book bookToUpdate)
        {
            if (selectedAuthors == null)
            {
                bookToUpdate.AuthorsBooks = new List<AuthorBook>();
                return;
            }

            var selectedAuthorsHS = new HashSet<int>(selectedAuthors);
            var booksAuthors = new HashSet<int>
                (bookToUpdate.AuthorsBooks.Select(a => a.Author.AuthorID));
            foreach (var author in _context.Authors)
            {
                if (selectedAuthorsHS.Contains(author.AuthorID))
                {
                    if (!booksAuthors.Contains(author.AuthorID))
                    {
                        bookToUpdate.AuthorsBooks.Add(new AuthorBook { BookID = bookToUpdate.BookID, AuthorID = author.AuthorID });
                    }
                }
                else
                {
                    if (booksAuthors.Contains(author.AuthorID))
                    {
                        AuthorBook authorToRemove = bookToUpdate.AuthorsBooks.FirstOrDefault(i => i.AuthorID == author.AuthorID);
                        _context.Remove(authorToRemove);
                    }
                }
            }
        }

        private void UpdateBookKeywords(string[] selectedKeywords, Book bookToUpdate)
        {
            if (selectedKeywords == null)
            {
                bookToUpdate.BooksKeywords = new List<BookKeyword>();
                return;
            }

            var selectedKeywordsHS = new HashSet<string>(selectedKeywords);
            var booksKeywords = new HashSet<string>
                (bookToUpdate.BooksKeywords.Select(k => k.Keyword.KeywordID));
            foreach (var keyword in _context.Kewords)
            {
                if (selectedKeywordsHS.Contains(keyword.KeywordID))
                {
                    if (!booksKeywords.Contains(keyword.KeywordID))
                    {
                        bookToUpdate.BooksKeywords.Add(new BookKeyword { BookID = bookToUpdate.BookID, KeywordID = keyword.KeywordID });
                    }
                }
                else
                {

                    if (booksKeywords.Contains(keyword.KeywordID))
                    {
                        BookKeyword courseToRemove = bookToUpdate.BooksKeywords.FirstOrDefault(i => i.KeywordID == keyword.KeywordID);
                        _context.Remove(courseToRemove);
                    }
                }
            }
        }

        // GET: Books/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorsBooks)
                    .ThenInclude(e => e.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
