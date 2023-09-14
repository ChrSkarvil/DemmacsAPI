using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Postalcode
    {
        public Postalcode()
        {
            Customers = new HashSet<Customer>();
            Deliveries = new HashSet<Delivery>();
            Departments = new HashSet<Department>();
            Employees = new HashSet<Employee>();
            Manufacturers = new HashSet<Manufacturer>();
            Stocks = new HashSet<Stock>();
        }

        public string PostalCode1 { get; set; } = null!;
        public string City { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
