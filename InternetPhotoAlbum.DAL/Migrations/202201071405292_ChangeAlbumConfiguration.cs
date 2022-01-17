namespace InternetPhotoAlbum.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeAlbumConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "PeriodStart", c => c.DateTime(storeType: "date"));
        }

        public override void Down()
        {
            AlterColumn("dbo.Albums", "PeriodStart", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
