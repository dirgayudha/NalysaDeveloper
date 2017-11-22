using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NalysaDeveloper.Models.Repository
{
    public class DevelopersRepository : IDevelopersRepository
    {
        DevContext ctx;
        public DevelopersRepository(DevContext ctx)
        {
            this.ctx = ctx;
        }
        public IQueryable<Developers> All
        {
            get { return ctx.Developers; }
        }

        public Developers Find(int? id)
        {
            Developers objDevelopers = new Developers();
            objDevelopers = ctx.Developers.Where(x => x.ID == id).FirstOrDefault();
            return objDevelopers;
        }

        public void InsertOrUpdate(Developers developer)
        {
            if (developer.ID == default(int))
            {
                ctx.Developers.Add(developer);
            }
            else
            {
                ctx.Entry(developer).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var developer = ctx.Developers.Find(id);
            ctx.Developers.Remove(developer);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }
        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}