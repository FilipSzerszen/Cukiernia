using AutoMapper;
using Cukiernia.Aplication.ApplicationUser;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.EditBaking
{
    public class EditBakingCommandHandler : IRequestHandler<EditBakingCommand>
    {
       
        private readonly IBakingRepository _bakingRepository;
        private readonly IUserContext _userContext;

        public EditBakingCommandHandler(IBakingRepository bakingRepository, IUserContext userContext)
        {
            _bakingRepository = bakingRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditBakingCommand request, CancellationToken cancellationToken)
        {
            var baking = await _bakingRepository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();
            if(user==null || user.Id != baking.CreatedById)
            {
                return Unit.Value;
            }

            for (int i = 0; i < request.ProductQuantities.Count; i++)
            {
                baking.ProductQuantities
                    .Where(pq => pq.Id == request.ProductQuantities[i].Id)
                    .First()
                    .SubProductQuantity= request.ProductQuantities[i].SubProductQuantity;
            }

            baking.Description = request.Description;
            baking.Price = request.Price;
            baking.Images.AddRange(request.Images);

            await _bakingRepository.Commit();
            return Unit.Value;
        }
    }
}
