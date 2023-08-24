using AutoMapper;
using Cukiernia.Aplication.Cukiernia.Commands.CreateBaking;
using Cukiernia.Domain.Entities;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.AddSuBProductToList
{
    public class AddSuBProductToListCommandHandler : IRequestHandler<AddSuBProductToListCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBakingRepository _bakingRepository;
        public AddSuBProductToListCommandHandler(IMapper mapper, IBakingRepository bakingRepository)
        {
            _mapper = mapper;
            _bakingRepository = bakingRepository;
        }

        public async Task<Unit> Handle(AddSuBProductToListCommand request, CancellationToken cancellationToken)
        {
            var quantitySubProductsList = new List<ProductQuantity>();
            foreach(var subProduct in request._subProducts)
            {
                quantitySubProductsList.Add(new ProductQuantity() { SubProductId = subProduct.Id, SubProductQuantity = 0 });
            }
            var productQuantity = _mapper.Map<IEnumerable<Domain.Entities.ProductQuantity>>(quantitySubProductsList);

            await _bakingRepository.AddSubProductsToList(productQuantity, request._encodedName);
            return Unit.Value;
        }
    }
}
