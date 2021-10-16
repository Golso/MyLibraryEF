namespace MyLibraryEF.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addBorrowedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblBorrowedBooks", "BorrowedTime", c => c.DateTime());
        }

        public override void Down()
        {
            DropColumn("dbo.tblBorrowedBooks", "BorrowedTime");
        }
    }
}
