using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Concrete
{
    public class TreatmentManager : ITreatmentService
    {
        ITreatmentDal _treatmentDal;

        public TreatmentManager(ITreatmentDal treatmentDal)
        {
            _treatmentDal = treatmentDal;
        }

        public void CalculateTreatmentCost(Treatment treatment)
        {
            throw new NotImplementedException();
        }

        public List<Treatment> GetList()
        {
            return _treatmentDal.List();
        }

        public void TreatmentAdd(Treatment treatment)
        {
            _treatmentDal.Insert(treatment);
        }

        public void TreatmentDelete(Treatment treatment)
        {
            _treatmentDal.Delete(treatment);
        }
    }
}
