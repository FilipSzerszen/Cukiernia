using Cukiernia.Aplication.Cukiernia.Commands.DeleteBacking;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.DeleteSubProduct
{
    public class DeleteSubProductCommandHandler : IRequestHandler<DeleteSubProductCommand>
    {
        private readonly IBakingRepository _bakingRepository;

        public DeleteSubProductCommandHandler(IBakingRepository bakingRepository)
        {
            _bakingRepository = bakingRepository;
        }
        public async Task<Unit> Handle(DeleteSubProductCommand request, CancellationToken cancellationToken)
        {
            var subProduct = await _bakingRepository.GetSubProductByEncodedName(request.EncodedName!);

            if (subProduct != null)
            {
                await _bakingRepository.DeleteSubProduct(subProduct);
            }
            return Unit.Value;
        }
    }
}
