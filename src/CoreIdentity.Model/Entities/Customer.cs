using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class Customer : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}
