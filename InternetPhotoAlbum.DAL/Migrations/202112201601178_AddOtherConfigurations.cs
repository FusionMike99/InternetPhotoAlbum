namespace InternetPhotoAlbum.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherConfigurations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LikeTypes", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.UserProfiles", "DateOfBirth", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Genders", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Genders", "Name", c => c.String());
            AlterColumn("dbo.UserProfiles", "DateOfBirth", c => c.DateTime());
            AlterColumn("dbo.LikeTypes", "Name", c => c.String());
        }
    }
}
