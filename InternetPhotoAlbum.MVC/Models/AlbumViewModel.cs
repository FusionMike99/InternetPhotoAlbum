using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.MVC.Models
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText), StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }
    }
}