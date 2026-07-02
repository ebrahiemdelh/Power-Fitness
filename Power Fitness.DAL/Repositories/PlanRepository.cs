using Power_Fitness.DAL.Context;

namespace Power_Fitness.DAL.Repositories
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly GymDbContext _dbContext;

        public PlanRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Plan?> GetByMemberIdAsync(int memberId, CancellationToken cancellationToken = default)
        {
            var membershipPlan = await _dbContext.Memberships.Where(ms => ms.MemberId == memberId)
                .Include(ms => ms.Plan)
                .FirstOrDefaultAsync(ms => ms.CreatedAt.AddDays(ms.Plan.DurationDays) <= DateTime.Now, cancellationToken);
            return membershipPlan?.Plan;
        }
    }
}
