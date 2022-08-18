
using Microsoft.Extensions.Hosting;
using Pet_Store.DataAcess.Repository;
using Pet_Store.DataAcess.Repository.IRepository;
using Project_PetStore.API.DataAccess;


namespace PetStore.DataAccess.Repository.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork

    {
        readonly ApplicationDbContext _context;

        public UnityOfWork(ApplicationDbContext context, IHostEnvironment hostEnvironment)
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



        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
