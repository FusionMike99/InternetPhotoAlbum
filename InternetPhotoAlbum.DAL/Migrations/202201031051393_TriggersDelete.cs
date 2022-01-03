namespace InternetPhotoAlbum.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TriggersDelete : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE OR ALTER TRIGGER [DELETE_Users]
                     ON dbo.[AspNetUsers]
                     INSTEAD OF DELETE
                  AS 
                  BEGIN
                     SET NOCOUNT ON;
                     DELETE FROM dbo.[UserProfiles] WHERE UserId IN (SELECT Id FROM DELETED)
                     DELETE FROM dbo.[Ratings] WHERE UserId IN (SELECT Id FROM DELETED)
                     DELETE FROM dbo.[Albums] WHERE UserId IN (SELECT Id FROM DELETED)
                     DELETE FROM dbo.[AspNetUsers] WHERE Id IN (SELECT Id FROM DELETED)
                  END");

            Sql(@"CREATE OR ALTER TRIGGER [DELETE_Albums]
                     ON dbo.[Albums]
                     INSTEAD OF DELETE
                  AS 
                  BEGIN
                     SET NOCOUNT ON;
                     DELETE FROM dbo.[Images] WHERE AlbumId IN (SELECT Id FROM DELETED)
                     DELETE FROM dbo.[Albums] WHERE Id IN (SELECT Id FROM DELETED)
                  END");

            Sql(@"CREATE OR ALTER TRIGGER [DELETE_Images]
                     ON dbo.[Images]
                     INSTEAD OF DELETE
                  AS 
                  BEGIN
                     SET NOCOUNT ON;
                     DELETE FROM dbo.[Ratings] WHERE ImageId IN (SELECT Id FROM DELETED)
                     DELETE FROM dbo.[Images] WHERE Id IN (SELECT Id FROM DELETED)
                  END");
        }

        public override void Down()
        {
            Sql("DROP TRIGGER [DELETE_Images]");
            Sql("DROP TRIGGER [DELETE_Albums]");
            Sql("DROP TRIGGER [DELETE_Users]");
        }
    }
}
