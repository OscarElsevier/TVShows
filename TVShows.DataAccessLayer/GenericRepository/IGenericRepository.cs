using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TVShows.DataAccessLayer.DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<T> Get(Expression<Func<T, bool>> expression);
        Task InsertRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
