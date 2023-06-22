using Microsoft.EntityFrameworkCore;
using RunningAppNetCore.Data;
using RunningAppNetCore.Interfaces;
using RunningAppNetCore.Models;

namespace RunningAppNetCore.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add a new Club
        /// </summary>
        /// <param name="club">Club type object with data</param>
        /// <returns></returns>
        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        /// <summary>
        /// Method to delete a new Club
        /// </summary>
        /// <param name="club">Club type object with data</param>
        /// <returns></returns>
        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        /// <summary>
        /// Method to Get all Clubs
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        /// <summary>
        /// Method to Get a Club by Id
        /// </summary>
        /// <param name="id">Id of the Club</param>
        /// <returns></returns>
        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(a => a.Address).FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Method to Get a Club By Id when there will be more than one instance of the interface instantiated
        /// </summary>
        /// <param name="id">Id of the Club</param>
        /// <returns></returns>
        public async Task<Club?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Method to Get a Club by City
        /// </summary>
        /// <param name="city">City to Search</param>
        /// <returns></returns>
        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        /// <summary>
        /// Method to send the action to bd
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        /// <summary>
        /// Method to edit a new Club
        /// </summary>
        /// <param name="club">Club type object with data</param>
        /// <returns></returns>
        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
