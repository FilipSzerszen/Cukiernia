using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetBakingByEncodedName
{
    public class GetBakingByEncodedNameQuery : IRequest<BakingDto>
    {
        public string EncodedName { get; set; }

        public GetBakingByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
