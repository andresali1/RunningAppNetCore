using Microsoft.EntityFrameworkCore;
using RunningAppNetCore.Data;
using RunningAppNetCore.Interfaces;
using RunningAppNetCore.Models;

namespace RunningAppNetCore.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add a new Race
        /// </summary>
        /// <param name="race">Race type object with data</param>
        /// <returns></returns>
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        /// <summary>
        /// Method to delete a Race
        /// </summary>
        /// <param name="race">Race type object with data</param>
        /// <returns></returns>
        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        /// <summary>
        /// Method to Get all the Races
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        /// <summary>
        /// Method to Get a Race by Id
        /// </summary>
        /// <param name="id">Id of the Race</param>
        /// <returns></returns>
        public async Task<Race?> GetByIdAsync(int id)
        {
            return await _context.Races.Include(a => a.Address).FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Method to Get a Race By Id when there will be more than one instance of the interface instantiated
        /// </summary>
        /// <param name="id">Id of the Race</param>
        /// <returns></returns>
        public async Task<Race?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Method to Get a Race by City
        /// </summary>
        /// <param name="city">City to search</param>
        /// <returns></returns>
        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
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
        /// Method to edit a Race
        /// </summary>
        /// <param name="race">Race type object with data</param>
        /// <returns></returns>
        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
