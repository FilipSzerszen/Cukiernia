using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Domain.Entities
{
    public class SubProduct
    {
        public int  Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsAllergenic { get; set; } 
        public bool IsDeletable { get; set; } 
        public Measure Measure { get; set; } = null!;
        public int MeasureId { get; set; }
        public decimal Price { get; set; }
        public int Package { get; set; } = default!;

        public List<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();
        public string EncodedName { get; private set; } = default!;

        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");

    }
}
