namespace Power_Fitness.DAL.Contracts
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<IEnumerable<Session>> GetSessionsWithCategoryAndTrainerAsync(CancellationToken cancellationToken = default);
        Task<Session> GetWithCategoryByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Session> GetWithTrainerByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<int> CountOfBookedSlots(int sessionId, CancellationToken cancellationToken = default);
    }
}
