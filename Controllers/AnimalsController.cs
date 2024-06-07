using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Rental.Models;
using System.Drawing.Drawing2D;
using System.Xml.Linq;

namespace Animal_Rental.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly AppDbContext _context;

        public AnimalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Animals.Include(a => a.Users);
            return View(await appDbContext.ToListAsync());
        }
        public async Task<IActionResult> Rental()
        {
            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name))
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }

            IEnumerable<Animals> model = _context.Animals.Where(p => p.State == "Rent").Include(a => a.Users);
            if (!model.Any())
            {
                return NotFound($"No products.");
            }
            else
            {
                return View(model);
            }

        }
        public async Task<IActionResult> Adoption()
        {
            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name))
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }

            IEnumerable<Animals> model = _context.Animals.Where(p => p.State == "Adoption").Include(a => a.Users);
            if (!model.Any())
            {
                return NotFound($"No products.");
            }
            else
            {
                return View(model);
            }

        }
        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animals = await _context.Animals
                .Include(a => a.Users)
                .FirstOrDefaultAsync(m => m.A_Id == id);
            if (animals == null)
            {
                return NotFound();
            }

            return View(animals);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {

            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name))
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }


            ViewData["User_Id"] = new SelectList(_context.Users, "U_Id", "Email");
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("A_Id,User_Id,Name,Age,State,Birth_Certificate,Type,Gender,ClientFile")] Animals animals)
        {
            var userEmail = HttpContext.Session.GetString("Email");
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail);
            if (user != null)
            {
                animals.User_Id = user.U_Id;
            }
            else
            {
                return View("Error");
            }

            if (animals.ClientFile != null)
            {
                MemoryStream stream = new MemoryStream();
                animals.ClientFile.CopyTo(stream);
                animals.Animal_Images = stream.ToArray();
            }
            _context.Add(animals);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["User_Id"] = new SelectList(_context.Users, "U_Id", "Email", animals.User_Id);
            return View(animals);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name))
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animals = await _context.Animals.FindAsync(id);
            if (animals == null)
            {
                return NotFound();
            }
            ViewData["User_Id"] = new SelectList(_context.Users, "U_Id", "U_Id", animals.User_Id);
            return View(animals);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("A_Id,User_Id,Name,Age,State,Birth_Certificate,Type,Gender,ClientFile")] Animals animals)
        {
            try
            {
                var existingAnimal = await _context.Animals.FindAsync(id);
                if (existingAnimal != null)
                {
                    _context.Entry(existingAnimal).State = EntityState.Detached;
                }

                if (animals.ClientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    animals.ClientFile.CopyTo(stream);
                    animals.Animal_Images = stream.ToArray();
                }
                else if (existingAnimal != null)
                {
                    // If no new file is uploaded, retrieve the existing image from the database
                    animals.Animal_Images = existingAnimal.Animal_Images;
                }

                _context.Update(animals);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalsExists(animals.A_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["User_Id"] = new SelectList(_context.Users, "U_Id", "U_Id", animals.User_Id);
            return View(animals);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name))
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animals = await _context.Animals
                .Include(a => a.Users)
                .FirstOrDefaultAsync(m => m.A_Id == id);
            if (animals == null)
            {
                return NotFound();
            }

            return View(animals);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set 'AppDbContext.Animals'  is null.");
            }
            var animals = await _context.Animals.FindAsync(id);
            if (animals != null)
            {
                _context.Animals.Remove(animals);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalsExists(int id)
        {
          return (_context.Animals?.Any(e => e.A_Id == id)).GetValueOrDefault();
        }
    }
}
