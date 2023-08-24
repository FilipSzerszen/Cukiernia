using AutoMapper;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetBakingByEncodedName
{
    public class GetBakingByEncodedNameQueryHandler : IRequestHandler<GetBakingByEncodedNameQuery, BakingDto>
    {
        private readonly IBakingRepository _bakingRepository;
        private readonly IMapper _mapper;

        public GetBakingByEncodedNameQueryHandler(IBakingRepository bakingRepository, IMapper mapper)
        {
            _bakingRepository = bakingRepository;
            _mapper = mapper;
        }
        public async Task<BakingDto> Handle(GetBakingByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var baking = await _bakingRepository.GetByEncodedName(request.EncodedName);

            var dto = _mapper.Map<BakingDto>(baking);

            return dto;
        }
    }
}
