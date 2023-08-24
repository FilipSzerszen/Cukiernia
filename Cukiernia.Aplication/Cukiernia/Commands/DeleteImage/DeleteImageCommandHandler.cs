using Cukiernia.Aplication.Cukiernia.Commands.DeleteSubProduct;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
    {
        private readonly IBakingRepository _bakingRepository;

        public DeleteImageCommandHandler(IBakingRepository bakingRepository)
        {
            _bakingRepository = bakingRepository;
        }
        public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _bakingRepository.GetImageById(request.Id!);

            await _bakingRepository.DeleteImage(image);
            return Unit.Value;
        }
    }
}
