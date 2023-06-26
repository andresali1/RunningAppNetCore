﻿namespace RunningAppNetCore.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? Department { get; set; }
        public IFormFile Image { get; set; }
    }
}
