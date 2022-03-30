using BusinessLayer.Abstract;
using BusinessLayer.Utilies.Cryptos.Hashing;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using EntityLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate
{
    public class WriterManager : IWriterService
    {
        IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetByID(int id)
        {
            return _writerDal.Get(x => x.WriterID == id);
        }

        public WriterLoginDto GetByIdWriterDto(int id)
        {
            var writer = _writerDal.Get(x => x.WriterID == id);
            //writerı writerlogindto'ya çevirme
            var writerLogInDto = new WriterLoginDto
            {
                id = writer.WriterID,
                WriterName = writer.WriterName,
                WriterSurname = writer.WriterSurname,
                WriterImage = writer.WriterImage,
                WriterMail = writer.WriterMail,
                WriterAbout = writer.WriterAbout,
                WriterTitle = writer.WriterTitle
            };
            return writerLogInDto;
        }

        public Writer GetByWriterMail(string mail)
        {
            return _writerDal.Get(x => x.WriterMail == mail);
        }

        public List<Writer> GetList()
        {
            return _writerDal.List();
        }

        public int GetWriterIDByWriterMail(string mail)
        {
            return (_writerDal.List(x => x.WriterMail == mail).Select(y => y.WriterID).FirstOrDefault());
        }

        public void WriterAdd(Writer writer)
        {
            _writerDal.Insert(writer);
        }

        public void WriterDelete(Writer writer)
        {
            _writerDal.Delete(writer);
        }

        public void WriterUpdate(Writer writer)
        {
            _writerDal.Update(writer);
        }

        public void WriterUpdate(WriterLoginDto p)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.WriterCreatePasswordHash(p.WriterPassword, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterID = p.id,
                WriterName = p.WriterName,
                WriterSurname = p.WriterSurname,
                WriterImage = p.WriterImage,
                WriterAbout = p.WriterAbout,
                WriterMail = p.WriterMail,
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
                WriterTitle = p.WriterTitle,
                WriterStatus = p.WriterStatus
            };
            _writerDal.Update(writer);
        }
    }
}