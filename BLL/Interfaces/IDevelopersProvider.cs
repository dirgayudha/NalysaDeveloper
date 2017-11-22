using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDevelopersProvider
    {
        List<Developers> GetDevelopersList();
        List<Developers> GetDevelopersListByRole(Role role);
        Developers GetDevelopersModel();
        Developers GetDeveloperByID(int? id);
        List<SkillMaster> GetDeveloperSkills(int? id);
        void Insert(Developers dev, List<int> selected);
        void Edit(Developers dev, List<int> selected);
        void Delete(int id);
    }
}
