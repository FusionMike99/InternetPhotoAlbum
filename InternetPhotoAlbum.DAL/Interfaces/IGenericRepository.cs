using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity item);
        Task<TEntity> FindByIdAsync(int id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Update(TEntity item);
        void Remove(TEntity item);
    }
}
