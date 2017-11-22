using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Common
{
    public class Developers
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Role Role { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<SkillMaster> Skills { get; set; }
        public virtual ICollection<Skills> Skill { get; set; }

        public Developers()
        {
            this.Skills = new HashSet<SkillMaster>();
        }
    }

    public enum Role
    {
        [Display(Name = "Project Manager")]
        ProjectManager = 1,
        [Display(Name = "Senior Developer")]
        SeniorDeveloper = 2,
        [Display(Name = "Developer")]
        Developer = 3,
        [Display(Name = "Junior Developer")]
        JuniorDeveloper = 4
    }


}
