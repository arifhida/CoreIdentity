using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Attribute = new List<ProductAttribute>();
        }
        public string SKU { get; set; }
        public string ProductName { get; set; } 
        public string ProductDescription { get; set; }
        public string PictureUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public long CategoriId { get; set; }
        public virtual Category Category { get; set; }
        public long? BrandId { get; set; }
        public Brand Brand { get; set; }
        public virtual ICollection<ProductAttribute> Attribute { get; set; }
    }
}
