using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager messageManager = new MessageManager(new EfMessageDal());

        ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }
        
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            if (contactvalues.IsRead)
            {
                contactvalues.IsRead = false;
            }
            else
            {
                contactvalues.IsRead = true;
            }

            cm.ContactUpdate(contactvalues);
            return View(contactvalues);
        }
        public ActionResult IsRead(int id)
        {
            var messageValue = cm.GetByID(id);

            if (messageValue.IsRead)
            {
                messageValue.IsRead = false;
            }
            else
            {
                messageValue.IsRead = true;
            }

            cm.ContactUpdate(messageValue);
            return RedirectToAction("Index");
        }
        public ActionResult IsImportant(int id)
        {
            var messageValue = cm.GetByID(id);

            if (messageValue.IsImportant)
            {
                messageValue.IsImportant = false;
            }
            else
            {
                messageValue.IsImportant = true;
            }

            cm.ContactUpdate(messageValue);
            return RedirectToAction("Index");
        }
        public PartialViewResult MessageListMenu()
        {
            string session = (string)Session["AdminUserName"];

            // inboxtaki toplam mesaj sayısı
            var contact = cm.GetList().Count();
            ViewBag.contact = contact;

            //gönderilen mesaj saytısı
            var sendMail = messageManager.GetListSendBox(session).Count();
            ViewBag.sendMail = sendMail;

            //alınan mesaj sayısı
            var receiverMail = messageManager.GetListInbox(session).Count();
            ViewBag.receiverMail = receiverMail;

            //taslaklara kaydedilen mesaj sayısı
            var draftMail = messageManager.GetListDraftBox(session).Count();
            ViewBag.draftMail = draftMail;

            // çöp kutusundaki mesaj sayısı
            var trashMail = messageManager.GetListTrashBox(session).Count();
            ViewBag.trashMail = trashMail;

            // spam maillerin sayısı
            var smapMail = messageManager.GetListSpamBox(session).Count();
            ViewBag.smapMail = smapMail;

            // önemli mesaj sayısı
            var importantMail = messageManager.GetListImportantBox(session).Count();
            ViewBag.importantMail = importantMail;

            //okunan mesaj sayısı
            var readMail = messageManager.GetListReadBox(session).Count();
            ViewBag.readMail = readMail;

            // okunmayan mesaj sayısı
            var unreadMail = messageManager.GetListIUnReadBox(session).Count();
            ViewBag.unreadMail = unreadMail;

            return PartialView();
        }
    }
}