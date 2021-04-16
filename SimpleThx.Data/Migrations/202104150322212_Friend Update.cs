namespace SimpleThx.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friend", "FriendReceive", c => c.Guid(nullable: false));
            AddColumn("dbo.Friend", "FriendSend", c => c.Guid(nullable: false));
            DropColumn("dbo.Friend", "AccountID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friend", "AccountID", c => c.Guid(nullable: false));
            DropColumn("dbo.Friend", "FriendSend");
            DropColumn("dbo.Friend", "FriendReceive");
        }
    }
}
