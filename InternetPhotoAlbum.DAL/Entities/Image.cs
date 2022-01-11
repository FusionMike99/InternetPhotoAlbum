using System;
using System.Collections.Generic;

namespace InternetPhotoAlbum.DAL.Entities
{
    /// <summary>
    /// Code First class for table Images
    /// </summary>
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public byte[] File { get; set; }
        public string ContentType { get; set; }
        public bool IsLocked { get; set; }
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
