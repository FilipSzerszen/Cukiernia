using AutoMapper;
using Cukiernia.Aplication.Cukiernia.Queries.GetSubProductByEncodedName;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetProductQuantityById
{
    public class GetProductQuantityByIdQueryHandler : IRequestHandler<GetProductQuantityByIdQuery, ProductQuantityDto>
    {
        private readonly IBakingRepository _bakingRepository;
        private readonly IMapper _mapper;
        public GetProductQuantityByIdQueryHandler(IBakingRepository bakingRepository, IMapper mapper)
        {
            _bakingRepository = bakingRepository;
            _mapper = mapper;
        }
        public async Task<ProductQuantityDto> Handle(GetProductQuantityByIdQuery request, CancellationToken cancellationToken)
        {
            var productQuantity = await _bakingRepository.GetQuantityOfSubProductInBaking(request.Id);
            if (productQuantity != null)
            {
                var dto = _mapper.Map<ProductQuantityDto>(productQuantity);

                return dto;
            }
            return null;

        }
    }
}
