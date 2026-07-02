

using Power_Fitness.DAL.Context;
using System.Linq.Expressions;

namespace Power_Fitness.DAL.Contracts
{
    public interface IMemberRepository:IRepository<Member>
    {
        Task<bool> AnyAsync(Expression<Func<Member, bool>> predicate,CancellationToken cancellationToken = default);

        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> PhoneExistsAsync(string phone, CancellationToken cancellationToken = default);
        Task<Membership?> GetMemberShipByMemberId(int memberId, CancellationToken cancellationToken = default);
    }
}
