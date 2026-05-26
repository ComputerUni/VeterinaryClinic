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

        public Task CalculateTreatmentCost(Treatment treatment)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Treatment>> GetList()
        {
            return await _treatmentDal.ListAsync();
        }

        public async Task<Treatment> TreatmentAdd(Treatment treatment)
        {
            await _treatmentDal.InsertAsync(treatment);
            return treatment;

        }

        public async Task TreatmentDelete(int id)
        {
            var deleteTreatment = await _treatmentDal.GetAsync(t => t.Id == id);
            if(deleteTreatment != null)
            {
                await _treatmentDal.DeleteAsync(deleteTreatment);
            }

        }
    }
}
