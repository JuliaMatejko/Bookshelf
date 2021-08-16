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
    public class KeywordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KeywordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Keywords
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kewords.ToListAsync());
        }

        // GET: Keywords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Keywords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _context.Kewords
                .FirstOrDefaultAsync(m => m.KeywordID == id);
            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // POST: Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var keyword = await _context.Kewords.FindAsync(id);
            _context.Kewords.Remove(keyword);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordExists(string id)
        {
            return _context.Kewords.Any(e => e.KeywordID == id);
        }
    }
}
