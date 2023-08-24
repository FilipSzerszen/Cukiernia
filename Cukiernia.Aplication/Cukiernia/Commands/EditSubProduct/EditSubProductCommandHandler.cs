using Cukiernia.Domain.Entities;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.EditSubProduct
{
    public class EditSubProductCommandHandler : IRequestHandler<EditSubProductCommand>
    {
        private readonly IBakingRepository _bakingRepository;

        public EditSubProductCommandHandler(IBakingRepository bakingRepository)
        {
            _bakingRepository = bakingRepository;
        }
        public async Task<Unit> Handle(EditSubProductCommand request, CancellationToken cancellationToken)
        {
            var subProduct = await _bakingRepository.GetSubProductByEncodedName(request.EncodedName!);

            if (subProduct != null)
            {
                subProduct.Name = request.Name;
                subProduct.IsAllergenic = request.IsAllergenic;
                subProduct.Price = request.Price;
                subProduct.Package = request.Package;
                subProduct.MeasureId = request.MeasureId;
                subProduct.IsDeletable = request.IsDeletable;
                subProduct.EncodeName();

                await _bakingRepository.Commit();
            }
            return Unit.Value;
        }
    }
}
