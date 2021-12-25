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
        [DataType(DataType.Date), Display(Name = "Period start")]
        public DateTime PeriodStart { get; set; }
        [DataType(DataType.Date), Display(Name = "Period end")]
        public DateTime PeriodEnd { get; set; }
        [Required]
        public string UserId { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}
