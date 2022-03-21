using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        ImageFileManager im = new ImageFileManager(new EfImageFileDal());
        public ActionResult Index()
        {
            var files = im.GetList();
            return View(files);
        }
    }
}