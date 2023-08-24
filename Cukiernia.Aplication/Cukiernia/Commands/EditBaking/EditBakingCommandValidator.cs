using Cukiernia.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.EditBaking
{
    public class EditBakingCommandValidator : AbstractValidator<EditBakingCommand>
    {
        public EditBakingCommandValidator()
        {
            RuleFor(n => n.Description)
                .NotEmpty();

            RuleFor(n => n.Price)
                .NotEmpty()
                .PrecisionScale(5, 2, false)
                .GreaterThan(0);

            RuleForEach(n => n.ProductQuantities)
                .ChildRules(p => p
                    .RuleFor(q => q.SubProductQuantity)
                        .GreaterThan(0).WithMessage("Ilość musi być większa niż 0")
                        .NotEmpty().WithMessage("Ilość musi być wpisana")
                        .NotNull().WithMessage("Ilość musi być wpisana")
                 );
                
                //.NotEmpty()
                //.PrecisionScale(5, 2, false)
                //.GreaterThan(0);
        }
    }
}
