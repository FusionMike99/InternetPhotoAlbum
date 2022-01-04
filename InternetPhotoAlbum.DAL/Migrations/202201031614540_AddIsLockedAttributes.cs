namespace InternetPhotoAlbum.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddIsLockedAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "IsLocked", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Images", "IsLocked", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.UserProfiles", "IsLocked", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "IsLocked");
            DropColumn("dbo.Images", "IsLocked");
            DropColumn("dbo.Albums", "IsLocked");
        }
    }
}
