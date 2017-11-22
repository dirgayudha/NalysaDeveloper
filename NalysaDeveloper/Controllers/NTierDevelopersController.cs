using BLL;
using BLL.Interfaces;
using Common;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NalysaDeveloper.Controllers
{
    public class NTierDevelopersController : Controller
    {
        [Inject]
        public readonly IDevelopersProvider _developersProvider;
        [Inject]
        public readonly ISkillMasterProvider _skillMasterProvider;

        public NTierDevelopersController(IDevelopersProvider developersProvider, ISkillMasterProvider skillMasterProvider)
        {
            _developersProvider = developersProvider;
            _skillMasterProvider = skillMasterProvider;
        }

        
        // GET: NTierDevelopers
        public ActionResult Index()
        {
            return View("GetList");
        }

        public PartialViewResult ListByRole(string role = "All")
        {            
            
            var data = _developersProvider.GetDevelopersList();
            if (role != "All")
            {
                var selected = (Role)Enum.Parse(typeof(Role), role);
                data = _developersProvider.GetDevelopersListByRole(selected);
            }

            return PartialView(data);
        }

        public ActionResult GetList(string role = "All")
        {
            return View((object)role);
        }
        // GET
        public ActionResult Create()
        {
            var model = _developersProvider.GetDevelopersModel();
            var allskills = _skillMasterProvider.GetSkillMasterList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Developers developers)
        {
            if (ModelState.IsValid)
            {
                var selectedSkills = developers.Skill.Where(x => x.HaveSkill).Select(x => x.ID).ToList();
                _developersProvider.Insert(developers, selectedSkills);
                return RedirectToAction("Index");
            }
            return View(developers);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = _developersProvider.GetDeveloperByID(id);
            if (developers == null)
            {
                return HttpNotFound();
            }
            var checkedskill = _developersProvider.GetDeveloperSkills(id);
            var allskills = _skillMasterProvider.GetSkillMasterList();
            var checkboxlist = new List<Skills>();
            foreach (var skill in allskills)
            {
                checkboxlist.Add(new Skills()
                {
                    ID = skill.ID,
                    SkillName = skill.SkillName,                    
                    HaveSkill = checkedskill.Where(x => x.ID == skill.ID).Any()
                });
            }
            developers.Skill = checkboxlist;

            return View(developers);
        }

        // GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = _developersProvider.GetDeveloperByID(id);
            if (developers == null)
            {
                return HttpNotFound();
            }
            var checkedskill = _developersProvider.GetDeveloperSkills(id);
            var allskills = _skillMasterProvider.GetSkillMasterList();
            var checkboxlist = new List<Skills>();
            foreach (var skill in allskills)
            {
                checkboxlist.Add(new Skills()
                {
                    ID = skill.ID,
                    SkillName = skill.SkillName,
                    HaveSkill = checkedskill.Where(x => x.ID == skill.ID).Any()
                });
            }
            developers.Skill = checkboxlist;

            return View(developers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Developers developers)
        {
            if (ModelState.IsValid)
            {

                var selectedSkills = developers.Skill.Where(x => x.HaveSkill).Select(x => x.ID).ToList();
                _developersProvider.Edit(developers, selectedSkills);
                return RedirectToAction("Index");
            }
            return View(developers);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developers developers = _developersProvider.GetDeveloperByID(id);
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
            _developersProvider.Delete(id);
            return RedirectToAction("Index");
        }



    }
}