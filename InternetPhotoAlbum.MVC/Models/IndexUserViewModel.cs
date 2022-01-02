﻿using System;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.MVC.Models
{
    public class IndexUserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}