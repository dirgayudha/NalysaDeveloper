namespace NalysaDeveloper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSkillLists : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SkillsDevelopers", "Skills_ID", "dbo.Skills");
            DropForeignKey("dbo.SkillsDevelopers", "Developers_ID", "dbo.Developers");
            DropForeignKey("dbo.SkillListsSkills", "SkillLists_ID", "dbo.SkillLists");
            DropForeignKey("dbo.SkillListsSkills", "Skills_ID", "dbo.Skills");
            DropIndex("dbo.SkillsDevelopers", new[] { "Skills_ID" });
            DropIndex("dbo.SkillsDevelopers", new[] { "Developers_ID" });
            DropIndex("dbo.SkillListsSkills", new[] { "SkillLists_ID" });
            DropIndex("dbo.SkillListsSkills", new[] { "Skills_ID" });
            AddColumn("dbo.Developers", "SkillLists_ID", c => c.Int());
            AddColumn("dbo.Skills", "SkillName", c => c.String());
            AddColumn("dbo.Skills", "HaveSkill", c => c.Boolean(nullable: false));
            AddColumn("dbo.Skills", "Developers_ID", c => c.Int());
            CreateIndex("dbo.Developers", "SkillLists_ID");
            CreateIndex("dbo.Skills", "Developers_ID");
            AddForeignKey("dbo.Skills", "Developers_ID", "dbo.Developers", "ID");
            AddForeignKey("dbo.Developers", "SkillLists_ID", "dbo.SkillLists", "ID");
            DropColumn("dbo.Skills", "SkillID");
            DropTable("dbo.SkillsDevelopers");
            DropTable("dbo.SkillListsSkills");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SkillListsSkills",
                c => new
                    {
                        SkillLists_ID = c.Int(nullable: false),
                        Skills_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillLists_ID, t.Skills_ID });
            
            CreateTable(
                "dbo.SkillsDevelopers",
                c => new
                    {
                        Skills_ID = c.Int(nullable: false),
                        Developers_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skills_ID, t.Developers_ID });
            
            AddColumn("dbo.Skills", "SkillID", c => c.String());
            DropForeignKey("dbo.Developers", "SkillLists_ID", "dbo.SkillLists");
            DropForeignKey("dbo.Skills", "Developers_ID", "dbo.Developers");
            DropIndex("dbo.Skills", new[] { "Developers_ID" });
            DropIndex("dbo.Developers", new[] { "SkillLists_ID" });
            DropColumn("dbo.Skills", "Developers_ID");
            DropColumn("dbo.Skills", "HaveSkill");
            DropColumn("dbo.Skills", "SkillName");
            DropColumn("dbo.Developers", "SkillLists_ID");
            CreateIndex("dbo.SkillListsSkills", "Skills_ID");
            CreateIndex("dbo.SkillListsSkills", "SkillLists_ID");
            CreateIndex("dbo.SkillsDevelopers", "Developers_ID");
            CreateIndex("dbo.SkillsDevelopers", "Skills_ID");
            AddForeignKey("dbo.SkillListsSkills", "Skills_ID", "dbo.Skills", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SkillListsSkills", "SkillLists_ID", "dbo.SkillLists", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SkillsDevelopers", "Developers_ID", "dbo.Developers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SkillsDevelopers", "Skills_ID", "dbo.Skills", "ID", cascadeDelete: true);
        }
    }
}
