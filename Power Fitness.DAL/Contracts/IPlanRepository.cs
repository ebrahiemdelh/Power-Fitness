namespace Power_Fitness.DAL.Contracts
{
    public interface IPlanRepository:IRepository<Plan>
    {
        Task <Plan?> GetByMemberIdAsync(int memberId,CancellationToken cancellationToken=default);
    }
}
