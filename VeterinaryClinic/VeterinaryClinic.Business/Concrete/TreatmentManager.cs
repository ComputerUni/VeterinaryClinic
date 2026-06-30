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
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task CalculateTreatmentCostAsync(Treatment treatment)
        {
            throw new NotImplementedException();
        }

        public async Task<Treatment> GetByIdAsync(int id)
        {
            return await _unitOfWork.Treatments.GetAsync(t => t.Id == id);
        }

        public async Task<List<Treatment>> GetListAsync()
        {
            return await _unitOfWork.Treatments.ListAsync();
        }

        public async Task<Treatment> TreatmentAddAsync(Treatment treatment)
        {
            await _unitOfWork.Treatments.InsertAsync(treatment);
            await _unitOfWork.SaveAsync();
            return treatment;

        }

        public async Task TreatmentDeleteAsync(int id)
        {
            var deleteTreatment = await _unitOfWork.Treatments.GetAsync(t => t.Id == id);
            if(deleteTreatment != null)
            {
                await _unitOfWork.Treatments.DeleteAsync(deleteTreatment);
                await _unitOfWork.SaveAsync();
            }

        }

        public async Task TreatmentUpdateAsync(Treatment treatment)
        {
            await _unitOfWork.Treatments.UpdateAsync(treatment);
            await _unitOfWork.SaveAsync();
        }
    }
}
