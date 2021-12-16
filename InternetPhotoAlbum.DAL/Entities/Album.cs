using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.DAL.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText), MaxLength(200)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime PeriodStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime PeriodEnd { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
