using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.DataAcess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        readonly DbContext _context;
        readonly DbSet<TEntity> dbSet;
        bool isDisposed;

        public Repository(DbContext context)
        {
            isDisposed = false;

            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entidad");
            }

            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entidad");
            }

            dbSet.Update(entity);
        }


        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? Filter = null, string? includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (Filter != null)
            {
                query = query.Where(Filter);
            }

            if (includeProperties != null)
            {

                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> Filter, string? includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;
            query = query.Where(Filter);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.FirstOrDefault();
        }


        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }

            isDisposed = true;
        }
    }
}
