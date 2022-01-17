namespace InternetPhotoAlbum.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddProcedureUpdateAlbumPeriods : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE PROCEDURE [dbo].[UpdateAlbumPeriods]
	                @albumId int
                AS
                BEGIN
	                DECLARE @periodStart DATE, @periodEnd DATE;

	                SELECT @periodStart = MIN(AddedDate), @periodEnd = MAX(AddedDate)
		                FROM [dbo].[Images] WHERE AlbumId = @albumId;

	                UPDATE [dbo].[Albums] SET PeriodStart = @periodStart,
		                PeriodEnd = @periodEnd WHERE Id = @albumId;
                END;");
        }

        public override void Down()
        {
            Sql("DROP PROCEDURE [dbo].[UpdateAlbumPeriods]");
        }
    }
}
