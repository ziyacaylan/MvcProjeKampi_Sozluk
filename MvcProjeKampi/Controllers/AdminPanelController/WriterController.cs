using BusinessLayer.Abstract;
using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using EntityLayer.Dto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writervalidator = new WriterValidator();
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        public ActionResult Index()
        {
            var writervalues = wm.GetList();
            return View(writervalues);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(WriterLoginDto p)
        {
            ValidationResult result = writervalidator.Validate(p);

            if (!authService.IsWriterVerifyRegister(p.WriterMail))
            {
                if (result.IsValid)
                {
                    authService.WriterRegister(p.WriterName, p.WriterSurname, p.WriterTitle, p.WriterAbout, p.WriterImage, p.WriterMail, p.WriterPassword, p.WriterStatus);

                    return RedirectToAction("MyContent", "WriterPanelContent");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Bu mail adresi ile daha önceden kayıt yapılmış...!";
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writervalue = wm.GetByID(id);
            WriterLoginDto value = new WriterLoginDto();
            value.id = id;
            value.WriterName = writervalue.WriterName;
            value.WriterSurname = writervalue.WriterSurname;
            value.WriterTitle = writervalue.WriterTitle;
            value.WriterStatus = writervalue.WriterStatus;
            value.WriterPassword = "";
            value.WriterMail = writervalue.WriterMail;
            value.WriterImage = writervalue.WriterImage;
            value.WriterAbout = writervalue.WriterAbout;
            return View(value);
        }

        [HttpPost]
        public ActionResult EditWriter(WriterLoginDto p)
        {
            ValidationResult result = writervalidator.Validate(p);
            Session["WriterMail"] = p.WriterMail;
            p.WriterStatus = true;
            if (result.IsValid)
            {
                authService.WriterRegisterEdit(p.id,p.WriterName, p.WriterSurname, p.WriterTitle, p.WriterAbout, p.WriterImage, p.WriterMail, p.WriterPassword, p.WriterStatus);
                return RedirectToAction("index");
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