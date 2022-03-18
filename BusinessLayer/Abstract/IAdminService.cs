using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<Admin> GetList();
        void AdminAdd(Admin admin);
        Admin GetByID(int id);
        Admin GetByName(string name);
        void AdminDelete(Admin admin);
        void AdminUpdate(Admin admin);
    }
}