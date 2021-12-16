using InternetPhotoAlbum.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetPhotoAlbum.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
}
