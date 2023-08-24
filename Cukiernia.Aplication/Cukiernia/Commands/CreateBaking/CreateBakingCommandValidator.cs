using Cukiernia.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.CreateBaking
{
    public class CreateBakingCommandValidator : AbstractValidator<CreateBakingCommand>
    {

        public CreateBakingCommandValidator(IBakingRepository bakingRepository)
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("Nazwa musi być wpisana")
                .NotNull().WithMessage("Nazwa musi być wpisana")
                .MinimumLength(3).WithMessage("Nazwa musi być dłuższa niż 2 znaki")
                .Custom((value, context) =>
                {
                    if(value != null)
                    {
                        var existingBaking = bakingRepository.GetByName(value).Result;
                        if (existingBaking != null)
                        {
                            context.AddFailure($"Nazwa {value} już istnieje w bazie");
                        }
                    }
                    else
                    {
                        context.AddFailure($"Nazwa musi zostać wpisana");
                    }
                });

            RuleFor(n => n.Description)
                .NotEmpty().WithMessage("Opis musi być wpisany");

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


        }
    }
}
