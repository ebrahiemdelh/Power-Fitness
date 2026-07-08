using Power_Fitness.BLL.ViewModels.Session;

namespace Power_Fitness.BLL.Contracts
{
    public interface ISessionService
    {
        Task<List<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default);
        Task<SessionViewModel> GetSessionByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> CreateSessionAsync(CreateSessionViewModel createSession, CancellationToken cancellationToken = default);
        Task<bool> EditSessionAsync(EditSessionViewModel editSession, CancellationToken cancellationToken = default);
        Task<bool> DeleteSessionAsync(int id, CancellationToken cancellationToken = default);

        Task<Dictionary<int, string>> GetCategories(CancellationToken cancellationToken = default);
        Task<Dictionary<int, string>> GetTrainers(CancellationToken cancellationToken = default);
    }
}
