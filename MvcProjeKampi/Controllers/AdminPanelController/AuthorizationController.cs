using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using EntityLayer.Dto;
using BusinessLayer.Abstract;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization

        AdminManager adminManager = new AdminManager(new EfAdminDal());
        RoleManager RoleManager = new RoleManager(new EfRoleDal());
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        public ActionResult Index(int? sayfa)
        {
            var adminvalues = adminManager.GetList().ToPagedList(sayfa ?? 1, 5);
            return View(adminvalues);
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            List<SelectListItem> valueAdminRole = (from x in RoleManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.RoleName,
                                                       Value = x.RoleId.ToString()
                                                   }).ToList();

            ViewBag.valueAdminRole = valueAdminRole;

            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(AdminLoginDto adminLogInDto)
        {
            authService.AdminRegister(adminLogInDto.AdminUserName, adminLogInDto.AdminMail, adminLogInDto.AdminPassword, adminLogInDto.AdminRoleId, adminLogInDto.AdminStatus);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            List<SelectListItem> valueAdminRole = (from x in RoleManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.RoleName,
                                                       Value = x.RoleId.ToString()
                                                   }).ToList();

            ViewBag.valueAdminRole = valueAdminRole;
            var adminvalue = adminManager.GetByID(id);
            return View(adminvalue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {
            adminManager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAdmin(int id)
        {
            var adminValue = adminManager.GetByID(id);
            adminManager.AdminDelete(adminValue);
            return RedirectToAction("Index");       
        }

        // Admin aktif / pasif yapma işlemi
        public ActionResult ChangeStatus(int id)
        {
            var adminValue = adminManager.GetByID(id);
            if (adminValue.AdminStatus)
            {
                adminValue.AdminStatus = false;
            }
            else
            {
                adminValue.AdminStatus = true;
            }
            adminManager.AdminUpdate(adminValue);
            return RedirectToAction("Index");
        }
        

        // Yetkiler Bölümü
        [HttpGet]
        public ActionResult Roles(int ? sayfa)
        {
            var roleValue = RoleManager.GetList().ToPagedList(sayfa ?? 1, 5);
            return View(roleValue);
        }
        [HttpPost]
        public ActionResult Roles(Role role)
        {
            RoleManager.RoleUpdate(role);
            return RedirectToAction("Roles");
        }

        // Role ekleme partialView
        public PartialViewResult AddRolePartial()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(Role role)
        {
            RoleManager.RoleAdd(role);
            return RedirectToAction("Roles");
        }

        [HttpGet]
        public ActionResult EditRole(int id)
        {
            var roleValue = RoleManager.GetByID(id);
            RoleManager.RoleUpdate(roleValue);
            return View(roleValue);
        }
        [HttpPost]
        public ActionResult EditRole(Role role)
        {
            RoleManager.RoleUpdate(role);
            return RedirectToAction("Roles");
        }
        public ActionResult AdminLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("HomePage", "Home");
        }

    }
}