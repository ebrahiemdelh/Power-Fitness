using Power_Fitness.DAL.Context;

namespace Power_Fitness.DAL.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;

        public PlanRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Plan> GetPLanWithMemberships(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Plans
                .Include(p => p.Memberships
                .Where(m => m.EndDate >= DateOnly.FromDateTime(DateTime.Now)))
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
