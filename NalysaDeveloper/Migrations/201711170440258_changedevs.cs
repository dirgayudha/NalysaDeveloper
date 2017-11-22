namespace NalysaDeveloper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedevs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Developers", "SkillLists_ID", "dbo.SkillLists");
            DropIndex("dbo.Developers", new[] { "SkillLists_ID" });
            CreateTable(
                "dbo.SkillListsDevelopers",
                c => new
                    {
                        SkillLists_ID = c.Int(nullable: false),
                        Developers_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillLists_ID, t.Developers_ID })
                .ForeignKey("dbo.SkillLists", t => t.SkillLists_ID, cascadeDelete: true)
                .ForeignKey("dbo.Developers", t => t.Developers_ID, cascadeDelete: true)
                .Index(t => t.SkillLists_ID)
                .Index(t => t.Developers_ID);
            
            DropColumn("dbo.Developers", "SkillLists_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Developers", "SkillLists_ID", c => c.Int());
            DropForeignKey("dbo.SkillListsDevelopers", "Developers_ID", "dbo.Developers");
            DropForeignKey("dbo.SkillListsDevelopers", "SkillLists_ID", "dbo.SkillLists");
            DropIndex("dbo.SkillListsDevelopers", new[] { "Developers_ID" });
            DropIndex("dbo.SkillListsDevelopers", new[] { "SkillLists_ID" });
            DropTable("dbo.SkillListsDevelopers");
            CreateIndex("dbo.Developers", "SkillLists_ID");
            AddForeignKey("dbo.Developers", "SkillLists_ID", "dbo.SkillLists", "ID");
        }
    }
}
