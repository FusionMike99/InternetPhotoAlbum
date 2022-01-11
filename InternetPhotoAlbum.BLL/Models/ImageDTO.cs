using System;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Models
{
    /// <summary>
    /// Data transform object for Image
    /// </summary>
    public class ImageDTO
    {
        public int Id { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public byte[] File { get; set; }
        public string ContentType { get; set; }
        [Required]
        public int AlbumId { get; set; }
    }
}
