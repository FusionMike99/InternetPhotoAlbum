using InternetPhotoAlbum.DAL.Entities;
using InternetPhotoAlbum.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Repositories
{
    /// <inheritdoc cref="IGendersRepository"/>
    public class GendersRepository : IGendersRepository
    {
        private readonly ApplicationContext context;

        /// <summary>
        /// Inject context
        /// </summary>
        /// <param name="context">Database's context</param>
        public GendersRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Gender Create(Gender gender)
        {
            var result = context.Genders.Add(gender);
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerable<Gender> GetAll()
        {
            var result = context.Genders
                .AsNoTracking()
                .ToList();
            return result;
        }

        public async Task<Gender> GetByIdAsync(int id)
        {
            var result = await context.Genders.FindAsync(id);
            return result;
        }

        public void Remove(Gender gender)
        {
            context.Genders.Remove(gender);
        }

        public void Update(Gender gender)
        {
            context.Entry(gender).State = EntityState.Modified;
        }
    }
}
