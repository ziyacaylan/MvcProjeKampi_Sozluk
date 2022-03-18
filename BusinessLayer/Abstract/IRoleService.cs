using EntityLayer.Concreate;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRoleService
    {
        List<Role> GetList();
        void RoleAdd(Role adminRole);
        Role GetByID(int id);
        void RoleDelete(Role adminRole);
        void RoleUpdate(Role adminRole);
    }
}