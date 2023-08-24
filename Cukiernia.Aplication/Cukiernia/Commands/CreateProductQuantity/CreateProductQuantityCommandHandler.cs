using AutoMapper;
using Cukiernia.Domain.Interfaces;
using MediatR;

namespace Cukiernia.Aplication.Cukiernia.Commands.CreateProductQuantity
{
    public class CreateProductQuantityCommandHandler : IRequestHandler<CreateProductQuantityCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBakingRepository _bakingRepository;
        public CreateProductQuantityCommandHandler(IMapper mapper, IBakingRepository bakingRepository)
        {
            _mapper = mapper;
            _bakingRepository = bakingRepository;
        }

        public async Task<Unit> Handle(CreateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var productQuantity = _mapper.Map<List<Domain.Entities.ProductQuantity>>(request);

            await _bakingRepository.CreateProductsQuantityList(productQuantity);
            return Unit.Value;
        }
    }
}
