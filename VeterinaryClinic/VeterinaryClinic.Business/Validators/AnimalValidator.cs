using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Entities.Models;

namespace VeterinaryClinic.Business.Validators
{
    public class AnimalValidator : AbstractValidator<AnimalDto>
    {
        public AnimalValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Hayvan sahibi seçilmelidir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hayvan adı zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Yaş zorunludur.")
                .GreaterThan(0).WithMessage("Yaş 0'dan büyük olmalıdır.")
                .LessThan(40);

            RuleFor(x => x.Species)
                .NotEmpty().WithMessage("Tür zorunludur.");

            RuleFor(x => x.Breed)
                .NotEmpty().WithMessage("Irk zorunludur.");

            RuleFor(x => x.Weight)
                .NotEmpty().WithMessage("Ağırlık zorunludur.")
                .GreaterThan(0).WithMessage("Ağırlık 0'dan büyük olmalıdır.");

            RuleFor(x => x.Height)
                .NotEmpty().WithMessage("Uzunluk zorunludur.")
                .GreaterThan(0).WithMessage("Uzunluk 0'dan büyük olmalıdır.");

            RuleFor(x => x.MedicalHistory)
                .NotEmpty().WithMessage("Tedavi tarihi zorunludur.");

        }
    }
}
