using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Pet_Store.DataAcess.Repository;
using Pet_Store.DataAcess.Repository.IRepository;
using PetStore.DataAccess.Repository.IRepositories;
using Project_PetStore.API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnityOfWork(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            ProductsRepository = new ProductsRepository(_context);
            OrderDetailsRepository = new OrderDetailsRepository(_context);
            OrderHeaderRepository = new OrderHeaderRepository(_context);
            ShoppingCartRepository = new ShoppingCartRepository(_context);
            UsersRepository = new UsersRepository(_context);

        }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IProductRepository ProductsRepository { get; private set; }

        public IShoppingCartRepository ShoppingCartRepository { get; private set; }

        public IOrderHeaderRepository OrderHeaderRepository { get; private set; }

        public IOrderDetailsRepository OrderDetailsRepository { get; private set; }

        public IUsersRepository UsersRepository { get; private set; }

        public IUserRoleRepository UserRoleRepository => throw new NotImplementedException();

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
