using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Models
{
    public class RatingDTO
    {
        public int ImageId { get; set; }
        public string UserId { get; set; }
        [Required]
        public int LikeTypeId { get; set; }
    }
}
