using System;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.MVC.Models
{
    public class RegisterModel
    {
        [Required]
        public string Login { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password"), Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Surname { get; set; }
        [DataType(DataType.Date), Display(Name = "Date of your birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required, Display(Name = "Gender"), Range(1, int.MaxValue, ErrorMessage = "Need to choose gender")]
        public int GenderId { get; set; }
    }
}