using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class Role : BaseEntity
    {               
        public Role()
        {
            UserInRole = new List<UserInRole>();
        }
        public string RoleName { get; set; } 
        public string Description { get; set; }
        public ICollection<UserInRole> UserInRole { get; set; }      
    }
}
