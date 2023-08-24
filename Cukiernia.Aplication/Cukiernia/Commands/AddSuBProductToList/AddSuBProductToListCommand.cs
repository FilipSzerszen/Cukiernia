using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.AddSuBProductToList
{
    public class AddSuBProductToListCommand : ProductQuantityDto, IRequest
    {
        public readonly List<SubProductDto> _subProducts;
        public readonly string _encodedName;

        public AddSuBProductToListCommand(List<SubProductDto> subProducts, string encodedName)
        {
            _subProducts = subProducts;
            _encodedName = encodedName;
        }
    }
}
