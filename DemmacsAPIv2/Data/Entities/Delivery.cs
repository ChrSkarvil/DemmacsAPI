using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Delivery
    {
        public Delivery()
        {
            Orders = new HashSet<Order>();
        }

        public int DeliveryId { get; set; }
        public string DeliveryAddr { get; set; } = null!;
        public DateTime? DispatchedDate { get; set; }
        public DateTime EstDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal DeliveryFee { get; set; }
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual Postalcode PostalCodeNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
