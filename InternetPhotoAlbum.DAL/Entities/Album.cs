using System;
using System.Collections.Generic;

namespace InternetPhotoAlbum.DAL.Entities
{
    /// <summary>
    /// Code First class for table Albums
    /// </summary>
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public bool IsLocked { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
