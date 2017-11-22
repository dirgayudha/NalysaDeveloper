using BLL.Interfaces;
using Common;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;


namespace BLL
{
    public class DevelopersProvider : IDevelopersProvider
    {
        [Inject]
        private readonly IDevelopersDAL DeveloperDataAccess;
        

        public DevelopersProvider(IDevelopersDAL developerDAL)
        {
            DeveloperDataAccess = developerDAL;
        }
        

        public List<Developers> GetDevelopersList()
        {
            var resultDAL = DeveloperDataAccess.GetAllDevelopers();

            return resultDAL;
        }

        public Developers GetDeveloperByID(int? id)
        {
            var developer = DeveloperDataAccess.GetDeveloper(id);

            return developer;
            
        }

        public List<SkillMaster> GetDeveloperSkills(int? id)
        {
            var developerSkills = DeveloperDataAccess.GetSkills(id);

            return developerSkills;
        }


        public List<Developers> GetDevelopersListByRole(Role role)
        {
            var resultDAL = DeveloperDataAccess.GetAllDevelopers();

            var filterResult = resultDAL.Where(x => x.Role == role).ToList();

            return filterResult;
        }

        public Developers GetDevelopersModel()
        {
            var model = DeveloperDataAccess.GetDevelopersModel();

            return model;
        }

        public void Insert(Developers dev, List<int> selected)
        {
            DeveloperDataAccess.Add(dev, selected);
        }

        public void Edit(Developers dev, List<int> selected)
        {
            DeveloperDataAccess.Update(dev, selected);
        }

        public void Delete(int id)
        {
            DeveloperDataAccess.Delete(id);
        }
    }
}
