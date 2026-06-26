using Power_Fitness.DAL.Context;
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

        public Task<bool> AnyAsync(Expression<Func<Member, bool>> predicate, CancellationToken cancellationToken = default)
            => _dbContext.Members.AnyAsync(predicate, cancellationToken);

        public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
            => _dbContext.Members.AnyAsync(m => m.Email == email);


        public Task<bool> PhoneExistsAsync(string phone, CancellationToken cancellationToken = default)
            => _dbContext.Members.AnyAsync(m => m.Phone == phone);
    }
}
