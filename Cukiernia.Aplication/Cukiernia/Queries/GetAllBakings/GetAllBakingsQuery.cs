using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetAllBakings
{
    public class GetAllBakingsQuery : IRequest<IEnumerable<BakingDto>>
    {

    }
}
