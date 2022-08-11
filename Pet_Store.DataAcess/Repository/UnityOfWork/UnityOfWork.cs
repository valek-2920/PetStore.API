
using Microsoft.Extensions.Hosting;
using Pet_Store.DataAcess.Repository;
using Pet_Store.DataAcess.Repository.IRepository;
using Project_PetStore.API.DataAccess;


namespace PetStore.DataAccess.Repository.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork

    {
        readonly ApplicationDbContext _context;
        readonly IHostEnvironment _hostEnvironment;

        public UnityOfWork(ApplicationDbContext context, IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            CategoryRepository = new CategoryRepository(_context);
            FilesRepository = new FilesRepository(_context, _hostEnvironment);
            ProductsRepository = new ProductsRepository(_context, FilesRepository, CategoryRepository);
            OrderDetailsRepository = new OrderDetailsRepository(_context);
            OrderHeaderRepository = new OrderHeaderRepository(_context);
            ShoppingCartRepository = new ShoppingCartRepository(_context);
            UsersRepository = new UsersRepository(_context);
        }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IFilesRepository FilesRepository { get; private set; }

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
