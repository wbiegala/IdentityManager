namespace IdentityManager.Domain.Base
{
    public abstract class DomainException<TAggregate> : Exception
        where TAggregate : Aggregate
    {
        protected readonly TAggregate _aggregate;

        public DomainException(TAggregate aggregate)
        {
            _aggregate = aggregate;
        }
    }
}
