using Power_Fitness.DAL.Context;

namespace Power_Fitness.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GymDbContext _context;
        public Repository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default)
            => trackChanges ? await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken)
            : await _context.Set<T>().ToListAsync(cancellationToken);

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Set<T>().FindAsync(id, cancellationToken);

        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
