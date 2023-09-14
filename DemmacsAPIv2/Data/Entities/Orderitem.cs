using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Orderitem
    {
        public Orderitem()
        {
            Orders = new HashSet<Order>();
        }

        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
