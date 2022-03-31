using BusinessLayer.Concreate;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules_FluentValidation;
using EntityLayer.Dto;
using BusinessLayer.Abstract;

namespace MvcProjeKampi.Controllers.WriterPanelController
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        ContentManager contentManager = new ContentManager(new EfContentDal());
        int id;
        // GET: WriterPanel
        [HttpGet]
        public ActionResult WriterProfile(int id=0)
        {
            string p = (string)Session["WriterMail"];
            ViewBag.d = p;
            id = wm.GetWriterIDByWriterMail(p);
            var writervalue = wm.GetByIdWriterDto(id);
            return View(writervalue);
        }

        [HttpPost]
        public ActionResult WriterProfile(WriterLoginDto p)
        {            
            //var writerValue = wm.GetByIdWriterDto(id);
            WriterValidator writervalidator = new WriterValidator();
            ValidationResult result = writervalidator.Validate(p);
            

            if (result.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading", "WriterPanel");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        //[AllowAnonymous]
        public ActionResult MyHeading(string p)
        {
            p = (String)Session["WriterMail"];
            var writerIDInfo = wm.GetWriterIDByWriterMail(p);
            id = writerIDInfo;
            var values = hm.GetListByWriterActiveHeadings(writerIDInfo);
            return View(values);
        }
        // Başlıklara ait tüm yazılar
        public ActionResult ContentByHeading(int id)
        {
            var contentvalues = contentManager.GetListByHeadingID(id);
            return View(contentvalues);
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterID = wm.GetWriterIDByWriterMail((String)Session["WriterMail"]);
            heading.HeadingStatus = true;
            hm.HeadingAdd(heading);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            var HeadingValue = hm.GetByID(id);
            return View(HeadingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            hm.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = hm.GetByID(id);
            if (headingvalue.HeadingStatus)
            {
                headingvalue.HeadingStatus = false;
            }
            else
            {
                headingvalue.HeadingStatus = true;
            }
            hm.HeadingDelete(headingvalue);
            return RedirectToAction("MyHeading");
        }
        public ActionResult AllHeading(int sayfa = 1)
        {

            var headings = hm.GetActiveHeadingList().ToPagedList(sayfa, 10);
            return View(headings);
        }
    }
}