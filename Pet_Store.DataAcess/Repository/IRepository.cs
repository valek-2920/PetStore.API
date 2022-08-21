using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.DataAcess.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> Filter, string? includeProperties = null);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? Filter = null, string? includeProperties = null);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entity);
    }
}
