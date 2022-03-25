using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetListInbox(string p);
        List<Message> GetListSendBox(string p);
        List<Message> GetListDraftBox(string p);
        List<Message> GetListTrashBox(string p);
        List<Message> GetListImportantBox(string p);
        List<Message> GetListSpamBox(string p);
        List<Message> GetListReadBox(string p);
        List<Message> GetListIUnReadBox(string p);
        void MessageAdd(Message message);
        Message GetByID(int id);
        void MessageDelete(Message message);
        void MessageUpdate(Message message);
    }
}