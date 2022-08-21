using Pet_Store.Domains.Models.DataModels;

namespace Pet_Store.DataAcess.Repository.IRepository
{
    public interface IPaymentRepository : IRepository<Payments>
    {
        void Update(Payments model);
    }
}