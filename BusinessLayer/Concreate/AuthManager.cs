using BusinessLayer.Abstract;
using BusinessLayer.Utilies.Cryptos.Hashing;
using EntityLayer.Concreate;
using EntityLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate
{
    public class AuthManager : IAuthService
    {
        IAdminService _adminService;
        IWriterService _writerService;

        public AuthManager(IAdminService adminService, IWriterService writerService)
        {
            _adminService = adminService;
            _writerService = writerService;
        }

        public bool AdminLogIn(AdminLoginDto adminLoginDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(adminLoginDto.AdminMail));
                var admin = _adminService.GetList();
                foreach (var item in admin)
                {
                    if (HashingHelper.AdminVerifyPasswordHash(adminLoginDto.AdminMail, adminLoginDto.AdminPassword, item.AdminUserName,
                        item.AdminPasswordHash, item.AdminPasswordSalt) && item.AdminStatus)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void AdminRegister(string adminUserName, string adminMail, string password, int adminRole, bool status)
        {
            byte[] mailHash, passwordHash, passwordSalt;
            HashingHelper.AdminCreatePasswordHash(adminMail, password, out mailHash, out passwordHash, out passwordSalt);
            var admin = new Admin
            {
                AdminName = adminUserName,
                AdminUserName = mailHash,
                AdminPasswordHash = passwordHash,
                AdminPasswordSalt = passwordSalt,
                RoleId = adminRole,
                AdminStatus = status
            };
            _adminService.AdminAdd(admin);
        }

        public bool IsWriterVerifyRegister(string mail)
        {
            var writerMailCheck = _writerService.GetByWriterMail(mail);
            if (writerMailCheck != null)
            {
                if (writerMailCheck.WriterMail == mail)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }


        // Writer Auth işlemleri 
        public bool WriterLogIn(WriterLoginDto writerLoginDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(writerLoginDto.WriterMail));
                var writer = _writerService.GetList();
                foreach (var item in writer)
                {
                    if (HashingHelper.WriterVerifyPasswordHash(writerLoginDto.WriterPassword, item.WriterPasswordHash, item.WriterPasswordSalt) && writerLoginDto.WriterMail==item.WriterMail && item.WriterStatus)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void WriterRegister(string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerMail, string password, bool WriterStatus)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.WriterCreatePasswordHash(password, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterName = writerName,
                WriterSurname = writerSurName,
                WriterImage = writerImage,
                WriterAbout = writerAbout,
                WriterMail = writerMail,                
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
                WriterTitle = writerTitle,
                WriterStatus = WriterStatus
            };
            _writerService.WriterAdd(writer);
        }

        public void WriterRegisterEdit(int id,string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerMail, string password, bool WriterStatus)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.WriterCreatePasswordHash(password, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterID = id,
                WriterName = writerName,
                WriterSurname = writerSurName,
                WriterImage = writerImage,
                WriterAbout = writerAbout,
                WriterMail = writerMail,
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
                WriterTitle = writerTitle,
                WriterStatus = WriterStatus
            };
            _writerService.WriterUpdate(writer);
        }
    }

}

    
