﻿using System;
using System.Collections.Generic;

namespace DemmacsAPIv2.Data.Entities
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
