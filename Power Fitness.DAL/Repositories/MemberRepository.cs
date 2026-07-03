using Power_Fitness.DAL.Context;
using Power_Fitness.DAL.Dtos.Members;
using System.Linq.Expressions;

namespace Power_Fitness.DAL.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository, IRepository<Member>
    {
        private readonly GymDbContext _dbContext;
        public MemberRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AnyAsync(Expression<Func<Member, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbContext.Members.AnyAsync(predicate, cancellationToken);

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
            => await _dbContext.Members.AnyAsync(m => m.Email == email);


        public async Task<bool> PhoneExistsAsync(string phone, CancellationToken cancellationToken = default)
            => await _dbContext.Members.AnyAsync(m => m.Phone == phone);


        public async Task<Membership?> GetMemberShipByMemberId(int memberId, CancellationToken cancellationToken = default)
            => await _dbContext.Memberships.Where(ms => ms.MemberId == memberId)
            .Include(ms => ms.Plan)
            .FirstOrDefaultAsync(ms => ms.CreatedAt.AddDays(ms.Plan.DurationDays) <= DateTime.Today, cancellationToken);

        public async Task<PartialMemberShipData?> GetPartialMemberShipDataByMemberIdAsync(int memberId, CancellationToken cancellationToken = default)
        {
            var membershipPlan = await _dbContext.Memberships.Where(ms => ms.MemberId == memberId)
                .Include(ms => ms.Plan)
                .Select(ms => new PartialMemberShipData
                {
                    PlanName = ms.Plan.Name,
                    DurationDays = ms.Plan.DurationDays,
                    MembershipStartDate = ms.CreatedAt,
                    MembershipEndDate = ms.EndDate
                })
                .FirstOrDefaultAsync(ms => ms.MembershipStartDate.AddDays(ms.DurationDays) <= DateTime.Now, cancellationToken);
            return membershipPlan ?? null;
        }
    }
}
