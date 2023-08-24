using Cukiernia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Cukiernia
{
    public class SubProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsAllergenic { get; set; } = false;
        public decimal Price { get; set; } = default!;
        public int Package { get; set; } = default!;
        public int MeasureId { get; set; }
        public Measure Measure { get; set; } = null!;
        public bool IsDeletable { get; set; }
        public string? EncodedName { get; set; }
        public bool IsInBaking { get; set; }
    }
}
