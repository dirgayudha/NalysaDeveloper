using Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Dev2Context : DbContext
    {
        public Dev2Context() : base("name : Devs2")
        {
            //Database.SetInitializer(new SkillMasterInitializer());
        }

        public DbSet<Developers> Developers { get; set; }
        public DbSet<SkillMaster> SkillMaster { get; set; }
        public DbSet<Skills> Skills { get; set; }
    }
}
