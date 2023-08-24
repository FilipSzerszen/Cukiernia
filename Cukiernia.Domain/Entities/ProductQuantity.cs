using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Domain.Entities
{
    public class ProductQuantity
    {
        public int Id { get; set; }
        public SubProduct? SubProduct { get; set; }
        public int? SubProductId { get; set; }
        public int? SubProductQuantity { get; set; }

        public Baking Baking { get; set; } = null!;
        public int BakingId { get; set; }

        

    }
}
