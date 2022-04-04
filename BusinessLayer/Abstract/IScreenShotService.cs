using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IScreenShotService
    {
        List<ScreenShot> GetList();
        void ScreenShotAdd(ScreenShot screenShot);
        ScreenShot GetByID(int id);
        void ScreenShotDelete(ScreenShot screenShot);
        void ScreenShotUpdate(ScreenShot screenShot);
    }
}
