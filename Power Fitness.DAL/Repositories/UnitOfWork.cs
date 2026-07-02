using Power_Fitness.DAL.Context;

namespace Power_Fitness.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;
        private readonly Dictionary<string, object> _repositories = [];
        public UnitOfWork(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            var entityName = typeof(T).Name;

            if (_repositories.ContainsKey(entityName))
                return (_repositories[entityName]as IRepository<T>)!;
            var repo = new Repository<T>(_dbContext);
            _repositories.Add(entityName, repo);
            return repo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
