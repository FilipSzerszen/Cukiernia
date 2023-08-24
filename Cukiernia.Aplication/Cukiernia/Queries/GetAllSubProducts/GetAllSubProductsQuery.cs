using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetAllSubProducts
{
    public class GetAllSubProductsQuery : IRequest<IEnumerable<SubProductDto>>
    {

    }
}
