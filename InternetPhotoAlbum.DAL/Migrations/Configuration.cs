namespace InternetPhotoAlbum.DAL.Migrations
{
    using InternetPhotoAlbum.DAL.Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationContext context)
        {
            var gender1 = new Gender { Id = 1, Name = "Male" };
            var gender2 = new Gender { Id = 2, Name = "Female" };

            var likeType1 = new LikeType { Id = 1, Name = "Like" };
            var likeType2 = new LikeType { Id = 2, Name = "Don't like" };

            context.Genders.AddOrUpdate(x => x.Id, gender1, gender2);
            context.LikeTypes.AddOrUpdate(x => x.Id, likeType1, likeType2);
        }
    }
}
