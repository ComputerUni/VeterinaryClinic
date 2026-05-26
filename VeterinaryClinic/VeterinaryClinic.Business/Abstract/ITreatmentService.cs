using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Abstract
{
    public interface ITreatmentService
    {
        Task<List<Treatment>> GetList();
        Task<Treatment> TreatmentAdd(Treatment treatment);
        Task TreatmentDelete(int id);
        Task CalculateTreatmentCost(Treatment treatment);
    }
}
