using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.MVC.Models
{
    /// <summary>
    /// Model for Changing Password View
    /// </summary>
    public class ChangePasswordViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Old password")]
        public string OldPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "New password")]
        public string NewPassword { get; set; }
    }
}