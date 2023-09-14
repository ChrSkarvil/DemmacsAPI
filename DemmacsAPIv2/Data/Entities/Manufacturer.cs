using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Products = new HashSet<Product>();
        }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; } = null!;
        public string ManufacturerAddr { get; set; } = null!;
        public long ManufacturerPhone { get; set; }
        public string ManufacturerEmail { get; set; } = null!;
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual Postalcode PostalCodeNavigation { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
