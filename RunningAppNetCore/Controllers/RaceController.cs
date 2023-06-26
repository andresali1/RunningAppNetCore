using Microsoft.AspNetCore.Mvc;
using RunningAppNetCore.Interfaces;
using RunningAppNetCore.Models;
using RunningAppNetCore.Repository;
using RunningAppNetCore.ViewModels;

namespace RunningAppNetCore.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        //Get: Index
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }

        //Get: Detail
        public async Task<IActionResult> Detail(int id)
        {
            Race? race = await _raceRepository.GetByIdAsync(id);

            if(race == null) return View("Error");

            return View(race);
        }

        //Get: Create
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createRaceViewModel = new CreateRaceViewModel { AppUserId = curUserId ?? "" };
            return View(createRaceViewModel);
        }

        /// <summary>
        /// Method to Create a new Race
        /// </summary>
        /// <param name="raceVM">View Model with post data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = raceVM.AppUserId,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        Department = raceVM.Address.Department
                    },
                    RaceCategory = raceVM.RaceCategory
                };

                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(raceVM);
        }

        //Get: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return View("Error");

            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                Url = race.Image,
                RaceCategory = race.RaceCategory
            };

            return View(raceVM);
        }

        /// <summary>
        /// Method to Update a Race
        /// </summary>
        /// <param name="id">Id of the race</param>
        /// <param name="raceVM">Model with the data to edit</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race");
                return View("Edit", raceVM);
            }

            var userRace = await _raceRepository.GetByIdAsyncNoTracking(id);

            if (userRace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(raceVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address
                };

                _raceRepository.Update(race);

                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }
        }

        //Get: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var raceDetails = await _raceRepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");
            return View(raceDetails);
        }

        /// <summary>
        /// Method to delete a race from db
        /// </summary>
        /// <param name="id">Id of the race</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var raceDetails = await _raceRepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");

            try
            {
                await _photoService.DeletePhotoAsync(raceDetails.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(raceDetails);
            }

            _raceRepository.Delete(raceDetails);
            return RedirectToAction("Index");
        }
    }
}
