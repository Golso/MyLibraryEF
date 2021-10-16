namespace MyLibraryEF.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddModelsToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblBooks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Author = c.String(nullable: false),
                    ToBuy = c.String(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.tblUsers",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    Login = c.String(nullable: false),
                    Password = c.String(nullable: false),
                    State = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.tblBorrowedBooks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Author = c.String(nullable: false),
                    ToWhom = c.String(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.tblBorrowedBooks", "UserId", "dbo.tblUsers");
            DropForeignKey("dbo.tblBooks", "UserId", "dbo.tblUsers");
            DropIndex("dbo.tblBorrowedBooks", new[] { "UserId" });
            DropIndex("dbo.tblBooks", new[] { "UserId" });
            DropTable("dbo.tblBorrowedBooks");
            DropTable("dbo.tblUsers");
            DropTable("dbo.tblBooks");
        }
    }
}
