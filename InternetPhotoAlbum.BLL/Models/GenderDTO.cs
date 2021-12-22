using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Models
{
    public class GenderDTO
    {
        public int Id { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
