using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
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
            var result = await context.Ratings.FindAsync(imageId, userId);
            return result;
        }

        public IEnumerable<Rating> GetAll()
        {
            var result = context.Ratings
                .AsNoTracking()
                .AsEnumerable();
            return result;
        }

        public void Remove(Rating item)
        {
            context.Ratings.Remove(item);
        }

        public void Update(Rating item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
