using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cukiernia.Domain.Entities
{
    public class Baking
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<ImageUrl> Images { get; set; } = new List<ImageUrl>();
        public decimal Price { get; set; }
        public List<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();

        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }


        public string EncodedName { get; private set; } = default!;

        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
    }
}
