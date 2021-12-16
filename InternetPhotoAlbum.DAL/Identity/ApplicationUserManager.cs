using InternetPhotoAlbum.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace InternetPhotoAlbum.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}
