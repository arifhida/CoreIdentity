using CoreIdentity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CoreIdentity.Model.Entities;

namespace CoreIdentity.Data.Abstract
{
    public interface IEntityBaseRepository<T> where T : class, IBaseEntity, new()
    {
        Task<IEnumerable<T>> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAll();
        Task<int> Count();
        T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetSingleAsync(int id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        Task Commit();
    }

    public interface IUserRepository : IEntityBaseRepository<User> { }
    public interface IRoleRepository : IEntityBaseRepository<Role> { }
    public interface IUserInRoleRepository : IEntityBaseRepository<UserInRole> { }
    public interface IOrderRequestRepository : IEntityBaseRepository<OrderRequest> { }

}
