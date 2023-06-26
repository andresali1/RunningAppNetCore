using RunningAppNetCore.Data;
using RunningAppNetCore.Interfaces;
using RunningAppNetCore.Models;

namespace RunningAppNetCore.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Method to get all clubs that belong to an user
        /// </summary>
        /// <returns></returns>
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id == curUserId);
            return userClubs.ToList();
        }

        /// <summary>
        /// Method to get all races that belong to an user
        /// </summary>
        /// <returns></returns>
        public async Task<List<Race>> GetAllUserRaces()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(r => r.AppUser.Id == curUserId);
            return userRaces.ToList();
        }
    }
}
