using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pet_Store.DataAcess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Products>
    {
        void Update(Products model);
    }
}
