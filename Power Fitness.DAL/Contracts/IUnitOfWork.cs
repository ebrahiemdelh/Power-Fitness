using System.Linq.Expressions;

namespace Power_Fitness.DAL.Contracts
{
    public interface IUnitOfWork
    {
        public IHealthRecordRepository HealthRecords { get; }
        public IMemberRepository Members { get; }
        public IPlanRepository Plans{ get; }
        public ISessionRepository Sessions { get; }
        IRepository<T> GetRepository<T>() where T : BaseEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : BaseEntity;
    }
}
