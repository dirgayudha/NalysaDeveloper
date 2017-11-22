using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NalysaDeveloper.Models;
using BLL;

namespace NalysaDeveloper.Controllers
{
    public class DevelopersController : Controller
    {
        private DevContext db = new DevContext();
        

        // GET: Developers
        public ActionResult Index()
        {
            return View("GetList");
        }

        public PartialViewResult ListByRole(string role = "All")
        {
            using (DevContext ctx = new DevContext())
            {
                var data = ctx.Developers.ToList();
                if (role != "All")
                {
                    var selected = (Role)Enum.Parse(typeof(Role), role);
                    data = ctx.Developers.Where(p => p.Role == selected).ToList();
                }

                return PartialView(data);
            }
            //DevelopersProvider provider = new DevelopersProvider();
            //var data = provider.GetDevelopersList();
            //if (role != "All")
            //{
            //    var selected = (Role)Enum.Parse(typeof(Role), role);
            //    data = provider.GetDevelopersListByRole(selected);
            //}

            //return PartialView(data);
        }
        public ActionResult GetList(string role = "All")
        {
            return View((object)role);
        }
        // GET: Developers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = db.Developers.Find(id);
            if (developers == null)
            {
                return HttpNotFound();
            }
            var checkedskill = SkillLists.GetForDeveloper(id);
            var allskills = SkillLists.GetAll();
            var checkboxlist = new List<Skills>();
            foreach (var skill in allskills)
            {
                checkboxlist.Add(new Skills()
                {
                    ID = skill.ID,
                    SkillName = skill.SkillName,
                    //We should have already-selected genres be checked
                    HaveSkill = checkedskill.Where(x => x.ID == skill.ID).Any()
                });
            }
            developers.Skill = checkboxlist;

            return View(developers);
        }

        // GET: Developers/Create
        public ActionResult Create()
        {
            Developers model = new Developers();
            var allskills = SkillLists.GetAll();
            var checkboxlist = new List<Skills>();
            foreach (var skill in allskills)
            {
                checkboxlist.Add(new Skills()
                {
                    ID = skill.ID,
                    SkillName = skill.SkillName,
                    HaveSkill = false
                });
            }
            model.Skill = checkboxlist;
            return View(model);
        }


        // POST: Developers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Developers developers)
        {
            if (ModelState.IsValid)
            {
                var selectedSkills = developers.Skill.Where(x => x.HaveSkill).Select(x => x.ID).ToList();
                Add(developers.Name, developers.Age, developers.BirthDate, developers.Role, selectedSkills);
                return RedirectToAction("Index");
            }

            return View(developers);
        }

        public static void Add(string name, int age, DateTime birthdate, Role role, List<int> skills)
        {
            using (DevContext ctx = new DevContext())
            {
                var developer = new Developers()
                {
                    Name = name,
                    Age = age,
                    BirthDate = birthdate,
                    Role = role
                };

                foreach (var skillid in skills)
                {
                    var skill = ctx.SkillLists.Find(skillid);
                    developer.Skills.Add(skill);
                }
                ctx.Developers.Add(developer);
                ctx.SaveChanges();
            }
        }

        // GET: Developers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = db.Developers.Find(id);
            if (developers == null)
            {
                return HttpNotFound();
            }

            var checkedskill = SkillLists.GetForDeveloper(id);
            var allskills = SkillLists.GetAll();
            var checkboxlist = new List<Skills>();
            foreach (var skill in allskills)
            {
                checkboxlist.Add(new Skills()
                {
                    ID = skill.ID,
                    SkillName = skill.SkillName,
                    //We should have already-selected genres be checked
                    HaveSkill = checkedskill.Where(x => x.ID == skill.ID).Any()
                });
            }
            developers.Skill = checkboxlist;
            
            return View(developers);
        }

        // POST: Developers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Developers developers)
        {
            if (ModelState.IsValid)
            {

                var selectedSkills = developers.Skill.Where(x => x.HaveSkill).Select(x => x.ID).ToList();
                Update(developers.ID, developers.Name, developers.Age, developers.BirthDate, developers.Role ,selectedSkills);
                return RedirectToAction("Index");
            }
            return View(developers);            
                
        }

        private void Update(int id,string name, int age, DateTime birthdate, Role role ,List<int> skills)
        {
            using (DevContext context = new DevContext())
            {
                var updateDeveloper = db.Developers.Where(x => x.ID == id).First();
                updateDeveloper.Name = name;
                updateDeveloper.Age = age;
                updateDeveloper.BirthDate = birthdate;
                updateDeveloper.Role = role;
                
                updateDeveloper = UpdateSkill(skills, updateDeveloper, context);
                db.Entry(updateDeveloper).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private Developers UpdateSkill(List<int> skills, Developers updateDeveloper, DevContext context)
        {
            
            if (skills == null)
            {
                updateDeveloper.Skills = new List<SkillLists>();
                return updateDeveloper;
            }
            var skillsHS = new HashSet<int>(skills);
            var developerSkills = new HashSet<int>
                (updateDeveloper.Skills.Select(c => c.ID));
            foreach (var skill in db.SkillLists)
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

        // GET: Developers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = db.Developers.Find(id);
            if (developers == null)
            {
                return HttpNotFound();
            }
            return View(developers);
        }

        // POST: Developers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Developers developers = db.Developers.Find(id);
            db.Developers.Remove(developers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
