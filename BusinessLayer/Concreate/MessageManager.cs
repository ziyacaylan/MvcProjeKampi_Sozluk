using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetByID(int id)
        {
            return _messageDal.Get(x => x.MessageID == id);
        }

        public List<Message> GetListDraftBox(string p)
        {
            return _messageDal.List(x=>x.IsDraft == true && x.SenderMail==p);
        }

        public List<Message> GetListImportantBox(string p)
        {
            return _messageDal.List(x=>x.IsImportant == true && x.ReceiverMail == p && x.Trash==false);
        }

        public List<Message> GetListInbox(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p && x.Trash == false);
        }

        public List<Message> GetListIUnReadBox(string p)
        {
            return _messageDal.List(x => x.IsRead == false && x.ReceiverMail == p && x.Trash == false);
        }

        public List<Message> GetListReadBox(string p)
        {
            return _messageDal.List(x => x.IsRead == true && x.ReceiverMail == p && x.Trash == false);
        }

        public List<Message> GetListSendBox(string p)
        {
            return _messageDal.List(x => x.SenderMail == p && x.Trash == false);
        }

        public List<Message> GetListSpamBox(string p)
        {
            return _messageDal.List(x=>x.IsSpam == true && x.ReceiverMail == p && x.Trash == false);
        }

        public List<Message> GetListTrashBox(string p)
        {
            return _messageDal.List(x=>x.Trash == true);
        }

        public int GetTotalMessages()
        {
            return _messageDal.List().Count();
        }

        public List<Message> IsDraft(string p)
        {
            return _messageDal.List(x => x.IsDraft == true && x.SenderMail == p && x.Trash == false);
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }
    }
}