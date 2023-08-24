using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetAllSubProductsNotInBaking
{
    public class GetAllSubProductsNotInBakingQuery : IRequest<IEnumerable<SubProductDto>>
    {
        public string EncodedName { get; set; }
        public GetAllSubProductsNotInBakingQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
