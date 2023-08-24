using AutoMapper;
using Cukiernia.Aplication.Cukiernia.Queries.GetAllBakings;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetAllSubProducts
{
    public class GetAllSubProductsQueryHandler : IRequestHandler<GetAllSubProductsQuery, IEnumerable<SubProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBakingRepository _bakingRepository;

        public GetAllSubProductsQueryHandler(IMapper mapper, IBakingRepository bakingRepository)
        {
            _mapper = mapper;
            _bakingRepository = bakingRepository;
        }

        public async Task<IEnumerable<SubProductDto>> Handle(GetAllSubProductsQuery request, CancellationToken cancellationToken)
        {
            var subProducts = await _bakingRepository.GetAllSubProducts();
            var dtos = _mapper.Map<IEnumerable<SubProductDto>>(subProducts);

            return dtos;
        }
    }
}
