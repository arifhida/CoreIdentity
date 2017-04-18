using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class User : BaseEntity
    {                  
        public User()
        {
            UserRole = new List<UserInRole>();              
        }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public ICollection<UserInRole> UserRole { get; set; }       
        
    }
}
