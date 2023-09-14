using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string DepartmentAddr { get; set; } = null!;
        public string DepartmentEmail { get; set; } = null!;
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual Postalcode PostalCodeNavigation { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
