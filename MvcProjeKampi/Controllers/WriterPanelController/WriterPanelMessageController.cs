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

namespace MvcProjeKampi.Controllers.WriterPanelController
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();

        public ActionResult Inbox()
        {
            string mail = (string)Session["WriterMail"];
            var messagelist = mm.GetListInbox(mail);
            return View(messagelist);
        }
        public PartialViewResult MessageListMenu()
        {
            string session = (string)Session["WriterMail"];
            var sendMail = mm.GetListSendBox(session).Count();
            ViewBag.sendMail = sendMail;

            var receiverMail = mm.GetListInbox(session).Count();
            ViewBag.receiverMail = receiverMail;

            var draftMail = mm.GetListDraftBox(session).Count();
            ViewBag.draftMail = draftMail;

            var trashMail = mm.GetListTrashBox(session).Count();
            ViewBag.trashMail = trashMail;

            var readMail = mm.GetListReadBox(session).Count;
            ViewBag.readMail = readMail;

            var unReadMail = mm.GetListIUnReadBox(session).Count;
            ViewBag.unReadMail = unReadMail;

            var importantMail = mm.GetListImportantBox(session).Count();
            ViewBag.importantMail = importantMail;
            var spamMail = mm.GetListSpamBox(session).Count();
            ViewBag.spam = spamMail;
            return PartialView();
        }

        public ActionResult Sendbox()
        {
            string mail = (string)Session["WriterMail"];
            var messagelist = mm.GetListSendBox(mail);

            return View(messagelist);
        }
        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            if (!values.IsRead)
            {
                values.IsRead = true;
            }
            mm.MessageUpdate(values);
            return View(values);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            if (!values.IsRead)
            {
                values.IsRead = true;
            }
            mm.MessageUpdate(values);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message message, string menuName)
        {

            ValidationResult result = messagevalidator.Validate(message);
            string sender = (string)Session["WriterMail"];
            //Eğer kullanıcı Gönder tuşuna basarsa;
            if (menuName == "send")
            {
                if (result.IsValid)
                {
                    message.SenderMail = sender;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    mm.MessageAdd(message);
                    return RedirectToAction("Sendbox");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            //Eğer kullanıcı Taslaklara Kaydet tuşuna basarsa;
            else if (menuName == "draft")
            {
                if (result.IsValid)
                {
                    message.SenderMail = sender;
                    message.IsDraft = true;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    mm.MessageAdd(message);
                    return RedirectToAction("DraftMessages");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            //Eğer kullanıcı İptal tuşuna basarsa;
            else if (menuName == "cancel")
            {
                return RedirectToAction("NewMessage");
            }
            return View();
        }

        // Okunmamış Mesajlar burada listelenecek
        public ActionResult GetUnreadMessage()
        {
            string session = (string)Session["WriterMail"];
            var messageValue = mm.GetListIUnReadBox(session);
            return View(messageValue);
        }
        // Okunmamış Mesajlar burada listelenecek
        public ActionResult GetReadMessage()
        {
            string session = (string)Session["WriterMail"];
            var messageValue = mm.GetListReadBox(session);
            return View(messageValue);
        }
        public ActionResult GetImportantMessage()
        {
            string session = (string)Session["WriterMail"];
            var messageValue = mm.GetListImportantBox(session);
            return View(messageValue);
        }

        public ActionResult IsRead(int id)
        {
            var messageValue = mm.GetByID(id);

            if (messageValue.IsRead)
            {
                messageValue.IsRead = false;
            }
            else
            {
                messageValue.IsRead = true;
            }

            mm.MessageUpdate(messageValue);            
            return RedirectToAction("Inbox");
        }

        public ActionResult IsImportant(int id)
        {
            var messageValue = mm.GetByID(id);

            if (messageValue.IsImportant)
            {
                messageValue.IsImportant = false;
            }
            else
            {
                messageValue.IsImportant = true;
            }

            mm.MessageUpdate(messageValue);
            return RedirectToAction("Inbox");
        }

        public ActionResult SpamMessage()
        {
            string session = (string)Session["WriterMail"];
            var messageValue = mm.GetListSpamBox(session);
            return View(messageValue);
        }

        public ActionResult DraftMessages()
        {
            string session = (string)Session["WriterMail"];
            var result = mm.IsDraft(session);
            return View(result);
        }

        public ActionResult GetDraftDetails(int id)
        {
            var result = mm.GetByID(id);
            return View(result);
        }

        // çöpe atılan mesajlar burada görüntülenecek
        public ActionResult TrashMailBox()
        {
            string session = (string)Session["AdminUserName"];
            var messageValue = mm.GetListTrashBox(session);
            return View(messageValue);
        }
    }
}