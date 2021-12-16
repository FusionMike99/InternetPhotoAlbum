using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText), MaxLength(200)]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AddedDate { get; set; }
        [Required]
        public byte[] File { get; set; }
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
