using CoreIdentity.Data.Abstract;
using CoreIdentity.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Data.Repositories
{
    public class UserRepository : BaseEntityRepository<User>, IUserRepository
    {
        public UserRepository(CoreIdentityContext context) : base(context)
        {
        }
    }
}
