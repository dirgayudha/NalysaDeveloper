using BLL.Interfaces;
using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using DAL.Interfaces;

namespace BLL
{
    public class SkillMasterProvider : ISkillMasterProvider
    {
        [Inject]
        private readonly ISkillMasterDAL SkillMasterDataAccess;

        public SkillMasterProvider(ISkillMasterDAL SkillMasterDAL)
        {
            SkillMasterDataAccess = SkillMasterDAL;
        }

        public List<SkillMaster> GetSkillMasterList()
        {
            var resultDAL = SkillMasterDataAccess.GetAllSkill();

            return resultDAL;
        }
    }
}
