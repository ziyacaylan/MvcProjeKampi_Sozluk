using BusinessLayer.Concreate;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class ContentController : Controller
    {
        // GET: Content

        ContentManager cm = new ContentManager(new EfContentDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllContent(string p = "")
        {
            var values = cm.GetList(p);
            //if (!string.IsNullOrEmpty(p))
            //{
            //    values = values.Where(y => y.ContentValue.Contains(p));
            //}
            //var values = c.Contents.ToList();
            return View(values);
        }

        public ActionResult ContentByHeading(int id)
        {
            var contentvalues = cm.GetListByHeadingID(id);
            ViewBag.headingName = (hm.GetByID(id)).HeadingName;
            return View(contentvalues);
        }
    }
}