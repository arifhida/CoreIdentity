using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Model
{
    public class UserInRoleViewModel
    {
        public long Id { get; set; }
        public RoleViewModel Role { get; set; }
        public bool Delete { get; set; }
    }
}
