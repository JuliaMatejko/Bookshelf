using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data;
using Bookshelf.Models;

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

        // GET: Books/Create
        public IActionResult Create()
        {
            PopulateAuthorsDropDownList();
            PopulateKeywordsDropDownList();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Title,AddedOn,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ;
            PopulateAuthorsDropDownList(book.AuthorsBooks.Select(s => s.AuthorID));
            PopulateKeywordsDropDownList(book.BooksKeywords.Select(s => s.KeywordID));
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }
            PopulateAuthorsDropDownList(book.AuthorsBooks.Select(s => s.AuthorID));
            PopulateKeywordsDropDownList(book.BooksKeywords.Select(s => s.KeywordID));
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,AddedOn,Description")] Book book)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
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
            PopulateAuthorsDropDownList(book.AuthorsBooks.Select(s => s.AuthorID));
            PopulateKeywordsDropDownList(book.BooksKeywords.Select(s => s.KeywordID));
            return View(book);
        }

        private void PopulateAuthorsDropDownList(object selectedAuthor = null)
        {
            var authorsQuery = from a in _context.Authors
                                   orderby a.LastName
                                   select a;
            ViewBag.AuthorID = new SelectList(authorsQuery.AsNoTracking(), "AuthorID", "LastName", selectedAuthor);
        }

        private void PopulateKeywordsDropDownList(object selectedKeyword = null)
        {
            var keywordsQuery = from k in _context.Kewords
                                   orderby k.KeywordID
                                   select k;
            ViewBag.KeywordtID = new SelectList(keywordsQuery.AsNoTracking(), "KeywordID", "KeywordID", selectedKeyword);
        }

        // GET: Books/Delete/5
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
