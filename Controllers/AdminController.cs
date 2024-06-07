using Microsoft.AspNetCore.Mvc;
using Animal_Rental.Models; // Assuming your repositories are defined here

namespace Animal_Rental.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index(int id)
        {
            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name) )
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }
            if (id == 7)
            {
                return View("Index");
            }
            else
            {
                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }
        }
        
    }
}
