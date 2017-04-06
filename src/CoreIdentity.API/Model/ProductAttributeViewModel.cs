using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Model
{
    public class ProductAttributeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string value { get; set; }
        public long ProductId { get; set; }
    }
}
