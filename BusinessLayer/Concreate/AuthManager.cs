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

        public AuthManager(IAdminService adminService)
        {
            _adminService = adminService;
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

        public string AdminMailDecode(string mail)
        {
            string decodeMail;
            decodeMail = HashingHelper.AdminMailDecode(mail);
            return decodeMail;
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
    }
}
