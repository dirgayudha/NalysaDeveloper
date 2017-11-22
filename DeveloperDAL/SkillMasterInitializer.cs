using Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SkillMasterInitializer : DropCreateDatabaseAlways<Dev2Context>
    {
        protected override void Seed(Dev2Context context)
        {
            IList<SkillMaster> defaultSkillMaster = new List<SkillMaster>();

            defaultSkillMaster.Add(new SkillMaster() { SkillName = "C#" });
            defaultSkillMaster.Add(new SkillMaster() { SkillName = "ASP.NET MVC" });
            defaultSkillMaster.Add(new SkillMaster() { SkillName = "Bootstrap" });
            defaultSkillMaster.Add(new SkillMaster() { SkillName = "SQL" });

            foreach (SkillMaster skill in defaultSkillMaster)
                context.SkillMaster.Add(skill);

            base.Seed(context);
        }
        
        
        
        
    }
}
