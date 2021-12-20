using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetPhotoAlbum.DAL.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public byte[] File { get; set; }
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
