using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.DataAcess.Repository.UnityOfWork
{
    public interface IUnityOfWork<out TContext>
        where TContext : DbContext
    {
        TContext Context { get; }

        void CrearTransaccion();

        void Commit();

        void Rollback();

        void Guardar();

        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;
    }
}
