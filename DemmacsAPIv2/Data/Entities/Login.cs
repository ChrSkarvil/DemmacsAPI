using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ulong UserType { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
