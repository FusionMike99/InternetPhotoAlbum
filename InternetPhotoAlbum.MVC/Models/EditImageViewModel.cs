using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Models
{
    public class EditImageViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }
        [Required, HiddenInput]
        public int AlbumId { get; set; }
    }
}