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
    public class TalentManager : ITalentService
    {
        ITalentDal _talentDal;

        public TalentManager(ITalentDal talentDal)
        {
            _talentDal = talentDal;
        }

        public Talent GetByID(int id)
        {
            return _talentDal.Get(x => x.Id == id);
        }

        public List<Talent> GetList()
        {
            return _talentDal.List();
        }

        public void TalentAdd(Talent talent)
        {
            _talentDal.Insert(talent);
        }

        public void TalentDelete(Talent talent)
        {
            _talentDal.Delete(talent);
        }

        public void TalentUpdate(Talent talent)
        {
            _talentDal.Update(talent);
        }
    }
}
