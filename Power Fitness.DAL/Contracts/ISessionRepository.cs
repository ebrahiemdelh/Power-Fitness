namespace Power_Fitness.DAL.Contracts
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<IEnumerable<Session>> GetSessionsWithCategoryAndTrainerAsync(CancellationToken cancellationToken = default);

        Task<int> CountOfBookedSlots(int sessionId, CancellationToken cancellationToken = default);
    }
}
