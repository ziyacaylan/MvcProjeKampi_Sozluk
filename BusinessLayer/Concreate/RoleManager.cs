using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using EntityLayer.Concrete;
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

        public void RoleAdd(Role Role)
        {
            _RoleDal.Insert(Role);
        }

        public void RoleDelete(Role Role)
        {
            _RoleDal.Delete(Role);
        }

        public void RoleUpdate(Role Role)
        {
            _RoleDal.Update(Role);
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