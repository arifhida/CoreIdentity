using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Model
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        bool isActive { get; set; }
        bool Delete { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool isActive { get; set; }
        public bool Delete { get; set; }
    }
}
