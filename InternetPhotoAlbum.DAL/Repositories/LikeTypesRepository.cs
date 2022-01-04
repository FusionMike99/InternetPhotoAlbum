using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    public class LikeTypesRepository : ILikeTypesRepository
    {
        private readonly ApplicationContext context;

        public LikeTypesRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public LikeType Create(LikeType type)
        {
            var result = context.LikeTypes.Add(type);
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerable<LikeType> GetAll()
        {
            var result = context.LikeTypes
                .AsNoTracking()
                .ToList();
            return result;
        }

        public async Task<LikeType> GetByIdAsync(int id)
        {
            var result = await context.LikeTypes.FindAsync(id);
            return result;
        }

        public void Remove(LikeType type)
        {
            context.LikeTypes.Remove(type);
        }

        public void Update(LikeType type)
        {
            context.Entry(type).State = EntityState.Modified;
        }
    }
}
