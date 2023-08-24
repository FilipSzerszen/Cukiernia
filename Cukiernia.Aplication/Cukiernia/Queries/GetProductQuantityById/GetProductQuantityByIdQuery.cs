using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Queries.GetProductQuantityById
{
    public class GetProductQuantityByIdQuery : IRequest<ProductQuantityDto>
    {
        public int Id { get; set; }

        public GetProductQuantityByIdQuery(int id)
        {
            Id=id;
        }
    }
}
