using Cukiernia.Aplication.ApplicationUser;
using Cukiernia.Aplication.Cukiernia.Commands.EditBaking;
using Cukiernia.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Cukiernia.Aplication.Cukiernia.Commands.DeleteBacking
{
    public class DeleteBakingCommandHandler : IRequestHandler<DeleteBakingCommand>
    {
        private readonly IBakingRepository _bakingRepository;
        private readonly IUserContext _userContext;

        public DeleteBakingCommandHandler(IBakingRepository bakingRepository, IUserContext userContext)
        {
            _bakingRepository = bakingRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeleteBakingCommand request, CancellationToken cancellationToken)
        {
            var baking = await _bakingRepository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();
            if (user == null || user.Id != baking.CreatedById)
            {
                return Unit.Value;
            }

            await _bakingRepository.Delete(baking);
            return Unit.Value;
        }
    }
}
