using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.Business.Validators
{
    public class AppointmentValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.AnimalId)
                .NotEmpty().WithMessage("Hayvan ID boş geçilemez");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Randevu tarihi boş geçilemez")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Geçmiş bir tarih seçilemez");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Randevu saati boş geçilemez")
                .Must((dto, time) =>
                dto.Date > DateOnly.FromDateTime(DateTime.Today) || time >= TimeOnly.FromDateTime(DateTime.Now))
                .WithMessage("Geçmiş bir saat seçilemez");

            RuleFor(x => x.Notes)
                .NotEmpty().WithMessage("Not kısmı boş geçilemez");

        }
    }
}
