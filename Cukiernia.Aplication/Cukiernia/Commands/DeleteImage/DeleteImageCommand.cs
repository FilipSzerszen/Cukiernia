using Cukiernia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia.Commands.DeleteImage
{
    public class DeleteImageCommand : IRequest
    {
        public int Id { get; set; }
    }
}
