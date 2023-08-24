using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.DeleteSubProductFromList
{
    public class DeleteSubProductFromListCommandHandler : IRequestHandler<DeleteSubProductFromListCommand>
    {
        private readonly IBakingRepository _bakingRepository;

        public DeleteSubProductFromListCommandHandler(IBakingRepository bakingRepository)
        {
            _bakingRepository = bakingRepository;
        }

        public async Task<Unit> Handle(DeleteSubProductFromListCommand request, CancellationToken cancellationToken)
        {
            var productQuantity = await _bakingRepository.GetQuantityOfSubProductInBaking(request.Id);
            if (productQuantity != null)
            {
                await _bakingRepository.DeleteProductQuantity(productQuantity);
            }
            return Unit.Value;
        }
    }
}
