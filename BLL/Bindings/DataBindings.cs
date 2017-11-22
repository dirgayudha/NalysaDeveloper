using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using DAL;
using DAL.Interfaces;

namespace BLL.Bindings
{
    public class DataBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDevelopersDAL>().To<DevelopersDAL>();
            Bind<ISkillMasterDAL>().To<SkillMasterDAL>();
        }
    }
}
