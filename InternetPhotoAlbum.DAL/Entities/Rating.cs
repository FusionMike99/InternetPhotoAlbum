namespace InternetPhotoAlbum.DAL.Entities
{
    /// <summary>
    /// Code First class for table Ratings
    /// </summary>
    public class Rating
    {
        public int ImageId { get; set; }
        public string UserId { get; set; }
        public int LikeTypeId { get; set; }

        public virtual LikeType LikeType { get; set; }
        public virtual Image Image { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
