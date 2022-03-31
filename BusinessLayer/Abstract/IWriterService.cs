using EntityLayer.Concreate;
using EntityLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IWriterService
    {
        List<Writer> GetList();
        void WriterAdd(Writer writer);
        void WriterDelete(Writer writer);
        void WriterUpdate(Writer writer);
        void WriterUpdate(WriterLoginDto p);
        Writer GetByID(int id);
        Writer GetByWriterMail(string mail);
        int GetWriterIDByWriterMail(string mail);
        int GetTotalWriter();
        WriterLoginDto GetByIdWriterDto(int id);
    }
}