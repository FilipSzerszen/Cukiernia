using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Domain.Entities
{
    public class Measure
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public List<SubProduct> SubProducts { get; set; } = new List<SubProduct>();
    }
}
