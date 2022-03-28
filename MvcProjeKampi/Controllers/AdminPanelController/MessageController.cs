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
using System.Web.Services;

namespace MvcProjeKampi.Controllers.AdminPanelController
{
    public class MessageController : Controller
    {
        // GET: Message

        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();

        [Authorize]
        public ActionResult Inbox() //(int? sayfa)
        {
            string mail = (string)Session["AdminUserName"];
            var messagelist = mm.GetListInbox(mail);//.ToPagedList(sayfa ?? 1, 8);
             return View(messagelist);
        }
        public ActionResult Sendbox()
        {
            string mail = (string)Session["AdminUserName"];
            var messagelist = mm.GetListSendBox(mail);
            return View(messagelist);
        }
        public ActionResult GetInboxMessageDetails(int id)
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
            return View(messageValue);
        }
        
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult NewMessage(Message message, string menuName)
        {
            string session = (string)Session["AdminUserName"];

            ValidationResult results = messagevalidator.Validate(message);

            //Eğer kullanıcı Gönder tuşuna basarsa;
            if (menuName == "send")
            {
                if (results.IsValid)
                {
                    message.SenderMail = session;
                    //message.IsDraft = false;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    mm.MessageAdd(message);
                    return RedirectToAction("Sendbox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            //Eğer kullanıcı Taslaklara Kaydet tuşuna basarsa;
            else if (menuName == "draft")
            {
                if (results.IsValid)
                {
                    message.SenderMail = session;
                    message.IsDraft = true;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    mm.MessageAdd(message);
                    return RedirectToAction("DraftMessages");
                }
                else
                {
                    foreach (var item in results.Errors)
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

            //ValidationResult result = messagevalidator.Validate(message);
            //if (result.IsValid)
            //{
            //    string session = (string)Session["AdminUserName"];
            //    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            //    message.IsRead = true;
            //    message.IsImportant = false;
            //    message.SenderMail = session;
            //    mm.MessageAdd(message);
            //    return RedirectToAction("Sendbox");
            //}
            //else
            //{
            //    foreach (var item in result.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}
            return View();
        }
        // çöpe atılan mesajlar burada görüntülenecek
        public ActionResult TrashMailBox()
        {
            string session = (string)Session["AdminUserName"];
            var messageValue = mm.GetListTrashBox(session);
            return View(messageValue);
        }

        //spam mailler bu alanda listelenecek.....
        public ActionResult SpamMessage()
        {
            string session = (string)Session["AdminUserName"];
            var messageValue = mm.GetListSpamBox(session);
            return View(messageValue);
        }

        public ActionResult DraftMessages()
        {
            string session = (string)Session["AdminUserName"];
            var result = mm.IsDraft(session);
            return View(result);
        }

        public ActionResult GetDraftDetails(int id)
        {
            var result = mm.GetByID(id);
            return View(result);
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

        // Önemli Mesajlar burada listelenecek
        public ActionResult GetImportantMessage()
        {
            string session = (string)Session["AdminUserName"];
            var messageValue = mm.GetListImportantBox(session);
            return View(messageValue);
        }

        // Okunmamış Mesajlar burada listelenecek
        public ActionResult GetUnreadMessage()
        {
            string session = (string)Session["AdminUserName"];
            var messageValue = mm.GetListIUnReadBox(session);
            return View(messageValue);
        }
        // Okunmamış Mesajlar burada listelenecek
        public ActionResult GetReadMessage()
        {
            string session = (string)Session["AdminUserName"];
            var messageValue = mm.GetListIUnReadBox(session);
            return View(messageValue);
        }

        [WebMethod]
        public ActionResult DeleteMeesage(string[] m_pos)
        {

            return RedirectToAction("Inbox");
        }
    }
}