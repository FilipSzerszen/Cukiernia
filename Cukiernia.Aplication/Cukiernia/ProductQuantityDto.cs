using Cukiernia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia
{
    public class ProductQuantityDto
    {
        public int Id { get; set; }
        public SubProduct SubProduct { get; set; } = default!;
        public int SubProductId { get; set; }
        public int SubProductQuantity { get; set; }

        public Baking Baking { get; set; } = default!;
        public int BakingId { get; set; }
    }
}
