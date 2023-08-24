using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cukiernia.Aplication.Cukiernia.Commands.EditBaking;
using Cukiernia.Domain.Interfaces;
using FluentValidation;


namespace Cukiernia.Aplication.Cukiernia.Commands.EditSubProduct
{
    public class EditSubProductCommandValidator : AbstractValidator<EditSubProductCommand>
    {
        public EditSubProductCommandValidator(IBakingRepository bakingRepository)
        {
            int verifyingId = 0;
            RuleFor(n => n.Id).Custom((value, context) =>
            {
                verifyingId = value;
            });

            RuleFor(n => n.Name)
               .NotEmpty().WithMessage("Nazwa musi być wpisana")
               .NotNull().WithMessage("Nazwa musi być wpisana")
               .MinimumLength(3).WithMessage("Nazwa musi być dłuższa niż 2 znaki")
               .Custom((value, context) =>
               {
                   if (value != null)
                   {
                       var existingSubProduct = bakingRepository.GetSubProductByName(value).Result;
                       if (existingSubProduct != null && existingSubProduct.Id != verifyingId)
                       {
                           context.AddFailure($"Nazwa {value} już istnieje w bazie");
                       }
                   }
                   else
                   {
                       context.AddFailure($"Nazwa musi zostać wpisana");
                   }
               });

            RuleFor(n => n.Price)
                .NotEmpty().WithMessage("Cena musi być większa niż 0")
                .Custom((value, context) =>
                {
                    if (value.GetType() != typeof(decimal))
                    {
                        context.AddFailure($"Wartość {value} nie jest poprawna dla ceny");
                    }
                })
                .PrecisionScale(5, 2, true).WithMessage("Cena musi być z zakresu 0,01-999,99")
                .GreaterThan(0).WithMessage("Cena musi być większa niż 0");

            RuleFor(n => n.Package)
                .NotEmpty().WithMessage("Wielkość opakowania musi zostać wpisana")
                .NotNull().WithMessage("Wielkość opakowania musi zostać wpisana")
                .GreaterThan(0).WithMessage("Wielkość opakowania musi być większa niż 0");
        }
    }
}
