using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Abstract
{
    public interface TreatmentService
    {
        List<Treatment> GetList();
        void TreatmentAdd(Treatment treatment);
        void TreatmentDelete(Treatment treatment);
        void CalculateTreatmentCost(Treatment treatment);
    }
}
