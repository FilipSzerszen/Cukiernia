using Cukiernia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia
{
    public class BakingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<ImageUrl> Images { get; set; }  = new List<ImageUrl>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; } = default!;
        public List<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();

        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
        public string? EncodedName { get; set; }
        public bool IsEditable { get; set; }
        
    }
}
