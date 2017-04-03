using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            SubCategory = new List<Category>();
        }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public long? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> SubCategory { get; set; }
    }
}
