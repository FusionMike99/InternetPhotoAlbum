using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly ApplicationContext context;

        public ImagesRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Image Create(Image item)
        {
            var result = context.Images.Add(item);
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            var result = await context.Images
                .Where(i => !i.IsLocked)
                .SingleOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public IEnumerable<Image> GetAll()
        {
            var result = context.Images
                .AsNoTracking()
                .Where(i => !i.IsLocked)
                .AsEnumerable();
            return result;
        }

        public void Remove(Image item)
        {
            context.Images.Remove(item);
        }

        public void Update(Image item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Image> Get(Func<Image, bool> predicate)
        {
            var result = context.Images
                .AsNoTracking()
                .Where(i => !i.IsLocked)
                .Where(predicate)
                .AsEnumerable();
            return result;
        }
    }
}
