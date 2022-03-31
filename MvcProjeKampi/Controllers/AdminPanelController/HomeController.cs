using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult HomePage()
        {           

            //var files = screenShotManager.GetList();

            var totalContent = cm.GetTotalContentCount(); //Toplam Entry sayısı
            ViewBag.totalContent = totalContent;

            var totalHeading = hm.GetTotalHeading(); //Toplam Baslik sayisi
            ViewBag.totalHeading = totalHeading;

            var totalWriter = wm.GetTotalWriter();//Toplam Yazar sayisi
            ViewBag.totalWriter = totalWriter;

            var totalMessages = mm.GetTotalMessages(); // Toplam mesaj sayısı
            ViewBag.totalMessages = totalMessages;

            return View();//(files);
        }
    }
}