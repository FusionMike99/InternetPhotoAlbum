using System;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Models
{
    /// <summary>
    /// Data transform object for ApplicationUser and UserProfile
    /// </summary>
    public class UserDTO
    {
        public string Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public bool IsLocked { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
