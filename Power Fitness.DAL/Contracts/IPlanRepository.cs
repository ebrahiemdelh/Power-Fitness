namespace Power_Fitness.DAL.Contracts
{
    public interface IPlanRepository
    {
        Task<Plan> GetPLanWithMemberships(int id, CancellationToken cancellationToken = default);
    }
}
