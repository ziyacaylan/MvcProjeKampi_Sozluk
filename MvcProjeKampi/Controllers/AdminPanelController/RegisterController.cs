using BusinessLayer.Abstract;
using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Dto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writervalidator = new WriterValidator();
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));

        
        [HttpGet]
        public ActionResult WriterRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterRegister(WriterLoginDto p)
        {
            ValidationResult result = writervalidator.Validate(p);
            p.WriterStatus = true;

            if (!authService.IsWriterVerifyRegister(p.WriterMail))
            {
                if (result.IsValid)
                {
                    authService.WriterRegister(p.WriterName, p.WriterSurname, p.WriterTitle, p.WriterAbout, p.WriterImage, p.WriterMail, p.WriterPassword, p.WriterStatus);

                    return RedirectToAction("WriterLogin", "Login");
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
    }
}