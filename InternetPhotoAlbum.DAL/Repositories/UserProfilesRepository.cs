using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    public class UserProfilesRepository : IUserProfilesRepository
    {
        private readonly ApplicationContext context;

        public UserProfilesRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public UserProfile Create(UserProfile profile)
        {
            var result = context.UserProfiles.Add(profile);
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerable<UserProfile> GetAll()
        {
            var result = context.UserProfiles
                .AsNoTracking()
                .AsEnumerable();
            return result;
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            var result = await context.UserProfiles.FindAsync(id);
            return result;
        }

        public void Remove(UserProfile profile)
        {
            context.UserProfiles.Remove(profile);
        }

        public void Update(UserProfile profile)
        {
            context.Entry(profile).State = EntityState.Modified;
        }
    }
}
