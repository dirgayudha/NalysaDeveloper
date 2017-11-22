using BLL;
using BLL.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NalysaDeveloper.Ninject
{
    public class BLLBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDevelopersProvider>().To<DevelopersProvider>();
            Bind<ISkillMasterProvider>().To<SkillMasterProvider>();
        }
    }
}