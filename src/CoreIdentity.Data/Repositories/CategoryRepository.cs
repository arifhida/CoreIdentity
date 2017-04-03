using CoreIdentity.Data.Abstract;
using CoreIdentity.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Data.Repositories
{
    public class CategoryRepository : BaseEntityRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CoreIdentityContext context) : base(context)
        {
        }
    }
}
