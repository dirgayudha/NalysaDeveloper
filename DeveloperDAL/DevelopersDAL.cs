using Common;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DevelopersDAL : IDevelopersDAL
    {
        Dev2Context ctx = new Dev2Context();
        public List<Developers> GetAllDevelopers()
        {
            return ctx.Developers.ToList();            
        }

        public Developers GetDeveloper(int? id)
        {
            return ctx.Developers.Where(x => x.ID == id).First();
        }
        
        public Developers GetDevelopersModel()
        {
            Developers dev = new Developers();
            return dev;
        }

        public List<SkillMaster> GetSkills(int? id)
        {
            return ctx.Developers.Where(x => x.ID == id).First().Skills.ToList();
        }

        public void Add(Developers dev, List<int> skills)
        {
            var developer = new Developers()
            {
                Name = dev.Name,
                Age = dev.Age,
                BirthDate = dev.BirthDate,
                Role = dev.Role
            };

            foreach (var skillid in skills)
            {
                var skill = ctx.SkillMaster.Find(skillid);
                developer.Skills.Add(skill);
            }
            ctx.Developers.Add(developer);
            ctx.SaveChanges();
        }

        public void Update(Developers dev, List<int> skills)
        {
            var updateDeveloper = ctx.Developers.Where(x => x.ID == dev.ID).First();
            updateDeveloper.Name = dev.Name;
            updateDeveloper.Age = dev.Age;
            updateDeveloper.BirthDate = dev.BirthDate;
            updateDeveloper.Role = dev.Role;

            updateDeveloper = UpdateSkill(skills, updateDeveloper);
            ctx.Entry(updateDeveloper).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        private Developers UpdateSkill(List<int> skills, Developers updateDeveloper)
        {

            if (skills == null)
            {
                updateDeveloper.Skills = new List<SkillMaster>();
                return updateDeveloper;
            }
            var skillsHS = new HashSet<int>(skills);
            var developerSkills = new HashSet<int>
                (updateDeveloper.Skills.Select(c => c.ID));
            foreach (var skill in ctx.SkillMaster)
            {
                if (skillsHS.Contains(skill.ID))
                {
                    if (!developerSkills.Contains(skill.ID))
                    {

                        updateDeveloper.Skills.Add(skill);

                    }
                }
                else
                {
                    if (developerSkills.Contains(skill.ID))
                    {
                        updateDeveloper.Skills.Remove(skill);

                    }
                }
            }
            return updateDeveloper;
        }
        public void Delete(int id)
        {
            Developers developers = ctx.Developers.Find(id);
            ctx.Developers.Remove(developers);
            ctx.SaveChanges();
        }
    }
}
