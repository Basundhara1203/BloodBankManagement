namespace Blood_Bank_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloodStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BloodType = c.String(nullable: false, maxLength: 12),
                        Stock = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        HospId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HospInfoes", t => t.HospId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HospId);
            
            CreateTable(
                "dbo.HospInfoes",
                c => new
                    {
                        HospId = c.Int(nullable: false, identity: true),
                        HospName = c.String(nullable: false),
                        RegNo = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HospId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleUsers", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DonorInfoes",
                c => new
                    {
                        DonorId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Age = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        BloodType = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Weight = c.Int(nullable: false),
                        MedicalHist = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonorId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DonorInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.BloodStocks", "UserId", "dbo.Users");
            DropForeignKey("dbo.BloodStocks", "HospId", "dbo.HospInfoes");
            DropForeignKey("dbo.HospInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.RoleUsers");
            DropIndex("dbo.DonorInfoes", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.HospInfoes", new[] { "UserId" });
            DropIndex("dbo.BloodStocks", new[] { "HospId" });
            DropIndex("dbo.BloodStocks", new[] { "UserId" });
            DropTable("dbo.DonorInfoes");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Users");
            DropTable("dbo.HospInfoes");
            DropTable("dbo.BloodStocks");
        }
    }
}
