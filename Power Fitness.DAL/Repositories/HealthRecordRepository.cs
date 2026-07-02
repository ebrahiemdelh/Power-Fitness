using Power_Fitness.DAL.Context;

namespace Power_Fitness.DAL.Repositories
{
    public class HealthRecordRepository : Repository<HealthRecord>, IHealthRecordRepository
    {
        private readonly GymDbContext _dbContext;

        public HealthRecordRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthRecord?> GetByMemberIdAsync(int memberId, CancellationToken cancellationToken = default)
        => await _dbContext.HealthRecords.FirstOrDefaultAsync(hr => hr.MemberId == memberId, cancellationToken);
    }
}
