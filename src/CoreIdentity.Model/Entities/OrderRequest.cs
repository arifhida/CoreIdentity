using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class OrderRequest : BaseEntity
    {        
        public User User { get; set; }
        public NpgsqlPoint Origin { get; set; }
        public NpgsqlPoint Destination { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
