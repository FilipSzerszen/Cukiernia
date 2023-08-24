using AutoMapper;
using Cukiernia.Domain.Entities;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetSubProductByEncodedName
{
    public class GetSubProductByEncodedNameQueryHandler : IRequestHandler<GetSubProductByEncodedNameQuery, SubProductDto>
    {
        private readonly IBakingRepository _bakingRepository;
        private readonly IMapper _mapper;

        public GetSubProductByEncodedNameQueryHandler(IBakingRepository bakingRepository, IMapper mapper)
        {
            _bakingRepository = bakingRepository;
            _mapper = mapper;
        }
        public async Task<SubProductDto> Handle(GetSubProductByEncodedNameQuery request, CancellationToken cancellationToken)
        {

            var subProduct = await _bakingRepository.GetSubProductByEncodedName(request.EncodedName);
            if (subProduct != null)
            {
                var dto = _mapper.Map<SubProductDto>(subProduct);

                return dto;
            }
            return null;

        }
    }
}
