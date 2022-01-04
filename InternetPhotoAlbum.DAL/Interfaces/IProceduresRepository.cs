using System;

namespace InternetPhotoAlbum.DAL.Interfaces
{
    public interface IProceduresRepository : IDisposable
    {
        void LockUser(string userId, bool isLocked);
    }
}
