using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NalysaDeveloper.Models;

namespace NalysaDeveloper.Controllers
{
    public class DevDataTablesController : Controller
    {
        // GET: DevDataTables
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DevContext ctx = new DevContext())
            {
                List<Developers> devList = ctx.Developers.ToList<Developers>();
                return Json(new { data = devList }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}