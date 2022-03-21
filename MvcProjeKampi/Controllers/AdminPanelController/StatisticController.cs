using DataAccessLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class StatisticController : Controller
    {
        Context _context = new Context();

        public ActionResult Index()
        {
            //1) Toplam kategori sayısı
            var totalCategory = _context.Categories.Count();
            ViewBag.totalCategory = totalCategory;

            //2) Başlık tablosunda "yazılım" kategorisine ait başlık sayısı
            var totalHeading = _context.Headings.Count(x => x.CategoryID == 9);
            ViewBag.totalHeading = totalHeading;

            //3) Yazar adında 'a' harfi geçen yazar sayısı
            var writerNameContains_a = _context.Writers.Count(x => x.WriterName.Contains("a"));
            ViewBag.writerNameContains_a = writerNameContains_a;

            //4) En fazla başlığa sahip kategori adı
            var mostTitleCategory = _context.Headings.Max(x => x.Category.CategoryName);
            ViewBag.mostTitleCategory = mostTitleCategory;

            //5) Kategori tablosunda durumu true olan kategoriler ile false olan kategoriler arasındaki sayısal fark
            //var activeCategory = _context.Categories.Count(x => x.CategoryStatus == true);
            var passiveCategory = _context.Categories.Count(x => x.CategoryStatus == false);
            var result = totalCategory - passiveCategory;
            ViewBag.result = result;

            return View();
        }
    }
}





