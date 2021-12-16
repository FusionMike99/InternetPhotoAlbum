﻿namespace InternetPhotoAlbum.DAL.Entities
{
    public class Rating
    {
        public int ImageId { get; set; }
        public string UserId { get; set; }
        public LikeType LikeType { get; set; }

        public virtual Image Image { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
