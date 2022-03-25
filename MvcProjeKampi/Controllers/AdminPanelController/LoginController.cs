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



namespace MvcProjeKampi.Controllers.AdminPanelController
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()));
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        // GET: Login
        [HttpGet]
        public ActionResult AdminLogin()
        {
            //AdminManager adminManager = new AdminManager(new EfAdminDal());
            //var values = adminManager.GetByName(admin.AdminUserName);

            //if (values.AdminUserName == admin.AdminUserName && values.AdminPassword == admin.AdminPassword)
            //{
            //    FormsAuthentication.SetAuthCookie(values.AdminUserName, false);
            //    Session["AdminUserName"] = values.AdminUserName;
            //    return RedirectToAction("Index", "AdminCategory");
            //}
            //else
            //{
            //    RedirectToAction("Index");
            //}
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginDto adminLoginDto)
        {

            if (authService.AdminLogIn(adminLoginDto) )
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



        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer writer)
        {
            //var values = wlm.GetWriter(writer.WriterMail, writer.WriterPassword);
            //if (values != null)
            //{
            //    FormsAuthentication.SetAuthCookie(values.WriterMail, false);
            //    Session["WriterMail"] = values.WriterMail;
            //    return RedirectToAction("MyContent", "WriterPanelContent");
            //}
            //else
            //{
            //    RedirectToAction("WriterLogin");
            //}
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}