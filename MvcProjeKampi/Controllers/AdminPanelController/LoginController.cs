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

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        WriterManager wlm = new WriterManager(new EfWriterDal());

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            AdminManager adminManager = new AdminManager(new EfAdminDal());
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