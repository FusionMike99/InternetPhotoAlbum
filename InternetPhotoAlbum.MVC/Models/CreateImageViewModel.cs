using InternetPhotoAlbum.MVC.Utils;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace InternetPhotoAlbum.MVC.Models
{
    /// <summary>
    /// Model for Creating Image View
    /// </summary>
    public class CreateImageViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        [StringLength(200, MinimumLength = 3), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required, AllowExtensions(Extensions = "png,jpg", ErrorMessage = "Please select only Supported Files .png | .jpg")]
        [Display(Name = "Supported Files .png | .jpg")]
        public HttpPostedFileBase File { get; set; }
        [Required, HiddenInput]
        public int AlbumId { get; set; }
    }
}