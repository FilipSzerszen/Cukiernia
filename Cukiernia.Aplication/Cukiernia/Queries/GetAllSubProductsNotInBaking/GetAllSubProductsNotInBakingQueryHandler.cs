using AutoMapper;
using Cukiernia.Aplication.Cukiernia.Queries.GetAllSubProducts;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetAllSubProductsNotInBaking
{
    public class GetAllSubProductsNotInBakingQueryHandler : IRequestHandler<GetAllSubProductsNotInBakingQuery, IEnumerable<SubProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBakingRepository _bakingRepository;

        public GetAllSubProductsNotInBakingQueryHandler(IMapper mapper, IBakingRepository bakingRepository)
        {
            _mapper = mapper;
            _bakingRepository = bakingRepository;
        }

        public async Task<IEnumerable<SubProductDto>> Handle(GetAllSubProductsNotInBakingQuery request, CancellationToken cancellationToken)
        {

            var subProducts = await _bakingRepository.GetAllSubProductsNotInBaking(request.EncodedName);
            var dtos = _mapper.Map<IEnumerable<SubProductDto>>(subProducts);
            

            return dtos;
        }

    }
}
