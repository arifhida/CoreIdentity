using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
