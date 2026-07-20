using Power_Fitness.DAL.Context;

namespace Power_Fitness.DAL.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Session>> GetSessionsWithCategoryAndTrainerAsync(CancellationToken cancellationToken = default)
            => await _dbContext.Sessions
                .Include(s => s.Category)
                .Include(s => s.Trainer)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        public async Task<Session> GetSessionWithCategoryAndTrainerAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.Sessions.Where(s => s.Id == id)
                .Include(s => s.Category)
                .Include(s => s.Trainer)
                .FirstOrDefaultAsync(cancellationToken)!;
        public async Task<int> CountOfBookedSlots(int sessionId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Bookings
                .CountAsync(b => b.SessionId == sessionId, cancellationToken);
        }

        public async Task<Session> GetWithCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Sessions.Where(s => s.Id == id)
                .Include(s => s.Category)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Session> GetWithTrainerByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Sessions.Where(s => s.Id == id)
                .Include(s => s.Trainer)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
