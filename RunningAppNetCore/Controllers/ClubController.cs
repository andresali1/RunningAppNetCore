using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningAppNetCore.Data;
using RunningAppNetCore.Models;

namespace RunningAppNetCore.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Club> clubs = _context.Clubs.ToList();
            return View(clubs);
        }

        public IActionResult Detail(int id)
        {
            Club? club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id);

            if (club == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(club);
        }
    }
}
