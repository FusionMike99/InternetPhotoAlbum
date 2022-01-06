using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    /// <inheritdoc cref="IAlbumsRepository"/>
    public class AlbumsRepository : IAlbumsRepository
    {
        private readonly ApplicationContext context;

        /// <summary>
        /// Inject context
        /// </summary>
        /// <param name="context">Database's context</param>
        public AlbumsRepository(ApplicationContext context)
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

        public async Task<Album> GetByIdAsync(int id)
        {
            var result = await context.Albums
                .Where(i => !i.IsLocked)
                .SingleOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public IEnumerable<Album> GetAll()
        {
            var result = context.Albums
                .AsNoTracking()
                .Where(i => !i.IsLocked)
                .Include(a => a.User)
                .ToList();
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

        public IEnumerable<Album> Get(Func<Album, bool> predicate)
        {
            var result = context.Albums
                .AsNoTracking()
                .Where(i => !i.IsLocked)
                .Where(predicate)
                .ToList();
            return result;
        }
    }
}
