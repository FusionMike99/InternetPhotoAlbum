using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IProceduresRepository : IDisposable
    {
        void LockUser(string userId, bool isLocked);
    }
}
