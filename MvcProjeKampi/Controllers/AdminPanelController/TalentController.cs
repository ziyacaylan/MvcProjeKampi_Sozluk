using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class TalentController : Controller
    {
        TalentManager talentManager = new TalentManager(new EfTalentDal());
        public ActionResult Index(int? page)
        {
            var talentValue = talentManager.GetList().ToPagedList(page ?? 1, 6);
            return View(talentValue);
        }
        public ActionResult TalentCard(int? page)
        {
            var valentValue = talentManager.GetList().ToPagedList(page ?? 1, 10); 
            return View(valentValue);
        }
        public ActionResult DeleteTalent(int id)
        {
            var talentValue = talentManager.GetByID(id);
            talentManager.TalentDelete(talentValue);
            return RedirectToAction("TalentCard");
        }
        [HttpGet]
        public ActionResult AddNewTalent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewTalent(Talent talent)
        {
            TalentValidator validationRules = new TalentValidator();
            ValidationResult result = validationRules.Validate(talent);
            if(result.IsValid)
            { 
            talentManager.TalentAdd(talent);
                return RedirectToAction("TalentCard");
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
        [HttpGet]
        public ActionResult UpdateTalent(int id)
        {
            var talentValue = talentManager.GetByID(id);
            return View(talentValue);
        }
        [HttpPost]
        public ActionResult UpdateTalent(Talent talent)
        {
            TalentValidator validationRules = new TalentValidator();
            ValidationResult result = validationRules.Validate(talent);
            if (result.IsValid)
            {
                talentManager.TalentUpdate(talent);
                return RedirectToAction("TalentCard");
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
    }
}