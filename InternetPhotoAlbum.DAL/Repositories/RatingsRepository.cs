using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    public class RatingsRepository : IRatingsRepository
    {
        private readonly ApplicationContext context;

        public RatingsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Rating Create(Rating item)
        {
            var result = context.Ratings.Add(item);
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<Rating> GetByIdAsync(int imageId, string userId)
        {
            var result = await context.Ratings
                .AsNoTracking()
                .SingleOrDefaultAsync(r =>  r.ImageId == imageId && r.UserId == userId);
            return result;
        }

        public IEnumerable<Rating> GetAll()
        {
            var result = context.Ratings
                .AsNoTracking()
                .ToList();
            return result;
        }

        public void Remove(Rating item)
        {
            context.Entry(item).State = EntityState.Deleted;
        }

        public void Update(Rating item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Rating> Get(Func<Rating, bool> predicate)
        {
            var result = context.Ratings
                .AsNoTracking()
                .Where(predicate)
                .ToList();
            return result;
        }
    }
}
