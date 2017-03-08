using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class UserInRole : BaseEntity
    {        
        public User User { get; set; }        
        public Role Role { get; set; }
    }
}
