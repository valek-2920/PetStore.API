using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.DataAcess.Repository.UnityOfWork
{
    public class UnityOfWork<TContext> : IUnityOfWork<TContext>, IDisposable
        where TContext : DbContext
    {
        public UnityOfWork(TContext dbContext)
        {
            context = dbContext;
        }

        readonly TContext context;
        IDbContextTransaction transaction;
        IDictionary<string, object> repositories;

        bool isDisposed;

        public TContext Context
        {
            get { return context; }
        }

        public void CrearTransaccion()
        {
            transaction = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
            transaction.Dispose();
        }

        public void Guardar()
        {
            Context.SaveChanges();
        }


        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var repository = Context.GetService<IRepository<TEntity>>();
            if (repository != null)
            {
                return repository;
            }

            var type = typeof(TEntity);
            var typeName = type.Name;

            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            if (!repositories.ContainsKey(typeName))
            {
                var repositoryInstance = new Repository<TEntity>(Context);
                repositories.Add(typeName, repositoryInstance);
            }

            return (IRepository<TEntity>)repositories[typeName];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            isDisposed = true;
        }
    }
}
