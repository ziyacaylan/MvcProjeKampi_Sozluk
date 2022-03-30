using BusinessLayer.Concreate;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using BusinessLayer.Abstract;
using EntityLayer.Dto;
using System.Net;
using Newtonsoft.Json;
using MvcProjeKampi.Models;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        // GET: Login
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginDto adminLoginDto)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6LdF1CsfAAAAANcIbb5_0HAekzsPO7lkD8N7qLDw";
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResult>(reply);
            if (captchaResponse.Success)
            {

                if (authService.AdminLogIn(adminLoginDto))
                {
                    FormsAuthentication.SetAuthCookie(adminLoginDto.AdminMail, false);
                    Session["AdminUserName"] = adminLoginDto.AdminMail;
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış...!";
                    return View();
                }
            }
            else
            {
                TempData["Message"] = "Lütfen Güvenliği Doğrulayınız";
                return RedirectToAction("AdminLogin");
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("WriterLogin");//("Headings", "Default");
        }
        public ActionResult WriterLogOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("AllHeading", "WriterPanel");
        }

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(WriterLoginDto writerLoginDto)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6LdF1CsfAAAAANcIbb5_0HAekzsPO7lkD8N7qLDw";
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResult>(reply);
            if (captchaResponse.Success)
            {

                if (authService.WriterLogIn(writerLoginDto))
                {
                    Session["WriterMail"] = writerLoginDto.WriterMail;
                    FormsAuthentication.SetAuthCookie(writerLoginDto.WriterMail, false);
                    return RedirectToAction("MyContent", "WriterPanelContent");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış...!";
                    return View();
                }
            }
            else
            {
                TempData["Message"] = "Lütfen Güvenliği Doğrulayınız";
                return RedirectToAction("WriterLogin");
            }
            return View();
        }

    }
}