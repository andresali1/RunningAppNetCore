using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunningAppNetCore.Helpers;
using RunningAppNetCore.Interfaces;
using RunningAppNetCore.Models;
using RunningAppNetCore.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RunningAppNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository, IConfiguration configuration)
        {
            _logger = logger;
            _clubRepository = clubRepository;
            _configuration = configuration;
        }

        //Get: Index
        public async Task<IActionResult> Index()
        {
            string ipInfoToken = (_configuration.GetSection("IpInfo").Get<IpInfoConfig>()).Token;
            var ipInfo = new IpInfo();
            var homeViewModel = new HomeViewModel();

            try
            {
                string url = "https://ipinfo.io?token=" + ipInfoToken;
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.Department = ipInfo.Region;

                if(homeViewModel.City != null)
                {
                    homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
                }
                else
                {
                    homeViewModel.Clubs = null;
                }

                return View(homeViewModel);
            }
            catch (Exception ex)
            {
                homeViewModel.Clubs = null;
            }

            return View(homeViewModel);
        }

        //Get: Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}