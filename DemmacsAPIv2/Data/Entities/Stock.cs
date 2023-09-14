using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Stock
    {
        public Stock()
        {
            StockProducts = new HashSet<StockProduct>();
        }

        public int StockId { get; set; }
        public string StockAddr { get; set; } = null!;
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual Postalcode PostalCodeNavigation { get; set; } = null!;
        public virtual ICollection<StockProduct> StockProducts { get; set; }
    }
}
