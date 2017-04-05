using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class ProductAttribute : BaseEntity
    {
        public string Name { get; set; }
        public string value { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

}
