using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        public int OrderItemId { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Delivery Delivery { get; set; } = null!;
        public virtual Orderitem OrderItem { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
