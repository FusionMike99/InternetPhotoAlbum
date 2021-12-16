using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
