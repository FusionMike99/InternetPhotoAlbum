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
        private readonly InternetPhotoAlbumDbContext context;

        public ImagesRepository(InternetPhotoAlbumDbContext context)
        {
            this.context = context;
        }

        public Image Create(Image item)
        {
            var result = context.Images.Add(item);
            return result;
        }

        public async Task<Image> FindByIdAsync(int id)
        {
            var result = await context.Images.FindAsync(id);
            return result;
        }

        public IEnumerable<Image> Get()
        {
            var result = context.Images
                .AsNoTracking()
                .AsEnumerable();
            return result;
        }

        public IEnumerable<Image> Get(Func<Image, bool> predicate)
        {
            var result = context.Images
                .AsNoTracking()
                .Where(predicate)
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
    }
}
