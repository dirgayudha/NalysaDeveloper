namespace NalysaDeveloper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SkillID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SkillLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SkillsDevelopers",
                c => new
                    {
                        Skills_ID = c.Int(nullable: false),
                        Developers_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skills_ID, t.Developers_ID })
                .ForeignKey("dbo.Skills", t => t.Skills_ID, cascadeDelete: true)
                .ForeignKey("dbo.Developers", t => t.Developers_ID, cascadeDelete: true)
                .Index(t => t.Skills_ID)
                .Index(t => t.Developers_ID);
            
            CreateTable(
                "dbo.SkillListsSkills",
                c => new
                    {
                        SkillLists_ID = c.Int(nullable: false),
                        Skills_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillLists_ID, t.Skills_ID })
                .ForeignKey("dbo.SkillLists", t => t.SkillLists_ID, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skills_ID, cascadeDelete: true)
                .Index(t => t.SkillLists_ID)
                .Index(t => t.Skills_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillListsSkills", "Skills_ID", "dbo.Skills");
            DropForeignKey("dbo.SkillListsSkills", "SkillLists_ID", "dbo.SkillLists");
            DropForeignKey("dbo.SkillsDevelopers", "Developers_ID", "dbo.Developers");
            DropForeignKey("dbo.SkillsDevelopers", "Skills_ID", "dbo.Skills");
            DropIndex("dbo.SkillListsSkills", new[] { "Skills_ID" });
            DropIndex("dbo.SkillListsSkills", new[] { "SkillLists_ID" });
            DropIndex("dbo.SkillsDevelopers", new[] { "Developers_ID" });
            DropIndex("dbo.SkillsDevelopers", new[] { "Skills_ID" });
            DropTable("dbo.SkillListsSkills");
            DropTable("dbo.SkillsDevelopers");
            DropTable("dbo.SkillLists");
            DropTable("dbo.Skills");
            DropTable("dbo.Developers");
        }
    }
}
