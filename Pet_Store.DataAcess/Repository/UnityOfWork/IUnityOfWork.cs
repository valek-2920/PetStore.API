using Microsoft.EntityFrameworkCore;
using Pet_Store.DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository.UnityOfWork
{
    public interface IUnityOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductsRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IUsersRepository UsersRepository { get; }

        void Save();

    }
}