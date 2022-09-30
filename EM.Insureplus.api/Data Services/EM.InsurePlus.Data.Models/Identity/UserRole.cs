﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.InsurePlus.Data.Models.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
