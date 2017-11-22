using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public class SkillMaster
    {
        public SkillMaster()
        {
            this.developers = new HashSet<Developers>();
        }
        public int ID { get; set; }
        public string SkillName { get; set; }
        //public virtual ICollection<Skills> Skills { get; set; }

        public virtual ICollection<Developers> developers { get; set; }

    }
}