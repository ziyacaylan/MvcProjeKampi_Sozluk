using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class AboutController : Controller
    {
        AboutManager abm = new AboutManager(new EfAboutDal());
        AboutValidator aboutValidator = new AboutValidator();
        public ActionResult Index()
        {
            var aboutvalues = abm.GetList();
            return View(aboutvalues);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            //ValidationResult result = aboutValidator.Validate(about);
            //if(result.IsValid)
            //{
            abm.AboutAdd(about);
            return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        foreach (var item in result.Errors)
            //        {
            //            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //        }
            //    }
            //    return View();
        }

        [HttpGet]
        public ActionResult EditAbout(int id)
        {
            var aboutValue = abm.GetByID(id);
            return View(aboutValue);
        }
        [HttpPost]
        public ActionResult EditAbout(About about)
        {
            ValidationResult result = aboutValidator.Validate(about);
            if (result.IsValid)
            {
                abm.AboutUpdate(about);
                return RedirectToAction("Index");
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

        public ActionResult DeleteAbout(int id)
        {
            var aboutValue = abm.GetByID(id);
            abm.AboutDelete(aboutValue);
            return RedirectToAction("Index");
        }


        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
    }
}