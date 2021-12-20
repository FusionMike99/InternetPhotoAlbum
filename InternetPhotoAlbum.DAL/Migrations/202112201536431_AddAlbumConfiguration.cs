namespace InternetPhotoAlbum.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddAlbumConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "PeriodStart", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Albums", "PeriodEnd", c => c.DateTime(storeType: "date"));
        }

        public override void Down()
        {
            AlterColumn("dbo.Albums", "PeriodEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Albums", "PeriodStart", c => c.DateTime(nullable: false));
        }
    }
}
