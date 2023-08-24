using AutoMapper;
using Cukiernia.Aplication.ApplicationUser;
using Cukiernia.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.CreateBaking
{
    public class CreateBakingCommandHandler : IRequestHandler<CreateBakingCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBakingRepository _bakingRepository;
        private readonly IUserContext _userContext;

        public CreateBakingCommandHandler(IMapper mapper, IBakingRepository bakingRepository, IUserContext userContext)
        {
            _mapper = mapper;
            _bakingRepository = bakingRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(CreateBakingCommand request, CancellationToken cancellationToken)
        {
            var baking = _mapper.Map<Domain.Entities.Baking>(request);
            baking.EncodeName();

            baking.CreatedById = _userContext.GetCurrentUser().Id;

            await _bakingRepository.Create(baking);
            return Unit.Value;
        }
    }
}
