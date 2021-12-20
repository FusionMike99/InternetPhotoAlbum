namespace InternetPhotoAlbum.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGendersandLikeTypestables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Ratings", "LikeTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.UserProfiles", "GenderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "LikeTypeId");
            CreateIndex("dbo.UserProfiles", "GenderId");
            AddForeignKey("dbo.Ratings", "LikeTypeId", "dbo.LikeTypes", "Id");
            AddForeignKey("dbo.UserProfiles", "GenderId", "dbo.Genders", "Id");
            DropColumn("dbo.Ratings", "LikeType");
            DropColumn("dbo.UserProfiles", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Gender", c => c.Byte(nullable: false));
            AddColumn("dbo.Ratings", "LikeType", c => c.Byte(nullable: false));
            DropForeignKey("dbo.UserProfiles", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Ratings", "LikeTypeId", "dbo.LikeTypes");
            DropIndex("dbo.UserProfiles", new[] { "GenderId" });
            DropIndex("dbo.Ratings", new[] { "LikeTypeId" });
            DropColumn("dbo.UserProfiles", "GenderId");
            DropColumn("dbo.Ratings", "LikeTypeId");
            DropTable("dbo.Genders");
            DropTable("dbo.LikeTypes");
        }
    }
}
