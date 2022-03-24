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
    public class RoleManager : IRoleService
    {
        IRoleDal _RoleDal;

        public RoleManager(IRoleDal RoleDal)
        {
            this._RoleDal = RoleDal;
        }

        public void RoleAdd(Role role)
        {
            _RoleDal.Insert(role);
        }

        public void RoleDelete(Role role)
        {
            _RoleDal.Delete(role);
        }

        public void RoleUpdate(Role role)
        {
            _RoleDal.Update(role);
        }

        public Role GetByID(int id)
        {
            return _RoleDal.Get(x => x.RoleId == id);
        }

        public List<Role> GetList()
        {
            return _RoleDal.List();
        }
    }
}