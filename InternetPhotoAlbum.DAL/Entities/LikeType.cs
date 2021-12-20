﻿using System.Collections.Generic;

namespace InternetPhotoAlbum.DAL.Entities
{
    public class LikeType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
