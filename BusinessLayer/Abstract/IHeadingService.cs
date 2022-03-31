using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IHeadingService
    {
        List<Heading> GetList();
        List<Heading> GetActiveHeadingList();
        List<Heading> GetListByWriter(int id);
        List<Heading> GetListByWriterActiveHeadings(int id);
        void HeadingAdd(Heading heading);
        Heading GetByID(int id);
        int GetTotalHeading();

        void HeadingDelete(Heading heading);
        void HeadingUpdate(Heading heading);
    }
}