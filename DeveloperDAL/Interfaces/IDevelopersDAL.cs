using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDevelopersDAL
    {
        List<Developers> GetAllDevelopers();
        Developers GetDevelopersModel();        
        Developers GetDeveloper(int? id);
        List<SkillMaster> GetSkills(int? id);
        void Update(Developers dev, List<int> skills);
        void Add(Developers dev, List<int> skills);
        void Delete(int id);
    }
}
