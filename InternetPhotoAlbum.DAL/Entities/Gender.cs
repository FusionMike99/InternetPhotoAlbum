using System.Collections.Generic;

namespace InternetPhotoAlbum.DAL.Entities
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserProfile> Users { get; set; }
    }
}
