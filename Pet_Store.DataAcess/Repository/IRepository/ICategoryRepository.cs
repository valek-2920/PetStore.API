using Pet_Store.Domains.Models.DataModels;

namespace Pet_Store.DataAcess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category model);
    }
}