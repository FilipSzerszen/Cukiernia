using AutoMapper;
using Cukiernia.Aplication.Cukiernia.Commands.CreateBaking;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.CreateSubProduct
{
    public class CreateSubProductCommandHandler : IRequestHandler<CreateSubProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBakingRepository _bakingRepository;

        public CreateSubProductCommandHandler(IMapper mapper, IBakingRepository bakingRepository)
        {
            _mapper = mapper;
            _bakingRepository = bakingRepository;
        }

        public async Task<Unit> Handle(CreateSubProductCommand request, CancellationToken cancellationToken)
        {
            var subProduct = _mapper.Map<Domain.Entities.SubProduct>(request);
            subProduct.EncodeName();

            await _bakingRepository.CreateSubProduct(subProduct);
            return Unit.Value;
        }
    }
}
