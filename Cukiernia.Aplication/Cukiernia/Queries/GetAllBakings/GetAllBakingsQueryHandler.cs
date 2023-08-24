using AutoMapper;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetAllBakings
{
    public class GetAllBakingsQueryHandler : IRequestHandler<GetAllBakingsQuery, IEnumerable<BakingDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBakingRepository _bakingRepository;

        public GetAllBakingsQueryHandler(IMapper mapper, IBakingRepository bakingRepository)
        {
            _mapper = mapper;
            _bakingRepository = bakingRepository;
        }
        public async Task<IEnumerable<BakingDto>> Handle(GetAllBakingsQuery request, CancellationToken cancellationToken)
        {
            var bakings = await _bakingRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<BakingDto>>(bakings);

            return dtos;
        }
    }
}
