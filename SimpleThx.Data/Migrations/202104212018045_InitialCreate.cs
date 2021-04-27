namespace SimpleThx.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountInfo",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        PictureURL = c.String(),
                        CreateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.AccountID);
            
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        FriendID = c.Int(nullable: false, identity: true),
                        FriendReceive = c.Guid(nullable: false),
                        FriendSend = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.FriendID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PostUserID = c.Guid(nullable: false),
                        AboutUserID = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.FriendAccountInfo",
                c => new
                    {
                        Friend_FriendID = c.Int(nullable: false),
                        AccountInfo_AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Friend_FriendID, t.AccountInfo_AccountID })
                .ForeignKey("dbo.Friend", t => t.Friend_FriendID, cascadeDelete: true)
                .ForeignKey("dbo.AccountInfo", t => t.AccountInfo_AccountID, cascadeDelete: true)
                .Index(t => t.Friend_FriendID)
                .Index(t => t.AccountInfo_AccountID);
            
            CreateTable(
                "dbo.PostAccountInfo",
                c => new
                    {
                        Post_PostID = c.Int(nullable: false),
                        AccountInfo_AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_PostID, t.AccountInfo_AccountID })
                .ForeignKey("dbo.Post", t => t.Post_PostID, cascadeDelete: true)
                .ForeignKey("dbo.AccountInfo", t => t.AccountInfo_AccountID, cascadeDelete: true)
                .Index(t => t.Post_PostID)
                .Index(t => t.AccountInfo_AccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.PostAccountInfo", "AccountInfo_AccountID", "dbo.AccountInfo");
            DropForeignKey("dbo.PostAccountInfo", "Post_PostID", "dbo.Post");
            DropForeignKey("dbo.FriendAccountInfo", "AccountInfo_AccountID", "dbo.AccountInfo");
            DropForeignKey("dbo.FriendAccountInfo", "Friend_FriendID", "dbo.Friend");
            DropIndex("dbo.PostAccountInfo", new[] { "AccountInfo_AccountID" });
            DropIndex("dbo.PostAccountInfo", new[] { "Post_PostID" });
            DropIndex("dbo.FriendAccountInfo", new[] { "AccountInfo_AccountID" });
            DropIndex("dbo.FriendAccountInfo", new[] { "Friend_FriendID" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropTable("dbo.PostAccountInfo");
            DropTable("dbo.FriendAccountInfo");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Post");
            DropTable("dbo.Friend");
            DropTable("dbo.AccountInfo");
        }
    }
}
