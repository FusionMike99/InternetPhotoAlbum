namespace InternetPhotoAlbum.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLockProcedure : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE PROCEDURE LockUser
                    @userId NVARCHAR(128),
                    @isLocked BIT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE dbo.[UserProfiles] SET IsLocked = @isLocked WHERE UserId = @userId;
                    UPDATE dbo.[Albums] SET IsLocked = @isLocked WHERE UserId = @userId;
                    UPDATE dbo.[Images] SET IsLocked = @isLocked WHERE AlbumId IN 
                        (SELECT Id FROM dbo.[Albums] WHERE UserId = @userId);
                END;");
        }

        public override void Down()
        {
            Sql("DROP PROCEDURE LockUser");
        }
    }
}
