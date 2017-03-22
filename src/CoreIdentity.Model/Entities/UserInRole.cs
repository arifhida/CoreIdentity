﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class UserInRole
    {        
        public long UserId { get; set; }
        public User User { get; set; }      
        public long RoleId { get; set; }  
        public Role Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool isActive { get; set; }
    }
}
