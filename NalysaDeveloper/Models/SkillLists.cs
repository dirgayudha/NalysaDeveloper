using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NalysaDeveloper.Models
{
    public class SkillLists
    {
        public SkillLists()
        {
            this.Developers = new HashSet<Developers>();
        }
        public int ID { get; set; }
        public string SkillName { get; set; }
        //public virtual ICollection<Skills> Skills { get; set; }

        public virtual ICollection<Developers> Developers { get; set; }

        public static List<SkillLists> GetAll()
        {
            using (DevContext ctx = new DevContext())
            {
                return ctx.SkillLists.ToList();
            }
        }
        public static SkillLists GetByID(int id)
        {
            using (DevContext ctx = new DevContext())
            {
                return ctx.SkillLists.Where(x => x.ID == id).First();
            }
        }

        public static List<SkillLists> GetForDeveloper(int? id)
        {
            using (DevContext ctx = new DevContext())
            {
                return ctx.Developers.Where(x => x.ID == id).First().Skills.ToList();
            }
        }
    }
}