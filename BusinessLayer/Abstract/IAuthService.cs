using EntityLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        // Admin Login
        void AdminRegister(string adminUserName, string adminMail, string password, int adminRole, bool status);
        bool AdminLogIn(AdminLoginDto adminLoginDto);

        // Writer Login 
        void WriterRegister(string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerMail, string password, bool WriterStatus);
        void WriterRegisterEdit(int id,string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerMail, string password, bool WriterStatus);
        bool WriterLogIn(WriterLoginDto writerLoginDto);
        bool IsWriterVerifyRegister(string mail);

    }
}
