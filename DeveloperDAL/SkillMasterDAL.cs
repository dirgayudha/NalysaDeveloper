using Common;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SkillMasterDAL : ISkillMasterDAL
    {
        public List<SkillMaster> GetAllSkill()
        {
            using (Dev2Context ctx = new Dev2Context())
            {
                return ctx.SkillMaster.ToList();
            }
        }
    }
}
