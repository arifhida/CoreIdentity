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
            RoledUser = new List<UserInRole>();
        } 
        public string RoleName { get; set; }       
        public ICollection<UserInRole> RoledUser { get; set; }
    }
}
