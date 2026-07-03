using Power_Fitness.DAL.Context;
using System.Linq.Expressions;

namespace Power_Fitness.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;
        private readonly Dictionary<string, object> _repositories = [];
        public IHealthRecordRepository HealthRecords { get; }
        public IMemberRepository Members { get; }
        public ISessionRepository Sessions { get; }

        public UnitOfWork(GymDbContext dbContext,
            IHealthRecordRepository healthRecordRepository,
            IMemberRepository memberRepository,
            ISessionRepository sessionRepository)
        {
            _dbContext = dbContext;
            HealthRecords = healthRecordRepository;
            Members = memberRepository;
            Sessions = sessionRepository;
        }
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            var entityName = typeof(T).Name;

            if (_repositories.ContainsKey(entityName))
                return (_repositories[entityName] as IRepository<T>)!;
            var repo = new Repository<T>(_dbContext);
            _repositories.Add(entityName, repo);
            return repo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : BaseEntity
        => await _dbContext.Set<T>().AnyAsync(predicate, cancellationToken);

    }
}
