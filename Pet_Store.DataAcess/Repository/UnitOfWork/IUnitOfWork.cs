using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.DataAcess.Repository.UnitOfWork
{
    public interface IUnitOfWork<out TContext>
        where TContext : DbContext
    {
        TContext Context { get; }

        void CreateTransaction();

        void Commit();

        void Rollback();

        void Save();

        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;
    }
}
