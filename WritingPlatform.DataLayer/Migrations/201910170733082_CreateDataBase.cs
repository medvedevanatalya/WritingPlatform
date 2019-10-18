namespace WritingPlatform.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataBase : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.CommentsUsers",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.Int(nullable: false),
            //            WriterWorkId = c.Int(nullable: false),
            //            CommentsText = c.String(nullable: false, maxLength: 500),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Users", t => t.UserId)
            //    .ForeignKey("dbo.WritersWorks", t => t.WriterWorkId)
            //    .Index(t => t.UserId)
            //    .Index(t => t.WriterWorkId);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            LoginUser = c.String(nullable: false, maxLength: 50),
            //            EmailUser = c.String(nullable: false, maxLength: 50),
            //            PasswordUser = c.String(nullable: false, maxLength: 50),
            //            RoleId = c.Int(nullable: false),
            //            IsDelete = c.Boolean(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Roles", t => t.RoleId)
            //    .Index(t => t.RoleId);
            
            //CreateTable(
            //    "dbo.Roles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            NameRole = c.String(nullable: false, maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.WritersWorks",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            TitleWork = c.String(nullable: false, maxLength: 50),
            //            WriterWorkText = c.String(nullable: false),
            //            GenreId = c.Int(nullable: false),
            //            UserId = c.Int(nullable: false),
            //            PublicationDate = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Genres", t => t.GenreId)
            //    .ForeignKey("dbo.Users", t => t.UserId)
            //    .Index(t => t.GenreId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Genres",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            NameGenre = c.String(nullable: false, maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WritersWorks", "UserId", "dbo.Users");
            DropForeignKey("dbo.WritersWorks", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.CommentsUsers", "WriterWorkId", "dbo.WritersWorks");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.CommentsUsers", "UserId", "dbo.Users");
            DropIndex("dbo.WritersWorks", new[] { "UserId" });
            DropIndex("dbo.WritersWorks", new[] { "GenreId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.CommentsUsers", new[] { "WriterWorkId" });
            DropIndex("dbo.CommentsUsers", new[] { "UserId" });
            DropTable("dbo.Genres");
            DropTable("dbo.WritersWorks");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.CommentsUsers");
        }
    }
}
