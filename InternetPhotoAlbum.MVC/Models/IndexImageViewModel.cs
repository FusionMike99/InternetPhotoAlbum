using System;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.MVC.Models
{
    /// <summary>
    /// Model for Index, Details, Delete Image Views
    /// </summary>
    public class IndexImageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Added at")]
        public DateTime AddedDate { get; set; }
        public string File { get; set; }
        public string ContentType { get; set; }
        public int FinalRating { get; set; }

        public int AlbumId { get; set; }
    }
}