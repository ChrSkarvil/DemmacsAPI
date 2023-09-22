using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Order
    {
        public Order()
        {
            Orderitems = new HashSet<Orderitem>();
        }
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Delivery Delivery { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
        public virtual ICollection<Orderitem> Orderitems { get; set; }
    }
}
