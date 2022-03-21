using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization

        AdminManager adminManager = new AdminManager(new EfAdminDal());
        RoleManager adminRoleManager = new RoleManager(new EfRoleDal());
        public ActionResult Index()
        {
            var adminvalues = adminManager.GetList();
            return View(adminvalues);
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            List<SelectListItem> valueAdminRole = (from x in adminRoleManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.RoleName,
                                                       Value = x.RoleId.ToString()
                                                   }).ToList();
            ViewBag.d = valueAdminRole;
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            adminManager.AdminAdd(admin);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var adminvalue = adminManager.GetByID(id);
            return View(adminvalue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {
            adminManager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }

        public ActionResult AdminLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("HomePage", "Home");
        }
    }
}