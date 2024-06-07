//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Animal_Rental.Models;

//namespace Animal_Rental.Controllers
//{
//    public class ArticalsController : Controller
//    {
//        private readonly AppDbContext _context;

//        public ArticalsController(AppDbContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public IActionResult Read()
//        {
//            var articles = _context.Articals.ToList();
//            var articlesList = articles.Select(article => new ArticalsModelView
//            {
//                Id = article.Id,
//                Title = article.Type_Of_Animals,
//                Description = article.Content
//            }).ToList();

//            return View(articlesList);
//        }

//        // GET: Articles
//        public async Task<IActionResult> Index()
//        {
//              return _context.Articals != null ? 
//                          View(await _context.Articals.ToListAsync()) :
//                          Problem("Entity set 'AppDbContext.Articles'  is null.");
//        }

//        // GET: Articles/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Articals == null)
//            {
//                return NotFound();
//            }

//            var articles = await _context.Articals
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (articles == null)
//            {
//                return NotFound();
//            }

//            return View(articles);
//        }

//        // GET: Articles/Create
//        public IActionResult Create(int id)
//        {
//            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
//            Response.Headers.Add("Pragma", "no-cache");
//            var name = HttpContext.Session.GetString("Email");
//            if (String.IsNullOrEmpty(name) || id != 7)
//            {

//                var returnUrl = Request.Path.Value;
//                return RedirectToAction("Login", "Users", new { returnUrl });
//            }
//            return View();
//        }

//        // POST: Articles/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Type_Of_Animals,Content")] Articals articles)
//        {

//            _context.Add(articles);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));

//            return View(articles);
//        }

//        // GET: Articles/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Articals == null)
//            {
//                return NotFound();
//            }

//            var articles = await _context.Articals.FindAsync(id);
//            if (articles == null)
//            {
//                return NotFound();
//            }
//            return View(articles);
//        }

//        // POST: Articles/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Type_Of_Animals,Content")] Articals articles)
//        {
//            if (id != articles.Id)
//            {
//                return NotFound();
//            }

//          //  if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(articles);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ArticlesExists(articles.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(articles);
//        }

//        // GET: Articles/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Articals == null)
//            {
//                return NotFound();
//            }

//            var articles = await _context.Articals
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (articles == null)
//            {
//                return NotFound();
//            }

//            return View(articles);
//        }

//        // POST: Articles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Articals == null)
//            {
//                return Problem("Entity set 'AppDbContext.Articles'  is null.");
//            }
//            var articles = await _context.Articals.FindAsync(id);
//            if (articles != null)
//            {
//                _context.Articals.Remove(articles);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ArticlesExists(int id)
//        {
//          return (_context.Articals?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Rental.Models;

namespace Animal_Rental.Controllers
{
    public class ArticalsController : Controller
    {
        private readonly AppDbContext _context;

        public ArticalsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Read()
        {
            var articles = _context.Articals.ToList();
            var articlesList = articles.Select(article => new ArticalsModelView
            {
                Id = article.Id,
                Title = article.Type_Of_Animals,
                Description = article.Content
            }).ToList();

            return View(articlesList);
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return _context.Articals != null ?
                        View(await _context.Articals.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Articles'  is null.");
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articals == null)
            {
                return NotFound();
            }

            var articles = await _context.Articals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }

        // GET: Articles/Create
        public IActionResult Create(int? id)
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type_Of_Animals,Content")] Articals articles)
        {
            //if (ModelState.IsValid)
            //{
            _context.Add(articles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            return View(articles);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articals == null)
            {
                return NotFound();
            }

            var articles = await _context.Articals.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type_Of_Animals,Content")] Articals articles)
        {
            if (id != articles.Id)
            {
                return NotFound();
            }

            //  if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticlesExists(articles.Id))
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
            return View(articles);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articals == null)
            {
                return NotFound();
            }

            var articles = await _context.Articals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articals == null)
            {
                return Problem("Entity set 'AppDbContext.Articles'  is null.");
            }
            var articles = await _context.Articals.FindAsync(id);
            if (articles != null)
            {
                _context.Articals.Remove(articles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticlesExists(int id)
        {
            return (_context.Articals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
