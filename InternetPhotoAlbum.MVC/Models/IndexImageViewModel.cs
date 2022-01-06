namespace InternetPhotoAlbum.MVC.Models
{
    /// <summary>
    /// Model for Index, Details, Delete Image Views
    /// </summary>
    public class IndexImageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public int FinalRating { get; set; }

        public int AlbumId { get; set; }
        public string UserId { get; set; }
    }
}