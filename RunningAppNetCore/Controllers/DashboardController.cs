using Microsoft.AspNetCore.Mvc;
using RunningAppNetCore.Data;
using RunningAppNetCore.Interfaces;
using RunningAppNetCore.ViewModels;

namespace RunningAppNetCore.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        //Get: Index
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserClubs();

            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };

            return View(dashboardViewModel);
        }
    }
}
