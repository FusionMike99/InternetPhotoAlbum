using System;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.BLL.Models
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        [Required, StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText), StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime PeriodStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime PeriodEnd { get; set; }
        public string UserId { get; set; }
    }
}
