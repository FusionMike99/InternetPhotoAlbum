namespace InternetPhotoAlbum.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddToImageContentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ContentType", c => c.String(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Images", "ContentType");
        }
    }
}
