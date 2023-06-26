using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Method to get an User by its Guid id
        /// </summary>
        /// <param name="id">Guid Id of the user</param>
        /// <returns></returns>
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Method to get an User by its Guid id when using multiple instances of the same object
        /// </summary>
        /// <param name="id">Guid Id of the user</param>
        /// <returns></returns>
        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        /// <summary>
        /// Metod to update an user
        /// </summary>
        /// <param name="user">User type object with data</param>
        /// <returns></returns>
        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        /// <summary>
        /// Method to save an operation in db
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
