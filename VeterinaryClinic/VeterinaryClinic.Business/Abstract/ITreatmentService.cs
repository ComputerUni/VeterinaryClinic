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
        Task<List<Treatment>> GetListAsync();
        Task<List<Treatment>> GetByAnimalIdAsync(int id);
        Task<Treatment> TreatmentAddAsync(Treatment treatment);
        Task TreatmentDeleteAsync(int id);
        Task<Treatment> GetByIdAsync(int id);
        Task TreatmentUpdateAsync(Treatment treatment);
        Task CalculateTreatmentCostAsync(Treatment treatment);
    }
}
