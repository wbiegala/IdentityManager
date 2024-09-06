using IdentityManager.Domain.Base;

namespace IdentityManager.Data.Repositories
{
    public interface IRepository<TAggregate>
        where TAggregate : Aggregate
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
