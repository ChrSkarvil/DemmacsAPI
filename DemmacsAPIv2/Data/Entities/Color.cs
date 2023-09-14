using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Color
    {
        public Color()
        {
            ProductColors = new HashSet<ProductColor>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; } = null!;

        public virtual ICollection<ProductColor> ProductColors { get; set; }
    }
}
