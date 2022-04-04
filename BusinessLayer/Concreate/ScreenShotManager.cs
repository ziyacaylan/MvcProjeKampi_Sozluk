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
    public class ScreenShotManager : IScreenShotService
    {
        IScreenShotDal _screenShotDal;

        public ScreenShotManager(IScreenShotDal screenShotDal)
        {
            _screenShotDal = screenShotDal;
        }

        public ScreenShot GetByID(int id)
        {
            return _screenShotDal.Get(x => x.ID==id);
        }

        public List<ScreenShot> GetList()
        {
            return _screenShotDal.List();
        }

        public void ScreenShotAdd(ScreenShot screenShot)
        {
            _screenShotDal.Insert(screenShot);
        }

        public void ScreenShotDelete(ScreenShot screenShot)
        {
            _screenShotDal.Delete(screenShot);
        }

        public void ScreenShotUpdate(ScreenShot screenShot)
        {
            _screenShotDal.Update(screenShot);
        }
    }
}
