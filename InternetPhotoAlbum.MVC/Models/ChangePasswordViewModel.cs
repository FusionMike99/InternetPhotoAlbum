using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.MVC.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required, DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}