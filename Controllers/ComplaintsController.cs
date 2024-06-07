using Animal_Rental.Models;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Rental.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly AppDbContext _context;

        public ComplaintsController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {

            Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            var name = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(name) || id != 7)
            {

                var returnUrl = Request.Path.Value;
                return RedirectToAction("Login", "Users", new { returnUrl });
            }


            var complaints = _context.Complaints.ToList();
            List<Complaints> Complaintslist = new List<Complaints>();
            if (complaints != null)
            {
                foreach (var complain in complaints )
                {
                    var ComplaintsModelView = new Complaints()
                    {
                        Id=complain.Id,
                        User_Id = complain.User_Id,
                        Defendant_Email = complain.Defendant_Email,
                        Notes = complain.Notes,                 
                    };
                    Complaintslist.Add(ComplaintsModelView);
                }
            }
            return View(Complaintslist); 
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var complaintToDelete = _context.Complaints.Find(id);
            if (complaintToDelete != null)
            {
                _context.Complaints.Remove(complaintToDelete);
                _context.SaveChanges();
                // Optionally, you can add a success message here
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
