namespace Power_Fitness.DAL.Contracts
{
    public interface IHealthRecordRepository : IRepository<HealthRecord>
    {
        Task<HealthRecord?> GetByMemberIdAsync(int memberId, CancellationToken cancellationToken = default);
    }
}
