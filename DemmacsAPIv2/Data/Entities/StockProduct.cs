using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class StockProduct
    {
        public int StockProductId { get; set; }
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Stock Stock { get; set; } = null!;
    }
}
