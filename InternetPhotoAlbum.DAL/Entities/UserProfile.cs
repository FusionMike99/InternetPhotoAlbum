using System;

namespace InternetPhotoAlbum.DAL.Entities
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int GenderId { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
