using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.WriterPanelController
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Headings()
        {
            
            var headingList = hm.GetList();
            foreach (var item in headingList)
            {
                var contentCount = cm.GetCountByHeadingID(item.HeadingID);
                item.HeadingName = item.HeadingName + " (" + contentCount + ")";
            }
            return View(headingList);
        }
        public PartialViewResult Index(int id = 0)
        {
            //var contents = cm.GetListByHeadingID()
            var contentList = cm.GetListByHeadingID(id);
            return PartialView(contentList);
        }
    }
}