using Pet_Store.Domains.Models.DataModels;

namespace Pet_Store.DataAcess.Repository.IRepository
{
    public interface IUsersRepository : IRepository<Users>
    {
        void Update(Users model);
    }
}
