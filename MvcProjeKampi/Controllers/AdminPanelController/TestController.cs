using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SweetAlert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SweetAlert(string p)
        {
            return RedirectToAction("Index");
        }
    }
}