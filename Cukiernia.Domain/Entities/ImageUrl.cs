using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Domain.Entities
{
    public class ImageUrl
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;

        public Baking Baking { get; set; } = null!;
        public int BakingId { get; set; }
    }
}
