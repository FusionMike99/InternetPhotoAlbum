using System;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Models
{
    /// <summary>
    /// Model for editing user profile
    /// </summary>
    public class EditUserProfileModel
    {
        public string Id { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Need to choose gender")]
        public int GenderId { get; set; }
    }
}
