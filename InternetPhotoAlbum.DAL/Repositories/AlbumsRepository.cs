using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    public class AlbumsRepository : IAlbumsRepository
    {
        private readonly InternetPhotoAlbumDbContext context;

        public AlbumsRepository(InternetPhotoAlbumDbContext context)
        {
            this.context = context;
        }

        public Album Create(Album item)
        {
            var result = context.Albums.Add(item);
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<Album> FindByIdAsync(int id)
        {
            var result = await context.Albums.FindAsync(id);
            return result;
        }

        public IEnumerable<Album> Get()
        {
            var result = context.Albums
                .AsNoTracking()
                .AsEnumerable();
            return result;
        }

        public IEnumerable<Album> Get(Func<Album, bool> predicate)
        {
            var result = context.Albums
                .AsNoTracking()
                .Where(predicate)
                .AsEnumerable();
            return result;
        }

        public void Remove(Album item)
        {
            context.Albums.Remove(item);
        }

        public void Update(Album item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
