using Animal_Rental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal_Rental.Controllers
{
    public class contactController : Controller
    {
        AppDbContext _context=new AppDbContext();
        public IActionResult contact()
        {

            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name))
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [HttpPost]
        public IActionResult addcomplaint( Complaints comp  )
        {


            if (comp.Defendant_Email!=null&&comp.Notes!=null)
            {
                //var userEmail = HttpContext.Session.GetString("Email");
                var valid = _context.Users.FirstOrDefault(u => u.Email == comp.Defendant_Email);
                if(valid != null) {
                    comp.User_Id = valid.U_Id;
                    _context.Complaints.Add(comp);
                    _context.SaveChanges();
                    return RedirectToAction("Index","Home");
                }

                else if(valid==null)
                {
                    //ViewBag.ErrorMessage = "This Email is not exist!";
                    ModelState.AddModelError("Defendant_Email", "Email not exists ");
                }

            }
        
            return View("contact");
        }
    }
}
