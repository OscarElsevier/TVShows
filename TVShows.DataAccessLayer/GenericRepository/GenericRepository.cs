using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TVShows.DataAccessLayer.Models;

namespace TVShows.DataAccessLayer.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBContext _dBContext;
        private readonly DbSet<T> _db;

        public GenericRepository(DBContext dbContext)
        {
            _dBContext = dbContext;
            _db = _dBContext.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            if (entity is not null)
                _db.Remove(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _db;
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _db;
            if (expression is not null)
                query = query.Where(expression);

            if (orderBy is not null)
                query = orderBy(query);

            return await query.AsNoTracking().ToListAsync();

        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dBContext.ChangeTracker.Clear();
            _dBContext.TVShows.Update(entity as TVShowsModel);

        }
    }
}
