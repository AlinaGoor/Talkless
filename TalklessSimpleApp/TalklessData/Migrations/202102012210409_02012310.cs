namespace TalklessData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02012310 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Participant1 = c.String(),
                        Participant2 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageGroupId = c.Int(nullable: false),
                        SenderUser = c.String(),
                        ReceiverUser = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        MessageText = c.String(),
                        SeenByReceiver = c.Boolean(nullable: false, defaultValue:false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        IsVisibleProfile = c.Boolean(nullable: false, defaultValue:true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Messages");
            DropTable("dbo.MessageGroups");
        }
    }
}
