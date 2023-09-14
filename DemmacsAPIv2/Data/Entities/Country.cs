using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Country
    {
        public Country()
        {
            Customers = new HashSet<Customer>();
            Deliveries = new HashSet<Delivery>();
            Departments = new HashSet<Department>();
            Employees = new HashSet<Employee>();
            Manufacturers = new HashSet<Manufacturer>();
            Postalcodes = new HashSet<Postalcode>();
            Stocks = new HashSet<Stock>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public string CountryCode { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        public virtual ICollection<Postalcode> Postalcodes { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
