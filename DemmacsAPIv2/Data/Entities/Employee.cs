using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Logins = new HashSet<Login>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeFname { get; set; } = null!;
        public string EmployeeSname { get; set; } = null!;
        public long EmployeePhone { get; set; }
        public string EmployeeAddr { get; set; } = null!;
        public decimal EmployeeSalary { get; set; }
        public string EmployeeEmail { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int DepartmentId { get; set; }
        public int CountryId { get; set; }
        public string PostalCode { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
        public virtual Postalcode PostalCodeNavigation { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Login> Logins { get; set; }
    }
}
