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
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        //public async Task<IActionResult> Index()
        //{
        //    return _context.Users != null ?
        //                View(await _context.Users.ToListAsync()) :
        //                Problem("Entity set 'AppDbContext.Users'  is null.");
        //}
        [HttpGet]
        public IActionResult Index(int id)
        {
            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name) || id!=7)
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }




            var users = _context.Users.ToList();
            List<UsersModelView> usreslist = new List<UsersModelView>();
            if (users != null)
            {
                foreach (var user in users)
                {
                    // Retrieve all the Defendant_Email values from Complaints
                    var defendantEmails = _context.Complaints.Select(c => c.Defendant_Email).ToList();

                    // Pass the Defendant_Email values to the view using ViewData
                    ViewData["DefendantEmails"] = defendantEmails;
                    var userModelView = new UsersModelView()
                    {
                        Id = user.U_Id,
                        First_Name = user.First_Name,
                        Last_Name = user.Last_Name,
                        National_ID = user.National_ID,
                        Age = user.Age,
                        Phone = user.Phone,
                        Address = user.Address,
                        Job = user.Jop,
                        Gender = user.Gender,
                        Email = user.Email,
                        Password = user.Password
                    };
                    usreslist.Add(userModelView); // Add the userModelView object to the list
                }
            }
            return View(usreslist); // Return the view after populating the list
        }


        // GET: Users/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name))
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == HttpContext.Session.GetString("Email") );
            List<Animals> courses = _context.Animals.Where(m => m.User_Id == user.U_Id).ToList();


            return View(user);

            //if (id == null || _context.Users == null)
            //{
            //    return NotFound();
            //}

            //var users = await _context.Users
            //    .FirstOrDefaultAsync(m => m.U_Id == id);
            ////List<Animals> courses = _context.Animals.Where(m => m.User_Id == id).ToList();

            //if (users == null)
            //{
            //    return NotFound();
            //}
            
            //return View(users);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("U_Id,First_Name,Last_Name,National_ID,Age,Phone,Address,Jop,Gender,Email,Password")] Users users)
        {

            var check = _context.Users.FirstOrDefault(s => s.Email == users.Email);
            if (check == null)
            {

                string input = users.Password;
                if (!string.IsNullOrEmpty(input))
                {
                    users.Password = pass_hash.Hashpassword(input);

                }
                _context.Add(users);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Users");
            }

            return View(users);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users us, string returnUrl)
        {
            var obj = _context.Users.Where(a => a.Email.Equals(us.Email) && a.Password.Equals(pass_hash.Hashpassword(us.Password))).FirstOrDefault();
            if (obj != null)
            {
                if (us.Email.ToLower() == "admin2@gmail.com")
                {
                    var sessionId = HttpContext.Session.Id;
                    HttpContext.Session.SetString("Email", us.Email);
                    Response.Cookies.Append("SessionId", sessionId, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddMinutes(30)
                    });
                    return RedirectToAction("Index", "Admin", new { id = obj.U_Id });
                }
                else
                {
                    //var sessionId = HttpContext.Session.Id;
                    //HttpContext.Session.SetString("Email", us.Email);
                    var sessionId = HttpContext.Session.Id;
                    HttpContext.Session.SetString("Email", us.Email);
                    Response.Cookies.Append("SessionId", sessionId, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddMinutes(30)
                    });
                    return RedirectToAction("Details", "Users", new { id = obj.U_Id });
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Email or PassWord Is Not Correct";
                return View();
            }
            return View();
        }
        public ActionResult Logout()
        {
            Response.Cookies.Delete("SessionId");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Edit/5
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
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == HttpContext.Session.GetString("Email"));
            return View(user);
            //if (id == null || _context.Users == null)
            //{
            //    return NotFound();
            //}

            //var users = await _context.Users.FindAsync(id);

            //if (users == null)
            //{
            //    return NotFound();
            //}
            //return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("U_Id,First_Name,Last_Name,National_ID,Age,Phone,Address,Jop,Gender,Email,Password")] Users users)
        {
            if (id != users.U_Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            string input = users.Password;
            if (!string.IsNullOrEmpty(input))
            {
                users.Password = pass_hash.Hashpassword(input);

            }
            try
            {
                _context.Update(users);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(users.U_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "Users");
            // return RedirectToAction(nameof(Index));
            //}
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.U_Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Users'  is null.");
        //    }
        //    var users = await _context.Users.FindAsync(id);
        //    if (users != null)
        //    {
        //        _context.Users.Remove(users);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    var userToDelete = _context.Users.Find(id);
        //    //if (userToDelete != null)
        //    {
        //        _context.Users.Remove(userToDelete);
        //        _context.SaveChanges();

        //        // Optionally, you can add a success message here
        //    }
        //    // Redirect to the index action after deletion
        //    return RedirectToAction(nameof(Index));
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var userToDelete = _context.Users.Find(id);
            if (userToDelete != null)
            {
                // Delete all animals associated with the user
                var animalsToDelete = _context.Animals.Where(a => a.User_Id == id).ToList();
                _context.Animals.RemoveRange(animalsToDelete);

                // Delete all complaints associated with the user
                var complaintsToDelete = _context.Complaints.Where(c => c.User_Id == id).ToList();
                _context.Complaints.RemoveRange(complaintsToDelete);

                // Delete the user
                _context.Users.Remove(userToDelete);

                _context.SaveChanges();
                // Optionally, you can add a success message here
            }
            return RedirectToAction(nameof(Index));
        }


        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.U_Id == id)).GetValueOrDefault();
        }
    }
}
