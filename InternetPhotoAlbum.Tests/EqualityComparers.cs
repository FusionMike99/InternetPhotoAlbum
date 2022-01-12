using InternetPhotoAlbum.BLL.Models;
using System.Collections.Generic;

namespace InternetPhotoAlbum.Tests
{
    internal class AlbumDTOEqualityComparer : IEqualityComparer<AlbumDTO>
    {
        public bool Equals(AlbumDTO x, AlbumDTO y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Title == y.Title
                && x.Description == y.Description
                && x.PeriodStart == y.PeriodStart
                && x.PeriodEnd == y.PeriodEnd
                && x.UserId == y.UserId
                && x.UserName == y.UserName;
        }

        public int GetHashCode(AlbumDTO obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ImageDTOEqualityComparer : IEqualityComparer<ImageDTO>
    {
        public bool Equals(ImageDTO x, ImageDTO y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Title == y.Title
                && x.Description == y.Description
                && x.AddedDate == y.AddedDate
                && x.AlbumId == y.AlbumId;
        }

        public int GetHashCode(ImageDTO obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class GenderDTOEqualityComparer : IEqualityComparer<GenderDTO>
    {
        public bool Equals(GenderDTO x, GenderDTO y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name;
        }

        public int GetHashCode(GenderDTO obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class LikeTypeDTOEqualityComparer : IEqualityComparer<LikeTypeDTO>
    {
        public bool Equals(LikeTypeDTO x, LikeTypeDTO y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name;
        }

        public int GetHashCode(LikeTypeDTO obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class RatingDTOEqualityComparer : IEqualityComparer<RatingDTO>
    {
        public bool Equals(RatingDTO x, RatingDTO y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.ImageId == y.ImageId
                && x.UserId == y.UserId
                && x.LikeTypeId == y.LikeTypeId;
        }

        public int GetHashCode(RatingDTO obj)
        {
            return obj.GetHashCode();
        }
    }
}
