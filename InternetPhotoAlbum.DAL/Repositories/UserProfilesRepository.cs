﻿using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    /// <inheritdoc cref="IUserProfilesRepository"/>
    public class UserProfilesRepository : IUserProfilesRepository
    {
        private readonly ApplicationContext context;

        /// <summary>
        /// Inject context
        /// </summary>
        /// <param name="context">Database's context</param>
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
                .Where(i => !i.IsLocked)
                .ToList();
            return result;
        }

        public async Task<UserProfile> GetByIdAsync(string id)
        {
            var result = await context.UserProfiles
                .Where(i => !i.IsLocked)
                .Include(up => up.Gender)
                .SingleOrDefaultAsync(up => up.UserId == id);
            return result;
        }

        public void Remove(UserProfile profile)
        {
            context.UserProfiles.Remove(profile);
        }

        public void Update(UserProfile profile)
        {
            context.Entry(profile).State = EntityState.Modified;
            context.Entry(profile).Property(up => up.IsLocked).IsModified = false;
        }
    }
}
