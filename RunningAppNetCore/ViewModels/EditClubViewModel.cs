﻿using RunningAppNetCore.Data.Enum;
using RunningAppNetCore.Models;

namespace RunningAppNetCore.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string? Url { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
