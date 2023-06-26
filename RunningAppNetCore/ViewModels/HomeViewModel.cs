using RunningAppNetCore.Models;

namespace RunningAppNetCore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Club> Clubs { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
    }
}
