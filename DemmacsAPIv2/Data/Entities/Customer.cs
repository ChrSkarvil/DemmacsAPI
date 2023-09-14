using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Carts = new HashSet<Cart>();
            Logins = new HashSet<Login>();
            Orders = new HashSet<Order>();
            Payments = new HashSet<Payment>();
        }

        public int CustomerId { get; set; }
        public string CustomerFname { get; set; } = null!;
        public string CustomerSname { get; set; } = null!;
        public string CustomerAddr { get; set; } = null!;
        public long CustomerPhone { get; set; }
        public string CustomerEmail { get; set; } = null!;
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual Postalcode PostalCodeNavigation { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
