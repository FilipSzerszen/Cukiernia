using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetSubProductByEncodedName
{
    public class GetSubProductByEncodedNameQuery : IRequest<SubProductDto>
    {
        public string EncodedName { get; set; }

        public GetSubProductByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
