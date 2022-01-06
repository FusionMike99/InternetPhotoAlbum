using InternetPhotoAlbum.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetPhotoAlbum.DAL.Identity
{
    /// <summary>
    /// User's role manager
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        /// <summary>
        /// Injection for User's Role Store
        /// </summary>
        /// <param name="store">User's Role Store</param>
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
}
